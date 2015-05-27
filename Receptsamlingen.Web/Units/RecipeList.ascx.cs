using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Receptsamlingen.Repository;
using Recipe = Receptsamlingen.Repository.Recipe;

namespace Receptsamlingen.Web.Units
{
	public partial class RecipeList : UserControl
	{

		protected void Page_Load(object sender, EventArgs e)
		{

			#region Eventlisteners

			allRecipesRepeater.ItemDataBound += OnAllRecipesRepeaterItemDataBound;

			#endregion

			if (!IsPostBack)
			{
				var allRecipes = RecipeRepository.Instance.GetAll();
				if (allRecipes != null && allRecipes.Count > 0)
				{
					allRecipesRepeater.DataSource = allRecipes;
					allRecipesRepeater.DataBind();
				}
			}
		}

		#region Events

		protected void OnAllRecipesRepeaterItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				var item = e.Item.DataItem as Recipe;
				if (item != null)
				{
					var itemHyperLink = e.Item.FindControl("itemHyperLink") as HyperLink;
					if (itemHyperLink != null)
					{
						if (!String.IsNullOrEmpty(item.Guid) && item.Guid != "00000000-0000-0000-0000-000000000000")
						{
							itemHyperLink.Text = item.Name ?? String.Empty;
							itemHyperLink.NavigateUrl = String.Format("~/recept/{0}", item.Id);
						}
					}
				}
			}
		}

		#endregion

	}
}