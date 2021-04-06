using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MessangingApp1.Models;
using MessangingApp1.ViewModels;

namespace MessangingApp1.Controllers
{
    public class PostController : Controller
    {
        // GET: Post
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Post newPost)
        {
            if (newPost.Title == null || newPost.Content == null)
            {
                ViewBag.ErrorMessage = "Empty Title or Content....";
                return View();
            }
            DataContext db = new DataContext();
            newPost.UserName = Convert.ToString(Session["name"]);
            newPost.ChannelId = int.Parse(Request.Url.Segments.Last());
            newPost.CreatedAt = DateTime.Now;
            db.posts.Add(newPost);
            db.SaveChanges();
            ViewBag.ErrorMessage = "Post Created Successfully....";
            return View();
        }


        public ActionResult ViewPosts(int? channelId)
        {
            Session["channelId"] = int.Parse(Request.Url.Segments.Last());
            DataContext db = new DataContext();
            SearchViewModel reference = new SearchViewModel();
            reference.posts = (List<Post>)db.posts.Where(item => item.ChannelId == int.Parse(Request.Url.Segments.Last())).ToList();
            return View(reference);
        }

        [HttpPost]
        public ActionResult ViewPosts(SearchViewModel model)
        {
            DataContext db = new DataContext();
            SearchViewModel reference = new SearchViewModel();
            reference.SearchQuery = model.SearchQuery;

            if (String.IsNullOrEmpty(model.SearchQuery))
            {
                reference.posts = (List<Post>)db.posts.Where(item => item.ChannelId == Convert.ToInt32(Session["channelId"])).ToList();
            }
            else
            {
                reference.posts = (List<Post>)db.posts.Where(item => item.ChannelId == Convert.ToInt32(Session["channelId"]) && item.Content.Contains(model.SearchQuery)).ToList();
            }
            return View(reference);
        }


        public ActionResult CreateMessage(int id)
        {
            DataContext db = new DataContext();

            Post post = db.posts.Where(item => item.PostId == id).First();

            Post p = new Post();
            p.PostId = id;
            p.Title = post.Title;
            p.Content = post.Content;
            p.Replies = db.replies.Where(item => item.PostId == id).ToList();
            p.UserName = Convert.ToString(Session["name"]);
            p.ChannelId = post.ChannelId;
            p.CreatedAt = post.CreatedAt;
            return View(p);
        }

        [HttpPost]
        public ActionResult CreateMessage(Post pr)
        {
            DataContext db = new DataContext();

            if (pr.enterReply.ReplyContent == null)
            {
                TempData["emptymessage"] = "Empty Message.....";
                return RedirectToAction("CreateMessage", int.Parse(Request.Url.Segments.Last()));
            }
            else
            {
                pr.enterReply.PostId = int.Parse(Request.Url.Segments.Last());
                pr.enterReply.userId = Convert.ToInt32(Session["userid"]);
                pr.enterReply.userName = Convert.ToString(Session["name"]);
                pr.enterReply.DateCreated = DateTime.Now;
                db.replies.Add(pr.enterReply);
                db.SaveChanges();
            }
            return RedirectToAction("CreateMessage", int.Parse(Request.Url.Segments.Last()));
        }
    }
}