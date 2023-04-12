<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true" CodeFile="TraningOrgView.aspx.cs" Inherits="TraningOrgManager_TraningOrgManager_TraningOrgView" %>

<%@ Register src="Controls/TraningOrgView.ascx" tagname="TraningOrgView" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <!--功能标题-->
    <h2 class="dv_title">
        查看培训机构
    </h2>
    <uc1:TraningOrgView ID="TraningOrgView1" runat="server" />
</asp:Content>

