<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master" AutoEventWireup="true" CodeFile="QuestionList.aspx.cs" Inherits="QuestionDB_QuQuestionAndAnswer_QuestionList" %>
<%@ Register src="../Controls/QuestionList.ascx" tagname="QuestionList" tagprefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBack" Runat="Server">
 <a  href="<%= ETMS.Utility.WebUtility.AppPath %>/QuestionDB/ExamSingleSelection.aspx" class="btn_Return" title="返回">返回</a>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:QuestionList ID="QuestionList1" runat="server" />
</asp:Content>

