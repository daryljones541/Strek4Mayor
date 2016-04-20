using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Strek4Mayor.Models
{
    public class DonationVM
    {
        [Required(ErrorMessage = "Please select or enter the amount you want to donate.")]
        [RegularExpression(@"^\d+\.?\d{0,2}$", ErrorMessage = "Amount must be a number and can't have more than 2 decimal places")]
        public string Amount { get; set; }
        [Required, StringLength(200)]
        public string Employer { get; set; }
        [Required, StringLength(200)]
        public string Occupation { get; set; }
        [Display(Name = "Retired or Unemployed")]
        public bool NoEmployment { get; set; }
    }
}