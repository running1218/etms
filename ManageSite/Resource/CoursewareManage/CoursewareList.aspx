<%@ Page Title="在线课件管理" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="CoursewareList.aspx.cs" Inherits="ETMS.WebApp.Manage.Resource.CoursewareManage.CoursewareList" %>

<%@ Register src="~/Resource/CoursewareManage/Controls/CoursewareList.ascx" tagname="CoursewareList" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:CoursewareList ID="CoursewareList1" runat="server" />
</asp:Content>
