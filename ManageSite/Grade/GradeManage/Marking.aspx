<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/MasterPages/MPagePop.Master" CodeFile="Marking.aspx.cs" Inherits="Grade_GradeManage_Marking" %>
<%@ Register Src="Controls/MarkingInfo.ascx" TagName="Marking" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:Marking ID="Marking" runat="server" />
    <!--提交表单-->
    <div class="dv_submit">
        <asp:Button ID="lbnSave" runat="server" CssClass="btn_Save" OnClick="btn_ClickHandle"
            ValidationGroup="Error" Text="保存"/>
        <a href="javascript:closeWindow();" class="btn_Cancel padleft10">取消</a>
    </div>
</asp:Content>

