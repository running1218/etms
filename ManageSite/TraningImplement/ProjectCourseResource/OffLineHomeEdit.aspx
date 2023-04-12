<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true" CodeFile="OffLineHomeEdit.aspx.cs" Inherits="TraningImplement_ProjectCourseResource_OffLineHomeEdit" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--表单录入-->
    <div class="dv_information">
        <table class="GridviewGray">
            <tr>
                <th width="20%">
                    作业名称：
                </th>
                <td width="80%">
                    离线作业测试1
                </td>
            </tr>
            <tr>
                <th>
                    是否启用：
                </th>
                <td>
                    <cc1:dictionaryradiobuttonlist runat="server" id="DictionaryRadioButtonList1" dictionarytype="Dic_TrueOrFalse" />
                </td>
            </tr>
            <tr>
                <th>
                    开始时间：
                </th>
                <td>
                    <cc1:DateTimeTextBox ID="DateTimeTextBox1" runat="server"></cc1:DateTimeTextBox>
                </td>
            </tr>
            <tr>
                <th>
                    结束时间：
                </th>
                <td>
                    <cc1:DateTimeTextBox ID="DateTimeTextBox2" runat="server"></cc1:DateTimeTextBox>
                </td>
            </tr>
        </table>
    </div>
    <div class="dv_submit">
        <input type="button" class="btn_Save" value="保存" onclick="javascript:popSuccessMsg('恭喜你保存成功！','提示',closeWindow);" />
        <input type="button" class="btn_Cancel" value="取消" onclick="javascript:closeWindow();" /></div>
</asp:Content>

