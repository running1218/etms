<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master" AutoEventWireup="true" CodeFile="QuestionList.aspx.cs" Inherits="QuestionDB_QuFillBlanks_QuestionList" %>

<%@ Register src="../Controls/QuestionList.ascx" tagname="QuestionList" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:QuestionList ID="QuestionList1" runat="server" />
</asp:Content>

