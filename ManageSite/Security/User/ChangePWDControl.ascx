<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ChangePWDControl.ascx.cs"
    Inherits="Admin_Site_User_ChangePWDControl" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<cc1:CustomMuliView runat="server" ID="Views">
    <asp:View runat="server" ID="View_Edit">
        <table class="GridviewGray">
            <tr>
                <th>
                    ‘≠√‹¬Î£∫
                </th>
                <td>
                    <asp:TextBox ID="txtOldPassWord" runat="server" TextMode="Password"></asp:TextBox>
                    <span style="color: Red;">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidatorPassWord"
                        runat="server" ErrorMessage="«ÎÃÓ–¥‘≠√‹¬Î£°" ControlToValidate="txtOldPassWord" ValidationGroup="EditPassword"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th>
                    –¬√‹¬Î£∫
                </th>
                <td>
                    <asp:TextBox ID="txtPassWord" runat="server" TextMode="Password"></asp:TextBox>
                    <span style="color: Red;">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                        runat="server" ErrorMessage="«ÎÃÓ–¥–¬√‹¬Î£°" Display="none" ControlToValidate="txtPassWord" ValidationGroup="EditPassword"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th>
                    »∑»œ√‹¬Î£∫
                </th>
                <td>
                    <asp:TextBox ID="txtPassWord1" runat="server" TextMode="Password"></asp:TextBox>
                    <span style="color: Red;">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                        runat="server" ErrorMessage="«ÎÃÓ–¥»∑»œ√‹¬Î£°" Display="none" ControlToValidate="txtPassWord1" ValidationGroup="EditPassword"></asp:RequiredFieldValidator>
                    <asp:CompareValidator runat="server" ID="CompareValidator1" ControlToValidate="txtPassWord1"
                        Text="*" ErrorMessage="*«Î¡Ω¥Œ√‹¬ÎÃÓ–¥≤ª“ª÷¬" Display="none" ValidationGroup="EditPassword"
                        ControlToCompare="txtPassWord"></asp:CompareValidator>
                </td>
            </tr>
        </table>
        <asp:ValidationSummary runat="server" ID="validationSummary1" ValidationGroup="EditPassword"
            ShowMessageBox="true" ShowSummary="false" />
    </asp:View>
</cc1:CustomMuliView>
