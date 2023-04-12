var isedit = false;
var addNotesUE;
var noteEditor;
var arrayNotesUE = new Array();
var NotesPageOn = 1;
var NotesPageSize = 6;
var NotesType = "1";//我的笔记 1，大家的笔记 2
var orderByType = "desc";//排序方式
var Notestotalsize = 1;


GetNotesPageList();

//我的问题，大家的问题，切换
$(".note_tab span").click(function () {
    $(this).addClass('cur').siblings().removeClass('cur');
    NotesType = $(this).attr("type");
    $("#Noteslist").html("");
    GetNotesPageList();
});

function GetPNotesPageList() {
    $("#btn_noteMore").hide();
    $(".note-more").hide();
    isedit = false;
    $.ajax({
        type: 'post',
        url: AppPath + "/PublicService/NotesHandler.ashx",
        data: {
            "PageSize": NotesPageSize,
            "PageOn": NotesPageOn,
            "NotesType": NotesType,
            "orderByType": orderByType,
            "ContentID": ContentID,
            "method": "SelectNotes",
            "TrainingItemCourseID": TrainingItemCourseID
        },
        datatype: "json",
        success: function (msg) {
            if (msg.Status == true) {
                if (NotesPageOn == 1) {
                    $("#Noteslist").html("");
                }

                $("#NotesItem").tmpl(msg.Data.DataList).appendTo('#Noteslist');

                Notestotalsize = msg.Data.TotalRecords;
                if (NotesPageOn * NotesPageSize < Notestotalsize) {
                    $(".note-more").show();
                    $("#btn_noteMore").show();
                }
                else {
                    $(".note-more").hide();
                    $("#btn_noteMore").hide();
                }

                $(".u-studying-note-text-content-show").each(function () {
                    if ($(this).height() <= 50) {
                        $(this).find(".u-questioncontent_show_hide").hide();
                    } else {

                    }
                    $(this).removeClass().addClass("u-studying-note-text-content");
                    $(this).children(".u-questioncontent_show_hide").html("<span class='span-show-pic'>&nbsp;&nbsp;&nbsp;</span>展开");
                });
                if (msg.Data.TotalRecords == 0) {
                    $("#Noteslist").html("<div class='u_note_none'></div>");
                } else {
                    $(".u_note_none").remove();
                }
            } else {

            }



        }
    });
}
$(function () {
    noteEditor = UE.getEditor('noteEditor', optionUENotes);
})
//更多
$("#btn_noteMore").click(function () {

    if (NotesPageOn * NotesPageSize < Notestotalsize) {
        NotesPageOn = NotesPageOn + 1;
        GetPNotesPageList();
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


//展开与收起
$('#Noteslist').delegate('.u-questioncontent_show_hide', 'click', function () {
    var questionContent = $(this).parent();
    if (questionContent.attr('class') == "u-studying-note-text-content") {
        questionContent.removeClass().addClass("u-studying-note-text-content-show");
        $(this).html("<span class='span-hide-pic'>&nbsp;&nbsp;&nbsp;</span>收起");
    }
    else {
        questionContent.removeClass().addClass("u-studying-note-text-content");
        $(this).html("<span class='span-show-pic'>&nbsp;&nbsp;&nbsp;</span>展开");
    }
});

//获取页面列表
function GetNotesPageList() {
    $(".u-studying-note-new-box").hide();
    NotesPageOn = 1;
    GetPNotesPageList();

}


$(".u-studying-note-new-box").hide();



//新建
$('#NoteAdd').click(function () {

    $('.u-studying-note-new-box').find("input[type='checkbox']").get(0).checked = true;

    $('.u-studying-note-new-box').find("#txt_title").val('');
    if (typeof (addNotesUE) === 'undefined') {
        addNotesUE = UE.getEditor('add_Notes_Content', optionUENotes);
    }
    else {
        addNotesUE.reset();
        setTimeout(function () {
            addNotesUE.setContent('');
        }, 500)
    }
    $(".u-studying-note-new-box").toggle(600);
});

//新建保存事件
$('.u-studying-note-new-box').delegate('#btn_note_item_add', 'click', function () {
    var title = $('.u-studying-note-new-box').find("#txt_title").val();
    if (title == "") {
        popAlertBox("标题不能为空");
        return;
    }
    var currentContent = addNotesUE.getContent();
    addNotesUE.setContent('');//清空
    $('.u-studying-note-new-box').find("#txt_title").val('');//清空
    var ispublic = 0;
    if ($('.u-studying-note-new-box').find("input[type='checkbox']").get(0).checked) {
        ispublic = 1
    }

    $.ajax({
        type: 'post',
        url: AppPath + "/PublicService/NotesHandler.ashx",
        data: {
            "ContentID": ContentID,
            "TrainingItemCourseID": TrainingItemCourseID,
            "IsPublic": ispublic,
            "NoteContent": currentContent,
            "title": title,
            "method": "insertnotes"
        },
        datatype: "json",
        success: function (msg) {
            if (msg.Status == true) {
                $(".u-studying-note-new-box").toggle(600);
                NotesPageOn = 1;
                GetPNotesPageList();

            }
        }
    });

});

//新建取消事件
$('.u-studying-note-new-box').delegate('#btn_note_item_cancel', 'click', function () {
    $(".u-studying-note-new-box").toggle(600);
});

//删除事件
$('#Noteslist').delegate('.u-studying-note-delete', 'click', function () {
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
                    GetPNotesPageList();
                }
            }
        });

    };

    var cancelHandler = function () {
        return;
    };

    popConfirmBox("您确定要删除吗？", "删除笔记", ["确定", "取消"], okHandler, cancelHandler);
});


