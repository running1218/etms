<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TraningProjectInfo.ascx.cs"
    Inherits="TraningImplement_TraningProjectManager_Controls_TraningProjectInfo" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/MiniUpFile.ascx" TagName="uploader" TagPrefix="uc" %>
<!--表单录入-->
<div class="dv_information">
    <table class="GridviewGray">
        <tr>
            <th>
                项目编码：
            </th>
            <td colspan="3">
                <asp:Label ID="txt_ItemCode" runat="server"  MaxLength="50">项目编码自动生成：P + 机构编码 + 4位年 + 4位流水号</asp:Label>
            </td>
        </tr>
        <tr>
            <th>
                项目名称：
            </th>
            <td colspan="3">
                <asp:TextBox ID="Txt_ItemName" runat="server" CssClass="inputbox_210" MaxLength="100"></asp:TextBox><font
                    color="red">*</font><asp:RequiredFieldValidator ID="RequiredFieldValidator1" Text="*"
                        Display="None" runat="server" ErrorMessage="请填写项目名称！" ControlToValidate="Txt_ItemName"
                        ValidationGroup="Saves"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr class="hide">
            <th>
                缩&nbsp;&nbsp;略&nbsp;&nbsp;图：
            </th>
            <td valign="top" colspan="3">
                <div style="overflow: hidden;">
                    <asp:Image ID="imgCourseLogo" runat="server" CssClass="imgCourseLogo" Width="156px"
                        Height="105px" />建议图片大小：156 * 105px</div>
                <%--<cc1:FileUpload ID="fuCourseImage" runat="server" CallBack="doCallBack" />--%>
                <uc:uploader ID="uploader" runat="server" FunctionType="ItemLogo" CallBack="doCallBack" />
                <script type="text/javascript">
                    function doCallBack(imgName, imgUrl, imgSize) {
                        document.getElementById('<%=imgCourseLogo.ClientID %>').src = imgUrl;
                    }
                </script>
            </td>
        </tr>
        <tr>
            <th>
                来自计划：
            </th>
            <td>
                <cc1:DictionaryRadioButtonList runat="server" ID="Rbl_IsPlanItem" DictionaryType="Dic_TrueOrFalse" />
            </td>
            <th>
                专业类别：
            </th>
            <td>
                <cc1:DictionaryDropDownList runat="server" ID="ddlSpecialtyTypeCode" DictionaryType="Dic_Sys_SpecialtyType"
                    SelectedDefaultValue="" IsShowChoose="false" IsShowAll="false" />
                <font color="red">*</font><asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                    ValidationExpression="\d[0,*]" Text="*" Display="None" runat="server" ErrorMessage="请选择专业类别！"
                    ControlToValidate="ddlSpecialtyTypeCode" ValidationGroup="Saves"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr class="Tr_TraningPlan">
            <th>
                培训计划：
            </th>
            <td colspan="3">
                <asp:DropDownList ID="Ddl_PlanID" runat="server" CssClass="select_190">
                </asp:DropDownList>
                <font color="red">*</font>
            </td>
        </tr>
        <tr>
            <th style="display:none">
                培训级别：
            </th>
            <td style="display:none">
                <cc1:DictionaryRadioButtonList runat="server" ID="rblTrainingLevelID" DictionaryType="Dic_Sys_TrainingLevel" />
            </td>
            <th>
                是否启用：
            </th>
            <td colspan="3">
                <cc1:DictionaryRadioButtonList runat="server" ID="rblIsUse" DictionaryType="Dic_TrueOrFalse" />
            </td>
        </tr>
        <tr class="Tr_Department" style="display: none">
            <th>
                组织部门：
            </th>
            <td colspan="3">
                <cc1:DictionaryDropDownList runat="server" ID="ddlDutyDeptID" DictionaryType="Site_DepartmentByOrgID"
                    SelectedDefaultValue="" IsShowChoose="true" />
                <font color="red">*</font>
            </td>
        </tr>
        <tr>
            <th>
                项目周期：
            </th>
            <td colspan="3">
                <cc1:DateTimeTextBox ID="Dtt_ItemBeginTime" runat="server" EndTimeControlID="Dtt_ItemEndTime"></cc1:DateTimeTextBox><font
                    color="red">*</font><asp:RequiredFieldValidator ID="RequiredFieldValidator3" Text="*"
                        Display="None" runat="server" ErrorMessage="请填写项目开始时间！" ControlToValidate="Dtt_ItemBeginTime"
                        ValidationGroup="Saves"></asp:RequiredFieldValidator>
                至
                <cc1:DateTimeTextBox ID="Dtt_ItemEndTime" runat="server" BeginTimeControlID="Dtt_ItemBeginTime"></cc1:DateTimeTextBox><font
                    color="red">*</font><asp:RequiredFieldValidator ID="RequiredFieldValidator4" Text="*"
                        Display="None" runat="server" ErrorMessage="请填写项目结束时间！" ControlToValidate="Dtt_ItemEndTime"
                        ValidationGroup="Saves"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th>
                报名方式：
            </th>
            <td colspan="3">
                <cc1:DictionaryRadioButtonList runat="server" ID="Dic_SignupMode" DictionaryType="Dic_Sys_SignupMode" />
            </td>
        </tr>
        <tr>
            <th>
                可选选修课数：
            </th>
            <td colspan="3">
               <%-- <cc1:DictionaryRadioButtonList runat="server" ID="rblChooseCourseNum" CheckedValue="0" DictionaryType="Dic_Sys_Limited"></cc1:DictionaryRadioButtonList>
                <span class="self-choose-num hide">--%>
                    <cc1:CustomTextBox ID="txtSelfChooseCourseNum" runat="server" ContentType="Number" Text="-1" CssClass="inputbox_60"></cc1:CustomTextBox>
                    <font color="red">*</font>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" Text="*" Display="None" runat="server" ErrorMessage="可选选修课数！" ControlToValidate="txtSelfChooseCourseNum" ValidationGroup="Saves"></asp:RequiredFieldValidator>
                    <span style="margin-left:20px; color:#0094ff;">-1:表示不限制，0:不可选，正数表示可选门数</span>
                <%--</span>--%>
            </td>
        </tr>
        <tr class="Tr_Signup" style="display: none">
            <th>
                报名时段：
            </th>
            <td colspan="3">
                <cc1:DateTimeTextBox ID="dttSignupBeginTime" runat="server" EndTimeControlID="dttSignupEndTime"></cc1:DateTimeTextBox><font
                    color="red">*</font> 至
                <cc1:DateTimeTextBox ID="dttSignupEndTime" runat="server" BeginTimeControlID="dttSignupBeginTime"></cc1:DateTimeTextBox><font
                    color="red">*</font>
            </td>
        </tr>
        <tr>
            <th>
                项目负责人：
            </th>
            <td colspan="3">
                <asp:TextBox ID="Txt_DutyUser" runat="server" CssClass="inputbox_80 inputdutyuser" MaxLength="64"></asp:TextBox><input
                    id="btnSelect" type="button" class="btn_SelectUser" value="" />
            </td>
        </tr>
        <tr>
            <th>
                手&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;机：
            </th>
            <td>
                <cc1:CustomTextBox ID="Txt_Mobile" runat="server" CssClass="inputbox_120 inputmobile" ContentType="Number"
                    MaxLength="12"></cc1:CustomTextBox>
            </td>
            <th>
                邮&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;箱：
            </th>
            <td>
                <asp:TextBox ID="Txt_EMAIL" runat="server" CssClass="inputbox_120 inputemail" MaxLength="128"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="Txt_EMAIL"
                    Display="None" ErrorMessage="邮箱格式错误！" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                    ValidationGroup="Saves"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <th>
                项目简介：
            </th>
            <td colspan="3">
                <asp:TextBox ID="Txt_ItemTarget" runat="server" CssClass="inputbox_area300" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>
                目标学员：
            </th>
            <td colspan="3">
                <asp:TextBox ID="Txt_ItemObjectStudent" runat="server" CssClass="inputbox_area300"
                    TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>
                备&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;注：
            </th>
            <td colspan="3">
                <asp:TextBox ID="Txt_Remark" runat="server" CssClass="inputbox_area300" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
    </table>
