using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Receptsamlingen.Mvc.Models;
using Receptsamlingen.Repository;
using Receptsamlingen.Repository.Domain;

namespace Receptsamlingen.Mvc.Controllers
{
    public class RecipeController : Controller
    {
        private RecipeRepository Repository { get; set; }

        public RecipeController()
        {
	        Repository = RecipeRepository.Instance;
        }

        public ActionResult Index(int? id)
        {
            if (id.HasValue)
            {
                var receipe = Repository.GetById(id.Value);
                return View("Detail", new RecipeModel
                {
                    CurrentRecipe = receipe
                });
            }

            var categories = Repository.GetAllCategories();
            var dishTypes = Repository.GetAllDishTypes();
            var portionItems = new List<SelectListItem>
                {
                    new SelectListItem { Text = "1", Value = "1" },
                    new SelectListItem { Text = "2", Value = "2" },
                    new SelectListItem { Text = "3", Value = "3" },
                    new SelectListItem { Text = "4", Value = "4" },
                    new SelectListItem { Text = "5", Value = "5" },
                    new SelectListItem { Text = "6", Value = "6" },
                    new SelectListItem { Text = "7", Value = "7" },
                    new SelectListItem { Text = "8", Value = "8" },
                };

            return View("Add", new AddModel
                {
                    NewRecipe = new Recipe(),
                    Categories = categories,
                    DishTypes = dishTypes,
                    Portions = portionItems
                });
        }

        public ActionResult Save(AddModel model)
        {
	        model.NewRecipe.Guid = Guid.NewGuid().ToString();
            var result = Repository.Save(model.NewRecipe);

            if (result > 0)
            {
                return View("Done");
            }
            else
            {
                // Show error message
            }
			return null; // TODO: remove
        }
    }
}