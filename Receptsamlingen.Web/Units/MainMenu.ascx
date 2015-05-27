<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MainMenu.ascx.cs" Inherits="Receptsamlingen.Web.Units.MainMenu" %>

<nav class="navbar navbar-inverse navbar-fixed-top" role="navigation">
	<div class="container">
		<div class="navbar-header">
			<button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#navbar" aria-expanded="false" aria-controls="navbar">
				<span class="sr-only">Toggle navigation</span>
				<span class="icon-bar"></span>
				<span class="icon-bar"></span>
				<span class="icon-bar"></span>
			</button>
			<a class="navbar-brand" href="/">
				<img alt="Brand" src="../Images/brand.png">
			</a>
		</div>
		<div id="navbar" class="navbar-collapse collapse">
			<ul class="nav navbar-nav">
				<li role="presentation">
					<asp:HyperLink runat="server" NavigateUrl="~/recept" Text="Lägg in recept" />
				</li>
				<li role="presentation">
					<asp:HyperLink runat="server" NavigateUrl="~/receptlista" Text="Alla recept" />
				</li>
				<li role="presentation">
					<asp:HyperLink runat="server" NavigateUrl="~/sok" Text="Sök" />
				</li>
				<li role="presentation">
					<asp:HyperLink runat="server" NavigateUrl="~/ansok" Text="Skaffa konto" />
				</li>
			</ul>
		</div>
	</div>
</nav>