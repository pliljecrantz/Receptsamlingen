using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Receptsamlingen.Mvc.Classes;
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
			var model = Load();
			return View("Manage", model);
		}

		public ActionResult Id(int id)
		{
			var model = Load(id);
			return View("Detail", model);
		}

		public ActionResult Edit(int id)
		{
			var model = Load(id);
			return View("Manage", model);
		}

		public ActionResult Save(RecipeModel model)
		{
			model.Recipe.Guid = Guid.NewGuid().ToString();
			model.Recipe.Name.HtmlEncode();
			model.Recipe.Description.HtmlEncode();
			model.Recipe.Ingredients.HtmlEncode();
			var result = Repository.Save(model.Recipe);

			if (result)
			{
				ViewBag.Response = Globals.InfoRecipeUpdated;
				return View("Response");
			}
			else
			{
				ViewBag.Response = Globals.ErrorSavingRecipe;
				return View("Response");
			}
		}

		// TODO: Add functionality for voting for a recipe
		public ActionResult Vote(int rating)
		{
			// Send in rating from view
			// Save vote
			// Return result view (success or error)
			//if (RatingRepository.Instance.UserHasVoted(SessionHandler.User.Username, SessionHandler.CurrentGuid))
			//{
			//	ViewBag.Response = Globals.ErrorUserHasVoted;
			//}
			//else
			//{
			//	var voted = RatingRepository.Instance.Save(SessionHandler.CurrentGuid, SessionHandler.User.Username, Convert.ToInt32(ratingHidden.Value));
			//	if (voted)
			//	{
			//		infoRatingLabel.Text = Globals.InfoVoteSaved;
			//		infoRatingLabel.Visible = true;
			//	}
			//	else
			//	{
			//		infoRatingLabel.Text = Globals.ErrorSavingVote;
			//		infoRatingLabel.Visible = true;
			//	}
			//}
			return View();
		}

		public ActionResult Delete(int id)
		{
			var recipe = RecipeRepository.Instance.GetById(id);
			RecipeRepository.Instance.DeleteSpecials(recipe.Guid);
			RecipeRepository.Instance.Delete(recipe.Guid);
			ViewBag.Response = Globals.InfoRecipeDeleted;
			return View("Response");
		}

		private RecipeModel Load(int id = 0)
		{
			var model = new RecipeModel();
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
			
			categoryItems.AddRange(allCategories.Select(category => new SelectListItem { Text = category.Name.HtmlDecode(), Value = category.Id.ToString() }));
			dishTypeItems.AddRange(allDishTypes.Select(dishType => new SelectListItem { Text = dishType.Name.HtmlDecode(), Value = dishType.Id.ToString() }));
			
			model.Recipe = new Recipe();
			model.CategoryList = categoryItems;
			model.DishTypeList = dishTypeItems;
			model.Portions = portionItems;
			model.SpecialList = allSpecials;

			if (id != 0)
			{
				var recipe = Repository.GetById(id);
				var category = Repository.GetCategoryById(recipe.CategoryId).HtmlDecode();
				var specials = Repository.GetSpecialsForRecipe(recipe.Guid);
				var specialsList = (from item in specials
									from special in allSpecials
									where item.SpecialId == special.Id
									select special).Aggregate(String.Empty, (current, special) => current + (special.Name.HtmlDecode() + ", "));
				var dishType = string.Empty;
				var avgRating = RatingRepository.GetAvarage(recipe.Guid);
				if (recipe.DishTypeId.HasValue)
				{
					dishType = Repository.GetDishTypeById(recipe.DishTypeId.Value).HtmlDecode();
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