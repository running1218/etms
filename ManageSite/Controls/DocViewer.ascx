<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DocViewer.ascx.cs" Inherits="Controls_DocViewer" %>
<div style="text-align:center; width:100%; height:100%;">
<object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" width="<%=this.Width %>px"
    height="<%=this.Height %>px" id="RFDocViewer201109">
    <param name="movie" value='<%=this.DocViewerUrl %>' />
    <param name="quality" value="high" />
    <param name="bgcolor" value="#ffffff" />
    <param name="allowScriptAccess" value="sameDomain" />
    <param name="allowFullScreen" value="true" />
    <param name="FlashVars" value="<%=ParamStr %>" />
    <param name="wmode" value="transparent" />
    <!--[if !IE]>-->
    <object type="application/x-shockwave-flash" data='<%=this.DocViewerUrl %>'
        width="<%=this.Width %>px" height="<%=this.Height %>px">
        <param name="FlashVars" value="<%=ParamStr %>" />
        <param name="quality" value="high" />
        <param name="bgcolor" value="#ffffff" />
        <param name="allowScriptAccess" value="sameDomain" />
        <param name="allowFullScreen" value="true" />
        <param name="wmode" value="transparent" />
        <!--<![endif]-->
        <!--[if gte IE 6]>-->
        <p>
            Either scripts and active content are not permitted to run or Adobe Flash Player
            version 10.2.0 or greater is not installed.
        </p>
        <!--<![endif]-->
        <a href="http://www.adobe.com/go/getflashplayer">
            <img src="http://www.adobe.com/images/shared/download_buttons/get_flash_player.gif"
                alt="Get Adobe Flash Player" />
        </a>
        <!--[if !IE]>-->
    </object>
    <!--<![endif]-->
</object>
</div>