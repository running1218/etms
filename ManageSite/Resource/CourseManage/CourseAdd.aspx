<%@ Page Title="" ValidateRequest="false" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master"
    AutoEventWireup="true" CodeFile="CourseAdd.aspx.cs" Inherits="ETMS.WebApp.Manage.Resource.CourseManage.CourseAdd" %>

<%@ Register Src="Controls/CourseInfo.ascx" TagName="CourseInfo" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:CourseInfo ID="CourseInfo1" runat="server" op="add" />
</asp:Content>
