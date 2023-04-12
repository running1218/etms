//接口请求失败
function error(XMLHttpRequest, textStatus, errorThrown) {
    sweetAlert("系统出错联系管理员!", "接口错误", "error");
   // layer.msg(errorThrown)
}
//  请求数据为空
function contentNull(parentObj){ 
    parentObj.html('<img class="NULL_BOX" src="' + AppPath + '/Images/no_content.png">');
}
//公共对象
var common = common || {};
/**
 * 地址栏根据参数名获取值
 * @param {[type]} name [description]
 */
common.GetQueryString = function (name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]);
    return null;
}
//清除登录缓存
common.clearLoginCookie = function() {
   // Cookies.remove('userName');
    Cookies.set('cookie_username', '');
    Cookies.set('cookie_userPhotoUrl', '');
    Cookies.set('cookie_userid', '');
};

//不刷页面修改url参数
common.changeURLPar = function(destiny, par, par_value) {
    var pattern = par + '=([^&]*)';
    var replaceText = par + '=' + par_value;
    if (destiny.match(pattern)) {
        var tmp = '/\\' + par + '=[^&]*/';
        tmp = destiny.replace(eval(tmp), replaceText);
        return (tmp);
    } else {
        if (destiny.match('[\?]')) {
            return destiny + '&' + replaceText;
        } else {
            return destiny + '?' + replaceText;
        }
    }
    return destiny + '\n' + par + '\n' + par_value;
}

//封装跳转
common.href = function (url, param, studentCodeIsHave) {
    if (!_.isEmpty(param)) {
        _.each(param, function (val, key) {
            if (url.indexOf('?') >= 0) {
                if (val.length == 1) {
                    if (val[0] == 'partnerId') {
                        url = url + "&partnerId=" + JSON.parse(Cookies.get('userInfo')).StudentCode;
                    } else {
                        url = url + "&" + val[0] + "=" + common.GetParameter(val[0]);
                    }
                } else {
                    url = url + "&" + val[0] + "=" + val[1];
                }
                //if (val == 'sysNum') {
                //    url = url + "&sysNum=8172422beeb84b25afba9b333e0328e2";
                //} else if (val == 'partnerId') {
                //    url = url + "&partnerId=" + JSON.parse(Cookies.get('userInfo')).StudentCode;
                //} else if (type == 'number') {
                //    url = url + "&iMessageID=" + val;
                //}  else {
                //    url = url + "&" + val + "=" + common.GetParameter(val);
                //}
            } else {
                if (val.length == 1) {
                    if (val[0] == 'partnerId') {
                        url = url + "?partnerId=" + JSON.parse(Cookies.get('userInfo')).StudentCode;
                    }
                    url = url + "?" + val[0] + "=" + common.GetParameter(val[0]);
                } else {
                    url = url + "?" + val[0] + "=" + val[1];
                }
                //if (val == 'sysNum') {
                //    url = url + "?sysNum=8172422beeb84b25afba9b333e0328e2";
                //} else if (val == 'partnerId') {
                //    url = url + "?partnerId=" + JSON.parse(Cookies.get('userInfo')).StudentCode;
                //} else if (type == 'number') {
                //    url = url + "?" + val[0] + "=" + val[1];
                //} else {
                //    url = url + "?" + val + "=" + common.GetParameter(val);
                //}
            }
        })
    }
    if(url.indexOf('bust')<0){
        if (url.indexOf('?') >= 0)
            url = url + "&bust=" + (new Date()).getTime();
        else
            url = url + "?bust=" + (new Date()).getTime();
    }
    //var userinfo = Cookie.get('userInfo');
    if (studentCodeIsHave == 1) {
        if (url.indexOf('StudentCode') < 0) {
            if (url.indexOf('?') >= 0)
                url = url + "&StudentCode=" + common.GetParameter('StudentCode');
            else
                url = url + "?StudentCode=" + common.GetParameter('StudentCode');
        }
    }
    window.location.href = AppPath+ url;
};


//检测是否由PC端跳转 

