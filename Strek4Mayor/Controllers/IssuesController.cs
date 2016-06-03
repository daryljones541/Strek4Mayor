using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Strek4Mayor.Controllers
{
    public class IssuesController : Controller
    {
        // GET: Issues
        [OutputCache(Duration = 300, VaryByParam = "none")]
        public ActionResult Index()
        {
            return View();
        }
        [OutputCache(Duration = 300, VaryByParam = "none")]
        public ActionResult AjaxIndex()
        {
            return PartialView();
        }
    }
}