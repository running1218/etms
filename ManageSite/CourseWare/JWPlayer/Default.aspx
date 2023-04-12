<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="JWPlayer_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script language="javascript" src="Scripts/jquery.js" type="text/javascript"></script>
    <script src="Scripts/jwplayer.js" type="text/javascript"></script>
    <script src="Scripts/Player.js" type="text/javascript"></script>
    <link href="../../App_Themes/ThemeAdmin/JWPlayer.css" rel="stylesheet" type="text/css" />
</head>
<body class="video_body">
    <form id="form1" runat="server">
    <div class="video_mainbox">
        <div class="video_top">
            <h1 class="video_logo">
                </h1>
            <ul>
                <li><h5>[ 课程编码：<asp:Literal ID="ltlCourseCode" runat="server"></asp:Literal> ]</h5></li>
                <li><h5>[ 课程名称：<asp:Literal ID="ltlCourseName" runat="server"></asp:Literal> ]</h5></li>
            </ul>
        </div>
        <div>
            <div style="padding: 5px 0px; width: 960px; height: 540px; background-color: Black;">
                <div id="container">
                </div>
                <asp:Literal ID="litJWPlayer" runat="server"></asp:Literal>
            </div>
        </div>
        <div class="video_footer">
            <%= ETMS.Utility.WebUtility.CopyRight %></div>
    </div>
    </form>
</body>
</html>
