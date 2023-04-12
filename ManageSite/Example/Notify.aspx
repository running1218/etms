<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Default.Master" AutoEventWireup="true"
    CodeFile="Notify.aspx.cs" Inherits="Example_Notify" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table>
        <thead>
            重置密码-消息提醒（暂时发送邮件！）
        </thead>
        <tr>
            <th>
                *收件人：
            </th>
            <td>
                <asp:TextBox ID="txtReceiver" runat="server" Text="xueyb@mail.com.cn"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtReceiver"
                    runat="server" ErrorMessage="请输入邮箱地址" EnableTheming="True"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                        ID="RegularExpressionValidator1" runat="server" ErrorMessage="请输入正确邮箱地址！" ControlToValidate="txtReceiver"
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <th>
            </th>
            <td>
                <asp:Button ID="Button2" runat="server" Text="发送" OnClick="Button2_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
