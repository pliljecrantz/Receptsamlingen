<%@ Page Language="C#" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true" CodeBehind="Recipe.aspx.cs" Inherits="Receptsamlingen.Web.Pages.Recipe" %>
<%@ Register TagPrefix="Artep" TagName="ConfirmDialog" Src="~/Units/ConfirmDialog.ascx" %>

<asp:Content ContentPlaceHolderID="headContent" runat="server">
	<link href="../Styles/jquery-ui.min.css" rel="stylesheet" />
	<link href="../Styles/jquery-ui.theme.min.css" rel="stylesheet" />
	<script src="../Scripts/jquery-1.6.2.min.js" type="text/javascript"></script>
	<script src="../Scripts/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
	<script src="../Scripts/jquery.raty.min.js" type="text/javascript"></script>
	<script src="../Scripts/rating-handler.js" type="text/javascript"></script>
	<script type="text/javascript">
		$(function () {
			$("#dialog-confirm").dialog({
				autoOpen: false,
				resizable: false,
				height: 180,
				width: 340,
				modal: true,
				buttons: {
					"Ja, radera!": function () {
						eval("<%=DeleteLinkButtonEvent%>");
						$(this).dialog("close");
					},
					"Avbryt": function () {
						$(this).dialog("close");
					}
				}
			});
			$('#deleteLinkButton').click(function (e) {
				e.preventDefault();
				$('#dialog-confirm').dialog('open');
			});
		});
	</script>
</asp:Content>
<asp:Content ContentPlaceHolderID="mainContent" runat="server">
	<div class="row">
		<div class="col-md-8">
			<h1>
				<asp:Label ID="headerLabel" runat="server" />
			</h1>
		</div>
		<div class="col-md-4 margin-top-25">
			<asp:Panel ID="ratingPanel" runat="server" Visible="false" CssClass="float-right">
				<input type="hidden" id="ratingHidden" runat="server" /><input type="hidden" id="avarageHidden" runat="server" /><input type="hidden" id="loggedInHidden" runat="server" />
				<div id="rating" class="float-left">
					<!-- -->
				</div>
				<asp:Button ID="rateButton" runat="server" CssClass="btn btn-warning" Text="Rösta!" Enabled="false" />
				<br />
				<br />
				<asp:Label ID="infoRatingLabel" CssClass="float-right label label-warning info-text" runat="server" Visible="false" />
			</asp:Panel>
		</div>
	</div>

	<div class="row">
		<div class="col-md-12 margin-bottom-10">
			<asp:Label ID="errorLabel" runat="server" Visible="false" CssClass="label label-warning info-text" />
		</div>
	</div>
	<asp:MultiView ID="recipeMultiView" runat="server">
		<!-- Not logged in-view -->
		<asp:View ID="anonymousView" runat="server">
			<asp:Label runat="server" Text="Du kan bara lägga in recept om du har loggat in." CssClass="label label-warning info-text" />
		</asp:View>
		<!-- Read-view -->
		<asp:View ID="readView" runat="server">
			<div class="row">
				<div class="col-xs-12">
						<asp:Label runat="server" Text="Kategori:" CssClass="orange" />
					<asp:Label ID="categoryLabel" runat="server" /><br />
					<br />
						<asp:Label runat="server" Text="Huvudråvara:" CssClass="orange" />
					<asp:Label ID="dishTypeLabel" runat="server" /><br />
					<br />
						<asp:Label runat="server" Text="Portioner:" CssClass="orange" />
					<asp:Label ID="portionsLabel" runat="server" /><br />
					<br />
						<asp:Label ID="specialIntroLabel" runat="server" Text="Receptet är:" CssClass="orange" />
					<asp:Label ID="specialLabel" runat="server" />
				</div>
			</div>
			<div class="row">
				<div class="col-md-12">
					<h3>Ingredienser:</h3>
					<asp:TextBox ID="readIngredientsTextBox" runat="server" Rows="10" Wrap="True" TextMode="MultiLine" CssClass="form-control" />
					<h3>Gör så här:</h3>
					<asp:TextBox ID="readDescriptionTextBox" runat="server" Rows="10" Wrap="True" TextMode="MultiLine" CssClass="form-control" />
					<br />
					<asp:PlaceHolder ID="adminButtonsPlaceHolder" runat="server" Visible="false">
						<asp:Button ID="editLinkButton" runat="server" Text="Redigera" CssClass="btn btn-warning float-right" />
						<asp:Button ID="deleteLinkButton" runat="server" Text="Radera" CssClass="btn btn-default float-right margin-right-15" />
					</asp:PlaceHolder>
					<Artep:ConfirmDialog ID="confirmDialog" runat="server" />
				</div>
			</div>
		</asp:View>
		<!-- Add/edit-view -->
		<asp:View ID="addEditView" runat="server">
			<div class="row">
				<div class="col-md-12">
					<asp:Label runat="server" Text="Receptnamn:" />
					<br />
					<asp:TextBox ID="nameTextbox" runat="server" CssClass="form-control" />
				</div>
			</div>
			<br />
			<div class="row">
				<div class="col-md-3 margin-bottom-10">
					<asp:Label runat="server" Text="Kategori:" />
					<br />
					<asp:DropDownList ID="categoryDdl" runat="server" CssClass="dropdownlist" AutoPostBack="true" />
				</div>
				<div class="col-md-3 margin-bottom-10">
					<asp:Label runat="server" Text="Huvudråvara:" />
					<br />
					<asp:DropDownList ID="dishTypeDdl" runat="server" CssClass="dropdownlist" />
				</div>

				<div class="col-md-3 margin-bottom-10">
					<asp:Label runat="server" Text="Antal portioner:" />
					<br />
					<asp:DropDownList ID="portionsDdl" runat="server" CssClass="dropdownlist">
						<asp:ListItem Text="1" Value="1" />
						<asp:ListItem Text="2" Value="2" />
						<asp:ListItem Text="3" Value="3" />
						<asp:ListItem Text="4" Value="4" />
						<asp:ListItem Text="5" Value="5" />
						<asp:ListItem Text="6" Value="6" />
						<asp:ListItem Text="7" Value="7" />
						<asp:ListItem Text="8" Value="8" />
					</asp:DropDownList>
				</div>
				<div class="col-md-3 margin-bottom-10">
					<asp:CheckBoxList ID="specialCheckBoxList" runat="server" RepeatDirection="Vertical" />
				</div>
			</div>
			<div class="row">
				<div class="col-md-12">
					<asp:Label runat="server" Text="Ingredienser:" />
					<asp:TextBox ID="addEditIngredientsTextBox" runat="server" Rows="10" Wrap="True" TextMode="MultiLine" CssClass="form-control" />
					<br />
					<asp:Label runat="server" Text="Beskrivning:" />
					<asp:TextBox ID="addEditDescriptionTextBox" runat="server" Rows="10" Wrap="True" TextMode="MultiLine" CssClass="form-control" />
					<br />
					<asp:Button ID="cancelButton" runat="server" Text="Avbryt" CssClass="btn btn-default float-left" />
					<asp:Button ID="addButton" runat="server" Text="Spara!" CssClass="btn btn-warning float-right" />
					<asp:Button ID="clearButton" runat="server" Text="Rensa" CssClass="btn btn-default float-right margin-right-15" />
				</div>
			</div>
		</asp:View>
		<!-- Saved/updated-view -->
		<asp:View ID="savedUpdatedView" runat="server">
			<asp:Label ID="savedUpdatedInfoLabel" runat="server" CssClass="label label-info info-text" />
		</asp:View>
	</asp:MultiView>
</asp:Content>