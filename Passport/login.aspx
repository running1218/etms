<%@ Page Language="C#" AutoEventWireup="true" Inherits="login" ValidateRequest="false" Codebehind="login.aspx.cs" %>
<!DOCTYPE html>
<html lang="zh-CN">
<head id="Head1" runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <meta name="viewport" content="width=device-width">
    <meta name="robots" content="all" />
    <title><%=ETMS.Utility.WebUtility.PlatTitle %></title>
    <link href="App_Themes/ThemeDefault/public.css" rel="stylesheet" type="text/css" />
    <link href="App_Themes/ThemeDefault/default.css" rel="stylesheet" type="text/css" />
    <link href="App_Themes/ThemeDefault/ymPrompt.css" rel="stylesheet" type="text/css" />
</head>
<body class="body-bg-login">
     <img class="login-bg1"src="App_Themes/ThemeDefault/Images/bg1_03.png"/>
     <img class="login-bg2"src="App_Themes/ThemeDefault/Images/yun2_03.png"/>
    <script type="text/javascript" src="<%=ETMS.Utility.WebUtility.AppPath%>/JScript/jquery-1.11.1.min.js"></script>
    <script type="text/javascript" src="<%=ETMS.Utility.WebUtility.AppPath%>/JScript/jsfunction.js"></script>
    <script src="<%=ETMS.Utility.WebUtility.AppPath%>/JScript/ymPromptYuan.js" type="text/javascript"></script>
    <form id="form1" runat="server">
		<div class="page" id="loginbg">
			<div class="wrap">
				<h1 class="logo"><%=ETMS.Utility.WebUtility.PlatTitle %></h1>
				<div class="login">
					<ul>
						<li><i class="icon icon-user"></i><asp:TextBox ID="username" CssClass="input_username" MaxLength="50" runat="server" TabIndex="1" placeholder="请输入用户名"></asp:TextBox></li>
						<li><i class="icon icon-password"></i><asp:TextBox ID="password" TextMode="Password" CssClass="input_password" runat="server" TabIndex="2" placeholder="请输入用户密码"></asp:TextBox></li>
						<li style="display:none;" runat="server" id="liValidCode"><i class="icon icon-validcode"></i><input type="text" name="validcode" id="validcode" class="input_repassword" maxlength="6" tabindex="3" />
							<img id="imgValidate" class="imgValidate" src="ValidCode.ashx?action=image" />
							<a id="btnNext" class="nextValidNumber" href="javascript:ChangeValidate();" tabindex="5">换一张</a>
						</li>
						<li class="li-end">
							<asp:LinkButton ID="lkLogin" runat="server" OnClientClick="if(!doLogin())return false;" CssClass="btn-login" OnClick="lkLogin_Click" TabIndex="4">登录</asp:LinkButton>
						</li>
                        <li class="hide"><a href="javascript:showWindow('忘记密码','ForgotPassword.aspx','470',320)" class="forgotpass" tabindex="7">忘记密码？</a></li>
					</ul>
				</div>
			</div>
		</div>
		<script type="text/javascript">
		    $(function () {
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
		    })
		    function ChangeValidate() {
		        var root = '<%=ETMS.Utility.WebUtility.AppPath%>';
		        var imgObjec = document.getElementById("imgValidate");
		        imgObjec.src = root + "/ValidCode.ashx?action=image&date=" + new Date();
		    }
		</script>
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
            //            var validCode = $('#validcode').val();
            //                        if (validCode == "") {
            //                            popAlertMsg("请输入验证码！", "登录提示");
            //                            return false;
            //                        }
            return true;
        }

    </script>
    <script type="text/javascript">
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
        document.onkeydown = function (e) {
            if (!e) e = window.event; //火狐中是 window.event
            if ((e.keyCode || e.which) == 13) {
                document.getElementById("<%=lkLogin.ClientID %>").click();
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
        function doResize() {

        }
       
    </script>
    </form>
</body>
</html>
