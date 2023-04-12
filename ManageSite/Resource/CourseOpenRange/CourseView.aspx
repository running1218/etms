<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master" AutoEventWireup="true" CodeFile="CourseView.aspx.cs" Inherits="Resource_CourseOpenRange_CourseView" %>

<%@ Register Src="../CourseManage/Controls/CourseInfoView.ascx" TagName="CourseInfoView" TagPrefix="uc1" %>

<asp:Content ID="cphNav" runat="server" ContentPlaceHolderID="cphBack">
    <asp:LinkButton ID="lbnBack" runat="server" PostBackUrl="CourseList.aspx" CssClass="btn_Return">返回</asp:LinkButton>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="info">
        <div id="Div_CourseInfo" class="dv_pageInformation">
            <div class="">
                <uc1:CourseInfoView ID="CourseInfoView1" runat="server" />
            </div>
        </div>      
    </div>   
</asp:Content>
