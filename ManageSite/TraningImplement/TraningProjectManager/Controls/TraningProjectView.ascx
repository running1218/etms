<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TraningProjectView.ascx.cs"
    Inherits="TraningImplement_TraningProjectManager_Controls_TraningProjectView" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<!--表单录入-->
<table class="GridviewGray">
    <tr>
        <th>
            项目编码：
        </th>
        <td>
            <asp:Label ID="Lbl_ItemCode" runat="server" Text=""></asp:Label>
        </td>
        <th>
            项目名称：
        </th>
        <td>
            <asp:Label ID="Lbl_ItemName" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>        
        <th>
            是否启用：
        </th>
        <td>
            <asp:Label ID="lblIsUse" runat="server" Text=""></asp:Label>
        </td>       
        <th>
            专业类别：
        </th>
        <td>
            <cc1:DictionaryLabel ID="DictionaryLabel1" DictionaryType="Dic_Sys_SpecialtyType"
                runat="server" />
        </td>
    </tr>
    <tr id="trPlan" runat="server" visible="false">
        <th>
            所属培训计划：
        </th>
        <td colspan="3">
            <asp:Label ID="Lbl_Plan" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr class="hide">
        <th style="display:none">
            培训级别：
        </th>
        <td style="display:none">
            <cc1:DictionaryLabel ID="DictionaryLabel2" DictionaryType="Dic_Sys_TrainingLevel"
                runat="server" />
        </td>
        <th>
            来自计划：
        </th>
        <td>
            <cc1:DictionaryLabel ID="dlblIsPlanItem" DictionaryType="Dic_TrueOrFalseBool" runat="server" />
        </td> 
    </tr>
    <tr id="trTrainingLevel" runat="server" visible="false">
        <th>
            组织<asp:Literal ID="ltlDepartment" runat="server" Text="<%$ Resources:UIResource, ui_department%>"></asp:Literal>：
        </th>
        <td colspan="3">
            <cc1:DictionaryLabel ID="lblDept" DictionaryType="vw_Dic_Sys_Department" runat="server" />
        </td>
    </tr>
    <tr>
        <th>
            项目开始时间：
        </th>
        <td>
            <asp:Label ID="lblItemBeginTime" runat="server" Text=""></asp:Label>
        </td>
        <th>
            项目结束时间：
        </th>
        <td>
            <asp:Label ID="lblItemEndTime" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <th>
            状&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;态：
        </th>
        <td>
            <cc1:DictionaryLabel ID="dlabItemStatus" DictionaryType="Dic_TraningProjectType"
                runat="server" />
        </td>
        <th>
            报名方式：
        </th>
        <td>
            <cc1:DictionaryLabel ID="dlabSignupMode" DictionaryType="Dic_Sys_SignupMode"
                runat="server" />
        </td>
    </tr>
    <tr id="trIsAllowSignup" runat="server" visible="false">
        <th>
            报名开始时间：
        </th>
        <td>
            <asp:Label ID="lblSignupBeginTime" runat="server" Text=""></asp:Label>
        </td>
        <th>
            报名结束时间：
        </th>
        <td>
            <asp:Label ID="lblSignupEndTime" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <th>
            项目负责人：
        </th>
        <td>
            <asp:Label ID="Lbl_DutyUser" runat="server" Text=""></asp:Label>
        </td>
        <th>
            手&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;机：
        </th>
        <td>
            <asp:Label ID="Lbl_Mobile" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <th>
            邮&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;箱：
        </th>
        <td>
            <asp:Label ID="Lbl_EMAIL" runat="server" Text=""></asp:Label>
        </td>
        <th>
            预&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;算：
        </th>
        <td>
            <asp:Label ID="Lbl_BudgetFee" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <th>
            培训学员：
        </th>
        <td>
            <asp:Label ID="Lbtn_StudentNumber" runat="server"></asp:Label>
        </td>
        <th>
            包含课程：
        </th>
        <td>
            <asp:Label ID="Lbtn_CourseNumber" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <th>
            项目简介：
        </th>
        <td colspan="3">
            <asp:Label ID="Lbl_ItemTarget" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <th>
            目标学员：
        </th>
        <td colspan="3">
            <asp:Label ID="Lbl_ItemObjectStudent" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <th>
            备&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;注：
        </th>
        <td colspan="3">
            <asp:Label ID="Lbl_Remark" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <th>
            创&nbsp;&nbsp;建&nbsp;&nbsp;人：
        </th>
        
        <td>
            <asp:Label ID="lbl_CreateUser" runat="server"></asp:Label>
        </td>
        <th>
            创建时间：
        </th>
        <td>
            <asp:Label ID="lbl_CreateTime" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <th>
            最后修改人：
        </th>
        
        <td>
            <asp:Label ID="lbl_ModifyUser" runat="server"></asp:Label>
        </td>
        <th>
            最后修改时间：
        </th>
        <td>
            <asp:Label ID="lbl_ModifyTime" runat="server"></asp:Label>
        </td>
    </tr>
    <tr id="tabAudit" runat="server" visible="true">
        <th>
            审&nbsp;&nbsp;核&nbsp;&nbsp;人：
        </th>
        
        <td>
            <asp:Label ID="labAuditUser" runat="server"></asp:Label>
        </td>
        <th>
            审核时间：
        </th>
        <td>
            <asp:Label ID="labAuditTime" runat="server"></asp:Label>
        </td>
    </tr>
    <tr id="tabAudit1" runat="server" visible="true">
        <th>
            审核意见：
        </th>
        <td colspan="3">
            <asp:Label ID="labAuditOpinion" runat="server"></asp:Label>
        </td>
    </tr>
    <tr id="tabIsIssueEnd" runat="server" visible="true">
        <th>
            发&nbsp;&nbsp;布&nbsp;&nbsp;人：
        </th>
        <td>
            <asp:Label ID="lblIssueUser" runat="server"></asp:Label>
        </td>
        <th>
            发布时间：
        </th>
        <td>
            <asp:Label ID="lblIssueTime" runat="server"></asp:Label>
        </td>
    </tr>
    <tr id="tabItemEndMode1" runat="server">
        <th>
            归档方式：
        </th>
        <td colspan="3">
            <cc1:DictionaryLabel ID="dlblItemEndModeID" DictionaryType="Dic_Sys_ItemEndMode"
                runat="server" />
        </td>
    </tr>
    <tr id="tabItemEndMode2" runat="server">
        <th>
            归&nbsp;&nbsp;档&nbsp;&nbsp;人：
        </th>
        <td>
            <asp:Label ID="lblModifyUser" runat="server"></asp:Label>
        </td>
        <th>
            归档时间：
        </th>
        <td>
            <asp:Label ID="lblModifyTime" runat="server"></asp:Label>
        </td>
    </tr>
    <tr id="tabItemEndMode3" runat="server">
        <th>
            归档说明：
        </th>
        <td colspan="3">
            <asp:Label ID="lblItemEndReMark" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <th>
            是否启用：
        </th>
        <td>
            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
        </td>
    </tr>
</table>
