<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FeeSetting.ascx.cs" Inherits="Fee_CourseFeeSetting_Controls_FeeSetting" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="uc" %>
<div class="dv_information">
    <table class="GridviewGray">
        
        <tr>
            <th style="width: 20%">
                讲师等级：
            </th>
            <td style="width: 80%">
                <uc:DictionaryDropDownList runat="server" ID="ddlTeacherLevelID" DictionaryType="Dic_Sys_TeacherLevel"
                    IsShowChoose="false" IsShowAll="false">
                </uc:DictionaryDropDownList>
            </td>
        </tr>
        <tr>
            <th>
                培训时间说明：
            </th>
            <td>
                <uc:DictionaryDropDownList runat="server" ID="ddlTrainingTimeDesc" DictionaryType="Dic_Sys_TrainingTimeDesc"
                     IsShowChoose="false" IsShowAll="false">
                </uc:DictionaryDropDownList>
            </td>
        </tr>
        <tr>
            <th>
                课酬标准：
            </th>
            <td>
                <asp:TextBox ID="txtCourseFee" CssClass="" runat="server"  MaxLength="20"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="请填2位小数的正数"
                                    ValidationGroup="Error" Display="Dynamic" ControlToValidate="txtCourseFee"
                                    ValidationExpression="\d{1,6}(\.\d{2})?"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <th>
                备&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;注：
            </th>
            <td>
                <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" CssClass="inputbox_area300"></asp:TextBox>
            </td>
        </tr>
    </table>
</div>
<div class="dv_submit">
<asp:LinkButton ID="lbtnSave" runat="server" CssClass="btn_Save" OnClick="lbtnSave_Click"
        ValidationGroup="Error">保存</asp:LinkButton>
    <asp:ValidationSummary runat="server" ID="ValidationSummary1" ValidationGroup="Error"
        ShowMessageBox="true" ShowSummary="false" />
    <a href="javascript:closeWindow();" class="btn_Cancel">取消</a>
</div>