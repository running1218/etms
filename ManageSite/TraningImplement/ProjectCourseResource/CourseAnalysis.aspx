<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true" CodeFile="CourseAnalysis.aspx.cs" Inherits="TraningImplement_ProjectCourseResource_CourseAnalysis" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="dv_information">
        <h1 style="color:#666; padding:0px 0px 5px 5px;"><asp:Literal ID="ltlTitle" runat="server"></asp:Literal></h1>
        <table class="GridviewGray fixedTable">
            <tr>
                <th class="center">学习总人数</th>
                <th class="center">已完成</th>
                <th class="center">进行中</th>
                <th class="center">未学习</th>
                <th class="center">学习内容完成度</th>
                <th class="center">测评完成度</th>
                <th class="center">课程完成度</th>
            </tr>
            <tr>
                <td class="center"><asp:Literal ID="ltlNum1" runat="server"></asp:Literal></td>
                <td class="center"><asp:Literal ID="ltlNum2" runat="server"></asp:Literal></td>
                <td class="center"><asp:Literal ID="ltlNum3" runat="server"></asp:Literal></td>
                <td class="center"><asp:Literal ID="ltlNum4" runat="server"></asp:Literal></td>
                <td class="center"><asp:Literal ID="ltlNum5" runat="server"></asp:Literal></td>
                <td class="center"><asp:Literal ID="ltlNum6" runat="server"></asp:Literal></td>
                <td class="center"><asp:Literal ID="ltlNum7" runat="server"></asp:Literal></td>
            </tr>
        </table>
    </div>
</asp:Content>

