using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Receptsamlingen.Repository;

namespace Receptsamlingen.Web.Pages
{
	public partial class Search : Page
	{

		#region Members

		public string NameProperty = "Name";
		public string IDProperty = "ID";

		#endregion

		protected void Page_Load(object sender, EventArgs e)
		{

			#region Eventlisteners

			searchButton.Click += OnSearchButtonClick;
			searchResultRepeater.ItemDataBound += OnSearchResultRepeaterItemDataBound;

			#endregion

			if (!IsPostBack)
			{
				var querySearch = Page.RouteData.Values["query"] as string;
				if (querySearch != null)
				{
					var result = RecipeRepository.Instance.Search(querySearch);
					ShowSearchResult(result);
				}
			}
		}

		#region Private methods

		private void ShowSearchResult(IList<Repository.Recipe> recipes)
		{
			if (recipes != null && recipes.Count > 0)
			{
				searchResultRepeater.DataSource = recipes;
				searchResultRepeater.DataBind();
				searchResultPlaceHolder.Visible = true;
			}
			else
			{
				noResultPlaceHolder.Visible = true;
			}
		}

		private void ClearSearchResult()
		{
			searchResultPlaceHolder.Visible = false;
			noResultPlaceHolder.Visible = false;
			searchResultRepeater.DataSource = null;
			searchResultPlaceHolder.Visible = false;
		}

		#endregion

		#region Events

		private void OnSearchResultRepeaterItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				var item = e.Item.DataItem as Repository.Recipe;
				if (item != null)
				{
					var itemHyperLink = e.Item.FindControl("itemHyperLink") as HyperLink;
					if (itemHyperLink != null)
					{
						itemHyperLink.Text = item.Name;
						itemHyperLink.NavigateUrl = String.Format("~/recept/{0}", item.Id);
					}
				}
			}
		}

		private void OnSearchButtonClick(object sender, EventArgs e)
		{
			ClearSearchResult();
			var searchText = HttpUtility.HtmlEncode(searchTextBox.Text.Trim());
			var result = RecipeRepository.Instance.Search(searchText);
			ShowSearchResult(result);
		}

		#endregion

	}
}