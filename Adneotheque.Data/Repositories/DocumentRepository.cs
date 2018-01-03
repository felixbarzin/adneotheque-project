using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Adneotheque.Entities;
using Adneotheque.Entities.Entities;
using Adneotheque.Entities.Enums;
using Adneotheque.ViewModels;
using AutoMapper.QueryableExtensions;
using PagedList;
using Adneotheque.Data.Services;

namespace Adneotheque.Data.Repositories
{
    public interface IDocumentRepository<TDocument> : IRepository<DocumentViewModel>
    {
        Task<TDocument> GetMostRecentAsync();
        Task<TDocument> GetLatestAddedAsync();
        Task<TDocument> GetMostReadAsync();
        Task<TDocument> GetByDocumentIdAsync(string documentId);

        Task<IEnumerable<object>> AutocompleteAsync(string term, string category);
        Task<IEnumerable<object>> AutocompleteDocumentIdAsync(string term);

        Task<IEnumerable<TDocument>> GetAllWithSearchTermAndPageAsync(string searchTerm);
        Task<IEnumerable<TDocument>> GetDocumentsFiltered(string category, string searchTerm,
            string filter, Boolean available);
        Task<IEnumerable<TDocument>> GetAllWithSearchTermAndPageAsync(string searchTerm, int page);
        Task<IEnumerable<TDocument>> GetDocumentsByCategoryAsync(string category, int page);
        Task<IEnumerable<TDocument>> GetDocumentsByCategoryAndSearchtermAsync(string category, string searchTerm, int page);
    }

    public class DocumentRepository : IDocumentRepository<DocumentViewModel>
    {
        private readonly AdneothequeDbContext _adneothequeDbContext;
        private readonly DbSet<Document> _documentContext;
        private readonly AuthorService _authorService;

        public DocumentRepository(AdneothequeDbContext dbContext)
        {
            _adneothequeDbContext = dbContext;
            _documentContext = dbContext.Documents;
            _authorService = new AuthorService();

        }

        public async Task<DocumentViewModel> GetMostRecentAsync()
        {
            var documentsAsync = await _adneothequeDbContext
                .Documents
                .ProjectTo<DocumentViewModel>()
                .ToListAsync();

            var model = documentsAsync
                .OrderByDescending(d => d.DayAdded)
                .FirstOrDefault();

            return model;
        }

        public async Task<DocumentViewModel> GetLatestAddedAsync()
        {
            var documentsAsync = await _adneothequeDbContext
                .Documents
                .ProjectTo<DocumentViewModel>()
                .ToListAsync();

            var model = documentsAsync
                .Where(d => !d.Available)
                .OrderByDescending(d => d.DayBorrowed)
                .FirstOrDefault();

            return model;
        }

        public async Task<DocumentViewModel> GetMostReadAsync()
        {
            var documentsAsync = await _adneothequeDbContext
                .Documents
                .ProjectTo<DocumentViewModel>()
                .ToListAsync();

            var model = documentsAsync
                .OrderByDescending(d => d.BorrowedCounter)
                .FirstOrDefault();

            return model;
        }

