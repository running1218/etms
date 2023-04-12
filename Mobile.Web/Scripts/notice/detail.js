function loadNoticeDetail(ArticleID) {
    var params = { "ArticleID": ArticleID };
    common.call(AppPath+"/Notice/GetAnnouncementDetail", params, 'get', function (data) {
        if (data.Status == true) {
            var template = Handlebars.compile($("#tmpl_NoticeDetail").html());
            var renderHTML = template(data);
            $('.main_content').html(renderHTML);

        }
    }, error);
}
var ArticleID = common.GetQueryString("ArticleID");
console.log(ArticleID)
loadNoticeDetail(ArticleID);