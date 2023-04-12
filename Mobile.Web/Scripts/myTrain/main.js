var userid = Cookies.get('cookie_userid');
if (_.isEmpty(userid)) {
    window.location.href = AppPath+ '/Login/Index';
}
//退出
function exit_out() {
    var params = { "UserID": userid };
    common.call(AppPath+"/Login/PostUserLoginQuit", params, 'post', function (data) {
        if (data.Status == true) {
            //清除登录缓存
            common.clearLoginCookie();
             window.location.href = AppPath+ '/Home/Index';
        } else {
          //  layer.msg(data.Message)
        };
        })
}
 
//课程列表
function loadTrainCourse() {    
    var params = { "UserID": userid , "Module": 1};
    common.call(AppPath+"/MyTrain/GetTrainCourseList", params, 'get', function (data) {
        if (data.Status == true && data.Data.length > 0) {
            var template = Handlebars.compile($("#tmpl_course_list").html());
            var renderHTML = template(data);
            $('#course_list').html(renderHTML);
        } else {
            contentNull($('#course_list'));
           // layer.msg(data.Message)
        };
    }, error)
}
function loadTrainLivingCourse()
{
    var params = { "UserID": userid, "Module": 2 };
    common.call(AppPath + "/MyTrain/GetTrainCourseList", params, 'get', function (data) {
        if (data.Status == true && data.Data.length > 0) {
            var template = Handlebars.compile($("#tmpl_course_list").html());
            var renderHTML = template(data);
            $('#living_course_list').html(renderHTML);
        } else {
            contentNull($('#living_course_list'));
            // layer.msg(data.Message)
        };
    }, error)
}
//培训项目的测评列表
function loadTrainEvaluation() {
    var params = { "UserID": userid };
    var nowDate = common.formatDate(new Date(), "yyyy-MM-dd");
    common.call(AppPath+"/MyTrain/GetTrainEvaluationList", params, 'get', function (data) {
        if (data.Status == true && data.Data.length > 0) {
            //console.log(data)
            var template = Handlebars.compile($("#tmpl_evaluation_list").html());
            var renderHTML = template(data);
            $('#evaluation_list').html(renderHTML);
        } else {
            contentNull($('#evaluation_list'));
            //layer.msg(data.Message)
        };
    }, error)
}
$(function () {
    var scrollContentHeight = $(document).height() - 190;
    $('.swiper-slide').css({ 'height': scrollContentHeight + 'px', 'overflow': 'auto' })
    //初始化tab
    var swiper = new Swiper('.swiper-container', {
        onSlideChangeStart: function () {
            var _index = swiper.activeIndex;
            $(".tab .item p").removeClass("choose");
            $(".tab .item").eq(_index).find('p').addClass("choose");
        }
    });
    //导航点击切换
    if (Cookies.get("cookie_worker")==1) {
        $(".nav_bar li").eq(1).find('p').addClass('choose').parent().siblings().find('p').removeClass('choose');
        swiper.slideTo(1);
        Cookies.set("cookie_worker", 0);

    }
    $(".nav_bar li").click(function () {
        var order = $(this).index();
        $(this).find('p').addClass('choose').parent().siblings().find('p').removeClass('choose');
        swiper.slideTo(order);
    });
    //加载数据
    if (!_.isEmpty(userid)) {
        loadTrainCourse();
        //loadTrainLivingCourse();
        //loadTrainEvaluation();
    }
    $(".info-box img").attr("src", Cookies.get('cookie_userPhotoUrl'));
    $(".user-name").html(Cookies.get('cookie_username'));

    var iscrollerUpOrDown = $(document).height() - $(".user-info").height() - $(".nav_bar").height();
    $('#iscroller_one').css({ 'height': scrollContentHeight + 'px', 'overflow': 'auto' })
    refresher.init({
        id: "iscroller_one",
        pullDownAction: function () {
            setTimeout(function () {
                iscroller_one.refresh();
            }, 1000);
        },
        pullUpAction: function () {
            setTimeout(function () {
                iscroller_one.refresh();
            }, 1000);
        },
    });
    $('#iscroller_two').css({ 'height': scrollContentHeight + 'px', 'overflow': 'auto' })
    refresher.init({
        id: "iscroller_two",
        pullDownAction: function () {
            setTimeout(function () {
                iscroller_two.refresh();
            }, 1000);
        },
        pullUpAction: function () {
            setTimeout(function () {
                iscroller_two.refresh();
            }, 1000);
        },
    });

})