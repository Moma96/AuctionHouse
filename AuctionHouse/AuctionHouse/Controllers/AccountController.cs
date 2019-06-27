using AuctionHouse.Models;
using AuctionHouse.Models.DisplayModels;
using AuctionHouse.Models.Tables;
using AuctionHouse.Models.ViewModels;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AuctionHouse.Controllers
{
    public class AccountController : BaseController
    {
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel loginView)
        {
            try {
                if (!ModelState.IsValid)
                    throw new Exception("All fields must be filled correctly!");

                using (AuctionHouseModel db = new AuctionHouseModel())
                {
                    User user = db.FindUser(loginView.Email);
                    if (user == null)
                        throw new Exception("There is no user with that e-mail.");

                    if (user.password != EncodePassword(loginView.Password))
                        throw new Exception("Wrong password.");
                            
                    Session["user"] = new PartialUser(user);
                }
            }
            catch (Exception error)
            {
                logger.Error("ERROR: " + error.Message);
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, error.Message);
            }
            return PartialView("_Header");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel registerView)
        {
            try {
                if (!ModelState.IsValid)
                    throw new Exception("All fields must be filled correctly!");

                using (AuctionHouseModel db = new AuctionHouseModel())
                    {
                        if (db.FindUser(registerView.Email) != null)
                            throw new Exception("There is already user with that e-mail.");

                        User user = new User
                        {
                            email = registerView.Email,
                            password = EncodePassword(registerView.Password),
                            first_name = registerView.First_name,
                            last_name = registerView.Last_name
                        };
                        db.Users.Add(user);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OrderTokens(OrderTokensViewModel orderView)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new Exception("All fields must be filled correctly!");

                PartialUser logged_user = IsLoggedIn();
                if (logged_user == null)
                    throw new Exception("Forbidden access!");
                
                using (AuctionHouseModel db = new AuctionHouseModel())
                {
                    SystemParameter sp = db.GetSystemParameters();

                    float package = 0;
                    switch (orderView.Package)
                    {
                        case (OrderTokensViewModel.PackageEnum.SILVER):
                            package = sp.S;
                            break;
                        case (OrderTokensViewModel.PackageEnum.GOLD):
                            package = sp.G;
                            break;
                        case (OrderTokensViewModel.PackageEnum.PLATINUM):
                            package = sp.P;
                            break;
                    }
                    
                    TokenOrder order = new TokenOrder
                    {
                        id = Guid.NewGuid(),
                        orderer = logged_user.email,
                        amount = package,
                        price = package * sp.T, 
                        state = "SUBMITTED"
                    };

                    db.TokenOrders.Add(order);
                    db.SaveChanges();

                    HttpStatusCodeResult service_result = TokenWebService(order.id);
                    if (service_result.StatusCode != 0xca)
                        throw new Exception(service_result.ToString());

                    User user = db.FindUser(logged_user.email);
                    user.tokens_amount += order.amount;
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

        //SIMULATED TOKEN WEB SERVICE
        private HttpStatusCodeResult TokenWebService(Guid id)
        {
            try
            {
                PartialUser logged_user = IsLoggedIn();
                if (logged_user == null)
                    throw new Exception("Forbidden access!");

                using (AuctionHouseModel db = new AuctionHouseModel())
                {
                    TokenOrder order = db.GetTokenOrder(id);
                    if (order.state == "SUBMITTED")
                    {
                        order.state = "COMPLETED";
                        db.SaveChanges();
                        /*SendMail("momaznikolic96@gmail.com", "Momcilo Nikolic",
                                logged_user.email, logged_user.first_name + logged_user.last_name,
                                "Token order result",
                                "Your order for " + order.amount + " tokens have been successfully completed!");*/
                    }
                    else
                    {
                        order.state = "CANCELED";
                        db.SaveChanges();
                       /* SendMail("momaznikolic96@gmail.com", "Momcilo Nikolic",
                                 logged_user.email, logged_user.first_name + logged_user.last_name,
                                 "Token order result",
                                 "Your order for " + order.amount + " tokens have been canceled due to error");*/
                        throw new Exception("Error occured: Token order is invalid!");
                    }
                }
            }
            catch (Exception error)
            {
                logger.Error("ERROR: " + error.Message);
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, error.Message);
            }
            return new HttpStatusCodeResult(HttpStatusCode.Accepted);
        }


        public ActionResult Logout()
        {
            Session["user"] = null;
            return PartialView("_Header");
        }

        [HttpGet]
        public ActionResult AccountDetails()
        {
            try
            {
                PartialUser logged_user = IsLoggedIn();
                if (logged_user == null)
                    throw new Exception("Forbidden access!");

                using (AuctionHouseModel db = new AuctionHouseModel())
                {
                    UserDetails user = db.GetUserDetails(logged_user.email);
                    PartialSystemParameters sp = db.GetPartialSystemParameters();
                    ViewBag.User = user;
                    ViewBag.SystemParams = sp;
                    return View();
                }
            }
            catch (Exception error) 
            {
                logger.Error("ERROR: " + error.Message);
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordViewModel changePassView)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new Exception("All fields must be filled correctly!");

                PartialUser logged_user = IsLoggedIn();
                if (logged_user == null)
                    throw new Exception("Forbidden access!");

                using (AuctionHouseModel db = new AuctionHouseModel())
                {
                    User user = db.FindUser(logged_user.email);

                    if (user.password != EncodePassword(changePassView.ConfirmPassword))
                        throw new Exception("Wrong password.");

                    user.password = EncodePassword(changePassView.NewPassword);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeName(ChangeNameViewModel changeNameView)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new Exception("All fields must be filled correctly!");

                PartialUser logged_user = IsLoggedIn();
                if (logged_user == null)
                    throw new Exception("Forbidden access!");

                using (AuctionHouseModel db = new AuctionHouseModel())
                {
                    User user = db.FindUser(logged_user.email);

                    user.first_name = changeNameView.New_first_name;
                    user.last_name = changeNameView.New_last_name;

                    db.SaveChanges();
                }
            }
            catch (Exception error)
            {
                logger.Error("ERROR: " + error.Message);
                return RedirectToAction("Index");
            }
            return RedirectToAction("AccountDetails");
        }
    }
}