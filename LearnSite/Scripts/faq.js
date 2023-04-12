$(document).ready(function () {

    var isPersonalQuestion = 1;//1我的问题，0大家的问题
    var pageIndex = 1;
    var pageSize = 6;
    var sort = " CreateTime desc";

    QuestionList();

    //我的问题，大家的问题，切换
    $(".question_tab span").click(function () {
        $(this).addClass('cur').siblings().removeClass('cur');
        isPersonalQuestion = $(this).attr("type");
        $("#divContext").html("");
        QuestionList();
    });


    //新建问题 展开收缩
    $('#span_question_operating_add').click(function () {
        var questionAdd = $('.u-studying-faq-new-box');
        $(".u-studying-faq-open").hide(1000);
        if (questionAdd.is(":hidden")) {
            ueEditor1.ready(function () {
                //阻止工具栏的点击向上冒泡
                $(this.container).click(function (e) {
                    e.stopPropagation()
                })
            });
            $('.u-studying-faq-new-box .u-studying-faq-new-editor').append(ueEditor1.container.parentNode);
            questionAdd.show(700);
        } else {
            questionAdd.hide(700);
        }
    });

    //添加取消
    $("#btn_question_item_cancel").click(function () {
        $('#Txt_QuestionTitle').val("");
        $('.u-studying-faq-new-box').hide(700);
    });

    //添加保存问题
    $("#btn_question_item_add").click(function () {
        var questionTitle = $('#Txt_QuestionTitle').val();
        var questionContent = ueEditor1.getContent();

        var showMsg = "";
        if (questionTitle.trim() == "") {
            showMsg = "标题不能为空！";
        } else if (questionContent.replace(/<?[p|P][^>]*>/g, "").replace(/&nbs/g, "").trim() == "") {
            showMsg = "内容不能为空！";
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
                $('.u-studying-faq-new-box').hide(700);
                QuestionList();
            }
        });

    });


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
                $('#btn_QuestionMore').hide();
            } else {
                $(".question-more").show();
                $('#btn_QuestionMore').show();
            }
        } else {
            $(".question-more").hide();
            $('#btn_QuestionMore').hide();
            if (data.msg == '没有更多问题了！' || data.length == 0) {
                $("#divContext").html("<div class='u_question_none'></div>");
            } else {
                $(".u_question_none").remove();
            }
        }
        QuestionContent_SetHeight();
    }

    //问题内容超高自动隐藏
    function QuestionContent_SetHeight() {
        $(".u-studying-faq-text-content-show").each(function () {
            if ($(this).height() <= 50) {
                $(this).find(".u-questioncontent_show_hide").hide();
            } else {
                $(this).find(".u-questioncontent_show_hide").html("<span class='span-show-pic'>&nbsp;&nbsp;&nbsp;</span>展开");
                $(this).append("&#8195;&#8195;&#8195;");
            }
            $(this).removeClass().addClass("u-studying-faq-text-content");
        });
    }


    //更多问题
    $("#btn_QuestionMore").click(function () {
        pageIndex = (pageIndex + 1);
        QuestionList();
    });

    //删除问题
    $('#divContext').delegate('.u-studying-faq-bottom ul .u-studying-faq-delete', 'click', function () {
        var item = $.tmplItem(this);
        var t = this;
        popConfirmBox("确定删除吗？", "删除提示", ["确定", "取消"], function () {
            $.ajax({
                url: AppPath + "/PublicService/Questions.ashx",
                type: 'POST',
                data: { Method: "QuestionDel", QuestionID: item.data.QuestionID },
                dataType: "json",
                success: function (resMsg) {
                    if (resMsg.Data.Status) {
                        $(t).parent().parent().parent().parent().parent().fadeOut("slow", function () { this.remove(); });
                        QuestionList();
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
    });

    //删除问题回复
    $('#divContext').delegate('.u-studying-faq-bottom ul .u-studying-faq-answer-delete', 'click', function () {
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
                        $(t).parent().parent().parent().parent().fadeOut("slow", function () { this.remove(); });
                        $('#span_question_answercount_' + questionid).html((parseInt($('#span_question_answercount_' + questionid).html()) - 1) + "");
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





















    //问题回复 展开收缩
    $('#divContext').delegate('.u-studying-faq-bottom ul .u-studying-faq-noborder-questionitem', 'click', function () {
        ueEditor1.reset();
        var item = $.tmplItem(this);
        $('.u-studying-faq-new-box').hide(700);
        $(".u-studying-faq-open").hide(700);
        var qalist = $('#div_question_answer_list_' + item.data.QuestionID);
        if (qalist.is(":hidden")) {
            //是否关闭回复 0：可回复 1：不可回复
            if (!item.data.IsCloseReply) {
                var divqatxt = $("#div_question_answer_text_" + item.data.QuestionID);
                ueEditor1.ready(function () {
                    //阻止工具栏的点击向上冒泡
                    $(this.container).click(function (e) {
                        e.stopPropagation();
                    });
                });

                if (ueEditor1.container.parentNode != null && $("#div_TempEditor").html() == "") {
                    $("#div_TempEditor").html(ueEditor1.container.parentNode);
                }

                divqatxt.html("");
                divqatxt.append($("#div_TempEditor").html());
                divqatxt.append($("#div_Editor_operating").html());
            }
            qalist.show(700);
            $("#btn_answer_add").data("questionid", item.data.QuestionID);
        } else {
            qalist.hide(700);
        }
    });

    //添加回复
    $("#divContext").delegate('#btn_answer_add', 'click', function () {
        ueEditor1.reset();
        var answerContent = ueEditor1.getContent();
        var questionID = $(this).data('questionid');

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
                $("#div_question_answer_item_" + questionID).prepend($("#AnswerItem").tmpl(answerData.Data));

                ueEditor1.setContent('');
                $('#span_question_answercount_' + questionID).html((parseInt($('#span_question_answercount_' + questionID).html()) + 1) + "");
            }
        });
    });

    

    

    

    //展开与收起
    $('#divContext').delegate('.u-questioncontent_show_hide', 'click', function () {
        var questionContent = $(this).parent();
        if (questionContent.attr('class') == "u-studying-faq-text-content") {
            questionContent.removeClass().addClass("u-studying-faq-text-content-show");
            $(this).html("<span class='span-hide-pic'>&nbsp;&nbsp;&nbsp;</span>收起");
        }
        else {
            questionContent.removeClass().addClass("u-studying-faq-text-content");
            $(this).html("<span class='span-show-pic'>&nbsp;&nbsp;&nbsp;</span>展开");
        }
    });
});




