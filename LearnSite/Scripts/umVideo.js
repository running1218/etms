var um = um || {};
um.video = um.video || {};

um.video.formatVideoTime = function(time)
{
    var totalTime = Math.round(time);
    var seconds = totalTime % 60;
    var minutes = parseInt(totalTime / 60);

    if (seconds < 10)
        seconds = "0" + seconds;

    if (minutes < 10)
        minutes = "0" + minutes;

    return minutes + ":" + seconds;
}

um.video.formatVideoTimeWithPoint = function(time)
{
    var point = Math.round(time * 100 % 100);
    if (point < 10)
        point = "0" + point;

    return um.video.formatVideoTime(parseInt(time)) + "." + point;
}

um.video.defaults =
{
    'width': '900',
    'height': '505',
    'httpUrl': '',
    'rtmpUrl': '',
    'flashName': '/Controls/PlayVideo/StrobeMediaPlayback.swf',
    'startTime': '0',
    'points': '',
    'controlBarAutoHide': true
};

um.video.html5 = function (container, settings)
{
    var elementScript = '<video id="UMHtml5MediaPlayer" width="' + settings.width + '" height="' + settings.height + '">' +
'   <source src="' + settings.httpUrl + '" type="video/mp4" />' +
'</video>' +
//'<div class="videologo"><img src=\"/App_Themes/Theme/Images/videologo.png\" /></div>' +
'<div class="controlsContainer">' +
'   <ul class="controlButton">' +
'       <li id="playButton" class="play"></li>' +
'       <li id="progressBarContainer">' +
'           <div id="progressBar">' +
'               <div id="bufferBar"></div>' +
'               <div id="timeBar"></div>' +
'               <div class="cuePointContainer"></div>' +
'           </div>' +
'           <div id="progressBlock"></div>' +
'       </li>' +
'       <li id="durationContainer"><span id="currentTimeSpan">00:00</span><span id="splitSpan">/</span><span id="durationSpan">00:00</span></li>' +
'       <li id="volumeButton" class="volume"></li>' +
'       <li id="volumeContainer">' +
'           <div id="volumeControl">' +
'               <div id="volumeBar"></div>' +
'               <div id="volumeBlock"></div>' +
'           </div>' +
'       </li>' +
'       <li id="fullscreenButton" class="fullscreenV"></li>' +
'   </ul>' +
'</div>' +
'<div class="cuePointTipPanel"><div id="cuePointTimeSpan"></div><div id="cuePointTipSpan"></div></div>' +
'<div class="readyPlay"><table><tr><td><a id="playOverlayedButton" href="javascript:" class="playOverlayed"></a></td></tr></table></div>' +
'<div class="loadingVideo"></div>';

    container.append($(elementScript));

    var videoElement = $('#UMHtml5MediaPlayer');
    var videoObject = videoElement[0];

    var progressBarLeft = 34;
    var progressBarRight = 210;
    var progressBarMaxWidth = container.width() - progressBarLeft - progressBarRight;
    var progressBlockOffsetLeft = 4;
    var volumeBarMaxWidth = 50;
    var volumeBlockOffsetBottom = 0;
    var videoDuration = 0;
    var hideControls = false;

    videoObject.load();

    $("#progressBar").css("width", progressBarMaxWidth);
    $("#progressBlock").css("left", -progressBlockOffsetLeft);
    $("#volumeBlock").css("bottom", volumeBlockOffsetBottom);
    if (settings.controlBarAutoHide)
        $(".controlsContainer").hide();
    else
        $(".controlsContainer").show();

    videoElement.on("loadedmetadata", function ()
    {
        videoDuration = videoObject.duration;
        if (settings.startTime > videoDuration)
            settings.startTime = videoDuration;

        if (settings.startTime > 0)
            videoObject.currentTime = settings.startTime;

        $("#durationSpan").text(um.video.formatVideoTime(videoDuration));
        updateVideoVolumeProgress(videoObject.volume);

        if (settings.points.length > 0)
            loadVideoCuePoint(settings.points);

        $(".loadingVideo").hide();
        //$(".readyPlay").fadeIn(200);
        $(".controlsContainer").fadeIn(200);

        setTimeout(startBuffer, 500);

        $(container).trigger("loaded");
        playToggle();
    });

    //buffer
    var startBuffer = function ()
    {
        var videoBuffer = videoObject.buffered.end(0);
        var percentage = 100 * videoBuffer / videoDuration;
        $("#bufferBar").css("width", percentage + "%");

        if (videoBuffer < videoDuration)
        {
            setTimeout(startBuffer, 500);
        }
    };

    videoElement.on('canplay', function ()
    {
        $(".loadingVideo").fadeOut(200);
    });

    var playToggle = function ()
    {
        hideControls = true;
        $(".readyPlay").fadeOut(200);
        if ((videoObject.paused) || (videoObject.ended))
        {
            videoObject.play();
            fadeInControls();
            $("#playButton").removeClass("play").addClass("pause");
        }
        else
        {
            videoObject.pause();
            $("#playButton").removeClass("pause").addClass("play");
        }
    };

    $("#playButton").click(function ()
    {
        playToggle();
        return false;
    });

    $("#playOverlayedButton").click(function ()
    {
        playToggle();
        return false;
    });

    videoElement.on("ended", function ()
    {
        videoObject.pause();
        $("#playButton").removeClass("pause").addClass("play");
        videoObject.currentTime = 0;

        hideControls = false;
        clearTimeout(fadeOutControlsTimeout);
        $(".readyPlay").fadeIn(200);
        $(".controlsContainer").fadeIn(200);

        $(container).trigger("ended");
    });

    var fadeOutControlsTimeout;
    videoElement.on("mouseenter", function (e)
    {
        fadeInControls();
    });

    videoElement.on("mousemove", function (e)
    {
        fadeInControls();
    });

    $(".controlsContainer").on("mouseenter", function (e)
    {
        clearTimeout(fadeOutControlsTimeout);
    });

    $(".controlsContainer").on("mouseleave", function (e)
    {
        if (hideControls)
            fadeInControls();
    });

    var fadeInControls = function ()
    {
        clearTimeout(fadeOutControlsTimeout);
        $(".controlsContainer").fadeIn(500);

        if (settings.controlBarAutoHide)
            fadeOutControlsTimeout = setTimeout(fadeOutControls, 3000);
    }

    var fadeOutControls = function ()
    {
        if (settings.controlBarAutoHide)
            $(".controlsContainer").fadeOut(500);
    }

    //playProgress
    var updateVideoTimeProgress = function (current)
    {
        var videoCurrent = current;
        $("#currentTimeSpan").text(um.video.formatVideoTime(videoCurrent));

        var percentage = videoCurrent / videoDuration;

        $("#timeBar").css("width", percentage * 100 + "%");
        $("#progressBlock").css("left", percentage * progressBarMaxWidth - progressBlockOffsetLeft + "px");
    };

    var setVideoCurrentTime = function (xPos)
    {
        var position = xPos - $("#progressBar").offset().left;
        var percentage = position / progressBarMaxWidth;
        if (percentage > 1)
        {
            percentage = 1;
            //dragProgress = false;
        }
        if ((percentage < 0) || (isNaN(percentage)))
        {
            percentage = 0;
            //dragProgress = false;
        }
        videoObject.currentTime = videoDuration * percentage;
        updateVideoTimeProgress(videoObject.currentTime);
    };

    videoElement.on("timeupdate", function ()
    {
        if (videoObject.paused) return;
        updateVideoTimeProgress(videoObject.currentTime);
    });

    var dragProgress = false;

    $("#progressBar").mousedown(function (e)
    {
        $(".cuePointTipPanel").fadeOut(500);
        dragProgress = true;
        setVideoCurrentTime(e.pageX);
    });

    $("#progressBlock").mousedown(function (e)
    {
        $(".cuePointTipPanel").fadeOut(500);
        dragProgress = true;
    });

    //volume
    var updateVideoVolumeProgress = function (volume)
    {
        var volumeBarWidth = 0;
        if ((videoObject.muted) || (volume == 0))
        {
            $("#volumeButton").removeClass("volume").addClass("mute");
        }
        else
        {
            volumeBarWidth = volume * volumeBarMaxWidth;
            $("#volumeButton").removeClass("mute").addClass("volume");
        }
        $("#volumeBar").css("width", volumeBarWidth + "px");
        if (volumeBarWidth <= 12)
            $("#volumeBlock").css("left", "0px");
        else if (volumeBarWidth >= 38)
            $("#volumeBlock").css("left", "38px");
        else
            $("#volumeBlock").css("left", volumeBarWidth - 6 + "px");
    };

    var setVideoVolume = function (xPos)
    {
        var position = xPos - $("#volumeControl").offset().left;
        var percentage = position / volumeBarMaxWidth;
        if (percentage > 1)
            percentage = 1;
        if ((percentage < 0) || (isNaN(percentage)))
            percentage = 0;

        videoObject.volume = percentage;
    };

    videoElement.on("volumechange", function ()
    {
        updateVideoVolumeProgress(videoObject.volume);
    });

    var dragVolume = false;

    $("#volumeControl").mousedown(function (e)
    {
        dragVolume = true;
        videoObject.muted = false;
        setVideoVolume(e.pageX);
    });

    $("#volumeBlock").mousedown(function (e)
    {
        dragVolume = true;
        videoObject.muted = false;
    });

    $("#volumeButton").click(function ()
    {
        videoObject.muted = !videoObject.muted;
        return false;
    });

    $(document).mouseup(function (e)
    {
        if (dragProgress)
            setVideoCurrentTime(e.pageX);
        dragProgress = false;

        if (dragVolume)
            setVideoVolume(e.pageX);
        dragVolume = false;
    });

    $(document).mousemove(function (e)
    {
        if (dragProgress)
            setVideoCurrentTime(e.pageX);

        if (dragVolume)
            setVideoVolume(e.pageX);
    });


    if (screenfull == false)
    {
        $("#fullscreenButton").removeClass("fullscreenV").addClass("fullscreenVDisbled");
    }
    else
    {
        document.addEventListener(screenfull.raw.fullscreenchange, function ()
        {
            if (screenfull.isFullscreen)
            {
                container.css("width", window.screen.width + "px").css("height", window.screen.height + "px");
                videoElement.css("width", window.screen.width + "px").css("height", window.screen.height + "px");
                $("#fullscreenButton").attr("class", "normalscreen");
                progressBarMaxWidth = window.screen.width - progressBarLeft - progressBarRight;
            }
            else
            {
                container.css("width", settings.width + "px").css("height", settings.height + "px");
                videoElement.css("width", settings.width + "px").css("height", settings.height + "px");
                $("#fullscreenButton").attr("class", "fullscreenV");
                progressBarMaxWidth = settings.width - progressBarLeft - progressBarRight;
            }
            $("#progressBar").css("width", progressBarMaxWidth);
            updateVideoTimeProgress(videoObject.currentTime);
        });

        $("#fullscreenButton").click(function ()
        {
            screenfull.toggle(container[0]);
            return false;
        });
    }

    this.loadVideoCuePoint = function (points)
    {
        loadVideoCuePoint(points)
    }

    function loadVideoCuePoint(points)
    {
        var pointList;
        if (typeof (points) == "string")
            pointList = eval('(' + points + ')');
        else
            pointList = points;

        if ((pointList == null) || (pointList == undefined))
            return;

        var cuePointContainerObject = $(".cuePointContainer");
        cuePointContainerObject.empty();
        for (var i = 0; i < pointList.length; i++)
        {
            var percentage = pointList[i].Time / videoDuration;
            if (percentage < 0)
                percentage = 0;
            if (percentage > 1)
                percentage = 1;

            var pointLeft = parseInt(percentage * progressBarMaxWidth) - 3;
            var pointLink = $("<a id=\"point_" + pointList[i].PointID + "\"></a>").addClass("cuepoint")
                .attr("data-time", pointList[i].Time)
                .attr("data-content", pointList[i].Content)
                .attr("data-timeformat", um.video.formatVideoTimeWithPoint(pointList[i].Time))
                .css("left", pointLeft);//percentage * 100 + "%");

            pointLink.mouseenter(function (e)
            {
                var obj = $(this);
                $("#cuePointTimeSpan").text(obj.attr("data-timeformat"));
                $("#cuePointTipSpan").text(obj.attr("data-content"));
                $(".cuePointTipPanel").css("left", parseInt(obj.css("left")) + 10 + "px");
                $(".cuePointTipPanel").fadeIn(200);
            });

            pointLink.mouseleave(function ()
            {
                $(".cuePointTipPanel").fadeOut(500);
            });

            pointLink.mousedown(function (e)
            {
                dragProgress = false;
                var current = parseFloat($(this).attr("data-time"))
                if (current >= videoDuration)
                    return;

                updateVideoTimeProgress(current);
                videoObject.currentTime = current;
                if (videoObject.paused)
                {
                    videoObject.play();
                    $(".readyPlay").fadeOut(200);
                    $("#playButton").removeClass("play").addClass("pause");
                }
            });

            cuePointContainerObject.append(pointLink);
        }
    }

    this.pause = function ()
    {
        hideControls = true;
        videoObject.pause();
        $(".readyPlay").fadeOut(200);
        $("#playButton").removeClass("pause").addClass("play");
    };

    this.getPlayerCurrentTime = function ()
    {
        return videoObject.currentTime;
    };

    this.showVideoCuePoint = function (pointID)
    {
        //var pointElement = $("#point_" + pointID);
        //if (pointElement.length == 0)
        //    return;

        //videoObject.currentTime = pointElement.attr("data-time");
        //updateVideoTimeProgress(videoObject.currentTime);
        //this.pause();

        //$("#cuePointTimeSpan").text(pointElement.attr("data-timeformat"));
        //$("#cuePointTipSpan").text(pointElement.attr("data-content"));
        //$(".cuePointTipPanel").css("left", parseInt(pointElement.css("left")) + 10 + "px");
        //$(".cuePointTipPanel").fadeIn(200);
    };

};

