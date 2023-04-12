<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true"
    CodeFile="QuFillBlanksView.aspx.cs" Inherits="Questionnaire_QuestionManage_QuFillBlanksView" %>

<%@ Register Src="Controls/QuFillBlanksInfo.ascx" TagName="QuFillBlanksInfo" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:QuFillBlanksInfo ID="QuFillBlanksInfo1" runat="server" />
</asp:Content>
