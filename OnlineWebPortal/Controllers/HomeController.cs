using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
//using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using MimeKit;
//using MimeKit.Text;
using OnlineWebPortal.Data;
using OnlineWebPortal.Models;
using OnlineWebPortal.ViewModels;

namespace Online_Web_Portal.Controllers
{
    [Authorize(Roles = "Admin, Leader, Member")]
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Portal()
        {
            var context = new OnlineWebPortalDbContext();
            var loggedUser = User.Identity.Name;
            var usr = context.RegUsers.Where(u => (u.FirstName +" "+ u.LastName) == loggedUser).SingleOrDefault();
            return View(usr);
        }

        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Contact(ContactViewModel input)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ////instantiate a new MimeMessage
                    //var message = new MimeMessage();
                    ////Setting the To e-mail address
                    //message.To.Add(new MailboxAddress("Isaa Tutu", "iczcoolkeep@yahoo.com"));
                    ////Setting the From e-mail address
                    //message.From.Add(new MailboxAddress("E-mail From Name", "from@domain.com"));
                    ////E-mail subject 
                    //message.Subject = input.Subject;
                    ////E-mail message body
                    //message.Body = new TextPart(TextFormat.Html)
                    //{
                    //    Text = input.Message + " Message was sent by: " + input.Name + " E-mail: " + input.Email
                    //};

                    ////Configure the e-mail
                    //using (var emailClient = new SmtpClient())
                    //{
                    //    emailClient.Connect("smtp.mail.yahoo.com", 587, false);
                    //    emailClient.Authenticate("iczcoolkeep@yahoo.com", "goldbook497");
                    //    emailClient.Send(message);
                    //    emailClient.Disconnect(true);
                    //}
                }
                catch (Exception ex)
                {
                    ModelState.Clear();
                    ViewBag.Message = $" Oops! We have a problem here {ex.Message}";
                }
            }
            return View();
        }

        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Payment()
        {
            var context = new OnlineWebPortalDbContext();
            List<Payment> payments = context.Payments.ToList();
            return View(payments);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
