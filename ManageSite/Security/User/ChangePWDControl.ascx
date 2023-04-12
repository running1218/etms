<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ChangePWDControl.ascx.cs"
    Inherits="Admin_Site_User_ChangePWDControl" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<cc1:CustomMuliView runat="server" ID="Views">
    <asp:View runat="server" ID="View_Edit">
        <table class="GridviewGray">
            <tr>
                <th>
                    ԭ���룺
                </th>
                <td>
                    <asp:TextBox ID="txtOldPassWord" runat="server" TextMode="Password"></asp:TextBox>
                    <span style="color: Red;">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidatorPassWord"
                        runat="server" ErrorMessage="����дԭ���룡" ControlToValidate="txtOldPassWord" ValidationGroup="EditPassword"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th>
                    �����룺
                </th>
                <td>
                    <asp:TextBox ID="txtPassWord" runat="server" TextMode="Password"></asp:TextBox>
                    <span style="color: Red;">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                        runat="server" ErrorMessage="����д�����룡" Display="none" ControlToValidate="txtPassWord" ValidationGroup="EditPassword"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th>
                    ȷ�����룺
                </th>
                <td>
                    <asp:TextBox ID="txtPassWord1" runat="server" TextMode="Password"></asp:TextBox>
                    <span style="color: Red;">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                        runat="server" ErrorMessage="����дȷ�����룡" Display="none" ControlToValidate="txtPassWord1" ValidationGroup="EditPassword"></asp:RequiredFieldValidator>
                    <asp:CompareValidator runat="server" ID="CompareValidator1" ControlToValidate="txtPassWord1"
                        Text="*" ErrorMessage="*������������д��һ��" Display="none" ValidationGroup="EditPassword"
                        ControlToCompare="txtPassWord"></asp:CompareValidator>
                </td>
            </tr>
        </table>
        <asp:ValidationSummary runat="server" ID="validationSummary1" ValidationGroup="EditPassword"
            ShowMessageBox="true" ShowSummary="false" />
    </asp:View>
</cc1:CustomMuliView>
