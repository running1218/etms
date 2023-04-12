<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true"
    CodeFile="TraningProjectCopy.aspx.cs" Inherits="TraningImplement_TraningProjectManager_TraningProjectCopy" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--表单录入-->
    <div class="dv_information">
        <table class="GridviewGray">
            <tr>
                <th colspan="2">
                    原项目信息
                </th>
            </tr>
            <tr>
                <th>
                    项目编码：
                </th>
                <td>
                   <asp:Label ID="labItemCode" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    项目名称：
                </th>
                <td>
                   <asp:Label ID="labItemName" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    项目周期：
                </th>
                <td>
                   <asp:Label ID="labItemCycle" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <table class="GridviewGray">
            <tr>
                <th colspan="4">
                    新项目信息
                </th>               
            </tr>
            <tr>
                <th>
                    项目编码：
                </th>
                <td colspan="3">
                   <asp:Label ID="txt_ItemCode" runat="server"  MaxLength="50">项目编码自动生成：P + 机构编码 + 4位年 + 4位流水号</asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    项目名称：
                </th>
                <td colspan="3">
                    <asp:TextBox ID="Txt_ItemName" runat="server" CssClass="inputbox_210" MaxLength="100"></asp:TextBox><font
                        color="red">*</font><asp:RequiredFieldValidator ID="RequiredFieldValidator1" Text="*"
                            Display="None" runat="server" ErrorMessage="请填写项目名称！" ControlToValidate="Txt_ItemName"
                            ValidationGroup="Saves"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th>
                    项目周期：
                </th>
                <td colspan="3">
                    <cc1:DateTimeTextBox ID="Dtt_ItemBeginTime" runat="server" EndTimeControlID="Dtt_ItemEndTime"></cc1:DateTimeTextBox><font
                        color="red">*</font><asp:RequiredFieldValidator ID="RequiredFieldValidator3" Text="*"
                            Display="None" runat="server" ErrorMessage="请填写项目开始时间！" ControlToValidate="Dtt_ItemBeginTime"
                            ValidationGroup="Saves"></asp:RequiredFieldValidator>
                    至
                    <cc1:DateTimeTextBox ID="Dtt_ItemEndTime" runat="server" BeginTimeControlID="Dtt_ItemBeginTime"></cc1:DateTimeTextBox><font
                        color="red">*</font><asp:RequiredFieldValidator ID="RequiredFieldValidator4" Text="*"
                            Display="None" runat="server" ErrorMessage="请填写项目结束时间！" ControlToValidate="Dtt_ItemEndTime"
                            ValidationGroup="Saves"></asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
        <div>
         说明：<br />
         复制信息包含 项目信息、项目课程、项目课程讲师、项目课程资源。<br />
        </div>
    </div>
    <div class="dv_submit">
        <asp:Button ID="Btn_Save" CssClass="btn_Save" runat="server" Text="保存" OnClick="Btn_Save_Click"
            ValidationGroup="Saves" />
        <asp:ValidationSummary runat="server" ID="ValidationSummary1" ValidationGroup="Saves"
            ShowMessageBox="true" ShowSummary="false" />
        <input type="button" class="btn_Cancel" value="取消" onclick="javascript:closeWindow();" /></div>
</asp:Content>