</div>
<script type="text/javascript">
    $(function () {
        //是否来自计划
        var radiosGroup = document.getElementById("<%= Rbl_IsPlanItem.ClientID %>");
        var radio0 = $(radiosGroup).find("input[type='radio']").get(0);
        var radio1 = $(radiosGroup).find("input[type='radio']").get(1);
        $(radio0).attr("checked") == "checked" || $(radio0).attr("checked") == "true" ? $(".Tr_TraningPlan").show() : $(".Tr_TraningPlan").hide();
        $(radio0).click(function () {
            $(".Tr_TraningPlan").show();
        })
        $(radio1).click(function () {
            $(".Tr_TraningPlan").hide();
        })

       <%-- var radiosLimt = document.getElementById("<%= rblChooseCourseNum.ClientID %>");
        var radio3 = $(radiosLimt).find("input[type='radio']").get(0);
        var radio4 = $(radiosLimt).find("input[type='radio']").get(1);
        $(radio3).attr("checked") == "checked" || $(radio3).attr("checked") == "true" ? $(".self-choose-num").hide() : $(".self-choose-num").show();
        $(radio3).click(function () {
            $(".self-choose-num").hide();
            var txtNum = document.getElementById("<%= txtSelfChooseCourseNum.ClientID %>");
            $(txtNum).val('-1');
        })
        $(radio4).click(function () {
            $(".self-choose-num").show();
        })--%>

        //培训级别
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

        //报名方式
        var radiosGroup3 = document.getElementById("<%= Dic_SignupMode.ClientID %>");
        var radio30 = $(radiosGroup3).find("input[type='radio']").get(0);
        var radio31 = $(radiosGroup3).find("input[type='radio']").get(1);
        var radio32 = $(radiosGroup3).find("input[type='radio']").get(2);
        var radio33 = $(radiosGroup3).find("input[type='radio']").get(3);
        $(radio32).attr("checked") == "checked" || $(radio32).attr("checked") == "true" || $(radio33).attr("checked") == "checked" || $(radio33).attr("checked") == "true" ? $(".Tr_Signup").show() : $(".Tr_Signup").hide();

        $(radio30).click(function () {
            $(".Tr_Signup").hide();
        })
        $(radio31).click(function () {
            $(".Tr_Signup").hide();
        })
        $(radio32).click(function () {
            $(".Tr_Signup").show();
        })
        $(radio33).click(function () {
            $(".Tr_Signup").show();
        })
        function checkMsg() {
            if ($(".Tr_Signup").find(":visible")) {
                if (document.getElementById("<%= dttSignupBeginTime.ClientID %>").value == "") {
                    alert('报名开始时间不能为空');
                } else if (document.getElementById("<%= dttSignupEndTime.ClientID %>").value == "") {
                    alert('报名结束时间不能为空');
                }
            }
        }

    });
</script>
<script type="text/javascript">
    $(document).ready(function () {
        $("#btnSelect").click(function () {
            showWindow('选择项目负责人', 'Selected/SelectUser.aspx', 500, 410);
        });
    });
</script>
<div class="dv_submit">
    <asp:Button ID="Btn_Save" CssClass="btn_Save" runat="server" Text="保存" OnClick="Btn_Save_Click"
        ValidationGroup="Saves" />
    <asp:ValidationSummary runat="server" ID="ValidationSummary1" ValidationGroup="Saves"
        ShowMessageBox="true" ShowSummary="false" />
    <input type="button" class="btn_Cancel" value="取消" onclick="javascript:closeWindow();" /></div>
