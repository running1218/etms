<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true"
    CodeFile="SetsCourseView.aspx.cs" Inherits="TraningImplement_TraningProjectManager_SetsCourseView" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--表单录入-->
    <div class="dv_information">
        <table class="GridviewGray fixedTable">
            <tr>
                <th style="width: 80px;">
                    项目编码：
                </th>
                <td colspan="3">
                    <asp:Label ID="lblItemCode" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    项目名称：
                </th>
                <td colspan="3">
                    <asp:Label ID="lblItemName" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    课程编码：
                </th>
                <td colspan="3">
                    <asp:Label ID="lblCourseCode" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    课程名称：
                </th>
                <td colspan="3">
                    <asp:Label ID="lblCourseName" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <table class="GridviewGray fixedTable">
            <tr>
                <th style="width: 80px;">
                    课程类型：
                </th>
                <td>
                    <cc1:DictionaryLabel ID="DictionaryLabelCourseType" DictionaryType="Dic_Sys_CourseType"
                        runat="server" />
                </td>
                <th>
                    课程等级：
                </th>
                <td>
                    <cc1:DictionaryLabel ID="DictionaryLabelCourseLevel" DictionaryType="Dic_Sys_CourseLevel"
                        runat="server" />
                </td>
            </tr>
            <tr>
                <th>
                    授课方式：
                </th>
                <td>
                    <cc1:DictionaryLabel ID="DictionaryLabelTeachModel" DictionaryType="Dic_Sys_TeachModel"
                        runat="server" />
                </td>
                <th>
                    状&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;态：
                </th>
                <td>
                    <asp:Label ID="lblCourseStatus" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    培训日期：
                </th>
                <td>
                    <asp:Label ID="lblCourseBeginTime" runat="server"></asp:Label>
                    至
                    <asp:Label ID="lblCourseEndTime" runat="server"></asp:Label>
                </td>
                <th>
                    及&nbsp;&nbsp;格&nbsp;&nbsp;线：
                </th>
                <td>
                    <asp:Label ID="lblPassLine" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    培训方式：
                </th>
                <td>
                    <cc1:DictionaryLabel ID="DictionaryLabelTrainingModel" DictionaryType="Dic_Sys_TrainingModel"
                        runat="server" />
                </td>
                <th>
                    外训机构：
                </th>
                <td>
                    <asp:Label ID="lblOuterOrg" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    课程学时：
                </th>
                <td>
                    <asp:Label ID="lblCourseHours" runat="server" Text=""></asp:Label>
                </td>
                <th>
                    外训机构联系人：
                </th>
                <td>
                    <asp:Label ID="lblOuterOrgDutyUser" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    课程积分：
                </th>
                <td>
                    <asp:Label ID="lblScore" runat="server" Text=""></asp:Label>
                </td>
                <th>
                    外训机构邮箱：
                </th>
                <td>
                    <asp:Label ID="lblOuterOrgEMAIL" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    课程属性：
                </th>
                <td>
                    <cc1:DictionaryLabel ID="DictionaryLabelCourseAttr" DictionaryType="Dic_Sys_CourseAttr"
                        runat="server" />
                </td>
                <th>
                    预&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;算：
                </th>
                <td>
                    <asp:Label ID="lblBudgetFee" runat="server" Text=""></asp:Label>
                    元
                </td>
            </tr>
            <tr>
                <th>
                    备&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;注：
                </th>
                <td colspan="3">
                    <asp:Label ID="lblRemark" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <!--提交表单-->
    <div class="dv_submit">
        <input type="button" class="btn_Close" onclick="javascript:closeWindow();" value="关闭" />
    </div>
</asp:Content>
