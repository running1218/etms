<%@ Page Language="C#" AutoEventWireup="true" Inherits="ETMS.WebApp.Default" CodeFile="Default.aspx.cs" ViewStateMode="Disabled" %>

<%@ Register Src="Security/Controls/FunctionTool.ascx" TagName="FunctionTool" TagPrefix="uc1" %>
<%@ Register Src="Controls/MenuModule.ascx" TagName="MenuModule" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title><%=ETMS.Utility.WebUtility.PlatTitle %></title>
    <script src="JScript/jquery-1.11.1.min.js" type="text/javascript"></script>
    <script src="JScript/JSFunction.js" type="text/javascript"></script>
    <script src="JScript/Tree.js" type="text/javascript"></script>
    <script src="JScript/jquery.tmpl.js" type="text/javascript"></script>
    <script type="text/javascript">
        var iecopy = Get_IE_Version();
        if (iecopy == 6) {          
            $("html").css({"overflow-y":"auto","overflow-x":"hidden"});   
        }

        if (window.addEventListener) {
            //如果是firfox浏览器,则使用window.addEventListener触化resize事件,注意第三个参数是false,不能丢失           
            window.addEventListener('onresize', doResize, false);
            window.addEventListener('resize', doResize, false);
        }
        else {
            //如果是IE浏览器则使用window.onresize触化浏览器resize事件  
            var resizeTimer = null;
            window.onresize = function () {
                resizeTimer = resizeTimer ? null : setTimeout(doResize, 0);                
            }
        }
        function doResize() {
            autoHeight();
            if (iecopy == 6) {
                if ($("body").width() <= 1000)
                    $("html").css({ "overflow-x": "auto" });
                else
                    $("html").css({ "overflow-x": "hidden" });
            }
        } 
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <!--网站顶部-->
    <div class="header" id="header">        
        <div class="log">
            <img src="" id="imgBG" runat="server" />
        </div>
        <div class="headeright">
            <uc1:MenuModule ID="menuModule" runat="server" />
        </div>
        <div class="dv_exitPannel">
            <a href='security/user/ChangeUserInfo.aspx' class="hello" target="myframe" title="<%= ETMS.AppContext.UserContext.Current.RealName%>"></a>
            <a href="Logoff.aspx" class="btn_Exit" title="退出">退出</a>
        </div>
    </div>
    <div class="dv_main" id="dv_main">
        <!--网站左边-->
        <div class="dv_left" id="dv_left">
            <a href="javascript:expandTree();" onfocus="this.blur()" title="隐藏左边菜单" class="expandFrame">
                <span class="hide">隐藏</span></a>
            <div id="dv_Menu"></div>
        </div>
        <!--主体右边-->
        <div class="dv_right" id="dv_right" style="position: relative">
            <a class="hideFrame" style="display: none" onfocus="this.blur()" title="显示左边菜单" href="javascript:expandTree();">
                <span class="hide">显示</span></a>
            <iframe src="home.aspx" noresize="noresize" id="myframe" width="100%" scrolling="no"
                frameborder="0" height="100%" name="myframe" style="overflow:hidden;"></iframe>
        </div>
        <div class="copyright" id="copyright">
             <%=ETMS.Utility.WebUtility.CopyRight %></div>
    </div>
    </form>
</body>
</html>
