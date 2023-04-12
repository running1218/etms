<%@ Page Language="C#" AutoEventWireup="true" CodeFile="top.aspx.cs" Inherits="Scorm_top" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Top</title>
</head>
<body>
<form id="form1" runat="server">
         <div class="dv_topMenu">
        <div class="dv_scromWebLogo dv_scromWebLogo_Scorm">
        </div>
        <div class="dv_scromRepeat">
            <div class="dv_courseState"><span style="color:Orange; text-align:center">如果SkillSoft课件无法正常播放，请<asp:LinkButton
                    ID="lbtnDownload" runat="server" onclick="lbtnDownload_Click">下载JAVA插件</asp:LinkButton>安装后重试。&nbsp;&nbsp;&nbsp;&nbsp;</span>
                <em>[课程编码：<asp:Literal ID="ltlCourseCode" runat="server"></asp:Literal>]</em><em>[课程名称：<asp:Literal
                    ID="ltlCourseName" runat="server"></asp:Literal>] </em>
                    <input id="resId" type="text" name="resId" value="ResourceID" style=" visibility:hidden;" />
            </div>
        </div>
    </div>
   </form>
</body>
<script language="javascript" type="text/javascript">
    var UserName = "<%=UserName %>";
    var ItemCourseResID = "";
</script>    
    <script language="javascript" src="API/jquery.js" type="text/javascript"></script>
    <script language="javascript" src="API/CallWebService.js" type="text/javascript"></script>
    <script language="javascript" src="API/CallCurseFunction.js" type="text/javascript"></script>
    <script type="text/javascript">
        //如果学习完成时在前台给标题打勾
        function setTreeViewNodeTitle(id) {
            var leftFrame = window.parent.frames["mFrame"].frames["leftFrame"];
            if (leftFrame != null) {
                var obj = leftFrame.document.getElementById(id);
                if (obj != null) {
                    obj.innerHTML = "√" + obj.innerHTML.replace("√", "");
                }
            }
        }
    </script>
</html>
