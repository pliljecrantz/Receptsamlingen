using System.Web.Mvc;
using Receptsamlingen.Mvc.Classes;
using Receptsamlingen.Mvc.Models;
using Receptsamlingen.Repository;

namespace Receptsamlingen.Mvc.Controllers
{
    public class LoginController : Controller
    {
        private UserRepository Repository { get; set; }

        public LoginController()
        {
	        Repository = UserRepository.Instance;
        }

        [HttpPost]
        public ActionResult DoLogin(LoginModel model)
        {
            if (!string.IsNullOrWhiteSpace(model.Username) && !string.IsNullOrWhiteSpace(model.Password))
            {
                var username = Server.HtmlEncode(model.Username.Trim());
                var password = Server.HtmlEncode(model.Password.Trim());
                var user = Repository.Get(username, password);

                if (user != null)
                {
                    SessionHandler.User = user;
	                SessionHandler.IsAuthenticated = true;
                    model.LoggedIn = true;
                }
                else
                {
	                SessionHandler.IsAuthenticated = false;
                    model.LoggedIn = false;
                }
            }
            return RedirectToAction("Index", "Home");
        }

		[HttpPost]
        public ActionResult DoLogout()
        {
            SessionHandler.Remove(Globals.UserSessionKeyString);
			SessionHandler.Remove(Globals.IsAuthenticatedSessionString);
            return RedirectToAction("Index", "Home");
        }
    }
}