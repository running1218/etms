<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master" AutoEventWireup="true"
    CodeFile="SetsCourseEdit.aspx.cs" Inherits="TraningImplement_TraningProjectManager_SetsCourseEdit" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--表单录入-->
    <div class="dv_information">
        <table class="GridviewGray">
            <tr>
                <th width="22%">
                    项目编码：
                </th>
                <td width="28%">
                    <asp:Label ID="lblItemCode" runat="server"></asp:Label>
                </td>
                <th width="22%">
                    项目名称：
                </th>
                <td width="28%">
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
                    课程类型：
                </th>
                <td>
                    <cc1:DictionaryLabel ID="DictionaryLabelCourseType" DictionaryType="Dic_Sys_CourseType"
                        runat="server" />
                </td>
                <th>
                    课程等级：
                </th>
                <td>
                    <cc1:DictionaryLabel ID="DictionaryLabelCourseLevel" DictionaryType="Dic_Sys_CourseLevel"
                        runat="server" />
                </td>
            </tr>
        </table>
        <h4 class="h4_title" id="title1">
            <a href="#" class="dropdownico dropupico">必填信息</a>
        </h4>
        <table class="GridviewGray">
            <tr>
                <th>
                    授课方式：
                </th>
                <td>
                    <cc1:DictionaryDropDownList runat="server" ID="ddlTeachModelID" DictionaryType="Dic_Sys_TeachModel"
                        SelectedDefaultValue="" IsShowChoose="false" IsShowAll="false" ValidationGroup="Saves" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationExpression="\d[0,*]"
                        Text="*" Display="None" runat="server" ErrorMessage="请选择授课方式！" ControlToValidate="ddlTeachModelID"
                        ValidationGroup="Saves"></asp:RequiredFieldValidator>
                </td>
                <th>
                    课程属性：
                </th>
                <td>
                    <cc1:DictionaryDropDownList runat="server" ID="ddlCourseAttrID" DictionaryType="Dic_Sys_CourseAttr"
                        IsShowChoose="false" IsShowAll="false" />
                </td>
            </tr>
            <tr>
                <th>
                    状&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;态：
                </th>
                <td>
                    <cc1:DictionaryRadioButtonList runat="server" ID="ddlCourseStatus" DictionaryType="Dic_Status">
                    </cc1:DictionaryRadioButtonList>
                </td>
                <th>
                    课程学时：
                </th>
                <td>
                    <cc1:CustomTextBox ID="txtCourseHours" runat="server" CssClass="inputbox_120" ContentType="Decimal"
                        MaxLength="12"></cc1:CustomTextBox>
                    <asp:TextBox ID="txtCompareValue" runat="server" Text="0" CssClass="hide"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Text="*" Display="None"
                        runat="server" ErrorMessage="请填写课程学时！" ControlToValidate="txtCourseHours" ValidationGroup="Saves"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="cvCourseHours" runat="server" Text="*" Display="None" ErrorMessage="课程学时要大于0"
                        ControlToValidate="txtCourseHours" ValidationGroup="Saves" ControlToCompare="txtCompareValue"
                        Type="Double" Operator="GreaterThan"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <th>
                    及&nbsp;&nbsp;格&nbsp;&nbsp;线：
                </th>
                <td colspan="3">
                    <cc1:CustomTextBox ID="txtPassLine" runat="server" CssClass="inputbox_120" ContentType="Number"
                        MaxLength="6"></cc1:CustomTextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator6"
                            Text="*" Display="None" runat="server" ErrorMessage="请填写及格线！" ControlToValidate="txtPassLine"
                            ValidationGroup="Saves"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th>
                    培训日期：
                </th>
                <td colspan="3">
                    <cc1:DateTimeTextBox ID="ttbCourseBeginTime" runat="server" EndTimeControlID="ttbCourseEndTime"
                        ValidationGroup="Saves"></cc1:DateTimeTextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                            Text="*" Display="None" runat="server" ErrorMessage="请填写培训日期开始时间！" ControlToValidate="ttbCourseBeginTime"
                            ValidationGroup="Saves"></asp:RequiredFieldValidator>
                    至
                    <cc1:DateTimeTextBox ID="ttbCourseEndTime" runat="server" BeginTimeControlID="ttbCourseBeginTime"
                        ValidationGroup="Saves"></cc1:DateTimeTextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator4"
                            Text="*" Display="None" runat="server" ErrorMessage="请填写培训日期结束时间！" ControlToValidate="ttbCourseEndTime"
                            ValidationGroup="Saves"></asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
        <h4 class="h4_title hide" id="title2">
            <a href="#" class="dropdownico">选填辅助信息</a></h4>
        <table class="hide" class="GridviewGray">
            <tr>
                <th>
                    培训方式：
                </th>
                <td>
                    <cc1:DictionaryDropDownList runat="server" ID="ddlTrainingModelID" DictionaryType="Dic_Sys_TrainingModel"
                        IsShowChoose="false" IsShowAll="false" />
                </td>
                <th>
                    课程积分：
                </th>
                <td>
                    <asp:TextBox ID="txtScore" runat="server" CssClass="inputbox_120" MaxLength="8"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>
                    外训机构：
                </th>
                <td>
                    <asp:DropDownList runat="server" ID="ddlOuterOrgID" />
                </td>
                <th>
                    外训机构联系人：
                </th>
                <td>
                    <asp:TextBox ID="txtOuterOrgDutyUser" runat="server" CssClass="inputbox_120" MaxLength="64"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>
                    外训机构邮箱：
                </th>
                <td colspan="3">
                    <asp:TextBox ID="txtOuterOrgEMAIL" runat="server" CssClass="inputbox_120" MaxLength="128"></asp:TextBox>
                </td>
            </tr>
            <th>
                预&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;算：
            </th>
            <td colspan="3">
                <cc1:CustomTextBox ID="txtBudgetFee" runat="server" CssClass="inputbox_120" ContentType="Decimal"
                    MaxLength="12"></cc1:CustomTextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator5"
                        Text="*" Display="None" runat="server" ErrorMessage="请填写预算！" ControlToValidate="txtBudgetFee"
                        ValidationGroup="Saves"></asp:RequiredFieldValidator>
            </td>
            <tr>
                <th>
                    备&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;注：
                </th>
                <td colspan="3">
                    <asp:TextBox ID="txtRemark" TextMode="MultiLine" Rows="5" runat="server" CssClass="inputbox_area300" MaxLength="1024"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    <!--提交表单-->
    <div class="dv_submit">
        <asp:Button ID="btn_Save" runat="server" CssClass="btn_Save" Text="保存" OnClick="btn_Save_Click"
            ValidationGroup="Saves" />
        <asp:ValidationSummary runat="server" ID="ValidationSummary1" ValidationGroup="Saves"
            ShowMessageBox="true" ShowSummary="false" />
        <input type="button" class="btn_Cancel" onclick="javascript:closeWindow();" value="取消" />
    </div>
    <script type="text/javascript">
        $(function () {
            $("#title1").find(".dropdownico").toggle(function () {
                $(this).parent().next("table").hide();
                $(this).removeClass("dropupico");
            }, function () {
                $(this).parent().next("table").show();
                $(this).addClass("dropupico");
            })
            $("#title2").find(".dropdownico").toggle(function () {
                $(this).parent().next("table").show();
                $(this).addClass("dropupico");
            }, function () {
                $(this).parent().next("table").hide();
                $(this).removeClass("dropupico");
            })
        })
    </script>
</asp:Content>
