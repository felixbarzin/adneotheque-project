using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adneotheque.Entities.Enums;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

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
        public string DocumentIdentifier { get; set; }

        [Required]
        public Boolean Available { get; set; }

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

        public string SelectedCategory { get; set; }

        public virtual ICollection<ReviewViewModel> Reviews { get; set; }

        public List<SelectListItem> GetDocumentCategoriesList()
        {
            List<SelectListItem> documentCategoriesList = new List<SelectListItem>();
            foreach (string d in Enum.GetNames(typeof(DocumentCategories)))
            {
                documentCategoriesList.Add(new SelectListItem(){ Text = d, Value = d});
            }

            return documentCategoriesList;
        }

        ///// <summary>
        ///// Calcule le rating d'un document : pourcentage de like
        ///// </summary>
        //public decimal DocumentRating()
        //{
        //    decimal countLikes = 0;
        //    decimal rating = 0;

        //    foreach (var review in Reviews)
        //    {
        //        if (review.Rating == true) countLikes++;
        //    }

        //    if (countLikes != 0) rating = ((countLikes / Reviews.Count) * 100);

        //    return rating;
        //}
    }
}
