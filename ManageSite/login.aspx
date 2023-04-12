<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><%= ETMS.Utility.WebUtility.PlatTitle %></title>
    <link href="App_Themes/ThemeAdmin/public.css" rel="stylesheet" type="text/css" />
    <link href="App_Themes/ThemeAdmin/default.css" rel="stylesheet" type="text/css" />
    <style>
        body
        {
            background: #f6f6f6;
        }
    </style>
</head>
<body>
    <script language="javascript" type="text/javascript" src="<%=ETMS.Utility.WebUtility.AppPath%>/JScript/jquery-1.11.1.min.js"></script>
    <script language="javascript" type="text/javascript" src="<%=ETMS.Utility.WebUtility.AppPath%>/JScript/jsfunction.js"></script>
    <script src="<%=ETMS.Utility.WebUtility.AppPath%>/JScript/ymPromptYuan.js" type="text/javascript"></script>
    <script>
        $(function () {

            $(".login_bj").css("margin-top", ((document.body.clientHeight - $(".login_bj").height()) / 2 + "px"));
        })
        function ChangeValidate() {
            var imgObjec = document.getElementById("imgValidate");
            imgObjec.src = "ValidCode.ashx?action=image&date=" + new Date();
        }
    </script>
    <form id="form1" runat="server">
    <div class="login_bj">
        <input type="text" name="username" id="username" class="input_username" maxlength="25" value="sysadmin" />
        <input type="password" name="password" id="password" class="input_password" maxlength="25" value="123456" />
        <input type="text" name="validcode" id="validcode" class="input_repassword" maxlength="5" />
        <img id="imgValidate" class="imgValidate" src="ValidCode.ashx?action=image" /><a
            id="btnNext" class="nextValidNumber" href="javascript:ChangeValidate();">换一张</a>
        <a class="btn_login" href="javascript:doLogin()"><span class="hide">登录</span></a>
    </div>
    <script language="javascript">
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
            var validCode = $('#validcode').val();
            if (validCode == "") {
                popAlertMsg("请输入验证码！", "登录提示");
                return false;
            }
            //验证验证码
            //要求：必须使用同步方式来获取
            $.ajax({
                url: 'ValidCode.ashx?action=valid&validcode=' + validCode,
                type: "get",
                dataType: "json",
                async: false,
                success: function (obj) {
                    if (obj.IsSuccess) {
                        //完成登录                                               
                        var parms = location.href.substr(location.href.indexOf('?', 0)+1, 10000);
                        var loginUrl = '<%=ETMS.Security.PassportClientSettings.GetConfig().AjaxSignInUrl.ToString() %>' + parms + '&lu=' + uid + '&lp=' + pwd;                       
                        $.getScript(loginUrl, callback);
                    }
                    else {
                        popAlertMsg("验证码错误！", "登录提示", ChangeValidate); //刷新验证码
                    }
                }
            });

        }
        function callback(obj) {
            if (loginResult.IsValid) {
                location.href = loginResult.parm;
            }
            else {
                popAlertMsg(loginResult.parm, "登录提示");
            }
        }  
    </script>
    </form>
</body>
</html>
