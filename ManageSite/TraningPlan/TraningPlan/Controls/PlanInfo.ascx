<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PlanInfo.ascx.cs"
    Inherits="ETMS.WebApp.Manage.TraningPlan.TraningPlan.Controls.PlanInfo" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>

<div class="dv_information">
    <table class="GridviewGray GridveiwFixed th80">
        <tr>
            <th >
                计划编码：
            </th>
            <td colspan="3">
                <asp:TextBox ID="txtPlanCode" runat="server" CssClass="inputbox_210" MaxLength="50"></asp:TextBox><font
                    color="red">*</font><asp:RequiredFieldValidator ID="RequiredFieldValidator6" Text="*"
                        Display="None" runat="server" ErrorMessage="请填写计划编码！" ControlToValidate="txtPlanCode"
                        ValidationGroup="Saves"></asp:RequiredFieldValidator>
            </td>
        </tr>     
        <tr>
            <th>
                计划名称：
            </th>
            <td colspan="3">
                <asp:TextBox ID="txtPlanName" runat="server" CssClass="inputbox_210" MaxLength="100"></asp:TextBox><font
                    color="red">*</font><asp:RequiredFieldValidator ID="RequiredFieldValidator1" Text="*"
                        Display="None" runat="server" ErrorMessage="请填写计划名称！" ControlToValidate="txtPlanName"
                        ValidationGroup="Saves"></asp:RequiredFieldValidator>
            </td>
        </tr>   
        <tr>
            <th>
                计划周期：
            </th>
            <td colspan="3">
                <cc1:DateTimeTextBox ID="dttPlanBeginTime" runat="server" EndTimeControlID="dttPlanEndTime"></cc1:DateTimeTextBox><font
                    color="red">*</font><asp:RequiredFieldValidator ID="RequiredFieldValidator3" Text="*"
                        Display="None" runat="server" ErrorMessage="请填写计划开始时间！" ControlToValidate="dttPlanBeginTime"
                        ValidationGroup="Saves"></asp:RequiredFieldValidator>
                至
                <cc1:DateTimeTextBox ID="dttPlanEndTime" runat="server" BeginTimeControlID="dttPlanBeginTime"></cc1:DateTimeTextBox><font
                    color="red">*</font><asp:RequiredFieldValidator ID="RequiredFieldValidator4" Text="*"
                        Display="None" runat="server" ErrorMessage="请填写计划结束时间！" ControlToValidate="dttPlanEndTime"
                        ValidationGroup="Saves"></asp:RequiredFieldValidator>
            </td>
        </tr>
             
    </table>
     <table class="GridviewGray GridveiwFixed th80" >   
        <tr>
            <th  >
                培训级别：
            </th>
            <td >
                <cc1:DictionaryRadioButtonList runat="server" ID="rblTrainingLevelID" DictionaryType="Dic_Sys_TrainingLevel" />
            </td>
            <th >
                是否启用：
            </th>
            <td >
                <cc1:DictionaryRadioButtonList runat="server" ID="rblIsUse" DictionaryType="Dic_TrueOrFalse" />
            </td>
        </tr>
        <tr class="Tr_Department" style="display: none">
            <th>
                组织<asp:Literal ID="ltlDepartment" runat="server" Text="<%$ Resources:UIResource, ui_department%>"></asp:Literal>：
            </th>
            <td colspan="3">
                <cc1:DictionaryDropDownList runat="server" ID="ddlDutyDeptID" DictionaryType="Site_DepartmentByOrgID"
                    SelectedDefaultValue="" IsShowChoose="true" />
            </td>
        </tr>
        <tr>
            <th>
                计划类型：
            </th>
            <td>
                <cc1:DictionaryDropDownList runat="server" ID="ddlPlanType" DictionaryType="Dic_Sys_PlanType"
                    IsShowChoose="true" />
                <font color="red">*</font><asp:RequiredFieldValidator ID="RequiredFieldValidator5"
                    ValidationExpression="\d[0,*]" Text="*" Display="None" runat="server" ErrorMessage="请选择计划类型！"
                    ControlToValidate="ddlPlanType" ValidationGroup="Saves"></asp:RequiredFieldValidator>
            </td>
            <th>
                负&nbsp;&nbsp;责&nbsp;&nbsp;人：
            </th>
            <td>
                <asp:TextBox ID="txtDutyUser" runat="server" CssClass="inputbox_120" MaxLength="64"></asp:TextBox><font
                    color="red">*</font><asp:RequiredFieldValidator ID="RequiredFieldValidator7" Text="*"
                        Display="None" runat="server" ErrorMessage="请填写负责人！" ControlToValidate="txtDutyUser"
                        ValidationGroup="Saves"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th>
                手&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;机：
            </th>
            <td>
                <asp:TextBox ID="txtMobile" runat="server" CssClass="inputbox_120" MaxLength="50"></asp:TextBox><font
                    color="red">*</font><asp:RequiredFieldValidator ID="RequiredFieldValidator8" Text="*"
                        Display="None" runat="server" ErrorMessage="请填写手机！" ControlToValidate="txtMobile"
                        ValidationGroup="Saves"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtMobile"
                    Display="None" ErrorMessage="手机格式错误！" ValidationExpression="\d{11}"
                    ValidationGroup="Saves"></asp:RegularExpressionValidator>
            </td>
            <th>
                邮&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;箱：
            </th>
            <td>
                <asp:TextBox ID="txtEMAIL" runat="server" CssClass="inputbox_120" MaxLength="128"></asp:TextBox><font
                    color="red">*</font>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEMAIL"
                    Display="None" ErrorMessage="邮箱格式错误！" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                    ValidationGroup="Saves"></asp:RegularExpressionValidator><asp:RequiredFieldValidator
                        ID="RequiredFieldValidator9" Text="*" Display="None" runat="server" ErrorMessage="请填写邮箱！"
                        ControlToValidate="txtEMAIL" ValidationGroup="Saves"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th>
                预&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;算：
            </th>
            <td>   
                <cc1:CustomTextBox ID="txtBudgetFee" runat="server" CssClass="inputbox_120" ContentType="Decimal"
                    MaxLength="12"></cc1:CustomTextBox>元<font color="red">*</font><asp:RequiredFieldValidator
                        ID="RequiredFieldValidator2" Text="*" Display="None" runat="server" ErrorMessage="预算不能为空！"
                        ControlToValidate="txtBudgetFee" ValidationGroup="Saves"></asp:RequiredFieldValidator>
            </td>
            <th>
                计划学员数：
            </th>
            <td>
                <asp:TextBox ID="txtStudentNum" runat="server" CssClass="inputbox_120"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtStudentNum"
                    Display="None" ErrorMessage="计划学员数格式错误！" ValidationExpression="\d{0,6}"
                    ValidationGroup="Saves"></asp:RegularExpressionValidator>
            </td>
        </tr>

        </table>

        <table class="GridviewGray GridveiwFixed th80">
           <tr>
            <th>
                计划目标：
            </th>
            <td colspan="3">
                <asp:TextBox ID="txtPlanTarget" runat="server" CssClass="inputbox_area300" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>
                目标学员：
            </th>
            <td colspan="3">
                <asp:TextBox ID="txtPlanObjectStudent" runat="server" CssClass="inputbox_area300"
                    TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>
                备&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;注：
            </th>
            <td colspan="3">
                <asp:TextBox ID="txtRemark" runat="server" CssClass="inputbox_area300" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        </table>
</div>
<script type="text/javascript">
    $(function () {
        var radiosGroup2 = document.getElementById("<%= rblTrainingLevelID.ClientID %>");
        var radio02 = $(radiosGroup2).find("input[type='radio']").get(0);
        var radio12 = $(radiosGroup2).find("input[type='radio']").get(1);
        $(radio12).attr("checked") == "checked" || $(radio12).attr("checked") == "true" ? $(".Tr_Department").show() : $(".Tr_Department").hide();
        $(radio02).click(function () {
            $(".Tr_Department").hide();
        })
        $(radio12).click(function () {
            $(".Tr_Department").show();
        })
    })
</script>
<div class="dv_submit">
    <asp:Button ID="Btn_Save" CssClass="btn_Save" runat="server" Text="保存" OnClick="Btn_Save_Click"
        ValidationGroup="Saves" />
    <asp:ValidationSummary runat="server" ID="ValidationSummary1" ValidationGroup="Saves"
        ShowMessageBox="true" ShowSummary="false" />
    <input type="button" class="btn_Cancel" value="取消" onclick="javascript:closeWindow();" /></div>
