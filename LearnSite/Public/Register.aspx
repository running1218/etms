<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Default.master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="ETMS.Studying.Public.Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="DefaultPlaceHolder" runat="server">
    <link href="<%=ETMS.Utility.WebUtility.AppPath%>/Styles/default.css" type="text/css" rel="stylesheet" />
    <div class="view-area">
        <div class="userInfo-container">
            <div class="userInfo-right" style="width:1200px !important;">
                <div class="password">
                    <h1 class="tit">用户注册</h1>
                    <ul>
                        <li>
                            <span>用户名：</span>
                            <em>
                                <input type="text" placeholder="请输入用户名" id="userName" />
                            </em>
                            <i class="error" id="msg_userName"></i>
                        </li>                        
                        <li>
                            <span>密码：</span>
                            <em>
                                <input type="password" placeholder="新密码只能输入英文、数字、符号" id="password" />
                            </em>
                            <i class="error" id="msg_password"></i>
                        </li>
                        <li>
                            <span>确认密码：</span>
                            <em>
                                <input type="password" placeholder="请再次输入" id="againPassword" />
                            </em>
                            <i class="error" id="msg_rePassword"></i>
                        </li>
                        <li>
                            <span>姓名：</span>
                            <em>
                                <input type="text" placeholder="请输入姓名" id="realName" />
                            </em>
                            <i class="error" id="msg_realName"></i>
                        </li>
                        <%--<li>
                            <span>邮箱：</span>
                            <em>
                                <input type="text" placeholder="请输入邮箱" id="email" />
                            </em>
                            <i class="error"></i>
                        </li>--%>
                        <li>
                            <span>手机号：</span>
                            <em>
                                <input type="text" placeholder="请输入手机号" id="phone" style="width:140px;" />
                                <input type="button" id="getValidCode" class="phone-valid" value="获取验证码" />
                            </em>
                            <i class="error" id="msg_phone"></i>
                        </li>
                        <li>
                            <span>验证码：</span>
                            <em>
                                <input type="text" placeholder="请输入手机验证码"  id="phoneCode" />
                                <input type="hidden" id="generateCode" />
                            </em>
                            <i class="error" id="msg_phoneCode"></i>
                        </li>
                        <li><span></span><em class="userInfo-submit password-submit" onclick="toRegister(this)">提交</em></li>
                        <li>
                            <i class="error" id="messagetip" style="padding:30px 0px 0px 260px"></i>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
    </div>

    <script lang="javascript">
        $('#getValidCode').click(function () {
            $('#generateCode').val('');
            var isRig = true;
            var phone = $('#phone').val();
            if (!checkMobile(phone)) {
                $("#msg_phone").show().text("请填写正确的手机号码");
                isRig = false;
            }
            else {
                $("#msg_phone").hide()
            }

            if (!isRig) {
                return;
            }
            else {
                settime(this);
                $.ajax({
                    url: AppPath + "/PublicService/Register.ashx",
                    type: 'POST',
                    dataType: "json",
                    data: { Method: "sendphonecdoe", Phone: phone },
                    success: function (result) {
                        if (result.success == true) {
                            $('#generateCode').val(result.validCode);
                        }
                        else {
                            //alert(result.Message);
                        }
                    },
                    error: function (err) {
                        //popFailedBox('保存失败，请与管理员联系！');
                    }
                });
            }
        });

        function toRegister(obj)
        {
            if (!validinput()) return;

            $.ajax({
                url: AppPath + "/PublicService/Register.ashx",
                type: 'POST',
                dataType: "json",
                data: { Method: "registerstudent", Phone: $('#phone').val(), UserName: $('#userName').val().trim(), Password: $('#password').val().trim(), RealName: $('#realName').val().trim() },
                success: function (result) {
                    if (result.Status != true) {
                        $('#messagetip').show().text(result.Message)
                    }
                    else {
                        window.location.href = '<%=ETMS.Utility.WebUtility.AppPath%>' + '/Index.aspx';
                    }
                },
                error: function (err) {
                    //popFailedBox('保存失败，请与管理员联系！');
                }
            });
        }

        function validinput() {
            var flag = true;
            var phone = $('#phone').val();
            if (!checkMobile(phone)) {
                $("#msg_phone").show().text("请填写正确的手机号码");
                flag = false;
            }
            else {
                $("#msg_phone").hide()
            }

            if ($('#userName').val().trim() == '') {
                $('#msg_userName').show().text('请输入用户名');
                flag = false;
            }
            else {
                $('#msg_userName').hide().text('');
            }

            if ($('#password').val().trim() == '') {
                $('#msg_password').show().text('请输入密码');
                flag = false;
            }
            else {
                $('#msg_password').hide().text('');
            }

            if ($('#againPassword').val().trim() == '') {
                $('#msg_rePassword').show().text('请输入确认密码');
                flag = false;
            }
            else {
                $('#msg_rePassword').hide().text('');
            }

            if ($('#againPassword').val().trim() != $('#password').val().trim()) {
                $('#msg_rePassword').show().text('密码与确认密码不符，请重新输入');
                flag = false;
            }
            else {
                $('#msg_rePassword').hide().text('');
            }

            if ($('#realName').val().trim() == '') {
                $('#msg_realName').show().text('请输入姓名');
                flag = false;
            }
            else {
                $('#msg_realName').hide().text('');
            }

            if ($('#phoneCode').val().trim() == '') {
                $('#msg_phoneCode').show().text('请输入手机验证码');
                flag = false;
            }
            else {
                $('#msg_phoneCode').hide().text('');
            }

            if ($('#generateCode').val() == '' || $('#phoneCode').val().trim() != $('#generateCode').val()) {
                $('#msg_phoneCode').show().text('验证码错误');
                flag = false;
            }
            else {
                $('#msg_phoneCode').hide().text('');
            }

            return flag;
        }

        var timelen = 90;
        var countdown = timelen;
        function settime(obj) {
            if (countdown == 0) {
                obj.removeAttribute("disabled");
                $(obj).removeClass('phone-valid-action-disabled');
                obj.value = "获取验证码";
                countdown = timelen;
                $('#generateCode').val('');
                return;
            } else {
                obj.setAttribute("disabled", true);
                $(obj).addClass('phone-valid-action-disabled');
                obj.value = "重新获取(" + countdown + ")";
                countdown--;
            }
            setTimeout(function () { settime(obj) }, 1000);
        }
    </script>
</asp:Content>


