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
                var username = model.Username.Trim().RemoveHtml();
                var password = model.Password.Trim().RemoveHtml();
                var user = Repository.Get(username, password);

                if (user != null)
                {
                    SessionHandler.User = user;
                    model.LoggedIn = true;
                }
                else
                {
                    model.LoggedIn = false;
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult DoLogout()
        {
            SessionHandler.Remove("User");
            return RedirectToAction("Index", "Home");
        }
    }
}