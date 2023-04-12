<%@ Page Title="" Language="C#"  AutoEventWireup="true" CodeFile="Play.aspx.cs" Inherits="CourseWare_Play" %>
 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title><%= ETMS.Utility.WebUtility.PlatTitle %></title>
    <script src="../JScript/player.js"></script>
</head>
<body>
<form>


<table border="0" cellpadding="0" cellspacing="0" width="100%" height="100%" align="center">
       <tr>
          <td valign="middle" height="100%">
            <!-- 播放器 BEGIN -->
            <div id="divplay" style="display: none;background-color:Black; height:800px; text-align:center; color:White;">
            </div>
            <!-- 播放器 END -->
            <!-- 下载 BEGIN -->
            <div id="divdownload" style="display: none; background-color:#FFFF99; height:343px; text-align:center;color:White;">
            
            </div>
            <!-- 下载 END -->
            <!-- Loading BEGIN -->
            <div id="divloading" style="background-color:Black; height:343px; text-align:center; color:White;">
            <br /><br /><br /><br /><br />欢迎你！视频正在加载，请稍等……
            </div>
            <!-- Loading END -->
          </td>
       </tr>
    </table>
    
    <asp:Literal ID="Literal1" runat="server"></asp:Literal>

</form>
</body>
</html>