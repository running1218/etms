var pageIndex = 1;
var pageSize = 99999;
var totalRecords = 1;

window.onload = function () {
    $(".input_username").focus();
    if (window.parent.document.getElementById("myframe")) {
        var href = window.parent.location.href;

        var i = href.indexOf("#");
        if (i > 0) {
            window.parent.location = href.substr(0, i);
        }
        else
            window.parent.location.reload();
    }
}

if (window.addEventListener) {
    //如果是firfox浏览器,则使用window.addEventListener触化resize事件,注意第三个参数是false,不能丢失           
    window.addEventListener('onresize', doResize, false);
    window.addEventListener('resize', doResize, false);
}
else {
    //如果是IE浏览器则使用window.onresize触化浏览器resize事件  
    var resizeTimer = null;
    window.onresize = function () {
        resizeTimer = resizeTimer ? null : setTimeout(doResize, 0);
    }
}

$(document).ready(function () {
    $(".login input").focus(function () {
        $(this).parent("span").addClass("inputsp-focus");
        if ($(this).val().length > 0 || $(this).val().length == 0) {
            $(this).addClass("selectedInput");
        } else {
            $(this).removeClass("selectedInput");
        }
    })

    $(".login input").blur(function () {
        $(this).parent("span").removeClass("inputsp-focus");
        if ($(this).val().length > 0) {
            $(this).addClass("selectedInput");
        } else {
            $(this).removeClass("selectedInput");
        }
    })

    $(".login input").each(function () {
        if ($(this).val().length > 0) {
            $(this).addClass("selectedInput");
        } else {
            $(this).removeClass("selectedInput");
        }
    })
    loadnotice();
    $(".notice-items").scrollQ({
        line: 8,
        scrollNum: 1,
        scrollTime: 2000
    });
    
    registerviewevent();
});

function ChangeValidate() {
    var imgObjec = document.getElementById("imgValidate");
    imgObjec.src = "ValidCode.ashx?action=image&date=" + new Date();
}

function doLogin() {
    var uid = $('#username').val();
    if (uid == "") {
        popAlertMsg("请输入用户名！", "登录提示");
        return false;
    }
    var pwd = $('#password').val();
    if (pwd == "") {
        popAlertMsg("请输入密码！", "登录提示");
        return false;
    }
    return true;
}
function doResize() {

}

function loadnotice()
{
    $('.notice-more').hide();

    $.ajax({
        url: 'NoticeService.ashx',
        type: 'POST',
        data: { PageSize: pageSize, PageIndex: pageIndex },
        dataType: "json",
        async: false,
        success: function (result) {
            if (result.Status == true) {
                loaddata(result);
            }
        },
        error: function (err) {

        }
    });
}

function loaddata(result)
{    
    $("#notice").tmpl(result.Data).appendTo('.notice-items');
}

function Ajaxloading() {
    //$("#divContext").html("<div style='padding:100px 0px 0px 45%'>数据加载中……</div>");
}

function registerviewevent()
{
    $('.notice-instance').unbind('click').click(function () {
        var id = $(this).attr('id');
        $.layer({
            type: 2,
            shade: [0.1],
            offset: [($(window).height() - 650) / 2 + 'px', ''],
            fix: true,
            title: ' ',
            fadeIn: 600,
            maxmin: true,
            //shift: 'left',
            iframe: { src: 'Notice.aspx?BulletinID=' + id },
            area: [800 + 'px', 650 + 'px']
        });
    });
}
