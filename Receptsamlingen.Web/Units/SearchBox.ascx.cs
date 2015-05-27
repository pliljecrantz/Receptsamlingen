using System;
using System.Web.UI;

namespace Receptsamlingen.Web.Units
{
    public partial class SearchBox : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
			searchImageButton.Click += OnSearchClick;
        }

        #region Events

        private void OnSearchClick(object sender, EventArgs e)
        {
            var searchText = searchTextbox.Text.Trim();
            Response.Redirect(String.Format("~/sok/{0}", searchText));
        }

        #endregion

    }
}