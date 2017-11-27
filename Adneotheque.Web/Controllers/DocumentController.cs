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

        public async Task<ActionResult> DisplayAll(string searchTerm, string buttonCategory, DocumentViewModel model, int page = 1)
        {
            Session["SearchTerm"] = "";
            Session["SelectedCategory"] = "";
        
            var documents = await _documentService.DocumentRepository.GetAllWithSearchTermAndPageAsync(searchTerm, page);

            if (Request.IsAjaxRequest())
            {
            //    //@ViewBag.SearchTerm = searchTerm;
            //    //@ViewBag.Category = buttonName;

            //    //Session["SearchTerm"] = searchTerm;
            //    //Session["Category"] = buttonCategory;

                var filteredList =
                    await _documentService.DocumentRepository.GetDocumentsFiltered(
                        Session["SelectedCategory"].ToString(), 
                        Session["SearchTerm"].ToString(), 
                        "Rating");

                return PartialView("_Documents", filteredList.ToPagedList(page, 8));
            }

            return View(documents.ToPagedList(page, 8));
        }

        //TEST
        public async Task<ActionResult> DisplayWithFilters(DocumentViewModel model, string searchTerm, string sort, string buttonFilter, int page = 1)
        {
            IEnumerable<DocumentViewModel> documents = null;

            if (String.IsNullOrEmpty(buttonFilter))
                buttonFilter = "";

            if (buttonFilter.Equals("Search By Category") && model.SelectedCategory != null)
                Session["SelectedCategory"] = model.SelectedCategory;

            //if (model.SelectedCategory == null && searchTerm == null)
            //    Session["SelectedCategory"] = "";

            //searchTerm = String.IsNullOrEmpty(searchTerm) ? Session["SerchTerm"].ToString() : "";

            if (!String.IsNullOrEmpty(sort))
                if(!Session["SearchTerm"].Equals(null))
                    searchTerm = String.IsNullOrEmpty(searchTerm) ? Session["SearchTerm"].ToString() : "";

            //searchTerm = String.IsNullOrEmpty(searchTerm) ? Session["SerchTerm"] ?? "";



            documents = await _documentService.DocumentRepository.GetDocumentsFiltered(
                    Session["SelectedCategory"].ToString(),
                    searchTerm,
                    sort);

            if (buttonFilter.Equals("Search By Category"))
                return View("DisplayAll", documents.ToPagedList(page, 8));

            return PartialView("_Documents", documents.ToPagedList(page, 8));

        }

        //OK
        //public async Task<ActionResult> DisplayWithFilters(DocumentViewModel model, string searchTerm, string sort, string buttonFilter, int page = 1)
        //{
        //    IEnumerable<DocumentViewModel> documents = null;

        //    if (String.IsNullOrEmpty(buttonFilter))
        //        buttonFilter = "";


        //    var test = buttonFilter.Equals("Search By Category");

        //    if (buttonFilter.Equals("Search By Category") && model.SelectedCategory != null)
        //    {
        //        Session["SelectedCategory"] = model.SelectedCategory;
        //        documents = await _documentService.DocumentRepository.GetDocumentsByCategoryAsync(model.SelectedCategory, page);
        //        return View("DisplayAll", documents.ToPagedList(page, 8));

        //    }
        //    else if (buttonFilter.Equals("Search By Title"))
        //    {
        //        if (!String.IsNullOrEmpty(Session["SelectedCategory"].ToString()))
        //        {
        //            documents = await _documentService.DocumentRepository.GetDocumentsByCategoryAndSearchtermAsync(
        //                Session["SelectedCategory"].ToString(), searchTerm, page);
        //            if (Request.IsAjaxRequest())
        //                return PartialView("_Documents", documents.ToPagedList(page, 8));
        //        }
        //        else
        //        {
        //            documents = await _documentService.DocumentRepository.GetAllWithSearchTermAndPageAsync(searchTerm, page);


        //            if (Request.IsAjaxRequest())
        //                return PartialView("_Documents", documents.ToPagedList(page ,8));
        //        }
        //    }
        //    else
        //    {
        //        documents = await _documentService.DocumentRepository.GetDocumentsFiltered(
        //            Session["SelectedCategory"].ToString(),
        //            Session["SearchTerm"].ToString(),
        //            "Rating");

        //    }


        //    return PartialView("_Documents", documents.ToPagedList(page, 8));
















        //    //var test = searchTerm;
        //    //IPagedList<DocumentViewModel> documents = null;

        //    //if (model.SelectedCategory != null)
        //    //    Session["SelectedCategory"] = model.SelectedCategory;
        //    //else
        //    //    Session["SelectedCategory"] = "";

        //    //if(String.IsNullOrEmpty(searchTerm))
        //    //    if(model.SelectedCategory == null)
        //    //        if (!String.IsNullOrEmpty(Session["SearchTerm"].ToString()))
        //    //            searchTerm = Session["SearchTerm"].ToString();

        //    ////string searchTerm = Session["SearchTerm"].ToString();
        //    //string category = Session["SelectedCategory"].ToString();

        //    //if (String.IsNullOrEmpty(searchTerm) && !String.IsNullOrEmpty(category))
        //    //{
        //    //    documents = await _documentService.DocumentRepository.GetDocumentsByCategoryAsync(category, page);
        //    //    //ViewBag.Category = category;
        //    //    return View("DisplayAll", documents);
        //    //}

        //    //if (!String.IsNullOrEmpty(searchTerm) && String.IsNullOrEmpty(category))
        //    //{
        //    //    documents = await _documentService.DocumentRepository.GetAllWithSearchTermAndPageAsync(searchTerm, page);

        //    //    //ViewBag.SearchTerm = searchTerm;

        //    //    if (Request.IsAjaxRequest())
        //    //        return PartialView("_Documents", documents);


        //    //}

        //    //documents = await _documentService.DocumentRepository.GetDocumentsByCategoryAndSearchtermAsync(category, searchTerm, page);

        //    //if (String.IsNullOrEmpty(sort))
        //    //{
        //    //    sort = "Title";
        //    //}

        //    //if (!String.IsNullOrEmpty(sort))
        //    //{
        //    //    switch (sort)
        //    //    {
        //    //        case "Title":
        //    //            documents = documents.OrderBy(d => d.Title).ToPagedList(page, 8);
        //    //            break;
        //    //        case "Rating":
        //    //            documents = documents.OrderByDescending(d => d.Rating).ToPagedList(page, 8);
        //    //            break;
        //    //        default:
        //    //            documents = documents.OrderBy(d => d.Title).ToPagedList(page, 8);
        //    //            break;
        //    //    }
        //    //}

        //    //return View("DisplayAll", documents);

        //}


        //public async Task<ActionResult> DisplayByCategory(DocumentViewModel model, int page = 1)
        //{

        //    if (model.SelectedCategory == null)
        //    {
        //        return RedirectToAction("DisplayAll");
        //    }

        //    var documents = await _documentService.DocumentRepository.GetDocumentsByCategoryAsync(model.SelectedCategory, page);





        //    ViewBag.Category = model.SelectedCategory;

        //    return View("DisplayAll", documents);
        //}

        public async Task<ActionResult> OrderByRating(string searchTerm, string category, DocumentViewModel model, string sort, int page = 1)
        {

            //var documents = await _documentService.DocumentRepository.GetAllAsync();
            var documents = await _documentService.DocumentRepository.GetDocumentsByCategoryAndSearchtermAsync(category, searchTerm, page);

            if (String.IsNullOrEmpty(sort))
            {
                sort = "Title";
            }

            if (!String.IsNullOrEmpty(sort))
            {
                switch (sort)
                {
                    case "Title":
                        documents = documents.OrderBy(d => d.Title).ToPagedList(page, 8);
                        break;
                    case "Rating":
                        documents = documents.OrderByDescending(d => d.Rating).ToPagedList(page, 8);
                        break;
                    default:
                        documents = documents.OrderBy(d => d.Title).ToPagedList(page, 8);
                        break;
                }
            }
            



            //ViewBag.Category = model.SelectedCategory;

            return PartialView("_Documents", documents);
        }


        public async Task<ActionResult> Autocomplete(string term, DocumentViewModel documentViewModel, string category)
        {
          Session["SearchTerm"] = term;

            if (String.IsNullOrEmpty(category))
                category = Session["SelectedCategory"].ToString();

            if (category.Equals(""))
                category = null;

            System.Diagnostics.Debug.WriteLine(term);
            var test = Session["SearchTerm"];

            var model =  await _documentService.DocumentRepository.AutocompleteAsync(term, documentViewModel, category);

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
                    await _documentService.DocumentRepository.InsertAsync(documentViewModel);
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
            await _documentService.DocumentRepository.UpdateAsync(model);

            return RedirectToAction("DisplayAll");
        }
    }
}
