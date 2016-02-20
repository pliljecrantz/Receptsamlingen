using System.Web.Mvc;
using Ninject;
using Receptsamlingen.Mvc.Classes;
using Receptsamlingen.Mvc.Models.Partials;
using Receptsamlingen.Repository.Interfaces;

namespace Receptsamlingen.Mvc.Controllers
{
    public class LoginController : Controller
    {
        [Inject]
		public IUserRepository UserRepository { get; set; }

		public LoginController()
		{
			this.Inject();
		}

        public ActionResult DoLogin(LoginModel model)
        {
            if (!string.IsNullOrWhiteSpace(model.Username) && !string.IsNullOrWhiteSpace(model.Password))
            {
                var username = Server.HtmlEncode(model.Username.Trim());
                var password = Server.HtmlEncode(model.Password.Trim());
                var user = UserRepository.Get(username, password);

                if (user != null)
                {
                    SessionHandler.User = user;
	                SessionHandler.IsAuthenticated = true;
					SessionHandler.FailedLogin = false;
                }
                else
                {
	                SessionHandler.IsAuthenticated = false;
					SessionHandler.FailedLogin = true;
                }
            }
			return RedirectToAction("Index", "Home");
        }

        public ActionResult DoLogout()
        {
            SessionHandler.Remove(Globals.UserSessionKeyString);
			SessionHandler.Remove(Globals.IsAuthenticatedSessionString);
            return RedirectToAction("Index", "Home");
        }
    }
}