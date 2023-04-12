<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true"
    CodeFile="StudentAddAll.aspx.cs" Inherits="TraningImplement_StudentCourseManager_StudentAddAll" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--表单录入-->
    <div class="dv_information">
        <table class="GridviewGray fixedTable">
            <tr>
                <th>
                    项目编码：
                </th>
                <td colspan="3">
                    <asp:Label ID="lblItemCode" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    项目名称：
                </th>
                <td colspan="3">
                    <asp:Label ID="lblItemName" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    课程编码：
                </th>
                <td colspan="3">
                    <asp:Label ID="lblCourseCode" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    课程名称：
                </th>
                <td colspan="3">
                    <asp:Label ID="lblCourseName" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th width="100">
                    项目学员数：
                </th>
                <td>
                    <asp:Label ID="lblProjectStudentTotal" runat="server"></asp:Label>
                </td>
                <th width="100">
                    已学学员数：
                </th>
                <td>
                    <asp:Label ID="lblAlreadylearnTotal" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    已报名数：
                </th>
                <td>
                    <asp:Label ID="lbl_SignUpStudentTotal" runat="server"></asp:Label>
                </td>
                <th>
                    已学学员报名数：
                </th>
                <td>
                    <asp:Label ID="lblAlreadylearnSignUpTotal" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    未报名学员数：
                </th>
                <td>
                    <asp:Label ID="lblNotSignUpTotal" runat="server"></asp:Label>
                </td>
                <th>
                    未学未报名学员数：
                </th>
                <td>
                    <asp:Label ID="lblNotStudyingNotSignUpTotal" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    添加学员方式：
                </th>
                <td colspan="3">
                    <asp:RadioButtonList ID="rblAddStudentWay" runat="server" CssClass="noborder" RepeatDirection="Horizontal">
                        <asp:ListItem Value="1" Text="仅添加未学未报名学员"></asp:ListItem>
                        <asp:ListItem Value="0" Text="添加项目全部学员" Selected="True"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
        </table>
        <div>
         说明：<br />
         项目学员数：培训项目学员数。<br />
         已报名数：培训项目学员中，已报名此课程的学员数。<br />
         未报名学员数：培训项目学员中，未报名此课程的学员数，未报名学员数＝项目学员数－已报名数。<br />
         已学学员数：培训项目学员中，在其它培训项目中已报名学习此课程的学员数。<br />
         已学学员报名数：培训项目学员中，已学学员再次报名此课程的学员数。<br />
         未学未报名学员数：培训项目学中，未学过此课程的学员未报名的学员数。<br />
         未学未报名学员数＝项目学员数－已学学员数－（已报名数－已学学员报名数）
        </div>
    </div>
    <div class="dv_submit">
        <asp:Button ID="Btn_Save" CssClass="btn_Save" runat="server" Text="确定" OnClick="Btn_Save_Click"
            ValidationGroup="Saves" />
        <input type="button" class="btn_Cancel" value="取消" onclick="javascript:closeWindow();" /></div>
</asp:Content>
