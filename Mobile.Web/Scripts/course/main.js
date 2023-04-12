 
$(function () {
    //refresher
    setTimeout(function () {
        refresher.init({
            id: "wrapper",
            pullDownAction: Refresh,
            pullUpAction: Load
        });
    }, 100);

    loadCourseType();//课程类型
   // searchCourse(false, PageSize, PageIndex, typeId, searchVal);//课程列表
    var documentHeight = $(document).outerHeight(true);
    var tit_boxHeight = $('.search-box').outerHeight(true) + $('.course_type').outerHeight(true);
    var footer_height = $("footer").outerHeight(true);
    $('#wrapper,.course_list').css({ 'height': documentHeight - tit_boxHeight - footer_height + 'px', "padding-bottom": "3rem" });
    //搜索框搜索事件
    $("#search_icon").click(function () {
        searchVal = $(".search").val();
        PageIndex = 1;
        RefreshFun();
    });
    $(document).keydown(function (event) {
        if (event.keyCode == 13) {
            $("#search_icon").click();
        };
    });

})
var typeId = '0';// 课程类型
var searchVal = '';//搜索关键字、词
var PageIndex = 1; //页
var PageSize = 6;//数量
function Refresh() {
    setTimeout(function () {
        RefreshFun();
    }, 1000);
}
 
function RefreshFun() {
    PageIndex = 1;
    $('.course_list .object_list').html("");
    searchCourse(false, PageSize, PageIndex, typeId, searchVal);
    setTimeout(function () {
        wrapper.refresh();
    }, 100);
}

function Load() {
    setTimeout(function () {
        PageIndex += 1;
            searchCourse(true, PageSize, PageIndex, typeId, searchVal);
            setTimeout(function () {
                wrapper.refresh();
            }, 100);
    }, 1000);
}
//课程类型
function loadCourseType() {
    common.call(AppPath+"/Course/GetCourseTypeList", '', 'get', function (data) {
        if (data.Status == true && data.Data.length > 0) {
            //console.log(data.Data)
            $('.course_type').show();
            var template = Handlebars.compile($("#tmpl_TypeList").html());
            var renderHTML = template(data);
            $('.course_type .type_list').html(renderHTML);
            //类型搜索事件
            $(".course_type li").click(function () {
                $(this).addClass("on").siblings().removeClass("on");
                typeId = $(this).attr("data-CourseTypeID");
                searchVal = $(".search").val();
                RefreshFun();
            });
            $(".course_type li").eq(0).click();
        } 
    }, error);
}
 
//课程数据
function searchCourse(isAdd, PageSize, PageIndex,typeId, searchVal) {
    var params = { "PageSize": PageSize, "PageIndex": PageIndex, "CourseName": searchVal, "CourseTypeID": typeId };
    common.call(AppPath+"/Course/GetCourseList", params, 'get', function (data) {
        if (data.Status == true && data.Data.length > 0) {
            $(".course_list").show();
            console.log(data)
            var template = Handlebars.compile($("#tmpl_CourseList").html());
            var renderHTML = template(data);
            if (isAdd) {
                $('.course_list .object_list').append(renderHTML);
            } else {
                $('.course_list .object_list').html(renderHTML);
            }
        } else {
            if (!isAdd) {
                contentNull($('.course_list .object_list'));
                if (data.Status == false) { layer.msg(data.Message) };
            }
        }
    }, error);
}

 

