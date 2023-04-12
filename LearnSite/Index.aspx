<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Default.master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="ETMS.Studying.Index" %>

<%@ Register Src="~/Controls/DemandCourse.ascx" TagPrefix="uc1" TagName="DemandCourse" %>
<%@ Register Src="~/Controls/Announcement.ascx" TagPrefix="uc1" TagName="Announcement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultPlaceHolder" runat="server">
    <link href="<%=ETMS.Utility.WebUtility.AppPath%>/Styles/default.css" type="text/css" rel="stylesheet" />
    <link href="<%=ETMS.Utility.WebUtility.AppPath%>/Scripts/library/swiper/css/swiper.css" type="text/css" rel="stylesheet" />
    <script src="<%=ETMS.Utility.WebUtility.AppPath%>/Scripts/library/layer/layer.js"></script>
    <script src="<%=ETMS.Utility.WebUtility.AppPath%>/Scripts/library/swiper/js/swiper.js"></script>
    <script src="<%=ETMS.Utility.WebUtility.AppPath%>/Scripts/home.js"></script>
    <style>
        .liveCourse li{
                width: 266px;
                height: 100%;
                float: left;
                margin-right: 45px;
                margin-bottom: 25px;
                cursor: pointer;
        }
        .liveCourse li img {
            width: 100%;
            height: 150px;
        }
         .liveCourse li .living-time{
            top: -25px;
            z-index: 999;
            position: relative;
            color: #fff;
            float: right;
            padding-right: 5px;
        }
         .liveCourse .course_info {
            padding: 10px;
        }
         .liveCourse .course_name {
            font-size: 16px;
            color: #333;
            font-weight: bold;
            white-space: nowrap;
            text-overflow: ellipsis;
            overflow: hidden;
            width: 100%;
        }
         .liveCourse li .teacher_name {
            font-size: 14px;
            line-height: 35px;
            font-weight: bold;
        }
         .liveCourse li .teacher_name span {
            color: #999;
            margin-left: 10px;
        }
        .living-login-box {
            height: 90px;
            width: 255px;
            margin: 10px;
        }
        .living-name-box {
            float: left;
        }
        .living-nikename {
            display: block;
            width: 230px;
            height: 30px;
            margin: 8px 0px;
            line-height: 30px;
            padding: 0 5px;
            border: 1px solid #ccc;
            box-shadow: 1px 1px 5px rgba(0,0,0,.1) inset;
            color: #333;
            float: left;
        }
        .living-enter {
            margin: 12px 0px 0px 100px;
            width: 70px;
            line-height: 28px;
            border-radius: 2px;
            background: #2e8ded;
            color: #fff;
            border-color: #4898d5;
            cursor: pointer;
        }
        .living-cancel {
            margin: 12px 0px 0px 5px;
            width: 70px;
            line-height: 28px;
            border-radius: 2px;
            background: #cac8c9;
            color: #333;
            border-color: #ababab;
            cursor: pointer;
        }
        .living-inqueired {
            color:#ff0000;
            line-height: 45px;
            margin-left: 2px;
        }
    </style>
    <%--<div class="banner-box"></div>--%>
    <div class="swiper-container">
        <div class="swiper-wrapper">
            <asp:Repeater runat="server" ID="rpList">
                <ItemTemplate>
                    <div class="swiper-slide">
                        <a href="<%# Eval("SpreadPCLink") %>">
                            <img src='<%#ETMS.Utility.StaticResourceUtility.GetFullPathByFileType("BannerImage", Eval("PCImagePath").ToString()) %>'/>
                        </a>                  
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <!-- Add Pagination -->
        <div class="swiper-pagination"></div>
    </div>
    <div class="view-area">
        <!--日常公告-->
        <div class="announcement-container hide">
            <div class="cont-title announcement-title">日常公告<a href="Public/AnnouncementList.aspx" target="_self">更多</a></div>
            <uc1:Announcement runat="server" id="Announcement" />
        </div>
        <!--热门课程-->
        <div class="course-container">
            <div class="cont-title course-title">
                热门课程
                <a href="Public/CourseCenter.aspx" target="_self">更多</a><!--点播课程更多-->
                <a href="Public/CourseCenter.aspx" target="_self" style="display: none;">更多</a><!--直播课程更多-->
            </div>
            <div class="course-block">
                <div class="nav-tab">
                    <span class="cur-tab">点播课程</span>
                    <span>直播课程</span>
                </div>
                <div class="course-list">
                    <!--点播课程-->
                    <div class="onCourse list-block cur-block" id="DemandCourseList">
                        <uc1:DemandCourse runat="server" ID="DemandCourse1" />
                    </div>
                    <!--直播课程-->
                    <ul class="liveCourse list-block"></ul>
                </div>
            </div>
        </div>
    </div>
    <script id="living_valid_tmpl" type="text/x-jquery-tmpl">
        {{each(i,Course) data}}
            <dl id="${Course.CourseID}" data-moudle="${Course.IsLiving}" data-status="${Course.LivingStatus}">
                <dt style="position: relative;">
                    <img src="${Course.ThumbnailURL}">
                    {{if Course.IsLiving==1}}
                    <div class="zbline">
                        <div class="zbleft">${Course.TimeString}</div>
                        {{if Course.LivingStatus==1}}<div class="zb">正在直播</div>
                        {{else Course.LivingStatus==2}}<div class="zb">直播预告</div>
                        {{else Course.LivingStatus==3}}<div class="zb">精彩回放</div>
                        {{/if}}
                    </div>
                    {{/if}}
                </dt>
                <dd>
                    <p class="h1 ellipsis">${Course.CourseName}</p>
                    <p><i>${Course.TeacherNameLimit}</i></p>
                    <p>
                        <i>${Course.CourseHours}</i>课时
                                    <span><i>${Course.FocusCount}</i>学习</span>
                    </p>
                </dd>
            </dl>
        {{/each}}
    </script>
    <script>
        $('.header-menu a').eq(0).addClass('cur').siblings().removeClass('cur');

        /*首页--点播课程、者直播课程 tab切换*/
        $(".course-block .nav-tab span").on("click", function () {
            var index = $(this).index();
            $(this).addClass("cur-tab").siblings("span").removeClass("cur-tab");
            $(".course-list .list-block").eq(index).addClass("cur-block").siblings(".list-block").removeClass("cur-block");
            $(".course-title a").eq(index).show().siblings("a").hide();
        })
        $.ajax({
            url: AppPath + "/PublicService/LivingHandler.ashx",
            type: "get",
            data: { Method: "indexliving" },
            contentType: "application/json",
            dataType: "json",
            async: false,
            success: function (result) {
                console.log(result);
                $("#living_valid_tmpl").tmpl(result).appendTo(".liveCourse");
            }
        });
        /*日常公告点击跳转*/
        //$(".announcement-block .toDetail").on("click", function () {
        //    window.location.href = "Public/AnnouncementView.aspx";
        //})
        /*点播课程点击跳转*/
        //$(".onCourse dl").on("click", function () {
        //    window.location.href = "Public/CourseView.aspx";
        //})
        //$(".onCourse dl").on("click", function () {
        //    window.location.href = "Public/CourseView.aspx";
        //})
        //$(".onCourse dl").on("click", function () {
        //    window.location.href = "../Public/CourseView.aspx";
        //})
        ///*直播课程点击跳转*/

        //游客进入直播
        function guessEnter(livingID)
        {
            var userID = '<%=Guid.NewGuid()%>';
            var nikeName = $('#txtNikeName').val();

            if ($('#txtNikeName').val().trim() == '')
            {
                $('.living-inqueired').removeClass('hide');
                return false;
            }
            enterLiving(livingID, userID, nikeName);
            layer.closeAll();
        }

        var liveID = "";
        var livingStatus = "";
        /*点播课程点击跳转*/
        $(".liveCourse").on("click", "dl", function () {
            liveID = $(this).attr("id");
            livingStatus = $(this).data("status");
            if ($(this).data("moudle") == 1) {
                window.location.href = AppPath +"/Public/CourseLivingView.aspx?courseid=" + $(this).attr("id");
                <%--var isAuthenticated = '<%=ETMS.Studying.BaseUtility.IsLogin%>';

                if (isAuthenticated == "True") {
                    userID = '<%=University.Mooc.AppContext.UserContext.Current.LoginName%>';
                    nikeName = '<%=University.Mooc.AppContext.UserContext.Current.RealName%>';
                    if (nikeName == '') { nikeName = userID };
                    enterLiving(liveID, userID, nikeName, livingStatus);
                } else {
                    var root = '<%=ETMS.Utility.WebUtility.AppPath%>';
                    layer.open({
                        type: 2,
                        title: '登录',
                        skin: 'layui-layer-rim',
                        area: ['360px', '325px'],
                        content: root + '/Login2.aspx?callbackJS=LivingOpen&LivingStatus='+livingStatus + '&objId='+liveID,
                        end: function () {
                            //enterLiving(liveID, liveUserID, liveNikeName, livingStatus);
                            window.location.href = window.location.href;
                        }
                    });
                }--%>
            } else {
                window.location.href = "CourseView.aspx?courseid=" + $(this).attr("id");
            }

        })
        var liveUserID = "";
        var liveNikeName = "";
        function LivingOpen(userID, nikeName) {
            if (nikeName == '') { nikeName = userID };
            //window.location.href = window.location.href;
            liveUserID = userID;
            liveNikeName = nikeName;
        }

        function enterLiving(livingID, userID, nikeName, livingStatus)
        {
            if (livingStatus == 3) {
                tohistory(livingID, userID, nikeName);
            }else{
                validLiving(livingID, userID, nikeName);
            }
        }
        //获取直播信息，并进入直播页面
        function validLiving(livingID, userID, nikeName) {
            $.ajax({
                url: AppPath + "/PublicService/LivingHandler.ashx",
                type: "get",
                data: { Method: "getlivinginfo", LivingID: livingID, UserID: userID, NikeName: nikeName },
                contentType: "application/json",
                dataType: "json",
                async: false,
                success: function (result) {
                    if (result.code == 0) {
                        var url = result.data.liveUrl;
                        window.open(url, '_blank');
                    }
                }
            });
        }

        function tohistory(livingID, userID, nikeName) {
            $.ajax({
                url: AppPath + "/PublicService/LivingHandler.ashx",
                type: "get",
                data: { Method: "getplaybackinfo", LivingID: livingID, UserID: userID, NikeName: nikeName },
                contentType: "application/json",
                dataType: "json",
                async: false,
                success: function (result) {
                    if (result.code == 0) {
                        var url = result.data.playbackUrl;
                        window.open(url, '_blank');
                    }
                    else {
                        layer.alert(result.msg);
                    }
                }
            });
        }
    </script>
</asp:Content>
