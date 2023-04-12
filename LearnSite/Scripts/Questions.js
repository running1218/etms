var ContentID = '';//资源ID
var isPersonalQuestion = 1;//1我的问题，0大家的问题
var pageIndex = 1;
var pageSize = 10;
var sort = " CreateTime desc";

$(document).ready(function () {
    $(".Notes-coursmenu ul li a").click(function () {
        $(".Notes-coursmenu ul li a").removeClass("activemenu");
        $(this).addClass("activemenu");
        ContentID = GetQueryString('TrainingItemCourseID');
    })


    //子节点 点击事件
    $(".Notes-box ul li").click(function () {
        $(".Notes-box ul li").removeClass("Notes-selected-li");
        $(this).addClass("Notes-selected-li");
        $('#Txt_QuestionTitle').val("");
        $('.question-item-add').hide(700);
        ContentID = $(this).attr("id");
        QuestionList();
    });

    //全部
    //$(".Notes-box ul .all").click(function () {
    //    var id = $(".Notes-coursmenu ul .activemenu").attr("id");
    //    isPersonalQuestion = (id == 'btn_PersonalQuestion' ? 1 : 0);
    //    pageIndex = 1;
    //    QuestionList();
    //});

    //请求问题列表数据
    function QuestionList() {
        $.ajax({
            url: AppPath + "/PublicService/Questions.ashx",
            type: 'POST',
            data: {
                Method: "QuestionTmpl",
                PageIndex: pageIndex,
                PageSize: pageSize,
                SortExpression: sort,
                IsPersonalQuestion: isPersonalQuestion,
                TrainingItemCourseID: TrainingItemCourseID,
                ContentID: ContentID
            },
            dataType: "json",
            success: QuestionSuccess
        });
    }


    //问题列表调用成功执行的方法
    function QuestionSuccess(data) {
        if (pageIndex == 1) {
            $("#divContext").html("");
        }
        if (data.msg == null) {
            $("#QuestionItem").tmpl(data.Data).appendTo('#divContext');
            if (data.Data[0].TotalRecordCount <= (pageIndex * pageSize)) {
                $(".question-more").hide();
            } else {
                $(".question-more").show();
            }
        } else {
            $(".question-more").hide();
            $("#divContext").html("<div class='question_none'></div>");
        }
        QuestionContent_SetHeight();
    }

    //问题内容超高自动隐藏
    function QuestionContent_SetHeight() {
        $(".QuestionTmpl-item-content-show").each(function () {
            if ($(this).height() <= 50) {
                $(this).find(".span_show_hide").hide();
            } else {
                $(this).find(".span_show_hide").html("<span class='span-show-pic'>&nbsp;&nbsp;&nbsp;</span>展开");
                $(this).append("&#8195;&#8195;&#8195;");
            }
            $(this).removeClass().addClass("QuestionTmpl-item-content");
        });
    }


    //我的问题
    $("#btn_PersonalQuestion").click(function () {
        $(".question-operating-add").show(300);
        pageIndex = 1;
        isPersonalQuestion = 1;
        QuestionList();
    });

    //大家的问题
    $("#btn_QuestionAll").click(function () {
        $(".question-operating-add").hide(300);
        $('.question-item-add').hide(1000);
        pageIndex = 1;
        isPersonalQuestion = 0;
        QuestionList();
    });

    //更多问题
    $("#btn_QuestionMore").click(function () {
        pageIndex = (pageIndex + 1);
        QuestionList();
    });

    //时间排序
    $("#question_list_datesort").click(function () {
        pageIndex = 1;
        $("#question_list_answercountsort").removeClass().addClass("answer-count-sort-none");

        var datesort = $("#question_list_datesort");
        if (datesort.attr('class') == "date-sort-asc" || datesort.attr('class') == "date-sort-none") {
            datesort.removeClass().addClass('date-sort-desc');
            sort = " CreateTime desc";
        } else {
            datesort.removeClass().addClass('date-sort-asc');
            sort = " CreateTime asc";
        }
        QuestionList();
    });

    //回复数排序
    $("#question_list_answercountsort").click(function () {
        pageIndex = 1;
        $("#question_list_datesort").removeClass().addClass("date-sort-none");

        var answercountsort = $("#question_list_answercountsort");
        if (answercountsort.attr('class') == "answer-count-sort-asc" || answercountsort.attr('class') == "answer-count-sort-none") {
            answercountsort.removeClass().addClass('answer-count-sort-desc');
            sort = " AnswerCount desc";
        } else {
            answercountsort.removeClass().addClass('answer-count-sort-asc');
            sort = " AnswerCount asc";
        }
        QuestionList();
    });


    //删除问题
    $('#divContext').delegate('.QuestionTmpl-item-operating .remove', 'click', function () {
        var item = $.tmplItem(this);
        var t = this;
        if (item.data.UserID != $("#btn_PersonalQuestion").data("userid")) {
            layer.tips("不能删除别人的问题！", this, { style: ['background-color:#78BA32; color:#fff', '#78BA32'], maxWidth: 185, time: 3, closeBtn: [0, true] });
            return;
        } else {
            popConfirmBox("确定删除吗？", "删除提示", ["确定", "取消"], function () {
                $.ajax({
                    url: AppPath + "/PublicService/Questions.ashx",
                    type: 'POST',
                    data: { Method: "QuestionDel", QuestionID: item.data.QuestionID },
                    dataType: "json",
                    success: function (resMsg) {
                        if (resMsg.Data.Status) {
                            $(t).parent().parent().fadeOut("slow", function () { $(t).remove(); });
                        } else {
                            layer.tips(resMsg.Data.Message, t, {
                                style: ['background-color:#78BA32; color:#fff', '#78BA32'],
                                maxWidth: 185,
                                time: 2,
                                closeBtn: [0, true]
                            });
                        }
                    }
                });
            }, function () {
                return;
            });
        }
    });

    //删除问题回复
    $('#divContext').delegate('.QuestionTmpl-Answer-item-operating .remove', 'click', function () {
        var item = $.tmplItem(this);
        var t = this;
        var questionid = $(t).data("questionid");
        popConfirmBox("确定删除回复吗？", "删除提示", ["确定", "取消"], function () {
            $.ajax({
                url: AppPath + "/PublicService/QuestionAnswer.ashx",
                type: 'POST',
                data: { Method: "QuestionAnswerDel", AnswerID: $(t).data("answerid") },
                dataType: "json",
                success: function (resMsg) {
                    if (resMsg.Status) {
                        $(t).parent().parent().fadeOut("slow", function () { this.remove(); });
                        $('#question_answer_count_' + questionid).html((parseInt($('#question_answer_count_' + questionid).html()) - 1) + "");
                    } else {
                        layer.tips(resMsg.Message, t, {
                            style: ['background-color:#78BA32; color:#fff', '#78BA32'],
                            maxWidth: 185,
                            time: 2,
                            closeBtn: [0, true]
                        });
                    }
                }
            });
        }, function () {
            return;
        });
    });

    //新建问题 展开与收缩
    $('.question-operating-add').click(function () {
        if (!ContentID) {
            layer.tips('请先选定目录', '.question-operating-add', {
                tips: [1, '#3595CC'],
                time: 4000
            });
            return;
        }
        $(".QuestionTmpl-AnswerList").hide(700);
        var qadd = $('.question-item-add');
        if (qadd.is(":hidden")) {
            ueEditor1.ready(function () {
                //阻止工具栏的点击向上冒泡
                $(this.container).click(function (e) {
                    e.stopPropagation()
                })
            });
            $('.question-item-add .content').append(ueEditor1.container.parentNode);
            qadd.show(700);
        } else {
            qadd.hide(700);
        }
    });

    //新建问题 保存
    $('#btn_question_item_add').click(function () {
        var questionTitle = $('#Txt_QuestionTitle').val();
        var questionContent = ueEditor1.getContent();
        var showMsg = "";
        if (questionTitle.trim() == "") {
            showMsg = "问题标题不能为空！";
        } else if (questionContent.replace(/<?[p|P][^>]*>/g, "").replace(/&nbs/g, "").trim() == "") {
            showMsg = "问题内容不能为空！";
        }
        if (showMsg != "") {
            layer.tips(showMsg, this, { style: ['background-color:#78BA32; color:#fff', '#78BA32'], maxWidth: 185, time: 2, closeBtn: [0, true] });
            return;
        }

        //添加问题
        $.ajax({
            url: AppPath + "/PublicService/Questions.ashx",
            type: 'POST',
            data: {
                Method: "QuestionAdd",
                QuestionTitle: questionTitle,
                QuestionContent: questionContent,
                TrainingItemCourseID: TrainingItemCourseID,
                ContentID: ContentID
            },
            dataType: "json",
            success: function (questionItem) {
                $('#Txt_QuestionTitle').val("");
                $("#divContext").prepend($("#QuestionItem").tmpl(questionItem));
                QuestionContent_SetHeight();
                $('.question-item-add').hide(700);
            }
        });
    });

    //新建问题 取消
    $('#btn_question_item_cancel').click(function () {
        $('#Txt_QuestionTitle').val("");
        $('.question-item-add').hide(700);
    });

    //问题回复
    $('#divContext').delegate('.answer', 'click', function () {
        $(".div_question_answer_text").html('');
        var item = $.tmplItem(this);
        //ueEditor1.reset();
        //ueEditor1.setContent('');
        var divqa = $("#div_question_answer_" + item.data.QuestionID);
        var divqatxt = $("#div_question_answer_text_" + item.data.QuestionID);
        $('.question-item-add').hide(700);
        $(".QuestionTmpl-AnswerList").hide(700, function () {
            //$(".QuestionTmpl-AnswerList .question-answer-text").remove();
        });
        if (divqa.is(":hidden")) {
            //是否关闭回复 0：可回复 1：不可回复
            if (!item.data.IsCloseReply) {
                ueEditor1.ready(function () {
                    //阻止工具栏的点击向上冒泡
                    $(this.container)
                        .click(function (e) {
                            e.stopPropagation();
                        });
                });


                if (ueEditor1.container.parentNode != null && $("#div_TempEditor").html() == "") {
                    $("#div_TempEditor").html(ueEditor1.container.parentNode);
                }

                divqatxt.html("");
                divqatxt.append($("#div_TempEditor").html());
                $("#div_TempEditor").html('');
                divqatxt.append($("#div_Editor_operating").html());
            }
            divqa.show(700);
            $("#btn_answer_add").data("questionid", item.data.QuestionID);
        } else {
            divqa.hide(700, function () {
                divqatxt.html("");
            });
        }
    });

    //添加回复
    $("#btn_answer_add").livequery('click', function () {
        var answerContent = ueEditor1.getContent();
        var questionID = $(this).data('questionid');
        //if (answerContent.replace(/<[^>]+>/g, "").replace(/&nbsp;/g, "").trim() == "") {
        if (answerContent.replace(/<?[p|P][^>]*>/g, "").replace(/&nbs/g, "").trim() == "") {
            layer.tips("回复内容不能为空！", this, { style: ['background-color:#78BA32; color:#fff', '#78BA32'], maxWidth: 185, time: 3, closeBtn: [0, true] });
            return;
        }
        //添加回答
        $.ajax({
            url: AppPath + "/PublicService/QuestionAnswer.ashx",
            type: 'POST',
            data: { Method: "QuestionAnswerAdd", QuestionID: questionID, AnswerContent: answerContent },
            dataType: "json",
            success: function (answerData) {
                $("#div_question_answer_list_" + questionID).prepend($("#AnswerItem").tmpl(answerData.Data));
                ueEditor1.reset();
                ueEditor1.setContent('');
                $('#question_answer_count_' + questionID).html((parseInt($('#question_answer_count_' + questionID).html()) + 1) + "");
            }
        });
    });

    //页面加载完默认显示 我的问题
    $('#btn_PersonalQuestion').trigger("click");
});