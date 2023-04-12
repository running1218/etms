
//register window.event/bug for firefox
if (/firefox/.test(navigator.userAgent.toLowerCase())) {
    var $E = function () { var c = $E.caller; while (c.caller) c = c.caller; return c.arguments[0] };
    __defineGetter__("event", $E);
}

function popMessageBoxAndCloseParent(msg, title, icon, handler) {
    var conf = {
        area: ['280', 'auto'],
        dialog: {
            msg: msg,
            type: icon,
            yes: function (index) {
                closeLayer(handler);
            }
        }
    };

    if ((title != undefined) && (title != null)) {
        conf.title = title;
    }
    $.layer(conf);
}

function popSuccessBoxAndCloseParent(msg, title, handler) {
    popMessageBoxAndCloseParent(msg, title, 1, handler)
}

function popMessageBox(msg, title, icon, handler) {
    var conf = {
        area: ['280', 'auto'],
        dialog: {
            msg: msg,
            type: icon,
            yes: function (index) {
                if (typeof handler === 'function') handler();
                if (layer) layer.close(index);
            }
        }
    };

    if ((title != undefined) && (title != null)) {
        conf.title = title;
    }
    $.layer(conf);
}

function popAlertBox(msg, title, handler) {
    popMessageBox(msg, title, 0, handler)
}

function popSuccessBox(msg, title, handler) {
    popMessageBox(msg, title, 1, handler)
}

function popFailedBox(msg, title, handler) {
    popMessageBox(msg, title, 5, handler)
}

function popConfirmBox(msg, title, buttonTitle, okHandler, cancelHandler) {
    var conf = {
        area: ['auto', 'auto'],
        dialog: {
            msg: msg,
            btns: 2,
            type: 4,
            btn: buttonTitle,
            yes: function (index) {
                if (typeof okHandler === 'function') okHandler();
                layer.close(index);
            },
            no: function (index) {
                if (typeof cancelHandler === 'function') cancelHandler();
                layer.close(index);
            }
        }
    };

    //var event = window.event;
    //if (event != undefined) {
    //    var y = (event.screenY - 180) < 0 ? 10 : (event.screenY - 180);
    //    var x = (event.screenX - 280) < 0 ? 240 : (event.screenX - 280);
    //    conf.area = ['280px', 'auto'];
    //    conf.offset = [y + "px", x + "px"];
    //}

    if ((title != undefined) && (title != null)) {
        conf.title = title;
    }
    $.layer(conf);
}

function popMessagePanel(msg, time, icon, handler) {
    var conf = {
        title: false,
        closeBtn: false,
        time: time === undefined ? 2 : time,
        shadeClose: true,
        dialog: { msg: msg, type: icon },
        border: [1, 0.3, '#000'],
        end: function (index) {
            if (typeof handler === 'function') handler();
            layer.close(index);
        }
    };
    $.layer(conf);
}

function popSuccessPanel(msg, time, handler) {
    popMessagePanel(msg, time, 1, handler);
    //layer.msg(msg, 2, 1);
}

function closeLayer(handler) {
    if (typeof handler === "function") handler();
    window.parent.layer.closeAll();
}

//弹出窗口（layer方式）
function showLayerWindow(title, url, width, height) {
    showLayerWindow(title, url, width, height, null);
}
//弹出窗口（layer方式）
function showLayerWindow(title, url, width, height, handler) {
    layer.open({
        type: 2,
        shade: [0.1],
        offset: [($(window).height() - height) / 2 + 'px', ''],
        fix: true,
        title: title,
        fadeIn: 600,
        maxmin: true,
        //shift: 'left',
        //iframe: { src: url },
        content:url,
        area: [width + 'px', height + 'px'],
        close: function (index) {
            if (typeof handler === 'function') handler();
        }
    });
}

//返回到页面顶部
(function () {
    str1 = '<div class="homePageBackTopBox"><div class="box"><div class="troubleShooting"title="常见问题解答"><a href="/FAQ/ModuleAndQuestion.aspx"target="view_window"></a></div><div class="feedBack"title="问题反馈"><a href="/FeedBackCenter/FeedBack.aspx"target="_bank"></a></div><div class="telephpneApp"title="手机二维码"><a><span class="teleBg"></span></a></div></div><div class="backToTop" title="页面置顶"><a href="#"></a></div></div>'
    $backToTopEle = $(str1).appendTo($("body"));

    $(".backToTop").click(function () {
        $("html, body").animate({ scrollTop: 0 }, 120);
    });
    $backToTopFun = function () {
        var st = $(document).scrollTop();
        winh = $(window).height();
        $(".homePageBackTopBox").show()
        $(".homePageBackTopBox .box").show();
        (st > 0) ? $(".backToTop").show() : $(".backToTop").hide();
        //管理员页面不显示
        $("#xubox_layer1 .homePageBackTopBox").hide();
        $(".panel").siblings(".homePageBackTopBox").hide();
        var urlStr = window.location.href
        if (urlStr.indexOf("Manage") > 0) {
            $(".homePageBackTopBox").hide();
        }
        //为了兼容IE6的浏览器
        if (!window.XMLHttpRequest) {
            $backToTopEle.css("top", st + winh - 166);
        }
    };
    $(window).bind("scroll", $backToTopFun);
    $(function () { $backToTopFun(); });

})();


