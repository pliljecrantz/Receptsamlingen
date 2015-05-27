<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TopList.ascx.cs" Inherits="Receptsamlingen.Web.Units.TopList" %>

<div class="box-wrapper">
	<div class="box-content">
		<h3>= = = Topp 5 = = =</h3>
		<asp:PlaceHolder ID="noTopListPlaceHolder" runat="server">
			<p>Det finns inga röster än...</p>
		</asp:PlaceHolder>
		<asp:PlaceHolder ID="topListPlaceHolder" runat="server">
			<div class="toplist-content">
				<asp:Repeater ID="topListRepeater" runat="server">
					<ItemTemplate>
						&rsaquo;&rsaquo;&nbsp;<asp:HyperLink ID="itemHyperLink" runat="server" />
					</ItemTemplate>
					<SeparatorTemplate>
						<br />
					</SeparatorTemplate>
				</asp:Repeater>
			</div>			
		</asp:PlaceHolder>
	</div>
</div>