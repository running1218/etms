<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Default.master" AutoEventWireup="true" CodeBehind="UserInfo.aspx.cs" Inherits="ETMS.Studying.Self.UserInfo" %>

<%@ Register Src="~/Controls/MiniUpFile.ascx" TagName="uploader" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="DefaultPlaceHolder" runat="server">
    <script src="<%=ETMS.Utility.WebUtility.AppPath%>/Scripts/UserInfo.js"></script>
    <link href="<%=ETMS.Utility.WebUtility.AppPath%>/Styles/default.css" type="text/css" rel="stylesheet" />
    <!--个人中心-->
    <div class="view-area">
        <div class="userInfo-container">
            <div class="userInfo-left">
                <dl>
                    <dt>
                        <img id="img_icon" /></dt>
                    <dd><span id="spRealName" runat="server"></span></dd>
                </dl>
                <ul>
                    <li class="cur"><em class="infoIcon"></em>基本信息</li>
                    <li><em class="logoIcon"></em>个人头像</li>
                    <li><em class="passwordIcon"></em>修改密码</li>
                </ul>
            </div>
            <div class="userInfo-right">
                <!--基本信息-->
                <div class="block information" style="display: block;">
                    <h1 class="tit">基本信息</h1>
                    <ul>
                        <li style="width: 100%">
                            <span>姓名：</span><em class="name" id="emRealName" runat="server"></em></li>
                        <li><span>账号：</span><em id="emUserName" runat="server"></em></li>
                        <li><span>性别：</span><em id="emUserSex" runat="server"></em></li>
                        <li><span>学员类型：</span><em id="emUserType" runat="server"></em></li>
                        <li><span>组织机构：</span><em id="emUserOrg" runat="server"></em></li>
                        <li><span>邮箱：</span>
                            <em>
                                <asp:TextBox runat="server" ID="txtEmail"></asp:TextBox>
                                <i class="emailError" style="color: #F38383"></i>
                            </em>
                        </li>
                        <li><span>手机号：</span>
                            <em>
                                <asp:TextBox runat="server" ID="txtMobile"></asp:TextBox>
                                <i class="mpError" style="color: #F38383"></i>
                            </em>
                        </li>
                        <li><span></span><em class="userInfo-submit information-submit">提交</em></li>
                    </ul>


                </div>

                <!--个人头像-->
                <div class="block logo">
                    <h1 class="tit">个人头像</h1>
                    <dl class="userLogo">
                        <dt>
                            <p class="uploadLogo">
                                <img id="img_user" />
                            </p>
                        </dt>
                        <dd>
                            <uc:uploader ID="uploader" runat="server" FunctionType="UserIcon" CallBack="doCallBack" />
                            <p class="userInfo-submit logo-submit">提交</p>
                        </dd>
                    </dl>
                    <input  type="hidden" id="ImgBizUrl"/>
                      <input  type="hidden" id="ImgFullUrl"/>
                </div>

                <!--修改密码-->
                <div class="block password">
                    <h1 class="tit">修改密码</h1>
                    <ul>
                        <li>
                            <span>原密码：</span>
                            <em>
                                <input type="password" placeholder="请输入原密码" class="oldPassword" />
                            </em>
                            <i class="error"></i>
                        </li>
                        <li>
                            <span>新密码：</span>
                            <em>
                                <input type="password" placeholder="新密码只能输入英文、数字、符号" class="newPassword" />
                            </em>
                            <i class="error"></i>
                        </li>
                        <li>
                            <span>确认密码：</span>
                            <em>
                                <input type="password" placeholder="请再次输入" class="againPassword" />
                            </em>
                            <i class="error"></i>
                        </li>
                        <li><span></span><em class="userInfo-submit password-submit">提交</em></li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
    <script>
        $(function () {
            var imgurl = "<%=ImgUrl%>";
            if (imgurl == "" || imgurl == undefined || imgurl.indexOf("App_Themes") > 0) {
                document.getElementById('img_icon').src = "../App_Themes/ThemeStudying/images/default_user.png";
                document.getElementById('img_user').src = "../App_Themes/ThemeStudying/images/default_user.png";
            }
            else {
                document.getElementById('img_icon').src = imgurl;
                document.getElementById('img_user').src = imgurl;
            }
        });
        /*修改个人资料*/
        $(".information-submit").on("click", function () {
            var email = $("#<%=txtEmail.ClientID%>").val();
            var mobilePhone = $("#<%=txtMobile.ClientID%>").val();
            var isRig = true;
            if (!checkEmail(email)) {
                $(".emailError").show().text("请填写正确的邮箱");
                isRig = false;
            }
            else {
                $(".emailError").hide();
            }
            if (!checkMobile(mobilePhone)) {
                $(".mpError").show().text("请填写正确的手机号码");
                isRig = false;
            }
            else {
                $(".mpError").hide()
            }
            if (!isRig) {
                return;
            }
            else {
                $.ajax({
                    url: AppPath + "/Service/UserInfoHandler.ashx",
                    type: 'POST',
                    dataType: "json",
                    data: { Method: "updateinfo", Email: email, MobilePhone: mobilePhone },
                    success: function (result) {
                        if (result.Status == true) {
                            popSuccessBox("保存成功");
                        }
                        else {
                            alert(result.Message);
                        }
                    },
                    error: function (err) {
                        popFailedBox('保存失败，请与管理员联系！');
                    }
                });
            }
        });
        function doCallBack(imgName, imgFullUrl,imgSize,imgOldName,imgNames) {
            document.getElementById('img_user').src = imgFullUrl;
            document.getElementById('img_icon').src = imgFullUrl;
            $("#ImgBizUrl").val(imgNames); 
            $("#ImgFullUrl").val(imgFullUrl);
            
        }

    </script>
</asp:Content>
