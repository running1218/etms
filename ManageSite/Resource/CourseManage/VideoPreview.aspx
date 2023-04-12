<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true" CodeFile="VideoPreview.aspx.cs" Inherits="ETMS.WebApp.Manage.Resource.CourseManage.VideoPreview" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        var Content=<%=Source%>;
        
    </script>
    <link href="../../App_Themes/ThemeAdmin/umVideo.css" type="text/css" rel="stylesheet">
    <link href="../../App_Themes/ThemeAdmin/video-js.css" type="text/css" rel="stylesheet">
    <script language="javascript" type="text/javascript" src="<%=ETMS.Utility.WebUtility.AppPath%>/JScript/jquery-1.11.1.min.js"></script>
    <script lang="javascript" type="text/javascript" src="<%=ETMS.Utility.WebUtility.AppPath%>/JScript/jquery.jcarousellite.js"></script>
    <script lang="javascript" type="text/javascript" src="<%=ETMS.Utility.WebUtility.AppPath%>/Tools/VideoPlayer/ckplayer/ckplayer.js"></script>
    <script lang="javascript" type="text/javascript" src="<%=ETMS.Utility.WebUtility.AppPath%>/JScript/screenfull.js"></script>
    <script lang="javascript" type="text/javascript" src="<%=ETMS.Utility.WebUtility.AppPath%>/JScript/umVideo.js"></script>
    <script lang="javascript" type="text/javascript" src="<%=ETMS.Utility.WebUtility.AppPath%>/JScript/fotorama.js"></script>
    <script src="<%=ETMS.Utility.WebUtility.AppPath%>/JScript/jquery.livequery.js"></script>
    <div class="play_file"></div>
    <script lang="javascript" type="text/javascript" src="<%=ETMS.Utility.WebUtility.AppPath%>/JScript/Preview.js"></script>
    <script type="text/javascript">
        $(function(){
            $(window.parent.document).find(".ymPrompt_close").off().click(function(){
                $(".play_file").empty();
            });
        })
        
    </script>
</asp:Content>

