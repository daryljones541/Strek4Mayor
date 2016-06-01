using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Strek4Mayor.Models;
using System.Web.UI.WebControls;

namespace Strek4Mayor.Controllers
{
    public class EventsController : Controller
    {
        private Strek4MayorContext db = new Strek4MayorContext();

        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            List<Event> events = db.Events.OrderBy(x => x.Date).ToList();
            return View(events);
        }

        // GET: Events
        public ActionResult List()
        {
            List<Event> events = db.Events.OrderBy(x => x.Date).ToList();
            return View(events);
        }

        public ActionResult AjaxList()
        {
            List<Event> events = db.Events.OrderBy(x => x.Date).ToList();
            return PartialView(events);
        }

        public JsonResult GetList()
        {
            var events=db.Events.OrderBy(x=>x.Date);
            return Json(events, JsonRequestBehavior.AllowGet);
        }

        // GET: Events/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // GET: Events/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            EventVM eventVM = new EventVM();
            PopulateTime(eventVM);
            eventVM.Date = DateTime.Now.AddDays(1);
            eventVM.TimeSelection = 30;
            return View(eventVM);
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "Date,Location,Title,TimeSelection")] EventVM eventVM)
        {
            if (ModelState.IsValid)
            {
                PopulateTime(eventVM);
                DateTime date = eventVM.Date;
                TimeSpan time = Convert.ToDateTime(eventVM.Time[eventVM.TimeSelection].Text).TimeOfDay;
                date = date.Add(time);
                Event addEvent=new Event
                { 
                    Date=date, 
                    Location=eventVM.Location,
                    Title=eventVM.Title
                };
                db.Events.Add(addEvent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else {
                PopulateTime(eventVM);
            }
            return View(eventVM);
        }

        private EventVM PopulateTime(EventVM eventVM)
        {
            eventVM.Time = new List<SelectListItem>();
            DateTime times = DateTime.ParseExact("00:00", "HH:mm", null);
            DateTime endTime = DateTime.ParseExact("23:30", "HH:mm", null);
            TimeSpan interval = new TimeSpan(0, 30, 0);
            int position = 0;
            while (times <= endTime)
            {
                eventVM.Time.Add(new SelectListItem() { Text = times.ToShortTimeString(), Value = position.ToString() });
                times = times.Add(interval);
                position++;
            }
            return eventVM;
        }

        // GET: Events/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            EventVM eventVM = new EventVM { ID=@event.EventID, Date = @event.Date, Location = @event.Location, Title = @event.Title };
            PopulateTime(eventVM);
            string currentTime = @event.Date.ToShortTimeString();
            eventVM.TimeSelection=0;
            while (currentTime != eventVM.Time[eventVM.TimeSelection].Text && eventVM.TimeSelection<48)
            {
                eventVM.TimeSelection++;
            }
            return View(eventVM);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Date,Location,Title,TimeSelection")] EventVM eventVM)
        {
            if (ModelState.IsValid)
            {
                PopulateTime(eventVM);
                DateTime date = eventVM.Date;
                TimeSpan time = Convert.ToDateTime(eventVM.Time[eventVM.TimeSelection].Text).TimeOfDay;
                date = date.Add(time);
                Event editEvent = db.Events.Find(eventVM.ID);
                editEvent.Date=date;
                editEvent.Location=eventVM.Location;
                editEvent.Title=eventVM.Title;
                db.Entry(editEvent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                PopulateTime(eventVM);
            }
            return View(eventVM);
        }

        // GET: Events/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Event @event = db.Events.Find(id);
            db.Events.Remove(@event);
            db.SaveChanges();
            return RedirectToAction("List");
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
