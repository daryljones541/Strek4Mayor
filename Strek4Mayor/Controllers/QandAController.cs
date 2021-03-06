﻿using System;
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
        public ActionResult Search(string searchString)
        {
            var search = db.QandAs.Where(s => s.Body.Contains(searchString));
            //return View(db.QandAs.ToList());
            return View(search);
        }

        public ActionResult Thanks()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View(db.QandAs.ToList());
        }

        public ActionResult AjaxIndex()
        {
            return PartialView(db.QandAs.ToList());
        }

        // GET: /QandA/
        [Authorize(Roles = "Admin")]
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

        public static class CaptchaHelper
        {
            public static MvcCaptcha GetExampleCaptcha()
            {
                // create the control instance
                MvcCaptcha exampleCaptcha = new MvcCaptcha("Create");

                // set up client-side processing of the Captcha code input textbox
                exampleCaptcha.UserInputID = "Create";

                return exampleCaptcha;
            }
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
        [CaptchaValidation("CaptchaCode", "ExampleCaptcha", "Incorrect CAPTCHA code!")]
        public ActionResult Create([Bind(Include="QandAId,Body,Title,Member")] QandA qanda, bool captchavalid)
        {
            if (ModelState.IsValid)
            {
                qanda.Date = DateTime.Now;
                qanda.MessageStatus = false;
                MvcCaptcha.ResetCaptcha("ExampleCaptcha");
                db.QandAs.Add(qanda);
                db.SaveChanges();
                return RedirectToAction("Thanks");
            }
            else
            {
                MvcCaptcha.IsCaptchaSolved("Incorrect CAPTCHA code!");
                ViewBag.ErrMessage = "Error: captcha is not valid."; 
            }

            return View(qanda);
        }

        // GET: /QandA/Edit/5
        [Authorize(Roles = "Admin")]
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
        public ActionResult Edit([Bind(Include="QandAId,Member,Date,Body,Title,Answer,MessageStatus")] QandA qanda)
        {
            if (ModelState.IsValid)
            {
                db.Entry(qanda).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(qanda);
        }

        // GET: /QandA/Edit/5
        [Authorize(Roles = "Admin")]
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

        // POST: /QandA/Edit/5
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
                return RedirectToAction("Admin");
            }
            return View(qanda);
        }

        // GET: /QandA/Delete/5
        [Authorize(Roles = "Admin")]
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
            return RedirectToAction("Admin");
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
