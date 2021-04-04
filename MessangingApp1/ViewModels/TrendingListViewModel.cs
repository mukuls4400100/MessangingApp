using MessangingApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessangingApp1.ViewModels
{
    public class TrendingListViewModel
    {
        public IEnumerable<Trending> trendingRegions { get; set; }

        public IEnumerable<Trending> trendingUser { get; set; }

        public IEnumerable<Trending> trendingTags { get; set; }
        public IEnumerable<Trending> trendingChannels { get; set; }
    }
}