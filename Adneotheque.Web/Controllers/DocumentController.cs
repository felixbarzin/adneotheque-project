using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Adneotheque.Data.Services;
using System.Threading.Tasks;
using Adneotheque.ViewModels;
using PagedList;

namespace adneotheque_solution.Controllers
{
    public class DocumentController : Controller
    {
        private readonly DocumentService _documentService;
        
        public DocumentController()
        {
            _documentService = new DocumentService();
        }

        public async Task<ActionResult> DisplayAll(string searchTerm, int page = 1)
        {
            var documents = await _documentService.DocumentRepository.GetAllAsync();
            var documents2 = await _documentService.DocumentRepository.GetAllWithSearchTermAndPageAsync(searchTerm, page);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_Document", documents2);
            }

            return View(documents2);
        }

        public async Task<ActionResult> Autocomplete(string term)
        {
            var model =  await _documentService.DocumentRepository.AutocompleteAsync(term);

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        //When the user use the search input for a document to bring back, this action makes it more friendly for the user because it helps with autocompletion
        public async Task<ActionResult> AutocompleteDocumentId(string term)
        {
            var model = await _documentService.DocumentRepository.AutocompleteDocumentIdAsync(term);

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult DocumentReturn()
        {
            
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> DocumentReturn(DocumentViewModel documentViewModel)
        {
            documentViewModel.Available = true;

            await _documentService.DocumentRepository.Update(documentViewModel);

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> DocumentReturnSearch(string searchTerm = "")
        {
            if (searchTerm != "")
            {
                try
                {
                    DocumentViewModel documentViewModel = await _documentService.DocumentRepository.GetByDocumentIdAsync(searchTerm);
                    return PartialView("_DocumentReturn", documentViewModel);

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

            }

            return View();
        }

        // GET: Document/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Document/Create
        // Use Ajax for displaying a form allowing the user to add a document in DB
        [HttpGet]
        public ActionResult Create(String category = null)
        {
            if (Request.IsAjaxRequest() && category != null)
            {
                ViewBag.Category = category;

                return PartialView("_FormCreateDocument");
            }

            return View();
        }

        // POST: Document/Create
        [HttpPost]
        public async Task<ActionResult> Create(DocumentViewModel documentViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    documentViewModel.Available = true;
                    await _documentService.DocumentRepository.Insert(documentViewModel);
                    return RedirectToAction("DisplayAll");
                }

                return View(documentViewModel);
            }
            catch(Exception e)
            {
                //TODO : Implement error strategy
                return View();
            }
        }

        // GET: Document/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Document/Edit/5
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

        // GET: Document/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Document/Delete/5
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
