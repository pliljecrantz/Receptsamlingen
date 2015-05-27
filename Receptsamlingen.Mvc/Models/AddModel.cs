using System.Collections.Generic;
using System.Web.Mvc;
using Receptsamlingen.Repository.Domain;

namespace Receptsamlingen.Mvc.Models
{
	public class AddModel
	{
		public Recipe NewRecipe { get; set; }
		public IList<Category> Categories { get; set; }
		public IList<DishType> DishTypes { get; set; }
		public List<SelectListItem> Portions { get; set; }
			   
		public string CategorySelection { get; set; }
		public string DishTypeSelection { get; set; }
		public string PortionSelection { get; set; }
	}
}