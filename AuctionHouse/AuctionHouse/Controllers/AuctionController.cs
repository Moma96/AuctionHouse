using AuctionHouse.Models;
using AuctionHouse.Models.DisplayModels;
using AuctionHouse.Models.Tables;
using AuctionHouse.Models.ViewModels;
using AuctionHouse.SignalR;
using log4net;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AuctionHouse.Controllers
{
    public class AuctionController : BaseController
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            using (AuctionHouseModel db = new AuctionHouseModel())
            {
                PartialSystemParameters sp = db.GetPartialSystemParameters();
                var auctions = SearchAuctions(new SearchAuctionsViewModel());
                ViewBag.Page_size = sp.N;
                ViewBag.Auctions = auctions;

                Session["parameters"] = sp;
            }

            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult SearchAuctions(SearchAuctionsViewModel auctionView)
        {
            PartialUser logged_user = IsLoggedIn();

            using (AuctionHouseModel db = new AuctionHouseModel())
            {
                string owned = null;
                string won = null;
                string state = null;
                switch (auctionView.Filter)
                {
                    case SearchAuctionsViewModel.FilterEnum.OWNED:
                        if (logged_user != null) owned = logged_user.email;
                        break;
                    case SearchAuctionsViewModel.FilterEnum.WON:
                        if (logged_user != null) won = logged_user.email;
                        break;
                    default:
                        state = auctionView.Filter.ToString();
                        break;
                }
                int admin_logged_in = 0;
                if (logged_user != null) admin_logged_in = logged_user.is_administrator;

                var auctions = db.GetAuctionsWithLastBid(1000, 0,
                                              auctionView.Regex, state,
                                              auctionView.Max_price, auctionView.Min_price,
                                              won, owned,
                                              admin_logged_in);

                return Json(auctions, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAuction(CreateAuctionViewModel auctionView)
        {
            using (AuctionHouseModel db = new AuctionHouseModel())
            {
                using (var transaction = db.Database.BeginTransaction(IsolationLevel.Serializable))
                {
                    try
                    {
                        if (!ModelState.IsValid)
                            throw new Exception("All fields must be filled correctly!");

                        PartialUser logged_user = IsLoggedIn();
                        if (logged_user == null)
                            throw new Exception("Forbidden access!");

                        if (auctionView.Image == null) throw new Exception("File was not uploaded");

                        var postedFileExtension = Path.GetExtension(auctionView.Image.FileName);
                        if (!string.Equals(postedFileExtension, ".png", StringComparison.OrdinalIgnoreCase))
                            throw new Exception("Wrong image type: .png is required type!");

                        Guid guid = Guid.NewGuid();
                
                        Auction auction = new Auction
                        {
                            id = guid,
                            name = auctionView.Name,
                            description = auctionView.Description,
                            starting_price = auctionView.Starting_price,
                            duration = auctionView.Days * 60 * 60 * 24 + auctionView.HH * 60 * 60 + auctionView.MM * 60 + auctionView.SS,
                            created = DateTime.Now,
                            owner = logged_user.email,
                            state = "READY"
                        };
                        db.Auctions.Add(auction);
                        db.SaveChanges();
                        transaction.Commit();

                        string path = Path.Combine(Server.MapPath("~/Images"), guid.ToString() + ".png");
                        auctionView.Image.SaveAs(path);
                    }
                    catch (Exception error)
                    {
                        transaction.Rollback();
                        TempData["error"] = error.Message;
                        logger.Error("ERROR: " + error.Message);
                        return RedirectToAction("Index");
                    }
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Bid(BidViewModel bidView)
        {
            using (AuctionHouseModel db = new AuctionHouseModel())
            {
                using (var transaction = db.Database.BeginTransaction(IsolationLevel.Serializable))
                {
                    try
                    {
                        if (!ModelState.IsValid)
                            throw new Exception("All fields must be filled correctly!");

                        PartialUser logged_user = IsLoggedIn();
                        if (logged_user == null)
                            throw new Exception("Forbidden access!");
                        
                        Guid auction_id = new Guid(bidView.Auction_id);

                        if (logged_user.email == db.GetAuction(auction_id).owner)
                            throw new Exception("You can not bid your own auction!");

                        Bid last_bid = db.GetLastBid(auction_id);
                        if (bidView.Amount <= (last_bid != null ? last_bid.amount : db.GetAuction(auction_id).starting_price))
                            throw new Exception("Your bidding amount must be greater then the last one!");

                        if (db.GetAvailableTokens(logged_user.email) < bidView.Amount)
                            throw new Exception("You have not enough tokens to procceed with the transaction!");

                        Auction auction = db.GetAuction(new Guid(bidView.Auction_id));
                        if (auction.opened.Value.AddSeconds(auction.duration) < DateTime.Now)
                            throw new Exception("Auction exipred!");
                    
                        Bid bid = new Bid
                        {
                            id = Guid.NewGuid(),
                            auction_id = auction_id,
                            bidder = logged_user.email,
                            created = DateTime.Now,
                            amount = bidView.Amount
                        };
                        db.Bids.Add(bid);
                        db.SaveChanges();
                        transaction.Commit();

                        string name = logged_user.first_name + " " + logged_user.last_name;
                        AuctionHouseHub.HubContext.Clients.All.updatebid(logged_user.email, name, bidView.Auction_id, bidView.Amount, bid.created.ToString());
                    }
                    catch (Exception error)
                    {
                        transaction.Rollback();
                        logger.Error("ERROR: " + error.Message);
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest, error.Message);
                    }
                }
            }
            
            return null;
        }

        [HttpPost]
        public ActionResult Approve(string id)
        {
            try
            {
                if (IsAdmin() == null)
                    throw new Exception("Only administrator is allowed to approve auctions!");

                using (AuctionHouseModel db = new AuctionHouseModel())
                {
                    Auction auction = db.GetAuction(new Guid(id));
                    auction.state = "OPENED";
                    auction.opened = DateTime.Now;
                    db.SaveChanges();
                }
            }
            catch (Exception error)
            {
                logger.Error("ERROR: " + error.Message);
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, error.Message);
            }
            return null;
        }

        [HttpGet]
        public ActionResult AuctionDetails(string id)
        {
            if (id == null || id == "") return RedirectToAction("Index");
            using (AuctionHouseModel db = new AuctionHouseModel())
            {
                AuctionAllBids auction = db.GetAuctionWithAllBids(new Guid(id));
                ViewBag.Auction = auction;
                return View();
            }
        }
    }
}