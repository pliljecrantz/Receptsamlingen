using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Receptsamlingen.Mvc.Models;
using Receptsamlingen.Repository;

namespace Receptsamlingen.Mvc.Controllers
{
	public class RecipeController : Controller
	{
		private RecipeRepository Repository { get; set; }
		private RatingRepository RatingRepository { get; set; }

		public RecipeController()
		{
			Repository = RecipeRepository.Instance;
			RatingRepository = RatingRepository.Instance;
		}

		public ActionResult Add()
		{
			var model = GetModel();
			return View("Manage", model);
		}

		public ActionResult Id(int id)
		{
			var model = GetModel(id);
			return View("Detail", model);
		}

		// TODO: Add functionality for editing a recipe
		public ActionResult Edit(int id)
		{
			var model = GetModel(id);
			return View("Manage", model);
		}

		public ActionResult Save(RecipeModel model)
		{
			model.Recipe.Guid = Guid.NewGuid().ToString();
			var result = Repository.Save(model.Recipe);

			if (result)
			{
				return View("Done");
			}
			else
			{
				// TODO: Show error message instead
				return null;
			}
		}

		// TODO: Add functionality for voting for a recipe
		public ActionResult Vote(int rating)
		{
			// Send in rating from view
			// Save vote
			// Return result view (success or error)
			return View();
		}

		// TODO: Add functionality for jQuery confirm dialog when deleting a recipe
		// TODO: Add functionality for deleting a recipe
		public ActionResult Delete(int id)
		{
			return View();
		}

		private RecipeModel GetModel(int id = 0)
		{
			var model = new RecipeModel();
			var categoryItems = new List<SelectListItem>
				{
					new SelectListItem {Text = "--- Välj ---", Value = "0"}
				};
			var dishTypeItems = new List<SelectListItem>
				{
					new SelectListItem {Text = "--- Välj ---", Value = "0"}
				};
			var allCategories = Repository.GetAllCategories();
			categoryItems.AddRange(allCategories.Select(category => new SelectListItem { Text = category.Name, Value = category.Id.ToString() }));
			var allDishTypes = Repository.GetAllDishTypes();
			dishTypeItems.AddRange(allDishTypes.Select(category => new SelectListItem { Text = category.Name, Value = category.Id.ToString() }));
			var allSpecials = Repository.GetAllSpecials();
			var portionItems = new List<SelectListItem>
				{
					new SelectListItem { Text = "--- Välj ---", Value = "0" },
					new SelectListItem { Text = "1", Value = "1" },
					new SelectListItem { Text = "2", Value = "2" },
					new SelectListItem { Text = "3", Value = "3" },
					new SelectListItem { Text = "4", Value = "4" },
					new SelectListItem { Text = "5", Value = "5" },
					new SelectListItem { Text = "6", Value = "6" },
					new SelectListItem { Text = "7", Value = "7" },
					new SelectListItem { Text = "8", Value = "8" },
				};

			model.Recipe = new Recipe();
			model.CategoryList = categoryItems;
			model.DishTypeList = dishTypeItems;
			model.Portions = portionItems;
			model.SpecialList = allSpecials;

			if (id != 0)
			{
				var recipe = Repository.GetById(id);
				var category = Repository.GetCategoryById(recipe.CategoryId);
				var specials = Repository.GetSpecialsForRecipe(recipe.Guid);
				var specialsList = (from item in specials
									from special in allSpecials
									where item.SpecialId == special.Id
									select special).Aggregate(String.Empty, (current, special) => current + (special.Name + ", "));
				var dishType = string.Empty;
				var avgRating = RatingRepository.GetAvarage(recipe.Guid);
				if (recipe.DishTypeId.HasValue)
				{
					dishType = Repository.GetDishTypeById(recipe.DishTypeId.Value);
				}

				if (specialsList.EndsWith(", "))
				{
					specialsList = specialsList.TrimEnd(',', ' ');
				}

				model.Recipe = recipe;
				model.Category = category;
				model.DishType = dishType;
				model.AvarageRating = avgRating;
				model.Specials = specialsList;
			}
			return model;
		}
	}
}