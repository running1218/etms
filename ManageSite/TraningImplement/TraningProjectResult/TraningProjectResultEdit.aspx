<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true"
    CodeFile="TraningProjectResultEdit.aspx.cs" Inherits="TraningImplement_TraningProjectResult_TraningProjectResultEdit" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--功能标题-->
    <h2 class="dv_title">
        培训项目实施结果管理
    </h2>
    <!--表单录入-->
    <div class="dv_information">
        <table class="GridviewGray padtable fixedTable">
            <tr>
                <th >
                    项目编码：
                </th>
                <td colspan="3">
                    <asp:Label ID="Lbl_ItemCode" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    项目名称：
                </th>
                <td colspan="3">
                    <asp:Label ID="Lbl_ItemName" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
        <table class="GridviewGray padtable fixedTable">
            <tr>
                <th >
                    项目状态：
                </th>
                <td >
                    <cc1:DictionaryLabel ID="dlblProjectType" DictionaryType="Dic_TraningProjectType"
                        runat="server" />
                </td>
                <th >
                    专业类别：
                </th>
                <td >
                    <cc1:DictionaryLabel ID="dlblSpecialtyType" DictionaryType="Dic_Sys_SpecialtyType"
                        runat="server" />
                </td>
            </tr>
        </table>
        <table class="GridviewGray padtable fixedTable">
            <tr>
                <th>
                    项目周期：
                </th>
                <td colspan="3">
                    <asp:Label ID="lblItemDate" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    归档方式：
                </th>
                <td colspan="3">
                    <asp:DropDownList ID="ddl_ItemEndMode" runat="server"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th>
                    归档备注：
                </th>
                <td colspan="3">
                    <asp:TextBox ID="txtItemEndReMark" TextMode="MultiLine" runat="server" CssClass="inputbox_area300"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    <div class="dv_submit">
        <cc1:CustomButton runat="server" ID="btnFile" Text="项目归档" CssClass="btn_Pache"
            EnableConfirm="true" ConfirmTitle="提示" ConfirmMessage="确定归档吗？" 
            onclick="btnFile_Click" />
        <input type="button" class="btn_Cancel" value="取消" onclick="javascript:closeWindow();" /></div>
</asp:Content>
