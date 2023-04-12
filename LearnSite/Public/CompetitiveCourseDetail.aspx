<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Default.master" AutoEventWireup="true" CodeBehind="CompetitiveCourseDetail.aspx.cs" Inherits="ETMS.Studying.Public.CompetitiveCourseDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="DefaultPlaceHolder" runat="server">
     <link href="<%=ETMS.Utility.WebUtility.AppPath%>/Styles/boutiqueCourse.css" type="text/css" rel="stylesheet" />
    <div id="course_box">
        <div class="course_detail">
            <div class="content_detail">
                <img alt="老师" src="../Styles/images/teacher.png" />
                <div id="course_detail"></div>
            </div>
           
        </div>
         <div class="characteristic_cont">
                <div class="title">课程详情</div>
                <div class="characteristic_detail"></div>
            </div>
            <div class="characteristic_cont">
                <div class="title">课程表</div>
                <div class="list_detail">
                </div>
            </div>
    </div>
    <script id="courselist_tmpl" type="text/x-jquery-tmpl">
        {{each(i,course) data}}
        <div class="course_info">
            <h3>${CourseName}</h3>
            <p>主讲教师：${TeacherName}</p>
            <p>适合年龄：${ForObject}岁</p>
            <p>课时:${CourseHours}</p>
            <p>直播次数：${livingNum}次 </p>
            <p>直播时间：${LivingTime}</p>
            <p class="price">价格：<del>¥${Price}</del><span>¥${DiscountPrice}</span></p>
            <p>报名人数：${SignupNum}人</p>
            <input type="button" id="${CourseID}" value="立即购买" onclick="tobuy('${CourseID}')" class="buy_now" />
        </div>
        {{/each}}
    </script>
    <script>
        var courseID = GetQueryString('CourseID');
        $.ajax({
            url: AppPath + "/PublicService/LivingHandler.ashx",
            type: "get",
            data: { Method: "GetCompetitiveCourse", 'CourseID': courseID },
            dataType: "json",
            async: false,
            success: function (result) {
                var results = { data: [] };
                results.data.push(result.Data);
                $("#courselist_tmpl").tmpl(results).appendTo('#course_detail');
                $('.characteristic_detail').html(result.Data.CourseIntroduction);
                $('.list_detail').html(result.Data.CourseOutline);
            }
        });

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
</asp:Content>
