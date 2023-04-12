<%@ Page Language="C#" AutoEventWireup="true" Inherits="LoginToStudying" Codebehind="LoginToStudying.aspx.cs" %>

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
            line-height:48px;
            height:48px;
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
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
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
                <li class="login-btn"><asp:Button ID="btnLogin" runat="server" Text="登录" OnClick="btnLogin_Click" ValidationGroup="login" /></li>
                <li style="padding: 8px 0;color: #666; display:none;">
                    <a href="../Self/ForgetPassword.aspx" style="float:left;">忘记密码</a>
                </li>
            </ul>    
        </div>
    </div>
    </form>
</body>
</html>
