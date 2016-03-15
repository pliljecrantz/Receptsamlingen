using System.Web.Mvc;
using Ninject;
using Receptsamlingen.Mvc.Classes;
using Receptsamlingen.Mvc.Models;
using Receptsamlingen.Repository.Interfaces;
using Logger;

namespace Receptsamlingen.Mvc.Controllers
{
    public class ApplyController : Controller
    {
        [Inject]
        public IRecipeRepository RecipeRepository { get; set; }

        [Inject]
        public IUserRepository UserRepository { get; set; }

        public ApplyController()
        {
            this.Inject();
        }

        public ActionResult Index()
        {
            return View(new ApplyModel());
        }

        public ActionResult DoApply(ApplyModel model)
        {
            if (model.Human && model.Alien)
            {
                ViewBag.Response = Globals.ErrorOnlyOneBoxCanBeSelected;
                ViewBag.Result = false.ToString();
            }
            else if (model.Alien && !model.Human)
            {
                ViewBag.Response = Globals.ErrorNoAccountGranted;
                ViewBag.Result = false.ToString();
            }
            else if (!model.Alien && model.Human)
            {
                if (model.Email != null && model.FullName != null && model.Username != null)
                {
                    var emailAddress = model.Email.Trim().StripHtml();
                    var fullName = model.FullName.Trim().StripHtml();
                    var userName = model.Username.Trim().StripHtml();
                    if (!Helper.ValidateEmail(emailAddress))
                    {
                        ViewBag.Response = Globals.ErrorInvalidEmail;
                        ViewBag.Result = false.ToString();
                    }
                    else
                    {
                        var userCreated = UserRepository.Create(userName, model.Password, fullName, emailAddress);
                        if (userCreated)
                        {
                            Helper.GenerateMail(emailAddress, fullName, userName, model.Password);
                            ViewBag.Response = Globals.InfoApplyApproved;
                            ViewBag.Result = true.ToString();
                            LogHandler.Log(LogType.Info, string.Format("Account created \t email: {0} fullname: {1} username: {2}", emailAddress, fullName, userName));
                        }
                        else
                        {
                            ViewBag.Response = Globals.ErrorCreatingAccount;
                            ViewBag.Result = false.ToString();
                        }
                    }
                }
                else
                {
                    ViewBag.Response = Globals.ErrorMustGiveInfo;
                    ViewBag.Result = false.ToString();
                }
            }
            else
            {
                ViewBag.Response = Globals.ErrorMustChooseBox;
                ViewBag.Result = false.ToString();
            }
            return View("Index");
        }

        // TODO: Add functionality for creating the user automatically
    }
}