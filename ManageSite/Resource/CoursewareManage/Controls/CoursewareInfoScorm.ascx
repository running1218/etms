<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CoursewareInfoScorm.ascx.cs"
    Inherits="Resource_CoursewareManage_Controls_CoursewareInfoScorm" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/ChooseCourseDropdown.ascx" TagName="ChooseCourseDropdown"
    TagPrefix="uc1" %>
<!--功能标题-->
<h2 class="dv_title">
    SCORM标准
</h2>
<!--表单录入-->
<div class="dv_information">
    <table class="GridviewGray fixedTable">
        <tr>
            <th>
                课程名称：
            </th>
            <td>
                <uc1:ChooseCourseDropdown ID="ddlCoursewareID" runat="server" />
            </td>
        </tr>
        <tr>
            <th>
                课件名称：
            </th>
            <td>
                <asp:TextBox ID="txtCoursewareName" runat="server" CssClass="inputbox_300" MaxLength="50"></asp:TextBox>
                <span style="color: Red;">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                    runat="server" ErrorMessage="请输入课件名称！" ControlToValidate="txtCoursewareName"
                    ValidationGroup="Error" Display="None"></asp:RequiredFieldValidator>
            </td>
        </tr>
    </table>
    <table class="GridviewGray fixedTable">
        <tr>
            <th>
                课件状态：
            </th>
            <td>
                <cc1:DictionaryRadioButtonList runat="server" ID="radlCoursewareStatus" DictionaryType='Dic_Status' />
            </td>
            <th>
                课件时长：
            </th>
            <td>
                <asp:TextBox ID="txtShowHoures" runat="server" CssClass="inputbox_60" MaxLength="6"></asp:TextBox>分钟
                <span style="color: Red;">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                    runat="server" ErrorMessage="请输入课件时长！" ControlToValidate="txtShowHoures" ValidationGroup="Error"
                    Display="None"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator Display="None" ID="RegularExpressionValidator2" runat="server"
                    ControlToValidate="txtShowHoures" ErrorMessage="课件时长格式错误！" ValidationExpression="^\d+$"
                    ValidationGroup="Error"></asp:RegularExpressionValidator>
            </td>
        </tr>
    </table>
    <table class="GridviewGray fixedTable">
        <tr>
            <th>
                课件来源：
            </th>
            <td>
                <asp:TextBox ID="txtCoursewareSource" runat="server" CssClass="inputbox_300" MaxLength="50"></asp:TextBox>
            </td>
        </tr>
         <tr>
            <th>
                缩&nbsp;&nbsp;略&nbsp;&nbsp;图：
            </th>
            <td valign="top" colspan="3">
                <div style="overflow: hidden;">
                    <asp:Image ID="imgCoverLogo" runat="server" CssClass="imgCourseLogo" Width="156px"
                        Height="105px" />建议图片大小：156 * 105px</div>
                <cc1:FileUpload ID="fuCoverImage" runat="server" CallBack="doCallBack" />
                <script type="text/javascript">
                    function doCallBack(imgName, imgUrl) {
                        document.getElementById('<%=imgCoverLogo.ClientID %>').src = imgUrl;
                    }
                </script>
            </td>
        </tr>
        <tr>
            <th>
                课件介绍：
            </th>
            <td>
                <asp:TextBox ID="txtRemark" TextMode="MultiLine" runat="server" CssClass="inputbox_area300"></asp:TextBox>
            </td>
        </tr>
    </table>
</div>
<!--提交表单-->
<div class="dv_submit">
    <asp:Button ID="lbtnSave" runat="server" CssClass="btn_Save" OnClick="lbtnSave_Click"
        ValidationGroup="Error" Text="保存"></asp:Button>
    <asp:Button ID="btnNext" runat="server" CssClass="btn_NextStep" OnClick="btnNext_Click"
        Text="下一步" ValidationGroup="Error"></asp:Button>
    <asp:ValidationSummary runat="server" ID="ValidationSummary1" ValidationGroup="Error"
        ShowMessageBox="true" ShowSummary="false" />
    <input onclick="javascript:closeWindow();" value="取消" type="button" class="btn_Cancel " />
</div>
