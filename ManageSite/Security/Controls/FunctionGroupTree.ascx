<%@ Control Language="C#" AutoEventWireup="true" Inherits="Admin_Controls_FunctionGroupTree"
    CodeFile="FunctionGroupTree.ascx.cs" %>
<div class="dv_treeView">
    <asp:TreeView runat="server" ID="lsManager" Width="100%" ShowLines="true" SelectedNodeStyle-Font-Bold="true"
        SelectedNodeStyle-BorderColor="black" NodeStyle-ForeColor="#313131" SelectedNodeStyle-ForeColor="White">
        <SelectedNodeStyle BorderColor="Black" Font-Bold="True" BackColor="Gray" ForeColor="White">
        </SelectedNodeStyle>
    </asp:TreeView>
</div>
