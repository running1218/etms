<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true" CodeFile="ElearningMapAdd.aspx.cs" Inherits="Resource_ElearningMap_ElearningMapAdd"  ValidateRequest="false" %>

<%@ Register src="Controls/ElearningMapInfo.ascx" tagname="ElearningMapInfo" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <uc1:ElearningMapInfo ID="ElearningMapInfo1" runat="server"  Action="Add" />

</asp:Content>

