<%@ Page Title="学习地图基本信息" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true" CodeFile="ElearningMapEdit.aspx.cs" Inherits="ETMS.WebApp.Manage.Resource.ElearningMap.ElearningMapEdit" ValidateRequest="false" %>
<%@ Register src="Controls/ElearningMapInfo.ascx" tagname="ElearningMapInfo" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <uc1:ElearningMapInfo ID="ElearningMapInfo1" runat="server" Action="Edit" />

</asp:Content>
