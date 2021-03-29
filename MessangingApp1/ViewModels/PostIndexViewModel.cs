using MessangingApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessangingApp1.ViewModels
{
    public class PostIndexViewModel
    {
        public int PostId { get; set; }
        public string Title { get; set; }

        public string Content { get; set; }
        public int ChannelId { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedAt { get; set; }

        public IEnumerable<PostReply> Replies { get; set; }
    }
}