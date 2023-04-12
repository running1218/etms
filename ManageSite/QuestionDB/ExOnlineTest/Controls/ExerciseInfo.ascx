<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ExerciseInfo.ascx.cs"
    Inherits="QuestionDB_ExOnlineTest_Controls_ExerciseInfo" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/ChooseCourseDropdown.ascx" TagName="ChooseCourseDropdown"
    TagPrefix="uc1" %>
<!--表单录入-->
<div class="dv_information">
    <table class="GridviewGray">
        <tr>
            <th >
                课程名称：
            </th>
            <td>
                <uc1:ChooseCourseDropdown ID="ddlCourseID" runat="server" />
            </td>
        </tr>
        <tr>
            <th>
                测试名称：
            </th>
            <td>
                <asp:TextBox ID="txtOnLineTestName" runat="server" CssClass="inputbox_210" MaxLength="50"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorName" runat="server" ControlToValidate="txtOnLineTestName" Display="None" ErrorMessage="请填写测试名称！" ValidationGroup="Error"></asp:RequiredFieldValidator>

            </td>
        </tr>
        <tr>
            <th>
                测试时长：
            </th>
            <td>
                <asp:TextBox ID="txtLimitTime" runat="server" CssClass="inputbox_60" MaxLength="5"></asp:TextBox>分钟

                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtLimitTime" Display="None" ErrorMessage="测试时长格式错误！" ValidationExpression="\d{0,6}(\.\d{1,2})?" ValidationGroup="Error"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtLimitTime" Display="None" ErrorMessage="测试时长格式错误！" ValidationGroup="Error"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th>
                总 分 数：
            </th>
            <td>
               <asp:TextBox ID="txtTotalScore" runat="server" CssClass="inputbox_60" MaxLength="5"></asp:TextBox>
               <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtTotalScore" Display="None" ErrorMessage="总分数格式错误！" ValidationExpression="\d{0,6}(\.\d{1,2})?" ValidationGroup="Error"></asp:RegularExpressionValidator>
               <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtTotalScore" Display="None" ErrorMessage="总分数格式错误！" ValidationGroup="Error"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th>
                及 格 线：
            </th>
            <td>
               <asp:TextBox ID="txtPassLine" runat="server" CssClass="inputbox_60" MaxLength="5"></asp:TextBox>
               <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPassLine" Display="None" ErrorMessage="及格线格式错误！" ValidationExpression="\d{0,6}(\.\d{1,2})?" ValidationGroup="Error"></asp:RegularExpressionValidator>
               <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPassLine" Display="None" ErrorMessage="及格线格式错误！" ValidationGroup="Error"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th>
                显示答案：
            </th>
            <td>
                <cc1:DictionaryRadioButtonList runat="server" ID="rbtnIsShowAnswer" DictionaryType='Dic_TrueOrFalse' />
            </td>
        </tr>
        <tr>
            <th>
                状&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;态：
            </th>
            <td>
                <cc1:DictionaryRadioButtonList runat="server" ID="rbtnOnLineTestStatus" DictionaryType='Dic_Status' />
            </td>
        </tr>
        <tr>
            <th style="vertical-align: top;">
                描&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;述：
            </th>
            <td>
                <asp:TextBox ID="txtOnLineTestDesc" runat="server" CssClass="inputbox_area300" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
    </table>
</div>
<!--提交表单-->
<div class="dv_submit">
    <asp:LinkButton ID="lbtnSave" runat="server" CssClass="btn_Save" OnClick="lbtnSave_Click"
        ValidationGroup="Error">保存</asp:LinkButton>
    <asp:ValidationSummary runat="server" ID="ValidationSummary1" ValidationGroup="Error"
        ShowMessageBox="true" ShowSummary="false" />
    <a href="javascript:closeWindow();" class="btn_Cancel">取消</a>
</div>
