using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adneotheque.ViewModels
{
    public class AuthorViewModel
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
}
