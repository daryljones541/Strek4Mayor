using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Strek4Mayor.Models
{
    public class DonationVM
    {
        [Required(ErrorMessage = "Please select the amount you want to donate or choose other.")]
        public int Amount { get; set; }
        [Required, StringLength(200)]
        public string Employer { get; set; }
        [Required, StringLength(200)]
        public string Occupation { get; set; }
        [Display(Name = "Retired or Unemployed")]
        public bool NoEmployment { get; set; }
    }
}