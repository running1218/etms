<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="AfficheAdd.aspx.cs" Inherits="Information_AfficheManager_AfficheAdd" %>

<%@ Register Src="~/Information/AfficheManager/Controls/AfficheInfo.ascx"
    TagPrefix="uc1" TagName="AfficheInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <uc1:AfficheInfo ID="AfficheInfo1" runat="server"></uc1:AfficheInfo>
</asp:Content>
