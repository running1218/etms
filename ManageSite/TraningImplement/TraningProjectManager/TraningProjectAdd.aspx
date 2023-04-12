<%@ Page Title=" 新增培训项目" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master"
    AutoEventWireup="true" CodeFile="TraningProjectAdd.aspx.cs" Inherits="TraningImplement_TraningProjectManager_TraningProjectAdd" %>

<%@ Register src="Controls/TraningProjectInfo.ascx" tagname="TraningProjectInfo" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--表单录入-->
    <uc1:TraningProjectInfo ID="TraningProjectInfo1" runat="server" Action="Add" />
</asp:Content>
