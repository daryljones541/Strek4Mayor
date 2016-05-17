using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Strek4Mayor.Models
{
    public class NewsArticle
    {
        public int NewsArticleID { get; set; }
        public string Title { get; set; }
        public DateTime PublishDate { get; set; }
        public string Url { get; set; }
        //Need to figure out a good way to implement a thumbnail image
    }
}