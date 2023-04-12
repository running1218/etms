<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="ExcerciseStatus.aspx.cs" Inherits="TraningImplement_ProjectCourseResource_ExcerciseStatus" %>

<%@ Register Src="ExOfflineHomework/Controls/ExcerciseNoSubmit.ascx" TagName="NoSubmit"
    TagPrefix="uc1" %>
<%@ Register Src="ExOfflineHomework/Controls/ExerciseUnEvaluation.ascx" TagName="UnEvaluation"
    TagPrefix="uc2" %>
<%@ Register Src="ExOfflineHomework/Controls/ExerciseEvaluated.ascx" TagName="Evaluated"
    TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBack" runat="Server">
    <a id="aBack" runat="server" class="btn_Return">返回</a>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="dv_GradeviewList">
        <table border="0" cellpadding="0" cellspacing="0" runat="server" id="tableQueryControlList">
            <tr>
                <th width="100">
                    项目名称：
                </th>
                <td>
                    <asp:Label ID="lblItemID" runat="server" />
                </td>
                <th width="100">
                    课程名称：
                </th>
                <td>
                    <asp:Label ID="lblCourseID" runat="server" />
                </td>
            </tr>
            <tr>
                <th>
                    名称：
                </th>
                <td>
                    <asp:Label ID="lblJobID" runat="server" />
                </td>
                <th>
                    起止时间：
                </th>
                <td>
                    <asp:Label ID="lblTime" runat="server" />
                </td>
            </tr>
            <tr>
                <th>
                    描述：
                </th>
                <td colspan="3">
                    <asp:Label ID="lblJobDescription" runat="server" />
                </td>
            </tr>
            <tr>
                <th>
                    附件：
                </th>
                <td>
                    <asp:Label ID="lblJobFileName" runat="server"  />
                </td>
                <th>
                    学员总数：
                </th>
                <td>
                    <asp:Label ID="lblStudentNum" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <!--表单录入-->
    <div class="">
        <div class="dv_serviceTab">
            <div class="dv_Tabmenus">
                <ul>
                    <li id="Tab_0" onclick="showTab('Tab_0', 'Div_Select_0','selected')"><a onfocus="blur()"
                        href="javascript:void(0);"><span class="bj">待批阅</span></a></li>
                    <li id="Tab_1" onclick="showTab('Tab_1', 'Div_Select_1','selected')"><a onfocus="blur()"
                        href="javascript:void(0);"><span class="bj">已批阅</span></a></li>
                    <li id="Tab_2" onclick="showTab('Tab_2', 'Div_Select_2','selected')"><a onfocus="blur()"
                        href="javascript:void(0);"><span class="bj">未提交</span></a></li>
                </ul>
            </div>
        </div>
        <div class="info">
            <div id="Div_Select_0" style="display: none">
                <uc2:UnEvaluation ID="ExerciseUnEvaluation" runat="server"/>
            </div>
            <div id="Div_Select_1" style="display: none">
                <uc3:Evaluated ID="Evaluated" runat="server" />
            </div>
            <div id="Div_Select_2" style="display: none">
                <uc1:NoSubmit ID="ExcerciseNoSubmit" runat="server" />
            </div>
        </div>
    </div>
</asp:Content>
