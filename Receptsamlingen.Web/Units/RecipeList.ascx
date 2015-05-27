<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RecipeList.ascx.cs" Inherits="Receptsamlingen.Web.Units.RecipeList" %>

<h1>Alla recept</h1>
<asp:Repeater ID="allRecipesRepeater" runat="server">
	<ItemTemplate>
		<span class="recipe-list">&rsaquo;&rsaquo;&nbsp;<asp:HyperLink ID="itemHyperLink" runat="server" /></span>
	</ItemTemplate>
</asp:Repeater>