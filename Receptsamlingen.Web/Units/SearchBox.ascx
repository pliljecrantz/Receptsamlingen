<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchBox.ascx.cs" Inherits="Receptsamlingen.Web.Units.SearchBox" %>

<div class="box-wrapper">
	<div class="box-content">
		<asp:Panel ID="searchBoxPanel" runat="server" DefaultButton="searchImageButton">
			<div class="row">
				<div class="col-xs-11">
					<asp:TextBox ID="searchTextbox" runat="server" CssClass="form-control" placeholder="Sök..." />
				</div>
				<asp:ImageButton ID="searchImageButton" runat="server" ImageUrl="~/Images/icon-search_small.png" />
			</div>
		</asp:Panel>
	</div>
</div>