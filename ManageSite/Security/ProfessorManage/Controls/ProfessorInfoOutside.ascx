<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProfessorInfoOutside.ascx.cs"
    Inherits="ETMS.WebApp.Manage.Resource.ProfessorManage.Controls.ProfessorInfoOutside" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/MiniUpFile.ascx" TagName="uploader" TagPrefix="uc" %>
<%@ Register Assembly="ETMS.Editor" Namespace="ETMS.Editor.UEditor" TagPrefix="wuc" %>
<!--功能标题-->
<h2 class="dv_title">
    讲师管理
</h2>
<!--表单录入-->
<div class="dv_information">
    <table class="GridviewGray">
        <tr>
            <th>
                用&nbsp;&nbsp;户&nbsp;&nbsp;名：
            </th>
            <td id="tdUserName" runat="server">
                <asp:TextBox ID="txtUserName" runat="server" CssClass="inputbox_120" MaxLength="50"></asp:TextBox>
                <span style="color: Red;">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                    runat="server" ErrorMessage="请输入用户名！" ControlToValidate="txtUserName" ValidationGroup="Error"
                    Display="None"></asp:RequiredFieldValidator>
            </td>
            <th width="80">
                讲师编码：
            </th>
            <td>
                <asp:TextBox ID="txtTeacherCode" runat="server" CssClass="inputbox_120" MaxLength="50" />
                <span style="color: Red;">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                    runat="server" ErrorMessage="请输入讲师编码！" ControlToValidate="txtTeacherCode" ValidationGroup="Error"
                    Display="None"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th width="80">
                讲师姓名：
            </th>
            <td>
                <asp:TextBox ID="txtTeacherName" runat="server" CssClass="inputbox_120" MaxLength="50"></asp:TextBox>
                <span style="color: Red;">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator4"
                    runat="server" ErrorMessage="请输入讲师姓名！" ControlToValidate="txtTeacherName" ValidationGroup="Error"
                    Display="None"></asp:RequiredFieldValidator>
            </td>
            <th>
                状&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;态：
            </th>
            <td>
                <cc1:DictionaryRadioButtonList runat="server" ID="Dic_Status" DictionaryType='Dic_Status'
                    CheckedValue="1" />
            </td>
        </tr>
        <tr id="thPassword" runat="server">
            <th>
                密&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;码：
            </th>
            <td id="tdPassword" runat="server">
                <asp:TextBox ID="txtPassword" runat="server" CssClass="inputbox_120" TextMode="Password"
                    MaxLength="50"></asp:TextBox>
                <span style="color: Red;">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                    runat="server" ErrorMessage="请输入密码！" ControlToValidate="txtPassword" ValidationGroup="Error"
                    Display="None"></asp:RequiredFieldValidator>
            </td>
            <th>
                确认密码：
            </th>
            <td>
                <asp:TextBox ID="tdPasswordAgain" runat="server" CssClass="inputbox_120" TextMode="Password"></asp:TextBox>
                <span style="color: Red;">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator5"
                    runat="server" ErrorMessage="请输入密码！" ControlToValidate="txtPassword" ValidationGroup="Error"
                    Display="None"></asp:RequiredFieldValidator>
                <asp:CompareValidator ControlToCompare="tdPasswordAgain" ID="comparValidator" ControlToValidate="txtPassword"
                    Operator="Equal" Display="None" ErrorMessage="请再输入确认密码" runat="server" ValidationGroup="Error" />
            </td>
        </tr>
        <tr>
            <th>
                讲师等级：
            </th>
            <td>
                <cc1:DictionaryDropDownList runat="server" ID="Dic_ProfessorGrade" DictionaryType="Dic_Sys_TeacherLevel" DefaultValue="2" IsShowAll="false"/>
                <span style="color: Red;">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidatorJobName"
                    runat="server" ErrorMessage="请选择讲师等级！" ControlToValidate="Dic_ProfessorGrade"
                    ValidationGroup="Error" Display="None"></asp:RequiredFieldValidator>
            </td>
            <th>
                出生日期：
            </th>
            <td>
                <cc1:DateTimeTextBox ID="txtBirthDay" runat="server"></cc1:DateTimeTextBox>
            </td>
        </tr>
        <tr>
            <th>
                培训机构：
            </th>
            <td>
                <cc1:DictionaryDropDownList runat="server" ID="Dic_Organization" DictionaryType="Tr_OuterOrg"
                    IsShowChoose="true" />
            </td>
            <th>
                性&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;别：
            </th>
            <td>
                <%-- <cc1:DictionaryRadioButtonList runat="server" ID="Dic_Sex" DictionaryType='Dic_Sex' />--%>
                <asp:RadioButtonList runat="server" ID="Dic_Sex" CssClass="noborder" RepeatDirection="Horizontal">
                    <asp:ListItem Value="1" Text="男" Selected="True" />
                    <asp:ListItem Value="0" Text="女" />
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <th>
                课&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;酬：
            </th>
            <td>
                <asp:TextBox ID="txtClassReward" runat="server" CssClass="inputbox_60" MaxLength="5"></asp:TextBox>（元/天）
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtClassReward"
                    Display="None" ErrorMessage="课酬格式错误！" ValidationExpression="^(0|[1-9]\d*)(\.\d*)?$"
                    ValidationGroup="Error"></asp:RegularExpressionValidator>
            </td>
            <th>
                职&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;务：
            </th>
            <td>
                <asp:TextBox ID="txtTitleName" runat="server" CssClass="inputbox_120" MaxLength="50"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>
                邮&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;箱：
            </th>
            <td>
                <asp:TextBox ID="txtMail" runat="server" CssClass="inputbox_120" MaxLength="50"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtMail"
                    Display="None" ErrorMessage="邮箱格式错误！" ValidationExpression="^[a-z0-9][a-z0-9_\.-]{0,}[a-z0-9]@[a-z0-9][a-z0-9_\.-]{0,}[a-z0-9][\.][a-z0-9]{2,4}$"
                    ValidationGroup="Error"></asp:RegularExpressionValidator>
            </td>
            <th>
                手&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;机：
            </th>
            <td>
                <asp:TextBox ID="txtTel" runat="server" CssClass="inputbox_120" MaxLength="11"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtTel"
                    Display="None" ErrorMessage="手机格式错误！" ValidationExpression="\d{11}" ValidationGroup="Error"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <th>
                讲师来源：
            </th>
            <td colspan="3">
                <cc1:DictionaryDropDownList runat="server" ID="ddlSourceType" DictionaryType="Dic_Sys_TeacherSource" DefaultValue="2" IsShowAll="false"/>                
            </td>
        </tr>
        <tr class="hide">
            <th>
                合作关系：
            </th>
            <td colspan="3">
                <cc1:DictionaryDropDownList runat="server" ID="ddlCorpationRealtion" DictionaryType="Dic_IsCorpation"
                    IsShowChoose="true" />
            </td>
        </tr>
        <tr>
            <th>
                个人照片：
            </th>
            <td colspan="3">
                <asp:Image ID="Image1" runat="server" Width="80" Height="100" ImageUrl="~/App_Themes/ThemeAdmin/Images/banner-1.gif" />
                <br /><br />
                <uc:uploader ID="uploader" runat="server" FunctionType="UserIcon" CallBack="doCallBack" FileTypeIsDisplay="false" />
                <script type="text/javascript">
                    function doCallBack(imgName, imgUrl, imgSize) {
                            document.getElementById('<%=Image1.ClientID %>').src = imgUrl;
                        }
                </script>
                <span class="upload-img-standard">支持jpg、png、gif格式图片，最佳尺寸为216×200像素</span>
            </td>
        </tr>
        <tr>
            <th>
                简&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;介：
            </th>
            <td colspan="3">
                <%--<asp:TextBox ID="txtDescription" TextMode="MultiLine" runat="server" CssClass="inputbox_area600"></asp:TextBox>--%>
                <wuc:UEditor ID="txtDescription" runat="server" Width="100%" Height="180" ToolType="Basic"></wuc:UEditor>
            </td>
        </tr>
        <tr class="hide">
            <th>
                服务企业：
            </th>
            <td colspan="3">
                <asp:TextBox ID="txtServiceEnterprise" TextMode="MultiLine" runat="server" CssClass="inputbox_area600"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>
                工作履历：
            </th>
            <td colspan="3">                
                <wuc:UEditor ID="txtWorkExperience" runat="server" Width="100%" Height="180" ToolType="Basic"></wuc:UEditor>
            </td>
        </tr>
        <tr>
            <th>
                专&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;长：
            </th>
            <td colspan="3">                
                <wuc:UEditor ID="txtExpertise" runat="server" Width="100%" Height="180" ToolType="Basic"></wuc:UEditor>
            </td>
        </tr>
        <tr class="hide">
            <th>
                代表作品：
            </th>
            <td colspan="3">
                <asp:TextBox ID="txtRepresentativeWorks" TextMode="MultiLine" runat="server" CssClass="inputbox_area600"></asp:TextBox>
            </td>
        </tr>
        
    </table>
</div>
<!--提交表单-->
<div class="dv_submit">
    <asp:LinkButton ID="lbnSave" runat="server" OnClick="lbnSave_Click" CssClass="btn_Save"
        ValidationGroup="Error">保存</asp:LinkButton>
    <asp:ValidationSummary runat="server" ID="ValidationSummary1" ValidationGroup="Error"
        ShowMessageBox="true" ShowSummary="false" />
    <a href="javascript:closeWindow();" class="btn_Cancel padleft10">取消</a>
</div>
