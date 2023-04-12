<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true"
    CodeFile="CoursePeriodResultEdit.aspx.cs" Inherits="TraningImplement_ProjectCoursePeriodResult_CoursePeriodResultEdit" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--表单录入-->
    <div class="dv_information">
        <table class="GridviewGray">
            <tr>
                <th>
                    项目名称：
                </th>
                <td colspan="3">
                    <asp:Label ID="Lbl_ItemName" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    课程名称：
                </th>
                <td colspan="3">
                    <asp:Label ID="Lbl_CourseName" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <th width="20%">
                    讲师姓名：
                </th>
                <td colspan="3">
                    <asp:Label ID="lbl_TeacherName" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    培训时间：
                </th>
                <td colspan="3">
                    <asp:Label ID="lblTraningDate" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    培训时间说明：
                </th>
                <td colspan="3">
                    <cc1:DictionaryLabel ID="dlblTrainingTimeDesc" DictionaryType="Dic_Sys_TrainingTimeDesc"
                        runat="server" />
                </td>
            </tr>
            <tr>
                <th>
                    课时状态：
                </th>
                <td colspan="3">
                    <cc1:DictionaryDropDownList runat="server" ID="ddl_CourseHoursStatus" DictionaryType="Dic_Sys_CourseHoursStatus"
                        IsShowChoose="true" />
                    <font color="red">*</font><asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                        ValidationExpression="\d[0,*]" Text="*" Display="None" runat="server" ErrorMessage="请选择课时状态！"
                        ControlToValidate="ddl_CourseHoursStatus" ValidationGroup="Saves"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th>
                    执行说明：
                </th>
                <td colspan="3">
                    <asp:TextBox ID="txtCourseHoursStatusDesc" TextMode="MultiLine" runat="server" CssClass="inputbox_area300"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    <div class="dv_submit">
        <asp:Button ID="btn_Save" CssClass="btn_Save" runat="server" Text="保存" OnClick="btn_Save_Click"
            ValidationGroup="Saves"></asp:Button>
        <asp:ValidationSummary runat="server" ID="ValidationSummary1" ValidationGroup="Saves"
            ShowMessageBox="true" ShowSummary="false" />
        <input type="button" class="btn_Cancel" value="取消" onclick="javascript:closeWindow();" /></div>
</asp:Content>
