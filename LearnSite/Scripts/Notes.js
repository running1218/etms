var PageSize = 6;//每页大小
var PageOn = 1;//第几页
var NotesType = "1";//我的笔记 1，大家的笔记 2
var orderByType = "desc";//排序方式
var ContentID = '';//资源ID
var isedit = false;
var totalsize = 1;
var addUE;
var arrayUE = new Array();


  
function PGetPageList(ContentID) {
    $(".Notes-item-add").hide();
    PageOn = 1;
    GetPageList(ContentID);
}

//获取页面列表
function GetPageList(ContentID) {

    $(".Notes-More").hide();
    isedit = false;
    $.ajax({
        type: 'post',
        url: AppPath + "/PublicService/NotesHandler.ashx",
        data: {
            "PageSize": PageSize,
            "PageOn": PageOn,
            "NotesType": NotesType,
            "orderByType": orderByType,
            "method": "SelectNotes",
            "ContentID": ContentID,
            "TrainingItemCourseID": TrainingItemCourseID
        },
        datatype: "json",
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

                    }
                    $(this).removeClass().addClass("QuestionTmpl-item-content");
                    $(this).children(".span_show_hide").html("<span class='span-show-pic'>&nbsp;&nbsp;&nbsp;</span>展开");
                });

                if (msg.Data.TotalRecords == 0) {
                    $(".Notes-list").html("<div class='note_none'></div>");
                }

            } else {

            }

        }
    });

}


