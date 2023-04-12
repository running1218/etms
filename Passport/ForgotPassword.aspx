<%@ Page Language="C#" AutoEventWireup="true" Inherits="ForgotPassword" Codebehind="ForgotPassword.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="App_Themes/ThemeDefault/public.css" rel="stylesheet" type="text/css" />
    <link href="App_Themes/ThemeDefault/default.css" rel="stylesheet" type="text/css" />
    <link href="App_Themes/ThemeDefault/ymPrompt.css" rel="stylesheet" type="text/css" />    
    <script language="javascript" type="text/javascript" src="<%=ETMS.Utility.WebUtility.AppPath%>/JScript/jquery-1.11.1.min.js"></script>
    <script language="javascript" type="text/javascript" src="<%=ETMS.Utility.WebUtility.AppPath%>/JScript/jsfunction.js"></script>
    <script src="<%=ETMS.Utility.WebUtility.AppPath%>/JScript/ymPromptYuan.js" type="text/javascript"></script>
    <script type="text/javascript">
        //验证输入内容是否符合规则
        function checkInfo() {
            if ($("#<%=txtUserName.ClientID %>").val() == "") {
                popAlertMsg("请输入用户名！", "提示");
                return false;
            } else if ($("#<%=txtNewPassword.ClientID %>").val() == "") {
                popAlertMsg("请输入新密码！", "提示");
                return false;
            } else if ($("#<%=txtNewPassword.ClientID %>").val().length < 6 || $("#<%=txtNewPassword.ClientID %>").val().length > 20) {
                popAlertMsg("新密码长度必须大于6位小于20位！", "提示");
                return false;
            } else if ($("#<%=txtNewPassword2.ClientID %>").val() == "") {
                popAlertMsg("请输入确认密码！", "提示");
                return false;
            } else if ($("#<%=txtNewPassword.ClientID %>").val() != $("#<%=txtNewPassword2.ClientID %>").val()) {
                popAlertMsg("确认密码与新密码不一致！", "提示");
                return false;
            } else {
                showLodfileDiv();
                return true;
            }
        }
        function showLodfileDiv() {
            var scrollHeight = document.documentElement.scrollHeight;
            $(".shadowBj").height(scrollHeight);
            $(".shadowBj").show();
            $(".loadfile").show();
        }
        function hideLodfileDiv() {
            $(".shadowBj").hide();
            $(".loadfile").hide();
        }
    </script>
    <script type="text/javascript">
        //CharMode函数
        //测试某个字符是属于哪一类.
        function CharMode(iN) {
            if (iN >= 48 && iN <= 57) //数字
                return 1;
            if (iN >= 65 && iN <= 90) //大写字母
                return 2;
            if (iN >= 97 && iN <= 122) //小写
                return 4;
            else
                return 8; //特殊字符
        }
        //bitTotal函数
        //计算出当前密码当中一共有多少种模式
        function bitTotal(num) {
            modes = 0;
            for (i = 0; i < 4; i++) {
                if (num & 1) modes++;
                num >>>= 1;
            }
            return modes;
        }
        //checkStrong函数
        //返回密码的强度级别
        function checkStrong(sPW) {
            if (sPW.length <= 5)
                return 0; //密码太短
            Modes = 0;
            for (i = 0; i < sPW.length; i++) {
                //测试每一个字符的类别并统计一共有多少种模式.
                Modes |= CharMode(sPW.charCodeAt(i));
            }
            return bitTotal(Modes);
        }
        //pwStrength函数
        //当用户放开键盘或密码输入框失去焦点时,根据不同的级别显示不同的颜色

        function pwStrength(obj) {
            var pwd = obj.value;
            pwdTrim(obj);

            O_color = "#eeeeee";
            L_color = "#FF0000";
            M_color = "#FF9900";
            H_color = "#33CC00";
            if (pwd == null || pwd == '') {
                Lcolor = Mcolor = Hcolor = O_color;
            }
            else {
                S_level = checkStrong(pwd);
                switch (S_level) {
                    case 0:
                        Lcolor = Mcolor = Hcolor = O_color;
                    case 1:
                        Lcolor = L_color;
                        Mcolor = Hcolor = O_color;
                        break;
                    case 2:
                        Lcolor = Mcolor = M_color;
                        Hcolor = O_color;
                        break;
                    default:
                        Lcolor = Mcolor = Hcolor = H_color;
                }
            }
            document.getElementById("strength_L").style.background = Lcolor;
            document.getElementById("strength_M").style.background = Mcolor;
            document.getElementById("strength_H").style.background = Hcolor;
            return;
        }
        //去掉空格
        function pwdTrim(obj) {
            var pwd = obj.value;
            obj.value = pwd.replace(/\s+/g, "");
        }
    </script>
    <script type="text/javascript" language="javascript">
        $(function () {
            isLoadFish();
            hideLodfileDiv();
        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <!--表单录入-->
    <div class="dv_information">
        <table class="GridviewGray">
            <tr>
                <th>
                    用&nbsp;&nbsp;户&nbsp;&nbsp;名：
                </th>
                <td>
                    <asp:TextBox ID="txtUserName" runat="server" MaxLength="50" CssClass="inputbox_210"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>
                    新&nbsp;&nbsp;密&nbsp;&nbsp;码：
                </th>
                <td class="Search_Area">
                    <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" MaxLength="50"
                        CssClass="inputbox_120 floatleft" onKeyUp="pwStrength(this)" onBlur="pwStrength(this)" ToolTip="只能输入字母、数字、符号。"></asp:TextBox>
                    <table class="tablePassword" border="1" cellspacing="0" cellpadding="0" bordercolor="#cccccc"
                        height="23" style='display: inline'>
                        <tr align="center" bgcolor="#eeeeee">
                            <td width="33%" id="strength_L">
                                弱
                            </td>
                            <td width="33%" id="strength_M">
                                中
                            </td>
                            <td width="33%" id="strength_H">
                                强
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <th>
                    确认密码：
                </th>
                <td>
                    <asp:TextBox ID="txtNewPassword2" runat="server" TextMode="Password" MaxLength="50"
                        CssClass="inputbox_120"></asp:TextBox>
                </td>
            </tr>
        </table>
        <br />
        提示：<br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;1、新密码重置成功后。<br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2、系统将会把激活连接发送到您用户信息所填写邮箱中，请登录邮箱激活。
    </div>
    <div class="dv_submit">
        <asp:Button ID="btnReset" runat="server" Text="重置密码" CssClass="btn_Repassword" OnClick="btnReset_Click"
            OnClientClick="javascript:return checkInfo()" />
        <input id="btn_Cancel" type="button" class="btn_Close" value="取消" onclick="javascript:closeWindow(null)" />
    </div>
    <div class='shadowBj' style="display: none;">
    </div>
    <div class="loadfile">
        <img src="App_Themes/ThemeDefault/Images/waiter.gif" alt="正在执行后台操作，请稍候..." align="absmiddle" />
        正在执行后台操作，请稍候...
    </div>
    </form>
</body>
</html>
