using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
//channel form
namespace MessangingApp1.Models
{
    public class Channel
    {

        [Key, Column(Order = 1)]
        public int ChannelId { get; set; }

        [Required]
        [MinLength(3)]
        public string ChannelName { get; set; }
        [Required]
        [MinLength(3)]
        public string Description { get; set; }

        [Required]
        public int UserId { get; set; }

        public int CountPosts { get; set; }

        public DateTime CreatedAt { get; set; }

        [NotMapped]
        public IEnumerable<Tag> tags { get; set; }
    }
    
}