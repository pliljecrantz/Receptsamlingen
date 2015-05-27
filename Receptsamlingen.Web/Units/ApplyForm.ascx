<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ApplyForm.ascx.cs" Inherits="Receptsamlingen.Web.Units.ApplyForm" %>

<h1>Ansök om konto</h1>
<div class="row">
	<div class="col-md-12">
		<asp:TextBox ID="fullNameTextBox" runat="server" CssClass="form-control" placeholder="Ditt namn" />
		<br />
		<asp:TextBox ID="emailaddressTextBox" runat="server" CssClass="form-control" placeholder="E-postadress" />
		<br />
		<asp:TextBox ID="userNameTextbox" runat="server" CssClass="form-control" placeholder="Önskat användarnamn" />
	</div>
</div>
<div class="row">
	<div class="col-md-12">
		<p>OBS! Var noga med att ange rätt e-postadress, en bekräftelse skickas dit inom några dagar.</p>
		<p>Välj en ruta för att godkännas som kontoinnehavare.</p>
		<asp:CheckBox ID="humanCheckBox" runat="server" Text="Jag är en människa." />
		<br />
		<asp:CheckBox ID="alienCheckBox" runat="server" Text="Jag är en alien." />
		<br /><br />
	</div>
</div>
<div class="row">
	<div class="col-md-8 margin-top-5">
		<asp:Label ID="errorLabel" runat="server" Visible="false" CssClass="label label-warning info-text" />
		<asp:Label ID="doneLabel" runat="server" Visible="false" CssClass="label label-info info-text" Text="Tack, din ansökan är skickad. Svar skickas till e-postadressen du angav." />
	</div>
	<div class="col-md-4">
		<asp:Button ID="applyButton" runat="server" CssClass="btn btn-warning float-right" Text="Ansök!" />
	</div>
</div>