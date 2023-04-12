<%@ Page Title="" ValidateRequest="false" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true" CodeFile="BatchVideoManage.aspx.cs" Inherits="Resource_CourseManage_BatchVideoManage" %>
<%@ Register Src="~/Controls/UpFile.ascx" TagName="uploader" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="dv_information">
        <table class="GridviewGray">
            <tr>
                <td colspan="2">支持mp4格式的视频文件上传，文件大小不能超过2GB</td>
            </tr>
            <tr>
                <th>选择文件</th>
                <td>
                    <uc:uploader ID="uploader1" runat="server" FunctionType="MediaResourceVideoMore" FileTypeIsDisplay="false" />
                </td>
            </tr>
        </table>
    </div>
    <!--提交表单-->
    <div class="dv_submit">
        <asp:HiddenField ID="hiddSort" runat="server" />
        <asp:LinkButton ID="lbtnSave" runat="server" CssClass="btn_Save" OnClick="lbtnSave_Click"
            ValidationGroup="Error">保存</asp:LinkButton>
        <asp:ValidationSummary runat="server" ID="ValidationSummary1" ValidationGroup="Error"
            ShowMessageBox="true" ShowSummary="false" />
        <a href="javascript:closeWindow();" class="btn_Cancel padleft10">取消</a>
    </div>
    <%--<uc:uploader ID="uploader" runat="server" FunctionType="MediaResourceVideo" FileTypeIsDisplay="false" Width="480" Height="340" />--%>
</asp:Content>

