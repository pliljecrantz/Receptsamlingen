using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Receptsamlingen.Mvc.Classes;
using Receptsamlingen.Mvc.Models;
using Receptsamlingen.Repository;

namespace Receptsamlingen.Mvc.Controllers
{
    public class SearchController : Controller
    {
		private RecipeRepository Repository { get; set; }

	    public SearchController()
	    {
			Repository = RecipeRepository.Instance;
	    }

        public ActionResult Index()
        {
			var model = new SearchModel();
			var allCategories = Repository.GetAllCategories();
			var allDishTypes = Repository.GetAllDishTypes();
			var allSpecials = Repository.GetAllSpecials();

			var categoryItems = new List<SelectListItem>
			{
				new SelectListItem {Text = "--- Välj ---", Value = "0"}
			};
			var dishTypeItems = new List<SelectListItem>
			{
				new SelectListItem {Text = "--- Välj ---", Value = "0"}
			};

			categoryItems.AddRange(allCategories.Select(category => new SelectListItem { Text = category.Name, Value = category.Id.ToString() }));
			dishTypeItems.AddRange(allDishTypes.Select(dishType => new SelectListItem { Text = dishType.Name, Value = dishType.Id.ToString() }));

			model.CategoryList = categoryItems;
			model.DishTypeList = dishTypeItems;
			model.SpecialList = allSpecials;
            return View(model);
        }

	    [HttpPost]
	    public ActionResult DoSearch(SearchModel model)
	    {
			// TODO: fix the search logic
		    if (!string.IsNullOrEmpty(model.SelectedCategory) || !string.IsNullOrEmpty(model.SelectedDishType) || model.SelectedSpecials != null)
		    {
			    
		    }
			model.SearchResult = RecipeRepository.Instance.Search(model.Query.HtmlEncode());
		    model.SearchPerformed = true;
		    return View("Index", model);
	    }
    }
}