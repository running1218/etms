<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HeaderActivity.ascx.cs" Inherits="ETMS.Studying.Controls.HeaderActivity" %>

<div class="aheader-all">
<div class="aheader-content">
    <div class="aheader-logo">
        <a href="<%=ETMS.Utility.WebUtility.AppPath %>/Index.aspx" target="_self">
            <asp:Image ID="imgLogo" runat="server" />
        </a>
    </div>
    <div class="aheader-right">
        <ul>
            <li class="user-login login-activity" id="liLogin" runat="server">
                <span>登录</span>
            </li>
            <li id="liLoginInfo" runat="server" class="user-login-info">
                <img id="imgHeadIcon"   onclick="window.location.href='<%=ETMS.Utility.WebUtility.AppPath %>/Self/UserInfo.aspx?index=0'" alt="user-info" />

                <div class="personalInfo_box">
                    <span class="triangleBorder"><em class="triangle"></em></span>
                    <ul>
                        <li><a href="<%=ETMS.Utility.WebUtility.AppPath %>/Activity/MyActivityList.aspx?index=2">我的评比</a></li>                  
                        <li><asp:Button ID="btnQuit" runat="server" OnClick="btnQuit_Click" Text="退出" /></li>
                    </ul>
                </div>
            </li>
            <li class="hide">
                
            </li>
        </ul>
    </div>
</div>
    </div>
<script src="<%=ETMS.Utility.WebUtility.AppPath%>/Scripts/library/jquery.qrcode.min.js"></script>
<script>
    

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
            content: root + '/Login2.aspx?callbackJS=login',
            end: function () {
                window.location.href = window.location.href;
            }
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