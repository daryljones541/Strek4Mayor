using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Strek4Mayor.Models
{
    public class volunteer
    {
        public int id { get; set; }
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name  is required")]
        public string first_name { get; set; }
        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last name Name  is required")]
        public string last_name { get; set; }
        [Display(Name = "Adress #1")]
        [Required(ErrorMessage = "Adress  is required")]
        public string adress_1 { get; set; }
        [Display(Name = "Adress #2")]
        public string adress_2 { get; set; }
        [Required(ErrorMessage = "City  is required")]
        public string city { get; set; }
        [Required(ErrorMessage = "State  is required")]
        public string state { get; set; }
        [Required(ErrorMessage = "Phone number is required")]
        [DataType(DataType.PhoneNumber)]
        [Phone]
        [Display(Name = "Phone Number")]
        public string phone { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [Display(Name = "Email")]
        public string email { get; set; }
        [Display(Name = "Event Volunteer")]
        public bool vol1 { get; set; }
        [Display(Name = "Commity Advertisment")]
        public bool vol2 { get; set; }
        [Display(Name = "Event Promter")]
        public bool vol3 { get; set; }
    }
}