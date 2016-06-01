using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Strek4Mayor.Models
{
    public class EventVM
    {
        public int ID { get; set; }
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime Date { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public string Title { get; set; }
        public List<SelectListItem> Time { get; set; }
        [Required]
        public int TimeSelection { get; set; }
    }
}