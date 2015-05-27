using System;
using System.Web.UI;
using Receptsamlingen.Web.Classes;

namespace Receptsamlingen.Web.Units
{
	public partial class RandomBox : UserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			randomLinkButton.Click += OnRandomLinkButtonClick;
		}

		#region Events

		private void OnRandomLinkButtonClick(object sender, EventArgs e)
		{
			var idList = SessionHandler.RecipeIdList;
			var total = idList.Count;
			var randomNumber = new Random().Next(0,total-1);
			var generatedRecipeID = idList[randomNumber];
			Response.Redirect(String.Format("~/recept/{0}", generatedRecipeID));
		}

		#endregion

	}
}