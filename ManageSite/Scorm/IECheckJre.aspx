<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IECheckJre.aspx.cs" Inherits="Scorm_IECheckJre" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Java插件检测</title>
    <link rel="stylesheet" type="text/css" href="/css/visitorLayout.css " />
    <link rel="stylesheet" type="text/css" href="/css/visitorPublic.css" />
    <link rel="stylesheet" type="text/css" href="/css/visitorMain.css" />
    <script language="JavaScript" src="IECheckJre/js/ie_check.js" type="text/javascript"></script>
    <script language="JavaScript" src="IECheckJre/js/cookie_lib.js" type="text/javascript"></script>
    <script type="text/javascript" src="IECheckJre/js/overlib.js"></script>
    <script type="text/javascript" src="IECheckJre/js/hidden.js"></script>
    <script type="text/javascript" language="JavaScript">
        var MsgOK = '<font color="green"><b>&nbsp;&nbsp;<img src="IECheckJre/images/ie_check/i_true.gif" alt="成功！" />&nbsp;&nbsp;&nbsp;&nbsp;</b></font>';
        var MsgNG = '<font color="red"><b>&nbsp;&nbsp;<img src="IECheckJre/images/ie_check/i_false.gif" alt="失败！" />&nbsp;&nbsp;&nbsp;&nbsp;</b></font>';

        var Msg = new Array(
    new Array(
        MsgNG + '您正在使用的浏览器不被支持。<a href="#" onClick="javascript:doit(ie_1);"><img src="IECheckJre/images/ie_check/ie_b_dj.gif" alt="点击这里" /></a>'
        , ''
        , MsgOK + '您正在使用正确的浏览器。'
    )
    ,
    new Array(
        ''
        , ''
        , MsgOK + '您的浏览器允许Javascript。'
    )
    ,
    new Array(
        MsgNG + '您的浏览器不允许Cookies。<a href="#" onClick="javascript:doit(ie_3);"><img src="IECheckJre/images/ie_check/ie_b_dj.gif" alt="点击这里" /></a>'
        , ''
        , MsgOK + '您的浏览器允许Cookies。'
    )
    ,
    new Array(
        MsgNG + '您的浏览器不允许弹出窗口。<a href="#" onClick="javascript:doit(ie_4);"><img src="IECheckJre/images/ie_check/ie_b_dj.gif" alt="点击这里" /></a>'
        , ''
        , MsgOK + '您的浏览器允许弹出窗口。'
    )
    ,
    new Array(
        MsgNG + '您需要安装Flash播放器。<a href="#" onClick="javascript:doit(ie_5);"><img src="IECheckJre/images/ie_check/ie_b_dj.gif" alt="点击这里" /></a>'
        , MsgNG + '您需要更新您的Flash播放器。<a href="#" onClick="javascript:doit(ie_5);"><img src="IECheckJre/images/ie_check/ie_b_dj.gif" alt="点击这里" /></a>'
        , MsgOK + '您的浏览器正在使用正确的Flash播放器。'
    )
    ,
    new Array(
        MsgNG + '您需要安装Java插件。<a href="IECheckJre/jre-6u7-windows-i586-p.exe">下载插件</a>'
        , MsgNG + '您需要更新您的Java插件。<a href="#" onClick="javascript:doit(ie_7);"><img src="IECheckJre/images/ie_check/ie_b_dj.gif" alt="点击这里" /></a>'
        , MsgOK + '您的浏览器正在使用正确的Java插件。'
    )
)

        var query_str = "${queryStr}";
        function go_study() {
            document.location.href = "courseRequest.jhtml?" + query_str;
        }
        
    
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <applet codebase="IECheckJre/applet" name="JREDetect" id="JREDetect" width="0" height="0"
            code="JREDetect.class">
        </applet>
        <table width="80%" cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td class="alignright">
                    <strong>Java插件：</strong>
                </td>
                <td>
                    <span id="jre_check">检查中...</span>
                </td>
            </tr>
            <tr>
                <td class="alignright">
                </td>
                <td>
                    <a href="javascript:window.location.reload()">
                        <img src="IECheckJre/images/ie_check/ie_b_zcjc.gif" alt="再次检查" /></a>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center; color: #666;">
                    (检测失败，可能会导致您无法学习课程。请按照右侧的提示进行相应操作！)
                </td>
            </tr>
        </table>
        <script type="text/javascript">
            //如果检测成功就跳转至学习页面
            function jreStatusOK() {
                window.location = "<%= url %>";
            }
            showEachClientCheckResult();
        </script>
    </div>
    </form>
</body>
</html>
