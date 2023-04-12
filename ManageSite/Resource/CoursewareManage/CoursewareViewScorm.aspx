<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true" CodeFile="CoursewareViewScorm.aspx.cs" Inherits="Resource_CoursewareManage_CoursewareViewScorm" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<!--功能标题-->
    <h2 class="dv_title">
        SCORM标准
    </h2>
    <!--表单录入-->
    <div class="dv_information">
         <table class="GridviewGray">
            <tr>
                <th style="width:15%;">课程编码：</th>
                <td style="width:35%;">
                    <asp:Literal ID="ltlCourseCode" runat="server"></asp:Literal>                    
                </td>
                <th style="width:15%;">课程名称：</th>
                <td style="width:35%;">
                    <cc1:ShortTextLabel ID="lblCourseName" runat="server" ShowTextNum="20"></cc1:ShortTextLabel>
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
                <th>
                    课件地址：
                </th>
                <td>
                    <asp:Literal ID="ltlCoursewarePath" runat="server"></asp:Literal>
                </td>
                <th>
                    课件来源：
                </th>
                <td>
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
                   创 建 者：
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
                   修 改 者：
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

