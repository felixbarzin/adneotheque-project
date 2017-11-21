using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adneotheque.Data.Repositories;

namespace Adneotheque.Data.Services
{
    public class ReviewService
    {
        public ReviewRepository ReviewRepository;

        public ReviewService()
        {
            ReviewRepository = new ReviewRepository(new AdneothequeDbContext());
        }
    }
}
