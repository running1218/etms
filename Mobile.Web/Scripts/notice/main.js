//refresher
var PageIndex = 1; //页
var PageSize = 8;//数量
function Refresh() {
    setTimeout(function () {
        PageIndex = 1;
        loadNoticeList(false, PageSize, PageIndex);//加载资讯（公告）列表
        setTimeout(function () {
            wrapper.refresh();
        }, 100)
    }, 1000);
}

function Load() {
    setTimeout(function () {
        PageIndex += 1;
        loadNoticeList(true, PageSize, PageIndex);//加载资讯（公告）列表
        setTimeout(function () {
            wrapper.refresh();
        }, 100)
    }, 1000);
}
//资讯（公告）列表
function loadNoticeList(isAdd, PageSize, PageIndex) {
    var params = { "PageSize": PageSize, "PageIndex": PageIndex };
    common.call(AppPath+"/Notice/GetAnnouncementList", params, 'get', function (data) {
        if (data.Data.length == 0) {
            $('.pullUpLabel').text('暂无内容');
          
        } else if (data.Status == true && data.Data.length > 0) {
            console.log(data.Data)
            $('.course_list').show();
            var template = Handlebars.compile($("#tmpl_NoticeList").html());
            var renderHTML = template(data);
            if (isAdd) {
                $('#listDiv').append(renderHTML);
            } else {
                $('#listDiv').html(renderHTML);
            }

        }
    }, error);
}
//方法调用
$(function () {
    loadNoticeList(false, PageSize, PageIndex);//加载资讯（公告）列表
    setTimeout(function () {
            refresher.init({
                id: "wrapper",
                pullDownAction: Refresh,
                pullUpAction: Load
            });
        
    }, 100);
})