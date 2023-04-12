<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="AfficheEdit.aspx.cs" Inherits="Information_AfficheManager_AfficheEdit" %>

<%@ Register Src="~/Information/AfficheManager/Controls/AfficheInfo.ascx"
    TagPrefix="uc1" TagName="AfficheInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <uc1:AfficheInfo ID="AfficheInfo1" runat="server"></uc1:AfficheInfo>
</asp:Content>
