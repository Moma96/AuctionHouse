using AuctionHouse.Models;
using AuctionHouse.Models.Tables;
using AuctionHouse.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AuctionHouse.Controllers
{
    public class AdministratorController : BaseController
    {

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSystemParameters(SystemParametersViewModel SPView)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new Exception("All fields must be filled correctly!");

                if (IsAdmin() == null)
                    throw new Exception("Forbidden access!");

                if (SPView.S >= SPView.G || SPView.G >= SPView.P)
                    throw new Exception("Gold package must be greater than Silver and Platinum must be greater then Gold!");

                using (AuctionHouseModel db = new AuctionHouseModel())
                {
                    SystemParameter sp = db.GetSystemParameters();

                    sp.N = SPView.N;
                    sp.D = SPView.D;
                    sp.S = SPView.S;
                    sp.G = SPView.G;
                    sp.P = SPView.P;
                    sp.C = SPView.C;
                    sp.T = SPView.T;

                    db.SaveChanges();
                }
            }
            catch (Exception error)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, error.Message);
            }
            return null;
        }

        /*[HttpPost]
        ActionResult EditSystemParameters(SystemParametersViewModel SPView)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new Exception("All fields must be filled correctly!");
                
                if (IsAdmin() == null)
                    throw new Exception("Forbidden access!");

                if (SPView.S >= SPView.G || SPView.G >= SPView.P)
                    throw new Exception("Gold package must be greater than Silver and Platinum must be greater then Gold!");

                using (AuctionHouseModel db = new AuctionHouseModel())
                {
                    SystemParameter sp = db.GetSystemParameters();

                    sp.N = SPView.N;
                    sp.D = SPView.D;
                    sp.S = SPView.S;
                    sp.G = SPView.G;
                    sp.P = SPView.P;
                    sp.C = SPView.C;
                    sp.T = SPView.T;

                    db.SaveChanges();
                }
            }
            catch (Exception error)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, error.Message);
            }
            return null;
        }*/
    }
}