<%@ Page Title="教学内容学习" Language="C#" AutoEventWireup="true" MasterPageFile="~/Master/Default.master" CodeBehind="StudyCourseResource.aspx.cs" Inherits="ETMS.Studying.Study.StudyCourseResource" %>
<asp:Content ID="Content1" ContentPlaceHolderID="DefaultPlaceHolder" runat="server">
     <link href="<%=ETMS.Utility.WebUtility.AppPath%>/Styles/studyDetail.css" type="text/css" rel="stylesheet" />
    <link href="<%=ETMS.Utility.WebUtility.AppPath%>/Styles/umVideo.css" type="text/css" rel="stylesheet">
    <link href="<%=ETMS.Utility.WebUtility.AppPath%>/Styles/video-js.css" type="text/css" rel="stylesheet">

    <script lang="javascript" type="text/javascript" src="<%=ETMS.Utility.WebUtility.AppPath%>/Scripts/library/jquery.jcarousellite.js"></script>
    <script lang="javascript" type="text/javascript" src="<%=ETMS.Utility.WebUtility.AppPath%>/Tools/VideoPlayer/ckplayer/ckplayer.js"></script>
    <script lang="javascript" type="text/javascript" src="<%=ETMS.Utility.WebUtility.AppPath%>/Scripts/screenfull.js"></script>
    <script lang="javascript" type="text/javascript" src="<%=ETMS.Utility.WebUtility.AppPath%>/Scripts/umVideo.js"></script>
    <script lang="javascript" type="text/javascript" src="<%=ETMS.Utility.WebUtility.AppPath%>/Scripts/library/fotorama.js"></script>
    <script src="<%=ETMS.Utility.WebUtility.AppPath%>/Scripts/library/jquery.livequery.js"></script>
    <div class="view-area">
        <div class="title">
            <div class="title_info"><i></i><span><a style="color:#3988fb;" href="javascript:window.history.go(-1)"><%=CourseName %></a> ></span><span id="spName"><%=ResourceName %> </span></div>
            <asp:Repeater ID="ResourceList" runat="server">
                <HeaderTemplate>
                    <ul class="chapter">
                </HeaderTemplate>
                <ItemTemplate>
                    <li class="<%# GetResourceStatus(Guid.Parse(Eval("ContentID").ToString())) %>" id="<%# Eval("ContentID") %>" data-contentID="<%# Eval("ContentID") %>" data-name="<%#Eval("Name") %>" data-isopen="<%# Eval("IsOpen") %>" data-type="<%# Eval("Type") %>">
                        <i class="status_icon"></i>
                        <span class="<%# Eval("IsOpen").ToString().ToLower() == "true"?"":"unopen" %>"><%# Eval("Name") %></span>
                    </li>
                </ItemTemplate>
                <FooterTemplate>
                    </ul>
                </FooterTemplate>
            </asp:Repeater>
            <%--<ul class="chapter">
                <li class="studying">
                    <i class="status_icon"></i>
                    <span>第一章 战略管理导论</span>
                </li>
                <li>
                    <i class="status_icon"></i>
                    <span>第一章 战略管理导论</span>
                </li>
                <li>
                    <i class="status_icon"></i>
                    <span>第一章 战略管理导论</span>
                </li>
                <li>
                    <i class="status_icon"></i>
                    <span>第一章 战略管理导论</span>
                </li>
            </ul>--%>
        </div>
        <div class="main_content">
            <div class="play_file" style="width:100%">
            </div>
           <%-- <div class="QAandNote">
                <ul class="QAandNote_tab">
                    <li class="faq tab_active" id="tab1" index="0">问答</li>
                    <li class="notes" id="tab2" index="1">笔记</li>
                </ul>
                <div class="u-studying-gard">
                    <div class="u-studying-block" style="display:none">
                        <uc1:FAQ runat="server" ID="FAQ" />
                    </div>
                    <div class="u-studying-block unvisible-element">
                        <uc1:Note runat="server" ID="Note" />
                    </div>
                </div>
            </div>--%>
        </div>
    </div>
    <script>
        $('.header-menu a').eq(1).addClass('cur').siblings().removeClass('cur');
        $('.title_info i').click(function () {
            $('.chapter').toggle();
            $('.title_info').toggleClass('checked');
        });
        $(".u-studying-block").eq(0).show().siblings().hide();
        $('.chapter').on('mouseleave', function () {
            $(this).hide();
            $('.title_info').removeClass('checked');
        })
        var ContentID = '<%=ContentID%>';
        var CourseID = '<%=CourseID%>';
        var ContentType = '<%=ContentType%>';
    </script>
    <script lang="javascript" type="text/javascript" src="<%=ETMS.Utility.WebUtility.AppPath%>/Scripts/StudyCourseResource.js"></script>

</asp:Content>