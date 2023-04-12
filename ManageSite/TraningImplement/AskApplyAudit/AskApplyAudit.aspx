<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master"
    AutoEventWireup="true" CodeFile="AskApplyAudit.aspx.cs" Inherits="TraningImplement_AskApplyAudit_AskApplyAudit" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--功能标题-->
    <h2 class="dv_title">
        审核
    </h2>
    <!--表单录入-->
    <div class="dv_information">
        <table class="GridviewGray">
            <tr>
                <th>
                    项目名称：
                </th>
                <td colspan="3">
                    <cc1:ShortTextLabel ShowTextNum="20" runat="server" ID="lblItemName" />
                </td>
            </tr>
            <tr>
                <th style=" width:15%;">
                    课程名称：
                </th>
                <td style=" width:25%;">
                   <cc1:ShortTextLabel ShowTextNum="10" runat="server" ID="lblCourseName" />
                </td>
                <th style=" width:15%;">
                    课程安排：
                </th>
                <td style=" width:25%;">
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
              <th>组织机构：</th>
              <td colspan="3">
                  <cc1:DictionaryLabel runat="server" ID="lblOrg" DictionaryType="vw_Dic_Sys_Organization" TextLength="10" />  
              </td>
            </tr>
            <tr>
                <th>
                    所属<asp:Literal ID="ltlDepartment" runat="server" Text="<%$ Resources:UIResource, ui_department%>"></asp:Literal>：
                </th>
                <td>
                     <cc1:DictionaryLabel ID="lblDepartment" DictionaryType="vw_Dic_Sys_Department" TextLength="20" runat="server" />
                </td>
                <th>
                    <asp:Literal ID="ltlPosition" runat="server" Text="<%$ Resources:UIResource, ui_position%>"></asp:Literal>：
                </th>
                <td>
                    <cc1:DictionaryLabel ID="lblPost" DictionaryType="vw_Dic_Sys_Post" TextLength="20" runat="server" />
                </td>
            </tr>
            <tr>
                <th>
                    <asp:Literal ID="ltlRank" runat="server" Text="<%$ Resources:UIResource, ui_rank%>"></asp:Literal>：
                </th>
                <td>
                    <cc1:DictionaryLabel ID="lblRankID" DictionaryType="vw_Dic_Sys_Rank" TextLength="20" runat="server" />
                </td>
                <th>
                    申请时间：
                </th>
                <td>
                    <asp:Literal ID="lblLeaveTime" runat="server" />
                </td>
            </tr>            
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
                    审核意见：
                </th>
                <td colspan="3">
                    <asp:TextBox TextMode="MultiLine" ID="txtAuditOpinion" CssClass="inputbox_area300" runat="server" />
                    <%--<textarea class="inputbox_area300"></textarea>--%>
                </td>
            </tr>
        </table>
    </div>
    <div class="dv_submit">
        <asp:Button ID="btnAgree" runat="server" CssClass="btn_Agree" Text="审核通过" OnClick="btnAgree_Click" />
        <asp:Button ID="btnDeny" runat="server" CssClass="btn_Deny" Text="审核不通过" OnClick="btnDeny_Click" />        
        <input type="button" class="btn_Cancel" value="取消" onclick="javascript:closeWindow();" /></div>
</asp:Content>
