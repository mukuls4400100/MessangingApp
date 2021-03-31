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
        public ActionResult Index(int? channelId)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Post newPost)
        {
            DataContext db = new DataContext();
            newPost.UserName = Convert.ToString(Session["name"]);
            newPost.ChannelId = int.Parse(Request.Url.Segments.Last());
            newPost.CreatedAt = DateTime.Now;
            db.posts.Add(newPost);
            db.SaveChanges();
            return RedirectToAction("Index", newPost.ChannelId);
        }


        public ActionResult ViewPosts(int? channelId)
        {
            Session["channelId"] = int.Parse(Request.Url.Segments.Last());
            DataContext db = new DataContext();
            SearchViewModel reference = new SearchViewModel();
            reference.posts = (List<Post>)db.posts.Where(item => item.ChannelId == int.Parse(Request.Url.Segments.Last())).ToList();
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
            p.CreatedAt = DateTime.Now;
            return View(p);
        }

        [HttpPost]
        public ActionResult CreateMessage(Post pr)
        {
            DataContext db = new DataContext();

            PostReply newpost = new PostReply();
            newpost.PostId = int.Parse(Request.Url.Segments.Last());
            newpost.userId = Convert.ToInt32(Session["userid"]);
            newpost.userName = Convert.ToString(Session["name"]);
            newpost.ReplyContent = pr.Content;
            newpost.DateCreated = DateTime.Now;
            db.replies.Add(newpost);
            db.SaveChanges();
            return RedirectToAction("CreateMessage", int.Parse(Request.Url.Segments.Last()));
        }
        // public ActionResult SearchPosts(int id)
        // {
        // return RedirectToAction("Index", id);
        // }


        [HttpPost]
        public ActionResult SearchPosts(SearchViewModel model)
        {
            DataContext db = new DataContext();
            ViewBag.message = Session["channelId"];
            SearchViewModel reference = new SearchViewModel();
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
    }
}