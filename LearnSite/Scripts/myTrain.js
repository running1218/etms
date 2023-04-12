function GetCourseList(id) {
    $.ajax({
        type: 'get',
        url: AppPath + "/PublicService/Course.ashx",
        data: { "TrainingItemCourseID": id, "method": "getTrainCourses", },
        datatype: "json",
        beforeSend: Ajaxloading,
        success: function (msg) {
            if (msg.Status == true) {
                if (PageOn == 1) {
                    $(".Notes-list").html("");
                }
                $("#NotesItem").tmpl(msg.Data.DataList).appendTo('.Notes-list');
                totalsize = msg.Data.TotalRecords;
                if (PageOn * PageSize < totalsize) {
                    $(".Notes-More").show();
                }
                else {
                    $(".Notes-More").hide();
                }
                $(".QuestionTmpl-item-content-show").each(function () {
                    if ($(this).height() <= 65) {
                        $(this).find(".span_show_hide").hide();
                    } else {

                        // $(this).append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
                    }
                    $(this).removeClass().addClass("QuestionTmpl-item-content");
                    $(this).children(".span_show_hide").html("<span class='span-show-pic'>&nbsp;&nbsp;&nbsp;</span>展开");
                });

                if (msg.Data.TotalRecords == 0) {
                    $(".Notes-list").html("<div class='note_none'></div>");
                }

            } else {
                //  layer.tips(data.msg, $("#btn_QuestionMore"), { style: ['background-color:#78BA32; color:#fff', '#78BA32'], maxWidth: 185, time: 3, closeBtn: [0, true] });
            }

        },
        error: QAError
    });
}
//ajax加载时友好显示
function Ajaxloading() {
    //$("#divContext").html("<div style='padding:100px 0px 0px 45%'>数据加载中……</div>");
}