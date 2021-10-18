using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using coreSessionManagementApplication.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using coreSessionManagementApplication.Models;

namespace coreSessionManagementApplication.Controllers
{
    public class AccountController : Controller
    {
        ApplicationDBContext context;
        public AccountController()
        {
            context = new ApplicationDBContext();
        }

        public object Context { get; private set; }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            var userObj = context.Users.Where(u => u.Username == user.Username 
            && u.Password == user.Password
            ).SingleOrDefault();
            if (userObj != null)
            {
                SessionHelper.setObjectAsJson(HttpContext.Session, "user", userObj);
                User usr = SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "user");
                HttpContext.Session.SetString("usertype", usr.UserType);
                HttpContext.Session.SetString("username", usr.Username);
                if (usr.UserType == "admin")
                {
                    return RedirectToAction("Index", "Home");
                }
                return RedirectToAction("Index", "Home");
                
                
            }
            else
            {
                ViewBag.Error = "Please Enter Your Credentials.";
                return View("Index");
            }
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("UserId,Username,Password,UserAddedDate,UserType")]  User user)
        {
            if (ModelState.IsValid)
            {
                context.Add(user);
                await context.SaveChangesAsync();
            }
            return View("Index");
        }
    }
}
