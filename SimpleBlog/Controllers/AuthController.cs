using SimpleBlog.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        public ActionResult Login(AuthLogin form)
        {
            if (!ModelState.IsValid)
            {
                // pass back invalid model to do again.
                return View(form);
            }
            if ( form.Username != "Rainbow dash")
            {
                ModelState.AddModelError("Username", "Username or password isn't 20% cooler");
                return View(form);
            }

            else
            {
                return Content("This! Form is Valid");
            }
        }
    }
}