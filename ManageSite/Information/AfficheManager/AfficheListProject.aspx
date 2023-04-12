<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master" AutoEventWireup="true" CodeFile="AfficheListProject.aspx.cs" Inherits="Information_AfficheManager_AfficheListProject" %>

<%@ Register src="Controls/AfficheList.ascx" tagname="AfficheList" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:AfficheList ID="AfficheList1" runat="server" />
</asp:Content>

