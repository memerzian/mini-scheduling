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
            return View();
        }

        public ActionResult PartAllocationStatus()
        {
            return View();
        }

        public ActionResult BOMViewer()
        {
            return View();
        }

        public ActionResult SpendForecast()
        {
            return View();
        }
    }
}