<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="SetCourse.aspx.cs" Inherits="TraningImplement_CourseStudentManager_SetCourse" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="Controls/CourseSelect.ascx" TagName="CourseSelect" TagPrefix="uc1" %>
<%@ Register Src="Controls/CourseNoSelect.ascx" TagName="CourseNoSelect" TagPrefix="uc2" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBack" runat="Server">
    <asp:LinkButton ID="lbtnReturn" runat="server" Text="返回" CssClass="btn_Return"></asp:LinkButton>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--查找条件-->
    <div class="dv_GradeviewList">
        <table class="" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <th width="100%">
                    项目编码：
                </th>
                <td width="200">
                    <asp:Label ID="lblItemCode" runat="server"></asp:Label>
                </td>
                <th width="100%">
                    项目名称：
                </th>
                <td>
                    <asp:Label ID="lblItemName" runat="server"></asp:Label>
                </td>
            </tr>
            <tr id="trOrg" runat="server">
                <th>
                    组织机构：
                </th>
                <td colspan="3">
                    <cc1:DictionaryLabel ID="lblOrganization" DictionaryType="Dic_CurrentAndSubOrganization"
                        runat="server" />
                </td>
            </tr>
            <tr>
                <th>
                    <asp:Literal ID="ltlDepartment" runat="server" Text="<%$ Resources:UIResource, ui_department%>"></asp:Literal>：
                </th>
                <td>
                    <cc1:DictionaryLabel ID="lblDepartmentID" DictionaryType="vw_Dic_Sys_Department"
                        runat="server" />
                </td>
                <th>
                    <asp:Literal ID="ltlPosition" runat="server" Text="<%$ Resources:UIResource, ui_position%>"></asp:Literal>：
                </th>
                <td>
                    <cc1:DictionaryLabel ID="lblPost" DictionaryType="vw_Dic_Sys_Post" runat="server" />
                </td>
            </tr>
            <tr>
                <th>
                    学员姓名：
                </th>
                <td colspan="3">
                    <asp:Label ID="lblRealName" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div class="">
        <div class="dv_serviceTab">
            <div class="dv_Tabmenus">
                <ul>
                    <li id="Tab_0" onclick="showTab('Tab_0', 'Div_Select_0','selected')"><a onfocus="blur()"
                        href="javascript:void(0);"><span class="bj">已选课程</span></a></li>
                    <li id="Tab_1" onclick="showTab('Tab_1', 'Div_Select_1','selected')"><a onfocus="blur()"
                        href="javascript:void(0);"><span class="bj">未选课程</span></a></li>
                </ul>
            </div>
        </div>
        <div class="info" align="center">
            <div id="Div_Select_0" style="display: none">
                <uc1:CourseSelect ID="CourseSelect1" runat="server" />
            </div>
            <div id="Div_Select_1" style="display: none">
                <uc2:CourseNoSelect ID="CourseNoSelect2" runat="server" />
            </div>
        </div>
    </div>
</asp:Content>
