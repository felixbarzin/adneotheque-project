using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Adneotheque.Entities;
using Adneotheque.Entities.Entities;
using Adneotheque.ViewModels;
using AutoMapper.QueryableExtensions;
using PagedList;

namespace Adneotheque.Data.Repositories
{
    public interface IDocumentRepository<TDocument> : IRepository<DocumentViewModel>
    {
        TDocument GetByDocumentId(string documentId);
        Task<IEnumerable<object>> AutocompleteAsync(string term);
    }

    public class DocumentRepository : IDocumentRepository<DocumentViewModel>
    {
        private readonly AdneothequeDbContext _adneothequeDbContext;

        public DocumentRepository(AdneothequeDbContext dbContext)
        {
            _adneothequeDbContext = dbContext;
        }

        //public async Task SaveChangesAsync(CancellationToken cancellationToken)
        //{

        //}

        public async Task Insert(DocumentViewModel t)
        {
            var document = AutoMapper.Mapper.Map<DocumentViewModel, Document>(t);

            _adneothequeDbContext.Documents.Add(document);

            await _adneothequeDbContext.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public DocumentViewModel GetById(int id)
        {
            var documentt = new Document()
            { Id = 1, Title = "test" };
            //var document = machin get by id
            var document = AutoMapper.Mapper.Map<Document, DocumentViewModel>(documentt);
            return document;
        }

        public async Task<IEnumerable<DocumentViewModel>> GetAllAsync()
        {
            var documentsAsync = await _adneothequeDbContext
                .Documents
                .ProjectTo<DocumentViewModel>()
                .ToListAsync();

            //var documents = AutoMapper.Mapper.Map<IEnumerable<Document>, IEnumerable<DocumentViewModel>>(documentsAsync);

            return documentsAsync;

        }

        public async Task<IPagedList<DocumentViewModel>> GetAllWithSearchTermAndPageAsync(string searchTerm, int page)
        {
            var documents = await GetAllAsync();

            return documents
                .OrderBy(d => d.Title)
                .Where(d => searchTerm == null || d.Title.StartsWith(searchTerm))
                .Select(d => d)
                .ToPagedList(page, 5);
        }

        public async Task<IEnumerable<object>> AutocompleteAsync(string term)
        {
            var documents = await GetAllAsync();

            return documents
                .Where(d => d.Title.ToLower().StartsWith(term))
                .Take(4)
                .Select(d => new
                {
                    label = d.Title
                });
        }

        public DocumentViewModel GetByDocumentId(string documentId)
        {
            throw new NotImplementedException();
        }



        public void Update(DocumentViewModel t)
        {
            throw new NotImplementedException();
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
