<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="TeacherTeachCourseView.aspx.cs" Inherits="Security_TeacherQuery_TeacherTeachCourseView" %>
    <%@ Register Src="~/Resource/CourseManage/Controls/CourseInfoView.ascx" TagName="CourseInfoView" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBack" runat="Server">
   <asp:LinkButton ID="lbtnReturn" runat="server" CssClass="btn_Return" Text="返回"></asp:LinkButton>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="Div_CourseInfo" class="dv_pageInformation">
        <uc1:courseinfoview id="CourseInfoView1" runat="server" />
    </div>
</asp:Content>
