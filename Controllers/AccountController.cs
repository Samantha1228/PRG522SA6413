using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PRG522SA6413.Models;

namespace PRG522SA6413.Controllers
{
    public class AccountController : Controller
    {
        SAdbEntities1 db = new SAdbEntities1();
        // GET: Account
        public ActionResult Index()
        {
            return View(db.UserLogins.ToList());
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserLogin userLogin)
        {
            if (db.UserLogins.Any(x=>x.Email == userLogin.Email))
            {
                ViewBag.Notification = "This account already exists";
                return View();
            }
            else
            {
                db.UserLogins.Add(userLogin);
                db.SaveChanges();

                Session["Iduser"] = userLogin.Id.ToString();
                Session["Emailuser"] = userLogin.Email.ToString();
                return RedirectToAction("Index", "Home");

            }
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLogin userLogin)
        {
            var checklogin = db.UserLogins.Where(x => x.Email.Equals(userLogin.Email) && x.Password.Equals(userLogin.Password)).FirstOrDefault();
            if (checklogin != null)
            {
                Session["Iduser"] = userLogin.Id.ToString();
                Session["Emailuser"] = userLogin.Email.ToString();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Notification = "Wrong Email or Password.";
            }
            return View();
            
        }


    }
}