<%@ Page Language="C#" AutoEventWireup="true" Inherits="Notice" Codebehind="Notice.aspx.cs" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="App_Themes/ThemeDefault/media.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="notice-detail-instance">
            <div class="notice-detail-title">
                <div>
                    <asp:Literal ID="ltlTitle" runat="server"></asp:Literal>
                </div>
                <div class="notice-read-num">
                    <span class="notice-detail-time"><asp:Literal ID="ltlTime" runat="server"></asp:Literal></span>
                    <span class="read-num">阅读：&nbsp;<asp:Literal ID="ltlReadNum" runat="server"></asp:Literal></span>                    
                </div>
            </div>
            <div class="notice-detail-content">
                <asp:Literal ID="ltlContent" runat="server"></asp:Literal>
            </div>            
        </div>      
    </form>
</body>
</html>
