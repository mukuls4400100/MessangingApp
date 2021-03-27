using MessangingApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MessangingApp1.ViewModels;

namespace MessangingApp1.Controllers
{
    public class ChannelController : Controller
    {
        // GET: Channel
        public ActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Index(Channel c)
        {
            DataContext db = new DataContext();
            var res = db.channels.Where(item => item.ChannelName == c.ChannelName).ToList();
            if(res.Count() == 0)
            {
                c.UserId = Convert.ToInt32(Session["userid"]);
                db.channels.Add(c);
                db.SaveChanges();

                var r= db.channels.Where(item=>item.ChannelName == c.ChannelName).ToList();
                Session["channelid"] = r.First().ChannelId;
                TagListViewModel t = new TagListViewModel();
                InviteListViewModel t2 = new InviteListViewModel();
                t.TagItems.Clear();
                t2.InviteUsers.Clear();

                ViewBag.message = "Channel is Created";
            }
            else
            {
                ViewBag.message = "Channel already Exist";
            }
            
            return View();
        }
    }
}