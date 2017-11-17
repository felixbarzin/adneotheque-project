using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adneotheque.Entities;
using Adneotheque.ViewModels;

namespace Adneotheque.Data.Repositories
{
    public interface IDocumentRepository<TDocument> : IRepository<DocumentViewModel>
    {
        TDocument GetByDocumentId(string documentId);
    }

    public class DocumentRepository : IDocumentRepository<DocumentViewModel>
    {
        private readonly AdneothequeDbContext _adneothequeDbContext;

        public DocumentRepository(AdneothequeDbContext dbContext)
        {
            _adneothequeDbContext = dbContext;
        }

        public void Add(DocumentViewModel t)
        {
            throw new NotImplementedException();
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
            var documentsAsync = await _adneothequeDbContext.Documents.ToListAsync();
            var documents = AutoMapper.Mapper.Map<IEnumerable<Document>, IEnumerable<DocumentViewModel>>(documentsAsync);

            return documents;

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
