<%@ Page Title="在线课件管理" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="ResourceCoursewareList.aspx.cs" Inherits="ETMS.WebApp.Manage.ResourceCoursewareList" %>

<%@ Register Src="~/Resource/CoursewareManage/Controls/CuResourceCoursewareList.ascx" TagName="ResourcewareList" TagPrefix="cu" %>
<asp:Content ID="cpback" runat="server" ContentPlaceHolderID="cphBack">
    <a href="../CourseResource.aspx" class="btn_Return">返回</a>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">       
    <cu:ResourcewareList ID="resourceWareList" runat="server" />   
</asp:Content>
