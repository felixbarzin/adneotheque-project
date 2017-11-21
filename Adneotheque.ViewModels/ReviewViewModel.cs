using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adneotheque.ViewModels
{
    public class ReviewViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Your review")]
        [DataType(DataType.MultilineText)]
        [Required]
        public string Body { get; set; }

        [Display(Name = "Like")]
        [Required]
        public Boolean Rating { get; set; }

        [Display(Name = "Your name")]
        [Required]
        public string ReviewerName { get; set; }

        public int DocumentId { get; set; }
    }
}
