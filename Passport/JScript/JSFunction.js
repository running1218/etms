/*
* =========================================================================
* Author:chenzw, AddDate:2012-02-17
* ========================================================================
* 功能主要是弹出页面窗体，和弹出普通层的等常用函数,初始化iframe框架高度
*/

function popAlertMsg(msgbox, titleName, handler, mwidth, mheight) {
    if (mwidth > 320 || mwidth == null || mwidth <= 0)
        mwidth = 320;
    if (mheight > 220 || mheight == null || mheight <= 0)
        mheight = 220;
    var callback = function (status) {
        if (status == "ok")
            handler();
    };
    ymPrompt.alert({ message: msgbox, title: titleName, handler: callback, maskAlpha: '0', width: mwidth, height: mheight });
}

//弹出成功信息
function popSuccessMsg(msgbox, titleName, handler, mwidth, mheight) {
    if (mwidth > 320 || mwidth == null || mwidth <= 0)
        mwidth = 320;
    if (mheight > 220 || mheight == null || mheight <= 0)
        mheight = 220;
    var callback = function (status) {
        if (status == "ok")
            handler();
    };
    ymPrompt.succeedInfo({ message: msgbox, title: titleName, handler: callback, maskAlpha: '0', width: mwidth, height: mheight });
}

//弹出失败信息
function popFailedMsg(msgbox, titleName, handler, mwidth, mheight) {
    if (mwidth > 320 || mwidth == null || mwidth <= 0)
        mwidth = 320;
    if (mheight > 220 || mheight == null || mheight <= 0)
        mheight = 220;
    var callback = function (status) {
        if (status == "ok")
            handler();
    };
    ymPrompt.errorInfo({ message: msgbox, title: titleName, handler: callback, maskAlpha: '0', width: mwidth, height: mheight });
}

//弹出询问信息
function popConfirmMsg(msgbox, titleName, handler, mwidth, mheight) {
    if (mwidth > 320 || mwidth == null || mwidth<=0)
        mwidth = 320;
    if (mheight > 220 || mheight == null || mheight<=0)
        mheight = 220;
    var callback = function (status) {
        if (status == "ok")
            handler();
    };
    ymPrompt.confirmInfo({ message: msgbox, title: titleName, handler: callback, maskAlpha: '0', width: mwidth, height: mheight });
}
//针对服务端按钮事件定制的弹出询问信息
function popConfirmMsgForControl(obj, msgbox, titleName) {
    if (obj.isCalled == undefined) {
        var callback = function (status) {
            if (status == "ok") {
                obj.isCalled = true;
                obj.click();
            }
        };
        ymPrompt.confirmInfo({ message: msgbox, title: titleName, handler: callback, maskAlpha: '0' });
    }

}
//以DIV ID号弹出普通弹出窗口
function showDiv(DivID, titleName, handler, mwidth, mheight) {
    var msg = $(DivID).html();   
     $(DivID).empty();
     if (mwidth > 700 || mwidth == null || mwidth <= 0)
         mwidth = 700;
     if (mheight > 480 || mheight == null || mheight <= 0)
         mheight = 480;    
    var callback = function (status) {
        if (status == "ok") {
            handler();
            $(DivID).html(msg);
        }
        else {
            $(DivID).html(msg);
        }
    };
    ymPrompt.win({ message: msg, title: titleName, handler: callback, maskAlpha: '0.4', btn: ['OK', 'CANCEL'], width: mwidth, height: mheight });
}
//以html内容方式弹出普通弹出窗口
function showHtml(html, titleName, handler, mwidth, mheight) {
    if (mwidth > 700 || mwidth == null || mwidth <= 0)
        mwidth =700;
    if (mheight > 480 || mheight == null || mheight <= 0)
        mheight = 480;    
    var callback = function (status) {
        if (status == "ok")
            handler();
    };
    ymPrompt.win({ message: html, title: titleName, handler: callback, maskAlpha: '0.4', btn: ['OK', 'CANCEL'], width: mwidth, height: mheight });
}
//关闭当前窗口
function closeWindow(handler) {
    window.parent.ymPrompt.close();
    if (typeof (handler) == "function") {
        handler('ok', true);
    }
}
//弹出页面模态窗口
function showWindow(titleName, url, mwidth, mheight) {
    if (mwidth > 600 || mwidth == null|| mwidth<=0)
        mwidth = 600;
    if (mheight > 480 || mheight == null || mheight<=0)
        mheight = 480;    
    ymPrompt.win({
        title: titleName            //弹出窗口标题
         , message: url              //消息内容或者路径
         , width: mwidth                //宽度
         , height: mheight               //高度
         ,maskAlphaColor:'#000'    //遮罩颜色
         , maskAlpha: '0.4'          //遮罩透明度
         , dragOut: false            //是否可以拖动到窗体外
         , iframe: true              //是否是弹出页面       
        //,winPos:[200,100]         //弹出窗口定位 默认居中 left,top
    });   
}

