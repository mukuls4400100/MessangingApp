using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Web;

namespace MessangingApp1.Models
{
    public partial class DataContext : DbContext
    {
        public DataContext()
            : base()
        {
        }

        public DbSet<User> users { get; set; }
        public DbSet<Tag> tags { get; set; }
        public DbSet<InviteUser> inviteUsers { get; set; }
        public DbSet<Channel> channels { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=LAPTOP-9GTIN7FV\SQLEXPRESS;Database=MessangingAppDatabase;Trusted_Connection=True;");
        }
    }
}
