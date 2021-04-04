using MessangingApp1.Models;
using MessangingApp1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MessangingApp1.Controllers
{
    public class InviteUserController : Controller
    {
        // GET: InviteUser
        public ActionResult Index()
        {

            DataContext db = new DataContext();
            List<InviteUser> res = db.inviteUsers.Where(item => item.ChannelId == Convert.ToInt32(Session["channelid"])).ToList();

            InviteListViewModel viewModel = new InviteListViewModel();
            /*changes~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
            /* viewModel.InviteUsers.Clear();*/
            viewModel.InviteUsers = res;

            return View(viewModel);
        }

        public ActionResult Delete(int id)
        {
            DataContext db = new DataContext();
            var res = db.inviteUsers.Where(item => item.InviteId == id).ToList();
            if (res.Count() != 0)
            {
                db.inviteUsers.Remove(res.First());
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult CreateUpdate(InviteListViewModel viewModel)
        {
            DataContext db = new DataContext();
            if(viewModel.instance.InviteUserName == null)
            {
                TempData["error"] = "User Name is Empty";
            }
            else if(viewModel.instance.InviteUserName != null)
            {
                ///already invited h ya nahi///
                var data = db.inviteUsers.Where(item => item.InviteUserName == viewModel.instance.InviteUserName && item.ChannelId == Convert.ToInt32(Session["channelid"])).ToList();
                ////exist karta h database m ya nahi////
                var res = db.users.Where(item => item.Username == viewModel.instance.InviteUserName).ToList();
                if(viewModel.instance.InviteUserName == Convert.ToString(Session["name"]))
                {
                    TempData["error"] = "you cannot invite himself";
                }
                else if (res.Count() != 0 && data.Count() == 0)
                {
                    viewModel.instance.ChannelId = Convert.ToInt32(Session["channelid"]);
                    db.inviteUsers.Add(viewModel.instance);
                    db.SaveChanges();
                    TempData["error"] = "Added Successfully";
                }
                else if(res.Count() == 0)
                {
                    TempData["error"] = "not have an account";
                }
                
            }
            return RedirectToAction("Index");
        }
    }
}