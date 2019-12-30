using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using OnlineWebPortal.Models;
using OnlineWebPortal.ViewModels;
using OnlineWebPortal.Data;
using Microsoft.AspNetCore.Authorization;

namespace OnlineWebPortal.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Register(RegisterViewModel input)
        {
            var db = new OnlineWebPortalDbContext();
            if (ModelState.IsValid)
            {
                RegUser user = new RegUser();
                user.FirstName = input.FirstName;
                user.LastName = input.LastName;
                user.Username = input.Username;
                user.Password = input.Password;
                user.Roles = "Member";
                user.Sex = input.Sex;
                user.MaritalStatus = input.MaritalStatus;
                user.DateOFBirth = input.DateOFBirth;
                user.PhoneNumber = input.PhoneNumber;
                user.Email = input.Email;
                user.MemType = input.MemType;
                user.DateOfMembership = input.DateOfMembership;

                try
                {
                    db.RegUsers.Add(user);
                    db.SaveChanges();
                    TempData["message"] = "Registration was successful!";
                    if (User.Identity.IsAuthenticated == true) {
                        return RedirectToAction("Users", "User");
                    }
                    else { return RedirectToAction("Login", "Account"); }
                }
                catch (Exception ex)
                {
                    TempData["message"] = " Oops! We have a problem here" + ex.Message;
                    return RedirectToAction("Register", "Account");
                }
            }
            return View();
        }
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(LoginViewModel input)
        {
            var db = new OnlineWebPortalDbContext();
            bool isUservalid = false;

            RegUser user = db.RegUsers.Where(u => u.Username == input.UserName && u.Password == input.Password).SingleOrDefault();

            if (user != null)
            {
                isUservalid = true;
            }

            if (ModelState.IsValid && isUservalid)
            {
                var names = user.FirstName + " " + user.LastName;
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, names));

                string[] roles = user.Roles.Split(",");
                foreach (string role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var props = new AuthenticationProperties();
                props.IsPersistent = input.RememberMe;
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props).Wait();
                return RedirectToAction("Portal", "Home");
            }
            else
            {
                ViewData["message"] = "Login attempt failed. Verify your information and try again!";
            }
            return View(user);
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }


        //Edit account
        [HttpGet]
        public IActionResult EditAccount(int id)
        {
            var context = new OnlineWebPortalDbContext();
            var user = context.RegUsers.FirstOrDefault(e => e.ID == id);
            if (user == null)
            {
                return RedirectToAction("UserProfile", "User");
            }
            return View(user);
        }

        [HttpPost]
        public IActionResult EditAccount(int id, RegisterViewModel input)
        {
            var context = new OnlineWebPortalDbContext();
            var user = context.RegUsers.FirstOrDefault(e => e.ID == id);

            if (user != null && ModelState.IsValid)
            {
                user.FirstName = input.FirstName;
                user.LastName = input.LastName;
                user.Username = input.LastName;
                user.Password = input.Password;
                user.Sex = input.Sex;
                user.MaritalStatus = input.MaritalStatus;
                user.DateOFBirth = input.DateOFBirth;
                user.PhoneNumber = input.PhoneNumber;
                user.Email = input.Email;
                user.MemType = input.MemType;
                user.DateOfMembership = input.DateOfMembership;
                context.SaveChanges();
                return RedirectToAction("Portal", "Home");
            }
            return View(user);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult DeleteUserAccount(int id)
        {
            var context = new OnlineWebPortalDbContext();
            try
            {
                var acc = context.RegUsers.SingleOrDefault(a => a.ID == id);
                context.RegUsers.Remove(acc);
                context.SaveChanges();
                TempData["message"] = "Member has been deleted!";
                return RedirectToAction("Portal", "Home");
            }
            catch (Exception ex)
            {
                TempData["message"] = " Oops! We have a problem here" + ex.Message;
                return RedirectToAction("Users", "User");
            }
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            if (User.Identity.IsAuthenticated) {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
    }
}
