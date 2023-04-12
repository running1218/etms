<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master" AutoEventWireup="true" CodeFile="GradeEntry.aspx.cs" Inherits="Grade_GradeManage_GradeEntry" %>

<%@ Register src="Controls/GradeEntry.ascx" tagname="GradeEntry" tagprefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBack" Runat="Server">
        <a id="aBack" runat="server" class="btn_Return">返回</a>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 
    <uc1:GradeEntry ID="GradeEntry1" runat="server" />
 
</asp:Content>

