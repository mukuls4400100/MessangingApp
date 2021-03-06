using MessangingApp1.Models;
using MessangingApp1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//forms channel,tag,inviteuser
namespace MessangingApp.Controllers
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
            if (ModelState.IsValid)
            {
                DataContext db = new DataContext();
                var res = db.channels.Where(item => item.ChannelName == c.ChannelName).ToList();
                if (res.Count() == 0)
                {
                    c.UserId = Convert.ToInt32(Session["userid"]);
                    c.CreatedAt = DateTime.Now;
                    db.channels.Add(c);
                    db.SaveChanges();

                    var r = db.channels.Where(item => item.ChannelName == c.ChannelName).ToList();
                    Session["channelid"] = r.First().ChannelId;
                    
                    TagListViewModel t = new TagListViewModel();
                    InviteListViewModel t2 = new InviteListViewModel();
                    t.TagItems.Clear();
                    t2.InviteUsers.Clear();

                    return RedirectToAction("../Tag/Index");
                }
                else
                {
                    ViewBag.message = "Channel already Exist";
                }
            }

            return View();
        }
    }
}