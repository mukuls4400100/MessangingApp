using MessangingApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MessangingApp1.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace MessangingApp1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            /*DataContext db = new DataContext();
            TrendingListViewModel tr = new TrendingListViewModel();
            tr.trendingRegions = db.users.FromSqlRaw("Select Region, Count(UserId) as 'Count Of User' from users group by Region orderBy Count(UserId) desc").ToList();
           */
            /*var regions = db.users.GroupBy(item=> item.Region).OrderByDescending()*/
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
                res = db.users.Where(item => item.Email == usr.Email);
                if (res.Count() != 0)
                {


                    ViewBag.ErrorMessage = "Email Already Exist!";
                    return View();
                }
                usr.CreatedAt = DateTime.Now;
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

        public ActionResult Channel()
        {
            if (Session["userid"] != null)
            {
                DataContext db = new DataContext();
                var res = db.channels.Where(item => item.UserId == Convert.ToInt32(Session["userid"])).ToList();

                if (res.Count() == 0)
                {
                    ViewBag.message = "No Channel Exist";
                    return View();
                }
                else
                {
                    foreach(var i in res)
                    {
                        var data = db.posts.Where(item => item.ChannelId == i.ChannelId).ToList();
                        i.CountPosts = data.Count();
                        db.SaveChanges();
                    }
                    return View(res);
                }

            }
            else
            {
                return RedirectToAction("Login");
            }

        }

        public ActionResult InviteChannels()
        {
            DataContext db = new DataContext();
            var list = db.inviteUsers.Where(item => item.InviteUserName == Convert.ToString(Session["name"])).ToList();
            List<Channel> channellist = new List<Channel>();
            foreach (var i in list)
            {
                //channellist.Add(db.channels.Where(item => item.ChannelId == i.ChannelId).First());
                var res = db.channels.Where(item => item.ChannelId == i.ChannelId).First();
                var posts = db.posts.Where(item => item.ChannelId == res.ChannelId).ToList();
                res.CountPosts = posts.Count();
                db.SaveChanges();
                channellist.Add(res);
            }
            
            
            return View(channellist);
        }
    }
}