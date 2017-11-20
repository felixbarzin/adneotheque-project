﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
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
        Task<TDocument> GetByDocumentIdAsync(string documentId);
        Task<IEnumerable<object>> AutocompleteAsync(string term);
        Task<IEnumerable<object>> AutocompleteDocumentIdAsync(string term);
    }

    public class DocumentRepository : IDocumentRepository<DocumentViewModel>
    {
        private readonly AdneothequeDbContext _adneothequeDbContext;

        public DocumentRepository(AdneothequeDbContext dbContext)
        {
            _adneothequeDbContext = dbContext;
        }

        public async Task Insert(DocumentViewModel t)
        {
            var document = AutoMapper.Mapper.Map<DocumentViewModel, Document>(t);

            _adneothequeDbContext.Documents.Add(document);

            await _adneothequeDbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            try
            {
                var document = _adneothequeDbContext.Documents.Find(id);
                //_adneothequeDbContext.Entry(document).State = EntityState.Deleted;

                //_adneothequeDbContext.Documents.Load();
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
            Document document = _adneothequeDbContext.Documents.Find(id);

            var model = AutoMapper.Mapper.Map<Document, DocumentViewModel>(document);

            return model;
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

            //var test = documents
            //    .Where(d => d.DocumentId.StartsWith(term) && d.Available == false)
            //    .Take(5)
            //    .Select(d => new
            //    {
            //        label = d.Title
            //    });

            //return documents
            //    .Where(d => d.DocumentId.StartsWith(term) && d.Available == false)
            //    .Take(5)
            //    .Select(d => new
            //    {
            //        label = d.Title
            //    });
        }

        public async Task<DocumentViewModel> GetByDocumentIdAsync(string documentId)
        {
            //var test = _adneothequeDbContext.Documents.First(d => d.DocumentId == documentId);

            return await _adneothequeDbContext
                .Documents
                .ProjectTo<DocumentViewModel>()
                .FirstAsync(d => d.DocumentIdentifier == documentId);
        }

        public async Task Update (DocumentViewModel t)
        {
            var document = _adneothequeDbContext.Documents.First(d => d.DocumentIdentifier == t.DocumentIdentifier);

            AutoMapper.Mapper.Map(t, document);

            _adneothequeDbContext.Entry(document).State = System.Data.Entity.EntityState.Modified;

            await _adneothequeDbContext.SaveChangesAsync();
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