um.video.flash = function (container, settings)
{
    var pointObject = getCuePointString(settings.points);

    var flashvars = {
        f: settings.rtmpUrl,//视频地址
        a: '',//调用时的参数，只有当s>0的时候有效
        s: '0',//调用方式，0=普通方法（f=视频地址），1=网址形式,2=xml形式，3=swf形式(s>0时f=网址，配合a来完成对地址的组装)
        c: '0',//是否读取文本配置,0不是，1是
        x: '',//调用配置文件路径，只有在c=1时使用。默认为空调用的是ckplayer.xml
        i: '',//初始图片地址
        d: '',//暂停时播放的广告，swf/图片,多个用竖线隔开，图片要加链接地址，没有的时候留空就行
        u: '',//暂停时如果是图片的话，加个链接地址
        l: '',//前置广告，swf/图片/视频，多个用竖线隔开，图片和视频要加链接地址
        r: '',//前置广告的链接地址，多个用竖线隔开，没有的留空
        t: '',//视频开始前播放swf/图片时的时间，多个用竖线隔开
        y: '',//这里是使用网址形式调用广告地址时使用，前提是要设置l的值为空
        z: '',//缓冲广告，只能放一个，swf格式
        e: '2',//视频结束后的动作，0是调用js函数，1是循环播放，2是暂停播放并且不调用广告，3是调用视频推荐列表的插件，4是清除视频流并调用js功能和1差不多，5是暂停播放并且调用暂停广告
        v: '80',//默认音量，0-100之间
        p: '1',//视频默认0是暂停，1是播放，2是不加载视频
        h: '3',//播放http视频流时采用何种拖动方法，=0不使用任意拖动，=1是使用按关键帧，=2是按时间点，=3是自动判断按什么(如果视频格式是.mp4就按关键帧，.flv就按关键时间)，=4也是自动判断(只要包含字符mp4就按mp4来，只要包含字符flv就按flv来)
        q: '',//视频流拖动时参考函数，默认是start
        m: '',//让该参数为一个链接地址时，单击播放器将跳转到该地址
        o: '',//当p=2时，可以设置视频的时间，单位，秒
        w: '',//当p=2时，可以设置视频的总字节数
        g: settings.startTime,//视频直接g秒开始播放
        j: '',//跳过片尾功能，j>0则从播放多少时间后跳到结束，<0则总总时间-该值的绝对值时跳到结束
        k: pointObject.k,//提示点时间，如 30|60鼠标经过进度栏30秒，60秒会提示n指定的相应的文字
        n: pointObject.n,//提示点文字，跟k配合使用，如 提示点1|提示点2
        wh: '',//宽高比，可以自己定义视频的宽高或宽高比如：wh:'4:3',或wh:'1080:720'
        lv: '0',//是否是直播流，=1则锁定进度栏
        loaded: 'flashPlayerLoadedHandler',//当播放器加载完成后发送该js函数loaded
        //调用播放器的所有参数列表结束
        //以下为自定义的播放器参数用来在插件里引用的
        my_url: encodeURIComponent(window.location.href)//本页面地址
        //调用自定义播放器参数结束
    };

    var params = { bgcolor: '#000', allowFullScreen: true, allowScriptAccess: 'always', wmode: 'transparent' };//这里定义播放器的其它参数如背景色（跟flashvars中的b不同），是否支持全屏，是否支持交互
    CKobject.embed(settings.flashName, container.attr("id"), 'UMFlashMediaPlayer', settings.width, settings.height, false, flashvars, params);

    var videoObject = CKobject.getObjectById('UMFlashMediaPlayer');
    
    this.loadVideoCuePoint = function (points)
    {
        loadVideoCuePoint(points)
    }

    function loadVideoCuePoint(points)
    {
        var pointObject = getCuePointString(points);

        videoObject.promptUnload();//卸载掉目前的
        videoObject.changeFlashvars("{k->" + pointObject.k + "}{n->" + pointObject.n + "}");
        videoObject.promptLoad();//重新加载	undefined

    }

    function getCuePointString(points)
    {
        var pointObject = new Object();
        pointObject.k = "";
        pointObject.n = "";

        if (points == "")
            return pointObject;

        var pointList;
        if (typeof (points) == "string")
            pointList = eval('(' + points + ')');
        else
            pointList = points;

        if ((pointList == null) || (pointList == undefined))
            return pointObject;

        var timeString = "";
        var contentString = "";
        for (var i = 0; i < pointList.length; i++)
        {
            if (i == 0)
            {
                timeString = pointList[i].Time;
                contentString = pointList[i].Content;
            }
            else
            {
                timeString = timeString + "|" + pointList[i].Time;
                contentString = contentString + "|" + pointList[i].Content;
            }
        }
        pointObject.k = timeString;
        pointObject.n = contentString;
        return pointObject;
    }

    this.pause = function ()
    {
        videoObject.videoPause();
    };

    this.getPlayerCurrentTime = function ()
    {
        return videoObject.getStatus().time;
    };

    this.showVideoCuePoint = function (pointID)
    {
    };
};

