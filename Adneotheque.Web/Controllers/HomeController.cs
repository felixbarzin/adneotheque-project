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
        //private DocumentService documentService;

        //public HomeController()
        //{
        //    documentService = new DocumentService();
        //}

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