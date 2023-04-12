<%@ Page Title="课程基本信息" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="CourseView.aspx.cs" Inherits="ETMS.WebApp.Manage.Living.CourseView" %>

<%@ Register Src="~/Resource/CourseManage/Controls/CourseInfoView.ascx" TagName="CourseInfoView" TagPrefix="uc1" %>
<asp:Content ID="cphNav" runat="server" ContentPlaceHolderID="cphBack">
    <asp:LinkButton ID="lbnBack" runat="server" PostBackUrl="~/Living/CourseList.aspx"
        CssClass="btn_Return">返回</asp:LinkButton>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="info" align="center">
        <div id="Div_Select_0" style="display: none">
            <div id="Div_CourseInfo" class="dv_pageInformation">
                <uc1:CourseInfoView ID="CourseInfoView2" runat="server" />
            </div>
        </div>
    </div>
</asp:Content>
