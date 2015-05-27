using System;
using System.Web.UI;
using Receptsamlingen.Web.Classes;

namespace Receptsamlingen.Web.Units
{
	public partial class ApplyForm : UserControl
	{

		#region Members

		private const string ErrorOnlyOneBoxCanBeSelected = "Du kan bara välja en av rutorna.";
		private const string ErrorMustChooseBox = "Du måste välja en av rutorna.";
		private const string ErrorNoAccountGranted = "Du får inget konto.";
		private const string ErrorInvalidEmail = "Ogiltig e-postadress.";

		#endregion

		protected void Page_Load(object sender, EventArgs e)
		{
			#region Eventlisteners

			applyButton.Click += OnApplyButtonClick;

			#endregion
		}

		#region Events

		protected void OnApplyButtonClick(object sender, EventArgs e)
		{
			if (humanCheckBox.Checked && alienCheckBox.Checked)
			{
				errorLabel.Text = ErrorOnlyOneBoxCanBeSelected;
				errorLabel.Visible = true;
				doneLabel.Visible = false;
			}
			else if (alienCheckBox.Checked && !humanCheckBox.Checked)
			{
				errorLabel.Text = ErrorNoAccountGranted;
				errorLabel.Visible = true;
				doneLabel.Visible = false;
			}
			else if (!alienCheckBox.Checked && humanCheckBox.Checked)
			{
				var emailaddress = emailaddressTextBox.Text.Trim();
				var fullName = fullNameTextBox.Text.Trim();
				var username = userNameTextbox.Text.Trim();

				var valid = Helper.ValidateEmail(emailaddress);

				if (!valid)
				{
					errorLabel.Text = ErrorInvalidEmail;
					errorLabel.Visible = true;
					doneLabel.Visible = false;
				}
				else
				{
					Common.GenerateMail(emailaddress, fullName, username);
					errorLabel.Text = String.Empty;
					errorLabel.Visible = false;
					doneLabel.Visible = true;
					emailaddressTextBox.Text = String.Empty;
					fullNameTextBox.Text = String.Empty;
					userNameTextbox.Text = String.Empty;
					alienCheckBox.Checked = false;
					humanCheckBox.Checked = false;
				}
			}
			else
			{
				errorLabel.Text = ErrorMustChooseBox;
				errorLabel.Visible = true;
				doneLabel.Visible = false;
			}
		}

		#endregion
	}
}