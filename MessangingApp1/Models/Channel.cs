using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MessangingApp1.Models
{
    public class Channel
    {

        [Key,Column(Order = 1)]
        public int ChannelId { get; set; }
        public string ChannelName { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
    }
}