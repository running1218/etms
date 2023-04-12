<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true"
    CodeFile="FCKEditor.aspx.cs" Inherits="FCKEditor" ValidateRequest="false" %>

<%@ Register Assembly="ETMS.Editor" Namespace="ETMS.Editor.UEditor" TagPrefix="wuc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server"> 
    <table width="80%">
        <tr>
            <th>
                标准编辑器
            </th>
            <td>
                <wuc:UEditor runat="server" ID="FCKeditor1" ToolType="Basic" Width="700"
                    Height="300" ></wuc:UEditor>
            </td>
        </tr>
        <tr>
            <th>
                基本编辑器
            </th>
            <td>
                <wuc:UEditor runat="server" ID="FCKeditor2" ToolType="Basic" Width="700"
                    Height="300" ></wuc:UEditor>
            </td>
        </tr>
        <tr>
            <th>
            </th>
            <td>
                <asp:Button runat="server" Text="test" OnClick="Unnamed1_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
