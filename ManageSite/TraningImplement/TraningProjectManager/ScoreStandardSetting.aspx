<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true"
    CodeFile="ScoreStandardSetting.aspx.cs" Inherits="TraningImplement_TraningProjectManager_ScoreStandardSetting" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--表单录入-->
    <div class="dv_information">
        <table class="GridviewGray">
            <tr>
                <th>
                    项目编码：
                </th>
                <td>
                    <asp:Label ID="lbl_ItemCode" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    项目名称：
                </th>
                <td>
                    <asp:Label ID="lbl_ItemName" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th rowspan="3">
                    考核标准：
                </th>
                <td>
                    <asp:CheckBox ID="ckbCourse" runat="server" Text="课程学习" CssClass="standard-type" />
                    <cc1:CustomTextBox ID="txtCourse" ContentType="Number" MaxLength="3" runat="server" CssClass="standard-rata"></cc1:CustomTextBox>%
                </td>
            </tr>  
            <tr>
                <td>
                     <asp:CheckBox ID="ckbTesting" runat="server" Text="在线测试" CssClass="standard-type" />
                    <cc1:CustomTextBox ID="txtTesting" ContentType="Number" MaxLength="3" runat="server" CssClass="standard-rata"></cc1:CustomTextBox>%
                </td>
            </tr>    
            <tr>
                <td>
                     <asp:CheckBox ID="ckbActual" runat="server" Text="在线作业" CssClass="standard-type" />
                    <cc1:CustomTextBox ID="txtActual" ContentType="Number" MaxLength="3" runat="server" CssClass="standard-rata"></cc1:CustomTextBox>%
                </td>
            </tr>      
        </table>
    </div>
    <div class="dv_submit">
        <asp:Button ID="Btn_Save" CssClass="btn_Save" runat="server" Text="保存" OnClick="Btn_Save_Click"
            ValidationGroup="Saves" />
        <asp:ValidationSummary runat="server" ID="ValidationSummary1" ValidationGroup="Saves"
            ShowMessageBox="true" ShowSummary="false" />
        <input type="button" class="btn_Cancel" value="取消" onclick="javascript:closeWindow();" />
    </div>
</asp:Content>