// 基于jquery的数字软键盘
function showKeyboard(inputId, length) {
    var kb = $('.kbdiv');
    if (kb.length != 0) {
        kb.remove();
    }

    kb = $('<div class="kbdiv"></div>');
    var i = 0;
    var keyboard = '<div class="kbtable">';
    for (i = 1; i < 10; i++) {
        keyboard += '<span class="kbkey">' + i + '</span>';
    }
    keyboard += '<span id="kbback" class="kbkey kbkey-button">清空</span><span class="kbkey">' + 0 + '</span><span id="kbclose" class="kbkey kbkey-button">确定</span>';
    keyboard += "</div>";
    kb.html(keyboard);
    kb.appendTo('body');

    $("span", kb).mouseover(function () {
        this.className += " kbmouseover";
    }).mouseout(function () {
        this.className = this.className.replace(" kbmouseover", "");
    }).click(function () {

        if (this.id == "kbclose") {
            kb.remove();
            return false;
        }
        //清空
        if (this.id == 'kbback') {
            $('#' + inputId).val("");
            return false;
        }

        if ($("#" + inputId).val().length < length) {
            $("#" + inputId).val($("#" + inputId).val() + "" + $(this).html());
        }
    });

    var offset = $("#" + inputId).offset();
    var left = offset.left;
    var height = $("#" + inputId).height();
    var top = offset.top + height + 2;
    kb.css({ "left": left + "px", "top": top + "px", "position": "absolute", "z-index": "10000" });
    return false;
}

// tab 
; (function ($) {
    $.fn.tab = function (options) {
        var opt = {
            tabNav: '.tab-nav li',   //选项卡tab
            tabMain: '.tab-cont',   //选项卡主体内容
            tabTrigger: 'click',    //tab绑定的触发事件
            hoverClass: 'tab-hover',  //tab鼠标hover时的class
            activeClass: 'tab-active'  //tab选中的效果的class
        }
        var option = $.extend({}, opt, options);
        var $tabNav = $(option.tabNav),
            $cont = $(option.tabMain);
        $tabNav.on(option.tabTrigger, function () {
            var $tabIndex = $(this).index();
            $(this).addClass(option.activeClass).siblings().removeClass(option.activeClass);
            $(option.tabMain).eq($tabIndex).css({ visibility: "visible", height: "auto" }).siblings(option.tabMain).css({ visibility: "hidden", height: "0px" });
        })
        //$tabNav.hover(function () {
        //    $(this).addClass(option.hoverClass).siblings().removeClass(option.hoverClass);
        //}, function () {
        //    $(this).removeClass(option.hoverClass);
        //})
    }

})(jQuery);

function IsAuthenticateError(msg) {
    $.ajax({
        url: AppPath + '/UserIsAuthenticated.ashx',
        type: 'POST',
        data: {},
        dataType: "json",
        async: false,
        success: function (result) {
            if (result == true) {
                layer.alert(msg);
            }
            else {
                window.location.href = window.location.href;
            }
        },
    });
}

//针对服务端按钮事件定制的弹出询问信息
function popConfirmMsgForControl(obj, msgbox, titleName) {
    if ($(obj).attr("disabled") == "disabled") return;
    if (obj.isCalled == undefined) {
        var callback = function (status) {
            if (status == "ok") {
                obj.isCalled = true;
                obj.click();
            }
        };
        ymPrompt.confirmInfo({ message: msgbox, title: titleName, handler: callback, maskAlpha: '0.4', closeBtn: false });
    }

}

/**      
 * 对Date的扩展，将 Date 转化为指定格式的String      
 * 月(M)、日(d)、12小时(h)、24小时(H)、分(m)、秒(s)、周(E)、季度(q) 可以用 1-2 个占位符      
 * 年(y)可以用 1-4 个占位符，毫秒(S)只能用 1 个占位符(是 1-3 位的数字)      
 * eg:      
 * (new Date()).pattern("yyyy-MM-dd hh:mm:ss.S") ==> 2006-07-02 08:09:04.423      
 * (new Date()).pattern("yyyy-MM-dd E HH:mm:ss") ==> 2009-03-10 二 20:09:04      
 * (new Date()).pattern("yyyy-MM-dd EE hh:mm:ss") ==> 2009-03-10 周二 08:09:04      
 * (new Date()).pattern("yyyy-MM-dd EEE hh:mm:ss") ==> 2009-03-10 星期二 08:09:04      
 * (new Date()).pattern("yyyy-M-d h:m:s.S") ==> 2006-7-2 8:9:4.18 
 * (new Date()).pattern("yyyy-M-d") ==> 2006-7-2 
 */
// 对Date的扩展，将 Date 转化为指定格式的String   
// 月(M)、日(d)、小时(h)、分(m)、秒(s)、季度(q) 可以用 1-2 个占位符，   
// 年(y)可以用 1-4 个占位符，毫秒(S)只能用 1 个占位符(是 1-3 位的数字)   
// 例子：   
// (new Date()).Format("yyyy-MM-dd hh:mm:ss.S") ==> 2006-07-02 08:09:04.423   
// (new Date()).Format("yyyy-M-d h:m:s.S")      ==> 2006-7-2 8:9:4.18   
Date.prototype.Format = function (fmt) {
    var o = {
        "M+": this.getMonth() + 1,                 //月份   
        "d+": this.getDate(),                    //日   
        "h+": this.getHours(),                   //小时   
        "m+": this.getMinutes(),                 //分   
        "s+": this.getSeconds(),                 //秒   
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度   
        "S": this.getMilliseconds()             //毫秒   
    };
    if (/(y+)/.test(fmt))
        fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt))
            fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
}

//格式化日期
function dateformat(value) {
    if (value != undefined) {
        value = value.replace(/-/g, "/");
        if (value)
            return (new Date(value)).Format("yyyy-MM-dd");
    } else {
        return '';
    }
}
GetQueryString = function (name) {
    //var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var reg = new RegExp('(^|&)' + name + '=([^&]*)(&|$)', 'i');
    var r = window.location.search.substr(1).toLocaleLowerCase().match(reg);
    if (r != null) return unescape(r[2]);
    return null;
}
