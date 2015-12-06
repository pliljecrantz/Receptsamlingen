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
					var emailaddress = model.Email.Trim().HtmlEncode();
					var fullName = model.FullName.Trim().HtmlEncode();
					var username = model.Username.Trim().HtmlEncode();
					if (!Helper.ValidateEmail(emailaddress))
					{
						ViewBag.Response = Globals.ErrorInvalidEmail;
						ViewBag.Result = false.ToString();
					}
					else
					{
						Helper.GenerateMail(emailaddress, fullName, username);
						ViewBag.Response = Globals.InfoApplyApproved;
						ViewBag.Result = true.ToString();
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
    }
}