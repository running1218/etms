<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpFile.aspx.cs" Inherits="Resource_CoursewareManage_UpFile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="../../JScript/jquery-1.7.2.js" type="text/javascript"></script>
    <script src="../../JScript/ymPromptYuan.js" type="text/javascript"></script>
    <script src="../../JScript/JSFunction.js" type="text/javascript"></script>
    <style type="text/css">
        html, body
        {
            width: 100%;
            height: 100%;
        }
        body
        {
            font-size: 13px;
            margin: 0px;
            font-family: Verdana,Arial, Helvetica, Sans-serif;
            background-color: #3d4a57;
        }
        input
        {
            border: 1px solid gray;
            padding: 2px 5px 2px 5px;
            margin-left: 5px;
        }
        .title
        {
            background-color: #02877E;
            color: white;
            font-size: 16px;
            height: 35px;
            padding-left: 20px;
        }
        .content
        {
            width: 880px;
            background-color: #eeeeee;
        }
    </style>
    <script language="javascript" type="text/javascript">
        //隐藏窗口中的关闭
        $(parent.document).find(".ymPrompt_close").hide();

        //上传是否成功
        var upResult = false;
        //是否开始上传
        var isUpFile = false;

        function GetRequest() {
            var url = location.search; //获取url中"?"符后的字串
            var theRequest = new Object();

            if (url.indexOf("?") != -1) {
                var str = url.substr(1);
                strs = str.split("&");
                for (var i = 0; i < strs.length; i++) {
                    theRequest[strs[i].split("=")[0]] = unescape(strs[i].split("=")[1]);
                }
            }
            return theRequest;
        }

        //连接FTP服务器 
        function connect() {
            try {
                var ftpServer = document.getElementById("hfFtpServer").value;
                var ftpUser = document.getElementById("hfFtpUser").value;
                var ftpPassword = document.getElementById("hfFtpPassword").value;
                var ftpPort = document.getElementById("hfFtpPort").value;

                FtpLibrary.ServerName = ftpServer;
                FtpLibrary.Username = ftpUser;
                FtpLibrary.Password = ftpPassword;
                FtpLibrary.Port = ftpPort;
                //--
                if (!FtpLibrary.Connect()) {
                    $("#spMsg").html("<span class='colorRed'>提示：</span>Ftp服务器连接失败！");
                }
            } catch (e) {
                $("#spMsg").html("<span class='colorRed'>提示：</span>Ftp服务器连接失败！");
            }
        }
        //上传文件 
        function upload(isFolder) {
            $("#spMsg").html("");
            var fileName = getFileName(getFilePath());
            if (fileName == "") {
                $("#spMsg").html("<span class='colorRed'>提示：</span>请选择上传的课件包！");
                return false;
            }
            isUpFile = true;
            connect();
            $("#trk").show();
            var localPath = getFilePath();
            var remotePath = GetFileName(localPath);
            if (remotePath == "") {
                remotePath = "\\";
            }
            try {
                document.getElementById("btnUpload").disabled = true;
                document.getElementById("btnAbort").disabled = false;
                document.getElementById("btnClose").disabled = true;

                FtpLibrary.ReplaceIndex = 1;
                //主动的还是被动
                FtpLibrary.Passive = false;
                FtpLibrary.LocalPath = localPath;
                FtpLibrary.RemotePath = remotePath;
                FtpLibrary.AllowType = $("#hfFtpScromAllowType").val();
                FtpLibrary.DenyType = "";
                FtpLibrary.MaxSize = $("#hfFtpMaxSize").val();
                if (FtpLibrary.Upload()) {
                    $("#spMsg").html("<span class='colorRed'>提示：</span>上传成功！");
                    upResult = true;
                } else {
                    isUpFile = false;
                    $("#spMsg").html("<span class='colorRed'>提示：</span>" + FtpLibrary.ErrorInfo.replace("[试用版]", "").replace("[ ,", "[").replace(", ]", "]"));
                }
                document.getElementById("btnUpload").disabled = false;
                document.getElementById("btnAbort").disabled = true;
                document.getElementById("btnClose").disabled = false;
            } catch (e) {
            }
        }

        function GetFileName(path) {
            var dic = path.split("\\");
            if (dic.length > 0)
                return GetFolder() + dic[dic.length - 1];

            return "";
        }

        function GetFolder() {
            var date = new Date();
            return date.getFullYear() + "\\" + (date.getMonth() + 1) + "\\";
        }

        //取消传输
        function cancel(current) {
            try {
                FtpLibrary.Abort();
                document.getElementById("btnAbort").disabled = true;
                document.getElementById("btnClose").disabled = false;
            } catch (e) {
                alert("cancel\n" + e.description);
            }
        }
        //关闭连接
        function ftpclose() {
            var fileName = getFileName(getFilePath());
            if (true == isUpFile) {
                $(window.parent.document).find(".lblFileName").html(fileName);
                $(window.parent.document).find(".lblState").html(upResult == true ? "<span class='colorGreen'>上传成功</span>" : "<span class='colorRed'>上传失败</span>");
                $(window.parent.document).find(".txtFileName").val(upResult == true ? fileName : "");
                self.close();
            }
            closeWindow();
        }

        //获取路径
        function getFilePath() {
            return $("#txtUpFile").val();
        }

        //获取路径中的文件名
        function getFileName(obj) {
            var pos = obj.lastIndexOf("\\") * 1;
            return obj.substring(pos + 1);
        }

        //获取路径中的后缀名
        function getFileType(obj) {
            var pos = obj.lastIndexOf(".") * 1;
            return obj.substring(pos + 1);
        }

        //浏览事件
        $(document).ready(function () {
            $("#btnUpFile").click(function () {
                $("#fuSelectFiles").click();
            });
            $("#spMsg").html("<span class='colorRed'>提示：</span>请在IE浏览器下使用此功能，上传格式只能为：" + $("#hfFtpScromAllowType").val());
        });

        //上传控件的onchange事件
        function setUpFileVal() {
            $("#fuSelectFiles").select();
            $("#picDiv").focus();
            $("#txtUpFile").val(document.selection.createRange().text);
        }

        //验证客户端是否安装了FTP插件
        function fnCheckFtpLibrary() {
            var OcxInstalled = false;
            $(".divCheckFtpSetup").show();
            $(".divUpFile").hide();
            $(".divFtpSetup").hide();
            try {
                try {
                    if (FtpLibrary.Version) {
                        OcxInstalled = true;
                    }
                } catch (e) {
                }

                $(".divCheckFtpSetup").hide();
                if (!OcxInstalled) {
                    $(".divUpFile").hide();
                    $(".divFtpSetup").show();
                } else {
                    $(".divUpFile").show();
                    $(".divFtpSetup").hide();
                }
            } catch (e) {
                alert("fnCheckFtpLibrary\n" + e.description);
            }
        }

        //安装插件
        function install() {
            try {
                //alert("请在接下来打开的所有IE对话框中,点击:[打开]、[运行]或[是]按钮.\n如果出现非IE的下载对话框,比如\"迅雷\",请点击[取消]按钮.");
                window.open("Activex/ftpocx_setup.exe", "_blank");
            } catch (e) {
                alert("install\n" + e.description);
            }
        }

    </script>
