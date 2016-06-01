using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Strek4Mayor.Models;
using System.Text;
using System.IO;
using System.Collections.Specialized;
using System.Web.Security;

namespace Strek4Mayor.Controllers
{
    public class DonationsController : Controller
    {
        private Strek4MayorContext db = new Strek4MayorContext();

        // GET: Donations
        [Authorize(Roles = "Admin")]
        public ActionResult RedactedList()
        {
            List<Donation> donations = db.Donations.Include(d => d.Donor.Employment).ToList();
            ViewBag.Number = donations.Count();
            var gross = donations.Sum(d => Convert.ToDecimal(d.Amount));
            var fee = donations.Sum(d => Convert.ToDecimal(d.TransactionFee));
            ViewBag.Gross = gross;
            ViewBag.Fee = fee;
            ViewBag.Net = gross - fee;
            return View(donations);
        }

        // GET: Donations
        [Authorize(Roles = "Admin")]
        [RequireHttps]
        public ActionResult FullList()
        {
            List<Donation> donations = db.Donations.Include(d => d.Donor.Employment).ToList();
            ViewBag.Number = donations.Count();
            var gross = donations.Sum(d => Convert.ToDecimal(d.Amount));
            var fee = donations.Sum(d => Convert.ToDecimal(d.TransactionFee));
            ViewBag.Gross = gross;
            ViewBag.Fee = fee;
            ViewBag.Net = gross - fee;
            return View(donations);
        }

        public ActionResult AjaxIndex()
        {
            return PartialView();
        }

        // GET: Donations/Create
        public ActionResult Index()
        {
            return View();
        }

        // POST: Donations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "Amount,Employer,Occupation,NoEmployment")] DonationVM donationVM)
        {
            if (string.IsNullOrEmpty(donationVM.Amount)) return View(donationVM);
            if (donationVM.NoEmployment==true)
            {
                donationVM.Occupation = "";
                donationVM.Employer = "";
            }
            else if (!ModelState.IsValid) return View();          
            Employment employment=new Employment { Employer=donationVM.Employer,
                Occupation=donationVM.Occupation, Unemployed=donationVM.NoEmployment };
            db.Employments.Add(employment);
            db.SaveChanges();
            ViewBag.Amount = donationVM.Amount.ToString();
            ViewBag.EmploymentID = employment.EmploymentID.ToString();
            return View("Continue");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AjaxIndex([Bind(Include = "Amount,Employer,Occupation,NoEmployment")] DonationVM donationVM)
        {
            if (string.IsNullOrEmpty(donationVM.Amount)) return View(donationVM);
            if (donationVM.NoEmployment == true)
            {
                donationVM.Occupation = "";
                donationVM.Employer = "";
            }
            else if (!ModelState.IsValid) return View();
            Employment employment = new Employment
            {
                Employer = donationVM.Employer,
                Occupation = donationVM.Occupation,
                Unemployed = donationVM.NoEmployment
            };
            db.Employments.Add(employment);
            db.SaveChanges();
            ViewBag.Amount = donationVM.Amount.ToString();
            ViewBag.EmploymentID = employment.EmploymentID.ToString();
            return View("Continue");
        }

        public ActionResult Complete()
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                string authToken = "idnKptTL9518CH6VW-ux0enRTDBGXHWAU0I87iebhVQ0p6NGqO2D0BQStLG";
                string txToken = Request.QueryString["tx"];
                string query = "cmd=_notify-synch&tx=" + txToken + "&at=" + authToken;

                //Post back to either sandbox or live
                string strSandbox = "https://www.sandbox.paypal.com/cgi-bin/webscr";
                string strLive = "https://www.paypal.com/cgi-bin/webscr";
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(strSandbox);

                //Set values for the request back
                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded";
                req.ContentLength = query.Length;

                //Send the request to PayPal and get the response
                StreamWriter streamOut = new StreamWriter(req.GetRequestStream(), System.Text.Encoding.ASCII);
                streamOut.Write(query);
                streamOut.Close();
                StreamReader streamIn = new StreamReader(req.GetResponse().GetResponseStream());
                string strResponse = streamIn.ReadToEnd();
                streamIn.Close();

                Dictionary<string, string> results = new Dictionary<string, string>();
                if (strResponse != "")
                {
                    StringReader reader = new StringReader(strResponse);
                    string line = reader.ReadLine();

                    if (line == "SUCCESS")
                    {

                        while ((line = reader.ReadLine()) != null)
                        {
                            results.Add(line.Split('=')[0], line.Split('=')[1]);
                        }
                        int employmentID;
                        if (int.TryParse(results["custom"].Trim(), out employmentID))
                        {
                            Employment employment=db.Employments.Find(employmentID);
                            if (employment != null)
                            {
                                Donor donor = new Donor
                                {
                                    Employment = employment,
                                    FirstName = HttpUtility.UrlDecode(results["first_name"]),
                                    LastName = HttpUtility.UrlDecode(results["last_name"]),
                                    Address = HttpUtility.UrlDecode(results["address_state"]),
                                    City = HttpUtility.UrlDecode(results["address_city"]),
                                    State = HttpUtility.UrlDecode(results["address_state"]),
                                    ZipCode = HttpUtility.UrlDecode(results["address_zip"]),
                                    Email = HttpUtility.UrlDecode(results["payer_email"]),
                                };
                                db.Donors.Add(donor);
                                db.SaveChanges();
                                Donation donation = new Donation
                                {
                                    Date = HttpUtility.UrlDecode(results["payment_date"]),
                                    Amount = HttpUtility.UrlDecode(results["mc_gross"]),
                                    TransactionFee = HttpUtility.UrlDecode(results["mc_fee"]),
                                    Donor=donor
                                };
                                db.Donations.Add(donation);
                                db.SaveChanges();
                                ViewBag.Amount = donation.Amount;
                            }                           
                        }
                        /*
                        string payPalResponse = "";
                        foreach (KeyValuePair<string, string> result in results)
                        {
                            payPalResponse += "<li>" + result.Key + " = " + result.Value + "</li>";
                        }
                        ViewBag.data = payPalResponse;
                         */
                    }
                    else if (line == "FAIL")
                    {
                        ViewBag.data = "PayPal returned FAIL.";
                    }
                }
                else
                {
                    //unknown error
                    ViewBag.data="Unknown error";
                }            
            }
            catch (Exception e)
            {
                ViewBag.data = "Error Message: " + e.Message;
            }

