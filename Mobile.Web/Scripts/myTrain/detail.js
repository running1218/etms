
var userid = Cookies.get('cookie_userid');
var TrainingItemCourseID = common.GetQueryString("TrainingItemCourseID");
var questionIsFirst = true;
var noteIsFirst = true;
//视频ID
var ResourceType = '';
var ResourceID = '';
var StartTime = '';
var StudyProgress = '';
var indexFlag = 0;//标记学习状态
var ResourceIDLast= '';
//课程学习列表
function loadTrainCourseStudy() {
    var params = { "UserID": userid, "TrainingItemCourseID": TrainingItemCourseID };
    //console.log(params)
    common.call(AppPath + "/MyTrain/GetResourceByTrainingItemCourse", params, 'get', function (data) {
        if (data.Status == true && data.Data.length > 0) {
            var template = Handlebars.compile($("#tmpl_CourseDetailStudy").html());
            var renderHTML = template(data);
            $('#object_list').html(renderHTML);
            //切换学习视频文档播放
            $("#object_list li").click(function () {
                $this = $(this);
                indexFlag = $this.index();
                $this.addClass("on").siblings().removeClass("on");
                ResourceID = $this.attr("data-contentid");
              //  alert(ResourceID)
                ResourceType = $this.attr("data-type");
                loadResource();
                    $('#question_list').html("");
                    $('#note_list').html("");
                    QuestionDown();//问答刷新
                    NoteDown();//笔记刷新
               
            });
            if (!_.isEmpty(ResourceIDLast)) {
                $("#object_list li").each(function () {
                    if ($(this).attr("data-contentid") == ResourceIDLast) {
                        $(this).click();//课程学习视频或文档初次加载
                    }
                })
            }else{
                $("#object_list li").eq(0).click();//课程学习视频或文档初次加载
        }
           
           // ResourceID = $("#object_list li").eq(0).attr("data-contentid");
            loadQuestionList();//加载问答列表
            loadNoteList();//笔记列表
            refresher.init({
                id: "myTrainStudy",
                pullDownAction: function () {
                    setTimeout(function () {
                        myTrainStudy.refresh();
                    }, 1000);
                },
                pullUpAction: function () {
                    setTimeout(function () {
                        myTrainStudy.refresh();
                    }, 1000);
                },
            });
        } else {
            contentNull($('#object_list'));
            if (data.Status == false) {
                //layer.msg(data.Message)
            }
        };
    }, error)
}
//课程公告列表
function loadTrainNoticeStudy() {
    var params = { "TrainingItemCourseID": TrainingItemCourseID };
    common.call(AppPath + "/Course/GetCourseNoticeList", params, 'get', function (data) {
        if (data.Status == true && data.Data.length > 0) {
            //console.log(data.Data)
            var template = Handlebars.compile($("#tmpl_CourseDetailNotice").html());
            var renderHTML = template(data);
            $('#notice_list').html(renderHTML);
            refresher.init({
                id: "noticeDetail",
                pullDownAction: function () {
                    setTimeout(function () {
                        noticeDetail.refresh();
                    }, 1000);
                },
                pullUpAction: function () {
                    setTimeout(function () {
                        noticeDetail.refresh();
                    }, 1000);
                },
            });
        } else {
            contentNull($('#notice_list'));
            if (data.Status == false) {
                //layer.msg(data.Message)
            }
        };
    }, error)
}
//课程测评列表
function loadTrainEvaluationStudy() {
    var params = { "UserID": userid, "TrainingItemCourseID": TrainingItemCourseID };
    common.call(AppPath + "/MyTrain/GetTrainCourseEvaluationList", params, 'get', function (data) {
        if (data.Status == true && data.Data.length > 0) {
            // console.log(data.Data)
            var template = Handlebars.compile($("#tmpl_evaluation_list").html());

            var renderHTML = template(data);
            $('#evaluation_list').html(renderHTML);
            refresher.init({
                id: "evaluationList",
                pullDownAction: function () {
                    setTimeout(function () {
                        evaluationList.refresh();
                    }, 1000);
                },
                pullUpAction: function () {
                    setTimeout(function () {
                        evaluationList.refresh();
                    }, 1000);
                },
            });
        } else {
            contentNull($('#evaluation_list'));
            if (data.Status == false) {
                //layer.msg(data.Message);
            }
        };
    }, error)
}

