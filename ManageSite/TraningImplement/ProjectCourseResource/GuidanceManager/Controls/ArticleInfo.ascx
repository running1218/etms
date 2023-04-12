<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ArticleInfo.ascx.cs" Inherits="QuestionDB_GuidanceManager_Controls_ArticleInfo" %>
<%@ Register Assembly="ETMS.Editor" Namespace="ETMS.Editor.UEditor" TagPrefix="wuc" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<!--功能标题-->
<h2 class="dv_title">
    课程公告管理
</h2>
<div class="dv_information">
    <table class="GridviewGray fixedTable">
        <tr>
            <th>
                标题：
            </th>
            <td colspan="3">
                <asp:TextBox ID="txtMainHead" runat="server" CssClass="inputbox_210" MaxLength="50"></asp:TextBox>
                <span style="color: Red;">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidatorJobName"
                    runat="server" ErrorMessage="请填标题！" ControlToValidate="txtMainHead" ValidationGroup="Error"
                    Display="None"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th>
                状&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;态：
            </th>
            <td colspan="3">
                <asp:RadioButtonList ID="rblStatus" runat="server" RepeatDirection="Horizontal" CssClass="noborder">
                    <asp:ListItem Value="1" Selected="True">启用</asp:ListItem>
                    <asp:ListItem Value="0">停用</asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <th style="display:none;">
                关&nbsp;&nbsp;键&nbsp;&nbsp;字：
            </th>
            <td style="display:none;">
                <asp:TextBox ID="txtKeyword" runat="server" CssClass="inputbox_90" MaxLength="50" />
            </td>
        </tr>
        <tr style="display:none;">
            <th>
                资料简介：
            </th>
            <td colspan="3">
                <asp:TextBox ID="txtBrief" runat="server" CssClass="inputbox_300" MaxLength="100"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>
                内&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;容：
            </th>
            <td colspan="3">
                <wuc:UEditor ID="fckEditor" runat="server" Width="100%" Height="280" ToolType="Basic">
                </wuc:UEditor>
            </td>
        </tr>
    </table>
</div>
<div class="dv_submit">
    <asp:LinkButton ID="lbnSave" runat="server" CssClass="btn_Save" OnClick="lbnSave_Click"
        ValidationGroup="Error">保存</asp:LinkButton>
    <asp:ValidationSummary runat="server" ID="ValidationSummary1" ValidationGroup="Error"
        ShowMessageBox="true" ShowSummary="false" />
    <a href="javascript:closeWindow();" class="btn_Cancel padleft10">取消</a>
</div>
