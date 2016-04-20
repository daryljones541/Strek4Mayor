using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Strek4Mayor.Models;
using BotDetect.Web.Mvc;

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

        // GET: /QandA/
        public ActionResult Admin()
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
        [AllowAnonymous]
        [CaptchaValidation("CaptchaCode", "Create", "Incorrect CAPTCHA code!")]
        public ActionResult Create([Bind(Include="QandAId,Body,Title,Member")] QandA qanda)
        {
            if (ModelState.IsValid)
            {
                qanda.Date = DateTime.Now;
                qanda.MessageStatus = false;
                db.QandAs.Add(qanda);
                db.SaveChanges();
                MvcCaptcha.ResetCaptcha("Create");
                return RedirectToAction("Thanks");
            }
            else
            {
                MvcCaptcha.ResetCaptcha("Incorrect CAPTCHA code!");
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
        public ActionResult Edit([Bind(Include="QandAId,Body,Title,MessageStatus")] QandA qanda)
        {
            if (ModelState.IsValid)
            {
                db.Entry(qanda).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(qanda);
        }

        // GET: /QandA/Answer/5
        public ActionResult Answer(int? id)
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

        // POST: /QandA/Answer/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Answer([Bind(Include = "Member,Title,Body,Answer")] QandA qanda, int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (ModelState.IsValid)
            {
                var findQuestion = db.QandAs.Find(id);
                findQuestion.Answer = qanda.Answer;
                findQuestion.MessageStatus = true;
                db.Entry(findQuestion).State = EntityState.Modified;
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