function isLoadFish() {
    if ($(".dv_information").length == 0) return;
    var height1 = $(window.parent.document).find(".ym-body").height();
    var height2 = $(".dv_submit").innerHeight();
    offsetHeight = height1 - height2 - $(".dv_information").offset().top;
    $(".dv_information").height(offsetHeight);
}
//点击选项卡
function clickTab(obj, selectedClass) {

    $(obj).addClass(selectedClass).siblings().removeClass(selectedClass);

}
//选项卡切换
function showTab(tabIdName, BlockName, selectedClass) {

    $("#" + tabIdName).addClass(selectedClass).siblings().removeClass(selectedClass);
    $("#" + BlockName).show().siblings().hide();
}

//得到ie的版本，判断是否是ie6
function Get_IE_Version() {
    var v;
    if (navigator.userAgent.indexOf("MSIE 6.0") > 0)//IE 6.0
    {
        v = 6;
    }
    else if (navigator.userAgent.indexOf("MSIE 7.0") > 0)//IE 7.0 
    {
        v = 7
    }
    else if (navigator.userAgent.indexOf("MSIE 8.0") > 0)//IE 8.0
    {
        v = 8;
    }
    else if (navigator.userAgent.indexOf("MSIE 9.0") > 0)//IE 9.0
    {
        v = 9;
    }
    else v = 0; //火狐firefox,或别的浏览器

    return v;
}

//异步方式提交表单(用于模态)
function AsyncSubmitOnModal(formid, url, method, callback) {
    var params = "1=1";
    if ($.browser.msie) {
        var iframe = parent.document.PopupWindow;
        var ret = $(iframe.document).find("#" + formid).find("input:radio:[checked='true'],input:checkbox[checked='true'],input[type='text'],input[type='password'],input[type='hidden'],textarea,select").each(function (index, el) {
            if (el.name != "") {
                params += "&" + el.name + "=" + encodeURIComponent(el.value);
            }
        });
    }
    else {
        var iframe = window.parent.document.getElementById("PopupWindow");
        var ret = $(iframe.contentDocument).find("#" + formid).find("input:radio:[checked='true'],input:checkbox[checked='true'],input[type='text'],input[type='password'],input[type='hidden'],textarea,select").each(function (index, el) {
            if (el.name != "") {
                params += "&" + el.name + "=" + encodeURIComponent(el.value);
            }
        });
    }
    params = params.replace('1=1&', '');


    var obj = new Object();
    $.ajax({
        url: url,
        type: method,
        dataType: "json",
        data: params,
        success: function (results) {
            obj.IsSuccess = results.Data.IsSuccess;
            obj.ErrorInfo = results.Data.ErrorInfo;
            if (typeof (callback) == "function") {
                callback(obj);
            }
        },
        error: function (msg) {
            //alert(msg.toString());
        },
        complete: function (msg) {
            // alert("complete:" + msg);
        }
    });

}

//异步方式提交表单(用于非模态)
function AsyncSubmit(formid, url, method, callback) {
    var params = "1=1";
    var ret = $("#" + formid).find("input:radio:[checked='true'],input:checkbox[checked='true'],input[type='text'],input[type='password'],input[type='hidden'],textarea,select").each(function (index, el) {
        if (el.name != "") {
            params += "&" + el.name + "=" + encodeURIComponent(el.value);
        }
    });
    params = params.replace('1=1&', '');

    var obj = new Object();
    $.ajax({
        url: url,
        type: method,
        dataType: "json",
        data: params,
        success: function (results) {
            obj.IsSuccess = results.Data.IsSuccess;
            obj.ErrorInfo = results.Data.ErrorInfo;
            if (typeof (callback) == "function") {
                callback(obj);
            }
        },
        error: function (msg) {
            //alert(msg.toString());
        },
        complete: function (msg) {
            // alert("complete:" + msg);
        }
    });
}


//异步方式提交表单
function AsyncSubmitNoForm(url, method, dataType, params, callback) {
    var obj = new Object();
    $.ajax({
        url: url,
        type: method, //"GET|POST"
        dataType: dataType, //"json|html"
        data: params,
        success: function (results) {
            if (typeof (callback) == "function") {
                callback(results);
            }
        },
        error: function (msg) {
            //alert(msg.toString());
        },
        complete: function (msg) {
            // alert("complete:" + msg);
        }
    });
}

