<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LoginBox.ascx.cs" Inherits="Receptsamlingen.Web.Units.LoginBox" %>

<div class="box-wrapper">
	<div class="box-content">
		<asp:Panel ID="loginPanel" runat="server" Visible="false" DefaultButton="loginButton">
			<div class="row">
				<div class="col-md-12 padding-bottom-10">
					<asp:TextBox ID="userNameTextbox" runat="server" CssClass="form-control" placeholder="Användarnamn" />
				</div>
			</div>
			<div class="row">
				<div class="col-md-12 padding-bottom-10">
					<asp:TextBox ID="passwordTextbox" runat="server" CssClass="form-control" TextMode="Password" placeholder="Lösenord" />
				</div>
			</div>
			<div class="row">
				<div class="col-md-12">
					<asp:Button ID="loginButton" runat="server" CssClass="btn btn-warning float-right width-100-percent" Text="Logga in &raquo;" />
				</div>
			</div>
			<div class="row">
				<div class="col-md-12 padding-top-5 text-align-center">
					<asp:Label ID="errorLabel" runat="server" CssClass="info-text login-error" Text="Fel användaruppgifter." Visible="false" />
				</div>
			</div>
		</asp:Panel>
		<asp:Panel ID="logoutPanel" runat="server" Visible="false" CssClass="text-align-center">
			<div class="row">
				<div class="col-md-12 padding-bottom-10">
					<asp:Label ID="loggedinLabel" runat="server" />
				</div>
			</div>
			<div class="row">
				<div class="col-md-12">
					<asp:Button runat="server" ID="logoutButton" CssClass="btn btn-warning float-right width-100-percent" Text="Logga ut &raquo;"/>
				</div>
			</div>
		</asp:Panel>
	</div>
</div>