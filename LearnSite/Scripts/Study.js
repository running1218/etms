function UserStudyProgress(trainingItemCourseID, rid, status, progress,type) {
    this.trainingItemCourseID = trainingItemCourseID;
    //this.courseID = courseid;
    this.resourceID = rid;
    this.studyStatus = status;
    this.studyProgress = progress;
    this.resourceType = type;
    this.startTime = null;
}
UserStudyProgress.prototype.getProgress = function () {
    return 0;
}

UserStudyProgress.prototype.initialize = function () {
    if (this.postData() == false)
        return;

    InitializeStudyStatus(this.trainingItemCourseID, this.resourceID)
}

UserStudyProgress.prototype.terminate = function () {
    if (this.postData() == false)
        return;
    if (this.resourceType == '1') {
        if (videoPlayer != null && videoPlayer != undefined) {
            this.studyProgress = parseInt(videoPlayer.getPlayerCurrentTime());
        }
    }
    else if (this.resourceType == '2') {
        this.studyProgress = activeIndex;
    }
    TerminateStudyStatus(this.trainingItemCourseID, this.resourceID, this.studyProgress, this.startTime)
}

UserStudyProgress.prototype.completed = function () {
    if (this.postData() == false)
        return;

    this.studyProgress = parseInt(this.getProgress());
    CompletedStudyStatus(this.trainingItemCourseID, this.resourceID, this.studyProgress)
}

UserStudyProgress.prototype.postData = function () {
    if ((this.trainingItemCourseID == null) || (this.trainingItemCourseID == undefined) || (this.resourceID == null) || (this.resourceID == undefined)) {
        return false;
    }
    else {
        return true;
    }
}
var currentUserStudyProgress = new UserStudyProgress(null, null, null, 0, 0,null);
var studyStatusCompletedFlag = 1;
var studyHandlerUrl = AppPath + "/PublicService/StudyHandler.ashx";

function InitializeStudyStatus(trainingItemCourseID, resourceID) {
    $.ajax({
        url: studyHandlerUrl,
        type: 'POST',
        data: { Method: "Initialize", ResourceID: resourceID, trainingItemCourseID: trainingItemCourseID },
        dataType: "json",
        async: false,
        success: function (result) {
            if (result.Status == true) {
            }
            else {
                alert(result.Message);
            }
        },
        error: function (err) {
            IsAuthenticateError("保存学习进度失败");
        }
    });
}
function TerminateStudyStatus(trainingItemCourseID, resourceID, progress, startTime) {
    $.ajax({
        url: studyHandlerUrl,
        type: 'POST',
        data: { Method: "Terminate", ResourceID: resourceID, trainingItemCourseID: trainingItemCourseID, Progress: progress, StartTime: startTime },
        dataType: "json",
        async: false,
        success: function (result) {
            if (result.Status == true) {

            }
            else {
                alert(result.Message);
            }
        },
        error: function (err) {
            IsAuthenticateError("保存学习进度失败");
        }
    });
}

function CompletedStudyStatus(trainingItemCourseID, resourceID, progress) {
    $.ajax({
        url: studyHandlerUrl,
        type: 'POST',
        data: { Method: "Completed", ResourceID: resourceID, TrainingItemCourseID: trainingItemCourseID, Progress: progress },
        dataType: "json",
        async: false,
        success: function (result) {
            if (result.Status == true) {
                changeChapterAndResourceStyle(trainingItemCourseID, resourceID);
            }
            else {
                alert(result.Message);
            }
        },
        error: function (err) {
            IsAuthenticateError("保存学习进度失败");
        }
    });
}

//更改状态
function changeChapterAndResourceStyle(trainingItemCourseID, resourceID) {

    $('#' + resourceID).removeClass('studying').addClass('study_end');

    //var isAllComplete = true;
    //$('#studyingBarList li input').each(function () {
    //    if ($(this).hasClass('u-studying-on') || $(this).hasClass('u-studying-doing')) {
    //        isAllComplete = false;
    //    }
    //});

    //if (isAllComplete) { $('#' + currentUserStudyProgress.chapterID).removeClass('study-undoing').removeClass('study-doing').addClass('study-completed'); }
}
//video palyer start
var videoPlayer = undefined;
function loadvideo(source) {
    $('.play_file').append('<div id=\"videoContainer\"></div>');
    videoPlayer = $('#videoContainer').MediaElement({
        width: '800',
        height: '450',
        httpUrl: source.Data.UrlRoot + "/" + source.Data.DataInfo,
        //rtmpUrl: getRtmpFilePath(source.Data.ContentUrl),
        flashName: '/Tools/VideoPlayer/ckplayer/ckplayer.swf',
        startTime: source.Data.StudyProgress == null ? 0 : source.Data.StudyProgress,
        //points: getResourcePoints(source.Data.ContentID),
        controlBarAutoHide: true
    });

    $(videoPlayer).bind("ended", function () {
        videoEnded();
    });
}

function videoEnded() {
    if (window.currentUserStudyProgress) {
        currentUserStudyProgress.studyStatus = studyStatusCompletedFlag;
        currentUserStudyProgress.completed();
    }
}
//flash播放 触发完成事件
function MediaPlayCompleted() {
    videoEnded();
}
if (window.currentUserStudyProgress) {
    currentUserStudyProgress.getProgress = function () {
        return videoPlayer.getPlayerCurrentTime();
    };
}

window.onpagehide = function () {
    pause();
};

function pause() {
    if (videoPlayer != undefined) {
        videoPlayer.pause();
    }
}
//video player end
//document player start
var activeIndex = 0;
var palyTime = 1;
var studyStatusCompletedFlag = 1;

