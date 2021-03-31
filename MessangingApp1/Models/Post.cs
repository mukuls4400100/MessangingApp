using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MessangingApp1.Models
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }
        public string Title { get; set; }

        public string Content { get; set; }
        public int ChannelId { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedAt { get; set; }

        [NotMapped]
        public IEnumerable<PostReply> Replies { get; set; }
    }
}