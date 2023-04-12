<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DocConvert.aspx.cs" MasterPageFile="~/MasterPages/Default.Master"
    Inherits="Example_DocConvert" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table>
        <tr>
            <th>
                原始文档：
            </th>
            <td>
                <cc1:FileUpload runat="server" ID="fileUpload1" FunctionType="DocResource" />
            </td>
        </tr>
        <tr>
            <th>
            </th>
            <td>
                <asp:Button ID="Button1" runat="server" Text="转换" OnClick="Button1_Click" />
                <asp:HyperLink runat="server" Visible="false" ID="resourceUrl" Text="预览" />
            </td>
        </tr>
    </table>
</asp:Content>
