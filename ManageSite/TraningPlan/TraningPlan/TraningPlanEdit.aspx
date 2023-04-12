<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true" CodeFile="TraningPlanEdit.aspx.cs" Inherits="TraningPlan_TraningPlan_TraningPlanEdit" %>
<%@ Register Src="Controls/PlanInfo.ascx" TagName="PlanInfo" TagPrefix="uc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:PlanInfo ID="PlanInfo1" runat="server" Action="Edit"  />
</asp:Content>


