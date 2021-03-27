using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MessangingApp1.Models
{
    public class User
    {
        [Key, Column(Order = 1)]
        public int UserId { get; set; }

        public string Email { get; set; }

        public string Region { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public DateTime CreatedAt { get; set; }

        [NotMapped]
        public string ConfirmPassword
        {
            get; set;
        }
    }
}