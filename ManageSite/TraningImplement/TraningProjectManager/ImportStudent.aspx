<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true"
    CodeFile="ImportStudent.aspx.cs" Inherits="TraningImplement_TraningProjectManager_ImportStudent" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/MiniUpFile.ascx" TagName="uploader" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--功能标题-->
    <h2 class="dv_title">
        导入学员
    </h2>
    <!--表单录入-->
    <div class="dv_information">
        <table class="GridviewGray">
            <tr>
                <th width="30%">
                    项目编码：
                </th>
                <td>
                    <asp:Label ID="lblItemCode" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    项目名称：
                </th>
                <td>
                    <asp:Label ID="lblItemName" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    项目周期：
                </th>
                <td>
                    <asp:Label ID="lblItemTime" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    导入模板：
                </th>
                <td>
                    <%--<a href="Template/导入培训项目学员模板.xls" runat="server" onclick="btn_Export_Click">下载导入模板</a>--%>
                    <asp:LinkButton ID="lbtnExport" runat="server" Text="下载导入模板" OnClick="btn_Export_Click" />
                </td>
            </tr>
            <tr>
                <th>
                    选择导入文件：
                </th>
                <td>                   
                    <uc:uploader ID="uploader" runat="server" FunctionType="ImportStudentInfo" />
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
        <input type="button" class="btn_Cancel" value="取消" onclick="javascript:closeWindow();" /></div>
    <div style="display:none">
        <asp:HiddenField ID="hfOrganizationID" runat="server" />
    </div>
</asp:Content>
