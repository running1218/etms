<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CourseResourceInfo.ascx.cs"
    Inherits="TraningImplement_TraningProjectManager_Controls_CourseResourceInfo" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<!--表单录入-->
<div class="dv_information">
    <table class="GridviewGray">
        <tr>
            <th>
                资源名称
            </th>
            <td colspan="3">
                <input type="text" name="textfield4" class="inputbox_210" />
            </td>
        </tr>
        <tr>
            <th>
                资源类型
            </th>
            <td>
                <cc1:DictionaryDropDownList runat="server" ID="MasterheadModeType1" DictionaryType="Dic_RegistrationModeType"
                    IsShowChoose="true" />
            </td>
            <th>
                状 态
            </th>
            <td>
                <cc1:DictionaryDropDownList runat="server" ID="MasterheadModeType2" DictionaryType="Dic_RegistrationModeType"
                    IsShowChoose="true" />
            </td>
        </tr>
        <tr>
            <th>
                开始时间
            </th>
            <td>
                <input type="text" name="textfield4" class="inputbox_120" />
            </td>
            <th>
                结束时间
            </th>
            <td>
                <input type="text" name="textfield4" class="inputbox_120" />
            </td>
        </tr>
        <tr>
            <th>
                设置源资源
            </th>
            <td>
                <input type="text" name="textfield4" class="inputbox_120" />
                <input class="btn_Search" type="button" value="查询"/>
            </td>
            <td colspan="3">
                <cc1:DictionaryDropDownList runat="server" ID="Dictionarydropdownlist1" DictionaryType="Dic_RegistrationModeType"
                    IsShowChoose="true" />
            </td>
        </tr>
    </table>
</div>
<div class="dv_submit">
    <input type="button" class="btn_Save" value="保存" onclick="javascript:popSuccessMsg('恭喜你保存成功！','提示',closeWindow);" />
    <input type="button" class="btn_Cancel" value="取消" onclick="javascript:closeWindow();" /></div>