$(document).ready(function () {
    ContentID = GetQueryString('TrainingItemCourseID');
    PGetPageList(ContentID);

    //左侧点击
    $(".Notes-box ul li").click(function () {
        $(".Notes-box ul li").removeClass("Notes-selected-li");
        $(".Notes-box ul li i").removeClass("file_iconH video_iconH");
        $(this).addClass("Notes-selected-li");
        if ($(this).find('i').hasClass('video_icon')) {
            $(this).find('i').addClass('video_iconH');
        } else {
            $(this).find('i').addClass('file_iconH');
        }
        ContentID = $(this).attr("id");
        PGetPageList(ContentID);
    });

    //切换我的笔记和大家的笔记
    $(".Notes-coursmenu ul li a").click(function () {
        $(".Notes-coursmenu ul li a").removeClass("activemenu");
        $(this).addClass("activemenu");

        var tempselectid = $(this).attr("id").toLowerCase().replace("selectnotes_", "");
        if (NotesType != tempselectid) {
            NotesType = tempselectid;
            PageOn = 1;
            GetPageList(ContentID);
        }
        if (NotesType == 1) {

            $(".Notes-add").show();

        }
        else {
            $(".Notes-add").hide();
        }

    });

    //排序
    $(".Notes-sort-Context ul li").click(function () {
        var currentObj = $(this);
        $(".Notes-sort-Context ul li").removeClass();
        orderByType = currentObj.attr("sort_way");
        if (orderByType == "desc") {
            currentObj.addClass("selectdown");
            currentObj.attr("sort_way", 'asc');
        }
        else {
            currentObj.addClass("selectup");
            currentObj.attr("sort_way", 'desc');
        }
        PageOn = 1;
        GetPageList(ContentID);
    });

    //更多
    $(".Notes-More").click(function () {

        if (PageOn * PageSize < totalsize) {
            PageOn = PageOn + 1;
            GetPageList(ContentID);
        }
        else {
            layer.tips("没有更多了", $(this), {
                style: ['background-color:#78BA32; color:#fff', '#78BA32'],
                maxWidth: 185,
                time: 3,
                closeBtn: [0, true]
            });
        }

    });



    //删除事件
    $('.Notes-list').delegate('.remove', 'click', function () {
        var item = $.tmplItem(this);
        var okHandler = function () {

            $.ajax({
                type: 'post',
                url: AppPath + "/PublicService/NotesHandler.ashx",
                data: { "NotesID": item.data.NotesID, "method": "deletenotes" },
                datatype: "json",
                success: function (msg) {
                    if (msg.Status == true) {
                        $("#" + item.data.NotesID).remove();
                    }

                }
            });

        };

        var cancelHandler = function () {
            return;
        };

        popConfirmBox("您确定要删除吗？", "删除笔记", ["确定", "取消"], okHandler, cancelHandler);
    });

    //新建
    $('.Notes-add').click(function () {
        if (!ContentID) {
            layer.tips("请先选定目录", $(this), {
                style: ['background-color:#78BA32; color:#fff', '#78BA32'],
                maxWidth: 185,
                time: 3,
                closeBtn: [0, true]
            });
            return;
        }
        $('.Notes-item-add').find(".bnt-share").get(0).checked = true;

        $('.Notes-item-add').find(".txt-title").val('');
        if (typeof (addUE) === 'undefined') {
            addUE = UE.getEditor('add_content', optionEditor1);
        }
        else {
            addUE.reset();
            setTimeout(function () {
                addUE.setContent('');
            }, 500)
        }
        $(".Notes-item-add").toggle(600);
    });

    //新建保存事件
    $('.Notes-item-add').delegate('.save', 'click', function () {
        var title = $('.Notes-item-add').find(".txt-title").val();
        if (title == "") {
            popAlertBox("标题不能为空");
            return;
        }
        var currentContent = addUE.getContent();

        var ispublic = 0;
        if ($('.Notes-item-add').find(".bnt-share").get(0).checked) {
            ispublic = 1
        }

        $.ajax({
            type: 'post',
            url: AppPath + "/PublicService/NotesHandler.ashx",
            data: {
                "NoteContent": currentContent,
                "title": title,
                "method": "insertnotes",
                "ContentID": ContentID,
                "TrainingItemCourseID": TrainingItemCourseID,
                "IsPublic": ispublic,
            },
            datatype: "json",
            success: function (msg) {
                if (msg.Status == true) {
                    $(".Notes-item-add").toggle(600);
                    PGetPageList(ContentID);
                    addUE.setContent('');

                }
            }
        });

    });

    //新建取消事件
    $('.Notes-item-add').delegate('.cancel', 'click', function () {
        $(".Notes-item-add").toggle(600);
    });

    //修改事件
    $('.Notes-list').delegate('.update', 'click', function () {

        if (isedit == false) {

            var item = $.tmplItem(this);

            if (typeof (arrayUE[item.data.NotesID]) === 'undefined') {

                var content = $('#' + item.data.NotesID).children(".Notes-item-content").find("#content").html();

                arrayUE[item.data.NotesID] = UE.getEditor('content_' + item.data.NotesID, optionEditor1);

                arrayUE[item.data.NotesID].reset();
                setTimeout(function () {
                    arrayUE[item.data.NotesID].setContent(content);
                }, 500);

            }
            else {
                var currentContent = $('#' + item.data.NotesID).children(".Notes-item-content").find("#content").html();
                arrayUE[item.data.NotesID].reset();
                setTimeout(function () {
                    arrayUE[item.data.NotesID].setContent(currentContent);
                }, 500)

            }

            $('#' + item.data.NotesID).children(".Notes-item-content").toggle();
            $('#' + item.data.NotesID).children(".Notes-item-edit").toggle(600);
            isedit = true;

        }
        else {
            layer.tips("请先提交正在编辑的", $(this), {
                style: ['background-color:#78BA32; color:#fff', '#78BA32'],
                maxWidth: 185,
                time: 3,
                closeBtn: [0, true]
            });
        }
    });

    //修改保存事件
    $('.Notes-list').delegate('.save', 'click', function () {
        var item = $.tmplItem(this);
        var title = $('#' + item.data.NotesID).find(".txt-title").val();
        if (title == "") {
            popAlertBox("标题不能为空");
            return;
        }
        var currentContent = arrayUE[item.data.NotesID].getContent();

        var ispublic = 0;
        if ($('#' + item.data.NotesID).find(".bnt-share").get(0).checked) {
            ispublic = 1
        }
        
        $.ajax({
            type: 'post',
            url: AppPath + "/PublicService/NotesHandler.ashx",
            data: {
                "NotesID": item.data.NotesID,
                "NoteContent": currentContent,
                "title": title,
                "method": "updatenotes",
                "IsPublic": ispublic
            },
            datatype: "json",
            success: function (msg) {
                if (msg.Status == true) {
                    $('#' + item.data.NotesID).children(".Notes-item-content").children(".Notes-item-title").html(title);
                    $('#' + item.data.NotesID).children(".Notes-item-content").find("#content").html(currentContent);
                    
                    isedit = false;
                    $('#' + item.data.NotesID).children(".Notes-item-edit").toggle(600);
                    $('#' + item.data.NotesID).children(".Notes-item-content").toggle(600);



                    var contentshow = $('#' + item.data.NotesID).children(".Notes-item-content").children("#show_" + item.data.NotesID);

                    if (contentshow.attr("class") == "QuestionTmpl-item-content-show") {
                        if (contentshow.height() <= 65) {
                            contentshow.find(".span_show_hide").hide();
                            contentshow.removeClass().addClass("QuestionTmpl-item-content");
                            contentshow.find(".span_show_hide").html("<span class='span-show-pic'>&nbsp;&nbsp;&nbsp;</span>展开");
                        } else {

                            contentshow.find(".span_show_hide").show();
                        }
                    }
                    else {
                        contentshow.removeClass().addClass("QuestionTmpl-item-content-show");
                        if (contentshow.height() <= 65) {
                            contentshow.find(".span_show_hide").hide();

                        } else {

                            contentshow.find(".span_show_hide").show();
                        }
                        contentshow.removeClass().addClass("QuestionTmpl-item-content");
                    }


                }
            }
        });

    });

    //浏览事件
    $('.Notes-list').delegate('.u-studying-note-bottom-time', 'click', function () {
        var item = $.tmplItem(this);
        if ((item.data.ResourceType == 1) && (item.data.ContentType == 1)) {
            showLayerWindow(" ", AppPath + "/Note/Preview.aspx?CourseID=" + item.data.CourseID + "&ChapterID=" + item.data.ChapterID + "&KnowledgeID=" + item.data.SectionID + "&ChapterResourceID=" + item.data.ChapterResourceID + "&playStart=" + item.data.PlayingFlag, "800", "506");
        }
        else {
            showLayerWindow(" ", AppPath + "/Note/Preview.aspx?ChapterResourceID=" + item.data.ChapterResourceID + "&playStart=" + item.data.PlayingFlag, "800", "640");
        }

    });

    //修改取消事件
    $('.Notes-list').delegate('.cancel', 'click', function () {

        var item = $.tmplItem(this);
        isedit = false;
        $('#' + item.data.NotesID).children(".Notes-item-edit").toggle(600);
        $('#' + item.data.NotesID).children(".Notes-item-content").toggle(600);

    });



});
