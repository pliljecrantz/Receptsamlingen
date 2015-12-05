﻿using System;
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

		// TODO: Fix so there´s only one form sending in the whole page, need because otherwise the Save method gets a null model
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
			ViewBag.Response = result ? Globals.InfoRecipeUpdated : Globals.ErrorSavingRecipe;
			return View("Response");
		}

		[HttpPost]
		public ActionResult Vote(RecipeModel model)
		{
			if (RatingRepository.Instance.UserHasVoted(SessionHandler.User.Username, SessionHandler.CurrentGuid))
			{
				ViewBag.Response = Globals.ErrorUserHasVoted;
			}
			else
			{
				var voteSuccess = RatingRepository.Instance.Save(SessionHandler.CurrentGuid, SessionHandler.User.Username, Convert.ToInt32(model.UserRating));
				ViewBag.Response = voteSuccess ? Globals.InfoVoteSaved : Globals.ErrorSavingVote;
			}
			return View("Response");
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
			var model = GetModel();

			if (id != 0)
			{
				var recipe = Repository.GetById(id);
				var category = Repository.GetCategoryById(recipe.CategoryId).HtmlDecode();
				var specials = Repository.GetSpecialsForRecipe(recipe.Guid);
				var specialsList = (from item in specials
									from special in model.SpecialList
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

			categoryItems.AddRange(
				allCategories.Select(category => new SelectListItem {Text = category.Name, Value = category.Id.ToString()}));
			dishTypeItems.AddRange(
				allDishTypes.Select(dishType => new SelectListItem {Text = dishType.Name, Value = dishType.Id.ToString()}));

			model.Recipe = new Recipe();
			model.CategoryList = categoryItems;
			model.DishTypeList = dishTypeItems;
			model.Portions = portionItems;
			model.SpecialList = allSpecials;
			return model;
		}
	}
}