using MessangingApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessangingApp1.ViewModels
{
    public class SearchViewModel
    {
        public IEnumerable<Post> posts { get; set; }

        public string SearchQuery { get; set; }

        public bool EmptySearchResult { get; set; }
    }
}