function loadDocument(source) {
    palyTime = source.Data.PlayTime;
    $('.play_file').append('<div id=\"fotorama\"></div>');

    $("#fotorama").fotorama({
        navwidth: 800,
        thumbwidth: 48,
        thumbheight: 48,
        thumbMargin: 5,
        captions: false,
        nav: "thumbs",
        fit: "cover",
        ratio: 16 / 9,
        thumbfit: "cover",
        transition: "crossfade",
        allowfullscreen: true,
        data: source.Data.ImageList
    });

    var fotorama = $('#fotorama').fotorama().data('fotorama');
    $('#fotorama').on('fotorama:show', function (e, fotorama) {
        activeIndex = fotorama.activeIndex;
        currentUserStudyProgress.studyProgress = activeIndex;
        if (activeIndex == palyTime - 1 && source.Data.StudyStatus != studyStatusCompletedFlag) {
            currentUserStudyProgress.studyStatus = studyStatusCompletedFlag;
            currentUserStudyProgress.completed();
        }
    });
    if (palyTime == null || palyTime <= 1) {
        currentUserStudyProgress.studyStatus = studyStatusCompletedFlag;
        currentUserStudyProgress.completed();
    }

    var startPage = 0;
    if (source.Data.StudyProgress != null) {
        startPage = source.Data.StudyProgress;
    }

    fotorama.show(startPage);

    //var imgWidth = 0, imgHeight = 0;
    //var pic = new Image();
    //pic.src = source.Data.ImageList[0]['img'];
    //pic.onload = function () {
    //    imgWidth = pic.width ;//> 800 ? 800 : pic.width;
    //    imgHeight = pic.width > 800 ? (800 * pic.height / pic.width) : pic.height;
    //    afterloadimage(imgWidth, imgHeight);
    //};
}

function afterloadimage(imgWidth, imgHeight) {
    $('#fotorama').on('fotorama:load', function (e, fotorama) {
        resetImageSize(imgWidth, imgHeight);
    });
    $('#fotorama').on('fotorama:fullscreenexit', function (e, fotorama) {
        resetImageSize(imgWidth, imgHeight);
    });

    fotorama.resize({
        width: imgWidth,
        minwidth: imgWidth,
        maxwidth: imgWidth,
        height: imgHeight,
        minheight: imgHeight,
        maxheight: imgHeight,
    });

    var userAgent = navigator.userAgent.toLowerCase().match(/msie ([\d.]+)/);
    if (userAgent) {
        if (userAgent[1] == "8.0" || userAgent[1] == "7.0") {
            fotorama.requestFullScreen();
            fotorama.setOptions({
                height: screen.height + 100,
                nav: false,
                allowfullscreen: false
            });
            $(".fotorama__stage__frame").css("overflow-y", "hidden");
        }
    }
}

function resetImageSize(width, height) {
    
    $('.fotorama__stage__frame img').each(function () {
        $(this).width(width + "px");
        $(this).height(height + "px");
    });
    $('.fotorama__stage').width(width).height(height);
    $('.fotorama__wrap').width(width).height(height);
}
//document player end

function loadResrouce() {//resourceID, trainingItemCourseID, flag
    $.ajax({
        async: false,
        type: "post",
        url: studyHandlerUrl,
        data: { Method: "resource", ResourceID: currentUserStudyProgress.resourceID, TrainingItemCourseID: currentUserStudyProgress.trainingItemCourseID, ResourceType: currentUserStudyProgress.resourceType },
        dataType: "json",
        success: function (source) {
            currentUserStudyProgress.resourceType = flag = source.Data.Type;
            if (source.Data.StudyStatus == null) {
                currentUserStudyProgress.initialize();
            }
            $(".play_file").empty();

            switch (flag.toString()) {
                case "1": //视频
                    loadvideo(source);
                    //$("#submitwork").hide();
                    if (!$('#' + currentUserStudyProgress.resourceID).hasClass('study_end')) {
                        $('#' + currentUserStudyProgress.resourceID).removeClass('status_icon').addClass('studying');
                    }
                    break;
                case "2"://文档
                    loadDocument(source);
                    if (!$('#' + currentUserStudyProgress.resourceID).hasClass('study_end')) {
                        $('#' + currentUserStudyProgress.resourceID).removeClass('status_icon').addClass('studying');
                    }
                    break;
                default:
                    break;
            };
        },
        error: function (err) {
            IsAuthenticateError('加载资源错误!');
        }
    });
}

$(function () {
    EditTitleName();
    $(window).on('unload', function () {
        currentUserStudyProgress.terminate();
    })
    $('.chapter').delegate("li", "click", function () {
        ContentID = $(this).data("contentid");
        
        ContentType = $(this).data("type");
        $('.chapter').hide();
        EditTitleName();
        currentUserStudyProgress.terminate();
        currentUserStudyProgress.resourceID = ContentID;
        currentUserStudyProgress.startTime = getServerDateTime();
        currentUserStudyProgress.resourceType = ContentType;
        loadResrouce();//(ContentID, TrainingItemCourseID, ContentType);
    })
    //笔记和问答切换
    $(".QAandNote_tab li").livequery('click', function () {
        $(".QAandNote_tab li").removeClass("tab_active");
        $(this).siblings().addClass("tab_active");
        var index = $(this).attr("index");
        $(".u-studying-block").eq(index).show().siblings().hide();
    });
    currentUserStudyProgress.startTime = getServerDateTime();
    loadResrouce();
})

function EditTitleName() {
    $("#resourceName").text($("#" + ContentID + " span:first").text());
}
function getServerDateTime() {
    var beginStudyTime = null;
    $.ajax({
        async: false,
        type: "post",
        url: studyHandlerUrl,
        data: { Method: "time" },
        dataType: "json",
        success: function (result) {
            if (result.Status == true) {
                beginStudyTime = result.Data;
            }
        }
    });
    return beginStudyTime;
}

