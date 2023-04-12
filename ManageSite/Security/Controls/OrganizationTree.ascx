<%@ Control Language="C#" AutoEventWireup="true" Inherits="ETMS.WebApp.Manage.Security.Controls.OrganizationTree"
    CodeFile="OrganizationTree.ascx.cs" %>
<div class="dv_treeView">
    <asp:TreeView runat="server" ID="lsManager" Width="100%" ShowLines="true" SelectedNodeStyle-Font-Bold="true" ParentNodeStyle-Height="20px"
        SelectedNodeStyle-BorderColor="black"  NodeStyle-Height="20px"  HoverNodeStyle-Height="20px" NodeStyle-ForeColor="#313131" SelectedNodeStyle-ForeColor="White">
        <SelectedNodeStyle BorderColor="Black" Font-Bold="True" BackColor="Gray" ForeColor="White">
        </SelectedNodeStyle>
    </asp:TreeView>
</div>
