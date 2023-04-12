<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login2.aspx.cs" Theme="" StylesheetTheme="" Inherits="ETMS.Studying.Login2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        ul,body,html{margin:0;list-style:none;}
        .login-popup {
            width:360px;
            height:300px;
            background:#fff;
            position:fixed;
            left:0px;
            top:0px;
        }
            .login-popup ul {
                margin-top:10px;
                padding:0 38px;
                clear:both;
            }
                .login-popup ul li {
                    float:left;
                    width:300px;
                }
                    .login-popup ul li input {
                        float:left;
                    }
                    .login-popup ul li span {
                        display:block;
                        float:right;
                        margin-top:35px;
                    }
                .login-popup ul li .text {
                    width:270px;
                    height:40px;
                    border:1px solid #a2a2a2;
                    margin-top:23px;
                    padding-left: 10px;
                }
            .login-btn {
                width:100%;
                height:42px;
                line-height:42px;
                text-align:center;
                background:#3685ef;
                font-size:15px;
                color:#fff;
                cursor:pointer;
            }
                .login-btn input {
                        background-color: #3685ef;
                        border: none;
                        width: 100%;
                        height: 100%;
                        font-size: 16px;
                        color: #fff;
                        font-weight: 700;
                        cursor:pointer;
                }
        .login-message {
            line-height:28px;
            height:36px;
            margin-top:5px !important;
            float:left !important;    
            color:#ff0000;
            font-size:10px;            
        }
        /*找回密码*/
        .forgetPassword-container {
            min-height:430px;
        }
        .forgetPassword-container ul {
            width:290px;
            margin:0 auto;
            padding-top: 90px;
        }
        .forgetPassword-container ul li {
            margin-bottom:20px;
        }
        .forgetPassword-container ul .tit {
            font-size:16px;
            line-height:3em;
        }
        .forgetPassword-container ul li p {
            font-size:12px;
            color:#999;
            line-height:1.5em;
        }
        .forgetPassword-container ul .userName {
            width:270px;
            height:30px;
            padding:2px 10px;
        }
        .forgetPassword-container .findPassword-submit{
            width:100%;
            height:42px;
            line-height:42px;
            text-align:center;
            background:#3685ef;
            color:#FFF;
            font-size:16px;
            cursor:pointer;
        }
        .login-register-link a {
            float: right !important;
            margin: 8px;
            text-decoration: none;
            font-size: 14px;
        }
    </style>
</head>

<body>
    <script lang="javascript" type="text/javascript" src="<%=ETMS.Utility.WebUtility.AppPath%>/Scripts/library/jquery-3.1.1.min.js"></script>
    <script lang="javascript" type="text/javascript" src="<%=ETMS.Utility.WebUtility.AppPath%>/Scripts/library/layer/layer.js"></script>
    <script>
        var AppPath = "<%=ETMS.Utility.WebUtility.AppPath%>";
    </script>

    <form id="form1" runat="server">
        <div class="login-popup">
            <ul>
                <li>
                    <asp:TextBox id="txtUserName" CssClass="text"  runat="server" placeholder="请输入登录名" />
                    <asp:RequiredFieldValidator ID="rfvUserName" ValidationGroup="login" ControlToValidate="txtUserName" runat="server" Text="*" ForeColor="Red"></asp:RequiredFieldValidator>
                </li>
                <li>
                    <asp:TextBox id="txtPassword" CssClass="text" runat="server" TextMode="Password" placeholder="请输入密码" />
                    <asp:RequiredFieldValidator ID="rfvPassword" ValidationGroup="login" ControlToValidate="txtPassword" runat="server" Text="*" ForeColor="Red"></asp:RequiredFieldValidator>
                </li>
                <li><asp:Label ID="lblMessage" runat="server" CssClass="login-message"></asp:Label></li>
                <li class="login-btn">
                    <%--<asp:Button ID="btnLogin" runat="server" Text="登录" OnClick="btnLogin_Click" ValidationGroup="login" />--%>
                    <input type="button" id="btnEnter" value="登录" onclick="loginClick()" />
                </li>
                <li class="login-register-link" style="padding: 8px 0;color: #666; display:none;">
                    <a href="javascript:void(0);" onclick="toRegister();">如无帐号，请先注册</a>
                </li>
            </ul>    
        </div>        
    </form>
    <script lang="javascript">
        GetQueryString = function (name) {
            //var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
            var reg = new RegExp('(^|&)' + name + '=([^&]*)(&|$)', 'i');
            var r = window.location.search.substr(1).match(reg);//.toLocaleLowerCase()
            if (r != null) return unescape(r[2]);
            return null;
        };
        var callbackJSstr = GetQueryString("callbackJS");
        var objId = GetQueryString("objId");
        var livingStatus = GetQueryString("LivingStatus");

        function loginClick()
        {
            var password = jnsimcode.encode($('#txtPassword').val())
            $.ajax({
                url: AppPath + "/Service/Auth.ashx",
                type: 'POST',
                dataType: "json",
                data: { UserName: $('#txtUserName').val(), Password: password },
                success: function (result) {
                    if (result.Status == true) {
                        if (callbackJSstr != null) {
                            //eval("parent." + callbackJSstr + "('" + result.Data.UserID + "','" + result.Data.RealName + "')");
                            //if (callbackJSstr == "LivingOpen") {
                            //    var realname = result.Data.RealName;
                            //    if (result.Data.RealName == '') { realname = result.Data.UserID };
                            //    enterLiving(objId, result.Data.UserID, realname, livingStatus);
                            //}
                            parent.layer.closeAll();
                        } else {
                            //parent.location.reload();
                            //parent.layer.closeAll(); 
                            parent.location.href = AppPath + "/Study/MyTrain.aspx";
                        }
                            
                    }
                    else {
                        $('#lblMessage').html(result.Message);
                    }
                },
                error: function (err) {                        
                    $('#lblMessage').html('登录失败，请与管理员联系！');
                }
            });
        }
        function enterLiving(livingID, userID, nikeName, livingStatus) {
            if (livingStatus == 3) {
                tohistory(livingID, userID, nikeName);
            }
            else{
                validLiving(livingID, userID, nikeName);
            }
        }
        //获取直播信息，并进入直播页面
        function validLiving(livingID, userID, nikeName) {
            $.ajax({
                url: AppPath + "/PublicService/LivingHandler.ashx",
                type: "get",
                data: { Method: "getlivinginfo", LivingID: livingID, UserID: userID, NikeName: nikeName },
                contentType: "application/json",
                dataType: "json",
                async: false,
                success: function (result) {
                    if (result.code == 0) {
                        var url = result.data.liveUrl;
                        window.open(url, '_blank');
                    }
                }
            });
        }

        function tohistory(livingID, userID, nikeName) {
            $.ajax({
                url: AppPath + "/PublicService/LivingHandler.ashx",
                type: "get",
                data: { Method: "getplaybackinfo", LivingID: livingID, UserID: userID, NikeName: nikeName },
                contentType: "application/json",
                dataType: "json",
                async: false,
                success: function (result) {
                    if (result.code == 0) {
                        var url = result.data.playbackUrl;
                        window.open(url, '_blank');
                    }
                    else {
                        layer.alert(result.msg);
                    }
                }
            });
        }
        function toRegister()
        {
            parent.location.href = AppPath + "/Public/Register.aspx";
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
</body>
</html>
