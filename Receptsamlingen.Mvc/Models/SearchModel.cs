using System.Collections.Generic;
using System.Web.Mvc;
using Receptsamlingen.Repository;

namespace Receptsamlingen.Mvc.Models
{
	public class SearchModel
	{
		// Properties for search
		public string Query { get; set; }
		public Recipe Recipe { get; set; }
		public IList<Recipe> SearchResult { get; set; }
		public bool SearchPerformed { get; set; }

		// Properties for adding dropdownlists and checkboxes in the view
		public IList<SelectListItem> CategoryList { get; set; }
		public IList<SelectListItem> DishTypeList { get; set; }
		public IList<Special> SpecialList { get; set; }

		// Properties for when doing advanced search
		public string SelectedCategory { get; set; }
		public string SelectedDishType { get; set; }
		public IList<Special> SelectedSpecials { get; set; }
		public PostedSpecials PostedSpecials { get; set; }
	}
}