<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DocView.aspx.cs" MasterPageFile="~/MasterPages/Default.Master"
    Inherits="Example_DocView" %>

<%@ Register Src="../Controls/DocViewer.ascx" TagName="DocViewer" TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:DocViewer ID="DocViewer1" runat="server" />
</asp:Content>
