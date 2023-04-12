<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true" CodeFile="FeeSettingEdit.aspx.cs" Inherits="Fee_CourseFeeSetting_FeeSettingEdit" %>
<%@ Register Src="~/Fee/CourseFeeSetting/Controls/FeeSetting.ascx" TagName="FeeSetting" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h2 class="dv_title">
        编辑课酬标准
    </h2>
    <uc:FeeSetting ID="FeeSetting1" runat="server" />
</asp:Content>

