using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Strek4Mayor.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public bool Newsletter { get; set; }
    }
}