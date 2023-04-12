<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true" CodeFile="FeeSetttingAdd.aspx.cs" Inherits="Fee_CourseFeeSetting_FeeSetttingAdd" %>
<%@ Register Src="~/Fee/CourseFeeSetting/Controls/FeeSetting.ascx" TagName="FeeSetting" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%--<h2 class="dv_title">
        新增课酬标准
    </h2>--%>
    <uc:FeeSetting ID="fsForm" runat="server" />
</asp:Content>

