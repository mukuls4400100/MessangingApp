using MessangingApp1.Models;
using MessangingApp1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MessangingApp1.Controllers
{
    public class TagController : Controller
    {
        // GET: Tag
        public ActionResult Index()
        {
            if (ModelState.IsValid)
            {
                DataContext db = new DataContext();
                List<Tag> res = db.tags.Where(item => item.ChannelId == Convert.ToInt32(Session["channelid"])).ToList();

                TagListViewModel viewModel = new TagListViewModel();
                viewModel.TagItems.Clear();
                viewModel.TagItems = res;
                return View(viewModel);
            }
            

            return View();
        }

        public ActionResult Edit(int id)
        {
            TagListViewModel viewModel = new TagListViewModel();
            viewModel.EditableItem = viewModel.TagItems.FirstOrDefault(x => x.TagId == id);
            return View(viewModel);
        }

        public ActionResult Delete(int id)
        {
            DataContext db = new DataContext();
            var res = db.tags.Where(item => item.TagId == id).ToList();
            if (res.Count() != 0)
            {
                db.tags.Remove(res.First());
                db.SaveChanges();
            }


            return RedirectToAction("Index");

        }

        [HttpPost]
        public ActionResult CreateUpdate(TagListViewModel viewModel)
        {
            DataContext db = new DataContext();

            viewModel.EditableItem.ChannelId = Convert.ToInt32(Session["channelid"]);
            db.tags.Add(viewModel.EditableItem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}