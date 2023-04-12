﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CourseInfo.ascx.cs" Inherits="ETMS.WebApp.Manage.Living.CourseInfo" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Assembly="ETMS.Editor" Namespace="ETMS.Editor.UEditor" TagPrefix="wuc" %>
<%@ Register Src="~/Controls/MiniUpFile.ascx" TagName="uploader" TagPrefix="uc" %>
<!--表单录入-->
<div class="dv_information">
    <table class="GridviewGray fixedTable">
        <tr>
            <th>
                课程编码：
            </th>
            <td >
                <asp:Label ID="lblCourseCode" runat="server">系统自动产生，课程类型代码+组织机构编码+年份（２位）+三位流水号</asp:Label>
            </td>
        </tr>
        <tr>
            <th>
                课程名称：
            </th>
            <td >
                <asp:TextBox ID="txtCourseName" runat="server" CssClass="inputbox_300" MaxLength="50"></asp:TextBox>
                <font color="red">*</font>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorName" runat="server" ControlToValidate="txtCourseName"
                    Display="None" ErrorMessage="请填写课程名称！" ValidationGroup="Error"></asp:RequiredFieldValidator>
            </td>
        </tr>
        </table>

         <table class="GridviewGray fixedTable">
        <tr>
            <th>
                课程类型：
            </th>
            <td>
                <asp:Panel ID="pnlCourseType" runat="server">
                    <cc1:DictionaryDropDownList runat="server" ID="dropCourseTypeCode" DictionaryType="Dic_Sys_CourseType"
                        IsShowChoose="true" CssText="select_120" />
                    <font color="red">*</font>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorType" runat="server" ControlToValidate="dropCourseTypeCode"
                        Display="None" ErrorMessage="请选择课程类型！" ValidationGroup="Error"></asp:RequiredFieldValidator>
                </asp:Panel>
                <asp:Panel ID="pnlCourseType2" runat="server" Visible="false">
                    <cc1:DictionaryLabel ID="lblCourseType" runat="server" DictionaryType="Dic_Sys_CourseType"></cc1:DictionaryLabel>
                </asp:Panel>
            </td>
            <th>
                课程等级：
            </th>
            <td>
                <cc1:DictionaryDropDownList runat="server" ID="dropCourseLevelID" DictionaryType="Dic_Sys_CourseLevel"
                    IsShowChoose="true" CssText="select_100" DefaultValue="2" />
                <font color="red">*</font>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorLevel" runat="server" ControlToValidate="dropCourseLevelID"
                    Display="None" ErrorMessage="请选择课程等级！" ValidationGroup="Error"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th>
                课程状态：
            </th>
            <td>
                <cc1:DictionaryRadioButtonList ID="radCourseStatus" runat="server" CssClass="noborder"
                    RepeatDirection="Horizontal" DictionaryType="Dic_Status" CheckedValue="1">
                </cc1:DictionaryRadioButtonList>
            </td>
            <th>
                课&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;时：
            </th>
            <td>
                <cc1:CustomTextBox ID="txtCourseHours" runat="server" CssClass="inputbox_100" ValidationGroup="Error"
                    Text="0" MaxLength="6" ContentType="Decimal"></cc1:CustomTextBox>小时
            </td>
        </tr>
        <tr>
            <th>
                是否收费：
            </th>
            <td>
                <cc1:DictionaryRadioButtonList ID="rblIsPay" runat="server" CssClass="noborder"
                    RepeatDirection="Horizontal" DictionaryType="Dic_TrueOrFalse" CheckedValue="0">
                </cc1:DictionaryRadioButtonList>
            </td>
            <th>适合年龄：</th>
            <td>
                <asp:TextBox ID="txtForObject" runat="server" CssClass="inputbox_120"></asp:TextBox>岁
            </td>
        </tr>
        <tr>
            <th>
                直播类型：
            </th>
            <td colspan="3">
                <cc1:DictionaryDropDownList runat="server" ID="ddlLivingType" DictionaryType="Dic_Sys_LivingType"
                    IsShowChoose="true" CssText="select_100" DefaultValue="1" />
                <font color="red">*</font>
                <asp:RequiredFieldValidator ID="rfvLivingType" runat="server" ControlToValidate="ddlLivingType"
                    Display="None" ErrorMessage="请选直播类型！" ValidationGroup="Error"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr class="hide_norm" id="tr_price">
            <th>
                原价格：
            </th>
            <td>
                <cc1:CustomTextBox ID="txtPrice" runat="server" ContentType="Decimal" CssClass="inputbox_80" MaxLength="50"></cc1:CustomTextBox>元
                <font color="red">*</font>
            </td>
            <th>
                优惠价格：
            </th>
            <td>
                <cc1:CustomTextBox ID="txtDiscountPrice" runat="server" CssClass="inputbox_80" MaxLength="50"></cc1:CustomTextBox>
                <font color="red">*</font>
            </td>
        </tr>
        <tr class="hide_norm">
            <th>
                是否公开：
            </th>
            <td>
                <cc1:DictionaryRadioButtonList ID="radIsPublic" runat="server" CssClass="noborder"
                    RepeatDirection="Horizontal" DictionaryType="Dic_TrueOrFalse" CheckedValue="0">
                </cc1:DictionaryRadioButtonList>
            </td>
        </tr>
        <tr>
            <th>
                缩&nbsp;&nbsp;略&nbsp;&nbsp;图：
            </th>
            <td valign="top" colspan="3">
                <div style="overflow: hidden;">
                    <asp:Image ID="imgCourseLogo" runat="server" CssClass="imgCourseLogo" Width="156px"
                        Height="105px" /></div>
                <uc:uploader ID="uploader" runat="server" FunctionType="CourseLogo" CallBack="doCallBack" FileTypeIsDisplay="false" />
                <script type="text/javascript">
                    function doCallBack(imgName, imgUrl, imgSize) {
                        document.getElementById('<%=imgCourseLogo.ClientID %>').src = imgUrl;
                    }
                </script>
                <span class="upload-img-standard">支持jpg、gif格式的深底色图片，最佳尺寸为256×256像素</span>
            </td>
        </tr>
        <tr>
            <th>
                课程详情：
            </th>
            <td colspan="3">
                <wuc:UEditor ID="FCKeditorCourseIntroduction" runat="server" Width="600" Height="100" ToolType="Basic" AutoHeightEnabled="false"></wuc:UEditor>
            </td>
        </tr>
        <tr class="hide_norm">
            <th>
                适用对象：
            </th>
            <td colspan="3">
                <wuc:UEditor ID="FCKeditorForObject" runat="server" Width="600" Height="100" ToolType="Basic" AutoHeightEnabled="false"></wuc:UEditor>
            </td>
        </tr>        
        <tr>
            <th>
                课程表：
            </th>
            <td colspan="3">
                <wuc:UEditor ID="FCKeditorCourseOutline" runat="server" Width="600" Height="100" ToolType="Basic" AutoHeightEnabled="false"></wuc:UEditor>
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

<script lang="javascript">
    $(function () {
        var radiosGroup = document.getElementById("<%= rblIsPay.ClientID %>");
        var radio0 = $(radiosGroup).find("input[type='radio']").get(0);
        var radio1 = $(radiosGroup).find("input[type='radio']").get(1);
        $(radio0).attr("checked") == "checked" || $(radio0).attr("checked") == "true" ? $("#tr_price").show() : $("#tr_price").hide();
        $(radio0).click(function () {
            $("#tr_price").show();
        })
        $(radio1).click(function () {
            $("#tr_price").hide();
        })
    });
    
</script>
