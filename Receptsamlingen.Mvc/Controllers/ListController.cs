using System.Web.Mvc;
using Receptsamlingen.Mvc.Models;
using Receptsamlingen.Repository;

namespace Receptsamlingen.Mvc.Controllers
{
    public class ListController : Controller
    {
		public ActionResult Index()
		{
			var repository = RecipeRepository.Instance;
			var recipes = repository.GetAll();

			var model = new ListModel
				{
					Recipes = recipes
				};

			return View(model);
        }
    }
}