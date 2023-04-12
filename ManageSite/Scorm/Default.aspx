<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Scorm_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" style="background: #e7d9d1; height: 100%">
<head id="Head1" runat="server">
    <title>课程首页</title>
    <script language="javascript" src="API/jquery.js" type="text/javascript"></script>    
    <%--<script language="JavaScript" src="IECheckJre/js/ie_check.js" type="text/javascript"></script>--%>
</head>
<body style="height: 100%;">
    <form id="form1" runat="server">
    
<%--   <applet codebase="IECheckJre/applet" name="JREDetect" id="JREDetect" width="0" height="0" code="JREDetect.class">
    </applet>--%>
    <div class="dv_scrom_body">
        <iframe src='<%= this.ActionHref(string.Format("~/Scorm/top.aspx?CourseID={0}&CourseWareID={1}&ItemCourseResID={2}", CourseID, CourseWareID,ItemCourseResID)) %>'
            frameborder="0" scrolling="no" height="127" width="100%" name="API" id="topFrame">
        </iframe>
        <div class="dv_allExercise" id="dv_main">
            <iframe src='<%= this.ActionHref(string.Format("~/Scorm/scorm.aspx?CourseID={0}&CourseWareID={1}&ItemCourseResID={2}", CourseID, CourseWareID,ItemCourseResID)) %>'
                frameborder="0" scrolling="no" height="100%" width="100%" name="mFrame" id="mFrame">
            </iframe>
        </div>
        <div class="dv_copyrightscorm" id="copyright">
            <div class="dv_copright-1">
            </div>
            <div class="dv_copright-2">
                <p>
                    <asp:Literal ID="Literal2" runat="server"></asp:Literal></p>
            </div>
        </div>
    </div>
    </form>
</body>
<script type="text/javascript">
//    //检测jre插件是否安装 如果没安装跳转到下载安装页面
//    if (2 != getJreStatus()) {
//        window.location = "<%= url %>";
//    }
</script>
</html>
