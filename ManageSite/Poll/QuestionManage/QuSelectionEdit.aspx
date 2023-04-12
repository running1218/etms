<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true"
    CodeFile="QuSelectionEdit.aspx.cs" Inherits="Questionnaire_QuestionManage_QuSingleSelectionEdit" %>

<%@ Register Src="Controls/QuSelectionEdit.ascx" TagName="QuSelectionEdit" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:QuSelectionEdit ID="QuSelectionEdit1" runat="server" />
</asp:Content>
