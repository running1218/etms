<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master" AutoEventWireup="true" CodeFile="PlanAuditView.aspx.cs" Inherits="TraningPlan_TraningPlanAudit_PlanAuditView" %>

<%@ Register Src="../TraningPlan/Controls/PlanInfoView.ascx" TagName="PlanInfoView" TagPrefix="uc3" %>
<%@ Register Src="../TraningPlan/Controls/CourseListView.ascx" TagName="CourseListView" TagPrefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBack" runat="Server">
    <a id="aBack" runat="server" class="btn_Return">返回</a>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="dv_serviceTab">
        <div class="dv_Tabmenus">
            <ul>
                <li id="Tab_0" onclick="showTab('Tab_0', 'Div_Select_0','selected');">
                    <a onfocus="blur()" href="javascript:void(0);"><span class="bj">计划信息</span></a></li>
                <li id="Tab_1" onclick="showTab('Tab_1', 'Div_Select_1','selected');">
                    <a onfocus="blur()" href="javascript:void(0);"><span class="bj">课程信息</span></a></li>
            </ul>
        </div>
    </div>
    <div class="info">
        <div id="Div_Select_0" class="dv_pageInformation">
            <uc3:PlanInfoView ID="PlanInfoView1" runat="server" />
        </div>
        <div id="Div_Select_1" style="display: none">
            <uc4:CourseListView ID="CourseListView1" runat="server" />
        </div>
    </div>
</asp:Content>
