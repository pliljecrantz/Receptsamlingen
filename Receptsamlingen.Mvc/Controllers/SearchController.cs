using System.Web.Mvc;
using Receptsamlingen.Mvc.Classes;
using Receptsamlingen.Mvc.Models;
using Receptsamlingen.Repository;

namespace Receptsamlingen.Mvc.Controllers
{
    public class SearchController : Controller
    {
        public ActionResult Index()
        {
            return View(new SearchModel());
        }

		// TODO: Implement advanced search
	    [HttpPost]
	    public ActionResult DoSearch(SearchModel model)
	    {
			model.SearchResult = RecipeRepository.Instance.Search(model.Query.HtmlEncode());
		    model.SearchPerformed = true;
		    return View("Index", model);
	    }
    }
}