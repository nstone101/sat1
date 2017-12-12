using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SAT1.Models; 
using System.Net.Mail;


namespace SAT1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact(ContactInfo info)
        {
            if (ModelState.IsValid)
            {
                //send the email
                MailMessage msg = new MailMessage(
                    "nick@develop3d-kc.com", //from
                    "admin@develop3d-kc.com",//to
                    "New Message from SAT", //subject
                    string.Format("Name: {0}<br />Email: {1}<br />Comment: {2}",
                    info.Name, info.Email, info.Comments));

                //there is html in the message
                msg.IsBodyHtml = true;

                //allows you to respond (when you hit reply in your mail client)
                //to the actual sender instead of your centriq hosting account
                msg.ReplyToList.Add(info.Email);

                //Centriq Hosting Mail client 
                SmtpClient client = new SmtpClient("relay-hosting.secureserver.net");

                //can only send mail when deployed
                try
                {
                    client.Send(msg);
                    ViewBag.Message = "Your email has been sent.";
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Your message could not be sent." +
                        "Please try again later...";

                    ViewBag.AdminMessage = ex.StackTrace;

                }
                return View("Thanks");
            }
            else
            {
                //validation failed return the form
                return View();
            }

        }

        
    }
}
