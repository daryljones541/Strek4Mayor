using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Strek4Mayor.Models;
using Microsoft.AspNet.Identity;
using System.Web.Security;

namespace Strek4Mayor.Controllers
{
    public class QandAController : Controller
    {
        private Strek4MayorContext db = new Strek4MayorContext();

        // GET: /QandA/
        public ActionResult Index()
        {
            return View(db.QandAs.ToList());
        }

        // GET: /QandA/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QandA qanda = db.QandAs.Find(id);
            if (qanda == null)
            {
                return HttpNotFound();
            }
            return View(qanda);
        }

        // GET: /QandA/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /QandA/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="QandAId,Body,Title,MessageStatus,Member,Date")] QandA qanda)
        {
            if (ModelState.IsValid)
            {
                db.QandAs.Add(qanda);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(qanda);
        }

        // GET: /QandA/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QandA qanda = db.QandAs.Find(id);
            if (qanda == null)
            {
                return HttpNotFound();
            }
            return View(qanda);
        }

        // POST: /QandA/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="QandAId,Body,Title,MessageStatus,Member,Date,Answer")] QandA qanda)
        {
            if (ModelState.IsValid)
            {
                var edit = db.QandAs.First(q => q.QandAId == qanda.QandAId);
                edit.QandAId = qanda.QandAId;
                edit.Title = qanda.Title;
                edit.Member = qanda.Member;
                edit.Date = DateTime.Now;
                edit.Body = qanda.Body;
                edit.Answer = qanda.Answer;
                edit.MessageStatus = qanda.MessageStatus;
               // db.Entry(qanda).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(qanda);
        }

        // GET: /QandA/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QandA qanda = db.QandAs.Find(id);
            if (qanda == null)
            {
                return HttpNotFound();
            }
            return View(qanda);
        }

        // POST: /QandA/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            QandA qanda = db.QandAs.Find(id);
            db.QandAs.Remove(qanda);
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
