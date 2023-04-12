<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true" CodeFile="CoursePeriodView.aspx.cs" Inherits="TraningPlan_CoursePeriod_CoursePeriodView" %>

<%@ Register Src="Controls/CoursePeriodView.ascx" TagName="CoursePeriodView" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--功能标题-->
    <h2 class="dv_title">
        查看课时安排
    </h2>
    <uc1:CoursePeriodView ID="CoursePeriodView1" runat="server" />
</asp:Content>


