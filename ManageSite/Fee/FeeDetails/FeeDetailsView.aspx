<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true"
    CodeFile="FeeDetailsView.aspx.cs" Inherits="Fee_FeeDetails_FeeDetailsView" %>
    <%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="dv_information">
        <table class="GridviewGray">
            <tr>
                <th style="width:20%">
                    流 水 号：
                </th>
                <td style="width:80%">
                    <asp:Literal ID="ltlFeeCostDetailNo" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th>
                    项目名称：
                </th>
                <td>
                    <asp:Literal ID="ltlItemName" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th>
                    流水名称：
                </th>
                <td>
                    <asp:Literal ID="ltlFeeName" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th>
                    金&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;额：
                </th>
                <td>
                    <asp:Literal ID="ltlAmount" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th>
                    经 手 人：
                </th>
                <td>
                    <asp:Literal ID="ltlHandler" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th>
                    发生日期：
                </th>
                <td>
                    <asp:Literal ID="ltlCostDate" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th>
                    用&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;途：
                </th>
                <td>
                    <asp:Literal ID="ltlPurpose" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th>
                    PR 单 号：
                </th>
                <td>
                    <asp:Literal ID="ltlPRNo" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th>
                   发票是否拿到：
                </th>
                <td>
                    <cc1:DictionaryLabel ID="ltlIsGetInvoice" DictionaryType="Dic_TrueOrFalseBool"   runat="server" />
                </td>
            </tr>
            <tr>
                <th>
                    报销日期：
                </th>
                <td>
                    <asp:Literal ID="ltlReimbursementDate" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th>
                    备&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;注：
                </th>
                <td>
                    <asp:Literal ID="ltlRemark" runat="server"></asp:Literal>
                </td>
            </tr>
        </table>
    </div>
    <div class="dv_submit">
        <input type="button" value="关闭" onclick="javascript:closeWindow();" class="btn_Close">
    </div>
</asp:Content>
