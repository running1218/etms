<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true"
    CodeFile="QuFillBlanksAdd.aspx.cs" Inherits="Questionnaire_QuestionManage_QuFillBlanksAdd" %>

<%@ Register Src="Controls/QuFillBlanksEdit.ascx" TagName="QuFillBlanksEdit" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:QuFillBlanksEdit ID="QuFillBlanksEdit2" runat="server" />
</asp:Content>
