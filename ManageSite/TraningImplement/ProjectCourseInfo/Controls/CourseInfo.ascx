<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CourseInfo.ascx.cs" Inherits="TraningImplement_ProjectCourseInfo_Controls_CourseInfo" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<!--表单录入-->
<div class="dv_information">
    <table class="GridviewGray">
        <tr>
            <th>
                课程属性
            </th>
            <td>
                <cc1:DictionaryDropDownList runat="server" ID="MasterheadModeType1" DictionaryType="Dic_CourseAttributeType"
                    IsShowChoose="true" />
            </td>
            <th>
                培训方式
            </th>
            <td>
                <cc1:DictionaryDropDownList runat="server" ID="Dictionarydropdownlist1" DictionaryType="Dic_CourseTrainingMode"
                    IsShowChoose="true" />
            </td>
        </tr>
        <tr>
            <th>
                课程学时
            </th>
            <td>
                <input type="text" name="textfield4" class="inputbox_120" />
            </td>
            <th>
                外训机构
            </th>
            <td>
                <cc1:DictionaryDropDownList runat="server" ID="Dictionarydropdownlist3" DictionaryType="Dic_OutsideOrganization"
                    IsShowChoose="true" />
            </td>
        </tr>
        <tr>
            <th>
                课程积分
            </th>
            <td>
                <input type="text" name="textfield4" class="inputbox_120" />
            </td>
            <th>
                辅助讲师
            </th>
            <td>
                <cc1:DictionaryDropDownList runat="server" ID="Dictionarydropdownlist2" DictionaryType="Dic_AuxiliaryTeacher"
                    IsShowChoose="true" />
            </td>
        </tr>
        <tr>
            <th>
                人数受限
            </th>
            <td>
                <cc1:DictionaryRadioButtonList runat="server" ID="DictionaryRadioButtonList1" DictionaryType="Dic_TrueOrFalse">
                </cc1:DictionaryRadioButtonList>
            </td>
            <th>
                最大人数
            </th>
            <td>
                <input type="text" name="textfield4" class="inputbox_120" />
            </td>
        </tr>
        <tr>
            <th>
                培训期间
            </th>
            <td colspan="3">
                <cc1:DateTimeTextBox ID="DateTimeTextBox1" runat="server" EndTimeControlID="DateTimeTextBox2"></cc1:DateTimeTextBox>
                至
                <cc1:DateTimeTextBox ID="DateTimeTextBox2" runat="server" BeginTimeControlID="DateTimeTextBox1"></cc1:DateTimeTextBox>
            </td>
        </tr>
        <tr>
            <th>
                备 注
            </th>
            <td colspan="3">
                <asp:TextBox ID="TextBox1" runat="server" CssClass="inputbox_area440"></asp:TextBox>
            </td>
        </tr>
    </table>
</div>
<div class="center">
      <input type="button" class="btn_Save" value="保存" onclick="javascript:popSuccessMsg('恭喜你保存成功！','提示',closeWindow);" />
    <input type="button" class="btn_Cancel" value="取消" onclick="javascript:closeWindow();" /></div>
