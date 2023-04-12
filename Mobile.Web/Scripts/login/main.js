/// <reference path="F:\A-One（ETMS）\V1.0\Mobile.Web\Views/MyTrain/Index.cshtml" />
 
$(function () {
    //登录
    var userid = common.GetQueryString('uid');
    $('#loginBtn').click(function () {
        var email_val = $('.email-ipt').val();
        var password_val = $('.password-ipt').val();
        if (_.isEmpty(email_val)) {
            layer.msg('用户名不能为空！');
            return false;
        }  else if (_.isEmpty(password_val)) {
            layer.msg('密码不能为空！');
            return false;
        } else {
            var params = { "UserName": email_val, "Password": password_val };
            common.call(AppPath+"/Login/PostUserLogin", params, 'post', function (data) {
                if (data.Status) {
                    userid = data.Data.UserID;
                    //把用户信息存入Cookie
                  //  console.log(data)
                    Cookies.set('cookie_userid', userid);
                    Cookies.set('cookie_username', data.Data.RealName);
                    Cookies.set('cookie_userPhotoUrl', data.Data.PhotoUrl);
                    if (common.GetQueryString("BackUrl") != null & common.GetQueryString("BackUrl") != undefined && common.GetQueryString("BackUrl") != '')
                        location.href = common.GetQueryString("BackUrl");
                    else
                        location.href =  AppPath+ "/MyTrain/Index";

                } else {
                    layer.msg(data.Message)
                };
            }, error)

        }
       
    })
    $(document).keydown(function (event) {
        if (event.keyCode == 13) {
            $('#loginBtn').click();
        };
    });
})