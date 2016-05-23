using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Strek4Mayor.Models
{
    public class QandA
    {
        public int QandAId { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        [Display(Name = "Question")]
        public string Body { get; set; }
        [Display(Name = "Topic (optional)")]
        public string Title { get; set; }
        public string Answer { get; set; }
        [Display(Name = "Answered")]
        public bool MessageStatus { get; set; }
        [Display(Name = "Your name (optional)")]
        public string Member { get; set; }
    }
}