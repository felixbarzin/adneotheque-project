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
        private readonly AuthorService _authorService;
        const int pageSize = 8;

        public DocumentController()
        {
            _documentService = new DocumentService();
            _authorService = new AuthorService();
        }

        public async Task<ActionResult> DisplayAll(string searchTerm, int page = 1)
        {
            DocumentsWithFilters model = new DocumentsWithFilters
            {
                DocumentViewModelPagedList =
                    (await _documentService.DocumentRepository.GetAllWithSearchTermAndPageAsync(searchTerm, page))
                    .ToPagedList(page, pageSize)
            };

            if (Request.IsAjaxRequest())
            {
                return PartialView("_Documents", model);
            }

            return View(model);
        }

        public async Task<ActionResult> DisplayWithFilters(DocumentsWithFilters model, int page = 1)
        {
            if (Request.IsAjaxRequest())
            {
                model.DocumentViewModelPagedList =
                    (await _documentService.DocumentRepository.GetDocumentsFiltered(
                    model.SelectedCategory,
                    model.SearchTerm,
                    model.Sort,
                    model.Available))
                    .ToPagedList(page, pageSize);

                return PartialView("_Documents", model);
            }

            model.DocumentViewModelPagedList =
                (await _documentService.DocumentRepository.GetDocumentsByCategoryAsync(model.SelectedCategory, page))
                .ToPagedList(page, pageSize);

            return View("DisplayAll", model);
        }

        public async Task<ActionResult> Autocomplete(string term, /*DocumentViewModel documentViewModel,*/ string category)
        {
            Session["SearchTerm"] = term;

            //if (String.IsNullOrEmpty(category))
            //    category = Session["SelectedCategory"].ToString();

            //if (category.Equals(""))
            //    category = null;

            System.Diagnostics.Debug.WriteLine(term);

            var model = await _documentService.DocumentRepository.AutocompleteAsync(term, category);

            if (!model.Any())
            {
                var notFound = string.Format("Term \"{0}\" cannot be found", term);

                List<object> list = new List<object>
                {
                    notFound
                };

                return Json(list, JsonRequestBehavior.AllowGet);
            }

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

            await _documentService.DocumentRepository.UpdateAsync(documentViewModel);

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
            var model = _documentService.DocumentRepository.GetById(id);

            return View(model);
        }

        // GET: Document/Create
        // Use Ajax for displaying a form allowing the user to add a document in DB
        [HttpGet]
        public async Task<ActionResult> Create(String category = null)
        {
            if (Request.IsAjaxRequest() && category != null)
            {
                ViewBag.Category = category;

                DocumentViewModel model = new DocumentViewModel();
                model.AuthorsSelectListItem = await _authorService.AuthorRepository.GetAuthorSelectListItemAsync();

                return PartialView("_FormCreateDocument", model);
            }

            return View();
        }

        // POST: Document/Create
        [HttpPost]
        public async Task<ActionResult> Create(DocumentViewModel documentViewModel, int[] authorsIdAddedLi)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    documentViewModel.Authors = new List<AuthorViewModel>();

                    foreach(var item in authorsIdAddedLi)
                    {
                        var author = _authorService.AuthorRepository.GetById(item);
                        documentViewModel.Authors.Add(author);
                    }

                    documentViewModel.Available = true;
                    documentViewModel.DayAdded = DateTime.Now;
                    await _documentService.DocumentRepository.InsertAsync(documentViewModel);
                    return RedirectToAction("DisplayAll");
                }

                return View(documentViewModel);
            }
            catch (Exception e)
            {
                //TODO : Implement error strategy
                return View();
            }
        }

        // GET: Document/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            DocumentViewModel model = _documentService.DocumentRepository.GetById(id);

            return View(model);
        }

        // POST: Document/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(DocumentViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _documentService.DocumentRepository.UpdateAsync(model);
                }

                return RedirectToAction("DisplayAll");
            }
            catch
            {
                return View();
            }
        }

        // GET: Document/Delete/5
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            DocumentViewModel model = _documentService.DocumentRepository.GetById(id.Value);

            return View(model);
        }

        // POST: Document/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _documentService.DocumentRepository.Delete(id);

                return RedirectToAction("DisplayAll");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public async Task<ActionResult> Grab(int id)
        {
            var model = await _documentService.DocumentRepository.GetByIdAsync(id);

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> GrabPost(int id)
        {
            DocumentViewModel model = await _documentService.DocumentRepository.GetByIdAsync(id);
            model.Available = false;
            model.BorrowedCounter++;
            await _documentService.DocumentRepository.UpdateAsync(model);

            return RedirectToAction("DisplayAll");
        }
    }
}
