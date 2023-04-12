<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Header.ascx.cs" Inherits="ETMS.Studying.Controls.Header" %>

<div class="header-content">
    <div class="header-logo">
        <a href="<%=ETMS.Utility.WebUtility.AppPath %>/Index.aspx" target="_self">
            <asp:Image ID="imgLogo" runat="server" />
        </a>
    </div>
    <div class="header-menu">
        <a href="<%=ETMS.Utility.WebUtility.AppPath %>/Index.aspx" target="_self">首页</a>
        <a href="<%=ETMS.Utility.WebUtility.AppPath %>/Public/CourseCenter.aspx" id="menu-course" class="<%=ETMS.Studying.BaseUtility.Limits.CourseMenu?"":"hide" %>" target="_self">精品课程</a>
        <a href="<%=System.Configuration.ConfigurationManager.AppSettings["SiteUrlRoot"] %>/Activity/Public/ActivityList.aspx" target="_blank" class="<%=ETMS.Studying.BaseUtility.Limits.CompareJob?"":"hide" %>">评比活动</a>        
        <a href="<%=ETMS.Utility.WebUtility.AppPath %>/Public/FamousTeacher.aspx" id="menu-teacher" target="_self" class="<%=ETMS.Studying.BaseUtility.Limits.TeacherInfo?"":"hide" %>">名师风采</a>
        <a href="<%=ETMS.Utility.WebUtility.AppPath %>/Public/AnnouncementList.aspx" id="menu-notice" class="<%=ETMS.Studying.BaseUtility.Limits.Notice?"":"hide" %>">日常公告</a>
    </div>
    <div class="header-right">
        <ul>
            <li class="hide">
                <input type="text" placeholder="请输入课程名称" class="search" />
                <img class="search_icon" src="<%=ETMS.Utility.WebUtility.AppPath %>/Styles/images/common/search.png" />
            </li>
            <li class="<%=ETMS.Studying.BaseUtility.Limits.ViewNum?"":"hide" %>">
                已<asp:Label ID="lblVistorNum" runat="server"></asp:Label>人关注
            </li>
            <li class="view <%=ETMS.Studying.BaseUtility.Limits.PhoneLink?"":"hide" %>">
                <span>手机浏览</span>
                <div class="phoneView">
                    <span class="triangleBorder"><em class="triangle"></em></span>
                    <%--<img src="<%=ETMS.Utility.WebUtility.AppPath%>/Styles/images/common/erweima.png">--%>
                    <div id="code"></div>
                     扫描查看
                </div>
            </li>
            <li class="user-login" id="liLogin" runat="server">
                <span>登录</span>
            </li>
            <li id="liLoginInfo" runat="server" class="user-login-info">
                <img id="imgHeadIcon"   onclick="window.location.href='<%=ETMS.Utility.WebUtility.AppPath %>/Self/UserInfo.aspx?index=0'" alt="user-info" />

                <div class="personalInfo_box">
                    <span class="triangleBorder"><em class="triangle"></em></span>
                    <ul>
                        <li><a href="<%=ETMS.Utility.WebUtility.AppPath %>/Study/MyTrain.aspx?index=1">我的课程</a></li>
                        <li><a href="<%=ETMS.Utility.WebUtility.AppPath %>/activity/myactivitylist.aspx?index=2" target="_blank">我的评比</a></li>
                        <li><a href="<%=ETMS.Utility.WebUtility.AppPath %>/Self/MyHistory.aspx">学习档案</a></li>
                        <li><a href="<%=ETMS.Utility.WebUtility.AppPath %>/Self/UserInfo.aspx?index=4">个人信息</a></li>                        
                        <li><asp:Button ID="btnQuit" runat="server" OnClick="btnQuit_Click" Text="退出" /></li>
                    </ul>
                </div>
            </li>
            <li class="hide">
                
            </li>
        </ul>
    </div>
</div>
<script src="<%=ETMS.Utility.WebUtility.AppPath%>/Scripts/library/jquery.qrcode.min.js"></script>
<script>
    /*手机浏览*/
    $(".view").on("mouseover", function () {
        $("#code").empty();
        $("#code").qrcode({
            width: 93,
            height: 95,
            text:'<%=QRcodeText  %>'
         });
        $(".phoneView").show();
    })
    $(".view,#code").on("mouseout", function () {
        $(".phoneView").hide();
    })

    $('.header-menu a').on('mouseover', function () {
        var index = $(this).index();
        if(index != 5){
            $('.course_type').hide();
        }
    })
    //头像滑过显示操作框
    $('.user-login-info').hover(function () {
        $(".personalInfo_box").show();
    },function(){
        $(".personalInfo_box").hide();
    })
    /*登录*/
    $(".user-login span").on("click", function () {
        var root = '<%=ETMS.Utility.WebUtility.AppPath%>';
        layer.open({
            type: 2,
            title: '登录',
            skin: 'layui-layer-rim', 
            area: ['360px', '325px'],
            content: root + '/Login2.aspx'
        });
    })

    //搜索
    $('.search_icon').click(function () {
        var text = escape($('.search').val());
        location.href = '<%=ETMS.Utility.WebUtility.AppPath %>/Public/CourseCenter.aspx?searchCont=' + text;
    })
    $(".search").keydown(function () {
        var event = window.event || arguments.callee.caller.arguments[0];
        if (event.keyCode == 13) {
            var text = escape($('.search').val());
            location.href = '<%=ETMS.Utility.WebUtility.AppPath %>/Public/CourseCenter.aspx?searchCont=' + text;
            return false;
        }
    });
    $('#toMyTraining').hover(function () {
        $('.course_type').show();
    });
    $('.course_type').mouseleave(function () {
        $('.course_type').hide();
    })
    $('.course_type li').click(function () {
        var logininfo = '<%=IsLogin%>';
        var index = $(this).index();
        if (logininfo == 'False') {
            $('.user-login span').click();
        }else {
            var root = '<%=ETMS.Utility.WebUtility.AppPath%>';
            if(index){
                window.location.href = root + '/Study/MyTrain.aspx';
            } else {
                window.location.href = root + '/Study/ExcellentCourseLearn.aspx';
            }
        }
    });
    $('#Living').click(function () {
        var logininfo = '<%=IsLogin%>';
        //var index = $(this).index();
        var root = '<%=ETMS.Utility.WebUtility.AppPath%>';
        if (logininfo == 'False') {
            $('.user-login span').click();
        } else {
            window.location.href = root + '/Public/LiveCourse.aspx';
        }
    });
    $(function () {
            var imgurl = "<%=ImgUrl%>";
            if (imgurl == "" || imgurl == undefined || imgurl.indexOf("App_Themes") > 0) {
                document.getElementById('imgHeadIcon').src = "../App_Themes/ThemeStudying/images/default_user.png";            
            }
            else {
                document.getElementById('imgHeadIcon').src = imgurl;
              
            }
    });
</script>