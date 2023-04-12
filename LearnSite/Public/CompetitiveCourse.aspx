<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Default.master" AutoEventWireup="true" CodeBehind="CompetitiveCourse.aspx.cs" Inherits="ETMS.Studying.Public.CompetitiveCourse" %>
<asp:Content ID="Content1" ContentPlaceHolderID="DefaultPlaceHolder" runat="server">
    <link href="<%=ETMS.Utility.WebUtility.AppPath%>/Styles/boutiqueCourse.css" type="text/css" rel="stylesheet" />
    <div class="view-area">
        <div id="famousTeacher-container">
        </div>
    </div>
    <script id="courselist_tmpl" type="text/x-jquery-tmpl">
        {{each(i,course) Data}}
         <div class="boutique_course">
                <a class="course_img" href="<%=ETMS.Utility.WebUtility.AppPath %>/Public/CompetitiveCourseDetail.aspx?CourseID=${CourseID}">
                    <img alt="课程图片" src="<%=ETMS.Utility.WebUtility.FileUrlRoot%>/CourseLogo/${ThumbnailURL}" />
                </a>
                <div class="course_info">
                    <h3>${CourseName}</h3>
                    <p>主讲教师：${TeacherName}</p>
                    <p>适合年龄：${ForObject}岁</p>
                    <p>课时：${CourseHours}</p>
                    <p>直播次数：${livingNum}次 </p>
                    <p>直播时间：${LivingTime}</p>
                    <p class="price">价格：<del>¥${Price}</del><span>¥${DiscountPrice}</span></p>
                    <p>报名人数：${SignupNum}人</p>
                    <input type="button" value="立即购买" onclick="tobuy('${CourseID}')" class="buy_now" />
                </div>
            </div>
        {{/each}}
    </script>
    <script lang="javascript">
        $(function () {
            $('.header-menu a').eq(3).addClass('cur').siblings().removeClass('cur');
            $.ajax({
                url: AppPath + "/PublicService/LivingHandler.ashx",
                type: "get",
                data: { Method: "GetCompetitiveCourses" },
                dataType: "json",
                async: false,
                success: function (result) {
                    $("#courselist_tmpl").tmpl(result).appendTo('#famousTeacher-container');
                }
            });
        })

        function tobuy(courseID) {
            var islogin = '<%= ETMS.Studying.BaseUtility.IsLogin%>';
            var root = '<%=ETMS.Utility.WebUtility.AppPath%>';
            if (islogin == 'False') {                
                layer.open({
                    type: 2,
                    title: '登录',
                    skin: 'layui-layer-rim',
                    area: ['360px', '325px'],
                    content: root + '/Login2.aspx'
                });
            }
            else {
                window.open(root + '/Study/Buy.aspx?CourseID=' + courseID, '_blank');
            }
        }
    </script>
    <script lang="javascript" type="text/javascript" src='<%=ETMS.Utility.WebUtility.AppPath %>/Scripts/library/layer/layer-2.4.min.js'></script>
</asp:Content>
