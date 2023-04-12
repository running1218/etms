<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/MPagePop.Master"
    CodeFile="ImportStudent.aspx.cs" Inherits="QuestionDB_ImportStudent" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/MiniUpFile.ascx" TagName="uploader" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function CloseRefesh() {
            //window.close();
            closeWindow();
            window.parent.location.href = window.parent.location.href;
        }
        $(function () {
            $(parent.document).find(".ymPrompt_close").bind("click", function () {
                CloseRefesh();
            })
        })
    </script>
    <!--功能标题-->
    <h2 class="dv_title">
        导入试题
    </h2>
    <!--表单录入-->
    <div class="dv_information">
        <table class="GridviewGray">
            <tr>
                <th>
                    课程编码：
                </th>
                <td>
                    <asp:Label ID="lblItemCode" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    课程名称：
                </th>
                <td>
                    <asp:Label ID="lblItemName" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    导入模板：
                </th>
                <td>
                    <a href="Template/<%=Name %>">下载导入模板</a>
                    <%--<a href="Template/导入培训项目学员模板.xls" runat="server" onclick="btn_Export_Click">下载导入模板</a>--%>
                    <%-- <asp:LinkButton ID="lbtnExport" runat="server" Text="下载导入模板" OnClick="btn_Export_Click" />--%>
                </td>
            </tr>
            <tr>
                <th>
                    选择导入文件：
                </th>
                <td>
                    <asp:Label ID="lblFileInfo" runat="server" Text="支持Excel文件" /><br />
                    <%--<cc1:FileUpload runat="server" ID="fileUpload1" FunctionType="ImportStudentInfo" />--%>
                    <uc:uploader ID="uploader" runat="server" FunctionType="ImportStudentInfo" FileTypeIsDisplay="false" />
                </td>
            </tr>
            <tr>
                <th>
                    说&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;明：
                </th>
                <td>
                    <asp:Label ID="lblRemark" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div class="dv_submit">
        <asp:Button ID="btnImport" runat="server" Text="导入" CssClass="btn_Import" OnClick="btnImport_Click"
            ValidationGroup="Edit" />
        <input type="button" class="btn_Cancel" value="取消" runat="server" id="btnClose" onserverclick="CloseWindow" /></div>
    <div style="display: none">
        <asp:HiddenField ID="hfOrganizationID" runat="server" />
    </div>
</asp:Content>
