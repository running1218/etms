<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true" CodeFile="QuestionAdd.aspx.cs" Inherits="QuestionDB_QuMultipleChoice_QuestionAdd" ValidateRequest="false" %>

<%@ Register src="Controls/QuestionInfo.ascx" tagname="QuestionInfo" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:QuestionInfo ID="QuestionInfo1" runat="server" />
</asp:Content>

