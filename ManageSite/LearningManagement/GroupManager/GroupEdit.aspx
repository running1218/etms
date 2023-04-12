<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true" CodeFile="GroupEdit.aspx.cs"   ValidateRequest="false" Inherits="LearningManagement_GroupManager_GroupEdit" %>

<%@ Register Src="Controls/GroupInfo.ascx" TagName="GroupInfo" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:GroupInfo ID="GroupInfo1" runat="server" />
</asp:Content>
