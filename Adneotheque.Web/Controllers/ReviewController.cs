using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Adneotheque.Data.Services;
using Adneotheque.ViewModels;

namespace adneotheque_solution.Controllers
{
    public class ReviewController : Controller
    {
        private readonly ReviewService _reviewService;
        private readonly DocumentService _documentService;

        public ReviewController()
        {
            _reviewService = new ReviewService();
            _documentService = new DocumentService();
        }
        // GET: Review
        // This action give a page that show all the reviews for a document
        public async Task<ActionResult> Index(int id)
        {
            DocumentViewModel model = await _documentService.DocumentRepository.GetByIdAsync(id);

            return View(model);
        }

        // GET: Review/Create
        [HttpGet]
        public ActionResult Create(int id)
        {
            ReviewViewModel model = new ReviewViewModel();
            model.Rating = true;
            model.DocumentId = id;
            return View(model);
        }

        // POST: Review/Create
        [HttpPost]
        public async Task<ActionResult> Create(ReviewViewModel model)
        {
            try
            {
                await _reviewService.ReviewRepository.InsertAsync(model);

                return RedirectToAction("Index", new {id = model.DocumentId});
            }
            catch
            {
                return View();
            }
        }

        // GET: Review/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Review/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Review/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Review/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
