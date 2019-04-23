using System.Collections.Generic;
using System.Web.Mvc;
using Receptsamlingen.Repository;

namespace Receptsamlingen.Mvc.Models
{
    public class RecipeModel
    {
		// Properties for viewing a chosen recipe
        public Recipe Recipe { get; set; }
		public string Category { get; set; }
		public string DishType { get; set; }
		public int AvarageRating { get; set; }
		public string Specials { get; set; }
		public int UserRating { get; set; }
        //public byte[] ImageData { get; set; } // TODO: In progress for adding images to recipe

        // Properties for adding dropdownlists and checkboxes in the view
        public IList<SelectListItem> AllCategories { get; set; }
		public IList<SelectListItem> AllDishTypes { get; set; }
		public IList<Special> AllSpecials { get; set; }
		public IList<SelectListItem> Portions { get; set; }

		// Properties for when adding or editing a recipe
		public string SelectedCategory { get; set; }
		public string SelectedDishType { get; set; }
		public string SelectedPortions { get; set; }
		public IList<Special> SelectedSpecials { get; set; }
		public PostedSpecials PostedSpecials { get; set; } 
        //public byte[] SelectedImageData { get; set; } // TODO: In progress for adding images to recipe

        public bool RecipeSaved { get; set; }
    }
}