//加载问答列表
function loadQuestionList() {
    setTimeout(function () {
        if (questionIsFirst) {
            refresher.init({
                id: "question_wrapper",
                pullDownAction: QuestionDown,
                pullUpAction: QuestionUp
            });
        }
    }, 500);
    GetQuestionList(null);
}

//获取问答列表
function GetQuestionList(LastQuestionID) {
    var params = { "UserID": userid, "TrainingItemCourseID": TrainingItemCourseID, "PageSize": 5, ContentID: ResourceID, "LastQuestionID": LastQuestionID };
    common.call(AppPath + "/MyTrain/GetQuestionList", params, 'get', function (data) {
        if (data.Status) {
            Handlebars.registerHelper("formate", function (date, options) {
                return common.formatDate(date, 'yyyy-MM-dd')
            });
            Handlebars.registerHelper("removeLabel", function (str, options) {
                return str.replace(/<.*?>/ig, "")
            });
            Handlebars.registerHelper("compareValue", function (v1, v2, options) {
                if (v1 == v2) {
                    return options.fn(this);
                } else {
                    return options.inverse(this);
                }
            });
            if (data.Data.length > 0) {
                var template = Handlebars.compile($("#tmpl_question_list").html());
                var renderHTML = template(data);
                if (LastQuestionID)
                    $('#question_list').append(renderHTML);
                else
                    $('#question_list').html(renderHTML);
                if ($('#question_list  .discuss_question').length == 0) {
                      $('.question_nocontent').show();
                } else {
                     $('.question_nocontent').hide();
                }

                //跳转问答回复列表
                $(".answer_list").click(function () {
                    var QuestionID = $(this).parents(".discuss_question").attr("QuestionID");
                    window.location.href = AppPath + "/MyTrain/AnswerList?QuestionID=" + QuestionID + "&UserID=" + userid;
                });

                //问答删除
                $(".question_remove").click(function () {
                    var QuestionID = $(this).parents(".discuss_question").attr("QuestionID");
                    swal({
                        title: "确定删除吗?",
                        text: "删除提示",
                        type: "warning",
                        showCancelButton: true,
                        cancelButtonText: "取消",
                        confirmButtonColor: "#DD6B55",
                        confirmButtonText: "确定",
                        closeOnConfirm: false
                    },
                        function () {
                            common.call(AppPath + "/MyTrain/PostRemoveQuestion", { "QuestionID": QuestionID }, 'post', function (data) {
                                var result = JSON.parse(data.Data);
                                if (result.Data.Status) {
                                    $("li[questionid='" + QuestionID + "']").fadeOut("slow", function () { this.remove(); });

                                    swal("删除成功！", "", "success");
                                } else {
                                    sweetAlert("删除失败!", "接口错误", "error");

                                    //layer.msg(result.Data.Message);
                                }
                            });
                        });
 
                });
            } else {
                if (!LastQuestionID)
                    $('.question_nocontent').show();
            }
        } else {
            //layer.msg(data.Message);
        }
    }, error);
}

//显示新增问答
$('.insert_question').livequery('click', function () {
    $(this).hide();
    if (!ResourceID) {
        //layer.msg('没有获取到视频ID');
        return;
    }
    var index = $('.nav_bar p[class="choose"]').parent().index();
    if (index == 3) {
        $(".create_question p label").text("新增问答");
        $(".create_question .title").val("");
        $(".create_question .content").val("");
        $('.create_question').show();
    } else {
        $(".create_note p label").text("新增笔记");
        $(".create_note .title").val("");
        $(".create_note .content").val("");
        $("#note_share").attr("checked", true);
        $(".create_note .send").removeAttr("noteid");
        $('.create_note').show();
    };
    
});

//取消新增问答按钮
$('.create_question .cancel').livequery('click', function () {
    $('.create_question').hide();
    $(".insert_question").show();
});

