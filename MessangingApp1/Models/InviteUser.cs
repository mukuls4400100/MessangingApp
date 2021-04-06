using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MessangingApp1.Models
{
    public class InviteUser
    {
        [Key, Column(Order = 1)]
        public int InviteId { get; set; }
        [Required]
        [MinLength(3)]
        public string InviteUserName { get; set; }
        public int ChannelId { get; set; }

        [NotMapped]
        public IEnumerable<Tag> tags { get; set; }
    }
}