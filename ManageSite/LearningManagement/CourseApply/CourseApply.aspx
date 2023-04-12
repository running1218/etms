<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true"
    CodeFile="CourseApply.aspx.cs" Inherits="LearningManagement_CourseApply_CourseApply" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--功能标题-->
    <h2 class="dv_title">
        课程申请审核
    </h2>
    <!--表单录入-->
    <div class="dv_information">
        <table class="GridviewGray">
            <tr>
                <th>
                    学员姓名：
                </th>
                <td>
                    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                </td>
                <th>
                    所属<asp:Literal ID="ltlDepartment" runat="server" Text="<%$ Resources:UIResource, ui_department%>"></asp:Literal>：
                </th>
                <td>
                    <asp:Literal ID="Literal2" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th>
                    岗&nbsp;&nbsp;&nbsp;&nbsp;位：
                </th>
                <td>
                    <asp:Literal ID="Literal3" runat="server"></asp:Literal>
                </td>
                <th>
                    <asp:Literal ID="ltlRank" runat="server" Text="<%$ Resources:UIResource, ui_rank%>"></asp:Literal>：
                </th>
                <td>
                    <asp:Literal ID="Literal4" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th>
                    项目名称：
                </th>
                <td>
                    <asp:Literal ID="Literal5" runat="server"></asp:Literal>
                </td>
                <th>
                    申请课程：
                </th>
                <td>
                    <asp:Literal ID="Literal6" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th>
                    申请时间：
                </th>
                <td>
                    <asp:Literal ID="Literal7" runat="server"></asp:Literal>
                </td>
                <th>
                    培训方式：
                </th>
                <td>
                    <asp:Literal ID="Literal8" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th>
                    最大人数限制：
                </th>
                <td>
                    <asp:Literal ID="Literal9" runat="server"></asp:Literal>
                </td>
                <th>
                    已申请人数：
                </th>
                <td>
                    <asp:Literal ID="Literal10" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th>
                    审核通过人数：
                </th>
                <td>
                    <asp:Literal ID="Literal11" runat="server"></asp:Literal>
                </td>
                <th>
                    审核状态：
                </th>
                <td>
                    <asp:Literal ID="Literal12" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th>
                    审核意见：
                </th>
                <td colspan="3">
                    <textarea class="inputbox_area440"></textarea>
                </td>
            </tr>
        </table>
    </div>
    <!--提交表单-->
    <div class="dv_submit">
        <asp:LinkButton ID="LinkButton1" runat="server" CssClass=" btn_Agree" OnClick="LinkButton1_Click">审核通过</asp:LinkButton>
        <asp:LinkButton ID="LinkButton2" runat="server" CssClass=" btn_Deny" OnClick="LinkButton2_Click">审核不通过</asp:LinkButton>
        <a href="javascript:closeWindow();" class="btn_Close">取消</a>
    </div>
</asp:Content>