//修改事件
$('#Noteslist').delegate('.u-studying-note-editor', 'click', function () {
    if (isedit == false) {
        var item = $.tmplItem(this);
        if (item.data.IsFavorite != 1) {
            if ($('#share_' + item.data.NotesID).attr("class") == "u-studying-note-share") {
                $('#note_edit_' + item.data.NotesID).find("input[type='checkbox']").get(0).checked = true;
            }
            else {
                $('#note_edit_' + item.data.NotesID).find("input[type='checkbox']").removeAttr("checked");
            }
        }



        var content = $('#note_text_' + item.data.NotesID).find("#Notecontent").html();

        $('#content_' + item.data.NotesID).html($("#noteEditor").show());
        noteEditor.ready(function () {
            setTimeout(function () { noteEditor.setContent(content); }, 100);

        });

        $('#note_text_' + item.data.NotesID).toggle();
        $('#note_edit_' + item.data.NotesID).toggle(600);
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
$('#Noteslist').delegate('#notesave', 'click', function () {
    var item = $.tmplItem(this);
    var title = $('#note_edit_' + item.data.NotesID).find(".u-studying-note-text-title").val();
    if (title == "") {
        popAlertBox("标题不能为空");
        return;
    }
    var currentContent = noteEditor.getContent();

    var ispublic = 0;
    if ($('#note_edit_' + item.data.NotesID).find("input[type='checkbox']").get(0).checked) {
        ispublic = 1
    }

    $.ajax({
        type: 'post',
        url: AppPath + "/PublicService/NotesHandler.ashx",
        data: {
            "NotesID": item.data.NotesID,
            "NoteContent": currentContent,
            "title": title,
            "IsPublic": ispublic,
            "method": "updatenotes"
        },
        datatype: "json",
        success: function (msg) {
            if (msg.Status == true) {
                $("#NoteAdd").append($("#noteEditor").hide());
                $('#note_text_' + item.data.NotesID).children(".u-studying-note-text-title").html(title);
                $('#note_text_' + item.data.NotesID).find("#Notecontent").html(currentContent);
                if (item.data.IsFavorite != 1) {
                    if (ispublic == 1) {
                        $('#share_' + item.data.NotesID).removeClass().addClass("u-studying-note-share");
                        $('#share_' + item.data.NotesID).attr("title", "取消分享");
                    }
                    else {
                        $('#share_' + item.data.NotesID).removeClass().addClass("u-studying-note-share-cancel");
                        $('#share_' + item.data.NotesID).attr("title", "分享");
                    }
                }
                isedit = false;
                $('#note_text_' + item.data.NotesID).toggle(600);
                $('#note_edit_' + item.data.NotesID).toggle(600);


                var contentshow = $('#' + item.data.NotesID).find("#show_" + item.data.NotesID);

                if (contentshow.attr("class") == "u-studying-note-text-content-show") {
                    if (contentshow.height() <= 50) {
                        contentshow.find(".u-questioncontent_show_hide").hide();
                        contentshow.removeClass().addClass("u-studying-note-text-content");
                        contentshow.find(".u-questioncontent_show_hide").html("<span class='span-show-pic'>&nbsp;&nbsp;&nbsp;</span>展开");
                    } else {

                        contentshow.find(".u-questioncontent_show_hide").show();
                    }
                }
                else {
                    contentshow.removeClass().addClass("u-studying-note-text-content-show");
                    if (contentshow.height() <= 50) {
                        contentshow.find(".u-questioncontent_show_hide").hide();

                    } else {

                        contentshow.find(".u-questioncontent_show_hide").show();
                    }
                    contentshow.removeClass().addClass("u-studying-note-text-content");
                }


            }
        }
    });

});

//修改取消事件
$('#Noteslist').delegate('#notecancel', 'click', function () {
    $("#NoteAdd").append($("#noteEditor").hide());
    var item = $.tmplItem(this);
    isedit = false;
    $('#note_text_' + item.data.NotesID).toggle(600);
    $('#note_edit_' + item.data.NotesID).toggle(600);

});


//分享事件
$('#Noteslist').delegate('.u-studying-note-share-cancel', 'click', function () {
    var item = $.tmplItem(this);
    var currentObj = $(this);
    ShareNote(currentObj, item.data.NotesID);
});

//取消分享事件
$('#Noteslist').delegate('.u-studying-note-share', 'click', function () {
    var item = $.tmplItem(this);
    var currentObj = $(this);
    ShareNote(currentObj, item.data.NotesID);
});


//分享
function ShareNote(currentObj, NotesID) {

    $.ajax({
        async: false,
        type: 'post',
        url: AppPath + "/PublicService/NotesHandler.ashx",
        data: { "NotesID": NotesID, "method": "sharenotes" },
        datatype: "json",
        success: function (msg) {
            if (msg.Status == true) {
                if (currentObj.attr("class") == "u-studying-note-share-cancel") {
                    currentObj.removeClass().addClass("u-studying-note-share");
                    currentObj.attr("title", "取消分享");
                    layer.tips("分享成功", currentObj, {
                        style: ['background-color:#78BA32; color:#fff', '#78BA32'],
                        maxWidth: 185,
                        time: 3,
                        closeBtn: [0, true]
                    });

                }
                else {
                    currentObj.removeClass().addClass("u-studying-note-share-cancel");
                    currentObj.attr("title", "分享");
                    layer.tips("取消分享成功", currentObj, {
                        style: ['background-color:#78BA32; color:#fff', '#78BA32'],
                        maxWidth: 185,
                        time: 3,
                        closeBtn: [0, true]
                    });
                }

            }

        }
    });
}
