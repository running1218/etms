<%@ Page Title="选择课程" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="SetsCourseAdd.aspx.cs" Inherits="TraningImplement_TraningProjectManager_SetsCourseAdd" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<%@ Register src="Controls/SetsCourseAdd.ascx" tagname="SetsCourseAdd" tagprefix="uc1" %>
<%@ Register src="Controls/SetsPlanCourseAdd.ascx" tagname="SetsPlanCourseAdd" tagprefix="uc2" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBack" runat="Server">
    <a href="" class="btn_Return" runat="server" id="aBack">返回</a>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="dv_serviceTab">
        <div class="dv_Tabmenus" <%=TabDisplay %>>
            <ul>
                <li id="Tab_0" onclick="showTab('Tab_0', 'Div_Select_0','selected')"><a onfocus="blur()"
                    href="javascript:void(0);"><span class="bj">所有课程</span></a></li>
                <li id="Tab_1" onclick="showTab('Tab_1', 'Div_Select_1','selected')"><a onfocus="blur()"
                    href="javascript:void(0);"><span class="bj">计划内课程</span></a></li>
            </ul>
        </div>
    </div>
    <div class="info">
        <div id="Div_Select_0" style="display: none">
            <uc1:SetsCourseAdd ID="SetsCourseAdd1" runat="server" />
        </div>
        <div id="Div_Select_1" style="display: none">
            <uc2:SetsPlanCourseAdd ID="SetsPlanCourseAdd1" runat="server" />
        </div>
    </div>
</asp:Content>
