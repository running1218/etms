<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true"
    CodeFile="IDPNotCourseDataInfo.aspx.cs" Inherits="IDP_IDPNotCourseDataAdd" %>

<%@ Register Src="~/IDP/IDPNotCourseDataInfo.ascx" TagName="IDPNotCourseDataInfo"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:IDPNotCourseDataInfo ID="IDPNotCourseDataInfo" runat="server" />
    <div class="dv_submit">        
        <asp:Button ID="btnUpdate" runat="server" Text="保存"  CssClass="btn_Save" OnClick="btnUpdate_Click"
            CommandName="edit" ValidationGroup="Edit" />   
         <asp:ValidationSummary runat="server" ID="ValidationSummary1" ValidationGroup="Edit"
            ShowMessageBox="true" ShowSummary="false" />    
        <asp:Button ID="btnReturn" SkinID="Return" runat="server" Text="取消" OnClientClick="closeWindow()" CssClass="btn_Cancel" />
    </div>
</asp:Content>
