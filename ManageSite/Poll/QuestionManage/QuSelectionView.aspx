<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true"
    CodeFile="QuSelectionView.aspx.cs" Inherits="Questionnaire_QuestionManage_QuSingleSelectionView" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="Controls/QuSelectionInfo.ascx" TagName="QuSelectionInfo" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:QuSelectionInfo ID="QuSelectionInfo1" runat="server" />
</asp:Content>
