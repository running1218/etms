<%@ Page Title="课程基本信息" ValidateRequest="false" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true" CodeFile="CourseEdit.aspx.cs" Inherits="ETMS.WebApp.Manage.Living.CourseEdit" %>
<%@ Register src="Controls/CourseInfo.ascx" tagname="CourseInfo" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">       
    <uc1:CourseInfo ID="courseInfo" runat="server" Action="Edit" />
</asp:Content>
