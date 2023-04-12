<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CoursePeriodInfo.ascx.cs"
    Inherits="TraningImplement_ProjectCoursePeriod_Controls_CoursePeriodInfo" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<!--表单录入-->
<div class="dv_information">
    <table class="GridviewGray GridveiwFixed">
        <tr>
            <th >
                项目编码：
            </th>
            <td width="30%">
                <asp:Label ID="lblItemCode" runat="server"></asp:Label>
            </td>
            <th >
                项目名称：
            </th>
            <td >
                <asp:Label ID="lblItemName" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <th>
                课程编码：
            </th>
            <td>
                <asp:Label ID="lblCourseCode" runat="server"></asp:Label>
            </td>
            <th>
                课程名称：
            </th>
            <td>
                <asp:Label ID="lblCourseName" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <th>
                组织机构：
            </th>
            <td>
                <cc1:DictionaryLabel ID="lblOrganization" DictionaryType="Dic_CurrentAndSubOrganization" runat="server" TextLength="10" />
            </td>
            <th>
                课程属性：
            </th>
            <td>
                <cc1:DictionaryLabel ID="dlblCourseAttr" DictionaryType="Dic_Sys_CourseAttr" runat="server" />
            </td>
        </tr>
        <tr>
            <th>
                课程类型：
            </th>
            <td>
                <cc1:DictionaryLabel ID="dlblCourseType" DictionaryType="Dic_Sys_CourseType" runat="server" />
            </td>
            <th>
                课程等级：
            </th>
            <td>
                <cc1:DictionaryLabel ID="dlblCourseLevel" DictionaryType="Dic_Sys_CourseLevel" runat="server" />
            </td>
        </tr>
        <tr>
            <th>
                培训方式：
            </th>
            <td>
                <cc1:DictionaryLabel ID="dlblTrainingModel" DictionaryType="Dic_Sys_TrainingModel"
                    runat="server" />
            </td>
            <th>
                授课方式：
            </th>
            <td>
                <cc1:DictionaryLabel ID="dlblTeachModel" DictionaryType="Dic_Sys_TeachModel" runat="server" />
            </td>
        </tr>
        <tr>
            <th>
                培训日期：
            </th>
            <td colspan="3">
                <cc1:DateTimeTextBox ID="dtxtTrainingDate" runat="server"></cc1:DateTimeTextBox><font
                    color="red">*</font><asp:RequiredFieldValidator ID="RequiredFieldValidator3" Text="*"
                        Display="None" runat="server" ErrorMessage="请填写培训日期！" ControlToValidate="dtxtTrainingDate"
                        ValidationGroup="Saves"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th>
                培训时段：
            </th>
            <td colspan="3">
                <cc1:DateTimeTextBox ID="dtxtTrainingBeginTime" runat="server"
                    DateTimeFormat="%h:%m" MaxLength="5" ></cc1:DateTimeTextBox><font color="red">*</font><asp:RequiredFieldValidator
                        ID="RequiredFieldValidator1" Text="*" Display="None" runat="server" ErrorMessage="请填写培训时段！"
                        ControlToValidate="dtxtTrainingBeginTime" ValidationGroup="Saves"></asp:RequiredFieldValidator>
                至
                <cc1:DateTimeTextBox ID="dtxtTrainingEndTime" runat="server" 
                    DateTimeFormat="%h:%m" MaxLength="5"  ></cc1:DateTimeTextBox><font color="red">*</font><asp:RequiredFieldValidator
                        ID="RequiredFieldValidator2" Text="*" Display="None" runat="server" ErrorMessage="请填写培训时段！"
                        ControlToValidate="dtxtTrainingEndTime" ValidationGroup="Saves"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th>
                讲&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;师：
            </th>
            <td colspan="3">
                <asp:DropDownList ID="ddlTeacher" runat="server">
                </asp:DropDownList>
                <font color="red">*</font><asp:RequiredFieldValidator ID="RequiredFieldValidator5"
                    ValidationExpression="\d[0,*]" Text="*" Display="None" runat="server" ErrorMessage="请选择讲师！"
                    ControlToValidate="ddlTeacher" ValidationGroup="Saves"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th>
                培训时间说明：
            </th>
            <td>
                <cc1:DictionaryDropDownList ID="dddlTrainingTimeDesc" runat="server" DictionaryType="Dic_Sys_TrainingTimeDesc"
                    IsShowChoose="true" />
                <font color="red">*</font><asp:RequiredFieldValidator ID="RequiredFieldValidator6"
                    ValidationExpression="\d[0,*]" Text="*" Display="None" runat="server" ErrorMessage="请选择培训时间说明！"
                    ControlToValidate="dddlTrainingTimeDesc" ValidationGroup="Saves"></asp:RequiredFieldValidator>
            </td>
            <th>
                培训课时：
            </th>
            <td><cc1:CustomTextBox ID="txtCourseHours" runat="server" CssClass="inputbox_120" ContentType="Decimal"
                    MaxLength="12"></cc1:CustomTextBox><font color="red">*</font><asp:RequiredFieldValidator
                    ID="RequiredFieldValidator7" Text="*" Display="None" runat="server" ErrorMessage="请填写培训课时！"
                    ControlToValidate="txtCourseHours" ValidationGroup="Saves"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th>
                培训地点：
            </th>
            <td colspan="3">
                <asp:DropDownList ID="ddlClassRoomAddress" runat="server" CssClass="select_390">
                </asp:DropDownList>
                <font color="red">*</font><asp:RequiredFieldValidator ID="RequiredFieldValidator8"
                    ValidationExpression="\d[0,*]" Text="*" Display="None" runat="server" ErrorMessage="请选择培训教室！"
                    ControlToValidate="ddlClassRoomAddress" ValidationGroup="Saves"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th>
                课时说明：
            </th>
            <td colspan="3">
                <asp:TextBox ID="txtCourseHoursDesc" TextMode="MultiLine" runat="server" CssClass="inputbox_area300"></asp:TextBox>
            </td>
        </tr>
    </table>
</div>
<div class="dv_submit">
    <asp:Button ID="Btn_Save" CssClass="btn_Save" runat="server" Text="保存" OnClick="Btn_Save_Click"
        ValidationGroup="Saves" />
    <asp:ValidationSummary runat="server" ID="ValidationSummary1" ValidationGroup="Saves"
        ShowMessageBox="true" ShowSummary="false" />
    <input type="button" class="btn_Cancel" value="取消" onclick="javascript:closeWindow();" /></div>