//新增问答
$(".create_question .send").livequery('click', function () {
    $(".insert_question").show();
    var title = $.trim($(".create_question .title").val());
    var content = $.trim($(".create_question .content").val());
    if (_.isEmpty(title) || _.isEmpty(content)) {
        //layer.msg('主题或内容不能为空');
        return;
    }
    if (!userid || !TrainingItemCourseID || !ResourceID) {
        //layer.msg('没有获取到数据');
        return;
    }
    var params = {
        "TrainingItemCourseID": TrainingItemCourseID, "ContentID": ResourceID, "UserID": userid, "QuestionTitle": title, "QuestionContent": content
    };
    common.call(AppPath + "/MyTrain/PostInsertQuestion", params, 'post', function (data) {
        if (data.Status) {
            $(".create_question").hide();
            $(".create_question .title").val("");
            $(".create_question .content").val("")
            QuestionDown();
        }
    }, error);
});

//问答下滑
function QuestionDown() {
    setTimeout(function () {
        questionIsFirst = false;
        var LastQuestionID = null;
        GetQuestionList(LastQuestionID);
        setTimeout(function () {
            question_wrapper.refresh();
        }, 100);

    }, 1000);
}

//问答上滑
function QuestionUp() {
    setTimeout(function () {
        questionIsFirst = false;
        var LastQuestionID = $('#question_list .discuss_question:last').attr('QuestionID');
        GetQuestionList(LastQuestionID);
        setTimeout(function () {
            question_wrapper.refresh();
        }, 100);
    }, 1000);
}

//加载笔记列表
function loadNoteList() {
    setTimeout(function () {
        if (noteIsFirst) {
            refresher.init({
                id: "note_wrapper",
                pullDownAction: NoteDown,
                pullUpAction: NoteUp
            });
        }
    }, 500);
    GetNoteList(null);
}

//获取笔记列表
function GetNoteList(LastNoteID) {
    var params = { "UserID": userid, "TrainingItemCourseID": TrainingItemCourseID, "PageSize": 5, ContentID: ResourceID, "LastNoteID": LastNoteID };
    common.call(AppPath + "/MyTrain/GetNoteList", params, 'get', function (data) {
        if (data.Status) {
            Handlebars.registerHelper("formate", function (date, options) {
                return common.formatDate(date, 'yyyy-MM-dd');
            });
            Handlebars.registerHelper("removeLabel", function (str, options) {
                return str.replace(/<.*?>/ig, "");
            });
            Handlebars.registerHelper("compareValue", function (v1, v2, options) {
                if (v1 == v2) {
                    return options.fn(this);
                } else {
                    return options.inverse(this);
                }
            });
            if (data.Data.length > 0) {
                var template = Handlebars.compile($("#tmpl_note_list").html());
                var renderHTML = template(data);
                if (LastNoteID)
                    $('#note_list').append(renderHTML);
                else
                    $('#note_list').html(renderHTML);
                if ($('#note_list  .discuss_note').length == 0) {
                     $('.note_nocontent').show();
                } else {
                     $('.note_nocontent').hide();
                }

                //跳转笔记详细
                $(".note_detail").click(function () {
                    var NoteID = $(this).parents(".discuss_note").attr("NoteID");
                    window.location.href = AppPath + "/MyTrain/Note?NoteID=" + NoteID;
                });

                //笔记删除
                $(".note_remove").click(function () {
                    var NoteID = $(this).parents(".discuss_note").attr("NoteID");

                    swal({
                        title: "确定删除吗?",
                        text: "删除提示",
                        type: "warning",
                        showCancelButton: true,
                        cancelButtonText: "取消",
                        confirmButtonColor: "#DD6B55",
                        confirmButtonText: "确定",
                        closeOnConfirm: false
                    },
                       function () {
                           common.call(AppPath + "/MyTrain/PostRemoveNote", { "NoteID": NoteID }, 'post', function (data) {
                               if (data.Status) {
                                   $("li[noteid='" + NoteID + "']").fadeOut("slow", function () { this.remove(); });
                                   swal("删除成功！", "", "success");
                               } else {
                                   sweetAlert("删除失败!", "接口错误", "error");

                                   //layer.msg(result.Data.Message);
                               }
                           });
                       });
 
                });

                //笔记编辑
                $(".note_edit").click(function () {
                    var NoteID = $(this).parents(".discuss_note").attr("NoteID");
                    common.call(AppPath + "/MyTrain/GetNote", { "NoteID": NoteID }, 'get', function (data) {
                        if (data.Status) {
                            $(".create_note p label").text("编辑笔记");
                            $(".create_note .title").val(data.Data.Title);
                            $(".create_note .content").val(data.Data.NoteContent);
                            if (data.Data.IsPublic == 1) {
                                $("#note_share").attr("checked", true);
                            } else {
                                $("#note_share").attr("checked", false);
                            }
                            $(".create_note .send").attr("noteid", NoteID);
                            $('.create_note').show();
                        } else {
                            //layer.msg("获取信息失败");
                        }
                    });
                });
            } else {
                if(!LastNoteID)
                      $('.note_nocontent').show();
            }
        } else {
            //layer.msg(data.Message);
        }
    }, error);
}

