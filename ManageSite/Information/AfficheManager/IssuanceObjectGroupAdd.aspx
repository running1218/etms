<%@ Page Title="发布对象按群组添加" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master" AutoEventWireup="true" CodeFile="IssuanceObjectGroupAdd.aspx.cs" Inherits="Information_AfficheManager_IssuanceObjectGroupAdd" %>

<%@ Register Src="~/Information/AfficheManager/Controls/IssuanceObjectGroup.ascx"
    TagName="IssuanceObjectGroup" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--功能标题-->
    <h2 class="dv_title">
        公告管理
    </h2>
    <uc1:IssuanceObjectGroup ID="IssuanceObjectGroup1" Op="add" runat="server" />
</asp:Content>

