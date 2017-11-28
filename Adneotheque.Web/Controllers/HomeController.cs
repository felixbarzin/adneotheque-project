using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Protocols;
using Adneotheque.Data.Services;

namespace adneotheque_solution.Controllers
{
    public class HomeController : Controller
    {

        [OutputCache(CacheProfile="Long", VaryByHeader = "X-Requested-With")]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}