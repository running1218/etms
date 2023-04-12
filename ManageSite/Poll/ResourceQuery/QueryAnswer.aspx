<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/MPageTree.Master"
    Title="调查问卷结果-查看" CodeFile="QueryAnswer.aspx.cs" Inherits="Poll_ResourceQuery_QueryAnswer" %>

<asp:Content runat="server" ID="PageContent" ContentPlaceHolderID="ContentPlaceHolder1">
    <script type="text/javascript">
        $(function () {
            $(".surveyUl").find("li:gt(0)").css("padding-left", "15px");
            $("#divButton").hide();
        })
    </script>
    <div id="isShow" runat="server" visible="false">
    </div>
    <asp:Literal runat="server" ID="ltContent" />
</asp:Content>
 