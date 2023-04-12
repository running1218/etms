<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true" CodeFile="CoursePeriodEdit.aspx.cs" Inherits="TraningPlan_CoursePeriod_CoursePeriodEdit" %>

<%@ Register Src="Controls/CoursePeriodInfo.ascx" TagName="CoursePeriodInfo" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--功能标题-->
    <h2 class="dv_title">
        修改课时安排
    </h2>
    <uc1:CoursePeriodInfo ID="CoursePeriodInfo1" runat="server" />
</asp:Content>


