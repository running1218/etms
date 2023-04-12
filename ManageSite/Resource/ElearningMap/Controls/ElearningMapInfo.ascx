<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ElearningMapInfo.ascx.cs"
    Inherits="Resource_ElearningMap_Controls_ElearningMapInfo" %>
<%@ Register Assembly="ETMS.Editor" Namespace="ETMS.Editor.UEditor" TagPrefix="wuc" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<!--表单录入-->
<div class="dv_information">
    <table class="GridviewGray th120">
        <tr id="trStudyMapCode" runat="server">
            <th >
                学习地图编码：
            </th>
            <td>
                <asp:Literal ID="ltlStudyMapCode" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <th>
                学习地图名称：
            </th>
            <td>
                <asp:TextBox ID="txtStudyMapName" runat="server" CssClass="inputbox_300" MaxLength="50"></asp:TextBox><font color="red">*</font><asp:RequiredFieldValidator ID="RequiredFieldValidatorType"
                    runat="server" ControlToValidate="txtStudyMapName" Display="None" ErrorMessage="请输入学习地图名称！"
                    ValidationGroup="Error"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr class="hide">
            <th>
                学习地图类型：
            </th>
            <td>
                <cc1:DictionaryDropDownList runat="server" ID="ddlElearningMapType" DictionaryType="Dic_Sys_ELearningMapType" CssClass="select_190" 
                    IsShowChoose="false" IsShowAll="false" /><font color="red">*</font><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                    runat="server" ControlToValidate="ddlElearningMapType" Display="None" ErrorMessage="请选择学习地图类型！"
                    ValidationGroup="Error" ></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr id="trDepartment" runat="server">
            <th>
                <asp:Literal ID="ltlDepartment" runat="server" Text="<%$ Resources:UIResource, ui_department%>"></asp:Literal>：
            </th>
            <td>
                <cc1:DictionaryDropDownList runat="server" ID="ddlDepartment" DictionaryType="Site_DepartmentByOrgID"
                    IsShowChoose="true" CssClass="select_190"/>
            </td>
        </tr>
        <tr id="trPost" runat="server">
            <th>
                <asp:Literal ID="ltlPosition" runat="server" Text="<%$ Resources:UIResource, ui_position%>"></asp:Literal>：
            </th>
            <td>
                <cc1:DictionaryDropDownList runat="server" ID="ddlPost" DictionaryType="Dic_PostByOrgID"
                    IsShowChoose="true" CssClass="select_190" />
            </td>
        </tr>
        <tr>
            <th>
                <asp:Literal ID="ltlRank" runat="server" Text="<%$ Resources:UIResource, ui_rank%>"></asp:Literal>：
            </th>
            <td>
                <cc1:DictionaryDropDownList runat="server" ID="ddlRank" DictionaryType="vw_Dic_Sys_Rank"
                    IsShowChoose="true"  CssClass="select_190"/>
            </td>
        </tr>
        <tr>
            <th>
                能力描述：
            </th>
            <td>
                <wuc:UEditor ID="txtStudyMapDesc" runat="server" Width="400px" Height="150" ToolType="Basic">
                </wuc:UEditor>
            </td>
        </tr>
    </table>
</div>
<!--提交表单-->
<div class="dv_submit">
    <asp:LinkButton ID="lbtnSave" runat="server" CssClass="btn_Save" OnClick="lbtnSave_Click"
        ValidationGroup="Error">保存</asp:LinkButton>
    <asp:ValidationSummary runat="server" ID="ValidationSummary1" ValidationGroup="Error"
        ShowMessageBox="true" ShowSummary="false" />
    <a href="javascript:closeWindow();" class="btn_Cancel">取消</a>
</div>
