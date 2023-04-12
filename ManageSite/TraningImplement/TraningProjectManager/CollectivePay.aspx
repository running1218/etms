<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/MPagePop.Master"
    CodeFile="CollectivePay.aspx.cs" Inherits="TraningImplement_TraningProjectManager_CollectivePay" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--表单录入-->
    <div class="dv_information">
        <table class="GridviewGray">
            <tr>
                <th>
                    项目编码：
                </th>
                <td colspan="3">
                    <asp:Label ID="lbl_ItemCode" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    项目名称：
                </th>
                <td colspan="3">
                    <asp:Label ID="lbl_ItemName" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    付费方：
                </th>
                <td colspan="3">
                    <asp:RadioButtonList runat="server" ID="Dic_PayMode" RepeatLayout="Flow" RepeatDirection="Horizontal">
                        <asp:ListItem Text="集体" Value="1" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="个人" Value="2"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <th>
                    收费标准（元/人）：
                </th>
                <td colspan="3">
                    <cc1:CustomTextBox ID="ctbPayMoney" runat="server" Text='<%# string.Format("{0:N2}",Eval("BudgetFee").ToString().ToDecimal())  %>'
                        CssClass="inputbox_60" ContentType="Decimal" MaxLength="6"></cc1:CustomTextBox>
                </td>
            </tr>
        </table>
    </div>
    <div class="dv_submit">
        <asp:Button ID="Btn_Save" CssClass="btn_Save" runat="server" Text="保存" OnClick="Btn_Save_Click"
            ValidationGroup="Saves" />
        <input type="button" class="btn_Cancel" value="取消" onclick="javascript:closeWindow();" /></div>
</asp:Content>
