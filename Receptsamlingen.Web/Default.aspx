<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Site.Master" CodeBehind="Default.aspx.cs" Inherits="Receptsamlingen.Web.Default" %>

<asp:Content ID="headContent" ContentPlaceHolderID="headContent" runat="server">
</asp:Content>
<asp:Content ID="mainContent" ContentPlaceHolderID="mainContent" runat="server">
	<h1>Receptsamlingen</h1>
	<p>Här kan du söka i en ständigt växande samling av recept av olika slag. För att lägga in ett recept själv 
	måste du dock logga in. Har du inget konto? Gå till <a href="/ansok">kontosidan</a> för att skaffa ett.</p>
	<p>Nu kan du ge betyg på recept när du är inloggad, se genomsnittsbetyg på recept och se vilka recept som ligger på topplistan.</p>
	<div>
		<h2>10 senaste recepten</h2>
		<asp:Repeater ID="latestRepeater" runat="server">
			<ItemTemplate>
				&rsaquo;&rsaquo;&nbsp;<asp:HyperLink ID="itemHyperLink" runat="server" />
			</ItemTemplate>
			<SeparatorTemplate>
				<br />
			</SeparatorTemplate>
		</asp:Repeater>
		<asp:Label ID="noRecipesLabel" runat="server" Text="Det finns inga recept ännu." Visible="false" />
	</div>
</asp:Content>