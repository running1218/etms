<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ActionHref.aspx.cs" Inherits="Example_ActionHref" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>
            必要条件：引入命名控件 using ETMS.Utility
        </h1>
        <table>
            <thead>
                <center>
                    <h1>
                        aspx页面或ascx控件内输出url方式：
                    </h1>
                </center>
            </thead>
            <tr>
                <td>
                    功能
                </td>
                <td>
                    效果
                </td>
                <td>
                    使用方式
                </td>
            </tr>
            <tr>
                <td>
                    1、仅传递path
                </td>
                <td>
                    <a href='<%=this.ActionHref("~/Example/ActionHref.aspx") %>'>
                        <%=this.ActionHref("~/Example/ActionHref.aspx") %></a>
                </td>
                <td>
                    <pre>this.ActionHref("~/Example/ActionHref.aspx")</pre>
                </td>
            </tr>
            <tr>
                <td>
                    2、pathAndQueryString
                </td>
                <td>
                    <a href='<%=this.ActionHref("~/Example/ActionHref.aspx?a=1234&b=1222") %>'>
                        <%=this.ActionHref("~/Example/ActionHref.aspx?a=1234&b=1222") %></a>
                </td>
                <td>
                    <pre>this.ActionHref("~/Example/ActionHref.aspx?a=1234&b=1222")</pre>
                </td>
            </tr>
            <tr>
                <td>
                    3、pathAndRouteValues（匿名类传递url参数）
                </td>
                <td>
                    <a href='<%=this.ActionHref("~/Example/ActionHref.aspx",new {a="1234",b="1222"}) %>'>
                        <%=this.ActionHref("~/Example/ActionHref.aspx",new {a="1234",b="1222"}) %></a>
                </td>
                <td>
                    <pre>this.ActionHref("~/Example/ActionHref.aspx",new {a="1234",b="1222"}) </pre>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
