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
            viewModel.InviteUsers.Clear();
            viewModel.InviteUsers = res;

            return View(viewModel);
        }

       /* public ActionResult Edit(int id)
        {
            InviteListViewModel viewModel = new InviteListViewModel();
            viewModel.instance = viewModel.InviteUsers.FirstOrDefault(x => x.InviteId == id);
            return View(viewModel);
        }*/

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
            var data = db.inviteUsers.Where(item => item.user == viewModel.instance.user && item.ChannelId == Convert.ToInt32(Session["channelid"])).ToList();

            List<User> res = db.users.Where(item => item.Username == viewModel.instance.user.Username).ToList();
            if (res.Count() != 0 && data.Count() == 0)
            {

                viewModel.instance.ChannelId = Convert.ToInt32(Session["channelid"]);
                db.inviteUsers.Add(viewModel.instance);
                db.SaveChanges();

            }

            return RedirectToAction("Index");
        }
    }
}