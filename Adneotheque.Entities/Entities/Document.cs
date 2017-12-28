using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adneotheque.Entities.Enums;

namespace Adneotheque.Entities.Entities
{
    public class Document
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DocumentCategories DocumentCategories { get; set; }
        public string DocumentIdentifier { get; set; }
        public Boolean Available { get; set; }
        public DateTime DayAdded { get; set; }
        public DateTime? DayBorrowed { get; set; }
        public int? BorrowedCounter { get; set; }
        public  DocumentLangage DocumentLangage { get; set; }
        //Pages
        //Summary

        //Navigation properties
        public virtual ICollection<Author> Authors { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
