<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="~/MasterPages/Site.Master" CodeBehind="Apply.aspx.cs" Inherits="Receptsamlingen.Web.Pages.Apply" %>
<%@ Register TagPrefix="Artep" TagName="Apply" Src="~/Units/ApplyForm.ascx" %>

<asp:Content ContentPlaceHolderID="mainContent" runat="server">
	<Artep:Apply id="applyControl" runat="server" />
</asp:Content>