//行全选
function SelectAll(thiz) {
    //全选
    var check = $(thiz).attr("checked");

    if (check == "checked") {
        $("input.input_checkbox").attr("checked", $(thiz).attr("checked"));
        $("input.input_selectAll").attr("checked", $(thiz).attr("checked"));
    }
    else {
        $("input.input_checkbox").removeAttr("checked");
        $("input.input_selectAll").removeAttr("checked");
    }
}
function len(str) {
    var i, sum = 0;
    for (i = 0; i < str.length; i++) {
        if ((str.charCodeAt(i) > 0) && (str.charCodeAt(i) < 255))
            sum = sum + 1;
        else
            sum = sum + 2;
    }
    return sum;
}
//格式化文件大小字符串(size为KB）
function FormatFileSize2(size) {
    var filesize = parseInt(size);
    if (filesize < 1024) {
        return size + "K";
    }
    else if (filesize < 1024 * 1024) {
        return parseInt(filesize / 1024) + "M";
    }
    else {
        return parseInt(filesize / (1024 * 1024)) + "G";
    }
}

function ReplaceScriptContent(str) {
    var tmpstr = str;
    tmpstr = tmpstr.replace(/<[^>]*script[^>]*>((\n|.)*)<\/[^>]*script[^>]*>/ig, '');
    return tmpstr;
}

//滚动条位置
function ScollPostion() {
    var t, l, w, h;
    if (document.documentElement && document.documentElement.scrollTop) {
        t = document.documentElement.scrollTop;
        l = document.documentElement.scrollLeft;
        w = document.documentElement.scrollWidth;
        h = document.documentElement.scrollHeight;
    } else if (document.body) {
        t = document.body.scrollTop;
        l = document.body.scrollLeft;
        w = document.body.scrollWidth;
        h = document.body.scrollHeight;
    }
    return { top: t, left: l, width: w, height: h };
}
//格式化浮点,加","自动四舍五入，控制精度
function FormatFloat(s, n) {
    n = n > 0 && n <= 20 ? n : 2;
    s = parseFloat((s + "").replace(/[^\d\.-]/g, "")).toFixed(n) + "";
    var l = s.split(".")[0].split("").reverse(),
   r = s.split(".")[1];
    t = "";
    for (i = 0; i < l.length; i++) {
        t += l[i] + ((i + 1) % 3 == 0 && (i + 1) != l.length ? "," : "");
    }
    return t.split("").reverse().join("") + "." + r;
}


function UpdateUrlWithParam(url, key, value) {

    var retUrl = url;

    if (retUrl.indexOf("?") == -1) {
        retUrl += "?" + key + "=" + value;
    }
    else {
        if (retUrl.indexOf("&" + key + "=") == -1) {
            if (retUrl.indexOf("?" + key + "=") == -1)
                retUrl += "&" + key + "=" + value;
            else
                retUrl = retUrl.replace(eval("/(\\?" + key + "=\\d*)/g"), "?" + key + "=" + value);
        } else
            retUrl = retUrl.replace(eval("/(\\&" + key + "=\\d*)/g"), "&" + key + "=" + value);

    }
    return retUrl;
}
function hideGridview(obj) {

    if ($(".dv_searchbox").find("table tr:gt(0)").is(":visible")) {
        $(".dv_searchbox").find("table tr:gt(0)").hide();
        $("#Highsearch").removeClass("dropupico");
    }
    else {
        $(".dv_searchbox").find("table tr:gt(0)").show();
        $("#Highsearch").addClass("dropupico");
    }
    setFrameHeight();
   
}


var time = "";
function setFrameHeight() {
    clearInterval(time);
    time = setInterval("autoHeight()", 50);   
}

