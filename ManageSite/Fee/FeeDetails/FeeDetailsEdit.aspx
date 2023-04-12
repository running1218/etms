<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true" CodeFile="FeeDetailsEdit.aspx.cs" Inherits="FeeDetailsEdit" %>
<%@ Register Src="~/Fee/FeeDetails/Controls/FeeDetails.ascx" TagName="FeeDetails" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc:FeeDetails ID="FeeDetails" runat="server" />
</asp:Content>

