using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Strek4Mayor.Models
{
    public class QandA
    {
        public int QandAId { get; set; }
        public DateTime Date { get; set; }
        public string Body { get; set; }
        public string Title { get; set; }
        public string Answer { get; set; }
        public bool MessageStatus { get; set; }
        public string Member { get; set; }
    }
}