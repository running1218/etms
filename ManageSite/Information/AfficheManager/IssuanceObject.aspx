<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master" AutoEventWireup="true" CodeFile="IssuanceObject.aspx.cs" Inherits="Information_AfficheManager_IssuanceObject" %>

<%@ Register Src="~/Information/AfficheManager/Controls/IssuanceObjectPersonal.ascx"
    TagName="IssuanceObjectPersonal" TagPrefix="uc1" %>
<%@ Register Src="~/Information/AfficheManager/Controls/IssuanceObjectGroup.ascx"
    TagName="IssuanceObjectGroup" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--导航路径-->
    <div class="dv_path">
        当前位置：信息公告系统>>公告管理>>发布对象
    </div>
    <!--功能标题-->
    <h2 class="dv_title">
        发布对象
    </h2>
    <div class="dv_serviceTab">
        <div class="dv_Tabmenus">
            <ul>
                <li id="Tab_0" onclick="showTab('Tab_0', 'Div_Personal','selected')" class="selected"><a onfocus="blur()"
                    href="javascript:void(0);"><span class="bj">发布对象按个人</span></a></li>
                <li id="Tab_1" onclick="showTab('Tab_1', 'Div_Group','selected')"><a onfocus="blur()"
                    href="javascript:void(0);"><span class="bj">发布对象按群组</span></a></li>
            </ul>
        </div>
        <div class="info">
            <div id="Div_Personal">
                <uc1:IssuanceObjectPersonal ID="IssuanceObjectPersonal2" Op="list" runat="server" />
            </div>
            <div id="Div_Group" style="display: none">
                <uc2:IssuanceObjectGroup ID="IssuanceObjectGroup2" Op="list" runat="server" />
            </div>
        </div>
    </div>
</asp:Content>

