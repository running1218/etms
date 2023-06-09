﻿/**
 * ymPrompt.js 弹窗消息提示组件
 * @author chenzw
 */
//<meta http-equiv="X-UA-Compatible" content="IE=7" />  IE8透明度解决方案

(function () {
    if (window.ymPrompt) return;
    window.ymPrompt = {
        version: '4.0',
        pubDate: '2009-02-16',
        apply: function (o, c, d) {
            if (d) ymPrompt.apply(o, d);
            if (o && c && typeof c == 'object') for (var p in c) o[p] = c[p];
            return o;
        },
        eventList: []
    };

    /**//*初始化可能在页面加载完成调用的接口，防止外部调用失败。_initFn:缓存初始调用传入的参数*/
    var initFn = ['setDefaultCfg', 'show'], _initFn = {}, t;
    while (t = initFn.shift()) ymPrompt[t] = eval('0,function(){_initFn.' + t + '=arguments}');

    /**//*以下为公用函数及变量*/
    var isIE = ! +'\v1';    //IE浏览器
    var isCompat = document.compatMode == 'CSS1Compat';
    var IE6 = isIE && /MSIE (\d)\./.test(navigator.userAgent) && parseInt(RegExp.$1) < 7; //IE6需要用iframe来遮罩
    var useFixed = !isIE || (!IE6 && isCompat);    //滚动时IE7+及其它浏览器使用Fixed定位
    var $ = function (id) {
        return document.getElementById(id)
    }; //获取元素
    var $height = function (obj) {
        return parseInt(obj.style.height) || obj.offsetHeight
    }; //获取元素高度
    var addEvent = (function () {
        return new Function('env', 'fn', 'obj', ['obj=obj||document;', window.attachEvent ? "obj.attachEvent('on'+env,fn)" : 'obj.addEventListener(env,fn,false)', ';ymPrompt.eventList.push([env,fn,obj])'].join(''))
    })(); //事件绑定
    var detachEvent = (function () {
        return new Function('env', 'fn', 'obj', ['obj=obj||document;', window.attachEvent ? "obj.detachEvent('on'+env,fn)" : 'obj.removeEventListener(env,fn,false)'].join(''))
    })(); //取消事件绑定

    //为元素的特定样式属性设定值
    var setStyle = function (el, n, v) {
        if (!el) return;
        if (typeof n == 'object') {
            for (var i in n) setStyle(el, i, n[i]);
            return;
        }
        /**//*dom数组或dom集合*/
        if (el instanceof Array || /htmlcollection|nodelist/i.test('' + el)) {
            for (var i = el.length - 1; i >= 0; i--) setStyle(el[i], n, v);
            return;
        }
        try { el.style[n] = v } catch (e) { }
    };
    /**//*----------------和业务有关的公用函数-----------------*/
    var btnIndex = 0, btnCache, seed = 0; //当前焦点的按钮的索引、当前存在的按钮、id种子
    /**//*创建按钮*/
    var mkBtn = function (txt, sign, autoClose, id) {
        if (!txt) return;
        if (txt instanceof Array) {
            /**//*无效按钮删除*/
            var item, t = [], dftBtn = { OK: [curCfg.okTxt, 'ok'], CANCEL: [curCfg.cancelTxt, 'cancel'] };
            while (txt.length) (item = txt.shift()) && t[t.push(mkBtn.apply(null, dftBtn[item] || item)) - 1] || t.pop();
            return t;
        }
        id = id || 'ymPrompt_btn_' + seed++;
        autoClose = autoClose == undefined ? 'undefined' : !!autoClose;
        return {
            id: id,
            html: "<input type='button' id='" + id + "' onclick='ymPrompt.doHandler(\"" + sign + "\"," + autoClose + ")' style='cursor:pointer' class='btnStyle handler' value='" + txt + "' />"
        };
    };
    /**//*生成按钮组合的html*/
    var joinBtn = function (btn) {
        if (!btn) return btnCache = '';
        if (!(btn instanceof Array)) btn = [btn];
        if (!btn.length) return btnCache = '';
        btnCache = btn.concat();
        var html = [];
        while (btn.length) html.push(btn.shift().html);
        return html.join('&nbsp;&nbsp;');
    }
    /**//*默认显示配置及用户当前配置*/
    var dftCfg = {
        titleBar: true,
        fixPosition: true,
        dragOut: false,
        autoClose: true,
        showMask: true,
        maskAlphaColor: '#000',    //遮罩透明色
        maskAlpha: 0.1,        //遮罩透明度
        winAlpha: 0.8,    //拖动窗体时窗体的透明度
        title: '标题',        //消息框标题
        message: '内容',    //消息框按钮
        width: 300,
        height: 185,
        winPos: 'c',
        iframe: false,
        btn: null,
        closeBtn: true,
        closeTxt: '关闭',
        okTxt: ' 确 定 ',
        cancelTxt: ' 取 消 ',
        icoCls: '',
        handler: function () { } //回调事件
    }, curCfg = {};
    /**//*开始解析*/
    (function () {
        var rootEl = document.body;
        if (!rootEl || typeof rootEl != 'object') return addEvent('load', arguments.callee, window); //等待页面加载完成
        rootEl = isCompat ? document.documentElement : rootEl; //根据html Doctype获取html根节点，以兼容非xhtml的页面
        var noFS = /body/i.test(document.body);    //不是frameset页面
        if (!isIE && !noFS) return;    //frameset页面且不是IE则直接返回，否则会出现错误。
        /**//*保存窗口定位信息*/
        var saveWinInfo = function () {
            ymPrompt.apply(dragVar, {
                _offX: ym_win.offsetLeft - rootEl.scrollLeft,    //弹出框相对屏幕的位移差
                _offY: ym_win.offsetTop - rootEl.scrollTop
            });
        };
        /**//*-------------------------创建弹窗html-------------------*/
        var maskStyle = 'position:absolute;top:0;left:0;display:none;text-align:center';
        var div = document.createElement('div');
        div.innerHTML = [
        /**//*遮罩*/
        "<div id='maskLevel' style=\'" + maskStyle + ';z-index:10000;\'></div>', IE6 ? ("<iframe id='maskIframe' style='" + maskStyle + ";z-index:9999;filter:alpha(opacity=0);opacity:0'></iframe>") : '',
        /*窗体*/
        "<div id='ym-window' style='position:absolute;z-index:10001;display:none'>", IE6 ? "<iframe scrolling='no' style='display:none;width:100%;height:100%;position:absolute;top:0;left:0;z-index:-1'></iframe>" : '', "<div class='ym-tl' id='ym-tl'><div class='ym-tr'><div class='ym-tc' style='cursor:move;'><div class='ym-header-text'></div><div class='ym-header-tools'></div><div class='ym-redArrow'></div></div></div></div>", "<div class='ym-ml' id='ym-ml'><div class='ym-mr'><div class='ym-mc'><div class='ym-body'></div></div></div></div>", "<div class='ym-ml' id='ym-btnl'><div class='ym-mr'><div class='ym-btn'></div></div></div>", "<div class='ym-bl' id='ym-bl'><div class='ym-br'><div class='ym-bc'></div></div></div>", "</div>"].join('');
        document.body.appendChild(div), div = null;

        var dragVar = {};
        /**//*mask、window*/
        var maskLevel = $('maskLevel');
        var ym_win = $('ym-window');

        /**//*header*/
        var ym_headbox = $('ym-tl');
        var ym_head = ym_headbox.firstChild.firstChild;
        var ym_hText = ym_head.firstChild;
        var ym_hTool = ym_hText.nextSibling;
        /**//*content*/
        var ym_body = $('ym-ml').firstChild.firstChild.firstChild;
        /**//*button*/
        var ym_btn = $('ym-btnl');
        var ym_btnContent = ym_btn.firstChild.firstChild;
        /**//*bottom*/
        var ym_bottom = $('ym-bl');
        var maskEl = [maskLevel];
        IE6 && maskEl.push($('maskIframe'));

        /**//*绑定事件*/
        var getWinSize = function () { return [Math.max(rootEl.scrollWidth, rootEl.clientWidth), Math.max(rootEl.scrollHeight, rootEl.clientHeight)] };
        var winSize = getWinSize();    //保存页面的实际大小
        //        var bindEl = ym_head.setCapture && ym_head;    //绑定拖放事件的对象
        //        var mEvent = function (e) {
        //            e = e || window.event;
        //            var sLeft = dragVar.offX + (e.x || e.pageX);
        //            var sTop = dragVar.offY + (e.y || e.pageY);

        //            if (!curCfg.dragOut) {
        //                var flag = curCfg.fixPosition && useFixed;    //自动滚动并使用fixed定位
        //                var sl = flag ? 0 : rootEl.scrollLeft, st = flag ? 0 : rootEl.scrollTop;
        //                sLeft = Math.min(Math.max(sLeft, sl), rootEl.clientWidth - ym_win.offsetWidth + sl);
        //                sTop = Math.min(Math.max(sTop, st), rootEl.clientHeight - ym_win.offsetHeight + st);
        //            } else if (curCfg.showMask && '' + winSize != '' + getWinSize())
        //                resizeMask(true);
        //            setStyle(ym_win, { left: sLeft + 'px', top: sTop + 'px' });
        //        };    //mousemove事件
        //        var uEvent = function () {
        //            /**//*frameset下不能实现半透明否则会消失，原因不明*/
        //            noFS && curCfg.winAlpha != 1 && setStyle(ym_win, isIE ? { filter: 'Alpha(opacity=100)'} : { opacity: 1 });    //鼠标按下时取消窗体的透明度
        //            detachEvent("mousemove", mEvent, bindEl);
        //            detachEvent("mouseup", uEvent, bindEl);
        //            saveWinInfo(); //保存当前窗口的位置
        //            bindEl && (detachEvent("losecapture", uEvent, bindEl), bindEl.releaseCapture());
        //            rootEl.style.cursor = rootEl._cursor;
        //        };    //mouseup事件
        //        addEvent('mousedown', function (e) {
        //            rootEl._cursor = rootEl.style.cursor || '';
        //            rootEl.style.cursor = 'pointer';
        //            e = e || window.event;
        //            /**//*frameset下不能实现半透明否则会消失，原因不明*/
        //            noFS && curCfg.winAlpha != 1 && setStyle(ym_win, isIE ? { filter: 'Alpha(opacity=' + curCfg.winAlpha * 100 + ')'} : { opacity: curCfg.winAlpha }); //鼠标按下时窗体的透明度
        //            ymPrompt.apply(dragVar, {
        //                offX: ym_win.offsetLeft - (e.x || e.pageX),    //鼠标与弹出框的左上角的位移差
        //                offY: ym_win.offsetTop - (e.y || e.pageY)
        //            });
        //            addEvent("mousemove", mEvent, bindEl);
        //            addEvent("mouseup", uEvent, bindEl);
        //            bindEl && (addEvent("losecapture", uEvent, bindEl), bindEl.setCapture());
        //        }, ym_head);

        /**//*键盘监听*/
        var keydownEvent = function (e) {
            var e = e || event, keyCode = e.keyCode;
            if (keyCode == 27) destroy(); //esc键
            if (btnCache) {
                var l = btnCache.length, nofocus;
                /**//*tab键/左右方向键切换焦点*/
                document.activeElement && document.activeElement.id != btnCache[btnIndex].id && (nofocus = true);
                if (keyCode == 9 || keyCode == 39) nofocus && (btnIndex = -1), $(btnCache[++btnIndex == l ? (--btnIndex) : btnIndex].id).focus();
                if (keyCode == 37) nofocus && (btnIndex = l), $(btnCache[--btnIndex < 0 ? (++btnIndex) : btnIndex].id).focus();
                if (keyCode == 13) return true;
            }
            /**//*禁止F1-F12/ tab 回车*/
            return keyEvent(e, (keyCode > 110 && keyCode < 123) || keyCode == 9 || keyCode == 13);
        };
        /**//*页面滚动弹出窗口滚动*/
        var scrollEvent = function () {
            setStyle(ym_win, {
                left: dragVar._offX + rootEl.scrollLeft + 'px',
                top: dragVar._offY + rootEl.scrollTop + 'px'
            });
        };
        /**//*监听键盘事件*/
        var keyEvent = function (e, d) {
            e = e || event;
            /**//*允许对表单项进行操作*/
            if (!d && /input|select|textarea/i.test((e.srcElement || e.target).tagName)) return true;
            try {
                e.returnValue = false;
                e.keyCode = 0;
            } catch (ex) {
                e.preventDefault && e.preventDefault();
            }
            return false;
        };
        maskLevel.oncontextmenu = ym_win.onselectstart = ym_win.oncontextmenu = keyEvent; //禁止右键
        /**//*重新计算遮罩的大小*/
        var resizeMask = function (noDelay) {
            setStyle(maskEl, 'display', 'none');    //先隐藏
            var size = getWinSize();
            var resize = function () {
                setStyle(maskEl, {
                    width: size[0] + 'px',
                    height: size[1] + 'px',
                    display: ''
                });
            };
            isIE ? noDelay === true ? resize() : setTimeout(resize, 0) : resize();
            setWinSize();
        };
        /**//*蒙版的显示隐藏,state:true显示,false隐藏，默认为true*/
        var maskVisible = function (visible) {
            if (!curCfg.showMask) return;
            (visible === false ? detachEvent : addEvent)("resize", resizeMask, window);
            if (visible === false) return setStyle(maskEl, 'display', 'none');
            setStyle(maskLevel, {
                background: curCfg.maskAlphaColor,
                filter: 'Alpha(opacity=' + curCfg.maskAlpha * 100 + ')',
                opacity: curCfg.maskAlpha
            });
            resizeMask(true);
        };
        var getPos = function (f) {
            var flag = curCfg.fixPosition && useFixed;
            var pos = [rootEl.clientWidth - ym_win.offsetWidth, (document.documentElement.clientHeight - ym_win.offsetHeight) / 2 + document.documentElement.scrollTop, flag ? 0 : rootEl.scrollLeft, flag ? 0 : rootEl.scrollTop];
            var arr = f.replace(/\{(\d)\}/g, function (s, s1) { return pos[s1] }).split(',');
            return [eval(arr[0]), eval(arr[1])];
        };
        var posMap = {
            c: '{0}/2+{2},{1}/2+{3}',
            l: '{2},{1}/2+{3}',
            r: '{0}+{2},{1}/2+{3}',
            t: '{0}/2+{2},{3}',
            b: '{0}/2,{1}+{3}',
            lt: '{2},{3}',
            lb: '{2},{1}+{3}',
            rb: '{0}+{2},{1}+{3}',
            rt: '{0}+{2},{3}'
        };
        /**//*设定窗口大小及定位*/
        var setWinSize = function (w, h) {
            if (!ym_win || ym_win.style.display == 'none') return;
            curCfg.height = parseInt(h) || curCfg.height;
            curCfg.width = parseInt(w) || curCfg.width;
            setStyle(ym_win, {
                //                left: 0,
                //                top: 0,
                width: curCfg.width + 'px',
                height: curCfg.height + 'px'
            });
            var pos = posMap[curCfg.winPos];
            pos = pos ? getPos(pos) : curCfg.winPos; //支持自定义坐标
            if (!(pos instanceof Array)) pos = getPos(posMap['c']);
            setStyle(ym_win, {
                top: pos[1] + 'px',
                left: pos[0] + 'px'
            });
            saveWinInfo();    //保存当前窗口位置信息          
            setStyle(ym_body, 'height', curCfg.height - $height(ym_headbox) - $height(ym_btn) - $height(ym_bottom) + 'px'); //设定内容区的高度
        };
        var _obj = [];    //IE中可见的obj元素
        var winVisible = function (visible) {
            var fn = visible === false ? detachEvent : addEvent;
            if (curCfg.fixPosition && !useFixed) fn('scroll', scrollEvent, window);
            setStyle(ym_win, 'position', curCfg.fixPosition && useFixed ? 'fixed' : 'absolute');

            fn('keydown', keydownEvent);
            if (visible === false) {
                setStyle(ym_win, 'display', 'none');
                setStyle(_obj, 'visibility', 'visible');
                _obj = [];
                return;
            }
            isIE && setStyle(ym_win, 'filter', '');    //清除IE下的阴影滤镜，避免在fixed定位时iframe内容不可见的bug
            for (var o = document.getElementsByTagName('object'), i = o.length - 1; i > -1; i--) o[i].style.visibility != 'hidden' && _obj.push(o[i]) && (o[i].style.visibility = 'hidden');
            setStyle([ym_hText, ym_hTool], 'display', (curCfg.titleBar ? '' : 'none'));
            ym_head.className = 'ym-tc' + (curCfg.titleBar ? '' : ' ym-ttc');
            ym_hText.innerHTML = curCfg.title; //标题
            ym_hTool.innerHTML = curCfg.closeBtn ? "<div class='ymPrompt_close' title='" + curCfg.closeTxt + "' onclick='ymPrompt.doHandler(\"close\")'><a href=\"#\"></a></div>" : '';
            ym_body.innerHTML = !curCfg.iframe ? ('<table style="margin-bottom:0px;height:100%;width:82%;padding:0px"><tr><td width="70"><div class=' + curCfg.icoCls + '></div></td><td>' + curCfg.message + '</td></tr></table>') : "<iframe scrolling='no' id='iframe1' width='100%' onload='this.height=this.contentWindow.document.documentElement.scrollHeight' style='overflow:hidden;' height='100%' border='0' frameborder='0' src='" + curCfg.message + "'></iframe>"; //内容
            (function (el, obj) { for (var i in obj) try { el[i] = obj[i] } catch (e) { } })(ym_body.firstChild, curCfg.iframe); //为iframe添加自定义属性
            //ym_body.className = "ym-body " + curCfg.icoCls; //图标类型
            setStyle(ym_btn, 'display', ((ym_btnContent.innerHTML = joinBtn(mkBtn(curCfg.btn))) ? '' : 'none')); //没有按钮则隐藏
            setStyle(ym_win, 'display', '');
            setWinSize();    //定位窗口
            btnCache && $(btnCache[btnIndex = 0].id).focus(); //第一个按钮获取焦点


        }; //初始化
        var isShow = false;
        var init = function () {
            isShow = true;
            maskVisible();
            winVisible();
        }; //销毁
        var destroy = function () {
            isShow = false;
            maskVisible(false);
            winVisible(false);
        };

        ymPrompt.apply(ymPrompt, {
            close: destroy,
            getPage: function () {
                return curCfg.iframe ? ym_body.firstChild : null
            },
            /**//*显示消息框,fargs:优先配置，会覆盖args中的配置*/
            show: function (args, fargs) {
                //if (isShow) ymPrompt.doHandler('close', curCfg.autoClose, true);
                /**//*支持两种参数传入方式:(1)JSON方式 (2)多个参数传入*/
                var a = [].slice.call(args, 0), o = {};
                if (typeof a[0] != 'object') {
                    var cfg = ['message', 'width', 'height', 'title', 'handler', 'maskAlphaColor', 'maskAlpha', 'iframe', 'icoCls', 'btn', 'autoClose', 'fixPosition', 'dragOut', 'titleBar', 'showMask', 'winPos', 'winAlpha'];
                    for (var i = 0, l = a.length; i < l; i++) if (a[i]) o[cfg[i]] = a[i];
                } else {
                    o = a[0];
                }
                ymPrompt.apply(curCfg, ymPrompt.apply({}, o, fargs), ymPrompt.setDefaultCfg()); //先还原默认配置
                /**//*修正curCfg中的无效值(null/undefined)改为默认值*/
                for (var i in curCfg) curCfg[i] = curCfg[i] != null ? curCfg[i] : ymPrompt.cfg[i];
                init();
            },
            doHandler: function (sign, autoClose, closeFirst) {
                if (autoClose == undefined ? curCfg.autoClose : autoClose) destroy();
                try { (curCfg.handler)(sign) } catch (e) { };
            },
            resizeWin: setWinSize,
            /**//*设定默认配置*/
            setDefaultCfg: function (cfg) {
                return ymPrompt.cfg = ymPrompt.apply({}, cfg, ymPrompt.apply({}, ymPrompt.cfg, dftCfg));
            },
            getButtons: function () {
                var btns = btnCache || [], btn, rBtn = [];
                while (btn = btns.shift()) rBtn.push($(btn.id));
                return rBtn;
            }
        });
        ymPrompt.setDefaultCfg(); //初始化默认配置
        /**//*执行用户初始化时的调用*/
        for (var i in _initFn) ymPrompt[i].apply(null, _initFn[i]);
        /**//*取消事件绑定*/
        addEvent('unload', function () {
            while (ymPrompt.eventList.length) detachEvent.apply(null, ymPrompt.eventList.shift());
        }, window);
    })();
})();                        //各消息框的相同操作
ymPrompt.apply(ymPrompt, {
    alert: function () {
        ymPrompt.show(arguments, {
            icoCls: 'ymPrompt_alert',
            btn: ['OK']
        });
        var Dsobj = document.getElementById("ym-window");
        var Titleobj = $(".ym-tr");
        DragAndDropNoFrame.Register(Dsobj, Titleobj);
    },
    succeedInfo: function () {
        ymPrompt.show(arguments, {
            icoCls: 'ymPrompt_succeed',
            btn: ['OK']
        });
        var Dsobj = document.getElementById("ym-window");
        var Titleobj = $(".ym-tr");
        DragAndDropNoFrame.Register(Dsobj, Titleobj);
    },
    errorInfo: function () {
        ymPrompt.show(arguments, {
            icoCls: 'ymPrompt_error',
            btn: ['OK']
        });
        var Dsobj = document.getElementById("ym-window");
        var Titleobj = $(".ym-tr");
        DragAndDropNoFrame.Register(Dsobj, Titleobj);
    },
    confirmInfo: function () {
        ymPrompt.show(arguments, {
            icoCls: 'ymPrompt_confirm',
            btn: ['OK', 'CANCEL']
        });
        var Dsobj = document.getElementById("ym-window");
        var Titleobj = $(".ym-tr");
        DragAndDropNoFrame.Register(Dsobj, Titleobj);
    },
    win: function () {
        ymPrompt.show(arguments);
        var isIE = ! +'\v1';    //IE浏览器       
        var IE6 = isIE && /MSIE (\d)\./.test(navigator.userAgent) && parseInt(RegExp.$1) < 7; //IE6需要用iframe来遮罩
        if (IE6)
            DD_belatedPNG.fix('.ym-tl,.ym-tr,.ym-tc,.ym-ml,.ym-bl,.ym-br,.ym-bc,.ym-mr');
        //增加移动窗体函数
        var Dsobj = document.getElementById("ym-window");
        var Titleobj = $(".ym-tr");
        DragAndDropNoFrame.Register(Dsobj, Titleobj);
    }
});

