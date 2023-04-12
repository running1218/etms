<%@ Page Title="设置班委" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master"
    AutoEventWireup="true" CodeFile="SetsClassCommittee.aspx.cs" Inherits="LearningManagement_ClassManager_SetsClassCommittee" %>
    
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--功能标题-->
    <h2 class="dv_title">
        设置班委
    </h2>
    <div class="dv_information">
        <table class="GridviewGray">
            <tr>
                <th>
                    班级名称：
                </th>
                <td>
                   <asp:Literal ID="lblClassName" runat="server" />
                </td>
            </tr>
            <tr>
                <th>
                    选择学员：
                </th>
                <td>
                    <asp:Literal ID="lblRealName" runat="server" />
                </td>
            </tr>
            <tr>
                <th>
                    设置班委：
                </th>
                <td>                                      
                    <asp:CheckBoxList ID="chkLeadStyle" runat="server"  RepeatDirection="Horizontal" CssClass="noborder"/>
                </td>
            </tr>
            <tr>
                <th>
                    设置版主：
                </th>
                <td>                                      
                    <asp:CheckBox ID="chkIsBanboo" runat="server" Text="设为版主"/>
                </td>
            </tr>
        </table>
    </div>
    <div class="dv_submit">
        <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" class="btn_Save" />
        <input type="button" class="btn_Cancel" value="取消" onclick="javascript:closeWindow();" /></div>
</asp:Content>
