<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true" CodeFile="AddAppraisal.aspx.cs" Inherits="Activity_AddAppraisal" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>

<%@ Register Src="Controls/AppraisalInfo.ascx" TagName="Appraisal" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc2:Appraisal ID="appraisal" runat="server" OperationAction="Add"/>
</asp:Content>

