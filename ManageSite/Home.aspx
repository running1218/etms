<%@ Page Language="C#" AutoEventWireup="true" Inherits="ETMS.WebApp.Admin_Default"
    CodeFile="Home.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
 <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <title>欢迎您使用企业培训系统</title>    
    <script src="JScript/jquery-1.7.2.js" type="text/javascript"></script>
    <script src="JScript/JSFunction.js" type="text/javascript"></script>  
    <script src="JScript/ymPromptYuan.js" type="text/javascript"></script>
    <link href="App_Themes/ThemeAdmin/ymPrompt.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">
        $(function () {         
            setFrameHeight();          
        })

        $(document).ready(function () {
            var o = window.top.document.getElementsByClassName('active-module');
            var code = $(o).attr('accesskey');
            $('.dv_welcomebg').css({ "background": "url(App_Themes/ThemeAdmin/Images/flows/" + code + ".png) 30px 0px no-repeat" });
        });
    </script>
</head>
<body >
    <div class="dv_hormain">      
    </div>
     <div class="dv_welcomebg"></div>
</body>
</html>
