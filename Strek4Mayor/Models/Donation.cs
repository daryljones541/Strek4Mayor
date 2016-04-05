﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Strek4Mayor.Models
{
    public class Donation
    {
        public int DonationID { get; set; }
        public string Amount { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public bool Employed { get; set; }
        public string Employer { get; set; }
        public string Occupation { get; set; }
    }
}