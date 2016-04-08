using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Strek4Mayor.Models
{
    public class Donor : User
    {
        public Employment Employment { get; set; }
    }
}