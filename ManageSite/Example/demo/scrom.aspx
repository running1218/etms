<%@ Page Language="C#" AutoEventWireup="true" CodeFile="scrom.aspx.cs" Inherits="demo_scrom" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <iframe src="top.aspx" frameborder="0" scrolling="no" height="90" width="100%" name="API"
        id="Iframe1"></iframe>
    <div class="dv_allExercise" id="dv_main">
        <!--网站左边-->
        <div class="dv_left dv_lefttree" id="dv_left">
            <a href="javascript:expandTree2();" onfocus="this.blur()" title="隐藏左边菜单" class="expandFrame">
                <span class="hide">隐藏</span></a>
            <div class="dv_MenuTitle">
            </div>
            <div class="tree">
                <asp:TreeView runat="server" ID="tvChapterList" Width="97%" SelectedNodeStyle-Font-Bold="false"
                    SelectedNodeStyle-BorderColor="white" NodeIndent="10" ShowLines="true" LineImagesFolder="~/TreeLineImages"
                    EnableClientScript="true">
                    <RootNodeStyle ForeColor="#FDCA1A" />
                    <ParentNodeStyle Font-Bold="False"></ParentNodeStyle>
                    <HoverNodeStyle BackColor="#7E6B5A" ForeColor="#FDCA1A"></HoverNodeStyle>
                    <NodeStyle NodeSpacing="2px" VerticalPadding="2px" HorizontalPadding="2px" Font-Names="宋体"
                        Font-Size="12px" ForeColor="White"></NodeStyle>
                    <SelectedNodeStyle BorderColor="White" Font-Bold="True" />
                </asp:TreeView>
            </div>
        </div>
        <!--主体右边-->
        <div class="dv_right" id="dv_right" style="position: relative">
            <a class="hideFrame" style="display: none" onfocus="this.blur()" title="显示左边菜单" href="javascript:expandTree2();">
                <span class="hide">显示</span></a>
            <iframe src="right.aspx" name="rightFrame" id="rightFrame" frameborder="0" title="rightFrame"
                height="760px" scrolling="yes" width="100%" style="overflow:auto;overflow-x:hidden;"></iframe>
        </div>
        <div class="copyright" id="copyright">
            <%= ETMS.Utility.WebUtility.CopyRight %></div>
    </div>
    </form>
</body>
</html>
