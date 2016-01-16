using SimpleBlog.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using NHibernate.Linq;
using SimpleBlog.Models;

namespace SimpleBlog.Controllers
{
    public class AuthController : Controller
    {
        // GET: Auth
        
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(AuthLogin form, string returnUrl)
        {

            var user = Database.Session.Query<User>().FirstOrDefault(u => u.Username == form.Username);

            if (user == null)
               SimpleBlog.Models.User.FakeHash();
            

            if (user == null || !user.CheckPassword(form.Password))
                ModelState.AddModelError("Username", "Username or password is incorrect");
            

            if (!ModelState.IsValid)
            {
                // pass back invalid model to do again.
                return View(form);
            }

            FormsAuthentication.SetAuthCookie(user.Username, true);
         

            if (!string.IsNullOrWhiteSpace(returnUrl))
                return Redirect(returnUrl);

            return RedirectToRoute("home");
            
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToRoute("home");
        }
    }
}