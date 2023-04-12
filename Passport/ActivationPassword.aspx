<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="ActivationPassword" Codebehind="ActivationPassword.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>密码重置激活</title>
    <script language="javascript" type="text/javascript">

        //倒计时
        var i = 4;
        intervalid = setInterval("fun()", 1000);
        function fun() {
            if (i == 0) {
                winClose();
                clearInterval(intervalid);
            }
            document.getElementById("labTime").innerHTML = i;
            i--;
        }

        //关闭页面
        function winClose() {
            var isIE = navigator.appName == "Microsoft Internet Explorer";
            //alert(isIE);
            if (isIE) {
                window.opener = "";
                window.open("", "_self");
                window.close();
            }
            else {
                /*FF 还要在 about:config 允许脚本脚本关闭窗口*/
                window.close();
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width: 100%; text-align: center; font-size: 12px">
        <br /><asp:Label ID="labMsg" runat="server" Text=""></asp:Label><br />
        此页面将在 <span style="color: Red">
            <asp:Label ID="labTime" runat="server" Text="5"></asp:Label></span> 秒后自动关闭
    </div>
    </form>
</body>
</html>
