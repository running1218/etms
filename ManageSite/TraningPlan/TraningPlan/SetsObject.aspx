<%@ Page Title="设置对象" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="SetsObject.aspx.cs" Inherits="ETMS.WebApp.Manage.TraningPlan.TraningPlan.SetsObject" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <!--功能标题-->
        <h2 class="dv_title">
            设置对象
        </h2>
        <div class="dv_information">
            <table class="GridviewGray">
                <tr>
                    <th>
                        计划名称：
                    </th>
                    <td colspan="3">
                        2012培训计划名称
                    </td>
                </tr>
                <tr>
                    <th>
                        计划类型：
                    </th>
                    <td colspan="3">
                        年度计划
                    </td>
                </tr>
                <tr>
                    <th>
                        本组织机构：
                    </th>
                    <td colspan="3">
                        组织机构一
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltlRank" runat="server" Text="<%$ Resources:UIResource, ui_rank%>"></asp:Literal>设置：
                    </th>
                    <td>
                        <textarea class="inputbox_area190"></textarea>
                    </td>
                    <td align="center">
                        <a href="#">>> </a>
                        <br />
                        <a href="#"><< </a>
                    </td>
                    <td>
                        <textarea class="inputbox_area190"></textarea>
                    </td>
                </tr>
                <tr>
                    <th>
                       <asp:Literal ID="ltlPosition" runat="server" Text="<%$ Resources:UIResource, ui_position%>"></asp:Literal>设置：
                    </th>
                    <td>
                        <textarea class="inputbox_area190"></textarea>
                    </td>
                    <td align="center">
                        <a href="#">>> </a>
                        <br />
                        <a href="#"><< </a>
                    </td>
                    <td>
                        <textarea class="inputbox_area190"></textarea>
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltlDepartment" runat="server" Text="<%$ Resources:UIResource, ui_department%>"></asp:Literal>设置：
                    </th>
                    <td>
                        <textarea class="inputbox_area190"></textarea>
                    </td>
                    <td align="center">
                        <a href="#">>> </a>
                        <br />
                        <a href="#"><< </a>
                    </td>
                    <td>
                        <textarea class="inputbox_area190"></textarea>
                    </td>
                </tr>
                <tr>
                    <th>
                        下级组织机构设置：
                    </th>
                    <td>
                        <textarea class="inputbox_area190"></textarea>
                    </td>
                    <td align="center">
                        <a href="#">>> </a>
                        <br />
                        <a href="#"><< </a>
                    </td>
                    <td>
                        <textarea class="inputbox_area190"></textarea>
                    </td>
                </tr>
            </table>
    <div class="center">
        <a href="#" class="btn_Save">保存</a><a href="javascript:closeWindow();" class="btn_Close">关闭</a></div>
        </div>
    </div>
</asp:Content>
