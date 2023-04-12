<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true"
    CodeFile="EvaluationInfo.aspx.cs" Inherits="TraningImplement_ProjectCourseResource_ExOfflineHomework_EvaluationInfo" %>

<%@ Register Src="Controls/EvaluationInfo.ascx" TagName="Evaluation" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:Evaluation ID="Evaluation" runat="server" />
    <!--提交表单-->
    <div class="dv_submit">
        <asp:Button ID="lbnSave" runat="server" CssClass="btn_Save" OnClick="btn_ClickHandle"
            ValidationGroup="Error" Text="保存"/>
        <a href="javascript:closeWindow();" class="btn_Cancel padleft10">取消</a>
    </div>
</asp:Content>
