<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QueryStatResult.aspx.cs"
    Inherits="Poll_ResourceQuery_QueryStatResult" MasterPageFile="~/MasterPages/MPageTree.Master"
    Title="�����ʾ���-�鿴" %>

<asp:Content runat="server" ID="PageContent" ContentPlaceHolderID="ContentPlaceHolder1">
    <script>
        $(function () {
            $(".surveyUl").find("li:gt(0)").css("padding-left", "15px");
        })
    </script>
    <asp:Literal runat="server" ID="ltContent" />
</asp:Content>
