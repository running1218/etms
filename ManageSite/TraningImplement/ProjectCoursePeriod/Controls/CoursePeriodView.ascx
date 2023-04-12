<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CoursePeriodView.ascx.cs"
    Inherits="TraningImplement_ProjectCoursePeriod_Controls_CoursePeriodView" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<!--表单录入-->
<div class="">
    <table class="GridviewGray GridveiwFixed">
        <tr>
            <th width="80">
                项目编码：
            </th>
            <td >
                <asp:Label ID="lblItemCode" runat="server"></asp:Label>
            </td>
            <th width="80">
                项目名称：
            </th>
            <td >
                <asp:Label ID="lblItemName" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <th>
                课程编码：
            </th>
            <td>
                <asp:Label ID="lblCourseCode" runat="server"></asp:Label>
            </td>
            <th>
                课程名称：
            </th>
            <td>
                <asp:Label ID="lblCourseName" runat="server"></asp:Label>
            </td>
        </tr>
        <tr id="trOrg" runat="server">
            <th>
                组织机构：
            </th>
            <td colspan="3">
                <asp:Label ID="lblOrg" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <th>
                课程属性：
            </th>
            <td colspan="3">
                <cc1:DictionaryLabel ID="dlblCourseAttr" DictionaryType="Dic_Sys_CourseAttr" runat="server" />
            </td>
        </tr>
        <tr>
            <th>
                课程类型：
            </th>
            <td>
                <cc1:DictionaryLabel ID="dlblCourseType" DictionaryType="Dic_Sys_CourseType" runat="server" />
            </td>
            <th>
                课程等级：
            </th>
            <td>
                <cc1:DictionaryLabel ID="dlblCourseLevel" DictionaryType="Dic_Sys_CourseLevel" runat="server" />
            </td>
        </tr>
        <tr>
            <th>
                培训方式：
            </th>
            <td>
                <cc1:DictionaryLabel ID="dlblTrainingModel" DictionaryType="Dic_Sys_TrainingModel"
                    runat="server" />
            </td>
            <th>
                授课方式：
            </th>
            <td>
                <cc1:DictionaryLabel ID="dlblTeachModel" DictionaryType="Dic_Sys_TeachModel" runat="server" />
            </td>
        </tr>
        <tr>
            <th>
                培训日期：
            </th>
            <td>
                <asp:Label ID="lblTrainingDate" runat="server"></asp:Label>
            </td>
            <th>
                培训时段：
            </th>
            <td>
                <asp:Label ID="lblTrainingBeginTime" runat="server"></asp:Label>
                至
                <asp:Label ID="lblTrainingEndTime" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <th>
                课时状态：
            </th>
            <td>
                <cc1:DictionaryLabel ID="dlblCourseHoursStatus" DictionaryType="Dic_Sys_CourseHoursStatus"
                    runat="server" />
            </td>
            <th>
                讲&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;师：
            </th>
            <td>
                <asp:Label ID="lblTeacher" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <th>
                培训时间说明：
            </th>
            <td>
                <cc1:DictionaryLabel ID="dlblTrainingTimeDesc" DictionaryType="Dic_Sys_TrainingTimeDesc"
                    runat="server" />
            </td>
            <th>
                培训课时：
            </th>
            <td>
                <asp:Label ID="lblCourseHours" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <th>
                培训地点：
            </th>
            <td colspan="3">
                <asp:Label ID="lblClassRoomAddress" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <th>
                课时说明：
            </th>
            <td colspan="3">
                <asp:Label ID="lblCourseHoursDesc" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
</div>
