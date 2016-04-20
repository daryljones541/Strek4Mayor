using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Strek4Mayor.Models;

namespace Strek4Mayor.Controllers
{
    public class EmploymentsController : Controller
    {
        private Strek4MayorContext db = new Strek4MayorContext();

        // GET: Employments
        [Authorize(Roles = "Admin")]
        public ActionResult List()
        {
            return View(db.Employments.ToList());
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
