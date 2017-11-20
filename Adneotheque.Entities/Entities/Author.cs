using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adneotheque.Entities.Entities
{
    public class Author
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        //Navigation properties
        public virtual ICollection<Document> Documents { get; set; }
    }
}
