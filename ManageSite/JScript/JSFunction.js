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
    ymPrompt.alert({ message: msgbox, title: titleName, handler: callback, maskAlpha: '0.4', width: mwidth, height: mheight, closeBtn: false });
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
    ymPrompt.succeedInfo({ message: msgbox, title: titleName, handler: callback, maskAlpha: '0.4', width: mwidth, height: mheight, closeBtn: false });
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
    ymPrompt.errorInfo({ message: msgbox, title: titleName, handler: callback, maskAlpha: '0.4', width: mwidth, height: mheight, closeBtn: false });
}

//弹出失败信息
function popBusinessRegulerMsg(msgbox, titleName, handler, mwidth, mheight) {
    if (mwidth > 320 || mwidth == null || mwidth <= 0)
        mwidth = 320;
    if (mheight > 220 || mheight == null || mheight <= 0)
        mheight = 220;

    ymPrompt.errorInfo({ message: msgbox, title: titleName, maskAlpha: '0.4', width: mwidth, height: mheight, needCloseCurrentWindow: false, closeBtn: false });
}

//弹出询问信息
function popConfirmMsg(msgbox, titleName, handler, mwidth, mheight) {
    if (mwidth > 320 || mwidth == null || mwidth <= 0)
        mwidth = 320;
    if (mheight > 220 || mheight == null || mheight <= 0)
        mheight = 220;
    var callback = function (status) {
        if (status == "ok")
            handler();
    };
    ymPrompt.confirmInfo({ message: msgbox, title: titleName, handler: callback, maskAlpha: '0.4', width: mwidth, height: mheight, closeBtn: false });
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
        mwidth = 700;
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
    //winPosition();
    if (mwidth == null || mwidth <= 0)
        mwidth = 800;
    if (mheight == null || mheight <= 0 || mheight > 530)
        mheight = 530;
    var left = getWinLeft(mwidth);

    ymPrompt.win({
        title: titleName            //弹出窗口标题
         , message: url              //消息内容或者路径
         , width: mwidth                //宽度
         , height: mheight               //高度
         , maskAlphaColor: '#000'    //遮罩颜色
         , maskAlpha: '0.4'          //遮罩透明度
         , dragOut: false            //是否可以拖动到窗体外
         , iframe: true              //是否是弹出页面       
         , winPos: [left, '10']         //弹出窗口定位 默认居中 left,top
    });

    autoHeight();
}

function getWinLeft(mwidth)
{
    var left = 0;
    var winWidth = $(window).width();
    if (winWidth > mwidth)
        left = (winWidth - mwidth) / 2;
    return left;
}

