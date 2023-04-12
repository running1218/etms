<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Admin_Dic_Post_Default"
    MasterPageFile="~/MasterPages/MPageAdmin.master" %>

<%@ Register Src="UserControl.ascx" TagName="UserControl" TagPrefix="uc1" %>
<asp:Content runat="server" ID="PageContent" ContentPlaceHolderID="ContentPlaceHolder1">
    <uc1:UserControl ID="UserControl1" runat="server" />
</asp:Content>
