using System.Web.Mvc;
using Receptsamlingen.Mvc.Classes;
using Receptsamlingen.Mvc.Models;

namespace Receptsamlingen.Mvc.Controllers
{
    public class ApplyController : Controller
    {
        public ActionResult Index()
        {
            return View(new ApplyModel());
        }

		[HttpPost]
	    public ActionResult DoApply(ApplyModel model)
	    {
			if (model.Human && model.Alien)
			{
				model.Response = Globals.ErrorOnlyOneBoxCanBeSelected;
				model.Result = false.ToString();
			}
			else if (model.Alien && !model.Human)
			{
				model.Response = Globals.ErrorNoAccountGranted;
				model.Result = false.ToString();
			}
			else if (!model.Alien && model.Human)
			{
				if (model.Email != null && model.FullName != null && model.Username != null)
				{
					var emailaddress = model.Email.Trim().HtmlEncode();
					var fullName = model.FullName.Trim().HtmlEncode();
					var username = model.Username.Trim().HtmlEncode();
					if (!Helper.ValidateEmail(emailaddress))
					{
						model.Response = Globals.ErrorInvalidEmail;
						model.Result = false.ToString();
					}
					else
					{
						Helper.GenerateMail(emailaddress, fullName, username);
						model.Response = Globals.InfoApplyApproved;
						model.Result = true.ToString();
					}
				}
				else
				{
					model.Response = Globals.ErrorMustGiveInfo;
					model.Result = false.ToString();
				}
			}
			else
			{
				model.Response = Globals.ErrorMustChooseBox;
				model.Result = false.ToString();
			}
			return View("Index", model);
	    }
    }
}