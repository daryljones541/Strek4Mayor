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
    public class NewsArticlesController : Controller
    {
        private Strek4MayorContext db = new Strek4MayorContext();

        // GET: NewsArticles
        public ActionResult Index()
        {
            List<NewsArticle> articles = db.NewsArticles.OrderByDescending(i => i.PublishDate).ToList();
            return View(articles);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult EditIndex()
        {
            List<NewsArticle> articles = db.NewsArticles.OrderByDescending(i => i.PublishDate).ToList();
            return View(articles);
        }

        public ActionResult AjaxIndex()
        {
            List<NewsArticle> articles = db.NewsArticles.OrderByDescending(i => i.PublishDate).ToList();
            return PartialView(articles);
        }

        // GET: NewsArticles/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsArticle newsArticle = db.NewsArticles.Find(id);
            if (newsArticle == null)
            {
                return HttpNotFound();
            }
            return View(newsArticle);
        }

        // GET: NewsArticles/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: NewsArticles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NewsArticleID,Title,PublishDate,Url")] NewsArticle newsArticle)
        {
            if (ModelState.IsValid)
            {
                db.NewsArticles.Add(newsArticle);
                db.SaveChanges();
                return RedirectToAction("Index", "Admin");
            }

            return View(newsArticle);
        }

        // GET: NewsArticles/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsArticle newsArticle = db.NewsArticles.Find(id);
            if (newsArticle == null)
            {
                return HttpNotFound();
            }
            return View(newsArticle);
        }

        // POST: NewsArticles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NewsArticleID,Title,PublishDate,Url,Hidden")] NewsArticle newsArticle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(newsArticle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("EditIndex");
            }
            return View(newsArticle);
        }

        // GET: NewsArticles/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsArticle newsArticle = db.NewsArticles.Find(id);
            if (newsArticle == null)
            {
                return HttpNotFound();
            }
            return View(newsArticle);
        }

        // POST: NewsArticles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NewsArticle newsArticle = db.NewsArticles.Find(id);
            db.NewsArticles.Remove(newsArticle);
            db.SaveChanges();
            return RedirectToAction("AdminList");
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
