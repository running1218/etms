<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true" CodeFile="TraningOrgAdd.aspx.cs" Inherits="TraningOrgManager_TraningOrgManager_TraningOrgAdd" %>

<%@ Register src="Controls/TraningOrgInfo.ascx" tagname="TraningOrgInfo" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <!--功能标题-->
    <h2 class="dv_title">
        新增培训机构
    </h2>
    <uc1:TraningOrgInfo ID="TraningOrgInfo1" runat="server" />
</asp:Content>

