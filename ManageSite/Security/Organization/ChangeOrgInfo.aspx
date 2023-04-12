<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChangeOrgInfo.aspx.cs" Inherits="Admin_Site_Organization_ChangeOrgInfo"
    MasterPageFile="~/MasterPages/MPageAdmin.master" %>

<%@ Register Src="UserControl.ascx" TagName="UserControl" TagPrefix="uc1" %>
<asp:Content runat="server" ID="PageContent" ContentPlaceHolderID="ContentPlaceHolder1">  
    <div class="dv_information1" style="width:90%;margin:0 auto">
        <uc1:UserControl ID="UserControl1" runat="server"></uc1:UserControl>
    </div>
    <div class="center">
        <asp:Button ID="btnUpdate" runat="server" Text="±£´æ" OnClick="btn_ClickHandle" SkinID="Edit"
            CommandName="edit" ValidationGroup="Edit" />
        <asp:Button ID="btnReturn" runat="server" SkinID="Return" Text="È¡Ïû" OnClientClick="history.back();return false;" />
    </div>
</asp:Content>
