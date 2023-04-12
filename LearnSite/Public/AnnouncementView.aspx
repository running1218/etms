<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Default.master" AutoEventWireup="true" CodeBehind="AnnouncementView.aspx.cs" Inherits="ETMS.Studying.Public.AnnouncementView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="DefaultPlaceHolder" runat="server">
    <link href="<%=ETMS.Utility.WebUtility.AppPath%>/Styles/default.css" type="text/css" rel="stylesheet" />
    <!--公告详情-->
    <div class="view-area">
        <div class="notice-container">
            <div class="cont-title course-title"><%= Entity.MainHead %><span>发布时间：<%= Entity.CreateTime.ToString("yyyy-MM-dd") %></span></div>
            <div class="notice-list">
                <asp:Literal ID="lblArticleContent" runat="server" />
            </div>
        </div>
    </div>
    <script>
        //定位菜单当前位置
        $('.header-menu #menu-notice').addClass('cur').siblings().removeClass('cur');
        var doumentHeight = $(document).height();
        $('.content-container').css({ 'min-height': doumentHeight - 152 - 90 + 'px', 'padding-top': '20px' });
    </script>
</asp:Content>
