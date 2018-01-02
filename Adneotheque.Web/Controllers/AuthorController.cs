using Adneotheque.Data.Services;
using Adneotheque.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace adneotheque_solution.Controllers
{
    public class AuthorController : Controller
    {
        private readonly AuthorService _authorService;

        public AuthorController()
        {
            _authorService = new AuthorService();
        }
        // GET: Author
        public ActionResult Index()
        {
            return View();
        }

        // GET: Author/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Author/Create
        public ActionResult Create()
        {

            return PartialView("_CreateAuthor");
        }

        // POST: Author/Create
        [HttpPost]
        public async Task<ActionResult> Create(AuthorViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _authorService.AuthorRepository.InsertAsync(model);
                }

                return RedirectToAction("Create", "Document");
            }
            catch
            {
                return View();
            }
        }

        // GET: Author/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Author/Edit/5
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

        // GET: Author/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Author/Delete/5
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
