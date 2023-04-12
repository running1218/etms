var CourseID = common.GetQueryString("CourseID");//课程ID
var ResourceID = "";

//初始列表
function loadCourseDetail() {
    var params = { "courseID": CourseID };
    common.call(AppPath + "/Course/GetCourseInfo", params, 'get', function (data) {
        if (data.Status == true) {
            $('#bgimg').attr('src', FileUrlRoot + '/CourseLogo/' + data.Data.ThumbnailURL);
            $('#p1').html(data.Data.CourseName);
            $('#p2').html(data.Data.StarDateString + '~' + data.Data.EndDateString);
            $('#p3').html('学习人数：' + data.Data.FocusCount);
            $('.description-content').html(data.Data.CourseIntroduction);
        }
    }, error);
}
function loadLivings()
{
    var params = { "courseID": CourseID };
    common.call(AppPath + "/Living/GetCourseLivings", params, 'get', function (data) {
        if (data.Status == true) {
            if (data.Data.length > 0) {
                var template = Handlebars.compile($("#tmpl_CourseDetailNoneUser").html());
                var renderHTML = template(data);
                $('.object_list').html(renderHTML);
            }
        }
    });
}
$(document).ready(function () {
    loadCourseDetail();
    loadLivings();

    $(".tab-living li").click(function () {
        var order = $(this).index();
        $('.living-active').removeClass('living-active');
        $(this).addClass('living-active');
        
        $('.content-block').addClass('unchoose');
        $('.content-block').eq(order).removeClass('unchoose');
    });
})

function toLivingRoom(livingID, isopen) {
    if (isopen) {
        var userid = Cookies.get('cookie_userid');
        if (!_.isEmpty(userid)) {
            var params = { "livingID": livingID, "userID": userid };
            common.call(AppPath + "/Living/GetLivingUrl", params, 'get', function (data) {
                if (data.Status == true) {
                    if (data.Data == '')
                        layer.msg('暂无直播信息，请耐心等待...');
                    else
                        window.location.href = data.Data;
                }
                else {
                    layer.msg(data.Message);
                }
            });
        }
        else {
            window.location.href = AppPath + '/Login/Index?BackUrl=' + AppPath + '/Course/LivingDetail?CourseID=' + CourseID;
        }
    }
    else {
        return false;
    }
}

 
