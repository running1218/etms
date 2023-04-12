<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true"
    CodeFile="TraningPlanResultEdit.aspx.cs" Inherits="TraningPlan_TraningPlanResult_TraningPlanResultEdit" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="dv_information">
        <table class="GridviewGray">
            <tr>
                <th width="20%">
                    计划编码：
                </th>
                <td colspan="3">
                    <asp:Label ID="lblPlanCode" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th width="20%">
                    计划名称：
                </th>
                <td colspan="3">
                    <asp:Label ID="lblPlanName" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th width="20%">
                    计划类型：
                </th>
                <td colspan="3">
                    <cc1:DictionaryLabel ID="dlblPlanTypeID" DictionaryType="Dic_Sys_PlanType" runat="server" />
                </td>
            </tr>
            <tr>
                <th>
                    计划周期：
                </th>
                <td colspan="3">
                    <asp:Label ID="lblPlanTime" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    归档方式：
                </th>
                <td colspan="3">
                    <asp:DropDownList ID="ddl_PlanEndMode" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th>
                    归档说明：
                </th>
                <td colspan="3">
                    <asp:TextBox ID="txtPlanEndRemark" runat="server" CssClass="inputbox_area300" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    <div class="dv_submit">
        <cc1:CustomButton runat="server" ID="btnFile" Text="计划归档" CssClass="btn_schedulled" EnableConfirm="true"
            ConfirmTitle="提示" ConfirmMessage="确定归档吗？" OnClick="btnFile_Click" />
        <input type="button" class="btn_Cancel" value="取消" onclick="javascript:closeWindow();" /></div>
</asp:Content>
