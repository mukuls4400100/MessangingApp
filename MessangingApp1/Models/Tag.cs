using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MessangingApp1.Models
{
    public class Tag
    {
        [Key, Column(Order = 1)]
        public int TagId { get; set; }

        [Required]
        [MinLength(3)]
        public string TagName { get; set; }

        public int ChannelId { get; set; }

    }
}