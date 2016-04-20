using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Strek4Mayor.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        [Display(Name="Can we contact you by e-mail?")]
        public bool Contact { get; set; }
    }
}