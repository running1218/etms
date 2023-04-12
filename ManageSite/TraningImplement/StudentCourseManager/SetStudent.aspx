<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="SetStudent.aspx.cs" Inherits="TraningImplement_StudentCourseManager_SetStudent" %>

<%@ Register Src="Controls/StudentSelect.ascx" TagName="StudentSelect" TagPrefix="uc1" %>
<%@ Register Src="Controls/StudentNoSelect.ascx" TagName="StudentNoSelect" TagPrefix="uc2" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBack" runat="Server">
    <a href="CourseList.aspx" class="btn_Return" runat="server" id="aBack">返回</a>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--查找条件-->
    <div class="dv_GradeviewList">
        <table class="" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <th width="100">
                    项目编码：
                </th>
                <td>
                    <asp:Label ID="lblItemCode" runat="server"></asp:Label>
                </td>
                <th width="100">
                    项目名称：
                </th>
                <td>
                    <asp:Label ID="lblItemName" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    课程编码：
                </th>
                <td>
                    <asp:Label ID="lblCourseCode" runat="server"></asp:Label>
                </td>
                <th>
                    课程名称：
                </th>
                <td>
                    <asp:Label ID="lblCourseName" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div class="dv_serviceTab">
        <div class="dv_Tabmenus">
            <ul>
                <li id="Tab_0" onclick="showTab('Tab_0', 'Div_Select_0','selected');"><a onfocus="blur()"
                    href="javascript:void(0);"><span class="bj">已选学员</span></a></li>
                <li id="Tab_1" onclick="showTab('Tab_1', 'Div_Select_1','selected');"><a onfocus="blur()"
                    href="javascript:void(0);"><span class="bj">未选学员</span></a></li>
            </ul>
        </div>
    </div>
    <div class="info" align="center">
        <div id="Div_Select_0" style="display: none">
            <uc1:StudentSelect ID="StudentSelect1" runat="server" />
        </div>
        <div id="Div_Select_1" style="display: none">
            <uc2:StudentNoSelect ID="StudentNoSelect2" runat="server" />
        </div>
    </div>
</asp:Content>
