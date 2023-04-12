<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Default.master" AutoEventWireup="true" CodeBehind="ExcellentCourseLearn.aspx.cs" Inherits="ETMS.Studying.Study.ExcellentCourseLearn" %>
<asp:Content ID="Content1" ContentPlaceHolderID="DefaultPlaceHolder" runat="server">
    <link href="<%=ETMS.Utility.WebUtility.AppPath%>/Styles/excellentCourseLearn.css" type="text/css" rel="stylesheet" />
    <div class="view-area" id="competitiveCourse">
        
    </div>
    <script id="courselist_tmpl" type="text/x-jquery-tmpl">
        {{each(i,course) Data}}
         <div class="learn_obj">
            <div class="excellentcourse_info">
                <img class="excellentcourse_img" alt="课程图片" src="<%=ETMS.Utility.WebUtility.FileUrlRoot%>/CourseLogo/${ThumbnailURL}" />
                <div class="excellentcourse_introduce">
                    <h3>${CourseName}</h3>
                    <p>主讲教师：${TeacherName}</p>
                    <p>适合年龄：${ForObject}岁</p>
                    <p>课时：${CourseHours}</p>
                    <p>直播次数：${livingNum}次  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;直播时间：${LivingTime} &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;报名人数：${SignupNum}人</p>
                </div>
                <p class='chapter_status'>直播列表<span class='bottom'></span></p>
            </div>
             <div class='chapter_list'>
                {{each(i,d) course.Livings}}
                    <ul class="chapter_information">
                        <li><span class="name">${LivingName}</span><span class="time">${Date} ${SHHMM}~${EHHMM}</span><span class="teacher">${TeacherName}</span><span class="enter"><a href="javascript:void(0)" onclick="toliving('${LivingID}','${EndTime}')" class="enter-living">进入</a></span></li>
                    </ul>
                 {{/each}}
             </div>
        </div>
        {{/each}}
    </script>

    <script lang="javascript">
        $(function () {
            $('.header-menu a').eq(5).addClass('cur').siblings().removeClass('cur');
            $.ajax({
                url: AppPath + "/PublicService/LivingHandler.ashx",
                type: "get",
                data: { Method: "getmycompetitivecourses" },
                dataType: "json",
                async: false,
                success: function (result) {
                    $("#courselist_tmpl").tmpl(result).appendTo('#competitiveCourse');
                }
            });
            $('.chapter_status').click(function () {
                if ($(this).find('span').hasClass('top')) {
                    $(this).parent().next().slideUp();
                    $(this).find('span').removeClass().addClass('bottom');
                } else {
                    $(this).parent().next().slideDown();
                    $(this).find('span').removeClass().addClass('top');
                }
            })
        })

        function toliving(livingID, endTime)
        {
            var end = new Date(endTime);
            var cur = new Date();

            if (cur > end) {
                tohistory(livingID);
            }
            else {
                toactive(livingID);
            }
        }

        var userID = '<%=University.Mooc.AppContext.UserContext.Current.UserID%>';
        var nikeName = '<%=University.Mooc.AppContext.UserContext.Current.LoginName%>';
        function toactive(livingID)
        {
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

        function tohistory(livingID)
        {
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
    <script lang="javascript" type="text/javascript" src='<%=ETMS.Utility.WebUtility.AppPath %>/Scripts/library/layer/layer-2.4.min.js'></script>
</asp:Content>
