<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="CourseEdit.aspx.cs" Inherits="TraningImplement_ProjectCourseInfo_CourseEdit" %>

<%@ Register Src="Controls/CourseInfo.ascx" TagName="CourseInfo" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--功能标题-->
    <h2 class="dv_title">
        编辑课程辅助信息
    </h2>
    <uc1:CourseInfo ID="CourseInfo1" runat="server" />
</asp:Content>
