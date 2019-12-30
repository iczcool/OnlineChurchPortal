using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineWebPortal.Data;
using OnlineWebPortal.Models;
using OnlineWebPortal.Paging;
using OnlineWebPortal.ViewModels;

namespace Online_Web_Portal.Views.Admin
{
    [Authorize(Roles = "Admin, Leader, Member")]
    public class UserController : Controller
    {
        /*########## USER ############*/
        //Get all users 
        [Authorize(Roles = "Admin")]
        public IActionResult Users(string sortField, string currentSortField, string currentSortOrder, string currentFilter, string SearchString,
          int? pageNum)
        {
            var context = new OnlineWebPortalDbContext();
            var users = context.RegUsers
                .ToList();
            if (SearchString != null)
            {
                pageNum = 1;
            }
            else
            {
                SearchString = currentFilter;
            }
            ViewData["CurrentSort"] = sortField;
            ViewBag.CurrentFilter = SearchString;
            if (!String.IsNullOrEmpty(SearchString))
            {
                users = users.Where(u => u.FirstName.Contains(SearchString)).ToList();
            }
            int pageSize = 10;
            return View(Paging<RegUser>.CreateAsync(users.AsQueryable<RegUser>(), pageNum ?? 1, pageSize));
        }
        IList<RegUser> SortUsers(IList<RegUser> users, string sortField, string currentSortField, string currentSortOrder)
        {
            if (string.IsNullOrEmpty(sortField))
            {
                ViewBag.SortField = "FirstName";
                ViewBag.SortOrder = "Asc";
            }
            else
            {
                if (currentSortField == sortField)
                {
                    ViewBag.SortOrder = currentSortOrder == "Asc" ? "Desc" : "Asc";
                }
                else
                {
                    ViewBag.SortOrder = "Asc";
                }
                ViewBag.SortField = sortField;
            }

            var propertyInfo = typeof(RegUser).GetProperty(ViewBag.SortField);
            if (ViewBag.SortOrder == "Asc")
            {
                users = users.OrderBy(s => propertyInfo.GetValue(s, null)).ToList();
            }
            else
            {
                users = users.OrderByDescending(s => propertyInfo.GetValue(s, null)).ToList();
            }
            return users;
        }

        //Get a user based on their id
        public IActionResult UserProfile(int id)
        {
            var context = new OnlineWebPortalDbContext();
            var user = context.RegUsers
            .Where(u => u.ID == id).SingleOrDefault();
            return View(user);
        }

        

        //Create a user
        //public IActionResult CreateUserInformation()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public IActionResult CreateUserInformation(UserInformationCreateViewModel input)
        //{
        //    var context = new OnlineWebPortalDbContext();
        //    UserInformation user = new UserInformation();
        //    if (ModelState.IsValid && (input != null))
        //    {
        //        user.Sex = input.Sex;
        //        user.MaritalStatus = input.MaritalStatus;
        //        user.DateOFBirth = input.DateOFBirth;
        //        user.PhoneNumber = input.PhoneNumber;
        //        user.Email = input.Email;
        //        user.MemType = input.MemType;
        //        user.DateOfMembership = input.DateOfMembership;
        //        context.Add(user);
        //        context.SaveChanges();
        //        return RedirectToAction("Portal", "Home");
        //    }
        //    return View();
        //}

        


        

       
    }
}




