<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true"
    CodeFile="ProfessorEditInner.aspx.cs" Inherits="ETMS.WebApp.Manage.Resource.ProfessorManage.ProfessorEditInner" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--表单录入-->
    <div class="dv_information">
        <table class="GridviewGray fixedTable">
            <tr>
                <th>
                    讲师姓名：
                </th>
                <td>
                    <asp:Label ID="lblTeacherName" runat="server" />
                </td>
                <th>
                    账&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;户：
                </th>
                <td>
                    <asp:Label ID="lblLoginName" runat="server" />
                </td>
            </tr>
            <tr>
                <th>
                    <asp:Literal ID="ltlDepartment" runat="server" Text="<%$ Resources:UIResource, ui_department%>"></asp:Literal>：
                </th>
                <td>
                    <cc1:DictionaryLabel ID="lblDepartment" DictionaryType="Site_DepartmentByOrgID" runat="server" />
                </td>
                <th>
                    <asp:Literal ID="ltlPosition" runat="server" Text="<%$ Resources:UIResource, ui_position%>"></asp:Literal>：
                </th>
                <td>
                    <cc1:DictionaryLabel ID="lblPost" DictionaryType="Dic_PostByOrgID" runat="server" />
                </td>
            </tr>
            <tr>
                <th>
                    讲师分类：
                </th>
                <td>
                    <% if (Operation == 1) %>
                    <%{ %>
                    <cc1:DictionaryLabel ID="lblTeacherType" DictionaryType="Dic_Sys_TeacherType" runat="server" />
                    <%}
                       else
                       { %>
                    <cc1:DictionaryDropDownList ID="ddlTeacherType" runat="server" DictionaryType="Dic_Sys_TeacherType"
                        IsShowChoose="true" />
                    <%} %>
                </td>
                <th>
                    讲师等级：
                </th>
                <td>
                    <% if (Operation == 1) %>
                    <%{ %>
                    <cc1:DictionaryLabel ID="lblTeacherLevel" runat="server" DictionaryType="Dic_Sys_TeacherLevel" />
                    <%}
                       else
                       { %>
                    <cc1:DictionaryDropDownList ID="ddlTeacherLevel" runat="server" DictionaryType="Dic_Sys_TeacherLevel" IsShowChoose="true" />                    
                    <%} %>
                </td>
            </tr>
            <tr>
                <th>
                    状&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;态：
                </th>
                <td>
                    <% if (Operation == 1) %>
                    <%{ %>
                    <cc1:DictionaryLabel runat="server" ID="lblStatus" DictionaryType="Dic_Status" />
                    <%}
                       else
                       { %>
                    <cc1:DictionaryRadioButtonList runat="server" ID="rbStatus" DictionaryType="Dic_Status" />
                    <%} %>
                </td>
                <th>
                    工&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;号：
                </th>
                <td>
                    <asp:Label ID="lblWorkNo" runat="server" />
                </td>
            </tr>
            <tr>
                <th>
                    课程设计人员：
                </th>
                <td colspan="3">
                    <cc1:DictionaryRadioButtonList runat="server" ID="RblCourseDesinger" DictionaryType="Dic_TrueOrFalse">
                    </cc1:DictionaryRadioButtonList>
                </td>
            </tr>
        </table>
    </div>
    <!--提交表单-->
    <div class="dv_submit">
        <asp:Button ID="LinkButton1" Text="保存" runat="server" CssClass="btn_Save" OnClick="LinkButton1_Click"
            ValidationGroup="Error"></asp:Button>
        <asp:ValidationSummary runat="server" ID="ValidationSummary1" ValidationGroup="Error"
            ShowMessageBox="true" ShowSummary="false" />
        <input type="button" value="取消" onclick="javascript:closeWindow();" class="btn_Cancel padleft10" />
    </div>
</asp:Content>
