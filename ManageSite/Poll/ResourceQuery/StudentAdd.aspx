<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StudentAdd.aspx.cs" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    Inherits="Poll_ResourceQuery_StudentAdd" %>

<asp:Content runat="server" ContentPlaceHolderID="cphBack" ID="Content2">
    <%--<a class="btn_Return" onclick='window.location ="<%=this.ActionHref(string.Format("SetArea_R2.aspx?QueryID={0}",this.PublishObject.QueryID))%>"'>
        返回</a>--%>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
        <asp:ListItem>AAA</asp:ListItem>
        <asp:ListItem>BBB</asp:ListItem>
        <asp:ListItem>CCC</asp:ListItem>
    </asp:RadioButtonList>
    <a href="javascript:hideGridview()" class="dropdownico" id="Highsearch">高级搜索</a>
</asp:Content>
