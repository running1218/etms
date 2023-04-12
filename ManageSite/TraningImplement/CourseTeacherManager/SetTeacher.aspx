<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master" AutoEventWireup="true"
    CodeFile="SetTeacher.aspx.cs" Inherits="TraningImplement_CourseTeacherManager_SetTeacher" %>

<%@ Register Src="Controls/TeacherSelect.ascx" TagName="TeacherSelect" TagPrefix="uc1" %>
<%@ Register Src="Controls/TeacherNoSelect.ascx" TagName="TeacherNoSelect" TagPrefix="uc2" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBack" runat="Server">
    <asp:LinkButton ID="lbtnReturn" runat="server" Text="返回" CssClass="btn_Return"></asp:LinkButton>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--查找条件-->
    <div class="dv_GradeviewList">
        <table class="" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <th>
                    项目编码：
                </th>
                <td>
                    <asp:Label ID="lblItemCode" runat="server"></asp:Label>
                </td>
                <th>
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
                <li id="Tab_0" onclick="showTab('Tab_0', 'Div_Select_0','selected')"><a onfocus="blur()"
                    href="javascript:void(0);"><span class="bj">已选讲师</span></a></li>
                <li id="Tab_1" onclick="showTab('Tab_1', 'Div_Select_1','selected')"><a onfocus="blur()"
                    href="javascript:void(0);"><span class="bj">未选讲师</span></a></li>
            </ul>
        </div>
    </div>
    <div class="dv_serviceTab">
        <div class="info" align="center">
            <div id="Div_Select_0" style="display: none">
                <uc1:TeacherSelect ID="TeacherSelect1" runat="server" />
            </div>
            <div id="Div_Select_1" style="display: none">
                <uc2:TeacherNoSelect ID="TeacherNoSelect2" runat="server" />
            </div>
        </div>
    </div>
</asp:Content>
