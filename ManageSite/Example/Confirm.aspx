<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Confirm.aspx.cs" Inherits="Default2" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <script language="javascript" type="text/javascript" src="<%=ETMS.Utility.WebUtility.AppPath%>/JScript/jquery-1.11.1.min.js"></script>
    <script language="javascript" type="text/javascript" src="<%=ETMS.Utility.WebUtility.AppPath%>/JScript/jsfunction.js"></script>
    <script src="<%=ETMS.Utility.WebUtility.AppPath%>/JScript/ymPromptYuan.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript" src="<%=ETMS.Utility.WebUtility.AppPath%>/JScript/ymPromptYuan.js"></script>
    <script src="<%=ETMS.Utility.WebUtility.AppPath%>/Tools/DateControl/WdatePicker.js"
        type="text/javascript"></script>
    <form id="form1" runat="server">
    <div>
        <!-- 自定义按钮实现自定义confirm功能 -->
        <cc1:CustomButton runat="server" ID="Button3" Text="11111111111" OnClick="btn1_Click"
            EnableConfirm="true" ConfirmTitle="sdfasfasf" ConfirmMessage="111111111111" />
        <br />
        <br />
        <!-- 自定义链接按钮实现自定义confirm功能 -->
        <cc1:CustomLinkButton runat="server" ID="CustomButton1" Text="2222222222222" OnClick="btn1_Click"
            EnableConfirm="true" ConfirmTitle="sdfasfasf" ConfirmMessage="22222222222222" />
    </div>
    </form>
</body>
</html>
