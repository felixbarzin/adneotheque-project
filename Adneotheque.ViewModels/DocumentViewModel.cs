using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adneotheque.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace Adneotheque.ViewModels
{
    public class DocumentViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public DocumentCategories DocumentCategories { get; set; }

        [Required]
        public string DocumentId { get; set; }

        [Required]
        public Boolean Available { get; set; }
    }
}
