<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GuidanceInfo.ascx.cs"
    Inherits="QuestionDB_GuidanceManager_Controls_GuidanceInfo" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Assembly="ETMS.Editor" Namespace="ETMS.Editor.UEditor" TagPrefix="wuc" %>
<div class="dv_information">
    <table class="GridviewGray">
        <tr>
            <th>
                培训项目：
            </th>
            <td colspan="3">
                <cc1:DictionaryDropDownList runat="server" ID="DictionaryDropDownList1" DictionaryType="Dic_ProjectList"
                    IsShowChoose="true" />
            </td>
        </tr>
        <tr>
            <th>
                所属课程：
            </th>
            <td>
                <cc1:DictionaryDropDownList runat="server" ID="DictionaryDropDownList2" DictionaryType=""
                    IsShowAll="true" />
            </td>
            <th>
                资料类型：
            </th>
            <td>
                <cc1:DictionaryDropDownList runat="server" ID="DictionaryDropDownList3" DictionaryType="Dic_GuidanceManagerType"
                    IsShowChoose="true" />
            </td>
        </tr>
        <tr>
            <th>
                状&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;态：
            </th>
            <td>
                <cc1:DictionaryDropDownList runat="server" ID="DictionaryDropDownList4" DictionaryType="Dic_GuidanceManagerState"
                    IsShowAll="true" />
            </td>
            <th>
                创建时间：
            </th>
            <td>
                2012-03-19
            </td>
        </tr>
        <tr>
            <th>
                资料名称：
            </th>
            <td colspan="3">
                <asp:TextBox ID="TextBox1" runat="server" CssClass="inputbox_210"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>
                资料内容：
            </th>
            <td colspan="3">
                <wuc:UEditor ID="fckEditor" runat="server" Width="420px" ToolType="Basic">
                </wuc:UEditor>
            </td>
        </tr>
    </table>
</div>
<div class="dv_submit">
    <input type="button" class="btn_Save" value="保存" onclick="javascript:popSuccessMsg('恭喜你保存成功！','提示',closeWindow);" />
    <input type="button" class="btn_Cancel" value="取消" onclick="javascript:closeWindow();" /></div>
