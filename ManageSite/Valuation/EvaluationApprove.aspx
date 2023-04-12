<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true" CodeFile="EvaluationApprove.aspx.cs" Inherits="Valuation_EvaluationApprove" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="dv_information">
        <table class="GridviewGray">
            <tr>
                <th>姓&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;名：
                </th>
                <td>
                    <asp:Label ID="lab_RealName" runat="server"></asp:Label>
                </td>
                <th>点评时间：
                </th>
                <td>
                    <asp:Label ID="lab_CreateTime" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th style="height:50px">综合点评：
                </th>
                <td colspan="3">
                    <asp:Label ID="lab_Result" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th style="height:50px">点评内容：
                </th>
                <td colspan="3">
                    <asp:Label ID="lab_EvaluationContent" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>审批状态：
                </th>
                <td colspan="3">
                    <cc1:DictionaryRadioButtonList runat="server" ID="rblApproveStatus" DictionaryType="ApproveStatus" />
                </td>
            </tr>
        </table>
    </div>
    <div class="dv_submit">
        <asp:Button ID="Btn_Save" CssClass="btn_Save" runat="server" Text="保存" OnClick="Btn_Save_Click"/>
        <input type="button" class="btn_Cancel" value="取消" onclick="javascript: closeWindow();" />
    </div>
</asp:Content>

