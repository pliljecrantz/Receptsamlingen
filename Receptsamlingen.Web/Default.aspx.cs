using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using Receptsamlingen.Repository;
using Recipe = Receptsamlingen.Repository.Recipe;

namespace Receptsamlingen.Web
{
	public partial class Default : Page
	{

		#region Members

		IList<Recipe> _recipes;

		#endregion

		protected void Page_Load(object sender, EventArgs e)
		{

			#region Eventlisteners

			latestRepeater.ItemDataBound += OnLatestRepeaterItemDataBound;

			#endregion

			if (!IsPostBack)
			{
				LoadRecipes();
			}
		}

		private void LoadRecipes()
		{
			_recipes = RecipeRepository.Instance.GetLatest();

			if (_recipes != null && _recipes.Count > 0)
			{
				latestRepeater.DataSource = _recipes;
				latestRepeater.DataBind();
			}
			else
			{
				noRecipesLabel.Visible = true;
			}
		}

		#region Events

		private void OnLatestRepeaterItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				Recipe item = (Recipe)e.Item.DataItem;
				if (item != null)
				{
					var itemHyperLink = e.Item.FindControl("itemHyperLink") as HyperLink;
					if (itemHyperLink != null)
					{
						itemHyperLink.Text = item.Name != null ? item.Name : String.Empty;
						itemHyperLink.NavigateUrl = String.Format("~/recept/{0}", item.Id);
					}
				}
			}
		}

		#endregion

	}
}