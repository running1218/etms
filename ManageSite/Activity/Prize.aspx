<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true" CodeFile="Prize.aspx.cs" Inherits="Activity_Prize" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="dv_information">
        <table class="GridviewGray">
            <tr>
                <th>
                    报名编号：
                </th>
                <td>
                    <asp:Label ID="lblSiginNo" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    姓名：
                </th>
                <td>
                    <asp:Label ID="lblName" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    作品名称：
                </th>
                <td>
                    <asp:Label ID="lblProductionName" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    分数：
                </th>
                <td>
                    <asp:Label ID="lblScore" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    奖项：
                </th>
                <td>
                    <cc1:DictionaryDropDownList runat="server" ID="ddlPrize" DictionaryType="Activity_Dic_Prize"
                    IsShowChoose="false" IsShowAll="false" CssText="select_120" />
                </td>
            </tr>
        </table>
    </div>
    <div class="dv_submit">
        <asp:Button ID="Btn_Save" CssClass="btn_Save" runat="server" Text="保存" OnClick="Btn_Save_Click"
            ValidationGroup="Saves" />
        <asp:ValidationSummary runat="server" ID="ValidationSummary1" ValidationGroup="Saves"
            ShowMessageBox="true" ShowSummary="false" />
        <input type="button" class="btn_Cancel" value="取消" onclick="javascript:closeWindow();" />
    </div>
</asp:Content>

