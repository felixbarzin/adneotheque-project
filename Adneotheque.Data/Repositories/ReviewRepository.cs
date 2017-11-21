using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adneotheque.Entities.Entities;
using Adneotheque.ViewModels;

namespace Adneotheque.Data.Repositories
{
    public interface IReviewRepository<TReview> : IRepository<ReviewViewModel>
    {
    }

    public class ReviewRepository : IReviewRepository<ReviewViewModel>
    {
        private readonly AdneothequeDbContext _adneothequeDbContext;
        private readonly DbSet<Review> _reviewContext;

        public ReviewRepository(AdneothequeDbContext dbContext)
        {
            _adneothequeDbContext = dbContext;
            _reviewContext = dbContext.Reviews;
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ReviewViewModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public ReviewViewModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ReviewViewModel> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task InsertAsync(ReviewViewModel t)
        {
            try
            {
                var review = AutoMapper.Mapper.Map<ReviewViewModel, Review>(t);
                //review.DocumentId = 7;
                _reviewContext.Add(review);

                await _adneothequeDbContext.SaveChangesAsync();
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            
        }

        public Task UpdateAsync(ReviewViewModel t)
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
