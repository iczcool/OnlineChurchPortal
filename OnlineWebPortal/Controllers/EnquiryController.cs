using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineWebPortal.Data;
using OnlineWebPortal.Models;
using OnlineWebPortal.ViewModels;

namespace OnlineWebPortal.Controllers
{
    [Authorize(Roles = "Admin, Leader, Member")]
    public class EnquiryController : Controller
    {
        public IActionResult CreateEnquiry()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateEnquiry(CreateEnquiryViewModel input)
        {
            var context = new OnlineWebPortalDbContext();
            Enquiry enquiry = new Enquiry();
            if (ModelState.IsValid && (input.EnquiryType != null))
            {
                try
                {

                    enquiry.EnquiryType = input.EnquiryType;
                    enquiry.EnquiryDate = input.EnquiryDate;
                    enquiry.EnquiryBody = input.EnquiryBody;
                    enquiry.RegUser = input.RegUser;
                    context.Add(enquiry);
                    context.SaveChanges();
                    TempData["successMessage"] = " Your enquiry has been sent successfully";
                    return RedirectToAction("CreateEnquiry", "Enquiry");
                }
                catch (Exception ex)
                {
                    TempData["failMessage"] = " Oops! Something went wrong" + ex.Message;
                    return RedirectToAction("CreateEnquiry", "Enquiry");
                }

            }
            return View();
        }
        public IActionResult Detail(int id)
        {
            var context = new OnlineWebPortalDbContext();
            var enquiry = context.Enquiries.Where(e => e.ID == id).SingleOrDefault();
            return View(enquiry);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var context = new OnlineWebPortalDbContext();
            var enq = context.Enquiries.FirstOrDefault(e => e.ID == id);
            context.Enquiries.Remove(enq);
            context.SaveChanges();
            return RedirectToAction("EnquiryList", "Enquiry");
        }
        [Authorize(Roles = "Admin")]
        public IActionResult EnquiryList()
        {
            var context = new OnlineWebPortalDbContext();
            var enq = context.Enquiries.ToList();
            return View(enq);
        }
    }
}