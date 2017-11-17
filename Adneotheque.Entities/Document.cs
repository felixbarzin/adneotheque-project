using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adneotheque.Entities
{
    public class Document
    {
        public int Id { get; set; }
        public string Title { get; set; }

        //Navigation properties
        public virtual ICollection<Author> Authors { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