//弹出页面模态窗口
function showCodeWindow(titleName, url, mwidth, mheight) {
    if (mwidth > 600 || mwidth == null || mwidth <= 0)
        mwidth = 600;
    if (mheight > 480 || mheight == null || mheight <= 0)
        mheight = 480;
    titleName = unescape(titleName);
    ymPrompt.win({
        title: titleName            //弹出窗口标题
         , message: url              //消息内容或者路径
         , width: mwidth                //宽度
         , height: mheight               //高度
         , maskAlphaColor: '#000'    //遮罩颜色
         , maskAlpha: '0.4'          //遮罩透明度
         , dragOut: false            //是否可以拖动到窗体外
         , iframe: true              //是否是弹出页面       
        //,winPos:[200,100]         //弹出窗口定位 默认居中 left,top
    });

    autoHeight();
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


function ReplaceScriptContent(str) {
    var tmpstr = str;
    tmpstr = tmpstr.replace(/<[^>]*script[^>]*>((\n|.)*)<\/[^>]*script[^>]*>/ig, '');
    return tmpstr;
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

function hideGridview() {
    if ($(".dv_searchbox").find("table tr:gt(0)").is(":visible") && $(".noneetHide").length == 0) {
        $(".dv_searchbox").find("table tr:gt(0)").hide();
        $("#Highsearch").removeClass("dropupico");
        setCacheValue("ShowSearchTable", '0');
    }
    else {
        $(".dv_searchbox").find("table tr:gt(0)").each(function (i, o) {
            if (!$(o).hasClass('hide'))
            {
                $(o).show();
            }
        });
        $("#Highsearch").addClass("dropupico");
        setCacheValue("ShowSearchTable", '1');
    }
    setFrameHeight();
}

//页面多个页签时 高级查询会联动 加一个ID 区分
function hideGridviewByID(divID) {
    if ($("#" + divID).find("table tr:gt(0)").is(":visible") && $(".noneetHide").length == 0) {
        $("#" + divID).find("table tr:gt(0)").hide();
        $("#Highsearch_" + divID).removeClass("dropupico");
        setCacheValue("ShowSearchTable", '0');
    }
    else {
        $("#" + divID).find("table tr:gt(0)").show();
        $("#Highsearch_" + divID).addClass("dropupico");
        setCacheValue("ShowSearchTable", '1');
    }
    setFrameHeight();
}

function autoLoadHideGridview() {
    var cookieValue = getCacheValue("ShowSearchTable");
    if (cookieValue != '1') {
        $(".dv_searchbox").find("table tr:gt(0)").hide();
        $("#Highsearch").removeClass("dropupico");
    }
    else
        $("#Highsearch").addClass("dropupico");
}

//选项卡切换
function showTab(tabIdName, BlockName, selectedClass) {

    $("#" + tabIdName).addClass(selectedClass).siblings().removeClass(selectedClass);
    $("#" + BlockName).show().siblings().hide();
    setCookie(tabIdName);
    createPageControl();
    setFrameHeight();
}
function showTabinfor() {
    var cookieValue = getCacheValue("dv_TabmenusIndex");
    if (cookieValue == null) cookieValue = 0;
    $(".dv_Tabmenus li").eq(Number(cookieValue)).addClass("selected");
    $(".info>div").eq(Number(cookieValue)).show().siblings().hide();
    createPageControl();
}

function setCookie(obj) {
    index_dt = $(".dv_Tabmenus li").index($("#" + obj)).toString();
    setCacheValue("dv_TabmenusIndex", index_dt);
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
        copyHeight = 0;
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

    var isIE = ! +'\v1';    //是否为IE浏览器
    var IE6 = isIE && /MSIE (\d)\./.test(navigator.userAgent) && parseInt(RegExp.$1) < 7;  //是否为IE6

    if (dv_left == undefined)
        return;
    else {

        if (rightFrame == undefined)
            return;
        else {
            if (rightFrame.contentWindow.document.readyState == "complete") {
                if (rightFrame.contentWindow.document.body.offsetHeight) {
                    height1 = rightFrame.contentWindow.document.body.offsetHeight;
                }
                else if (rightFrame.contentWindow.document.body.scrollHeight) {
                    height1 = rightFrame.contentWindow.document.body.scrollHeight;
                }
                height2 = dv_ul.scrollHeight;
                bodyHeight = parent.document.documentElement.clientHeight;
                var offsetHeight = bodyHeight - topHeight - headerHeight - copyHeight;
                height3 = offsetHeight;
                if (time != undefined && time != null) clearInterval(time);
                rightFrame.height = Math.max(height1, height2, height3) + "px";
                dv_left.style.height = Math.max(height1, height2, height3) + "px";
                $(dv_left).css("min-height", Math.max(height1, height2, height3) + "px");
                parent.window.scrollTo(0, 0);
                // $(mainbody).find(".hello").html("bodyHeight:" + bodyHeight +"height1:"+height1+ "resultHeight:" + Math.max(height1, height2, height3));
            }
        }
    }
}
//初始化翻页控件
function createPageControl() {
    var pagePanelSize = $(".dv_searchlist:visible").find(".dv_pagePanel");
    var pagePanel1 = $(pagePanelSize).get(0);
    var checkboxid1 = $(pagePanelSize).find("input[type='checkbox']:visible").attr("id");
    if (pagePanelSize.length == 2) {
        var pagePanel2 = $(pagePanelSize).get(1);
        var checkboxid2 = $(pagePanelSize).find("input[type='checkbox']:visible").attr("id") + "__checkbox";
        $(pagePanel2).html($(pagePanel1).html());
        $(pagePanel2).find("input[type='checkbox']:visible").attr("id", checkboxid2);
        $("#" + checkboxid2).click(function () {
            SelectAllCheckbox(this);
        })
    }
    $("#" + checkboxid1).click(function () {
        SelectAllCheckbox(this);
    })
    var checkbox1 = $(pagePanel1).find("input[type=checkbox]:visible").get(0);
    if (checkbox1 == undefined) return;
    if (pagePanelSize.length == 2) {
        var checkbox2 = $(pagePanel2).find("input[type=checkbox]:visible").get(0);
        $(checkbox2).click(function () {
            checkbox1.checked = checkbox2.checked;
        })
    }
    $(checkbox1).click(function () {
        if (checkbox2 != undefined)
            checkbox2.checked = checkbox1.checked;
    })

}
function IsSelectRecord(checkMsg) {
    var selectNumber = $(".dv_searchlist:visible").find("input[type=checkbox]:checked").length;
    var reurnResult = selectNumber == 0 ? false : true;

    if (!reurnResult) {
        popFailedMsg(checkMsg, "提示信息", null, 320, 220);
    }
    return reurnResult;
}
//行全选
function SelectAllCheckbox(thiz) {
    //全选  
    var check = $(thiz).attr("checked");
    if (check == "checked" || check == true) {
        $(".dv_searchlist:visible").find("input[type=checkbox]").each(function () {
            if ($(this).attr("disabled") != "disabled")
                $(this).attr("checked", $(thiz).attr("checked"));
        })
    }
    else {
        $(".dv_searchlist:visible").find("input[type=checkbox]").removeAttr("checked");
    }
    $("input").radioStyle();
}

//展开和隐藏左边菜单
function expandTree() {
    if ($(".dv_left").is(":visible")) {
        $(".dv_left").animate({ "margin-left": "-172px" }, 300, function () {
            // $(this).animate({ "margin-left": "+=22" }, 200, function () {
            //$(this).animate({ "margin-left": "-=22" }, 150, function () {
            $(this).hide();
            //});
            // });
        });
        $(".hideFrame").show();
    }
    else {
        $(".dv_left").show();
        $(".dv_left").animate({ "margin-left": "0px" }, 300, function () {
            //$(this).animate({ "margin-left": "-=22" }, 200, function () {
            //$(this).animate({ "margin-left": "+=22" }, 150);
            //});
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
//触发查询事件
function triggerParentSearchEvent() {
    //查找搜索按钮
    var btnSearchs = $(window.parent.document).find(".btn_Search");
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
}();

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
}();


//Ajax调用页面方法
function CallWebServiceMethod(url, param) {
    var _return = null;
    $.ajax({
        async: false,
        type: "POST",
        url: url,
        dataType: "html",
        data: param,
        success: function (data) {
            //alert("success:" + data);
            _return = data;
        },
        error: function (response) {
            alert(response.responseTEXT + "  error");
        }
    });

    return _return;
}

