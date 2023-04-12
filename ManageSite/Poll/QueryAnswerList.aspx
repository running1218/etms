<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QueryAnswerList.aspx.cs" Inherits="Poll_ResourceQuery_QueryAnswerList"
    MasterPageFile="~/MasterPages/MPageTree.Master" Title="µ÷²éÎÊ¾í" %>

<asp:Content runat="server" ID="PageContent" ContentPlaceHolderID="ContentPlaceHolder1">    
     <a href='<%=this.ActionHref(string.Format("QueryAnswer.aspx?QueryID={0}&ResourceType=R2&ResourceCode=00000000-0000-0000-0000-000000000002",6))%>' >´ð¾í</a>
</asp:Content>
