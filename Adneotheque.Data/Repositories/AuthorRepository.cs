using Adneotheque.Entities.Entities;
using Adneotheque.ViewModels;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Adneotheque.Data.Repositories
{
    public interface IAuthorRepository<TDocument> : IRepository<AuthorViewModel>
    {
        Task<List<SelectListItem>> GetAuthorSelectListItemAsync();
        List<Author> GetByIdWithoutMappingList(DocumentViewModel d, AdneothequeDbContext a);
    }

    public class AuthorRepository : IAuthorRepository<AuthorViewModel>
    {
        private readonly DbSet<Author> _authorContext;

        public AuthorRepository(AdneothequeDbContext context)
        {
            _authorContext = context.Authors;
        }

        public async Task<List<SelectListItem>> GetAuthorSelectListItemAsync()
        {
            var authorsAsync = await _authorContext
                .ProjectTo<AuthorViewModel>()
                .OrderBy(a => a.Lastname)
                .ToListAsync();

            List<SelectListItem> authorSelectListItem = new List<SelectListItem>();
            foreach (var a in authorsAsync)
            {
                authorSelectListItem.Add(new SelectListItem() { Text = a.Lastname + " " + a.Firstname, Value = a.Id.ToString() });
            }

            return authorSelectListItem;
        }

        public AuthorViewModel GetById(int id)
        {
            var author = _authorContext
                .Find(id);

            var model = AutoMapper.Mapper.Map<Author, AuthorViewModel>(author);

            return model;
        }

        public List<Author> GetByIdWithoutMappingList(DocumentViewModel d, AdneothequeDbContext _adneothequeDbContext)
        {
            List<Author> authorList = new List<Author>();

            foreach(var item in d.Authors)
            {
                var author = _adneothequeDbContext.Authors
                .Find(item.Id);
                
                authorList.Add(author);
            }

            return authorList;
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AuthorViewModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<AuthorViewModel> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task InsertAsync(AuthorViewModel t)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(AuthorViewModel t)
        {
            throw new NotImplementedException();
        }
    }
}
