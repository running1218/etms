<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Default.Master" AutoEventWireup="true"
    CodeFile="FileUpload.aspx.cs" Inherits="Example_FileUpload" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table>
        <tr>
            <th>
                用户名：
            </th>
            <td>
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>
                用户头像：
            </th>
            <td>
                <asp:Image ID="Image1" runat="server" />
                <cc1:FileUpload runat="server" ID="fileUpload1" FunctionType="Editor" CallBack="doCallBack" />
                <script language="javascript">
                    function doCallBack(imgName, imgUrl) {
                        document.getElementById('<%=Image1.ClientID %>').src = imgUrl;
                    }
                </script>
            </td>
        </tr>
        <tr>
            <th>
            </th>
            <td>
                <asp:Button ID="Button1" runat="server" Text="保存表单信息" OnClick="Button1_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
