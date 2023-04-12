<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true" CodeFile="CoursePeriodEdit.aspx.cs" Inherits="TraningImplement_ProjectCoursePeriod_CoursePeriodEdit" %>

<%@ Register Src="Controls/CoursePeriodInfo.ascx" TagName="CoursePeriodInfo" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:CoursePeriodInfo ID="CoursePeriodInfo1" runat="server" Action="Edit" />
</asp:Content>

