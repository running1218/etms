<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true" CodeFile="BulletinInfo.aspx.cs" Inherits="Information_AfficheManager_BulletinInfo" ValidateRequest="false" %>
<%@ Register Src="~/Information/AfficheManager/Controls/BulletinInfo.ascx" TagPrefix="uc1" TagName="BulletinInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <uc1:BulletinInfo ID="BulletinInfo" runat="server" />
</asp:Content>

