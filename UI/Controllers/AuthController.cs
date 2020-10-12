using DAL.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using UI.Models;
using UI.Utils;

namespace UI.Controllers
{
    public class AuthController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            var signInManager = HttpContext.GetOwinContext().Get<AppSignInManager>();

            var status = await signInManager.PasswordSignInAsync(model.Login, model.Password, false, false);

            if (status == SignInStatus.Success)
                return RedirectToAction("Index", "Game");
            return Content("Something went wrong...");
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var manager = HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
                var user = new User
                {
                    UserName = model.Login,
                    Email = model.Email
                };
                string errors = "";
                var result = await manager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                    return RedirectToAction("Login");
                else
                {
                    foreach (var item in result.Errors)
                        errors += item + " ";
                }
                return Content(errors);
            }
            return RedirectToAction("Register");
        }

        public ActionResult Logout()
        {
            var authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignOut();
            return RedirectToAction("Index", "Game");
        }

        public ActionResult Account(LoginViewModel model)
        {
            return View();
        }     
    }
}