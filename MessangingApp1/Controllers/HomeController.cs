using MessangingApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MessangingApp1.Controllers
{
    
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()

        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User usr)
        {
            DataContext db = new DataContext();
            if (ModelState.IsValid)
            {
                 //validate the email and password
                var res = db.users.Where(item => item.Username == usr.Username && item.Password == usr.Password).ToList();

                if (res.Count() != 0)
                {
                    Session["userid"] = res[0].UserId;
                    Session["name"] = res[0].Username;
                    
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ErrorMessage = "Invalid UserName or Password";
                    return View();
                }
            }

            return View();
        }
        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(User usr)
        {
            DataContext db = new DataContext();
            if (ModelState.IsValid)
            {
                //validate the email
                var res = db.users.Where(item => item.Username == usr.Username);

                if (res.Count() != 0)
                {
                    ViewBag.ErrorMessage = "User Already Exist!";
                    return View();
                }
                res = db.users.Where(item => item.Email== usr.Email);
                if(res.Count() != 0)
                {
                    ViewBag.ErrorMessage = "Email Already Exist!";
                    return View();
                }
                db.users.Add(usr);
                db.SaveChanges();
                return RedirectToAction("Login");
            }
            
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }

        public ActionResult Channel ()
        {
            if (Session["userid"] != null)
            {
                DataContext db = new DataContext();
                var res = db.channels.Where(item => item.UserId == Convert.ToInt32(Session["userid"])).ToList();
                if(res.Count() == 0)
                {
                    ViewBag.message = "No Channel Exist";
                    return View();
                }
                else
                {
                    return View(res);
                }
                
            }
            else
            {
                return RedirectToAction("Login");
            }
            
        }
    }
}