<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Default.master" AutoEventWireup="true" CodeBehind="AnnouncementList.aspx.cs" Inherits="ETMS.Studying.Public.AnnouncementList" %>

<%@ Import Namespace="ETMS.Utility" %>
<%@ Import Namespace="ETMS.Utility.Service.FileUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultPlaceHolder" runat="server">
    <link href="<%=WebUtility.AppPath%>/Styles/default.css" type="text/css" rel="stylesheet" />
    <!--日常公告列表-->
    <div class="view-area">
        <div class="announcementList-container">
            <div class="announcement-list" id="Aannouncement_List">
                <asp:Repeater ID="rptAnnouncementList" runat="server">
                    <ItemTemplate>
                        <dl class="announcementList-block" id="<%# Eval("ArticleID") %>">
                            <dt>
                                <img src="<%# StaticResourceUtility.GetFullPathByFileType(FileUploadFunctionType.BulletinImage, string.IsNullOrEmpty(Eval("ImageUrl").ToString())?"default.png":Eval("ImageUrl").ToString())%>"></dt>
                            <dd class="announcement-info">
                                <div class="h1 ellipsis">
                                    <%# Eval("MainHead") %>
                                    <span><%# Eval("CreateTime").ToDateTime().ToString("yyyy-MM-dd") %></span>
                                </div>
                                <p class="p">
                                    <%# StringUtility.StripHTML(Eval("ArticleContent").ToString()) %>
                                </p>
                            </dd>
                        </dl>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <div class="loadMore" runat="server" id="div_LoadMore">加载更多</div>
        </div>
    </div>
    <script>
        //定位菜单当前位置
        $('.header-menu #menu-notice').addClass('cur').siblings().removeClass('cur');
        /*日常公告点击跳转*/
        $("#Aannouncement_List").on("click","dl",function () {
            window.open("AnnouncementView.aspx?ArticleID=" + $(this).attr("id"), '_blank');
        })
        /*加载更多*/
        var pageIndex = 1;
        var pageSize = 5;
        var totalPage = 1;
        $('.loadMore').click(function () {
            debugger;
            var $this = $(this);
            if (pageIndex <= totalPage) {
                pageIndex++;
                $.ajax({
                    url: AppPath + "/PublicService/Notice.ashx",
                    type: "get",
                    data: { Method: "announcementlist", PageSize: pageSize, PageIndex: pageIndex },
                    dataType: "json",
                    async: false,
                    success: function (result) {
                        totalPage = result.PageTotal;
                        if (pageIndex == totalPage) {
                            $this.html("加载完成");
                        }
                        $(result.data).each(function (index) {
                            var announcementHtml = '<dl class="announcementList-block" id="' + result.data[index].ArticleID + '">' +
                                 '<dt><img src="' + result.data[index].ImageUrl + '"></dt>' +
                                 '<dd class="announcement-info">' +
                                 '<div class="h1 ellipsis">' + result.data[index].MainHead + '<span>' + result.data[index].CreateTime + '</span>' +
                                 '</div>' +
                                 '<p class="p">' + result.data[index].ArticleContent + '</p>' +
                                 '</dd>' +
                                 '</dl>';
                            $("#Aannouncement_List").append(announcementHtml);
                        })
                    }
                });
            }
        });
    </script>
</asp:Content>
