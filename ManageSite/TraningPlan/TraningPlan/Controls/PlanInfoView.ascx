<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PlanInfoView.ascx.cs"
    Inherits="TraningPlan_TraningPlan_Controls_PlanInfoView" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<table class="GridviewGray">
      <tr>
        <th style="text-align:right;" width="100">
            计划编码：
        </th>
        <td >
            <asp:Label ID="lblPlanCode" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <th style="text-align:right;" width="100">
            计划名称：
        </th>
        <td>
            <asp:Label ID="lblPlanName" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <th style="text-align:right;">
            计划周期：
        </th>
        <td >
            <asp:Label ID="lblPlanTime" runat="server"></asp:Label>
        </td>
    </tr>
     <tr>
        <th style="text-align:right;">
            计划状态：
        </th>
        <td >
            <cc1:DictionaryLabel ID="dlblPlanStatus" DictionaryType="Dic_TraningPlanState"
                runat="server" />
        </td>
    </tr>
</table>
<table class="GridviewGray"> 
   
    <tr>
        <th style="text-align:right;">
            培训级别：
        </th>
        <td width="300">
            <cc1:DictionaryLabel ID="dlblTrainingLevel" DictionaryType="Dic_Sys_TrainingLevel"
                runat="server" />
        </td>
        <th style="text-align:right;" width="100">
            是否启用：
        </th>
        <td >
            <cc1:DictionaryLabel ID="dlblIsUse" DictionaryType="Dic_TrueOrFalse" runat="server" />
        </td>
    </tr>
    <tr id="Tr_Department" class="Tr_Department" runat="server" visible="false">
        <th style="text-align:right;">
            组织<asp:Literal ID="ltlDepartment" runat="server" Text="<%$ Resources:UIResource, ui_department%>"></asp:Literal>：
        </th>
        <td colspan="3">
            <cc1:DictionaryLabel ID="lblDept" DictionaryType="Site_DepartmentByOrgID" runat="server" />
        </td>
    </tr>
    <tr>
        <th style="text-align:right;">
            计划类型：
        </th>
        <td>
            <cc1:DictionaryLabel ID="dlblPlanType" DictionaryType="Dic_Sys_PlanType" runat="server" />
        </td>
        <th style="text-align:right;">
            负&nbsp;&nbsp;责&nbsp;&nbsp;人：
        </th>
        <td>
            <asp:Label ID="lblDutyUser" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <th style="text-align:right;">
            手&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;机：
        </th>
        <td>
            <asp:Label ID="lblMobile" runat="server"></asp:Label>
        </td>
        <th style="text-align:right;">
            邮&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;箱：
        </th>
        <td>
            <asp:Label ID="lblEMAIL" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <th style="text-align:right;">
            预&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;算：
        </th>
        <td>
            <asp:Label ID="lblBudgetFee" runat="server"></asp:Label>元
        </td>
        <th style="text-align:right;">
            计划学员数：
        </th>
        <td>
            <asp:Label ID="lblStudentNum" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <th style="text-align:right;">
            创&nbsp;&nbsp;建&nbsp;&nbsp;人：
        </th>
        <td>
            <asp:Label ID="lblCreateUser" runat="server"></asp:Label>
        </td>
        <th style="text-align:right;">
            创建时间：
        </th>
        <td>
            <asp:Label ID="lblCreateTime" runat="server"></asp:Label>
        </td>        
    </tr>

</table>
<table class="GridviewGray">
       <tr>
        <th style="text-align:right;">
            计划目标：
        </th>
        <td colspan="3">
            <asp:Label ID="lblPlanTarget" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <th style="text-align:right;">
            目标学员：
        </th>
        <td colspan="3">
            <asp:Label ID="lblPlanObjectStudent" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <th style="text-align:right;">
            备&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;注：
        </th>
        <td colspan="3">
            <asp:Label ID="lblRemark" runat="server"></asp:Label>
        </td>
    </tr>
</table>
<table id="tabAudit" class="GridviewGray" runat="server" visible="true">
    <tr>
        <th width="100" style="text-align:right;">
            审&nbsp;&nbsp;核&nbsp;&nbsp;人：
        </th>
        <td width="300">
            <asp:Label ID="labAuditUser" runat="server"></asp:Label>
        </td>
        <th width="100" style="text-align:right;">
            审核时间：
        </th>
        <td >
            <asp:Label ID="labAuditTime" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <th width="100" style="text-align:right;">
            审核意见：
        </th>
        <td align="left" colspan="3">
            <asp:Label ID="labAuditOpinion" runat="server"></asp:Label>
        </td>
    </tr>
</table>
<table id="tabResult" class="GridviewGray" runat="server" visible="true">
    <tr>
        <th width="100" style="text-align:right;">
            归&nbsp;&nbsp;档&nbsp;&nbsp;人：
        </th>
        <td width="300">
            <asp:Label ID="lblModifyUser" runat="server"></asp:Label>
        </td>
        <th width="100" style="text-align:right;">
            归档时间：
        </th>
        <td >
            <asp:Label ID="lblModifyTime" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <th style="text-align:right;">
            归档方式：
        </th>
        <td colspan="3">
            <cc1:DictionaryLabel ID="dlblPlanEndModeID" DictionaryType="Dic_TraningPlanResultEndMode"
                runat="server" />
        </td>
    </tr>
    <tr>
        <th style="text-align:right;">
            归档说明：
        </th>
        <td align="left" colspan="3">
            <asp:Label ID="lblPlanEndRemark" runat="server"></asp:Label>
        </td>
    </tr>
</table>