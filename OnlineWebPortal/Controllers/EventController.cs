using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineWebPortal.Data;
using OnlineWebPortal.Models;
using OnlineWebPortal.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineWebPortal.Controllers
{
    public class EventController : Controller
    {
        public IActionResult EventList()
        {
            var context = new OnlineWebPortalDbContext();
            var eve = context.Events.ToList();
            return View(eve);
        }

        public IActionResult CreateEvent()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateEvent(CreateEventViewModel input)
        {
            var context = new OnlineWebPortalDbContext();
            Event myEvent = new Event();
            if (ModelState.IsValid)
            {
                try
                {
                    myEvent.EventDate = input.EventDate;
                    myEvent.EventName = input.EventName;
                    myEvent.Description = input.Description;
                    context.Add(myEvent);
                    context.SaveChanges();
                    TempData["successMessage"] = " Event has been created successfully";
                    return RedirectToAction("EventList", "Event");
                }
                catch (Exception ex)
                {
                    TempData["failMessage"] = " Oops! Something went wrong" + ex.Message;
                    return RedirectToAction("EventList", "Event");
                }

            }
            return View();

        }

        public IActionResult EventDetail(int id)
        {
            var context = new OnlineWebPortalDbContext();
            var myEvent = context.Events.Where(e => e.ID == id).SingleOrDefault();
            return View(myEvent);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var context = new OnlineWebPortalDbContext();
            var eventId = context.Events.FirstOrDefault(e => e.ID == id);
            context.Events.Remove(eventId);
            context.SaveChanges();
            return RedirectToAction("EventList", "Event");
        }
    }
}