        public async Task InsertAsync(DocumentViewModel t)
        {
            var document = AutoMapper.Mapper.Map<DocumentViewModel, Document>(t);

            document.Authors = new List<Author>();

            var listAuthors = _authorService.AuthorRepository.GetByIdWithoutMappingList(t, _adneothequeDbContext);

            foreach(var item in listAuthors)
            {
                document.Authors.Add(item);
            }

            _adneothequeDbContext.Documents.Add(document);

            await _adneothequeDbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            try
            {
                var document = _adneothequeDbContext.Documents.Find(id);

                _adneothequeDbContext.Documents.Remove(document);

                await _adneothequeDbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public DocumentViewModel GetById(int id)
        {
            Document document = _documentContext.Find(id);

            var model = AutoMapper.Mapper.Map<Document, DocumentViewModel>(document);

            return model;
        }

        public async Task<DocumentViewModel> GetByIdAsync(int id)
        {
            Document document = await _documentContext.FindAsync(id);
            var model = AutoMapper.Mapper.Map<Document, DocumentViewModel>(document);
            return model;
        }

        public async Task<IEnumerable<DocumentViewModel>> GetAllAsync()
        {
            var documentsAsync = await _adneothequeDbContext
                .Documents
                .ProjectTo<DocumentViewModel>()
                .ToListAsync();

            return documentsAsync;

        }

        public async Task<IEnumerable<DocumentViewModel>> GetAllWithSearchTermAndPageAsync(string searchTerm, int page)
        {
            var documents = await GetAllAsync();

            return documents
                .OrderBy(d => d.Title)
                .Where(d => searchTerm == null || d.Title.ToLower().Contains(searchTerm.ToLower()))
                .Select(d => d)
                .ToList();
        }

        public async Task<IEnumerable<DocumentViewModel>> GetDocumentsFiltered(string category, string searchTerm,
            string filter, Boolean available)
        {
            var documents = await GetAllAsync();

            IEnumerable<DocumentViewModel> filteredList = new List<DocumentViewModel>();


            if (!String.IsNullOrEmpty(category))
            {
                documents = documents
                    .Where(d => d.DocumentCategories ==
                                (DocumentCategories) System.Enum.Parse(typeof(DocumentCategories), category))
                    .Select(d => d)
                    .ToList();
            }


            if (!String.IsNullOrEmpty(searchTerm))
            {
                documents = documents
                    .Where(d => d.Title.ToLower().Contains(searchTerm.ToLower()))
                    .Select(d => d)
                    .ToList();
            }

            if (!String.IsNullOrEmpty(filter))
            {
                switch (filter)
                {
                    case "Rating":
                        documents = documents
                            .OrderByDescending(d => d.Rating)
                            .ToList();
                        break;
                    default:
                        documents = documents.OrderBy(d => d.Title);
                        break;
                }
            }

            if (available == true)
            {
                documents = documents
                    .Where(d => d.Available == true)
                    .Select(d => d)
                    .ToList();
            }

            return documents;

        }

        public async Task<IEnumerable<DocumentViewModel>> GetAllWithSearchTermAndPageAsync(string searchTerm)
        {
            var documents = await GetAllAsync();

            return documents
                .OrderBy(d => d.Title)
                .Where(d => searchTerm == null || d.Title.ToLower().Contains(searchTerm.ToLower()))
                .Select(d => d)
                .ToList();
        }

        public async Task<IEnumerable<DocumentViewModel>> GetDocumentsByCategoryAsync(string category, int page)
        {
            var documents = await GetAllAsync();

            if (String.IsNullOrEmpty(category))
            {
                return documents;
            }

            return documents.OrderBy(d => d.Title)
                .Where(d => d.DocumentCategories == (DocumentCategories)System.Enum.Parse(typeof(DocumentCategories),category))
                .Select(d => d)
                .ToList();
        }

        public async Task<IEnumerable<DocumentViewModel>> GetDocumentsByCategoryAndSearchtermAsync(string category, string searchTerm, int page)
        {
            var documents = await GetAllAsync();

            return documents
                .Where(d => d.DocumentCategories ==
                            (DocumentCategories) System.Enum.Parse(typeof(DocumentCategories), category) &&
                            d.Title.ToLower().Contains(searchTerm))
                .Select(d => d)
                .ToList();
        }

        public async Task<IEnumerable<object>> AutocompleteAsync(string term, /*DocumentViewModel model,*/ string category)
        {
            var documents = await GetAllAsync();

            if (category != null)
            {
                return documents
                    .Where(d => d.Title.ToLower().Contains(term) 
                    && d.DocumentCategories == (DocumentCategories)System.Enum.Parse(typeof(DocumentCategories), category))
                    .Take(4)
                    .Select(d => new
                    {
                        label = d.Title
                    });
            }
            
            return documents
                .Where(d => d.Title.ToLower().Contains(term))
                .Take(4)
                .Select(d => new
                {
                    label = d.Title
                });
        }

        public async Task<IEnumerable<object>> AutocompleteDocumentIdAsync(string term)
        {
            var documents = await GetAllAsync();

            return documents
                .Where(d => d.DocumentIdentifier.StartsWith(term) && d.Available == false)
                .Take(5)
                .Select(d => new
                {
                    label = d.DocumentIdentifier
                });
        }

        public async Task<DocumentViewModel> GetByDocumentIdAsync(string documentId)
        {

            return await _adneothequeDbContext
                .Documents
                .ProjectTo<DocumentViewModel>()
                .FirstAsync(d => d.DocumentIdentifier == documentId);
        }

        //That function cannot update a document entirely, it only makes it available
        //TODO : improve that function or rename it
        public async Task UpdateAsync (DocumentViewModel t)
        {
            var document = _documentContext
                .First(d => d.DocumentIdentifier == t.DocumentIdentifier);
            document.Available = true;
                //AutoMapper.Mapper.Map(t, document);

            try
            {
                _adneothequeDbContext.Entry(document).State = System.Data.Entity.EntityState.Modified;

                await _adneothequeDbContext.SaveChangesAsync();
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            
        }

        public async Task UpdateGrabAsync(DocumentViewModel t)
        {
            var document = _documentContext
                .First(d => d.DocumentIdentifier == t.DocumentIdentifier);
            document.Available = false;

            try
            {
                _adneothequeDbContext.Entry(document).State = System.Data.Entity.EntityState.Modified;

                await _adneothequeDbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _adneothequeDbContext.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }
    }
}
