var CourseID = common.GetQueryString("CourseID");//课程ID
var ResourceID = "";

//初始列表
function loadCourseDetail() {
    var params = { "CourseID": CourseID };
    common.call(AppPath+"/Course/GetCourseResourceList", params, 'get', function (data) {
        if (data.Status == true) {
            if (data.Data.length > 0) {
                var template = Handlebars.compile($("#tmpl_CourseDetailNoneUser").html());
                var renderHTML = template(data);
                $('.object_list').html(renderHTML);
                $(".courseContent li").each(function () {
                    $this = $(this);
                    if ($this.attr("data-IsOpen") == 'true') {
                        $this.addClass("data-IsOpen").find("i").css("color", "rgb(65, 133, 255)");
                    }
                })
                $(".courseContent li.data-IsOpen").click(function () {
                    $this = $(this);
                    $this.css("color", "rgb(65, 133, 255)").siblings().css("color", "#333");
                    ResourceID = $this.attr("data-contentid");
                    loadResource();
                })
                $(".courseContent li.data-IsOpen").eq(0).click();//课程学习视频或文档初次加载
            } else {
                $(".courseContent, .object_list").css({"padding-left":0,"padding-right":0,"width":100+"vw"})
                contentNull($('.object_list'));
                $('#PlayerBox').html('<video id="videojs-open-player" src="" poster='+AppPath+'"/Images/video-bg.png" style="width:100%;height:100%;" controls="" autobuffer="" webkit-playsinline="true"></video>');
            }
            
        }
    }, error);
}
function loadResource() {
    var params = { "CourseID": CourseID, "ResourceID": ResourceID };
    //console.log(params)
    common.call(AppPath+"/Course/GetCourseResourceContent", params, 'get', function (data) {
        if (data.Status == true) {
           // console.log(data)
            switch (data.Data.Type) {
                case 1: //视频
                    var src = data.Data.UrlRoot + "/" + data.Data.DataInfo;
                    console.log(src)
                    $('#PlayerBox').html('<video id="videojs-open-player" src="' + src + '" poster="' + data.Data.ThumbnailURL + '" style="width:100%;height:100%;" controls="" autobuffer="" webkit-playsinline="true"></video>');
                    break;
                case 2://文档
                    $('#PlayerBox').html('<a class="pictureBox" href="' + AppPath + '/Course/DocumentPlay?CourseID=' + CourseID + '&ResourceID=' + ResourceID + '"><i class="ico iconfont icon-iconfont-jt"></i><img class="picturePlayer" src="' + data.Data.ImageList[0].img + '" style="width:100%;height:100%;" /></a>');
                  //  window.location.href = AppPath+ "/Course/DocumentPlay?CourseID=" + CourseID + "&ResourceID=" + ResourceID;
                    break;
                default:
                    break;
            };

        } else {
            layer.msg(data.Message)
        }
    }, error)
}
loadCourseDetail();
 
