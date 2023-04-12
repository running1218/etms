<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QuSelectionAdd.aspx.cs" MasterPageFile="~/MasterPages/MPagePop.Master"
    Inherits="QS_QuSelectionAdd" %>

<%@ Register Src="Controls/QuSelectionEdit.ascx" TagName="QuSelectionEdit" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:QuSelectionEdit ID="QuSelectionEdit1" runat="server" />
</asp:Content>
