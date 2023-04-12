<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QueryPreView.aspx.cs" Inherits="QS_QueryPreView" MasterPageFile="~/MasterPages/MPageTree.Master" %>

<asp:Content runat="server" ID="PageContent" ContentPlaceHolderID="ContentPlaceHolder1">
    <div style="width: 61%; margin:0 auto;" >
        <table id="tableQuery" runat="server" border="1" cellpadding="1" cellspacing="100" width="90%">
            <tr>
                <td width="1%" bgcolor="#f1e3ff" align="center">
                    <asp:Label ID="lblQueryTitle" runat="server" Visible="True" Font-Bold="True">问卷标题</asp:Label>
                    <br />
                    <asp:Label ID="lblQueryTime" runat="server" Text="问卷时间">调查开始时间:无,调查结束时间:无</asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Table ID="tableQueryTitle" runat="server" GridLines="Horizontal" Width="100%" EnableViewState="false">
                    </asp:Table>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="btnSave" runat="server" Text="提交" Enabled="true" OnClick="btnSave_Click"></asp:Button>
                    <asp:Button ID="btnResult" runat="server" Text="查看结果" Enabled="true" OnClick="btnResult_Click"></asp:Button>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
