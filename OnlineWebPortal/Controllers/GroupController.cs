using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
//using MimeKit;
using OnlineWebPortal.Data;
using OnlineWebPortal.Models;
using OnlineWebPortal.ViewModels;

namespace OnlineWebPortal.Controllers
{
    [Authorize(Roles = "Admin, Leader, Member")]
    public class GroupController : Controller
    {
        public IActionResult FindGroup()
        {
            var context = new OnlineWebPortalDbContext();
            var group = context.ChurchGroups
                .Include(u => u.RegUserChurchGroups)
                .ToList();
            return View(group);
        }

        public IActionResult GroupDetail(int id)
        {
            var context = new OnlineWebPortalDbContext();
            ChurchGroup group = context.ChurchGroups.Single(g => g.ID == id);
            return View(group);
        }

        public IActionResult UserChurchGroups(int id)
        {
            var context = new OnlineWebPortalDbContext();
            var join = context.RegUserChurchGroups
                .Include(g => g.ChurchGroups)
                .Where(u => u.RegUsers.ID == id)
                .ToList();
            return View(join);
        }

        [Authorize(Roles = "Admin, Leader")]
        public IActionResult CreateGroup()
        {
            return View();
        }
        [Authorize(Roles = "Admin, Leader")]
        [HttpPost]
        public IActionResult CreateGroup(GroupCreateViewModel input)
        {
            var context = new OnlineWebPortalDbContext();
            ChurchGroup group = new ChurchGroup();
            if (ModelState.IsValid && (input.GroupName != null))
            {
                group.GroupName = input.GroupName;
                group.GroupDescription = input.GroupDescription;
                group.MeetingDay = input.MeetingDay;
                group.StartTime = input.StartTime;
                group.EndTime = input.EndTime;
                group.Location = input.Location;
                group.LeaderName = input.LeaderName;
                group.LeaderProfile = input.LeaderProfile;
                context.Add(group);
                context.SaveChanges();
                return RedirectToAction("FindGroup", "Group");
            }
            return View();
        }

        [Authorize(Roles = "Admin, Leader")]
        [HttpGet]
        public IActionResult EditGroup(int id)
        {
            var context = new OnlineWebPortalDbContext();
            var model = context.ChurchGroups.FirstOrDefault(e => e.ID == id);

            if (model == null)
            {
                return RedirectToAction("FindGroup", "Group");
            }
            return View(model);
        }
        [Authorize(Roles = "Admin, Leader")]
        [HttpPost]
        public IActionResult EditGroup(int id, GroupEditViewModel input)
        {
            var context = new OnlineWebPortalDbContext();
            var group = context.ChurchGroups.FirstOrDefault(e => e.ID == id);

            if (group != null && ModelState.IsValid)
            {
                group.GroupName = input.GroupName;
                group.GroupDescription = input.GroupDescription;
                group.MeetingDay = input.MeetingDay;
                group.StartTime = input.StartTime;
                group.EndTime = input.EndTime;
                group.Location = input.Location;
                group.LeaderName = input.LeaderName;
                group.LeaderProfile = input.LeaderProfile;
                context.SaveChanges();
                return RedirectToAction("FindGroup", "Group");
            }
            return View(group);
        }

        //Delete Group
        [Authorize(Roles = "Admin, Leader")]
        public IActionResult DeleteGroup(int id)
        {
            var context = new OnlineWebPortalDbContext();
            var groupId = context.ChurchGroups.FirstOrDefault(e => e.ID == id);
            context.ChurchGroups.Remove(groupId);
            context.SaveChanges();
            return RedirectToAction("FindGroup", "Group");
        }

        public IActionResult GroupMembers(int id)
        {
            var context = new OnlineWebPortalDbContext();
            var items = context.RegUserChurchGroups
                .Include(item => item.RegUsers)
                .Where(g => g.ChurchGroupID == id)
                .ToList();
            return View(items);
        }

        [Authorize(Roles = "Admin, Leader, RegUser")]
        public IActionResult EmailNotification(int id)
        {
            //MimeMessage message = new MimeMessage();
            //MailboxAddress from = new MailboxAddress("User", "iczcoolkeep360@gmail.com");
            //message.From.Add(from);
            //MailboxAddress to = new MailboxAddress("Leader", "iczcoolkeep@yahoo.com");
            //message.To.Add(to);
            //message.Subject = "This is email subject";
            //BodyBuilder bodyBuilder = new BodyBuilder();
            //bodyBuilder.HtmlBody = "<h1>Hello World!</h1>";
            //bodyBuilder.TextBody = "Hello World!";
            //message.Body = bodyBuilder.ToMessageBody();
            //SmtpClient client = new SmtpClient();
            //client.Connect("Mail.yahoo.com", 587, true);
            //client.Authenticate("iczcoolkeep@yahoo.com", "goldbook497");
            //client.Send(message);
            //client.Disconnect(true);
            //client.Dispose();


            var context = new OnlineWebPortalDbContext();
            var join = new RegUserChurchGroup();
            if (ModelState.IsValid)
            {
                var user = context.RegUsers.Where(u => u.FirstName == User.Identity.Name).Single();
                join.ChurchGroupID = id;
                join.RegUserID = user.ID;
                context.Add(join);
                context.SaveChanges();
                ViewData["message"] = "Registration was successful!";
                return RedirectToAction("FindGroup", "Group");
            }
            return RedirectToAction("Portal", "Home");
        }

        [Authorize(Roles = "Admin, Leader")]
        //[HttpGet]
        public IActionResult DeleteMember(int id)
        {
            var context = new OnlineWebPortalDbContext();
            //RegUser user = new RegUser();
            var join = context.RegUserChurchGroups
                .Include(u => u.RegUsers)
                .Where(u => u.ChurchGroupID == id).FirstOrDefault();
            return View(join);


            
        }

        //[Authorize(Roles = "Admin, Leader")]
        //[HttpPost]
        //public IActionResult DeleteMember(DeleteViewModel input)
        //{
        //    return View();
        //}


        [HttpGet]
        public IActionResult AddMember(int id)
        {
            var context = new OnlineWebPortalDbContext();
            var group = context.ChurchGroups.FirstOrDefault(g => g.ID == id);
            ViewBag.GroupID = group.ID;

            var users = context.RegUsers.ToList();
            return View(users);
        }

        [HttpPost]
        public IActionResult AddMember(AddGroupMemberViewModel input)
        {
            var context = new OnlineWebPortalDbContext();
            var join = new RegUserChurchGroup();
            if (ModelState.IsValid)
            {
                join.ChurchGroupID = input.ChurchGroupID;
                join.RegUserID = input.RegUserID;
                context.Add(join);
                context.SaveChanges();
                ViewData["message"] = "Registration was successful!";
                return RedirectToAction("FindGroup", "Group");
            }
            return RedirectToAction("Portal", "Home");
        }
    }
}