function autoHeight() {
    var height1 = 0;
    var height2 = 0;
    var height3 = 0;
    var copyHeight = 0;
    var topHeight = 0;
    var headerHeight = 0;
    var m_width = 0;
    var bodyWidth = 0;
    var bodyHeight = 0;

    var mainbody = window.parent.document.body;
    var dv_main = parent.document.getElementById("dv_main");
    var copyright = parent.document.getElementById("copyright");
    var topPannel = parent.document.getElementById("TopPannel");
    var header = parent.document.getElementById("header");
    if (document.getElementById("myframe") == undefined)
        var rightFrame = parent.document.getElementById("myframe");
    else
        var rightFrame = document.getElementById("myframe");

    
    var dv_left = parent.document.getElementById("dv_left");
    var dv_right = parent.document.getElementById("dv_right");
    var dv_ul = parent.document.getElementById("dv_Menu");
   
    if (rightFrame == undefined)
        return;

    if (copyright == undefined)
        copyHeight=0;
    else
        copyHeight = copyright.clientHeight;

    if (topPannel == undefined)
        topHeight = 0;
    else
        topHeight = topPannel.clientHeight;

    if (header == undefined)
        headerHeight = 0;
    else
        headerHeight = header.clientHeight;

//    if (dv_main == undefined)
//        return;
//    else {
//        m_width = window.parent.document.body.scrollWidth;
//        bodyWidth = m_width < 823 ? 823 : m_width;
//    }

    if (dv_left == undefined)
        return;
    else {
        //        var leftWidth = dv_left.clientWidth;
        //        if (dv_left.style.display == "none")
        //            leftWidth = 0;
        //        dv_main.style.width = bodyWidth + "px";
        //        dv_right.style.width = bodyWidth - leftWidth + "px";

        

        if (rightFrame == undefined)
            return;
        else {
            bodyHeight = mainbody.clientHeight;
            var offsetHeight = bodyHeight - topHeight - headerHeight - copyHeight;
            if (rightFrame.contentWindow.document.readyState == "complete") {
                height1 = rightFrame.contentWindow.document.body.scrollHeight;
                height2 = dv_ul.scrollHeight;
                height3 = offsetHeight;
                rightFrame.height = Math.max(height1, height2, height3) + "px";
                var hello = parent.document.getElementById("TopPannel");
                if (time != undefined && time != null) clearInterval(time);
                dv_left.style.height = Math.max(height1, height2, height3) + "px";
                $(dv_left).css("min-height", Math.max(height1, height2, height3) + "px");

            }
        }

    }
}

//初始化翻页控件
function createPageControl() {
    var pagePanelSize = $(".dv_pagePanel");
    if (pagePanelSize.length < 2) return;
    var pagePanel1 = $(pagePanelSize).get(0);
    var pagePanel2 = $(pagePanelSize).get(1);
    $(pagePanel2).html($(pagePanel1).html());
    var checkbox1 = $(pagePanel1).find("input[type=checkbox]").get(0);
    if (checkbox1 == undefined) return;
    var checkbox2 = $(pagePanel2).find("input[type=checkbox]").get(0);
    $(checkbox1).click(function () {
        checkbox2.checked = checkbox1.checked;
    })
    $(checkbox2).click(function () {
        checkbox1.checked = checkbox2.checked;
    })
}
//展开和隐藏左边菜单
function expandTree() {
    if ($(".dv_left").is(":visible")) {
        $(".dv_left").animate({ "margin-left": "-=172" }, 300, function () {
            $(this).animate({ "margin-left": "+=22" }, 200, function () {
                $(this).animate({ "margin-left": "-=22" }, 150, function () {
                    $(this).hide();
                });                
            });
        });
        $(".hideFrame").show();        
    }
    else {
            $(".dv_left").show();
            $(".dv_left").animate({ "margin-left": "+=172" }, 300, function () {
            $(this).animate({ "margin-left": "-=22" }, 200, function () {
                $(this).animate({ "margin-left": "+=22" }, 150);
            });
        });
        $(".hideFrame").hide();        
    }    
}



//触发查询事件
function triggerSearchEvent() {
    //查找搜索按钮
    var btnSearchs = $(".btn_Search");
    if (btnSearchs.length < 1) return;
    //触发搜索事件
    btnSearchs.get(0).click();
}
//触发当前页刷新事件
function triggerRefreshEvent() {
    //查找翻页go按钮
    var btnGos = $(".btn_go");
    if (btnGos.length < 1) return;
    //触发翻页事件
    btnGos.get(0).click();
}

//格式化文件大小字符串(size为字节），精确计量
function FormatFileSize(size) {
    var filesize = parseInt(size);
    if (filesize < 1024) {
        return size + '字节';
    }
    else if (filesize < 1024 * 1024) {
        return FormatFloat(filesize / 1024, 2) + "K";
    }
    else if (filesize < 1024 * 1024 * 1024) {
        return FormatFloat(filesize / (1024 * 1024), 2) + "M";
    }
    else {
        return FormatFloat(filesize / (1024 * 1024 * 1024), 2) + "G";
    }
}

