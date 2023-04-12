<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ExerciseView.ascx.cs" Inherits="QuestionDB_ExOnlineTest_Controls_ExerciseView" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<!--表单录入-->
<div class="dv_information">
    <table class="GridviewGray">
        <tr>
            <th style="width: 15%">
                课程编号：
            </th>
            <td>
                <asp:Literal ID="ltlCourseCode" runat="server"></asp:Literal>
            </td>
            <th style="width: 15%">
                课程名称：
            </th>
            <td>
                <asp:Literal ID="ltlCourseName" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <th>
                测试名称：
            </th>
            <td colspan="3">
                <asp:Literal ID="ltlOnLineTestName" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <th style="width: 15%">
                显示答案：
            </th>
            <td style="width: 35%">
                <cc1:DictionaryLabel ID="lblIsShowAnswer" DictionaryType="Dic_TrueOrFalse" runat="server" />
            </td>
            <th style="width: 15%">
                状&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;态：
            </th>
            <td style="width: 35%">
                <cc1:DictionaryLabel ID="lblOnLineTestStatus" DictionaryType="Dic_Status" runat="server" />
            </td>
        </tr>
        <tr>
            <th style="vertical-align: top; height:80px;">
                描&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;述：
            </th>
            <td colspan="3" style="vertical-align:top;">
                <asp:Literal ID="ltlOnLineTestDesc" runat="server"></asp:Literal>
            </td>
        </tr>
    </table>
</div>
<!--提交表单-->
<div class="dv_submit">
    <a href="javascript:closeWindow();" class="btn_Close">关闭</a>
</div>
