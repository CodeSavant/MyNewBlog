﻿using System.Linq;
using System.Web.Mvc;
using NHibernate.Linq;
using SimpleBlog.Areas.Admin.ViewModels;
using SimpleBlog.Infrastructure;
using SimpleBlog.Models;

namespace SimpleBlog.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [SelectedTab("users")]
    public class UsersController : Controller
    {
        
        public ActionResult Index()
        {
            
                return View(new UsersIndex
                {
                   Users = Database.Session.Query<User>().ToList()
                });
                
            
        }

        public ActionResult New()
        {
            return View(new UsersNew
            {
                
            });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult New(UsersNew form)
        {
            if (Database.Session.Query<User>().Any(u => u.Username == form.Username))
            {
                ModelState.AddModelError("UserName", "UserName must be Unique");
            }
            if (!ModelState.IsValid)
            {
                return View(form);
            }

            var user = new User
            {
                Email = form.Email,
                Username = form.Username

            };

            user.SetPassword(form.Password);

            Database.Session.Save(user);

            return RedirectToAction("index");
        }


        public ActionResult Edit(int id)
        {
            var user = Database.Session.Load<User>(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            return View(new UsersEdit
            {
                Username = user.Username,
                Email = user.Email
            });

        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(int id, UsersEdit form)
        {
            var user = Database.Session.Load<User>(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            if (Database.Session.Query<User>().Any(u => u.Username == form.Username && u.Id != id))
            {
                ModelState.AddModelError("Username", "Username must be unique");
            }

            if (!ModelState.IsValid)
            {
                return View(form);
            }

            user.Username = form.Username;
            user.Email = form.Email;
            Database.Session.Update(user);


            return RedirectToAction("Index");
        }

        public ActionResult ResetPassword(int id)
        {
            var user = Database.Session.Load<User>(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            return View(new UsersResetPassword
            {
                Username = user.Username,
                
            });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult ResetPassword(int id, UsersResetPassword form)
        {
            var user = Database.Session.Load<User>(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            // this is necessary for the username to appear on the view. User != UserResetPassword
            form.Username = user.Username;

            if (!ModelState.IsValid)
            {
                return View(form);
            }

            user.SetPassword(form.Password);
            Database.Session.Update(user);


            return RedirectToAction("Index");
            
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var user = Database.Session.Load<User>(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            Database.Session.Delete(user);
            return RedirectToAction("Index");

        }
    }
}