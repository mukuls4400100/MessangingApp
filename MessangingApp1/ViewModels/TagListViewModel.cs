using MessangingApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessangingApp1.ViewModels
{
    public class TagListViewModel
    {
        public TagListViewModel()
        {
            DataContext db = new DataContext();

            this.EditableItem = new Tag();
            this.TagItems = db.tags.ToList();
        }
            public List<Tag> TagItems { get; set; }

            public Tag EditableItem { get; set; }
    }
    
}