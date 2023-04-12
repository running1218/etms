var userId = Cookies.get('cookie_userid');
var userName = Cookies.get('cookie_username');
//  直播预告
function loadLivingData(livingPageIndex, PageSize, loadType) {
    var params = { "PageIndex": livingPageIndex, 'PageSize': PageSize };
    common.call(AppPath+"/Living/ValidList", params, 'get', function (data) {
        if (data.Status == true && data.Data.DataList.length > 0) {
            var template = Handlebars.compile($("#tmpl_living_list").html());
            var renderHTML = template(data);
            if (loadType) {
                $('#living_list').append(renderHTML);
            } else {
                $('#living_list').html(renderHTML);
            }
        } else {
            if ($('#living_list li').length == 0) {
                $('.search1').show();
            }
        };
    }, error)
}
//精彩回放
function playBackData(playBackPageIndex, PageSize, loadType) {  
    var params = { "PageIndex": playBackPageIndex, 'PageSize': PageSize };
    common.call(AppPath + "/Living/PlayBackList", params, 'get', function (data) {
        if (data.Status == true && data.Data.DataList.length > 0) {
            var template = Handlebars.compile($("#tmpl_playBack_list").html());
            var renderHTML = template(data);
            if (loadType) {
                 $('#playback_list').append(renderHTML);
            } else {
                $('#playback_list').html(renderHTML);
            }
        } else {
            if ($('#playback_list li').length == 0) {
                $('.search2').show();
            }
        };
    }, error)
}
//进入直播点击事件
function validClick(courseID) {
    //if (_.isEmpty(Cookies.get('cookie_userid'))) {
    window.location.href = AppPath + '/Course/LivingDetail?CourseID=' + courseID;
    //}
    //else {
    //    enterLiving(livingID, Cookies.get('cookie_userid'), Cookies.get('cookie_username'));
    //}
    //if (!_.isEmpty(userId)) {
    //    if (userName == '') { userName = userId };
    //    enterLiving(livingID, userId, userName);
    //}
    //else {
    //    layer.open({
    //        title: '进入直播',
    //        type: 1,
    //        skin: 'layui-layer-demo', //样式类名
    //        closeBtn: 1, //不显示关闭按钮
    //        btn: ['确定', '取消'],
    //        area: ['85%', '150px'],
    //        shadeClose: true, //开启遮罩关闭
    //        content: '<div class="living-login-box">'
    //                + '<p class="living-name-box"><input class="living-nikename" type="text" id="txtNikeName" placeholder="请输入昵称" /><span class="living-inqueired">*</span></p>'
    //                + '</p></div>',
    //        yes: function(index, layero){
    //            var nick_name = $('.living-nikename').val().trim();
    //            if (_.isEmpty(nick_name)) {
    //                $('.living-inqueired').show();
    //                return;
    //            };
    //            guessEnter(livingID);
    //        }
    //    });
        
    //}
}
//获取直播信息，并进入直播页面
function enterLiving(livingID, userID, nikeName) {
    var params = {
        LivingID: livingID,
        UserID: userID,
        NikeName: nikeName
    }
    common.call(AppPath + "/Living/LivingUrl", params, 'get', function (result) {
        if (result.Status) {
            var url = result.Data.liveUrl;
            location.href=url;
        } else {
            layer.msg('视频加载失败！');
        }
    }, error)
}
//游客进入直播
function guessEnter(livingID) {
    var userID = new GUID().newGUID();
    var nikeName = $('#txtNikeName').val();
    enterLiving(livingID, userID, nikeName);
    layer.closeAll();
}
//历史回放
function historyClick(livingID) {   
    if (_.isEmpty(Cookies.get('cookie_userid'))) {
        window.location.href = AppPath + '/Login/index';
    }
    else {
        var params = {
            LivingID: livingID,
            UserID: Cookies.get('cookie_userid'),
            NikeName: Cookies.get('cookie_username')
        };
        common.call(AppPath + "/Living/PlayBackUrl", params, 'get', function (result) {
            if (result.Status) {
                var url = result.Data.playbackUrl;
                location.href = url;
            } else {
                layer.msg(result.Message);
            }
        }, error);
    }   
}
$(function () {
    var livingPageIndex = 1;
    var playBackPageIndex = 1;
    var PageSize = 20;
    var scrollContentHeight = $(document).height() - 104;
    var flag = true;
    $('.swiper-slide').css({ 'height': scrollContentHeight + 'px', 'overflow': 'auto' });
    loadLivingData(livingPageIndex, PageSize);
    playBackData(playBackPageIndex, PageSize);
    //初始化tab
    var swiper = new Swiper('.swiper-container', {
        onSlideChangeStart: function () {
            var _index = swiper.activeIndex;
            $(".nav_bar .item p").removeClass("choose");
            $(".nav_bar .item").eq(_index).find('p').addClass("choose");
        }
    });
    $('.swiper-container').on('touchmove', function () {
        flag = false;
    })
    $('.swiper-container').on('touchend', function () {
        setTimeout(function () {
            flag = true;
        }, 200)
    })

    $(".nav_bar li").click(function () {
        var order = $(this).index();
        $(this).find('p').addClass('choose').parent().siblings().find('p').removeClass('choose');
        swiper.slideTo(order);
        flag = true;
    });
   
    $('#iscroller_one,#iscroller_two').css({ 'height': scrollContentHeight + 'px', 'overflow': 'auto' });
    refresher.init({
        id: "iscroller_one",
        pullDownAction: function () {
            loadLivingData(1, PageSize);
            setTimeout(function () {
               iscroller_one.refresh();
            }, 1000);
        },
        pullUpAction: function () {
            livingPageIndex += 1;
            loadLivingData(livingPageIndex, PageSize, true);
            setTimeout(function () {
                iscroller_one.refresh();
            }, 500);
        },
    });
    refresher.init({
        id: "iscroller_two",
        pullDownAction: function () {
            playBackData(1, PageSize);
            setTimeout(function () {
                iscroller_two.refresh();
            }, 1000);
        },  
        pullUpAction: function () {
            playBackPageIndex += 1;
            playBackData(playBackPageIndex, PageSize,true);
            setTimeout(function () {
                iscroller_two.refresh();
            }, 500);
        },
    });
})