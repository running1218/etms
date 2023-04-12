<%@ Page Language="C#" AutoEventWireup="true" CodeFile="right.aspx.cs" Inherits="Scorm_right" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>right</title>
    <script language="javascript" src="API/jquery.js" type="text/javascript"></script>
    <script language="javascript" src="API/APIWrapper.js" type="text/javascript"></script>
    <script language="javascript" src="API/AUFunctions.js" type="text/javascript"></script>
    <script language="javascript" src="API/SCOFunction.js" type="text/javascript"></script>

    <script type="text/javascript">
       

        function CallInit() {
            if (LMSInitialize("").toUpperCase() == "TRUE") {
                alert("初始化Scorm成功！");
            }
            else {
                alert("初始化Scorm失败！");
            }
        }

        function CallFinish() {
            if (LMSFinish("") == "True") {
                alert("成功调用完成方法！");
            }
            else {
                alert("调用完成方法失败！");
            }
        }

        function CallLMSGetErrorString() {
            var errorCode = document.getElementById("errcode").value;
            alert("错误信息为：" + LMSGetErrorString(errorCode));
        }
      
    </script>
</head>

<body style="background:white;">
   
    <div class="flashContent" id="dv_scrom_right">
             <div class="dv_al"></div>
    </div>
  
</body>
</html>
