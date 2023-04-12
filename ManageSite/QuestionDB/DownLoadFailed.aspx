<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DownLoadFailed.aspx.cs" Inherits="QuestionDB_DownLoadFailed" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .Tab
        {
            border-collapse: collapse;
            width: 300px;
            height: 300px;
        }
        .Tab td
        {
            border: solid 1px black;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table cellspacing="0" class="Tab" style="width: 70%;">
            <tr>
                <td>
                    题号
                </td>
                <td>
                    题目
                </td>
                <td>
                    难易度
                </td>
                <td>
                    答案
                </td>
                <td>
                    选项
                </td>
                <td>
                    选项内容
                </td>
                <td>
                    状态
                </td>
                <td>
                    描述
                </td>
            </tr>
            <%=StrContent%>
        </table>
    </div>
    </form>
</body>
</html>
