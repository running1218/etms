function LoadNoticeList() {
    $.ajax({
        url: AppPath + "/PublicService/Course.ashx",
        type: 'POST',
        data: {
            Method: "getguidancelist",
            TrainingItemCourseID: GetQueryString('TrainingItemCourseID')
        },
        dataType: "json",
        success: function (data) {
            if (data.Status) {
                if (data.Data.length == 0) {
                    $('.no_content').removeClass('hide');
                } else {
                    for (var i = 0; i < data.Data.length; i++) {
                        var html = "<div class='view-area'>"
                                        + "<div class='title'>"
                                            + "<h1>" + data.Data[i].MainHead + "</h1>"
                                            + "<p>" + data.Data[i].CreateTime + "</p>"
                                        + "</div>"
                                        + "<div class='main_content'>" + data.Data[i].ArticleContent + "</div>"
                                 + "</div>";
                        $("#CourseGuidanceList").append(html);
                    }
                }
            }
        }
    });
}

LoadNoticeList();