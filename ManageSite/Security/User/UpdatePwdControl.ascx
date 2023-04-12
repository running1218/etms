<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UpdatePwdControl.ascx.cs"
    Inherits="Security_User_UpdatePwdControl" %>
<script type="text/javascript">
    //验证输入内容是否符合规则
    function checkInfo() {
        if ($("#<%=txtOldPassWord.ClientID %>").val() == "") {
            popAlertMsg("请输入原密码！", "提示");
            return false;
        } else if ($("#<%=txtPassWord.ClientID %>").val() == "") {
            popAlertMsg("请输入新密码！", "提示");
            return false;
        } else if ($("#<%=txtPassWord.ClientID %>").val().length < 6 || $("#<%=txtPassWord.ClientID %>").val().length > 20) {
            popAlertMsg("新密码长度必须大于6位小于20位！", "提示");
            return false;
        } else if ($("#<%=txtPassWord1.ClientID %>").val() == "") {
            popAlertMsg("请输入确认密码！", "提示");
            return false;
        } else if ($("#<%=txtPassWord.ClientID %>").val() != $("#<%=txtPassWord1.ClientID %>").val()) {
            popAlertMsg("确认密码与新密码不一致！", "提示");
            return false;
        } else {
            return true;
        }
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
<div class="dv_information">
    <table class="GridviewGray">
        <tr>
            <th>
                原&nbsp;&nbsp;密&nbsp;&nbsp;码：
            </th>
            <td>
                <asp:TextBox ID="txtOldPassWord" runat="server" TextMode="Password" CssClass="inputbox_120 floatleft"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>
                新&nbsp;&nbsp;密&nbsp;&nbsp;码：
            </th>
            <td>
                <asp:TextBox ID="txtPassWord" CssClass="inputbox_120 floatleft" runat="server" TextMode="Password" MaxLength="50" onKeyUp="pwStrength(this)"
                    onBlur="pwStrength(this)" ToolTip="只能输入字母、数字、符号。"></asp:TextBox><table class="tablePassword"
                        border="1" cellspacing="0" cellpadding="0" bordercolor="#cccccc" height="23"
                        style='display: inline'>
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
                <asp:TextBox ID="txtPassWord1" runat="server" TextMode="Password" CssClass="inputbox_120 floatleft"></asp:TextBox>
            </td>
        </tr>
    </table>
    <div class="dv_submit">
        <asp:Button ID="btnChangePassword" runat="server" CssClass="btn_Save" Text="保存" OnClick="btnChangePassword_Click"
            OnClientClick="javascript:return checkInfo()" /><asp:Button ID="btnReturn" runat="server"
                Text="返回" SkinID="Return" />
    </div>
</div>
