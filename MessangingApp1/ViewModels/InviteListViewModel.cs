using MessangingApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessangingApp1.ViewModels
{
    public class InviteListViewModel
    {
        public InviteListViewModel()
        {
            DataContext db = new DataContext();

            this.instance = new InviteUser();
            this.InviteUsers = db.inviteUsers.ToList();
        }
        public List<InviteUser> InviteUsers { get; set; }

        public InviteUser instance { get; set; }
    }
}