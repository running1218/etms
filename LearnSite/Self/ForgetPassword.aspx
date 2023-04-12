<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Default.master" AutoEventWireup="true" CodeBehind="ForgetPassword.aspx.cs" Inherits="ETMS.Studying.Self._2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="DefaultPlaceHolder" runat="server">
    <link href="<%=ETMS.Utility.WebUtility.AppPath%>/Styles/default.css" type="text/css" rel="stylesheet" />
    <style>
        .header-container {box-shadow: 0px 1px 3px rgba(0,0,0,0.5);}
    </style>
    <!--忘记密码-->
    <div class="view-area">
        <div class="forgetPassword-container">
            <ul>
                <li class="tit">忘记密码</li>
                <li>
                    <input type="text" placeholder="请输入用户名" class="userName" />
                </li>
                <li>
                    <p>提示：</p>
                    <p>1.  输入用户名，点击提交按钮发送密码重置邮件到邮箱</p>
                    <p>2.  登录邮箱，点击链接重置密码</p>
                </li>
                <li class="findPassword-submit">提交</li>
            </ul>
        </div>
    </div>
</asp:Content>
