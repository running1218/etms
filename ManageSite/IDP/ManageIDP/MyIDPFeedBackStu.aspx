<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master" AutoEventWireup="true" CodeFile="MyIDPFeedBackStu.aspx.cs" Inherits="IDP_ManageIDP_MyIDPFeedBackStu" %>

<%@ Register src="~/IDP/ManageIDP/Contorls/MyIDPFeedBackStu.ascx" tagname="MyIDPFeedBackStu" tagprefix="uc1" %>

<%@ Register src="Contorls/StudentFeedBackInfoShow.ascx" tagname="StudentFeedBackInfoShow" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBack" Runat="Server">
 <a  href="<%=GetURL() %>" class="btn_Return" title="返回">返回</a>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <uc2:StudentFeedBackInfoShow ID="StudentFeedBackInfoShow1" runat="server" />
    <div class="dv_searchlist">
    <uc1:MyIDPFeedBackStu ID="MyIDPFeedBackStu1" runat="server" />
    </div>
</asp:Content>

