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
        [Required]
        public string Body { get; set; }
        [Required]
        public string Title { get; set; }
        public string Answer { get; set; }
        public bool MessageStatus { get; set; }
        public string Member { get; set; }
    }
}