<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true" CodeFile="OnLineHomeEdit.aspx.cs" Inherits="TraningImplement_ProjectCourseResource_OnLineHomeEdit" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--表单录入-->
    <div class="dv_information">
        <table class="GridviewGray">
            <tr>
                <th width="20%">
                    作业名称：
                </th>
                <td width="80%">
                    <asp:Label ID="lblOnLineJobName" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    状&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;态：
                </th>
                <td>
                    <cc1:dictionaryradiobuttonlist runat="server" id="dllIsUse" dictionarytype="Dic_Status" />
                </td>
            </tr>
            <tr>
                <th>
                    开始时间：
                </th>
                <td>
                    <cc1:DateTimeTextBox ID="ResBeginTime" runat="server" EndTimeControlID="ResEndTime"></cc1:DateTimeTextBox><asp:RequiredFieldValidator
                    ID="RequiredFieldValidator3" Text="*" Display="None" runat="server" ErrorMessage="请填写开始时间！"
                    ControlToValidate="ResBeginTime" ValidationGroup="Saves"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th>
                    结束时间：
                </th>
                <td>
                    <cc1:DateTimeTextBox ID="ResEndTime" runat="server" BeginTimeControlID="ResBeginTime"></cc1:DateTimeTextBox><asp:RequiredFieldValidator
                    ID="RequiredFieldValidator1" Text="*" Display="None" runat="server" ErrorMessage="请填写结束时间！"
                    ControlToValidate="ResEndTime" ValidationGroup="Saves"></asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
    </div>
    <div class="dv_submit">
        <asp:Button ID="Btn_Save" CssClass="btn_Save" runat="server" Text="保存" OnClick="Btn_Save_Click" ValidationGroup="Saves" />
    <asp:ValidationSummary runat="server" ID="ValidationSummary1" ValidationGroup="Saves"
        ShowMessageBox="true" ShowSummary="false" />
        <input type="button" class="btn_Cancel" value="取消" onclick="javascript:closeWindow();" /></div>
</asp:Content>

