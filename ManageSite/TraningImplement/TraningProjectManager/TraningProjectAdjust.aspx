<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true"
    CodeFile="TraningProjectAdjust.aspx.cs" Inherits="TraningImplement_TraningProjectManager_TraningProjectAdjust" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--表单录入-->
    <div class="dv_information">
        <table class="GridviewGray">
            <tr>
                <th>
                    项目编码：
                </th>
                <td colspan="3">
                    <asp:Label ID="lbl_ItemCode" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    项目名称：
                </th>
                <td colspan="3">
                    <asp:Label ID="lbl_ItemName" runat="server"></asp:Label>
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
            <tr>
                <th>
                    可选选修课数：
                </th>
                <td colspan="3">
                    <cc1:CustomTextBox ID="txtSelfChooseCourseNum" runat="server" ContentType="Number" Text="-1" CssClass="inputbox_60"></cc1:CustomTextBox>
                    <font color="red">*</font>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" Text="*" Display="None" runat="server" ErrorMessage="可选选修课数！" ControlToValidate="txtSelfChooseCourseNum" ValidationGroup="Saves"></asp:RequiredFieldValidator>
                    <span style="margin-left:20px; color:#0094ff;">-1:表示不限制，0:不可选，正数表示可选门数</span>
                </td>
            </tr>
            <tr>
                <th>
                    报名方式：
                </th>
                <td colspan="3">
                    <cc1:DictionaryLabel ID="lblSignupMode" DictionaryType="Dic_Sys_SignupMode"
                                TextLength="6" runat="server" />
                </td>
            </tr>
            <tr id="trSignup" runat="server" visible="false">
                <th>
                    报名时段：
                </th>
                <td colspan="3">
                    <cc1:DateTimeTextBox ID="dttSignupBeginTime" runat="server" EndTimeControlID="dttSignupEndTime"></cc1:DateTimeTextBox><font
                        color="red">*</font><asp:RequiredFieldValidator ID="RequiredFieldValidator1" Text="*"
                            Display="None" runat="server" ErrorMessage="请填写报名时段开始时间！" ControlToValidate="dttSignupBeginTime"
                            ValidationGroup="Saves"></asp:RequiredFieldValidator>
                    至
                    <cc1:DateTimeTextBox ID="dttSignupEndTime" runat="server" BeginTimeControlID="dttSignupBeginTime"></cc1:DateTimeTextBox><font
                        color="red">*</font><asp:RequiredFieldValidator ID="RequiredFieldValidator2" Text="*"
                            Display="None" runat="server" ErrorMessage="请填写报名时段结束时间！" ControlToValidate="dttSignupEndTime"
                            ValidationGroup="Saves"></asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
    </div>
    <div class="dv_submit">
        <asp:Button ID="Btn_Save" CssClass="btn_Save" runat="server" Text="保存" OnClick="Btn_Save_Click"
            ValidationGroup="Saves" />
        <asp:ValidationSummary runat="server" ID="ValidationSummary1" ValidationGroup="Saves"
            ShowMessageBox="true" ShowSummary="false" />
        <input type="button" class="btn_Cancel" value="取消" onclick="javascript:closeWindow();" /></div>
</asp:Content>
