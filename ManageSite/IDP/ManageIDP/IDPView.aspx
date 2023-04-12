<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="IDPView.aspx.cs" Inherits="IDP_ManageIDP_IDPView" %>

<%@ Register Src="~/IDP/ManageIDP/Contorls/ManageContent.ascx" TagName="ManageContent"
    TagPrefix="uc1" %>
<%@ Register Src="~/IDP/ManageIDP/Contorls/ManageDevelopment.ascx" TagName="ManageDevelopment"
    TagPrefix="uc2" %>
<%@ Register Src="~/IDP/ManageIDP/Contorls/IDPInfo.ascx" TagName="IDPInfo" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBack" runat="Server">
<asp:LinkButton ID="lbnBack" runat="server" PostBackUrl="~/IDP/ManageIDP/IDPList.aspx" CssClass="btn_Return">返回</asp:LinkButton>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc3:IDPInfo ID="IDPInfo1" runat="server" />
    <div class="dv_serviceTab">
        <div class="dv_Tabmenus" >
            <ul>
                <li id="Tab_0" onclick="showTab('Tab_0', 'Div_Select_0','selected')"><a onfocus="blur()"
                    href="javascript:void(0);"><span class="bj">发展目标管理</span></a></li>
                <li id="Tab_1" onclick="showTab('Tab_1', 'Div_Select_1','selected')"><a onfocus="blur()"
                    href="javascript:void(0);"><span class="bj">学习内容/方式</span></a></li>
            </ul>
        </div>
    </div>
    <div class="dv_searchlist dv_searchlist_w">
    <div class="info">
        <div id="Div_Select_0">
            <uc2:ManageDevelopment ID="ManageDevelopment1" runat="server" />
        </div>
        <div id="Div_Select_1">
            <uc1:ManageContent ID="ManageContent1" runat="server" />
            
        </div>
    </div>
    </div>
</asp:Content>
