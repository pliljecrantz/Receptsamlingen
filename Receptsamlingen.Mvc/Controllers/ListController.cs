using System.Web.Mvc;
using Ninject;
using Receptsamlingen.Mvc.Classes;
using Receptsamlingen.Mvc.Models;
using Receptsamlingen.Repository.Interfaces;

namespace Receptsamlingen.Mvc.Controllers
{
    public class ListController : Controller
    {
	    [Inject]
		public IRecipeRepository RecipeRepository { get; set; }

		public ListController()
		{
            ViewBag.Title = "Alla recept";
            this.Inject();
		}

		public ActionResult Index()
		{
			var recipes = RecipeRepository.GetAll();

			var model = new ListModel
				{
					Recipes = recipes
				};

			return View(model);
        }
    }
}