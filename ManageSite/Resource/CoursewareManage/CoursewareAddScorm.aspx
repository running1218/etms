<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true" CodeFile="CoursewareAddScorm.aspx.cs" Inherits="Resource_CoursewareManage_CoursewareAddScorm" %>

<%@ Register src="Controls/CoursewareInfoScorm.ascx" tagname="CoursewareInfoScorm" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:CoursewareInfoScorm ID="CoursewareInfoScorm1" runat="server" Action="Add" />
</asp:Content>
