<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master"
    AutoEventWireup="true" CodeFile="CoursePeriodAdd.aspx.cs" Inherits="TraningImplement_ProjectCoursePeriod_CoursePeriodAdd" %>

<%@ Register Src="Controls/CoursePeriodInfo.ascx" TagName="CoursePeriodInfo" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--功能标题-->
    <h2 class="dv_title">
        添加课时安排
    </h2>
    <uc1:CoursePeriodInfo ID="CoursePeriodInfo1" runat="server" Action="Add"/>
</asp:Content>
