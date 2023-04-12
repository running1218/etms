<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserControl.ascx.cs" Inherits="Admin_Site_UserRole_UserControl" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<cc1:CustomMuliView runat="server" ID="Views">
    <asp:View runat="server" ID="View_Edit">
        <table style="width: 96%">
            <tr>
                <td colspan="3">
                    用户账户：
                    <asp:Label ID="lblLoginName" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <th style="text-align: center">
                    可分配角色
                </th>
                <td>
                </td>
                <th style="text-align: center">
                    已分配角色
                </th>
            </tr>
            <tr>
                <td style="width: 45%">
                    <asp:ListBox ID="lboxRoles" runat="server" SelectionMode="Multiple" Width="100%"
                        Height="280"></asp:ListBox>
                </td>
                <td style="width: 10%; text-align: center;">
                    <div><asp:Button ID="Button1" runat="server" Text="" CssClass="move_right" OnClick="Button1_Click" /></div>
                    <br />
                    
                    <div><asp:Button ID="Button2" runat="server" Text="" CssClass="move_left" OnClick="Button2_Click" /></div>
                </td>
                <td style="width: 45%">
                    <asp:ListBox ID="lboxSelectedRoles" runat="server" SelectionMode="Multiple" Width="100%"
                        Height="280"></asp:ListBox>
                </td>
            </tr>
        </table>
    </asp:View>
    <asp:View runat="server" ID="View_Browse">
        <table>
            <tr>
                <th>
                    用户账户：
                </th>
                <td colspan="3">
                    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <th valign="top">
                    已分配角色：
                </th>
                <td>
                    <asp:ListBox ID="ListBox1" runat="server" SelectionMode="Multiple" Width="250" Height="280">
                    </asp:ListBox>
                </td>
            </tr>
        </table>
    </asp:View>
</cc1:CustomMuliView>
