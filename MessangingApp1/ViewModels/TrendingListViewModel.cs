using MessangingApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessangingApp1.ViewModels
{
    public class TrendingListViewModel
    {
        public List<User> trendingRegions { get; set; }

        public List<User> trendingUser { get; set; }

        public List<User> trendingTags { get; set; }
        public List<User> trendingChannels { get; set; }
    }
}