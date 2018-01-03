using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Protocols;
using Adneotheque.Data.Services;
using System.Threading.Tasks;
using Adneotheque.ViewModels;

namespace adneotheque_solution.Controllers
{
    public class HomeController : Controller
    {
        private readonly DocumentService _documentService;

        public HomeController()
        {
            _documentService = new DocumentService();
        }

        [OutputCache(CacheProfile = "Long", VaryByHeader = "X-Requested-With")]
        public ActionResult Index()
        {

            return View();
        }

        #region Fun facts
        public async Task<ActionResult> MostRecent()
        {
            DocumentViewModel model = await _documentService.DocumentRepository.GetMostRecentAsync();

            return PartialView("_Details", model);
        }

        public async Task<ActionResult> LatestBorrowed()
        {
            DocumentViewModel model = await _documentService.DocumentRepository.GetLatestAddedAsync();

            return PartialView("_Details", model);
        }

        public async Task<ActionResult> MostRead()
        {
            DocumentViewModel model = await _documentService.DocumentRepository.GetMostReadAsync();

            return PartialView("_Details", model);
        }
        #endregion

        //Test authorization and roles
        [Authorize(Users = "ADN-BE-019P\\felix")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}