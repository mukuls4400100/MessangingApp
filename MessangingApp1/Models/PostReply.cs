using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MessangingApp1.Models
{
    public class PostReply
    {
        [Key]
        public int Id { get; set; }
        public int userId { get; set; }
        public string userName { get; set; }
        public DateTime DateCreated { get; set; }
        public string ReplyContent { get; set; }
        public int PostId { get; set; }
    }
}