<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master" AutoEventWireup="true" CodeFile="CoursePointSearch.aspx.cs" Inherits="Point_CoursePointManager_CoursePointSearch" %>
<%@ Register Src="~/Point/CoursePointManager/Controls/CoursePointSearchPublish.ascx" TagName="PointPublish" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBack" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <uc1:PointPublish ID="PointPublish" runat="server" />     
</asp:Content>

