using MyChicken.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyChicken.Controllers
{
    public class LogController : Controller
    {
        LogService ls = new LogService();
        // GET: Log
        public ActionResult Index()
        {
            var data = ls.GetLog(DateTime.Today);
            return View(data);
        }
    }
}