<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Site.Master" CodeBehind="Search.aspx.cs" Inherits="Receptsamlingen.Web.Pages.Search" %>

<asp:Content ID="headContent" ContentPlaceHolderID="headContent" runat="server"></asp:Content>
<asp:Content ID="mainContent" ContentPlaceHolderID="mainContent" runat="server">
	<h1>Sök</h1>
	<div class="row">
		<div class="col-xs-10">
			<asp:TextBox ID="searchTextBox" runat="server" CssClass="form-control" placeholder="Fritextsök..." />
		</div>
		<div class="col-xs-2 search-button">
			<asp:Button ID="searchButton" runat="server" CssClass="btn btn-warning" Text="Sök" />
		</div>
	</div>
	<br />
	<div class="row">
		<div class="col-md-12">
			<asp:PlaceHolder ID="noResultPlaceHolder" runat="server" Visible="false">
				<asp:Label runat="server" Text="Sökningen gav inga resultat." CssClass="label label-warning info-text" />
				<br />
			</asp:PlaceHolder>
		</div>
	</div>
	<div class="row">
		<div class="col-md-12">
			<asp:PlaceHolder ID="searchResultPlaceHolder" runat="server" Visible="false">
				<h2>Sökresultat</h2>
				<asp:Repeater ID="searchResultRepeater" runat="server">
					<ItemTemplate>
						&rsaquo;&rsaquo;&nbsp;<asp:HyperLink ID="itemHyperLink" CssClass="Text" runat="server" />
					</ItemTemplate>
					<SeparatorTemplate>
						<br />
					</SeparatorTemplate>
				</asp:Repeater>
				<br />
			</asp:PlaceHolder>
		</div>
	</div>
</asp:Content>