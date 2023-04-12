<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true" CodeFile="FeeDetailsAdd.aspx.cs" Inherits="FeeDetailsAdd" %>
<%@ Register Src="~/Fee/FeeDetails/Controls/FeeDetails.ascx" TagName="FeeDetails" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc:FeeDetails ID="FeeDetails1" runat="server" />
</asp:Content>

