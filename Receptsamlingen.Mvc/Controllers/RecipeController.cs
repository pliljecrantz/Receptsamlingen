﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Ninject;
using Receptsamlingen.Mvc.Classes;
using Receptsamlingen.Mvc.Models;
using Receptsamlingen.Repository;
using Receptsamlingen.Repository.Interfaces;
using System.Web;
using Logger;

namespace Receptsamlingen.Mvc.Controllers
{
    public class RecipeController : Controller
    {
        [Inject]
        public IRecipeRepository RecipeRepository { get; set; }
        [Inject]
        public IRatingRepository RatingRepository { get; set; }

        public RecipeController()
        {
            this.Inject();
        }

        public ActionResult Add()
        {
            ViewBag.Title = "Lägg till recept";
            var model = Load();
            SessionHandler.CurrentGuid = null;
            SessionHandler.CurrentId = null;
            return View("Manage", model);
        }

        public ActionResult Id(int id)
        {
            var model = Load(id);
            ViewBag.Title = model.Recipe.Name;
            return View("Detail", model);
        }

        public ActionResult Edit(int id)
        {
            var model = Load(id);
            ViewBag.Title = model.Recipe.Name;
            return View("Manage", model);
        }

        public ActionResult Save(RecipeModel model)
        {
            // If these seesions are null then it´s a new recipe to save
            if (SessionHandler.CurrentGuid == null && SessionHandler.CurrentId == null)
            {
                model.Recipe.Guid = Guid.NewGuid().ToString();
            }
            else
            {
                model.Recipe.Guid = SessionHandler.CurrentGuid;
                model.Recipe.Id = Convert.ToInt32(SessionHandler.CurrentId);
            }

            model.Recipe.Name.HtmlEncode();
            model.Recipe.Description.HtmlEncode();
            model.Recipe.Ingredients.HtmlEncode();
            model.Recipe.CategoryId = Convert.ToInt32(model.SelectedCategory);
            model.Recipe.DishTypeId = Convert.ToInt32(model.SelectedDishType);
            model.Recipe.Portions = Convert.ToInt32(model.SelectedPortions);

            var specials = GetSelectedSpecials(model.PostedSpecials);

            var result = RecipeRepository.Save(model.Recipe);
            if (result)
            {
                // Delete any specials saved to the recipe first
                var deleted = RecipeRepository.DeleteSpecials(model.Recipe.Guid);

                // Then save the new specials added to the recipe
                foreach (var special in specials)
                {
                    var saved = RecipeRepository.SaveSpecial(model.Recipe.Guid, special.Id);
                }

                if (model.Recipe.Id == 0) // This means that the recipe is a new one and we need to fetch the id for further use
                {
                    model.Recipe.Id = RecipeRepository.GetByGuid(model.Recipe.Guid).Id;
                }
                model.RecipeSaved = true;
                ViewBag.Response = Globals.InfoRecipeSaved;
                SessionHandler.CurrentGuid = model.Recipe.Guid;
                SessionHandler.CurrentId = model.Recipe.Id.ToString();
                SessionHandler.ForceReload = true;
            }
            else
            {
                model.RecipeSaved = false;
                ViewBag.Response = Globals.ErrorSavingRecipe;
            }

            ViewBag.Title = model.Recipe.Name;
            return View("Detail", model);
        }

        public ActionResult Vote(RecipeModel model)
        {
            var ratingSaved = false;
            if (RatingRepository.UserHasVoted(SessionHandler.User.Username, SessionHandler.CurrentGuid))
            {
                ViewBag.Response = Globals.ErrorUserHasVoted;
            }
            else
            {
                var voteSuccess = RatingRepository.Save(SessionHandler.CurrentGuid, SessionHandler.User.Username, Convert.ToInt32(model.UserRating));
                if (voteSuccess)
                {
                    ratingSaved = true;
                    ViewBag.Response = Globals.InfoVoteSaved;
                }
                else
                {
                    ViewBag.Response = Globals.ErrorSavingVote;
                }
            }
            model = Load(Convert.ToInt32(SessionHandler.CurrentId));
            model.RatingSaved = ratingSaved;
            return View("Detail", model);
        }

