<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master" AutoEventWireup="true" CodeFile="AfficheListCompany.aspx.cs" Inherits="Information_AfficheManager_AfficheListCompany" %>



<%@ Register src="Controls/AfficheList.ascx" tagname="AfficheList" tagprefix="uc1" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <uc1:AfficheList ID="AfficheList1" runat="server" />

</asp:Content>