<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site.master" AutoEventWireup="true" CodeBehind="StudyDetailAlone.aspx.cs" Inherits="ETMS.Studying.Study.StudyDetailAlone" %>

<%@ Register Src="~/Controls/FAQ.ascx" TagPrefix="uc1" TagName="FAQ" %>
<%@ Register Src="~/Controls/Note.ascx" TagPrefix="uc1" TagName="Note" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SitePlaceHolder" runat="server">
    <link href="<%=ETMS.Utility.WebUtility.AppPath%>/Styles/studyDetail.css" type="text/css" rel="stylesheet" />
    <link href="<%=ETMS.Utility.WebUtility.AppPath%>/Styles/umVideo.css" type="text/css" rel="stylesheet" />
    <link href="<%=ETMS.Utility.WebUtility.AppPath%>/Styles/video-js.css" type="text/css" rel="stylesheet" />

    <script lang="javascript" type="text/javascript" src="<%=ETMS.Utility.WebUtility.AppPath%>/Scripts/library/jquery.jcarousellite.js"></script>
    <script lang="javascript" type="text/javascript" src="<%=ETMS.Utility.WebUtility.AppPath%>/Tools/VideoPlayer/ckplayer/ckplayer.js"></script>
    <script lang="javascript" type="text/javascript" src="<%=ETMS.Utility.WebUtility.AppPath%>/Scripts/screenfull.js"></script>
    <script lang="javascript" type="text/javascript" src="<%=ETMS.Utility.WebUtility.AppPath%>/Scripts/umVideo.js"></script>
    <script lang="javascript" type="text/javascript" src="<%=ETMS.Utility.WebUtility.AppPath%>/Scripts/library/fotorama.js"></script>
    <script src="<%=ETMS.Utility.WebUtility.AppPath%>/Scripts/library/jquery.livequery.js"></script>
    <div class="view-area">
        <div class="title" style="margin-top:15px;">
            <div class="title_info"><i></i><span><a style="color:#3988fb;" href="CourseStudy.aspx?TrainingItemCourseID=<%=TrainingItemCourseID%>"><%=CourseNameString %></a> ></span><span id="resourceName"> </span></div>
            <asp:Repeater ID="ResourceList" runat="server">
                <HeaderTemplate>
                    <ul class="chapter">
                </HeaderTemplate>
                <ItemTemplate>
                    <li id="<%# Eval("ContentID") %>" class="<%# GetResourceStatus(Eval("StudyStatus").ToString()) %>" data-contentID="<%# Eval("ContentID") %>" data-type="<%# Eval("Type") %>">
                        <i class="status_icon"></i>
                        <span><%# Eval("Name") %></span>
                    </li>
                </ItemTemplate>
                <FooterTemplate>
                    </ul>
                </FooterTemplate>
            </asp:Repeater>            
        </div>
        <div class="main_content">
            <div class="play_file">
            </div>
            <div class="QAandNote">
                <ul class="QAandNote_tab">
                    <li class="faq" id="tab1" index="0">问答</li>
                    <li class="notes tab_active" id="tab2" index="1">笔记</li>
                </ul>
                <div class="u-studying-gard">
                    <div class="u-studying-block" style="display:none">
                        <uc1:FAQ runat="server" ID="FAQ" />
                    </div>
                    <div class="u-studying-block unvisible-element">
                        <uc1:Note runat="server" ID="Note" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>        
        //$('.header-menu a').eq(4).addClass('cur').siblings().removeClass('cur');
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
        var TrainingItemCourseID = '<%=TrainingItemCourseID%>';
        var ContentType = '<%=ContentType%>';
    </script>
    <script lang="javascript" type="text/javascript" src="<%=ETMS.Utility.WebUtility.AppPath%>/Scripts/Study.js"></script>
    <style>
        .play_file .fotorama__wrap .fotorama__stage .fotorama__stage__frame img{
            width: auto !important;
            height: auto !important;
            max-width: 100% !important;
            /*max-height: 100% !important;*/
        }
        .fotorama__stage__shaft,.fotorama__stage__frame {
             width: auto !important;
            /*height: auto !important;*/
            max-width: 100% !important;
            /*max-height: 100% !important;*/
        }
    </style>
</asp:Content>
