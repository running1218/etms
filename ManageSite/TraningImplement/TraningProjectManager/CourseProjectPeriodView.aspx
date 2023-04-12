<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="CourseProjectPeriodView.aspx.cs" Inherits="TraningImplement_TraningProjectManager_CourseProjectPeriodView" %>

<%@ Register Src="Controls/CourseProjectPeriodView.ascx" TagName="CourseProjectPeriodView"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
        <div class="">
        <table class="GridviewGray">
            <tr>
                <th>
                    项目编码：
                </th>
                <td>
                    XM1002369
                </td>
                <th>
                    项目名称：
                </th>
                <td>
                    飞鹰01
                </td>
            </tr>
            <tr>
                <th>
                    课程编码：
                </th>
                <td>
                    KC1203254
                </td>
                <th>
                    课程名称：
                </th>
                <td>
                    世界精品课程
                </td>
            </tr>
        </table>
    </div>
    <uc1:CourseProjectPeriodView ID="CourseProjectPeriodView1" runat="server" />
    <div class="dv_submit">
        <a href="javascript:closeWindow();" class="btn_Close">关闭</a></div>
</asp:Content>
