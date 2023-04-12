<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master" AutoEventWireup="true" CodeFile="CoursePointRuleList.aspx.cs" Inherits="Point_CoursePointManager_CoursePointRuleList" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="Controls/CourseRoleListView.ascx" TagName="CourseRoleListView"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBack" runat="Server">
    <asp:LinkButton ID="lbtn_Return" runat="server" CssClass="btn_Return" Text="返回"></asp:LinkButton>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<uc1:CourseRoleListView ID="CourseRoleListView1" runat="server" />
</asp:Content>

