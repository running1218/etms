<%@ Page Title="课件基本信息" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master"
    AutoEventWireup="true" CodeFile="CoursewareView.aspx.cs" Inherits="ETMS.WebApp.Manage.Resource.CoursewareManage.CoursewareView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--功能标题-->
    <h2 class="dv_title">
        非SCORM标准
    </h2>
    <!--表单录入-->
    <div class="dv_information">
        <table class="GridviewGray">
            <tr>
                <th style="width:20%;">课程编码：</th>
                <td style="width:30%;">
                    <asp:Literal ID="ltlCourseCode" runat="server"></asp:Literal>
                </td>
                <th style="width:20%;">课程名称：</th>
                <td style="width:30%;">
                    <asp:Literal ID="ltlCourseName" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th>
                    课件名称：
                </th>
                <td colspan="3">
                    <asp:Literal ID="ltlCoursewareName" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th>
                    课件状态：
                </th>
                <td>
                    <asp:Literal ID="ltlCoursewareStatus" runat="server"></asp:Literal>
                </td>
                <th>
                    课件时长：
                </th>
                <td>
                    <asp:Literal ID="ltlShowHoures" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <%--<th>
                    课件地址：
                </th>
                <td>
                    <asp:Literal ID="ltlCoursewarePath" runat="server"></asp:Literal>
                </td>--%>
                <th>
                    课件来源：
                </th>
                <td colspan="3">
                    <asp:Literal ID="ltlCoursewareSource" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th style="vertical-align: top">
                   课件介绍：
                </th>
                <td colspan="3">
                    <asp:Literal ID="ltlRemark" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th>
                   创&nbsp;&nbsp;建&nbsp;&nbsp;者：
                </th>
                <td>
                    <asp:Literal ID="ltlCreateUser" runat="server"></asp:Literal>
                </td>
                <th>
                   创建时间：
                </th>
                <td>
                    <asp:Literal ID="ltlCreateTime" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th>
                   修&nbsp;&nbsp;改&nbsp;&nbsp;者：
                </th>
                <td>
                    <asp:Literal ID="ltlModifyUser" runat="server"></asp:Literal>
                </td>
                <th>
                   修改时间：
                </th>
                <td>
                    <asp:Literal ID="ltlModifyTime" runat="server"></asp:Literal>
                </td>
            </tr>
        </table>
    </div>
    <!--关闭-->
    <div class="dv_submit">
        <a href="javascript:closeWindow();" class="btn_Close">关闭</a>
    </div>
</asp:Content>
