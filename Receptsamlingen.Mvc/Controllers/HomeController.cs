using System.Web.Mvc;
using Receptsamlingen.Mvc.Models;
using Receptsamlingen.Repository;

namespace Receptsamlingen.Mvc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var repository = RecipeRepository.Instance;
            var recipes = repository.GetLatest();

            var model = new HomeModel
                {
                    Recipes = recipes
                };

            return View(model);
        }
    }
}