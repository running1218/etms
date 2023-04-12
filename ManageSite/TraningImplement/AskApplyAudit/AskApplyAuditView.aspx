<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true"
    CodeFile="AskApplyAuditView.aspx.cs" Inherits="TraningImplement_AskApplyAudit_AskApplyAuditView" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--功能标题-->
    <h2 class="dv_title">
        查看
    </h2>
    <!--表单录入-->
    <div class="dv_information">
        <table class="GridviewGray fixedTable">
            <tr>
                <th>
                    项目名称：
                </th>
                <td colspan="3">
                    <asp:Literal ID="lblItemName" runat="server" />
                </td>
            </tr>
        </table>
        <table class="GridviewGray fixedTable">
            <tr>
                <th>
                    课程名称：
                </th>
                <td>
                    <asp:Literal ID="lblCourseName" runat="server" />
                </td>
                <th>
                    课程安排：
                </th>
                <td>
                    <asp:Literal ID="lblTrainingDate" runat="server" />
                </td>
            </tr>
            <tr>
                <th>
                    学员工号：
                </th>
                <td>
                    <asp:Literal ID="lblWorkerNo" runat="server" />
                </td>
                <th>
                    学员姓名：
                </th>
                <td>
                    <asp:Literal ID="lblRealName" runat="server" />
                </td>
            </tr>
            <tr>
                <th>
                    所属<asp:Literal ID="ltlDepartment" runat="server" Text="<%$ Resources:UIResource, ui_department%>"></asp:Literal>：
                </th>
                <td>
                    <cc1:dictionarylabel id="lblDepartment" dictionarytype="Site_DepartmentByOrgID" textlength="20"
                        runat="server" />
                </td>
                <th>
                    <asp:Literal ID="ltlPosition" runat="server" Text="<%$ Resources:UIResource, ui_position%>"></asp:Literal>：
                </th>
                <td>
                    <cc1:dictionarylabel id="lblPost" dictionarytype="Dic_PostByOrgID" textlength="20"
                        runat="server" />
                </td>
            </tr>
            <tr>
                <th>
                    <asp:Literal ID="ltlRank" runat="server" Text="<%$ Resources:UIResource, ui_rank%>"></asp:Literal>：
                </th>
                <td>
                    <cc1:dictionarylabel id="lblRankID" dictionarytype="vw_Dic_Sys_Rank" textlength="20"
                        runat="server" />
                </td>
                <th>
                    申请时间：
                </th>
                <td>
                    <asp:Literal ID="lblLeaveTime" runat="server" />
                </td>
            </tr>
        </table>
        <table class="GridviewGray fixedTable">
            <tr>
                <th>
                    请假原因：
                </th>
                <td colspan="3">
                    <asp:Literal ID="lblLeaveReason" runat="server" />
                </td>
            </tr>
            <tr>
                <th>
                    审核结果：
                </th>
                <td colspan="3">
                   <cc1:DictionaryLabel ID="lblAuditResult" runat="server" DictionaryType="Dic_TraningPlanAuditState" />                    
                </td>
            </tr>
            <tr>
                <th>
                    审核意见：
                </th>
                <td colspan="3">
                    <asp:Literal ID="lblAuditOpinion" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <div class="dv_submit">
        <input type="button" onclick="javascript:closeWindow();" value="关闭" class="btn_Close">
    </div>
</asp:Content>
