<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NotifyClassList.aspx.cs"
    Inherits="Security_SysConfig_NotifyClassList" MasterPageFile="~/MasterPages/MPagePop.Master" %>

<%@ Register Src="../../Dictionary/UserControl.ascx" TagName="UserControl" TagPrefix="uc1" %>
<asp:Content runat="server" ID="PageContent" ContentPlaceHolderID="ContentPlaceHolder1">
    <uc1:UserControl ID="UserControl1" runat="server" DicType="Notify_MessageClass" />
</asp:Content>
