<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true"
    CodeFile="StudentImport.aspx.cs" Inherits="Security_StudentManager_StudentImport" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/MiniUpFile.ascx" TagName="Uploader" TagPrefix="uc" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="dv_information">
        <table class="GridviewGray">
            <tr>
                <th width="25%">
                    导入模板：
                </th>
                <td width="75%">
                    <a href="学员导入模板.xls">点击下载导入模板</a>
                </td>
            </tr>
            <tr>
                <th>
                    默认密码：
                </th>
                <td>
                    <asp:TextBox ID="txtDefaultPassword" runat="server"></asp:TextBox><span style="color: Red;">*</span><asp:RequiredFieldValidator
                        ID="RequiredFieldValidatorLoginName" Text="&nbsp;" Display="None" runat="server"
                        ErrorMessage="请填写默认密码！" ControlToValidate="txtDefaultPassword" ValidationGroup="Edit"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th>
                    选择导入文件：
                </th>
                <td>
                    <uc:Uploader ID="uploader" runat="server" FunctionType="ImportStudentInfo" />
                </td>
            </tr>
            <tr>
                <th>
                    说&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;明：
                </th>
                <td>
                    <asp:Label ID="lblRemark" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div class="dv_submit">
        <asp:Button ID="btnImport" runat="server" Text="导入" CssClass="btn_Import" OnClick="btnImport_Click"
            ValidationGroup="Edit" />
        <input type="button" class="btn_Cancel" value="取消" onclick="javascript:closeWindow();" /></div>
    <asp:ValidationSummary runat="server" ID="validationSummary1" ValidationGroup="Edit"
        ShowMessageBox="true" ShowSummary="false" />
</asp:Content>
