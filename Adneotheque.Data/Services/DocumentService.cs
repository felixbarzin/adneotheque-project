using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adneotheque.Data.Repositories;

namespace Adneotheque.Data.Services
{
    public class DocumentService
    {
         public DocumentRepository DocumentRepository;
        
        public DocumentService()
        {
            DocumentRepository = new DocumentRepository(new AdneothequeDbContext());
        }

    }
}
