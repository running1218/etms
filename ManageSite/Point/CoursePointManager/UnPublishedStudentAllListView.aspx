<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master" AutoEventWireup="true" CodeFile="UnPublishedStudentAllListView.aspx.cs" Inherits="Point_CoursePointManager_UnPublishedStudentAllListView" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<%@ Register src="Controls/UnPublishedStudentListView.ascx" tagname="UnPublishedStudentListView" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBack" runat="Server">
<asp:LinkButton ID="lbtn_Return" runat="server" CssClass="btn_Return" Text="返回"></asp:LinkButton>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:UnPublishedStudentListView ID="UnPublishedStudentListView1" runat="server" />
</asp:Content>

