<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true" CodeFile="Setting.aspx.cs" Inherits="Security_Organization_Setting" %>
<%@ Register Assembly="ETMS.Editor" Namespace="ETMS.Editor.UEditor" TagPrefix="wuc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="org-info-block">
        <p>Title标题：<asp:TextBox ID="txtTitle" runat="server" CssClass="inputbox_160"></asp:TextBox></p>
    </div>
    <div class="org-info-block">
        <p>学习端菜单授权</p>
        <p class="dv_selectAll">
            <asp:CheckBox ID ="ck1"  runat="server" Text="精品课程" />
            <asp:CheckBox ID ="ck2"  runat="server" Text="评比活动" />
            <asp:CheckBox ID ="ck3"  runat="server" Text="名师风采" />
            <asp:CheckBox ID ="ck4"  runat="server" Text="日常公告" />
            <asp:CheckBox ID ="ck5"  runat="server" Text="关注人数" />
            <asp:CheckBox ID ="ck6"  runat="server" Text="手机浏览" />
        </p>
    </div>
    <div class="org-info-block" style="float:left;">
        <p>学习端信息配置</p>
        <asp:TextBox ID="ueSettingInfo" runat="server" CssClass="inputbox_area750" Height="250px" TextMode="MultiLine"></asp:TextBox>
    </div>
    <div class="org-info-block" style="float:left;text-align:center;">
        <p style="text-align:center;padding:20px 0px 0px 350px;"><asp:Button ID="btnSave" OnClick="btnSave_Click" runat="server" Text="保存" CssClass="btn_2" /></p>
    </div>
</asp:Content>

