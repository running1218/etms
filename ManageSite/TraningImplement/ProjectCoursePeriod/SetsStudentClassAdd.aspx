<%@ Page Title="按班级添加学员" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master"
    AutoEventWireup="true" CodeFile="SetsStudentClassAdd.aspx.cs" Inherits="TraningImplement_ProjectCoursePeriod_SetsStudentClassAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="dv_information">
        <table class="GridviewGray">
            <tr>
                <th width="25%">
                    项目名称：
                </th>
                <td>
                    <asp:Label ID="lblItemName" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    课程名称：
                </th>
                <td>
                    <asp:Label ID="lblCourseName" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    班级名称：
                </th>
                <td>
                    <asp:DropDownList ID="ddlClassName" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th>
                    课程学员数：
                </th>
                <td>
                    <asp:Label ID="lblCourseStudent" runat="server">5</asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div class="dv_submit">
        <asp:Button ID="Btn_Save" CssClass="btn_Save" runat="server" Text="保存" OnClick="Btn_Save_Click" /><input type="button" class="btn_Cancel" value="关闭" onclick="javascript:closeWindow();" /></div>
</asp:Content>
