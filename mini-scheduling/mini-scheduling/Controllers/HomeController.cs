using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mini_scheduling.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult RunSummary()
        {
            return View();
        }

        public ActionResult ProgressView()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult PartAllocationStatus()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult BOMViewer()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}