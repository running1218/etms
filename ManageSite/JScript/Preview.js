
//video palyer start
var videoPlayer = undefined;
function loadvideo(source) {
    $('.play_file').append('<div id=\"videoContainer\"></div>');
    videoPlayer = $('#videoContainer').MediaElement({
        width: '800',
        height: '450',
        httpUrl: source.UrlRoot + "/" + source.DataInfo,
        //rtmpUrl: getRtmpFilePath(source.Data.ContentUrl),
        flashName: '/Tools/VideoPlayer/ckplayer/ckplayer.swf',
        startTime: 0,
        //points: getResourcePoints(source.Data.ContentID),
        controlBarAutoHide: true
    });

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
    palyTime = source.PlayTime;
    $('.play_file').append('<div id=\"fotorama\"></div>');

    $("#fotorama").fotorama({
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
        data: source.ImageList
    });

    var fotorama = $('#fotorama').fotorama().data('fotorama');
    $('#fotorama').on('fotorama:show', function (e, fotorama) {
        activeIndex = fotorama.activeIndex;

    });

    var imgWidth = 800, imgHeight;
    var i1 = $("<img/>").attr("src", source.ImageList[0]['img']).on("load", function () {
        imgWidth = i1[0].width > 800 ? 800 : i1[0].width;
        //imgHeight = i1[0].height > 550 ? 550 : i1[0].height;
    });
    var startPage = 0;
    if (source.StudyProgress != null) {
        startPage = source.StudyProgress;
    }

    fotorama.show(startPage);
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
    //$.ajax({
    //    async: false,
    //    type: "post",
    //    url: studyHandlerUrl,
    //    data: { Method: "resource", ResourceID: currentUserStudyProgress.resourceID, TrainingItemCourseID: currentUserStudyProgress.trainingItemCourseID, ResourceType: currentUserStudyProgress.resourceType },
    //    dataType: "json",
    //    success: function (source) {
    //currentUserStudyProgress.resourceType = flag = source.Data.Type;
    //if (source.Data.StudyStatus == null) {
    //    currentUserStudyProgress.initialize();
    //}
    //$(".play_file").empty();
    //debugger;
    if (Content != undefined) {
        var source = Content.Data;
        switch (source.Type.toString()) {
            case "1": //视频
                loadvideo(source);
                break;
            case "2"://文档
                loadDocument(source);
                break;
            default:
                break;
        };
    }
    //    },
    //    error: function (err) {
    //        IsAuthenticateError('加载资源错误!');
    //    }
    //});
}

$(function () {
    loadResrouce();
})
