using AuctionHouse.Models.DisplayModels;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace AuctionHouse.Controllers
{

    public class BaseController : Controller
    {
        protected static readonly ILog logger = LogManager.GetLogger("AuctionHouseLogger");

        public static object Settings { get; private set; }

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

        public static void SendMail(string fromEmail, string fromName, string toEmail, string toName, string subject, string body)
		{
			using (SmtpClient client = new SmtpClient("smtp.gmail.com", 587))
			{
				client.EnableSsl = true;
				client.UseDefaultCredentials = false;
				client.Credentials = new NetworkCredential("momaznikolic96@gmail.com", "L33tTr4pz0r");

				using (MailMessage message = new MailMessage(new MailAddress(fromEmail, fromName, Encoding.UTF8), new MailAddress(toEmail, toName, Encoding.UTF8)))
				{
					message.Subject = subject;
					message.SubjectEncoding = Encoding.UTF8;
					message.Body = body;
					message.BodyEncoding = Encoding.UTF8;
					client.Send(message);
				}
			}
        }
    }
}