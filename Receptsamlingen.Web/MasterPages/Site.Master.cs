using System;
using System.Web.UI;
using Receptsamlingen.Repository;
using Receptsamlingen.Web.Classes;

namespace Receptsamlingen.Web.MasterPages
{
    public partial class Site : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
			if (!IsPostBack)
			{
				SetTopList();
				SetRecipeIdList();
			}
        }

		private static void SetRecipeIdList()
		{
			var idList = RecipeRepository.Instance.GetAllIds();
			if (idList != null && idList.Count > 0)
			{
				SessionHandler.RecipeIdList = idList;
			}
		}

		private void SetTopList()
		{
			var topList = RecipeRepository.Instance.GetToplist();
			topListControl.Source = topList;
		}
    }
}