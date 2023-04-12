<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CourseInfoView.ascx.cs"
    Inherits="Resource_CourseManage_Controls_CourseInfoView" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<table class="GridviewGray">
    <tr>
        <td rowspan="5" style="width: 110px; text-align: center;">
            <asp:Image ID="imgLogo" runat="server" ImageAlign="Middle" Height="100px" Width="100px" />
        </td>
        <th>
            课程编码：
        </th>
        <td>
            <asp:Literal ID="ltlCourseCode" runat="server"></asp:Literal>
        </td>
        <th>
            课程名称：
        </th>
        <td>
            <asp:Literal ID="ltlCourseName" runat="server"></asp:Literal>
        </td>
    </tr>
    <tr>
        <th>
            课程类型：
        </th>
        <td>
            <cc1:DictionaryLabel ID="lblCourseType" runat="server" DictionaryType="Dic_Sys_CourseType"></cc1:DictionaryLabel>
        </td>
        <th>
            课程等级：
        </th>
        <td>
            <cc1:DictionaryLabel ID="lblCourseLevel" runat="server" DictionaryType="Dic_Sys_CourseLevel"></cc1:DictionaryLabel>
        </td>
    </tr>
    <tr>
        <th>
            讲师人数：
        </th>
        <td>
            <asp:Literal ID="ltlTeacherNum" runat="server"></asp:Literal>
        </td>
        <th>
            课&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;时：
        </th>
        <td>
            <asp:Literal ID="ltlCourseHours" runat="server"></asp:Literal>&nbsp;&nbsp;小时
        </td>
    </tr>
    <tr>
        <th>
            课程状态：
        </th>
        <td colspan="3">
            <cc1:DictionaryLabel ID="lblCourseStatus" runat="server" DictionaryType="Dic_Status"></cc1:DictionaryLabel>
        </td>
    </tr>
    <tr>
        <th>
            创建人：
        </th>
        <td>
            <asp:Literal ID="ltlCreateUser" runat="server"></asp:Literal>
        </td>
        <th>
            创建时间：
        </th>
        <td>
            <asp:Literal ID="ltlCreateTime" runat="server"></asp:Literal>
        </td>
    </tr>
    <tr >
        <th style="text-align: right;"class="hide">
            是否公开：
        </th>
        <td class="hide">
            <cc1:DictionaryLabel ID="lblIsPublic" runat="server" DictionaryType="Dic_TrueOrFalseBool"></cc1:DictionaryLabel>
        </td>
        <th style="text-align: right;" class="hide">
            评&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;价：
        </th>
        <td class="hide">
            <asp:Literal ID="ltlCourseEvaluation" runat="server"></asp:Literal>
        </td>
    </tr>
</table>
<table class="GridviewGray">
    <tr>
        <th>
            适用对象：
        </th>
        <td class="ol-style">
            <asp:Literal ID="ltlForObject" runat="server"></asp:Literal>
        </td>
    </tr>
    <tr>
        <th style="vertical-align: top; text-align: right;">
            课程介绍：
        </th>
        <td style="vertical-align: top;" class="ol-style">
            <asp:Literal ID="ltlCourseIntroduction" runat="server"></asp:Literal>
        </td>
    </tr>
    <tr>
        <th style="vertical-align: top; text-align: right;">
            课程大纲：
        </th>
        <td style="vertical-align: top;" class="ol-style">
            <asp:Literal ID="ltlCourseOutline" runat="server"></asp:Literal>
        </td>
    </tr>
</table>