        public ActionResult Delete(int id)
        {
            var recipe = RecipeRepository.GetById(id);
            RecipeRepository.DeleteSpecials(recipe.Guid);
            RecipeRepository.Delete(recipe.Guid);
            RecipeRepository.DeleteVotes(recipe.Guid);
            ViewBag.Response = Globals.InfoRecipeDeleted;
            SessionHandler.ForceReload = true;
            return View("Response");
        }

        private RecipeModel Load(int id = 0)
        {
            var model = GetModel();

            if (id != 0)
            {
                var recipe = RecipeRepository.GetById(id);
                if (recipe != null)
                {
                    var category = RecipeRepository.GetCategoryById(recipe.CategoryId).HtmlDecode();
                    var specials = RecipeRepository.GetSpecialsForRecipe(recipe.Guid);
                    var selectedSpecials = (from item in specials
                                            from special in model.AllSpecials
                                            where item.SpecialId == special.Id
                                            select special).ToList();
                    var selectedSpecialsAsString = (from item in specials
                                                    from special in model.AllSpecials
                                                    where item.SpecialId == special.Id
                                                    select special).Aggregate(string.Empty, (current, special) => current + (special.Name.ToLower() + ", "));
                    var dishType = string.Empty;
                    var avgRating = RatingRepository.GetAvarage(recipe.Guid);
                    if (recipe.DishTypeId.HasValue)
                    {
                        dishType = RecipeRepository.GetDishTypeById(recipe.DishTypeId.Value);
                    }

                    if (selectedSpecialsAsString.EndsWith(", "))
                    {
                        selectedSpecialsAsString = selectedSpecialsAsString.TrimEnd(',', ' ');
                    }

                    model.Recipe = recipe;
                    model.Recipe.Name.HtmlDecode();
                    model.Recipe.Ingredients.HtmlDecode();
                    model.Recipe.Description.HtmlDecode();
                    model.Category = category;
                    model.DishType = dishType;
                    model.AvarageRating = avgRating;
                    model.Specials = selectedSpecialsAsString;
                    model.SelectedSpecials = selectedSpecials;
                    SessionHandler.CurrentGuid = model.Recipe.Guid;
                    SessionHandler.CurrentId = model.Recipe.Id.ToString();
                }
                else
                {
                    LogHandler.Log(nameof(RecipeController), LogType.Info, string.Format("Recipe with id: {0} could not be found", id));
                    throw new HttpException(404, "Not found");
                }
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

            categoryItems.AddRange(allCategories.Select(category => new SelectListItem { Text = category.Name, Value = category.Id.ToString() }));
            dishTypeItems.AddRange(allDishTypes.Select(dishType => new SelectListItem { Text = dishType.Name, Value = dishType.Id.ToString() }));

            model.Recipe = new Recipe();
            model.AllCategories = categoryItems;
            model.AllDishTypes = dishTypeItems;
            model.Portions = portionItems;
            model.AllSpecials = allSpecials;
            return model;
        }

        private IList<Special> GetSelectedSpecials(PostedSpecials postedSpecials)
        {
            IList<Special> selectedSpecials = new List<Special>();
            var postedSpecialIds = new string[0];

            if (postedSpecials == null)
            {
                postedSpecials = new PostedSpecials();
            }

            if (postedSpecials.Ids != null && postedSpecials.Ids.Any())
            {
                postedSpecialIds = postedSpecials.Ids;
            }

            if (postedSpecialIds.Any())
            {
                selectedSpecials = RecipeRepository.GetAllSpecials().Where(x => postedSpecialIds.Any(s => x.Id.ToString().Equals(s))).ToList();
            }
            return selectedSpecials;
        }

        // TODO: Add functionality for editing categories etc from UI
        // TODO: Add functionality for adding an image to the recipe
    }
}