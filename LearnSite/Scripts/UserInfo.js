$(function () {
    $('#percent_b').hide();//进度隐藏
    var option_index = GetQueryString('index');
    if (option_index == 3) {
        $(".userInfo-left ul li").eq(3).addClass("cur").siblings("li").removeClass("cur");
        $(".userInfo-right .block").eq(3).show().siblings(".block").hide();
    }
    /*切换菜单*/
    $(".userInfo-left ul li").on("click", function () {
        var index = $(this).index();
        $(this).addClass("cur").siblings("li").removeClass("cur");
        $(".userInfo-right .block").eq(index).show().siblings(".block").hide();
    })
    /*修改信息*/
    $(".change").on("click", function () {
        $(this).siblings("input").removeAttr("disabled").focus();
    })

    /*修改密码 -- 提交*/
    $(".password-submit").on("click", function () {
        var regex = /^[\x00-\xff]+$/;// 匹配英文、数字、半角符号

        if ($(".oldPassword").val() == "") {
            $(".oldPassword").parent().siblings(".error").show().text("请输入原密码");
            return;
        } else {
            $(".oldPassword").parent().siblings(".error").hide();
        }
        if ($(".newPassword").val() == "") {
            $(".newPassword").parent().siblings(".error").show().text("请输入新密码");
            return;
        } else if (!($(".newPassword").val()).match(regex)) {
            $(".newPassword").parent().siblings(".error").show().text("新密码只能输入英文、数字、半角符号");
        } else {
            $(".newPassword").parent().siblings(".error").hide();
        }
        if ($(".againPassword").val() == "") {
            $(".againPassword").parent().siblings(".error").show().text("请输入新密码");
            return;
        } else if ($(".againPassword").val() != $(".newPassword").val()) {
            $(".againPassword").parent().siblings(".error").show().text("两次输入的新密码不一致");
            return;
        } else {
            $(".againPassword").parent().siblings(".error").hide();
        }
        $.ajax({
            url: AppPath + "/Service/UserInfoHandler.ashx",
            type: 'POST',
            dataType: "json",
            data: { Method: "updatepwd", OldPwd: $(".oldPassword").val(), NewPwd: $(".newPassword").val() },
            success: function (result) {
                if (result.Status == true) {
                    popSuccessBox("修改成功");
                }
                else {
                    $(".oldPassword").parent().siblings(".error").show().text(result.Message);
                }
            },
            error: function (err) {
                popFailedBox('修改失败，请与管理员联系！');
            }
        });
    })
    $(".logo-submit").on("click", function () {
        console.log(123)
        var imgUrl = $("#ImgBizUrl").val();
        var imgfull = $("#ImgFullUrl").val();
        if (imgUrl != "" && imgUrl != undefined) {
            $.ajax({
                url: AppPath + "/Service/UserInfoHandler.ashx",
                type: 'POST',
                dataType: "json",
                data: { Method: "updateicon", ImgUrl: imgUrl },
                success: function (result) {
                    if (result.Status == true) {
                        document.getElementById('img_icon').src = imgfull;
                        popSuccessBox("提交成功");
                        //window.history.go(0);
                      
                    }
                    else {
                        popFailedBox('提交失败，请与管理员联系！');
                    }
                },
                error: function (err) {
                    popFailedBox('提交失败，请与管理员联系！');
                }
            });
        }
    })
})
