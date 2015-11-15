using System;
using System.Web;
using System.Web.UI;
using Receptsamlingen.Repository;
using Receptsamlingen.Web.Classes;
using Globals = Receptsamlingen.Web.Classes.Globals;

namespace Receptsamlingen.Web.Units
{
    public partial class LoginBox : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
			loginButton.Click += OnLoginClick;
	        logoutButton.Click += OnLogoutClick;

			if (!IsPostBack)
			{
				if (SessionHandler.User != null)
				{
					SetLoggedInView();
				}
				else
				{
					SetLoggedOutView();
				}
			}
        }
		
		private void SetLoggedOutView()
		{
			loginPanel.Visible = true;
		}

		private void SetLoggedInView()
		{
			loggedinLabel.Text = "Inloggad som: " + SessionHandler.User.Username;
			loginPanel.Visible = false;
			logoutPanel.Visible = true;
		}

        #region Events

        protected void OnLogoutClick(object sender, EventArgs e)
        {
            Common.Logout();
			Response.Redirect(Globals.DefaultUrl);
        }

        protected void OnLoginClick(object sender, EventArgs e)
        {
            var username = HttpUtility.HtmlEncode(userNameTextbox.Text.Trim());
	        var password = HttpUtility.HtmlEncode(passwordTextbox.Text);
            var user = UserRepository.Instance.Get(username, password);

            if (user != null)
            {
				SessionHandler.User = user;
				SetLoggedInView();
            }
            else
            {
                errorLabel.Visible = true;
            }
        }

        #endregion

    }
}