</head>
<body onload="fnCheckFtpLibrary()">
    <div class="divCheckFtpSetup" style="height: 159px; margin-bottom: 28px; overflow: hidden;">
        <p style="margin-top:50px; text-align:center"><img src="../../App_Themes/ThemeAdmin/Images/waiter.gif" /> 正在检测FTP的传控件，<br />IE浏览器会出现假死的情况请耐心等待...</p>
    </div>
    <form runat="server" id="Form1">
    <div class="divUpFile" style="text-align: center; margin-top: 10px; display:none">
        <asp:HiddenField ID="hfFtpServer" runat="server" Value="" />
        <asp:HiddenField ID="hfFtpUser" runat="server" Value="" />
        <asp:HiddenField ID="hfFtpPassword" runat="server" Value="" />
        <asp:HiddenField ID="hfFtpPort" runat="server" Value="21" />
        <asp:HiddenField ID="hfCourseWareID" runat="server" />
        <asp:HiddenField ID="hfFtpScromAllowType" runat="server" />
        <asp:HiddenField ID="hfFtpMaxSize" runat="server" />
        <input type="file" id="fuSelectFiles" onchange="setUpFileVal()" style="width: 297px;
            height: 24px; position: absolute; background: red; opacity: 0.0; filter: alpha(opacity=0);
            left: 0px; z-index: -1" />
        <input type="text" id="txtUpFile" style="width: 220px; margin-right: 1px;" readonly /><input
            type="button" id="btnUpFile" value="浏览..." style="height: 22px; margin-left: 0px;" />
    </div>
    </form>
    <div class="divUpFile" style="height: 126px; margin-bottom: 28px; overflow: hidden; display:none">
        <table width="100%" border="0" cellspacing="0" cellpadding="0" align="left">
            <tr>
                <object id="FtpLibrary" classid="CLSID:31AE647D-11D1-4E6A-BE2D-90157640019A" style="display: none"
                    codebase="activex/EaseWeFtp.Cab#version=4,6,0,2">
                </object>
            </tr>
            <tr>
                <td colspan="4" style="padding-top: 20px; padding-bottom: 20px; text-align: center;">
                    <input type="button" value="开始上传" id="btnUpload" onclick="upload(false)" class="btn_upload"
                        style="left: 280px; top: 1px" />
                    <input type="button" value="取消传输" id="btnAbort" onclick="cancel()" class="btn_Nopass"
                        disabled="disabled" /><div id="picDiv" style="width: 1px; height: 1px; margin: 0px;
                            z-index: 1px; right: 0px;">
                        </div>
                </td>
            </tr>
            <tr style="display: none" id="trk">
                <td style="text-align: right;">
                    进度：
                </td>
                <td colspan="3" style="width: 300px">
                    <table style="float: left; width: 85%" border="0">
                        <tr>
                            <td width="95%">
                                <div style="border: 1px solid #515151; background-color: #ffffff">
                                    <span id="idCompleted" style="background-color: #3333cc; height: 15px; display: block;
                                        width: 0%;"></span>
                                </div>
                            </td>
                            <td width="10px">
                                <span id="idProgress">0%</span>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <div style="position: absolute; bottom: 60px; left: 10px;">
            <span id="spMsg" style="width: 400px; display: block; word-break: break-all; white-space: normal">
            </span>
        </div>
        <script language="javascript" type="text/javascript" for="FtpLibrary" event="OnTransfer">
            //显示传输进度
            function FtpLibrary_OnTransfer() {
	            try{
		            //---------
		            var percent = FtpLibrary.Percent;
		            document.getElementById("idCompleted").style.width = percent + "%";
		            document.getElementById("idProgress").innerHTML = parseInt(percent) + "%";
	            }catch(e){
		            FtpLibrary.Abort();
		            //--
		            alert("FtpLibrary_OnTransfer:\n"+e.description);
	            }
            }
            FtpLibrary_OnTransfer();

        </script>
    </div>
    <div class="divFtpSetup" style="height: 159px; margin-bottom: 28px; overflow: hidden;
        display: none">
        <p style="margin-top:50px; margin-left:65px; width:310px">请在浏览器上方黄色的条上点击右键，选择为此计算机上的所有用户安装此加载项，刷新当前页面后会出现安装窗口 点击安装后即可正常使用。</p>
<%--        <p>
            请点击[安装控件]按钮安装控件。<br />
            如有任何问题，欢迎随时与我们联系。</p>
        <div>
            <input type="button" value="安装控件" id="btnInstall" onclick="install()" />
        </div>--%>
    </div>
    <div style="text-align: center;" class="dv_submit">
        <input type="button" value="关闭" id="btnClose" class="btn_Cancel" onclick="ftpclose();"
            style="left: 280px; top: 1px" />
    </div>
</body>
</html>