var DragAndDrop = function () {
    var _clientWidth;
    var _clientHeight;
    var _controlObj;
    var _dragObj;
    var _flag = false;
    var _dragObjCurrentLocation;
    var _mouseLastLocation;

    var getElementDocument = function (element) {
        return element.ownerDocument || element.document;
    };

    var dragMouseDownHandler = function (evt) {
        if (_dragObj) {
            evt = evt || window.event;
            _clientWidth = window.parent.window.document.body.clientWidth;
            _clientHeight = window.parent.window.document.documentElement.scrollHeight;

            $(_dragObj).find("#bgdiv").css("display", "");
            _flag = true;
            _dragObjCurrentLocation = {
                x: $(_dragObj).offset().left,
                y: $(_dragObj).offset().top

            };
            _mouseLastLocation = {
                x: evt.screenX,
                y: evt.screenY
            };
            $(parent.document).bind("mousemove", dragMouseMoveHandler);
            $(parent.document).bind("mouseup", dragMouseUpHandler);

            if (evt.preventDefault)
                evt.preventDefault();
            else
                evt.returnValue = false;
        }
    };

    var dragMouseMoveHandler = function (evt) {
        if (_flag) {
            evt = evt || window.event;
            var _mouseCurrentLocation = {
                x: evt.screenX,
                y: evt.screenY
            };
            _dragObjCurrentLocation.x = _dragObjCurrentLocation.x + (_mouseCurrentLocation.x - _mouseLastLocation.x);
            _dragObjCurrentLocation.y = _dragObjCurrentLocation.y + (_mouseCurrentLocation.y - _mouseLastLocation.y);
            _mouseLastLocation = _mouseCurrentLocation;
            $(_dragObj).offset({ left: _dragObjCurrentLocation.x, top: _dragObjCurrentLocation.y });
            if (evt.preventDefault)
                evt.preventDefault();
            else
                evt.returnValue = false;
        }
    };

    var dragMouseUpHandler = function (evt) {
        if (_flag) {
            evt = evt || window.event;
            $(_dragObj).find("#bgdiv").css("display", "none");
            cleanMouseHandlers();
            _flag = false;
        }
    };
    var cleanMouseHandlers = function () {
        if (_controlObj) {
            $(_controlObj.document).unbind("mousemove");
            $(_controlObj.document).unbind("mouseup");
        }
    };
    return {
        Register: function (dragObj, controlObj) {
            _dragObj = dragObj;
            _controlObj = controlObj;
            $(_controlObj).bind("mousedown", dragMouseDownHandler);
        }
    }
} ();

var DragAndDropNoFrame = function () {

    var _clientWidth;
    var _clientHeight;
    var _controlObj;
    var _dragObj;
    var _flag = false;
    var _dragObjCurrentLocation;
    var _mouseLastLocation;
    var getElementDocument = function (element) {
        return element.ownerDocument || element.document;
    };
    var dragMouseDownHandler = function (evt) {
        if (_dragObj) {
            evt = evt || window.event;
            _clientWidth = window.document.body.clientWidth;
            _clientHeight = window.document.documentElement.scrollHeight;
            $(_dragObj).find("#bgdiv").css("display", "");
            _flag = true;

            _dragObjCurrentLocation = {
                x: $(_dragObj).offset().left,
                y: $(_dragObj).offset().top
            };

            _mouseLastLocation = {
                x: evt.screenX,
                y: evt.screenY
            };
            $(document).bind("mousemove", dragMouseMoveHandler);
            $(document).bind("mouseup", dragMouseUpHandler);
            if (evt.preventDefault)
                evt.preventDefault();
            else
                evt.returnValue = false;
        }
    };

    var dragMouseMoveHandler = function (evt) {
        if (_flag) {
            evt = evt || window.event;
            var _mouseCurrentLocation = {
                x: evt.screenX,
                y: evt.screenY
            };
            _dragObjCurrentLocation.x = _dragObjCurrentLocation.x + (_mouseCurrentLocation.x - _mouseLastLocation.x);
            _dragObjCurrentLocation.y = _dragObjCurrentLocation.y + (_mouseCurrentLocation.y - _mouseLastLocation.y);
            _mouseLastLocation = _mouseCurrentLocation;
            $(_dragObj).offset({ left: _dragObjCurrentLocation.x, top: _dragObjCurrentLocation.y });
            if (evt.preventDefault)
                evt.preventDefault();
            else
                evt.returnValue = false;
        }
    };
    var dragMouseUpHandler = function (evt) {
        if (_flag) {
            evt = evt || window.event;
            $(_dragObj).find("#bgdiv").css("display", "none");
            cleanMouseHandlers();
            _flag = false;
        }
    };
    var cleanMouseHandlers = function () {
        if (_controlObj) {
            $(_controlObj.document).unbind("mousemove");
            $(_controlObj.document).unbind("mouseup");
        }
    };
    return {
        Register: function (dragObj, controlObj) {
            _dragObj = dragObj;
            _controlObj = controlObj;
            $(_controlObj).bind("mousedown", dragMouseDownHandler);
        }
    }
} ();