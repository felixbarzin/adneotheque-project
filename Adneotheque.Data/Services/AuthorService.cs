using Adneotheque.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adneotheque.Data.Services
{
    public class AuthorService
    {
        public AuthorRepository AuthorRepository;

        public AuthorService()
        {
            AuthorRepository = new AuthorRepository(new AdneothequeDbContext());
        }
    }
}
