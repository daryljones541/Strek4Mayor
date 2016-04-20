using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Strek4Mayor.Models
{
    public class Event
    {
        public int EventID { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}