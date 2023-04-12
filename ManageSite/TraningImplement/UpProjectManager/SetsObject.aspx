<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="SetsObject.aspx.cs" Inherits="TraningImplement_UpProjectManager_SetsObject" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <!--功能标题-->
        <h2 class="dv_title">
            设置对象
        </h2>
        <div class="">
            <table width="98%" border="0" cellspacing="0" cellpadding="0" class="GridviewGray">
                <tr>
                    <th>
                        项目名称
                    </th>
                    <td colspan="3">
                        android应用开发实践
                    </td>
                </tr>
                <tr>
                    <th>
                        专业类别
                    </th>
                    <td colspan="3">
                        管理
                    </td>
                </tr>
                <tr>
                    <th>
                        项目周期
                    </th>
                    <td colspan="3">
                        2012/2/26 至 2012/03/31
                    </td>
                </tr>
                <tr>
                    <th>
                        培训对象说明
                    </th>
                    <td colspan="3">
                        培训对象说明培训对象说明培训对象说明培训对象说明
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltlDepartment" runat="server" Text="<%$ Resources:UIResource, ui_department%>"></asp:Literal>设置
                    </th>
                    <td>
                        <textarea class="inputbox_area190"></textarea>
                    </td>
                    <td align="center">
                        <a href="#">>> </a>
                        <br />
                        <a href="#">> </a>
                        <br />
                        <a href="#">< </a>
                        <br />
                        <a href="#"><< </a>
                    </td>
                    <td>
                        <textarea class="inputbox_area190"></textarea>
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltlRank" runat="server" Text="<%$ Resources:UIResource, ui_rank%>"></asp:Literal>设置
                    </th>
                    <td>
                        <textarea class="inputbox_area190"></textarea>
                    </td>
                    <td align="center">
                        <a href="#">>> </a>
                        <br />
                        <a href="#">> </a>
                        <br />
                        <a href="#">< </a>
                        <br />
                        <a href="#"><< </a>
                    </td>
                    <td>
                        <textarea class="inputbox_area190"></textarea>
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltlPosition" runat="server" Text="<%$ Resources:UIResource, ui_position%>"></asp:Literal>设置
                    </th>
                    <td>
                        <textarea class="inputbox_area190"></textarea>
                    </td>
                    <td align="center">
                        <a href="#">>> </a>
                        <br />
                        <a href="#">> </a>
                        <br />
                        <a href="#">< </a>
                        <br />
                        <a href="#"><< </a>
                    </td>
                    <td>
                        <textarea class="inputbox_area190"></textarea>
                    </td>
                </tr>
            </table>
        </div>
        <div class="center">
            <a href="#" class="btn_Save" onclick="javascript:popSuccessMsg('恭喜你保存成功！','提示',closeWindow);">
                保存</a><a href="javascript:closeWindow();" class="btn_Close">关闭</a></div>
    </div>
</asp:Content>
