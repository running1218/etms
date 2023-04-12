<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/MPagePop.Master" CodeFile="QuFillBlanksView.aspx.cs" Inherits="QS_QuFillBlanksView" %>

<%@ Register Src="~/QS/Controls/QuFillBlanksInfo.ascx" TagName="QuFillBlanksInfo" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:QuFillBlanksInfo ID="QuFillBlanksInfo1" runat="server" />
</asp:Content>

