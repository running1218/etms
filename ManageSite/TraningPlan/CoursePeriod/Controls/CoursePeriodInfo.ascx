<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CoursePeriodInfo.ascx.cs"
    Inherits="TraningPlan_CoursePeriod_Controls_CoursePeriodInfo" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<!--表单录入-->
<div class="dv_information">
    <table class="GridviewGray">
        <tr>
            <th>
                计划编码：
            </th>
            <td colspan="3">
                JH1202547
            </td>
        </tr>
        <tr>
            <th>
                计划名称：
            </th>
            <td colspan="3">
                飞鹰计划001
            </td>
        </tr>
        <tr>
            <th>
                课程名称：
            </th>
            <td colspan="3">
                领导力培训1
            </td>
        </tr>
        <tr>
            <th>
                组织机构：
            </th>
            <td colspan="3">
            </td>
        </tr>
        <tr>
            <th>
                课程类型：
            </th>
            <td>
                专业技能
            </td>
            <th>
                培训方式：
            </th>
            <td>
                内部授课
            </td>
        </tr>
        <tr>
            <th>
                授课方式：
            </th>
            <td colspan="3">
                面授
            </td>
        </tr>
        <tr>
            <th>
                培训日期：
            </th>
            <td>
                <cc1:DateTimeTextBox ID="DateTimeTextBox3" runat="server"></cc1:DateTimeTextBox>
            </td>
            <th>
                讲&nbsp;&nbsp;&nbsp;&nbsp;师：
            </th>
            <td>
                <cc1:DictionaryDropDownList ID="DictionaryDropDownList2" runat="server" DictionaryType="">
                    <asp:ListItem>张老师</asp:ListItem>
                    <asp:ListItem>李老师</asp:ListItem>
                </cc1:DictionaryDropDownList>
            </td>
        </tr>
        <tr>
            <th>
                培训时段：
            </th>
            <td colspan="3">
                <cc1:DateTimeTextBox ID="DateTimeTextBox1" runat="server" EndTimeControlID="DateTimeTextBox2"></cc1:DateTimeTextBox>
                至
                <cc1:DateTimeTextBox ID="DateTimeTextBox2" runat="server" BeginTimeControlID="DateTimeTextBox1"></cc1:DateTimeTextBox>
            </td>
        </tr>
        <tr>
            <th>
                讲师等级：
            </th>
            <td colspan="3">
                高级
            </td>
        </tr>
        <tr>
            <th>
                培训时间说明：
            </th>
            <td>
                <cc1:DictionaryDropDownList ID="DictionaryDropDownList3" runat="server" DictionaryType="Dic_TraningCourseSignIn"
                    IsShowChoose="true" />
            </td>
            <th>
                培训课时：
            </th>
            <td>
                <input type="text" name="textfield4" class="inputbox_120" />
            </td>
        </tr>
        <tr>
            <th>
                培训地点：
            </th>
            <td colspan="3">
                <cc1:DictionaryDropDownList ID="DictionaryDropDownList5" runat="server" DictionaryType=""
                    IsShowChoose="true">
                    <asp:ListItem>请选择</asp:ListItem>
                </cc1:DictionaryDropDownList>
                支持录入检索
            </td>
        </tr>
        <tr>
            <th>
                课时说明：
            </th>
            <td colspan="3">
                <textarea class="inputbox_area300"></textarea>
            </td>
        </tr>
    </table>
</div>
<div class="dv_submit">
    <input type="button" class="btn_Save" onclick="javascript:popSuccessMsg('恭喜你保存成功！','提示',closeWindow);"
        value="保存" />
    <input type="button" class="btn_Cancel" value="取消" onclick="javascript:closeWindow();" /></div>
