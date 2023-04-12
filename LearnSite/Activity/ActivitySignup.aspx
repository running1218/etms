<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ActivitySignup.aspx.cs" Inherits="ETMS.Studying.Activity.ActivitySignup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
    <style>
        input {
            background-color: #EEEEEE;
            border: 0px solid #a2a2a2;
            height: 30px;
            line-height: 30px;
            width: 260px;
            padding-left: 10px;
        }

        table {
            margin: 0 auto;
            width: 500px;
            padding-top: 10px;
        }

            table td {
                height: 40px;
                line-height: 40px;
                font-size: 16px;
            }

                table td span {
                    color: red;
                    padding-right: 5px;
                }

                table td select {
                    width: 270px;
                    padding-left: 10px;
                    background-color: #EEEEEE;
                    border: 0px solid #a2a2a2;
                    height: 30px;
                    line-height: 30px;
                }

        .td1 {
            text-align: right;
            padding-right: 5px;
            width: 120px;
        }

        .td2 {
            text-align: left;
            width: 380px;
        }

        .cancel {
            width: 100px;
            height: 30px;
            line-height: 30px;
            background-color: #999999;
            text-align: center;
            margin-left: 20px;
            margin-right: 20px;
            padding-left: 0px;
            cursor: pointer;
        }

        .save {
            width: 100px;
            height: 30px;
            line-height: 30px;
            background-color: #02BE74;
            cursor: pointer;
            padding-left: 0px;
        }

        .cg1 {
            width: 100%;
            text-align: center;
            font-size: 14px;
            color: red;
            margin-top: 80px;
        }

        .cg2 {
            margin-top: 20px;
            width: 100%;
        }

            .cg2 .div1 {
                float: left;
                width: 40px;
                height: 40px;
                padding-left: 100px;
            }

            .cg2 .div2 {
                float: left;
                height: 40px;
                width: 300px;
                line-height: 40px;
                font-size: 24px;
                padding-left: 5px;
            }

        .cg3 {
            margin-top: 80px;
            padding-left: 200px;
        }

        .msg {
            font-size: 14px;
            color: red;
            display: none;
            text-align: center;
            height: 25px;
            line-height: 25px;
        }
    </style>
    
</head>
<body>
    <script lang="javascript" type="text/javascript" src="<%=WebUtility.AppPath %>/Scripts/library/jquery-3.1.1.min.js"></script>
    <script lang="javascript" type="text/javascript" src="<%=WebUtility.AppPath %>/Scripts/library/layer/layer.js"></script>

    <form id="form1" runat="server">
        <div id="tab1">
            <table border="0">
                <tr>
                    <td class="td1"><span>*</span>姓名：</td>
                    <td class="td2">
                        <input type="text" id="userName" placeholder="请输入您的姓名" />

                    </td>
                </tr>
                <tr>
                    <td class="td1"><span>*</span>年龄：</td>
                    <td class="td2">
                        <input type="text" id="userAge" placeholder="请输入您的年龄" />

                    </td>
                </tr>
                <tr>
                    <td class="td1"><span>*</span>性别：</td>
                    <td class="td2">
                        <select id="userSex">
                            <option value="1">男</option>
                            <option value="2">女</option>
                        </select></td>
                </tr>
                <tr>
                    <td class="td1">手机号：</td>
                    <td class="td2">
                        <input type="text" id="userphone" /></td>
                </tr>
                <tr>
                    <td class="td1"><span>*</span>报名赛区：</td>
                    <td class="td2">
                        <asp:DropDownList ID="DDL_Area" runat="server"></asp:DropDownList>
                        <%--<select id="userArea">
                            <option value="1">华北区</option>
                            <option value="2">华南区</option>
                        </select>--%></td>
                </tr>
                <tr>
                    <td class="td1"><span>*</span>报名组别：</td>
                    <td class="td2">
                        <asp:DropDownList ID="DDL_Team" runat="server"></asp:DropDownList>
                        <%--<select id="userTeam">
                            <option value="1">华北区</option>
                            <option value="2">华南区</option>
                        </select>--%></td>
                </tr>
                <tr>
                    <td class="td1">学校：</td>
                    <td class="td2">
                        <input type="text" id="userSchool" /></td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <input type="button" class="cancel" value="清空所填信息" onclick="Cancel()" />
                        <input type="button" class="save" value="提交报名信息" onclick="Save()" />
                    </td>
                </tr>
            </table>
            <div class="msg"></div>
        </div>
        <div id="tab2" style="display: none;">
            <div class="cg1">为您生成的报名号为：XXXXXXXXXX</div>
            <div class="cg2">
                <div class="div1">
                    <img src="<%=WebUtility.AppPath %>/Styles/images/Activity/success.png" />
                </div>
                <div class="div2">恭喜成功报名该评比活动！</div>
            </div>
            <div class="cg3">
                <input type="button" class="save" value="查看我的评比" onclick="GoActivity()" />
            </div>
        </div>
    </form>
</body>
</html>
<script type="text/javascript">
    function GoActivity() {
        //parent.layer.closeAll();
        window.top.location.href = "<%=WebUtility.AppPath %>/Activity/MyActivityList.aspx";
    }
        function Cancel() {
            $("#userName").val("");
            $("#userAge").val("");
            $("#userphone").val("");
            $("#userSchool").val("");
            $("#userSex").val("1");
            $("#DDL_Area").val("1");
            $("#DDL_Team").val("1");
            $(".msg").hide();
        }
        function Save() {
            if ($("#userName").val() == "") {
                $(".msg").text("请输入姓名").show();
                $("#userName").focus();
                return;
            }
            if ($("#userAge").val() == "") {
                $(".msg").text("请输入年龄").show();
                $("#userAge").focus();
                return;
            }
            if (!(Math.floor($("#userAge").val()) == $("#userAge").val())) {
                $(".msg").text("年龄无效").show();
                $("#userAge").focus();
                return;
            }
            if ($("#userphone").val()!=""){
                var myreg = /^(((13[0-9]{1})|(15[0-9]{1})|(18[0-9]{1})|(19[0-9]{1}))+\d{8})$/;
                if (!myreg.test($("#userphone").val())) {
                    $(".msg").text("手机号码无效").show();
                    $("#userphone").focus();
                    return;
                }
            }
            $.ajax({
                url: "<%=WebUtility.AppPath %>/PublicService/Appraisal.ashx",
                type: "get",
                data: { Method: "signup", ActivityID: "<%=ActivityID%>", Name: escape($("#userName").val()), Age: $("#userAge").val(), Sex: $("#userSex").val(), Phone: $("#userphone").val(), School: $("#userSchool").val(), Area: $("#DDL_Area").val(), Team: $("#DDL_Team").val() },
                dataType: "json",
                async: false,
                success: function (result) {
                    if (result.Status == true) {
                        $(".cg1").text("为您生成的报名号为：" + result.Data);
                        $("#tab1").css("display", "none");
                        $("#tab2").css("display", "");
                    } else {
                        $(".msg").text(result.Message).show();//"报名失败，请联系管理员"
                    }
                }
            });

            
        }
    </script>