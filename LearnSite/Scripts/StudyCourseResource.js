//video palyer start
var videoPlayer = undefined;
function loadvideo(source) {
    $('.play_file').append('<div id=\"videoContainer\"></div>');
    videoPlayer = $('#videoContainer').MediaElement({
        width: '1200',
        //height: '100%',
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
        allowfullscreen: false,
        data: source.Data.ImageList
    });

    var fotorama = $('#fotorama').fotorama().data('fotorama');
    $('#fotorama').on('fotorama:show', function (e, fotorama) {
        activeIndex = fotorama.activeIndex;
        //currentUserStudyProgress.studyProgress = activeIndex;
        if (activeIndex == palyTime - 1 && source.Data.StudyStatus != studyStatusCompletedFlag) {
            //currentUserStudyProgress.studyStatus = studyStatusCompletedFlag;
            //currentUserStudyProgress.completed();
        }
    });
    if (palyTime == null || palyTime <= 1) {
        //currentUserStudyProgress.studyStatus = studyStatusCompletedFlag;
        //currentUserStudyProgress.completed();
    }
    var startPage = 0;
    if (source.Data.StudyProgress != null) {
        startPage = source.Data.StudyProgress;
    }

    fotorama.show(startPage);

    var imgWidth = 0, imgHeight = 0;
    var pic = new Image();
    pic.src = source.Data.ImageList[0]['img'];
    pic.onload = function () {
        imgWidth = pic.width > 800 ? 800 : pic.width;
        imgHeight = pic.width > 800 ? (800 * pic.height / pic.width) : pic.height;
        afterloadimage(imgWidth, imgHeight);
    };   
}

function afterloadimage(imgWidth, imgHeight)
{
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

function loadResrouce(resourceID, CourseID, flag) {
    $.ajax({
        async: false,
        type: "post",
        url: AppPath+"/PublicService/StudyCourseResourceHandler.ashx",
        data: { Method: "resource", ResourceID: resourceID, CourseID: CourseID, ResourceType: flag },
        dataType: "json",
        success: function (source) {
            //currentUserStudyProgress.resourceType = flag = source.Data.ResourceType + '' + source.Data.ContentType;
            if (source.Data.StudyStatus == null) {
                //currentUserStudyProgress.initialize();
            }
            $(".play_file").empty();

            switch (flag.toString()) {
                case "1": //视频
                    loadvideo(source);
                    //$("#submitwork").hide();
                    break;
                case "2"://文档
                    loadDocument(source);
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
    //loadDocument("");
    $('.chapter').delegate("li", "click", function () {
        ContentID = $(this).data("contentid");
        ContentType = $(this).data("type");
        var isopen = $(this).data('isopen');

        if (isopen == 'True') {
            $("#spName").html($(this).data("name"));
            $('.chapter').hide();
            loadResrouce(ContentID, CourseID, ContentType);
        }
    })
    //$('.chapter').mouseleave(function () {
    //    $('.chapter').hide();
    //})
    //笔记和问答切换
    //$(".QAandNote_tab li").livequery('click', function () {
    //    $(".QAandNote_tab li").removeClass("tab_active");
    //    $(this).addClass("tab_active");
    //    var index = $(this).attr("index");
    //    $(".u-studying-block").eq(index).show().siblings().hide();
    //});

    //loadResrouce(ContentID, CourseID, ContentType);
})

$(document).ready(function () {
    //loadResrouce(ContentID, CourseID, ContentType);
    $('.chapter #' + ContentID).click();
});

//loadResrouce('E3C91F66-B376-4EFD-9A5A-7BE7B0A8FC29', '59BCB411-CD20-4301-9847-87A616D4B9AB', '1');//视频
//loadResrouce('8a46b4fc-54bb-449d-9985-6866cf8b5484', '59BCB411-CD20-4301-9847-87A616D4B9AB', '2');//文档