            return View();
        }

        public ActionResult Cancel()
        {
            return View();
        }
        [RequireHttps]
        [HttpPost]
        public void IPN()
        {
            var formVals = new Dictionary<string, string>();

            formVals.Add("cmd", "_notify-validate");

            // if you want to use the PayPal sandbox change this from false to true
            string response = GetPayPalResponse(formVals, true);

            if (response == "VERIFIED")
            {
                int custom = Convert.ToInt32(Request["custom"]);
                var employment=db.Employments.Find(custom);
                if (employment != null)
                {
                    var donor = db.Donors.SingleOrDefault(d => d.Employment == employment);
                    if (donor == null)
                    {
                        Donor newDonor = new Donor
                                    {
                                        Employment = employment,
                                        FirstName = Request["first_name"],
                                        LastName = Request["last_name"],
                                        Address = Request["address_street"],
                                        City = Request["address_city"],
                                        State = Request["address_state"],
                                        ZipCode = Request["address_zip"],
                                        Email = Request["payer_email"],
                                    };
                        db.Donors.Add(newDonor);
                        db.SaveChanges();
                        Donation donation = new Donation
                        {
                            Date = Request["payment_date"],
                            Amount = Request["mc_gross"],
                            TransactionFee = Request["mc_fee"],
                            Donor = donor
                        };
                        db.Donations.Add(donation);
                        db.SaveChanges();
                    }
                }
            }
        }

        string GetPayPalResponse(Dictionary<string, string> formVals, bool useSandbox)
        {

            string paypalUrl = useSandbox ? "https://www.sandbox.paypal.com/cgi-bin/webscr"
                : "https://www.paypal.com/cgi-bin/webscr";


            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(paypalUrl);

            // Set values for the request back
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";

            byte[] param = Request.BinaryRead(Request.ContentLength);
            string strRequest = Encoding.ASCII.GetString(param);

            StringBuilder sb = new StringBuilder();
            sb.Append(strRequest);

            foreach (string key in formVals.Keys)
            {
                sb.AppendFormat("&{0}={1}", key, formVals[key]);
            }
            strRequest += sb.ToString();
            req.ContentLength = strRequest.Length;

            //for proxy
            //WebProxy proxy = new WebProxy(new Uri("http://urlort#");
            //req.Proxy = proxy;
            //Send the request to PayPal and get the response
            string response = "";
            using (StreamWriter streamOut = new StreamWriter(req.GetRequestStream(), System.Text.Encoding.ASCII))
            {
                streamOut.Write(strRequest);
                streamOut.Close();
                using (StreamReader streamIn = new StreamReader(req.GetResponse().GetResponseStream()))
                {
                    response = streamIn.ReadToEnd();
                }
            }

            return response;
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
