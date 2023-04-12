<%@ Page Language="C#" AutoEventWireup="true" Inherits="Admin_User_FunctionGroup_View"
    MasterPageFile="~/MasterPages/MPageTree.Master" CodeFile="View.aspx.cs" %>

<asp:Content runat="server" ID="PageContent" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="dv_information">
        <div class="dv_searchlist" style="width: 95%">
            <div class="dv_treeView" style="height: auto!important; min-height: 350px; height: 350px;">
                <!--成员分级列表 -->
                <asp:TreeView runat="server" ID="lsMember" Width="96%" ShowCheckBoxes="All" ShowLines="true" Enabled="false"
                    NodeStyle-ForeColor="#313131" SelectedNodeStyle-BorderColor="black" SelectedNodeStyle-ForeColor="Red">
                </asp:TreeView>
            </div>
        </div>
    </div>
    <!--列表 end-->
    <div class="dv_submit">
        <asp:Button ID="btnReturn" runat="server" SkinID="Return" Text="关闭" OnClientClick="closeWindow()" />
    </div>
    <script language="javascript" type="text/javascript">
        function public_GetParentByTagName(element, tagName) {
            var parent = element.parentNode;
            var upperTagName = tagName.toUpperCase();
            //如果这个元素还不是想要的tag就继续上溯
            while (parent && (parent.tagName.toUpperCase() != upperTagName)) {
                parent = parent.parentNode ? parent.parentNode : parent.parentElement;
            }
            return parent;
        }
        //设置节点的父节点Cheched——该节点可访问，则他的父节点也必能访问
        function setParentChecked(objNode) {
            var objParentDiv = public_GetParentByTagName(objNode, "div");
            if (objParentDiv == null || objParentDiv == "undefined") {
                return;
            }
            var objID = objParentDiv.getAttribute("ID");
            objID = objID.substring(0, objID.indexOf("Nodes"));
            objID = objID + "CheckBox";
            var objParentCheckBox = document.getElementById(objID);
            if (objParentCheckBox == null || objParentCheckBox == "undefined") {
                return;
            }
            if (objParentCheckBox.tagName != "INPUT" && objParentCheckBox.type == "checkbox")
                return;
            objParentCheckBox.checked = true;
            setParentChecked(objParentCheckBox);
        }
        //设置节点的子节点uncheched——该节点不可访问，则他的子节点也不能访问
        function setChildUnChecked(divID) {
            var objchild = divID.children;
            var count = objchild.length;
            for (var i = 0; i < objchild.length; i++) {
                var tempObj = objchild[i];
                if (tempObj.tagName == "INPUT" && tempObj.type == "checkbox") {
                    tempObj.checked = false;
                }
                setChildUnChecked(tempObj);
            }
        }
        //设置节点的子节点cheched——该节点可以访问，则他的子节点也都能访问
        function setChildChecked(divID) {
            var objchild = divID.children;
            var count = objchild.length;
            for (var i = 0; i < objchild.length; i++) {
                var tempObj = objchild[i];
                if (tempObj.tagName == "INPUT" && tempObj.type == "checkbox") {
                    tempObj.checked = true;
                }
                setChildChecked(tempObj);
            }
        }
        //触发事件
        function CheckEvent() {
            var objNode = event.srcElement;
            if (objNode.tagName != "INPUT" || objNode.type != "checkbox")
                return;
            if (objNode.checked == true) {
                setParentChecked(objNode);
                var objID = objNode.getAttribute("ID");
                var objID = objID.substring(0, objID.indexOf("CheckBox"));
                var objParentDiv = document.getElementById(objID + "Nodes");
                if (objParentDiv == null || objParentDiv == "undefined") {
                    return;
                }
                setChildChecked(objParentDiv);
            }
            else {
                var objID = objNode.getAttribute("ID");
                var objID = objID.substring(0, objID.indexOf("CheckBox"));
                var objParentDiv = document.getElementById(objID + "Nodes");
                if (objParentDiv == null || objParentDiv == "undefined") {
                    return;
                }
                setChildUnChecked(objParentDiv);
            }
        }

        //设置节点的子节点uncheched——该节点不可访问，则他的子节点也不能访问
        function setChildDisabled(divID) {
            var objchild = divID.children;
            var count = objchild.length;
            for (var i = 0; i < objchild.length; i++) {
                var tempObj = objchild[i];
                if (tempObj.tagName == "INPUT" && tempObj.type == "checkbox") {
                    if (tempObj.checked) tempObj.disabled = true;
                }
                setChildDisabled(tempObj);
            }
        }
        //自动设置
        //setChildDisabled(document.getElementById("dv_treeView"));
    </script>
</asp:Content>
