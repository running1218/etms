<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true" CodeFile="CoursewareAdd.aspx.cs" Inherits="ETMS.WebApp.Manage.Resource.CoursewareManage.CoursewareAdd" %>
<%@ Register src="Controls/CoursewareInfo.ascx" tagname="CoursewareInfo" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:CoursewareInfo ID="CoursewareInfo1" runat="server" Action="Add" />   
</asp:Content>
