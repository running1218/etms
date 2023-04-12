<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CourseInfo.ascx.cs" Inherits="TraningOrgManager_TraningOrgManager_Controls_CourseInfo"  %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Assembly="ETMS.Editor" Namespace="ETMS.Editor.UEditor" TagPrefix="wuc" %>
<!--表单录入-->
<div class="dv_information">
    <table class="GridviewGray">
        <tr>
            <th>
                课程编码：
            </th>
            <td>
                <asp:TextBox ID="txtCourseCode" runat="server" CssClass="inputbox_300" MaxLength="20"></asp:TextBox>
                <font color="red">*</font>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCourseCode"
                    Display="None" ErrorMessage="请填写课程编码！" ValidationGroup="Error"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th>
                课程名称：
            </th>
            <td>
                <asp:TextBox ID="txtCourseName" runat="server" CssClass="inputbox_300" MaxLength="50"></asp:TextBox>
                <font color="red">*</font>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorName" runat="server" ControlToValidate="txtCourseName"
                    Display="None" ErrorMessage="请填写课程名称！" ValidationGroup="Error"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th>
                课程类型：
            </th>
            <td>
                <cc1:DictionaryDropDownList runat="server" ID="dropCourseTypeCode" DictionaryType="Dic_Sys_CourseType"
                    IsShowChoose="true" CssText="select_120" />
                <font color="red">*</font>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorType" runat="server" ControlToValidate="dropCourseTypeCode"
                    Display="None" ErrorMessage="请选择课程类型！" ValidationGroup="Error"></asp:RequiredFieldValidator>
            </td>           
        </tr>       
        <tr>
           <th>试用地址：</th>
           <td>
              <asp:TextBox ID="txtAdress" runat="server" CssClass="inputbox_300" MaxLength="60" />
           </td>
        </tr>
        <tr>
            <th>
                适用对象：
            </th>
            <td >
                <wuc:UEditor runat="server" ID="FCKeditorForObject" ToolType="Basic"
                    Width="420" Height="100"></wuc:UEditor>
            </td>
        </tr>
        <tr>
            <th>
                课程介绍：
            </th>
            <td>
                <wuc:UEditor runat="server" ID="FCKeditorCourseIntroduction" ToolType="Basic"
                    Width="420" Height="100"></wuc:UEditor>
            </td>
        </tr>
        <tr>
            <th>
                课程大纲：
            </th>
            <td>
                <wuc:UEditor runat="server" ID="FCKeditorCourseOutline" ToolType="Basic"
                    Width="420" Height="100"></wuc:UEditor>
            </td>
        </tr>
         <tr>
            <th>
                备&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;注：
            </th>
            <td>
                <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" CssClass="inputbox_area300" />
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
    <a href="javascript:closeWindow();" class="btn_Cancel padleft10">取消</a>
</div>

