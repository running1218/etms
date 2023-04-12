<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GroupInfo.ascx.cs" Inherits="LearningManagement_GroupManager_Controls_GroupInfo" %>

<!--表单录入-->
<div class="dv_information">
    <table class="GridviewGray">
        <tr>
            <th style="width: 20%">
                群组名称：
            </th>
            <td colspan="3" style="width: 80%">
                <asp:TextBox id="txtGroupName" runat="server" CssClass="inputbox_210" MaxLength="20"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>
                群组说明：
            </th>
            <td colspan="3">
               <asp:TextBox TextMode="MultiLine" CssClass="inputbox_area300" runat="server" ID="txtGroupDescription" MaxLength="500" />                             
            </td>
        </tr>
    </table>
</div>
<!--提交表单-->
<div class="dv_submit">
    <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="btn_Save" OnClick="btnSave_Click" />
    <%--<input type="button" class="btn_Save" value="保存" onclick="javascript:popSuccessMsg('恭喜你保存成功！','提示',closeWindow);" />--%>
    <input type="button" class="btn_Cancel" value="取消" onclick="javascript:closeWindow();" />
</div>
