var uid = Cookies.get('cookie_userid');
if (!_.isEmpty(uid)) {
    var params = { "uid": uid };
    common.call(AppPath+"/Login/PostUserLoginByID", params, 'post', function (data) {
        console.log(data)
        if (data.Status) {
            var userid = data.Data.UserID;
            //把用户信息存入Cookie
          //  console.log(data)
            Cookies.set('cookie_userid', userid);
            Cookies.set('cookie_username', data.Data.RealName);
            Cookies.set('cookie_userPhotoUrl', data.Data.PhotoUrl);
        } else {
           // layer.msg(data.Message)
        };
    }, error)
}
//轮播函数
function carouselFigure() {
    var swiper = new Swiper('.swiper-container', {
        pagination: '.swiper-pagination',
        nextButton: 'null',
        prevButton: 'null',
        paginationClickable: true,
        // spaceBetween: 30,
        centeredSlides: true,
        autoplay: 2500,
        autoplayDisableOnInteraction: false
    });
}
 
//轮播加载数据
function loadBanner() {
    common.call(AppPath+"/Home/GetBannerList", '', 'get', function (data) {
        var template = Handlebars.compile($("#tmpl_BannerSpread").html());
        var renderHTML = template(data);
        $('.swiper-wrapper').html(renderHTML);
        carouselFigure();//banner轮播图
    }, error);
}
//最新资讯
function loadNewNotice() {
    var params = { "PageSize": 2, "PageIndex": 1 };
    common.call(AppPath+"/Home/GetAnnouncementList", params, 'get', function (data) {
        if (data.Status == true && data.Data.length > 0) {
            $('.new-notice').show();
            var template = Handlebars.compile($("#tmpl_NewNotice").html());
            var renderHTML = template(data);
            $('.new-notice ul').html(renderHTML);
            setTimeout(function () {
                var imgLength = $('.new-notice ul li img').length;
                var imgHeight = $('.new-notice ul li img').eq(0).height();
                for (var i = 0; i < imgLength; i++) {
                    if (imgHeight > $('.new-notice ul li img').eq(i).height()) {
                        imgHeight = $('.new-notice ul li img').eq(i).height();
                    }
                }
                $('.new-notice ul li img').height(imgHeight);

            }, 100)
               
        }
    }, error);
}
//推荐课程
function loadCourseRecommend() {
    var params = { "PageSize": 8, "PageIndex": 1 };
    common.call(AppPath+"/Home/GetCourseRecommendList", params, 'get', function (data) {
        if (data.Status == true && data.Data.length > 0) {
            //console.log(data.Data)
            $('.recommend_class').show();
            var template = Handlebars.compile($("#tmpl_CourseRecommend").html());
            var renderHTML = template(data);
            $('.recommend_class .object_list').html(renderHTML);
            //for (var i = 0; i < $('.recommend_class .object_list li').length; i++) {
            //    var microClassHref = $('.recommend_class .object_list li').eq(i).find('a').attr('href');
            //    $('.recommend_class .object_list li').eq(i).find('a').attr('href', microClassHref + '&userid=' + userid);
            //}
        }
    }, error);
}

$(function () {
    loadBanner();//加载banner图
    loadNewNotice()//最新资讯
    loadCourseRecommend();//推荐课程
})