using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Strek4Mayor.Models
{
    public class Donation
    {
        public int DonationID { get; set; }
        public string Date { get; set; }
        public string Amount { get; set; }
        [Display(Name="Transaction Fee")]
        public string TransactionFee { get; set; }
        public Donor Donor { get; set; }
    }
}