var videoPlayer = undefined;
function loadvideo(videoUrl) {
    $('.play_file').append('<div id=\"videoContainer\"></div>');
    videoPlayer = $('#videoContainer').MediaElement({
        width: '1050',
        //height: '100%',
        httpUrl: videoUrl,
        //rtmpUrl: getRtmpFilePath(source.Data.ContentUrl),
        flashName: '/Tools/VideoPlayer/ckplayer/ckplayer.swf',
        startTime: 0,
        //points: getResourcePoints(source.Data.ContentID),
        controlBarAutoHide: true
    });
}


