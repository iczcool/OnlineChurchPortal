using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineWebPortal.Data;
using OnlineWebPortal.Models;
using OnlineWebPortal.ViewModels;

namespace OnlineWebPortal.Controllers
{
    [Authorize(Roles = "Admin, Leader, Member")]
    public class GivingController : Controller
    {
        public IActionResult OnlineGiving()
        {
            var context = new OnlineWebPortalDbContext();
            var user = context.RegUsers.FirstOrDefault();
            var name = user.FirstName + " " + user.LastName;
            var usr = context.RegUsers.FirstOrDefault(u => name == User.Identity.Name);
            return View(usr);
        }
        public IActionResult ScheduleGiving()
        {
            return View();
        }
        public IActionResult Statement()
        {
            var context = new OnlineWebPortalDbContext();
            var user = User.Identity.Name;
            var usr = context.RegUsers.Where(u => u.FirstName + " " + u.LastName == user).SingleOrDefault();
            IList<Payment> payments = context.Payments
                .Include(p => p.RegUser)
                .Where(u => u.RegUserID == usr.ID)
                .ToList();
            return View(payments);
        }

        //Create a payment
        [HttpGet]
        public IActionResult CreateGiving()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateGiving(CreatePaymentViewModel input)
        {
            var context = new OnlineWebPortalDbContext();
            Payment payment = new Payment();
            if (ModelState.IsValid && (input.PaymentType != null))
            {

                var usr = User.Identity.Name;
                var user = context.RegUsers.Where(u => u.FirstName + " " + u.LastName == usr).SingleOrDefault();


                payment.PaymentType = input.PaymentType;
                payment.PaymentDate = input.PaymentDate;
                payment.Amount = input.Amount;
                payment.RegUserID = user.ID;
                //payment.RegUserID = 2;


                context.Add(payment);
                context.SaveChanges();
                return RedirectToAction("OnlineGiving", "Giving");
            }
            return View();
        }
        [Authorize(Roles = "Admin")]
        public IActionResult ListOfStatements()
        {
            var context = new OnlineWebPortalDbContext();
            //var user = User.Identity.Name;
            //var usr = context.RegUsers.Where(u => u.FirstName + " " + u.LastName == user).SingleOrDefault();
            var pay = context.Payments
                .Include(p => p.RegUser)
                .ToList();
            return View(pay);
        }

        public IActionResult StatementDetail(int id)
        {
            var context = new OnlineWebPortalDbContext();
            //var usr = context.RegUsers.Where(u => u.ID == id).SingleOrDefault();
            IList<Payment> payment = context.Payments
                .Include(p => p.RegUser)
                .Where(u => u.ID == id)
                .ToList();
            return View(payment);
        } 
    }
}
