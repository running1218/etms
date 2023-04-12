<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true" CodeFile="SetLeader.aspx.cs" Inherits="LearningManagement_GroupManager_SetLeader" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<!--功能标题-->
    <h2 class="dv_title">
        设置班委
    </h2>
    <div class="dv_information">
        <table class="GridviewGray">            
            <tr>
                <th>
                    设置队长：
                </th>
                <td>                                      
                    <asp:CheckBox ID="chkIsLeader" runat="server" Text="设为队长"/>
                </td>
            </tr>
        </table>
    </div>
    <div class="dv_submit">
        <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" class="btn_Save" />
        <input type="button" class="btn_Cancel" value="取消" onclick="javascript:closeWindow();" /></div>
</asp:Content>