//显示新增笔记
//$('.insert_note').livequery('click', function () {
//    if (!ResourceID) {
//        //layer.msg('没有获取到视频ID');
//        return;
//    }
//    $(".create_note p label").text("新增笔记");
//    $(".create_note .title").val("");
//    $(".create_note .content").val("");
//    $("#note_share").attr("checked", true);
//    $(".create_note .send").removeAttr("noteid");
//    $('.create_note').show();
//});

//取消新增笔记按钮
$('.create_note .cancel').livequery('click', function () {
    $('.create_note').hide();
});

//新增笔记
$(".create_note .send").livequery('click', function () {
    var noteid = $(this).attr("noteid");
    var title = $.trim($(".create_note .title").val());
    var content = $.trim($(".create_note .content").val());
    var share = $(".create_note .share").prop("checked") ? 1 : 0;
    if (_.isEmpty(title) || _.isEmpty(content)) {
        //layer.msg('主题或内容不能为空');
        return;
    }
    if (!userid || !TrainingItemCourseID || !ResourceID) {
        //layer.msg('没有获取到数据');
        return;
    }
    if (noteid) {
        var params = {
            "NoteID": noteid, "Title": title, "NoteContent": content, "IsPublic": share
        };
        common.call(AppPath + "/MyTrain/PostUpdateNote", params, 'post', function (data) {
            if (data.Status) {
                $(".create_note").hide();
                $(".create_note .title").val("");
                $(".create_note .content").val("");
                NoteDown();
            } else {
                //layer.msg('编辑保存失败');
            }
        }, error);
    } else {
        var params = {
            "TrainingItemCourseID": TrainingItemCourseID, "ContentID": ResourceID, "UserID": userid, "Title": title, "NoteContent": content, "IsPublic": share
        };
        common.call(AppPath + "/MyTrain/PostInsertNote", params, 'post', function (data) {
            if (data.Status) {
                $(".create_note").hide();
                $(".create_note .title").val("");
                $(".create_note .content").val("");
                NoteDown();
            }
        }, error);
    }
});

//笔记下滑
function NoteDown() {
    setTimeout(function () {
        noteIsFirst = false;
        var LastNoteID = null;
        GetNoteList(LastNoteID);
        setTimeout(function () {
            note_wrapper.refresh();
        }, 100)
    }, 1000);
}

