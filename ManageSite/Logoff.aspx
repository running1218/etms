<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="Logoff.aspx.cs"
    Inherits="Logoff" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <title></title>
</head>
<body>
    <script language="javascript">
        window.onload = function () {
            window.location.href = '<%=ETMS.Security.PassportClientSettings.GetConfig().LogOffUrl.ToString() %>redirectUrl=<%=this.RedirctUrl %>';
        }
    </script>
</body>
</html>
