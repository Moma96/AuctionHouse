using AuctionHouse.Models.DisplayModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace AuctionHouse.Controllers
{
    public class BaseController : Controller
    {
        public PartialUser IsLoggedIn()
        {
            if (Session["user"] == null)
                return null;
            return (PartialUser)Session["user"];
        }

        public PartialUser IsAdmin()
        {
            PartialUser user = IsLoggedIn();
            if (user == null ||
                user.is_administrator == 0)
                return null;
            return user;
        }

        public static string EncodePassword(string password)
        {
            Byte[] originalBytes;
            Byte[] encodedBytes;
            MD5 md5;

            md5 = new MD5CryptoServiceProvider();
            originalBytes = ASCIIEncoding.Default.GetBytes(password);
            encodedBytes = md5.ComputeHash(originalBytes);

            return BitConverter.ToString(encodedBytes);
        }
    }
}