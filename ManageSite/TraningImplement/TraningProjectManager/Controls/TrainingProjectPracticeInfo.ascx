<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TrainingProjectPracticeInfo.ascx.cs" Inherits="TraningImplement_TraningProjectManager_Controls_TrainingProjectPracticeInfo" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Assembly="ETMS.Editor" Namespace="ETMS.Editor.UEditor" TagPrefix="wuc" %>
<%@ Register Src="~/Controls/ChooseCourseDropdown.ascx" TagName="ChooseCourseDropdown" TagPrefix="uc1" %>
<!--功能标题-->
<h2 class="dv_title">实践基本信息
</h2>
<!--表单录入-->
<div class="dv_information">
    <table class="GridviewGray fixedTable">
        <tr>
            <th>所属项目：
            </th>
            <td>
                <asp:UpdatePanel runat="server" ID="up1">
                    <ContentTemplate>
                <asp:DropDownList ID="ddl_Item" OnSelectedIndexChanged="ddl_Item_SelectedIndexChanged" width="196px" AutoPostBack="true" runat="server" CssClass="easyui-combobox">
                </asp:DropDownList>
                <asp:Label runat="server" Visible="false" ID="lbItemName"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
            </td>
        </tr>
    </table>
     
    <table class="GridviewGray fixedTable">
        <tr>
            <th>名称：
            </th>
            <td colspan="3">
                <asp:TextBox ID="txtJobName" runat="server" CssClass="inputbox_210" MaxLength="50"></asp:TextBox>
                <span style="color: Red;">*</span><asp:RequiredFieldValidator
                    ID="RequiredFieldValidatorJobName" runat="server" ErrorMessage="请填写名称！" ControlToValidate="txtJobName"
                    ValidationGroup="Error" Display="None"></asp:RequiredFieldValidator>
            </td>
        </tr>

        <tr>
            <th>描述：
            </th>
            <td colspan="3">
                <wuc:UEditor ID="FCKeditorJobDescription" runat="server" Width="495" Height="100" CssClass="inputbox_area349" ToolType="Basic" AutoHeightEnabled="false"></wuc:UEditor>

            </td>
        </tr>

        <tr>
          
            <th>有&nbsp;&nbsp;效&nbsp;&nbsp;期：
            </th>
            <td colspan="3">
                 <asp:UpdatePanel runat="server" ID="update1"  UpdateMode="Conditional">
                <ContentTemplate>
                <cc1:DateTimeTextBox runat="server" ID="dttbBeginDate" DataTimeFormat="%Y-%M-%D %h:%m:%s" EndTimeControlID="dttbEndDate"></cc1:DateTimeTextBox>
                <%--<span style="color: Red;">*</span>  --%>                              
                    至<cc1:DateTimeTextBox runat="server" ID="dttbEndDate" DataTimeFormat="%Y-%M-%D %h:%m:%s" BeginTimeControlID="dttbBeginDate"></cc1:DateTimeTextBox>
                <span style="color: Red;">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorJobDescription" runat="server" ErrorMessage="请填写有效期开始时间！" ControlToValidate="dttbBeginDate" ValidationGroup="Error" Display="None"></asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="请填写有效期结束时间！" ControlToValidate="dttbEndDate" ValidationGroup="Error" Display="None"></asp:RequiredFieldValidator>
            </td>
                  </ContentTemplate>
            </asp:UpdatePanel>
        </tr>

        <tr>
            <th>状&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;态：
            </th>
            <td colspan="3">
                <asp:RadioButtonList ID="rblStatus" runat="server" RepeatDirection="Horizontal" CssClass="noborder">
                    <asp:ListItem Value="1" Selected="True">启用</asp:ListItem>
                    <asp:ListItem Value="0">停用</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>

        <%-- <tr>
                <th>
                    创建人：
                </th>
                <td>
                    <asp:TextBox ID="txtTeacherID" runat="server" CssClass="inputbox_210"></asp:TextBox>
                    <%-- <span style="color: Red;">*</span><asp:RequiredFieldValidator
                        ID="RequiredFieldValidatorTeacherID" runat="server" ErrorMessage="请填写创建人！" ControlToValidate="txtTeacherID"
                        ValidationGroup="Edit"></asp:RequiredFieldValidator> 
                </td>
            </tr>--%>
    </table>
                  
</div>
<!--提交表单-->
<div class="dv_submit">
    <asp:LinkButton ID="lbnSave" runat="server" CssClass="btn_Save" OnClick="lbnSave_Click" ValidationGroup="Error">保存</asp:LinkButton>
    <asp:ValidationSummary runat="server" ID="ValidationSummary1" ValidationGroup="Error"
        ShowMessageBox="true" ShowSummary="false" />
    <a href="javascript:closeWindow();" class="btn_Cancel padleft10">取消</a>
</div>
