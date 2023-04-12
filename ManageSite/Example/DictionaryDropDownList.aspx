<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DictionaryDropDownList.aspx.cs"
    Inherits="DictionaryDropDownList" %>

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
        <cc1:DictionaryDropDownList runat="server" ID="DictionaryDropDownList1" DictionaryType="Dic_Sys_CourseType"
            IsShowChoose="true" />
               <cc1:DictionaryDropDownList runat="server" ID="DictionaryDropDownList2" DictionaryType="Dic_Sys_CourseAttr"
            IsShowChoose="true" />
    </div>
    </form>
</body>
</html>