var videoPlayerContainer;

(function ($)
{
    $.fn.MediaElement = function (options)
    {
        var selfObject = this;
        videoPlayerContainer = this;
        var settings = $.extend({}, um.video.defaults, options);

        if (settings.width < 480)
            settings.width = 480;

        if (settings.height < 270)
            settings.height = 270;

        if (settings.startTime < 0)
            settings.startTime = 0;

        this.css("width", settings.width + "px");
        this.css("height", settings.height + "px");
        this.css("background-color", "#000");
        this.css("position", "relative");

        var videoElement;

        var videoTest = document.createElement("video");
        if ((videoTest != null) && (videoTest.canPlayType) && (videoTest.canPlayType('video/mp4; codecs="avc1.42E01E"') != ""))
        {
            //Html5
            videoElement = new um.video.html5(selfObject, settings);
        }
        else
        {
            //Flash
            videoElement = new um.video.flash(selfObject, settings);
        }

        this.loadVideoCuePoint = function (points)
        {
            videoElement.loadVideoCuePoint(points);
        }

        this.pause = function ()
        {
            videoElement.pause();
        };

        this.getPlayerCurrentTime = function ()
        {
            var time = parseFloat(videoElement.getPlayerCurrentTime());
            if (isNaN(time))
                return 0;

            time = Math.round(time * 100) / 100;
            return time;
        };

        this.showVideoCuePoint = function (pointID)
        {
            videoElement.showVideoCuePoint(pointID);
        };

        return this;
    };

})(jQuery);


function flashPlayerLoadedHandler()
{
    CKobject.getObjectById('UMFlashMediaPlayer').addListener('ended', 'flashPlayerEndedHandler');

    videoPlayerContainer.trigger("loaded");
}

function flashPlayerEndedHandler()
{
    videoPlayerContainer.trigger("ended");
}
