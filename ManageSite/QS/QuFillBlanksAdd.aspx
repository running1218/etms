<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/MPagePop.Master"
    CodeFile="QuFillBlanksAdd.aspx.cs" Inherits="QS_QuFillBlanksAdd" %>

<%@ Register Src="~/QS/Controls/QuFillBlanksEdit.ascx" TagName="QuFillBlanksEdit"
    TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:QuFillBlanksEdit ID="QuFillBlanksEdit2" runat="server" />
</asp:Content>