//笔记上滑
function NoteUp() {
    setTimeout(function () {
        noteIsFirst = false;
        var LastNoteID = $('#note_list .discuss_note:last').attr('NoteID');
        GetNoteList(LastNoteID);
        setTimeout(function () {
            note_wrapper.refresh();
        }, 100)
    }, 1000);
}
//加载资源（视频，文档）
function loadResource() {
    var params = { "TrainingItemCourseID": TrainingItemCourseID, "ResourceType": ResourceType, "ResourceID": ResourceID, "StudentID": userid };
    common.call(AppPath + "/Study/GetStudyResourceContent", params, 'get', function (data) {
        if (data.Status == true && ResourceType==1) {
            //视频
            loadvideo(data.Data);

        } else if (data.Status == true && ResourceType == 2) {
            var StudyProgress = data.Data.StudyProgress ? data.Data.StudyProgress-1 : 0;
            var src = data.Data.ImageList[StudyProgress].img ? data.Data.ImageList[StudyProgress].img : 0;
            $('#PlayerBox').html('<a class="pictureBox" href="' + AppPath + '/MyTrain/DocumentPlay?TrainingItemCourseID=' + TrainingItemCourseID + '&ResourceID=' + ResourceID + '"><i class="ico iconfont icon-iconfont-jt"></i><img class="picturePlayer"  src="' + src + '" style="width:100%;height:100%;" /></a>');
            InitializeStudyProgress();
            //  window.location.href = AppPath + "/MyTrain/DocumentPlay?TrainingItemCourseID=" + TrainingItemCourseID + "&ResourceID=" + ResourceID;
        }
    }, error)
}
//初始状态PostInitializeStudyProgress
function InitializeStudyProgress() {
    var params = { "TrainingItemCourseID": TrainingItemCourseID, "ResourceID": ResourceID, "StudentID": userid };
    common.call(AppPath + "/Study/PostInitializeStudyProgress", params, 'post', function (data) {
        if (data.Status == true) {
            StartTime = data.Data;
        } else {
            //layer.msg(data.Message)
        }
    }, error)
}
//终止状态PostTerminateStudyProgress
function TerminateStudyProgress() {
    var params = { "StartTime": StartTime, "StudyProgress": StudyProgress, "TrainingItemCourseID": TrainingItemCourseID, "ResourceID": ResourceID, "StudentID": userid };
    common.call(AppPath + "/Study/PostTerminateStudyProgress", params, 'post', function (data) {
        if (data.Status == true) {
        } else {
            //layer.msg(data.Message)
        }
    }, error)
}
//完成状态PostCompletedStudyProgress
function CompletedStudyProgress() {
    var params = { "TrainingItemCourseID": TrainingItemCourseID, "StudyProgress": StudyProgress, "ResourceID": ResourceID, "StudentID": userid };
    common.call(AppPath + "/Study/PostCompletedStudyProgress", params, 'post', function (data) {
        if (data.Status == true) {
        } else {
            //layer.msg(data.Message)
        }
    }, error)
}
//视频方法
function loadvideo(source) {
    // alert(source.StudyProgress)
    InitializeStudyProgress();
    if (window.player) {
        window.player.dispose();
    }
    var mark = [];//ThumbnailURL
    $('#PlayerBox').html('<video id="videojs-open-player" webkit-playsinline="true"x-webkit-airplay="true"  poster=' + source.ThumbnailURL + '" class="video-js" controls data-setup={"language":"zh-CN"}></video>');
    var player = window.player = videojs('videojs-open-player', {
        controls: true,
        aspectRatio: '16:9',
        plugins: {
            videoJsResolutionSwitcher: {//初始化分辨率
                ui: false, //是否显示选择分辨率的按钮
                "default": '480',
                dynamicLabel: true
            },
            disableProgress: {//初始化禁用滚动条拖动
                autoDisable: false
            },
            recordPoint: {
                finishPct: 10,
                secPerTime: 5
            },
            // 添加logo或水印
            waterMark: {
                file: '',//图片地址../../Images/videologo.png
                xpos: 100,
                ypos: 0,
                xrepeat: 0,
                opacity: 0.5,
            }
        }
    }, function () {
        var player = this;
        window.player = player;
        // 设置缩略图
        player.poster(source.ThumbnailURL);//../../Images/load.jpg
        // 更新视频分辨率
        player.updateSrc([{
            src: source.UrlRoot + '/' + source.DataInfo,
            type: 'video/mp4',
            label: '高清',
            res: '720'
        }, {
            src: source.UrlRoot + '/' + source.DataInfo,
            type: 'video/mp4',
            label: '标清',
            res: '480'
        }, {
            src: source.UrlRoot + '/' + source.DataInfo,
            type: 'video/mp4',
            label: '流畅',
            res: '360'
        }]);
        player.currentTime(source.StudyProgress == null ? 0 : source.StudyProgress);
        //player.play();
        // 切换分辨率事件
        player.on('resolutionchange', function (e) {
            //console.info('Source changed to %s', player.src())
        });
        player.on('timeUpdate', function (e, data) {
            //console.info(data);
            //console.info("trigger:%s,percent:%s",player.currentTime(),player.currentTime()/player.duration());
        });
        player.on("pause", function () {
            StudyProgress = parseInt(player.currentTime());
            // alert(StudyProgress)
            //  console.info("trigger:%s,percent:%s", player.currentTime(), player.currentTime() / player.duration());
            TerminateStudyProgress();
            if (!$(".myTrainCourseContent li i").eq(indexFlag).hasClass("endStatus")) {
                $(".myTrainCourseContent li i").eq(indexFlag).addClass("studyingStatus   icon-iconfont-videob ");
            }
            if (player.currentTime() == player.duration()) {
                $(".myTrainCourseContent li i").eq(indexFlag)[0].className = "icon iconfont endStatus  icon-iconfont-videoq";
            } else {

            }

        });
        player.on("ended", function (e, data) {
            //  alert("ended")
            StudyProgress = parseInt(player.currentTime());
            CompletedStudyProgress();
            $(".myTrainCourseContent li i").eq(indexFlag)[0].className = "icon iconfont endStatus  icon-iconfont-videoq";
            // console.info("trigger:%s,percent:%s", player.currentTime(), player.currentTime() / player.duration());
        });
        // 初始化分辨率控件
        player.videoJsResolutionSwitcher();
        // 禁用、启用滚动条拖动
        player.disableProgress.disable();
        player.disableProgress.enable();

    });
    player.open();

    window.onbeforeunload = function () {
        player.pause();
        return "真的离开?";
    }
}
//最后一次学习列表记录
function lastStyleRecord() {
    var params = { "TrainingItemCourseID": TrainingItemCourseID, "StudentID": userid };
    common.call(AppPath + "/Study/GetUserStudyLastContent", params, 'get', function (data) {
        if (data.Status == true) {
            ResourceID = data.Data;
            ResourceIDLast = data.Data;
        } else {
            //layer.msg(data.Message)
        }
    }, error)

}
$(function () {

    var scrollContentHeight = $(document).height() - 225;
    $('.swiper-slide').css({ 'height': scrollContentHeight + 'px', 'overflow': 'auto' });
    $('#question_wrapper').css({ 'height': scrollContentHeight + 'px', 'overflow': 'auto' });
    $('#note_wrapper').css({ 'height': scrollContentHeight + 'px', 'overflow': 'auto' });
    //初始化tab
    var swiper = new Swiper('.swiper-container', {
        onSlideChangeStart: function () {
            var _index = swiper.activeIndex;
            if (_index <= 2) {
                $('.insert_question').hide();
            } else {
                $('.insert_question').show();
            }
            $(".tab .item p").removeClass("choose");
            $(".tab .item").eq(_index).find('p').addClass("choose");
        }
    });
    //导航点击切换
    if (Cookies.get("cookie_worker") == 1) {//测评
        $(".nav_bar li").eq(2).find('p').addClass('choose').parent().siblings().find('p').removeClass('choose');
        swiper.slideTo(2);
        Cookies.set("cookie_worker", 0);
    } else if (Cookies.get("cookie_worker") == 2) {//问答
        $(".nav_bar li").eq(3).find('p').addClass('choose').parent().siblings().find('p').removeClass('choose');
        swiper.slideTo(3);
        Cookies.set("cookie_worker", 0);
    } else if (Cookies.get("cookie_worker") == 3) {//笔记
        $(".nav_bar li").eq(4).find('p').addClass('choose').parent().siblings().find('p').removeClass('choose');
        swiper.slideTo(4);
        Cookies.set("cookie_worker", 0);
    }
 
    $(".nav_bar li").click(function () {
        var order = $(this).index();
        if (order <= 2) {
            $('.insert_question').hide();
        } else {
            $('.insert_question').show();
        }
        $(this).find('p').addClass('choose').parent().siblings().find('p').removeClass('choose');
        swiper.slideTo(order);
    });
    lastStyleRecord();//上次学习记录
    loadTrainCourseStudy();//课程学习列表
    loadTrainNoticeStudy();//课程公告列表
    loadTrainEvaluationStudy();//课程测评列表
    //loadQuestionList();//加载问答列表
    //loadNoteList();//笔记列表
})