using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adneotheque.Entities.Enums;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel;

namespace Adneotheque.ViewModels
{
    public class DocumentViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [DisplayName("Type")]
        public DocumentCategories DocumentCategories { get; set; }

        [Required]
        [DisplayName("Document Identifier")]
        public string DocumentIdentifier { get; set; }

        [Required]
        public Boolean Available { get; set; }

        [Required]
        [DisplayName("Added on")]
        public DateTime DayAdded { get; set; }

        [Required]
        [DisplayName("Langage")]
        public DocumentLangage DocumentLangage { get; set; }

        [Range(1,1000)]
        public int Pages { get; set; }

        [MaxLength(500)]
        public string Summary { get; set; }

        [DisplayName("Previous borrowing")]
        public DateTime? DayBorrowed { get; set; }

        [DisplayName("Times borrowed")]
        public int? BorrowedCounter { get; set; }

        private int _rating;

        public int Rating
        {
            get
            {
                decimal countLikes = 0;
                decimal rating = 0;

                if (Reviews != null)
                {
                    foreach (var review in Reviews)
                    {
                        if (review.Rating == true) countLikes++;
                    }
                }

                if (countLikes != 0) rating = ((countLikes / Reviews.Count) * 100);

                _rating = Convert.ToInt32(rating);

                return this._rating;                 
            }
        }

        public List<SelectListItem> AuthorsSelectListItem { get; set; }

        private List<SelectListItem> _langages;
        public List<SelectListItem> Langages
        {
            get
            {
                _langages = GetLangageList();
                return _langages;
            }
        }

        public List<SelectListItem> GetDocumentCategoriesList()
        {
            List<SelectListItem> documentCategoriesList = new List<SelectListItem>();
            foreach (string d in Enum.GetNames(typeof(DocumentCategories)))
            {
                documentCategoriesList.Add(new SelectListItem() { Text = d, Value = d });
            }

            return documentCategoriesList;
        }

        public List<SelectListItem> GetLangageList()
        {
            List<SelectListItem> langageList = new List<SelectListItem>();
            foreach (string d in Enum.GetNames(typeof(DocumentLangage)))
            {
                langageList.Add(new SelectListItem() { Text = d, Value = d });
            }

            langageList.Add(new SelectListItem() { Text = "French", Value = "French", Selected = true });

            return langageList;
        }

        public string SelectedCategory { get; set; }

        public virtual ICollection<ReviewViewModel> Reviews { get; set; }
        [DisplayName("Author(s)")]
        public virtual ICollection<AuthorViewModel> Authors { get; set; }


    }
}
