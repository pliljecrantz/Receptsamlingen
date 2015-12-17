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
	        model = GetModel(model);
            return View(model);
        }

	    public ActionResult DoSearch(SearchModel model)
	    {
		    var category = model.SelectedCategory != null ? int.Parse(model.SelectedCategory) : 0;
			var dishType = model.SelectedDishType != null ? int.Parse(model.SelectedDishType) : 0;
			var specials = Helper.GetSelectedSpecials(model.PostedSpecials);
			model.SearchResult = RecipeRepository.Instance.Search(model.Query.HtmlEncode(), category, dishType, specials);
		    model.SearchPerformed = true;
		    model = GetModel(model);
		    return View("Index", model);
	    }

	    private SearchModel GetModel(SearchModel model)
	    {
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
		    model.SelectedSpecials = Helper.GetSelectedSpecials(model.PostedSpecials);
		    return model;
	    }
    }
}