<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="Left.aspx.cs"
    Inherits="Scorm_Left" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>章节列表</title>
    <script language="javascript" src="API/jquery.js" type="text/javascript"></script>
    <script language="javascript" src="API/AutoFrameHeight.js" type="text/javascript"></script>
</head>
<script language="javascript" type="text/javascript">
    var timer;
    var resID;
    var resLocation;

    function SendResourceId(id, resourceLocation, titleName, itemID) {
        var topframe = window.parent.parent.document.getElementById("topFrame");
        resID = id;
        resLocation = resourceLocation;
        window.parent.parent.document.getElementById("topFrame").contentWindow.document.getElementById("resId").value = resID;
        window.open(resLocation, 'rightFrame');
    }
</script>
<body class="dv_MenuTree">
    <form id="form1" runat="server">
    <div  id="dv_left">
        <div class="dv_MenuTitle">
        </div>
        <div class="tree" id="tree">
             <asp:TreeView runat="server" ID="tvChapterList" CssClass="tree-object" NodeIndent="10" ShowLines="true" LineImagesFolder="~/TreeLineImages"
                EnableClientScript="true">
                <RootNodeStyle CssClass="tree-root-node" />
                <ParentNodeStyle CssClass="tree-parent-node" ></ParentNodeStyle>
                <HoverNodeStyle CssClass="tree-hover-node"></HoverNodeStyle>
                <NodeStyle CssClass="tree-node"></NodeStyle>
                <SelectedNodeStyle CssClass="tree-selected-node" />
            </asp:TreeView>
        </div>
    </div>
    </form>
</body>
</html>
