<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master" AutoEventWireup="true" CodeFile="ExamMultipleChoice.aspx.cs" Inherits="QuestionDB_ExamMultipleChoice" %>

<%@ Register src="Controls/ExamQuestionBank.ascx" tagname="ExamQuestionBank" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBack" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <uc1:ExamQuestionBank ID="ExamQuestionBank1" runat="server" />

</asp:Content>
