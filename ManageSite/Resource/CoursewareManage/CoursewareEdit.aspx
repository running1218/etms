<%@ Page Title="课件基本信息" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true" CodeFile="CoursewareEdit.aspx.cs" Inherits="ETMS.WebApp.Manage.Resource.CoursewareManage.CoursewareEdit" %>
<%@ Register src="Controls/CoursewareInfo.ascx" tagname="CoursewareInfo" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:CoursewareInfo ID="CoursewareInfo1" runat="server" Action="Edit" />
</asp:Content>
