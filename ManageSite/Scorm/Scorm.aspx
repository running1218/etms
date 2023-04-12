<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="Scorm.aspx.cs"
    Inherits="Scorm_Scorm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>scorm</title>
    <link href="../App_Themes/ThemeLearn/default.css" rel="stylesheet" type="text/css" />
    <script src="API/AutoFrameHeight.js" type="text/javascript"></script>
</head>
<frameset cols="193,*" frameborder="no" border="0" framespacing="0" id="myFrame">
    <frame src='<%= this.ActionHref(string.Format("~/Scorm/left.aspx?CourseID={0}&CourseWareID={1}&ItemCourseResID={2}", CourseID, CourseWareID,ItemCourseResID)) %>'
        scrolling="auto" noresize="noresize" id="leftFrame" name="leftFrame" title="leftFrame" style="overflow-x:hidden;"
        frameborder="0" height="100%" />
    <frame src='<%= this.ActionHref(string.Format("~/Scorm/right.aspx?CourseID={0}&CourseWareID={1}&ItemCourseResID={2}", CourseID, CourseWareID,ItemCourseResID)) %>'
        name="rightFrame" id="rightFrame" title="rightFrame" height="100%" scrolling="auto" onload="setFrameHeight()" />
</frameset>
</html>
