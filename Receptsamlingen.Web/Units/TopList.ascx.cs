using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Receptsamlingen.Repository;

namespace Receptsamlingen.Web.Units
{
	public partial class TopList : UserControl
	{

		#region Members

		public IList<Recipe> Source { get; set; }

		#endregion

		protected void Page_Load(object sender, EventArgs e)
		{

			#region Eventlisteners

			topListRepeater.ItemDataBound += OnTopListRepeaterItemDataBound;

			#endregion

			if (!IsPostBack)
			{
				if (Source != null && Source.Count > 0)
				{
					topListRepeater.DataSource = Source;
					topListRepeater.DataBind();
					topListPlaceHolder.Visible = true;
					noTopListPlaceHolder.Visible = false;
				}
				else
				{
					topListPlaceHolder.Visible = false;
					noTopListPlaceHolder.Visible = true;
				}
			}
		}

		#region Events

		protected void OnTopListRepeaterItemDataBound(object sender, RepeaterItemEventArgs e)
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