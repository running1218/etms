<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QueryPreView.aspx.cs" Inherits="Poll_ResourceQuery_QueryPreView"
    MasterPageFile="~/MasterPages/MPageTree.Master" Title="µ÷²éÎÊ¾í-Ô¤ÀÀ" %>

<asp:Content runat="server" ID="PageContent" ContentPlaceHolderID="ContentPlaceHolder1">    
    <script>
        $(function () {
            $(".surveyUl").find("li:gt(0)").css("padding-left", "15px");
        })
    </script>
    <asp:Literal runat="server" ID="ltContent" />
</asp:Content>
