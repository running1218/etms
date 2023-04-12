<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="TraningProjectAuditView.aspx.cs" Inherits="TraningImplement_TraningProjectAudit_TraningProjectAuditView" %>
    
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="../TraningProjectManager/Controls/TraningProjectView.ascx" TagName="TraningProjectView"
    TagPrefix="uc1" %>
<%@ Register Src="../TraningProjectManager/Controls/TraningCourseListView.ascx" TagName="TraningCourseListView"
    TagPrefix="uc3" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBack" runat="Server">
    <a href="TraningProjectList.aspx" class="btn_Return">返回</a>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="dv_serviceTab">
        <div class="dv_Tabmenus" style="min-width: 800px;">
            <ul>
                <li id="Tab_0" onclick="showTab('Tab_0', 'Div_Select_0','selected');">
                    <a onfocus="blur()" href="javascript:void(0);"><span class="bj">项目信息</span></a></li>
                <li id="Tab_1" onclick="showTab('Tab_1', 'Div_Select_1','selected');"><a onfocus="blur()"
                    href="javascript:void(0);"><span class="bj">包含课程</span></a></li>
            </ul>
        </div>
    </div>
    <div class="info">
        <div id="Div_Select_0" class="dv_pageInformation" style="display: none">
            <uc1:TraningProjectView ID="TraningProjectView1" runat="server" />            
        </div>
        <div id="Div_Select_1" style="display: none">
            <uc3:TraningCourseListView ID="TraningCourseListView3" runat="server" />
        </div>
    </div>
</asp:Content>
