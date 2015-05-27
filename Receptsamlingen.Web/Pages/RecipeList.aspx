<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="~/MasterPages/Site.Master" CodeBehind="RecipeList.aspx.cs" Inherits="Receptsamlingen.Web.Pages.RecipeList" %>
<%@ Register TagPrefix="Artep" TagName="RecipeList" Src="~/Units/RecipeList.ascx" %>

<asp:Content ID="headContent" ContentPlaceHolderID="headContent" runat="server"></asp:Content>
<asp:Content ID="mainContent" ContentPlaceHolderID="mainContent" runat="server">
	<Artep:RecipeList ID="recipeListControl" runat="server" />
</asp:Content>