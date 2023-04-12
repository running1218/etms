<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DateTime.aspx.cs" Inherits="Default2" %>

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
    浮点输入：<cc1:CustomTextBox runat="server" ContentType="Decimal" /><br />
    整数输入：<cc1:CustomTextBox ID="CustomTextBox1" runat="server" ContentType="Number" />
    <div>
        <table>
            <thead>
                时间控件使用
            </thead>
            <tr>
                <th>
                    方式1
                </th>
                <th>
                    演示效果
                </th>
                <th>
                    使用场景
                </th>
            </tr>
            <tr>
                <td>
                    时间功能(%h:%m:%s）
                </td>
                <td>
                    <cc1:DateTimeTextBox runat="server" ID="DateTimeTextBox1" DateTimeFormat="%h:%m"></cc1:DateTimeTextBox>
                    <cc1:DateTimeTextBox runat="server" ID="DateTimeTextBox5" DateTimeFormat="%h:%m"></cc1:DateTimeTextBox>
                </td>
                <td>
                    如：时间
                </td>
            </tr>
            <tr>
                <td>
                    日期功能(默认特性：%Y-%M-%D）
                </td>
                <td>
                    <cc1:DateTimeTextBox runat="server" ID="date1"></cc1:DateTimeTextBox>
                </td>
                <td>
                    如：生日
                </td>
            </tr>
            <tr>
                <td>
                    时间功能（%Y-%M-%D %h:%m:%s）
                </td>
                <td>
                    <cc1:DateTimeTextBox runat="server" ID="DateTimeTextBox2" DateTimeFormat="%Y-%M-%D %h:%m:%s"></cc1:DateTimeTextBox>
                </td>
                <td>
                    如：考试开始时间，精确到时分秒
                </td>
            </tr>
            <tr>
                <td>
                    同时设置开始、结束时间功能（%Y-%M-%D %h:%m:%s）
                </td>
                <td>
                    <cc1:DateTimeTextBox runat="server" ID="DateTimeTextBox3" DateTimeFormat="%Y-%M-%D %h:%m:%s"
                        EndTimeControlID="DateTimeTextBox4"></cc1:DateTimeTextBox>
                    <cc1:DateTimeTextBox runat="server" ID="DateTimeTextBox4" DateTimeFormat="%Y-%M-%D %h:%m:%s"
                        BeginTimeControlID="DateTimeTextBox3"></cc1:DateTimeTextBox>
                </td>
                <td>
                    如：考试开始、结束时间，精确到时分秒
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