if (common.GetQueryString("callway")=="qrcode") {
    if (!_.isEmpty(common.GetQueryString("uid"))) {
        Cookies.set('cookie_userid', common.GetQueryString("uid"));
    } else {
        common.clearLoginCookie();
    }
}
//检查cookie是否登录
common.checkLoginCookie = function() {
    if (_.isEmpty(Cookies.get('cookie_userid'))) {
        window.location.href = AppPath+ '/Login/index';
    } else {
        window.location.href = AppPath+ '/MyTrain/Index';
    }
};
/*解码*/
common.decode = function (name) {
    return decodeURIComponent(decodeURIComponent(escape(name)));
}
//url 随机数
common.fixWXhref = function() {
    // $('a').each(function(){
    //     var url = $(this).attr('href');
    //     if(url.indexOf('bust')<0){
    //         if (url.indexOf('?') >= 0)
    //             url = url + "&bust=" + (new Date()).getTime();
    //         else
    //             url = url + "?bust=" + (new Date()).getTime();
    //         $(this).attr('href',url);
    //     }
    // });
};

//ajax封装
common.call = function (url, data, type, successCallback, errorCallback, jsonData) {
    //默认参数
    var defaults = {
        type: 'get',
        dataType: 'json',
        // jsonp: 'callback',
        async: true,
        cache: false,
        // xhrFields: {
        //     withCredentials: true
        // },
        // crossDomain: true,
        title: 'defaultTitle',
        timeout: 20000,
        error: function () { },
        success: function () { }
    };
   
    //{传入参数
    var options = {
        url: url,
        type: type,
        data: data,
        //async: async,
        error: errorCallback,
        success: successCallback
    };
    //与传入参数合并
    var settings = $.extend({}, defaults, options);
    //判断是否是jsonp
    if (settings.dataType.toLowerCase() == "jsonp") {
        if (settings.url.indexOf('?') >= 0)
            settings.url = settings.url + "&callback=?";
        else
            settings.url = settings.url + "?callback=?";
    }
    if (settings.url.indexOf('?') >= 0)
        settings.url = settings.url + "&bust=" + (new Date()).getTime();
    else
        settings.url = settings.url + "?bust=" + (new Date()).getTime();

    //var request = $.ajax(options);
    if (!_.isEmpty(jsonData) && jsonData.Success!=undefined) {
        if (typeof settings.success === "function") {
            settings.success(jsonData);
        }
        return;
    }
    
    //开始执行ajax
    $.ajax({
        type: settings.type,
        dataType: settings.dataType,
        async: settings.async,
        // jsonp: settings.jsonp,
        cache: settings.cache,
        // xhrFields: settings.xhrFields,
        // crossDomain: settings.crossDomain,
        url: settings.url,
        data: settings.data,
        timeout: 60000,
        
        success: function (resp) { //成功
            if (typeof settings.success === "function") {
                settings.success(resp);
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) { //失败
            // bootbox.alert('error:'+textStatus+errorThrown);
            if (typeof settings.error === "function") settings.error(XMLHttpRequest, textStatus, errorThrown);
        }
    });
};


/**
 * 获取url需要传的参数
 */
common.GetParameter = function (name) {
    var userInfo = JSON.parse(Cookies.get('userInfo'));
    var QueryStringParameter = encodeURIComponent(common.GetQueryString(name));
    if (!_.isEmpty(QueryStringParameter) && QueryStringParameter != 'null') {
        return QueryStringParameter;
    } else {
        var parameter = "";
        $.each(userInfo, function (key, val) {
            if (name == key) {
                parameter = val;
            }
        });
        return parameter;
    }
}


common.formatDate = function(date, format) {
    if (!date) return;
    if (!format) format = "yyyy-MM-dd";

    switch (typeof date) {
        case "string":
            date = new Date(date.replace(/-/g, "/"));
            break;
        case "number":
            date = new Date(date);
            break;
    }

    if (!date instanceof Date) return;
    if (date == "Invalid Date") return;
    var dict = {
        "yyyy": date.getFullYear(),
        "M": date.getMonth() + 1,
        "d": date.getDate(),
        "H": date.getHours(),
        "m": date.getMinutes(),
        "s": date.getSeconds(),
        "MM": ("" + (date.getMonth() + 101)).substr(1),
        "dd": ("" + (date.getDate() + 100)).substr(1),
        "HH": ("" + (date.getHours() + 100)).substr(1),
        "mm": ("" + (date.getMinutes() + 100)).substr(1),
        "ss": ("" + (date.getSeconds() + 100)).substr(1)
    };
    return format.replace(/(yyyy|MM?|dd?|HH?|ss?|mm?)/g, function () {
        return dict[arguments[0]];
    });
}
Date.prototype.Format = function (fmt) { //author: meizz 
    var o = {
        "M+": this.getMonth() + 1, //月份 
        "d+": this.getDate(), //日 
        "h+": this.getHours(), //小时 
        "m+": this.getMinutes(), //分 
        "s+": this.getSeconds(), //秒 
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度 
        "S": this.getMilliseconds() //毫秒 
    };
    if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
}

common.date_compare = function (type, module, courseID) {
    switch(type){
        case "0":
            sweetAlert("课程还没有开始哦!", "", "");
            break;
        case "1":
            sweetAlert("课程已结束!", "", "");
            break;
        default:
            if (module == 1)
                window.location.href = AppPath + "/MyTrain/Detail?TrainingItemCourseID=" + type + '&CourseID=' + courseID;
            else if (module == 2)
                window.location.href = AppPath + "/MyTrain/LivingStudy?TrainingItemCourseID=" + type + '&CourseID=' + courseID;
            else
                window.location.href = AppPath + "/MyTrain/Detail?TrainingItemCourseID=" + type;
    }
}
//console.log(Cookies.get('cookie_userid') + "=======---" + Cookies.get('cookie_username') + "=======" + Cookies.get('cookie_userPhotoUrl'))

$(function(){
    common.fixWXhref();
});
 
function GUID() {
    this.date = new Date();   /* 判断是否初始化过，如果初始化过以下代码，则以下代码将不再执行，实际中只执行一次 */
    if (typeof this.newGUID != 'function') {   /* 生成GUID码 */
        GUID.prototype.newGUID = function () {
            this.date = new Date(); var guidStr = '';
            sexadecimalDate = this.hexadecimal(this.getGUIDDate(), 16);
            sexadecimalTime = this.hexadecimal(this.getGUIDTime(), 16);
            for (var i = 0; i < 9; i++) {
                guidStr += Math.floor(Math.random() * 16).toString(16);
            }
            guidStr += sexadecimalDate;
            guidStr += sexadecimalTime;
            while (guidStr.length < 32) {
                guidStr += Math.floor(Math.random() * 16).toString(16);
            }
            return this.formatGUID(guidStr);
        }
        /* * 功能：获取当前日期的GUID格式，即8位数的日期：19700101 * 返回值：返回GUID日期格式的字条串 */
        GUID.prototype.getGUIDDate = function () {
            return this.date.getFullYear() + this.addZero(this.date.getMonth() + 1) + this.addZero(this.date.getDay());
        }
        /* * 功能：获取当前时间的GUID格式，即8位数的时间，包括毫秒，毫秒为2位数：12300933 * 返回值：返回GUID日期格式的字条串 */
        GUID.prototype.getGUIDTime = function () {
            return this.addZero(this.date.getHours()) + this.addZero(this.date.getMinutes()) + this.addZero(this.date.getSeconds()) + this.addZero(parseInt(this.date.getMilliseconds() / 10));
        }
        /* * 功能: 为一位数的正整数前面添加0，如果是可以转成非NaN数字的字符串也可以实现 * 参数: 参数表示准备再前面添加0的数字或可以转换成数字的字符串 * 返回值: 如果符合条件，返回添加0后的字条串类型，否则返回自身的字符串 */
        GUID.prototype.addZero = function (num) {
            if (Number(num).toString() != 'NaN' && num >= 0 && num < 10) {
                return '0' + Math.floor(num);
            } else {
                return num.toString();
            }
        }
        /*  * 功能：将y进制的数值，转换为x进制的数值 * 参数：第1个参数表示欲转换的数值；第2个参数表示欲转换的进制；第3个参数可选，表示当前的进制数，如不写则为10 * 返回值：返回转换后的字符串 */GUID.prototype.hexadecimal = function (num, x, y) {
            if (y != undefined) { return parseInt(num.toString(), y).toString(x); }
            else { return parseInt(num.toString()).toString(x); }
        }
        /* * 功能：格式化32位的字符串为GUID模式的字符串 * 参数：第1个参数表示32位的字符串 * 返回值：标准GUID格式的字符串 */
        GUID.prototype.formatGUID = function (guidStr) {
            var str1 = guidStr.slice(0, 8) + '-', str2 = guidStr.slice(8, 12) + '-', str3 = guidStr.slice(12, 16) + '-', str4 = guidStr.slice(16, 20) + '-', str5 = guidStr.slice(20);
            return str1 + str2 + str3 + str4 + str5;
        }
    }
}