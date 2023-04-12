<%@ Page Language="C#" AutoEventWireup="true" Inherits="Index" Codebehind="Index.aspx.cs" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><%=ETMS.Utility.WebUtility.PlatTitle %></title>
    <link href="App_Themes/ThemeDefault/media.css" rel="stylesheet" type="text/css" />
    <link href="App_Themes/ThemeDefault/public.css" rel="stylesheet" type="text/css" />
    <link href="App_Themes/ThemeDefault/default.css" rel="stylesheet" type="text/css" />
    <link href="App_Themes/ThemeDefault/ymPrompt.css" rel="stylesheet" type="text/css" />
</head>
<body class="login-body">
    <script type="text/javascript" src="<%=ETMS.Utility.WebUtility.AppPath%>/JScript/jquery-1.11.1.min.js"></script>
    <script type="text/javascript" src="<%=ETMS.Utility.WebUtility.AppPath%>/JScript/jsfunction.js"></script>
    <script src="<%=ETMS.Utility.WebUtility.AppPath%>/JScript/ymPromptYuan.js" type="text/javascript"></script>
    <script src="<%=ETMS.Utility.WebUtility.AppPath %>/JScript/jquery.tmpl.js" type="text/javascript"></script>
    <script src="<%=ETMS.Utility.WebUtility.AppPath %>/JScript/layer.min.js" type="text/javascript"></script>
    <script src="<%=ETMS.Utility.WebUtility.AppPath %>/JScript/index.js" type="text/javascript"></script>
    <script src="<%=ETMS.Utility.WebUtility.AppPath %>/JScript/notice.scroll.js" type="text/javascript"></script>
    <script lang="javascript">
        document.onkeydown = function (e) {
            if (!e) e = window.event; //火狐中是 window.event
            if ((e.keyCode || e.which) == 13) {
                document.getElementById("<%=lkLogin.ClientID %>").click();
            }
        }
    </script>
    <form id="form1" runat="server">
        <div class="login-containner">
            <div class="login-header">
                <div class="login-logo">
                    <p class="logo-chs">职业培训平台</p>                    
                </div>
            </div>
            <div class="login-middle">
                <div class="login-box">
                    <div class="login-block">
                        <div class="login">
					        <ul>
                                <li style="color:#fff; font-size:16px; height:24px !important;font-weight:700;">登录平台</li>
						        <li><i class="icon icon-user"></i><asp:TextBox ID="username" CssClass="input_username" MaxLength="50" runat="server" TabIndex="1"></asp:TextBox></li>
						        <li><i class="icon icon-password"></i><asp:TextBox ID="password" TextMode="Password" CssClass="input_password" runat="server" TabIndex="2"></asp:TextBox></li>
						        <li><i class="icon icon-validcode"></i><input type="text" name="validcode" id="validcode" class="input_repassword" maxlength="6" tabindex="3" />
							        <img id="imgValidate" class="imgValidate" src="ValidCode.ashx?action=image" />
							        <a id="btnNext" class="nextValidNumber" href="javascript:ChangeValidate();" tabindex="5">换一张</a>
						        </li>
						        <li class="li-end">
							        <asp:LinkButton ID="lkLogin" runat="server" OnClientClick="if(!doLogin())return false;" CssClass="btn-login" OnClick="lkLogin_Click" TabIndex="4"><span class="login-text">登录</span></asp:LinkButton>
						        </li>
                                <li><a href="javascript:showWindow('忘记密码','ForgotPassword.aspx','470',320)" class="forget-password" tabindex="7">忘记密码？</a></li>
					        </ul>
				        </div>
                    </div>                    
                </div>
            </div>
            <div class="login-notice">
                <div class="login-notice-box"></div>
                <div class="notice-more">
                    <a>查看更多</a>
                </div>
            </div>
            <div class="login-footer"></div>
        </div>
        <script id="notice" type="text/x-jquery-tmpl">
            {{each DataList }} 
            <div class="notice-instance" id="${ArticleID}">
                <h5 class="notice-title">
                    <a>${MainHead}</a>
                </h5>
                <div class="notice-content">
                    ${Brief}
                </div>
                <div class="notice-time">${BeginDate}</div>
            </div>
            {{/each}}
        </script>
    </form>
</body>
</html>
