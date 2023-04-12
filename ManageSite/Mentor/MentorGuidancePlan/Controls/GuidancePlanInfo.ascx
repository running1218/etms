<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GuidancePlanInfo.ascx.cs"
    Inherits="Mentor_MentorGuidancePlan_Controls_GuidancePlanInfo" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<div class="dv_information">
    <table class="GridviewGray">
        <tr>
            <th>
                辅导计划标题：
            </th>
            <td>
                <input type="text" name="textfield" class="inputbox_210" />
            </td>
        </tr>
        <tr>
            <th>
                任务描述：
            </th>
            <td>
                <input type="text" name="textfield" class="inputbox_area300" />
            </td>
        </tr>
        <tr>
            <th>
                辅导期间：
            </th>
            <td>
                <cc1:datetimetextbox id="DateTimeTextBox1" runat="server" endtimecontrolid="DateTimeTextBox2"></cc1:datetimetextbox>
                至
                <cc1:datetimetextbox id="DateTimeTextBox2" runat="server" begintimecontrolid="DateTimeTextBox1"></cc1:datetimetextbox>
            </td>
        </tr>
    </table>
</div>
<div class="dv_submit">
    <input type="button" class="btn_Save" value="保存" onclick="javascript:popSuccessMsg('恭喜你保存成功！','提示',closeWindow);" />
    <input type="button" class="btn_Cancel" value="取消" onclick="javascript:closeWindow();" /></div>
