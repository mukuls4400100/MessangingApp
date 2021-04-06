using MessangingApp1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }
    }
}