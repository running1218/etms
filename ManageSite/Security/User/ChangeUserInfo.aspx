<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChangeUserInfo.aspx.cs" Inherits="Admin_Site_User_View"
    MasterPageFile="~/MasterPages/MPageAdmin.master" %>

<%@ Register Src="UpdatePwdControl.ascx" TagName="UpdatePwdControl" TagPrefix="uc1" %>
<%@ Register Src="UserControl.ascx" TagName="UserControl" TagPrefix="uc2" %>
<asp:Content runat="server" ID="PageContent" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="dv_serviceTab">
        <div class="dv_Tabmenus">
            <ul>
                <li id="Tab_0" onclick="showTab('Tab_0', 'Div_Select_0','selected')"><a onfocus="blur()"
                    href="javascript:void(0);"><span class="bj">用户信息</span></a></li>
                <li id="Tab_1" onclick="showTab('Tab_1', 'Div_Select_1','selected')"><a onfocus="blur()"
                    href="javascript:void(0);"><span class="bj">修改密码</span></a></li>
            </ul>
        </div>
    </div>
    <div class="info">
        <div id="Div_Select_0" style="display: none">
            <div class="dv_information" style="height:400px">
                <uc2:UserControl ID="UserControl2" runat="server" />
                <div class="dv_submit">
                    <asp:Button ID="btnUpdate" runat="server" Text="保存" OnClick="btnUpdate_Click" SkinID="Edit"
                        ValidationGroup="Edit" /><asp:Button ID="Button2" runat="server" Text="返回" SkinID="Return" />
                </div>
            </div>
        </div>
        <div id="Div_Select_1" style="display: none">
                <uc1:UpdatePwdControl ID="UserControl1" runat="server"></uc1:UpdatePwdControl>
        </div>
    </div>
</asp:Content>
