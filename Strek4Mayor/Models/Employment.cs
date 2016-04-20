using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Strek4Mayor.Models
{
    public class Employment
    {
        [Display(Name="PayPal Custom #")]
        public int EmploymentID { get; set; }
        public bool Unemployed { get; set; }
        public string Employer { get; set; }
        public string Occupation { get; set; }
    }
}