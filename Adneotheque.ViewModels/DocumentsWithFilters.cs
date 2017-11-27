using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;

namespace Adneotheque.ViewModels
{
    public class DocumentsWithFilters
    {
        //public DocumentViewModel DocumentViewModel { get; set; }

        public string SelectedCategory { get; set; }
        public string SearchTerm { get; set; }
        public string Sort { get; set; }
        public Boolean Available { get; set; }
        public IPagedList<DocumentViewModel> DocumentViewModelPagedList { get; set; }
    }
}
