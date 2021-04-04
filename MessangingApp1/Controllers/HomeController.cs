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
            if (Session["userid"] != null)
            {
              TrendingListViewModel tr = new TrendingListViewModel();
              DataContext db = new DataContext();
              var list = (from p in db.users
                          join f in db.posts
                          on p.Username equals f.UserName into trend
                          from x in trend.DefaultIfEmpty()
                          select new 
                          {
                            Name = p.Region,
                            Count = x == null?0:1
                          }).GroupBy(o=>o.Name).Select(o=> new Trending
                          { 
                              Name = o.Key, Count = o.Sum(p=>p.Count)
                          }).OrderByDescending(c => c.Count).ToList().Take(5);

                tr.trendingRegions = (IEnumerable<Trending>)list;

                var list1 = (from p in db.users
                            join f in db.posts
                            on p.Username equals f.UserName into trend
                            from x in trend.DefaultIfEmpty()
                            select new 
                            {
                                Name = p.Username,
                                Count = x == null ? 0 : 1
                            }).GroupBy(o => o.Name).Select(o => new Trending
                            {
                                Name = o.Key,
                                Count = o.Sum(p => p.Count)
                            }).OrderByDescending(c => c.Count).ToList().Take(5);

                tr.trendingUser = (IEnumerable<Trending>)list1;

                var list2 = (from p in db.channels
                             join f in db.posts
                             on p.ChannelId equals f.ChannelId into trend
                             from x in trend.DefaultIfEmpty()
                             group x by new { p.ChannelName } into r
                             select new Trending
                             {
                                 Name = r.Key.ChannelName,
                                 Count = r.Count()
                             }).OrderByDescending(c => c.Count).ToList().Take(5);

                tr.trendingChannels = (IEnumerable<Trending>)list2;

                var list3 = (from p in db.tags
                             group p by new { p.TagName } into r
                             select new Trending
                             {
                                 Name = r.Key.TagName,
                                 Count = r.Count()
                             }).OrderByDescending(c => c.Count).ToList().Take(5);

                tr.trendingTags = (IEnumerable<Trending>)list3;

                return View(tr);
            }
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
                var res1 = db.users.Where(item => item.Username == usr.Username).ToList();

                if (res.Count() != 0)
                {
                    Session["userid"] = res[0].UserId;
                    Session["name"] = res[0].Username;

                    return RedirectToAction("InviteChannels");
                }
                else if(res1.Count() == 0)
                {
                    ViewBag.ErrorMessage = "Not have an account";
                    return View();
                }
                else
                {
                    ViewBag.ErrorMessage = "Invalid password";
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
                        List<Tag> channeltags = (List<Tag>)db.tags.Where(item => item.ChannelId == i.ChannelId).ToList();
                        i.tags = channeltags;
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