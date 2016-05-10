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
    public class volunteersController : Controller
    {
        private Strek4MayorContext db = new Strek4MayorContext();

        // GET: volunteers
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(db.volunteers.ToList());
        }

        public ActionResult AjaxCreate()
        {
            return PartialView();
        }

        // GET: volunteers/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            volunteer volunteer = db.volunteers.Find(id);
            if (volunteer == null)
            {
                return HttpNotFound();
            }
            return View(volunteer);
        }

        // GET: volunteers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: volunteers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,first_name,last_name,adress_1,adress_2,city,state,phone,email,vol1,vol2,vol3")] volunteer volunteer)
        {
            if (ModelState.IsValid)
            {
                if (volunteer.vol1 == false && volunteer.vol2 == false && volunteer.vol3 == false)
                {
                    ViewBag.errormessage = "please chose one of the volunteer opations";
                    return View(volunteer);
                  
                }
                else
                {
                    db.volunteers.Add(volunteer);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
 

            return View(volunteer);
        }

        // GET: volunteers/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            volunteer volunteer = db.volunteers.Find(id);
            if (volunteer == null)
            {
                return HttpNotFound();
            }
            return View(volunteer);
        }

        // POST: volunteers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,first_name,last_name,adress_1,adress_2,city,state,phone,email,vol1,vol2,vol3")] volunteer volunteer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(volunteer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(volunteer);
        }

        // GET: volunteers/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            volunteer volunteer = db.volunteers.Find(id);
            if (volunteer == null)
            {
                return HttpNotFound();
            }
            return View(volunteer);
        }

        // POST: volunteers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            volunteer volunteer = db.volunteers.Find(id);
            db.volunteers.Remove(volunteer);
            db.SaveChanges();
            return RedirectToAction("Index");
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
