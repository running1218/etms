<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true"
    CodeFile="UploadReport.aspx.cs" Inherits="Poll_ResourceQuery_UploadReport" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="dv_information">
        <table class="GridviewGray">
            <tr>
                <th style="width:100px">
                    上传分析报告：
                </th>
                <td>
                     <cc1:FileUpload runat="server" id="fileUpload1" FunctionType="PollReport" />
                </td>
            </tr>
        </table>
    </div>
    <!--提交表单-->
    <div class="dv_submit">
        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn_Save" ValidationGroup="Edit"
            OnClick="LinkButton1_Click">保存</asp:LinkButton>
        <a href="javascript:closeWindow();" class="btn_Cancel padleft10">取消</a>
    </div>
</asp:Content>
