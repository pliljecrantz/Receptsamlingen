using System.Web.Mvc;
using Receptsamlingen.Mvc.Classes;
using Receptsamlingen.Mvc.Models;
using Receptsamlingen.Repository;

namespace Receptsamlingen.Mvc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
			var recipes = RecipeRepository.Instance.GetLatest();
			SetRecipeIdList();

            var model = new HomeModel
                {
                    Recipes = recipes
                };

            return View(model);
        }

		private static void SetRecipeIdList()
		{
			var idList = RecipeRepository.Instance.GetAllIds();
			if (idList != null && idList.Count > 0)
			{
				SessionHandler.RecipeIdList = idList;
			}
		}
    }
}