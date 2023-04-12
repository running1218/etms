<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true"
    CodeFile="SetsCourseView.aspx.cs" Inherits="ETMS.WebApp.Manage.TraningPlan.TraningPlan.SetsCourseView" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <div class="dv_information">
            <table class="GridviewGray">
                <tr>
                    <th>
                        计划编码：
                    </th>
                    <td colspan="3">
                        <asp:Label ID="lblPlanCode" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th>
                        计划名称：
                    </th>
                    <td colspan="3">
                        <asp:Label ID="lblPlanName" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th width="20%">
                        课程编码：
                    </th>
                    <td colspan="3">
                        <asp:Label ID="lblCourseCode" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th width="20%">
                        课程名称：
                    </th>
                    <td colspan="3">
                        <asp:Label ID="lblCourseName" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th width="20%">
                        课程类型：
                    </th>
                    <td width="30%">
                        <cc1:DictionaryLabel ID="lblCourseType" DictionaryType="Dic_Sys_CourseType" runat="server" />
                    </td>
                    <th width="20%">
                        课程等级：
                    </th>
                    <td width="30%">
                        <cc1:DictionaryLabel ID="lblCourseLevel" DictionaryType="Dic_Sys_CourseLevel" runat="server" />
                    </td>
                </tr>
                <tr>
                    <th width="20%">
                        课程状态：
                    </th>
                    <td width="30%">
                        <cc1:DictionaryLabel ID="lblCourseStatus" DictionaryType="Dic_Status" runat="server" />
                    </td><th>
                        授课方式：
                    </th>
                    <td>
                        <cc1:DictionaryLabel ID="lblTeachModel" DictionaryType="Dic_Sys_TeachModel" runat="server" />
                    </td>
                    
                </tr>
                <tr style="display:none;">
                    <th>
                        培训方式：
                    </th>
                    <td>
                        <cc1:DictionaryLabel ID="lblTrainingModel" DictionaryType="Dic_Sys_TrainingModel"
                            runat="server" />
                    </td>
                    <th>
                        外训机构：
                    </th>
                    <td>
                        <cc1:DictionaryLabel ID="dlblOuterOrg" DictionaryType="Tr_OuterOrg" runat="server" />
                    </td>
                </tr>
                <tr style="display:none;">                   
                    <th>
                        预&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;算：
                    </th>
                    <td colspan="3">
                        <asp:Label ID="lblBudgetFee" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th>
                        适用对象：
                    </th>
                    <td colspan="3">
                        <asp:Label ID="lblForObject" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th>
                        课程介绍：
                    </th>
                    <td colspan="3">
                        <asp:Label ID="lblCourseIntroduction" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th>
                        课程大纲：
                    </th>
                    <td colspan="3">
                        <asp:Label ID="lblCourseOutline" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div class="dv_submit">
            <input value="关闭" type="button" onclick="javascript:closeWindow();" class="btn_Close"></a></div>
    </div>
</asp:Content>
