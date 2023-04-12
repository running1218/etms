<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="UpFile3.aspx.cs" Inherits="Resource_CoursewareManage_UpFile3" %>
    <%@ Register Src="~/Controls/UpFile.ascx" TagPrefix="uc1" TagName="UpFile" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <script src="../../JScript/ymPromptYuan.js" type="text/javascript"></script>
    <script src="../../JScript/JSFunction.js" type="text/javascript"></script>
</head>
<body>
<form runat="server">
    <div class="dv_information" style="text-align: center; margin:4px; height:240px">
        <uc1:UpFile runat="server" ID="UpFile" FunctionType="ScormUp" CallBack="callbacktest" />
        <asp:HiddenField ID="hfFtpScromAllowType" runat="server" />
    </div>
    <div style="position: absolute; bottom: 60px; left: 10px; text-align: left">
        <span id="spMsg" runat="server" style="width: 400px; display: block; word-break: break-all;
            white-space: normal"></span>
    </div>             
    <div style="text-align: center;" class="dv_submit">
        <asp:Button ID="btn_Save" runat="server" Text="保存" CssClass="btn_Save" OnClick="btn_Save_Click"  />
        <input type="button" value="关闭" id="btnClose" class="btn_Cancel" onclick="javascript:closeWindow();" style="left: 280px; top: 1px" />
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=spMsg.ClientID %>").html("<span class='colorRed'>提示：</span>上传格式只能为：" + $("#<%= hfFtpScromAllowType.ClientID %>").val());
        });

        function getFileType(obj) {
            var pos = obj.lastIndexOf(".") * 1;
            return obj.substring(pos + 1);
        }

        //上传成功
        function UpFileSuccess(fileName) {
            $(window.parent.document).find(".lblFileName").html(fileName);
            $(window.parent.document).find(".lblState").html("<span class='colorGreen'>已上传，未导入。</span>");
            $(window.parent.document).find(".txtFileName").val(fileName);
            closeWindow('window.parent.triggerRefreshEvent');
        }

        //上传失败
        function UpFileError() {
            $(window.parent.document).find(".lblState").html("<span class='colorRed'>未上传文件！</span>");
            closeWindow();
        }        
    </script>
    </form>
</body>
</html>
