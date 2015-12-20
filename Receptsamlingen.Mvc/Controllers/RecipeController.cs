using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Ninject;
using Receptsamlingen.Mvc.Classes;
using Receptsamlingen.Mvc.Models;
using Receptsamlingen.Repository;
using Receptsamlingen.Repository.Interfaces;

namespace Receptsamlingen.Mvc.Controllers
{
	public class RecipeController : Controller
	{
		[Inject]
		public IRecipeRepository RecipeRepository { get; set; }
		[Inject]
		public IRatingRepository RatingRepository { get; set; }
		[Inject]
		public IHelper Helper { get; set; }

		public RecipeController()
		{
			this.Inject();
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
			if (SessionHandler.CurrentGuid == null)
			{
				model.Recipe.Guid = Guid.NewGuid().ToString();
			}
			model.Recipe.Name.HtmlEncode();
			model.Recipe.Description.HtmlEncode();
			model.Recipe.Ingredients.HtmlEncode();
			model.Recipe.CategoryId = Convert.ToInt32(model.SelectedCategory);
			model.Recipe.DishTypeId = Convert.ToInt32(model.SelectedDishType);
			model.Recipe.Portions = Convert.ToInt32(model.SelectedPortions);

			var specials = Helper.GetSelectedSpecials(model.PostedSpecials);
			
			var result = RecipeRepository.Save(model.Recipe);
			if (result)
			{
				foreach (var special in specials)
				{
					RecipeRepository.SaveSpecial(model.Recipe.Guid, special.Id);
				}
				ViewBag.Response = SessionHandler.CurrentGuid == null ? Globals.InfoRecipeSaved : Globals.InfoRecipeUpdated;
			}
			else
			{
				ViewBag.Response = Globals.ErrorSavingRecipe;
			}
			return View("Response");
		}

		public ActionResult Vote(RecipeModel model)
		{
			if (RatingRepository.UserHasVoted(SessionHandler.User.Username, SessionHandler.CurrentGuid))
			{
				ViewBag.Response = Globals.ErrorUserHasVoted;
			}
			else
			{
				var voteSuccess = RatingRepository.Save(SessionHandler.CurrentGuid, SessionHandler.User.Username, Convert.ToInt32(model.UserRating));
				ViewBag.Response = voteSuccess ? Globals.InfoVoteSaved : Globals.ErrorSavingVote;
			}
			return View("Response");
		}

		public ActionResult Delete(int id)
		{
			var recipe = RecipeRepository.GetById(id);
			RecipeRepository.DeleteSpecials(recipe.Guid);
			RecipeRepository.Delete(recipe.Guid);
			ViewBag.Response = Globals.InfoRecipeDeleted;
			return View("Response");
		}

		private RecipeModel Load(int id = 0)
		{
			var model = GetModel();

			if (id != 0)
			{
				var recipe = RecipeRepository.GetById(id);
				var category = RecipeRepository.GetCategoryById(recipe.CategoryId).HtmlDecode();
				var specials = RecipeRepository.GetSpecialsForRecipe(recipe.Guid);
				var specialsList = (from item in specials
									from special in model.SpecialList
									where item.SpecialId == special.Id
									select special).Aggregate(String.Empty, (current, special) => current + (special.Name + ", "));
				var dishType = string.Empty;
				var avgRating = RatingRepository.GetAvarage(recipe.Guid);
				if (recipe.DishTypeId.HasValue)
				{
					dishType = RecipeRepository.GetDishTypeById(recipe.DishTypeId.Value);
				}

				if (specialsList.EndsWith(", "))
				{
					specialsList = specialsList.TrimEnd(',', ' ');
				}

				model.Recipe = recipe;
				model.Recipe.Name.HtmlDecode();
				model.Recipe.Ingredients.HtmlDecode();
				model.Recipe.Description.HtmlDecode();
				model.Category = category;
				model.DishType = dishType;
				model.AvarageRating = avgRating;
				model.Specials = specialsList;
				SessionHandler.CurrentGuid = model.Recipe.Guid;
			}
			return model;
		}

		private RecipeModel GetModel()
		{
			var model = new RecipeModel();
			var allCategories = RecipeRepository.GetAllCategories();
			var allDishTypes = RecipeRepository.GetAllDishTypes();
			var allSpecials = RecipeRepository.GetAllSpecials();

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
				new SelectListItem {Text = "--- Välj ---", Value = "0"},
				new SelectListItem {Text = "1", Value = "1"},
				new SelectListItem {Text = "2", Value = "2"},
				new SelectListItem {Text = "3", Value = "3"},
				new SelectListItem {Text = "4", Value = "4"},
				new SelectListItem {Text = "5", Value = "5"},
				new SelectListItem {Text = "6", Value = "6"},
				new SelectListItem {Text = "7", Value = "7"},
				new SelectListItem {Text = "8", Value = "8"},
			};

			categoryItems.AddRange(allCategories.Select(category => new SelectListItem {Text = category.Name, Value = category.Id.ToString()}));
			dishTypeItems.AddRange(allDishTypes.Select(dishType => new SelectListItem {Text = dishType.Name, Value = dishType.Id.ToString()}));

			model.Recipe = new Recipe();
			model.CategoryList = categoryItems;
			model.DishTypeList = dishTypeItems;
			model.Portions = portionItems;
			model.SpecialList = allSpecials;
			return model;
		}
	}
}