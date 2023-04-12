<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ETMS.Studying.Login" %>
<!DOCTYPE html>
<html lang="zh-CN">
<head id="Head1" runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <meta name="viewport" content="width=device-width">
    <meta name="robots" content="all" />
    <title><%=ETMS.Utility.WebUtility.PlatTitle %></title>
    <link href="Styles/passport/public.css" rel="stylesheet" type="text/css" />
    <link href="Styles/passport/default.css" rel="stylesheet" type="text/css" />
    <link href="Styles/passport/ymPrompt.css" rel="stylesheet" type="text/css" />
</head>
<body class="body-bg-login">
     <img class="login-bg1"src="Styles/passport/Images/bg1_03.png"/>
     <img class="login-bg2"src="Styles/passport/Images/yun2_03.png"/>
    <script type="text/javascript" src="<%=ETMS.Utility.WebUtility.AppPath%>/scripts/library/jquery-3.1.1.min.js"></script>
    <script type="text/javascript" src="<%=ETMS.Utility.WebUtility.AppPath%>/scripts/jsfunction.js"></script>
    <script src="<%=ETMS.Utility.WebUtility.AppPath%>/scripts/ymPromptYuan.js" type="text/javascript"></script>
    <form id="form1" runat="server">
		<div class="page" id="loginbg">
			<div class="wrap">
				<h1 class="logo">登录</h1>
				<div class="login">
					<ul>
						<li><i class="icon icon-user"></i><asp:TextBox ID="username" CssClass="input_username" MaxLength="50" runat="server" TabIndex="1" placeholder="请输入用户名"></asp:TextBox></li>
						<li><i class="icon icon-password"></i><asp:TextBox ID="password" TextMode="Password" CssClass="input_password" runat="server" TabIndex="2" placeholder="请输入用户密码"></asp:TextBox></li>
						<li class="li-end">
							<asp:LinkButton ID="lkLogin" runat="server" OnClientClick="if(!doLogin())return false;" CssClass="btn-login" OnClick="lkLogin_Click" TabIndex="4">登录</asp:LinkButton>
						</li>
                        <li class="login-tooltip"><asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label></li>
					</ul>
				</div>
			</div>
		</div>
		<script type="text/javascript">
		    function submitForm() {
		        var theForm = document.forms[0];
		        if (theForm.password.value != '') {
		            if (parent != null) {
		                theForm.password.value = jnsimcode.encode(theForm.password.value);
		            }
		        }
		    }

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
            submitForm();
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
        String.prototype.padLeft = function (len, char) {
            var s = this;
            for (var i = 0; i < len - this.length; i++) {
                s = char + s;
            }
            return s;
        }

        var jnsimcode = new (function () {
            this.encode = function (str) {
                if (str === "")
                    return "";
                var hexCharCode = [];
                for (var i = 0; i < str.length; i++) {
                    hexCharCode.push((str.charCodeAt(i)).toString(16).padLeft(4, '0'));
                }
                return hexCharCode.join("");
            }
            this.decode = function (rawStr) {
                var len = rawStr.length;
                if (len % 4 !== 0) {
                    //alert("Illegal Format ASCII Code!");
                    return "";
                }
                var curCharCode = 0;
                var resultStr = [];
                for (var i = 0; i < len; i += 4) {
                    curCharCode = parseInt(rawStr.substr(i, 4), 16);
                    resultStr.push(String.fromCharCode(curCharCode));
                }
                return resultStr.join("");
            }
        })();
    </script>
    </form>
</body>
</html>