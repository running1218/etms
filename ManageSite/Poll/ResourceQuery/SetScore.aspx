<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true"
    CodeFile="SetScore.aspx.cs" Inherits="Poll_ResourceQuery_SetScore" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="dv_information">
        <table class="GridviewGray">
            <tr>
                <th style="width: 100px">
                    调查名称：
                </th>
                <td>
                    <asp:Label ID="lblQueryName" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    调查时间：
                </th>
                <td>
                    <cc1:DateTimeLabel ID="lblBeginTime" runat="server" />至<cc1:DateTimeLabel ID="lblEndTime"
                        runat="server" />
                </td>
            </tr>
            <tr>
                <th>
                    负责人：
                </th>
                <td>
                    <asp:Label runat="server" ID="lblDutyUser"></asp:Label>
                </td>
            </tr>
             <tr>
                <th>
                    调查项目：
                </th>
                <td>
                    <asp:Label runat="server" ID="lblItem"></asp:Label>
                </td>
            </tr>
             <tr>
                <th>
                    调查课程：
                </th>
                <td>
                    <asp:Label runat="server" ID="lblCourse"></asp:Label>
                </td>
            </tr>
             <tr>
                <th>
                    调查讲师：
                </th>
                <td>
                    <asp:Label runat="server" ID="lblTeacher"></asp:Label>
                </td>
            </tr>
            <tr>
                <th style="width: 100px">
                    综合分数：
                </th>
                <td>
                    <cc1:CustomTextBox ContentType="Number" runat="server" ID="txtScore" CssClass="inputbox_90" />
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
