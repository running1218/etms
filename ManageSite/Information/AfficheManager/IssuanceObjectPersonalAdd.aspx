<%@ Page Title="发布对象按个人添加" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master" AutoEventWireup="true" CodeFile="IssuanceObjectPersonalAdd.aspx.cs" Inherits="Information_AfficheManager_IssuanceObjectPersonalAdd" %>

<%@ Register Src="~/Information/AfficheManager/Controls/IssuanceObjectPersonal.ascx"
    TagName="IssuanceObjectPersonal" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--功能标题-->
    <h2 class="dv_title">
        公告管理
    </h2>
    <uc1:IssuanceObjectPersonal ID="IssuanceObjectPersonal1" Op="add" runat="server" />
</asp:Content>

