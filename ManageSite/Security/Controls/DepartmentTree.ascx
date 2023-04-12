<%@ Control Language="C#" AutoEventWireup="true" Inherits="ETMS.WebApp.Manage.Security.Controls.DepartmentTree"
    CodeFile="DepartmentTree.ascx.cs" %>
<div class="dv_treeView">
    <asp:TreeView runat="server" ID="lsManager" Width="100%" ShowLines="true" SelectedNodeStyle-Font-Bold="true"
        SelectedNodeStyle-BorderColor="black" SelectedNodeStyle-ForeColor="White" NodeStyle-ForeColor="#313131">
        <SelectedNodeStyle BorderColor="Black" Font-Bold="True" BackColor="Gray" ForeColor="White">
        </SelectedNodeStyle>
    </asp:TreeView>
</div>
