<%@ Page Language="C#" AutoEventWireup="true" CodeFile="View.aspx.cs" Inherits="Admin_Site_Organization_View"
    MasterPageFile="~/MasterPages/MPagePop.Master" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="UserControl.ascx" TagName="UserControl" TagPrefix="uc1" %>
<asp:Content runat="server" ID="PageContent" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="dv_information">
        <uc1:UserControl ID="UserControl1" runat="server"></uc1:UserControl>
    </div>
    <div class="dv_submit">
        <asp:Button ID="btnAdd" runat="server" Text="����" OnClick="btn_ClickHandle" CommandName="add"
            ValidationGroup="Edit" SkinID="Insert" /><asp:Button ID="btnUpdate" runat="server"
                Text="����" OnClick="btn_ClickHandle" SkinID="Edit" CommandName="edit" ValidationGroup="Edit" /><cc1:CustomButton
                    ID="btnDelete" runat="server" Text="ɾ��" OnClick="btn_ClickHandle" CommandName="delete"
                    EnableConfirm="true" ConfirmMessage="ȷ��Ҫɾ���˻�����" ConfirmTitle="ɾ����ʾ" CssClass="btn_Del" /><asp:Button
                        ID="btnReturn" runat="server" SkinID="Return" OnClientClick="closeWindow()" />
    </div>
</asp:Content>
