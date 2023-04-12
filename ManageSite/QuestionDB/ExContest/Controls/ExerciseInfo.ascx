<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ExerciseInfo.ascx.cs" Inherits="QuestionDB_ExContest_Controls_ExerciseInfo" %>
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
                竞赛名称：
            </th>
            <td>
                <asp:TextBox ID="txtContestName" runat="server" CssClass="inputbox_210" MaxLength="50"></asp:TextBox>
<asp:RequiredFieldValidator ID="RequiredFieldValidatorName" runat="server" ControlToValidate="txtContestName" Display="None" ErrorMessage="请填写竞赛名称！" ValidationGroup="Error"></asp:RequiredFieldValidator>

            </td>
        </tr>
        <tr>
            <th>
                状&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;态：
            </th>
            <td>
                <cc1:DictionaryRadioButtonList runat="server" ID="rbtnContestStatus" DictionaryType='Dic_Status' />
            </td>
        </tr>
        <tr>
            <th style="vertical-align: top">
                描&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;述：
            </th>
            <td>
                <asp:TextBox ID="txtContestDesc" runat="server" CssClass="inputbox_area300" TextMode="MultiLine"></asp:TextBox>
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
