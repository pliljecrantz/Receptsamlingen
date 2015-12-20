using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Ninject;
using Receptsamlingen.Mvc.Classes;
using Receptsamlingen.Mvc.Models;
using Receptsamlingen.Repository.Interfaces;

namespace Receptsamlingen.Mvc.Controllers
{
	public class SearchController : Controller
	{
		[Inject]
		public IRecipeRepository RecipeRepository { get; set; }
		[Inject]
		public IHelper Helper { get; set; }

		public SearchController()
		{
			this.Inject();
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
			model.SearchResult = RecipeRepository.Search(model.Query.HtmlEncode(), category, dishType, specials);
			model.SearchPerformed = true;
			model = GetModel(model);
			return View("Index", model);
		}

		private SearchModel GetModel(SearchModel model)
		{
			var allCategories = RecipeRepository.GetAllCategories();
			var allDishTypes = RecipeRepository.GetAllDishTypes();
			var allSpecials = RecipeRepository.GetAllSpecials();

			var categoryItems = new List<SelectListItem>
			{
				new SelectListItem { Text = "--- Välj ---", Value = "0" }
			};
			var dishTypeItems = new List<SelectListItem>
			{
				new SelectListItem { Text = "--- Välj ---", Value = "0" }
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