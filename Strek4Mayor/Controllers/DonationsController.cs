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

namespace Strek4Mayor.Controllers
{
    public class DonationsController : Controller
    {
        private Strek4MayorContext db = new Strek4MayorContext();

        // GET: Donations
        public ActionResult Index()
        {
            return View(db.Donations.ToList());
        }

        // GET: Donations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donation donation = db.Donations.Find(id);
            if (donation == null)
            {
                return HttpNotFound();
            }
            return View(donation);
        }

        // GET: Donations/Create
        public ActionResult Make()
        {
            return View();
        }

        // POST: Donations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public void Make([Bind(Include = "Amount,Employer,Occupation,NoEmployment")] DonationVM donationVM)
        {
            Response.Clear();

            StringBuilder sb = new StringBuilder();
            sb.Append("<html>");
            sb.AppendFormat(@"<body onload='document.forms[""form""].submit()'>");
            sb.AppendFormat("<form name='form' action='{0}' method='post'>", "https://www.sandbox.paypal.com/cgi-bin/webscr");
            sb.AppendFormat("<input type='hidden' name='cmd' value='{0}'>", "_s-xclick");
            switch (donationVM.Amount)
            {
                case 5:
                    sb.AppendFormat("<input type='hidden' name='hosted_button_id' value='{0}'>", "JYH9SAUTZPGFW");
                    break;
                case 10:
                    sb.AppendFormat("<input type='hidden' name='hosted_button_id' value='{0}'>", "36EGVJY9Q75W4");
                    break;
                case 20:
                    sb.AppendFormat("<input type='hidden' name='hosted_button_id' value='{0}'>", "BGZKDHEJAS4UW");
                    break;
                case 50:
                    sb.AppendFormat("<input type='hidden' name='hosted_button_id' value='{0}'>", "5QMULVXLPYE2U");
                    break;
                case 100:
                    sb.AppendFormat("<input type='hidden' name='hosted_button_id' value='{0}'>", "B5PYHRYAAD2GA");
                    break;
                default:
                    sb.AppendFormat("<input type='hidden' name='hosted_button_id' value='{0}'>", "6ZA9ACTF6BHWU");
                    break;
            }
            sb.Append("</form>");
            sb.Append("</body>");
            sb.Append("</html>");

            Response.Write(sb.ToString());

            Response.End();
            /*
            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["os0"] = donationVM.Occupation;
                data["os1"] = donationVM.Employer;
                data["amount"] = donationVM.Amount.ToString();
                data["return"] = Url.Action("Complete", "Donations");
                data["notify_url"] = Url.Action("IPN", "Donations");
                data["cancel_return"] = Url.Action("Cancel", "Donations");
                data["currency_code"] = Url.Action("USD");
                data["cmd"] = "_donations";
                data["hosted_button_id"] = "3PT462UMQ3HA2";
                var response = wb.UploadValues("https://www.sandbox.paypal.com/cgi-bin/webscr", "POST", data);
            }
             * */
        }

        // GET: Donations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donation donation = db.Donations.Find(id);
            if (donation == null)
            {
                return HttpNotFound();
            }
            return View(donation);
        }

        // POST: Donations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DonationID,Amount")] Donation donation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(donation);
        }

        // GET: Donations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donation donation = db.Donations.Find(id);
            if (donation == null)
            {
                return HttpNotFound();
            }
            return View(donation);
        }

        // POST: Donations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Donation donation = db.Donations.Find(id);
            db.Donations.Remove(donation);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Complete()
        {
            /*
            Donation donation = new Donation();
            donation.FirstName = Request["first_name"];
            donation.LastName = Request["last_name"];
            donation.Amount = Request["mc_gross"];
            donation.Address = Request["address_street"];
            donation.City = Request["address_city"];
            donation.ZipCode = Request["address_zip"];
            donation.Email = Request["payer_email"];
             * */
            string data = new System.IO.StreamReader(Request.InputStream).ReadToEnd();
            ViewBag.data = data;
            return View();
        }

        public ActionResult Cancel()
        {
            return View();
        }

        public void IPN()
        {
            var formVals = new Dictionary<string, string>();

            formVals.Add("cmd", "_notify-validate");

            // if you want to use the PayPal sandbox change this from false to true
            string response = GetPayPalResponse(formVals, true);

            if (response == "VERIFIED")
            {
                Donation donation = new Donation();
                donation.FirstName = Request["first_name"];
                donation.LastName = Request["last_name"];
                donation.Amount = Request["mc_gross"];
                donation.Address = Request["address_street"];
                donation.City = Request["address_city"];
                donation.ZipCode = Request["address_zip"];
                donation.Email = Request["payer_email"];
                db.Donations.Add(donation);
                db.SaveChanges();
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
