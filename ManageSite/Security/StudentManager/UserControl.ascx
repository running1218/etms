<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserControl.ascx.cs" Inherits="Security_StudentManager_UserControl" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<cc1:CustomMuliView runat="server" ID="Views">
    <asp:View runat="server" ID="View_Edit">
        <table class="GridviewGray th80">
            <tr>
                <th>
                    学员帐号：
                </th>
                <td colspan="3">
                    <asp:TextBox ID="txtLoginName" runat="server"></asp:TextBox><span style="color: Red;">*</span><asp:RequiredFieldValidator
                        ID="RequiredFieldValidatorLoginName" Text="&nbsp;" Display="None" runat="server"
                        ErrorMessage="请填写学员帐号！" ControlToValidate="txtLoginName" ValidationGroup="Edit"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <%--仅添加模式下显示密码输入--%>
            <%if (Request.QueryString["op"].Equals("add", StringComparison.InvariantCultureIgnoreCase))
              { %>
            <tr>
                <th>
                    密码：
                </th>
                <td>
                    <asp:TextBox ID="txtPassWord" runat="server" TextMode="Password"></asp:TextBox><span
                        style="color: Red;">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidatorPassWord"
                            Text="&nbsp;" Display="None" runat="server" ErrorMessage="请填写密码！" ControlToValidate="txtPassWord"
                            ValidationGroup="Edit"></asp:RequiredFieldValidator>
                </td>
                <th>
                    确认密码：
                </th>
                <td>
                    <asp:TextBox ID="txtPassWord1" runat="server" TextMode="Password"></asp:TextBox><span
                        style="color: Red;">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                            Display="None" Text="&nbsp;" runat="server" ErrorMessage="请填写确认密码！" ControlToValidate="txtPassWord1"
                            ValidationGroup="Edit"></asp:RequiredFieldValidator>
                    <asp:CompareValidator runat="server" ID="CompareValidator1" ControlToValidate="txtPassWord1"
                        Text="&nbsp;" ErrorMessage="请两次密码填写不一致" Display="Dynamic" ValidationGroup="Edit"
                        ControlToCompare="txtPassWord"></asp:CompareValidator>
                </td>
            </tr>
            <%} %>
            <tr>
                <th>
                    学员姓名：
                </th>
                <td>
                    <asp:TextBox ID="txtRealName" runat="server"></asp:TextBox><span style="color: Red;">*</span><asp:RequiredFieldValidator
                        Display="None" ID="RequiredFieldValidatorRealName" runat="server" ErrorMessage="请填写学员姓名！"
                        ControlToValidate="txtRealName" ValidationGroup="Edit"></asp:RequiredFieldValidator>
                </td>
                <th>
                    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:UIResource, ui_workno%>"></asp:Literal>：
                </th>
                <td>
                    <asp:TextBox ID="txtWorkNo" runat="server"></asp:TextBox>
                    <%-- 之前是必填，唯一（全局）；现在是选填 --%>
                    <%--  <span style="color: Red;">*</span><asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator2"
                        runat="server" ErrorMessage="请填写工号！" ControlToValidate="txtWorkNo" ValidationGroup="Edit"></asp:RequiredFieldValidator>--%>
                </td>
            </tr>
            <tr style="display:none">
                <th>
                    <asp:Literal ID="ltlDepartment" runat="server" Text="<%$ Resources:UIResource, ui_department%>"></asp:Literal>：
                </th>
                <td colspan="3">
                    <cc1:DictionaryDropDownList runat="server" ID="ddlDepartment" DictionaryType="Site_DepartmentByOrgID"
                        CssClass="select_120" IsShowChoose="true" />
                </td>
            </tr>
            <tr style="display:none">
                <th>
                    <asp:Literal ID="ltlRank" runat="server" Text="<%$ Resources:UIResource, ui_rank%>"></asp:Literal>：
                </th>
                <td>
                    <cc1:DictionaryDropDownList runat="server" ID="ddlRank" DictionaryType="vw_Dic_Sys_Rank"
                        IsShowChoose="true" />
                </td>
                <th>
                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:UIResource, ui_position%>"></asp:Literal>：
                </th>
                <td>
                    <cc1:DictionaryDropDownList runat="server" ID="ddlPost" DictionaryType="Dic_PostByOrgID"
                        IsShowChoose="true" />
                </td>
            </tr>
            <tr>
                <th>
                    邮箱：
                </th>
                <td>
                    <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                    <asp:RegularExpressionValidator ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                        ID="RequiredFieldValidatorEmail" runat="server" ErrorMessage="请正确填写邮箱！" ControlToValidate="txtEmail"
                        Display="None" ValidationGroup="Edit"></asp:RegularExpressionValidator>
                </td>
                <th>
                    手机：
                </th>
                <td>
                    <asp:TextBox ID="txtMobilePhone" runat="server"></asp:TextBox>
                    <asp:RegularExpressionValidator ValidationExpression="\d{11}" ID="RegularExpressionValidator1"
                        Display="None" runat="server" ErrorMessage="请正确填写手机！" ControlToValidate="txtMobilePhone"
                        ValidationGroup="Edit"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <th>
                    工作职务：
                </th>
                <td>
                    <asp:TextBox ID="txtTitleName" runat="server"></asp:TextBox>
                </td>
                <th>
                    直接上级：
                </th>
                <td>
                    <asp:TextBox ID="txtParent" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>
                    出生日期：
                </th>
                <td>
                    <cc1:DateTimeTextBox ID="txtBirthDay" runat="server"></cc1:DateTimeTextBox>
                </td>
                <th>
                    性别：
                </th>
                <td>
                    <cc1:DictionaryRadioButtonList runat="server" ID="rdlSex" DictionaryType="Dic_Sex"
                        CheckedValue="1" />
                </td>
            </tr>
            <tr>
                <th>
                    身份证号：
                </th>
                <td>
                    <asp:TextBox ID="txtIdentity" runat="server"></asp:TextBox>
                </td>
                <th>
                    电话：
                </th>
                <td>
                    <asp:TextBox ID="txtTelphone" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>
                    最高学历：
                </th>
                <td>
                    <asp:TextBox ID="txtHighestEducation" runat="server"></asp:TextBox>
                </td>
                <th>
                    专业：
                </th>
                <td>
                    <asp:TextBox ID="txtSpecialty" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>
                    政治面貌：
                </th>
                <td>
                    <cc1:DictionaryDropDownList runat="server" ID="ddlPolitics" DictionaryType="Dic_Sys_Politics"
                      SelectedDefaultValue="-1" IsShowChoose="true" />
                </td>
                <th>
                    入职日期：
                </th>
                <td>
                    <cc1:DateTimeTextBox ID="txtJobTime" runat="server"></cc1:DateTimeTextBox>
                </td>
            </tr>
            <tr>
                <th>
                    学员身份：
                </th>
                <td colspan="3">
                    <cc1:DictionaryDropDownList runat="server" ID="ddlResettlementWay" DictionaryType="Dic_Sys_ResettlementWay"
                      SelectedDefaultValue="-1" IsShowChoose="true" />
                </td>
            </tr>
            <tr>
                <th>
                    备注：
                </th>
                <td colspan="3">
                    <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" SkinID="textarea"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>
                    状态：
                </th>
                <td colspan="3">
                    <cc1:DictionaryRadioButtonList runat="server" ID="rbStatus" DictionaryType="Dic_Status"
                        CheckedValue="1">
                    </cc1:DictionaryRadioButtonList>
                </td>
            </tr>
        </table>
        <asp:ValidationSummary runat="server" ID="validationSummary1" ValidationGroup="Edit"
            ShowMessageBox="true" ShowSummary="false" />
    </asp:View>
    <asp:View runat="server" ID="View_Browse">
        <table class="GridviewGray">
            <tr>
                <th>
                    学员帐号：
                </th>
                <td colspan="3">
                    <asp:Label ID="lblLoginName" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    学员姓名：
                </th>
                <td>
                    <asp:Label ID="lblRealName" runat="server"></asp:Label>
                </td>
                <th>
                    <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:UIResource, ui_workno%>"></asp:Literal>：
                </th>
                <td>
                    <asp:Label ID="lblWorkNo" runat="server"></asp:Label>
                </td>
            </tr>
            <tr style="display:none">
                <th>
                    <asp:Literal ID="ltlDepartment1" runat="server" Text="<%$ Resources:UIResource, ui_department%>"></asp:Literal>：
                </th>
                <td colspan="3">
                    <cc1:DictionaryLabel runat="server" ID="lblDepartment" DictionaryType="Site_DepartmentByOrgID" />
                </td>
            </tr>
            <tr style="display:none;">
                <th>
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:UIResource, ui_rank%>"></asp:Literal>：
                </th>
                <td>
                    <cc1:DictionaryLabel runat="server" ID="lblRank" DictionaryType="vw_Dic_Sys_Rank" />
                </td>
                <th>
                    <asp:Literal ID="ltlPosition" runat="server" Text="<%$ Resources:UIResource, ui_position%>"></asp:Literal>：
                </th>
                <td>
                    <cc1:DictionaryLabel runat="server" ID="lblPost" DictionaryType="Dic_PostByOrgID" />
                </td>
            </tr>
            <tr>
                <th>
                    性别：
                </th>
                <td>
                    <cc1:DictionaryLabel runat="server" ID="lblSex" DictionaryType="Dic_Sex" />
                </td>
                <th>
                    身份证号：
                </th>
                <td>
                    <asp:Label ID="lblIdentity" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    工作职务：
                </th>
                <td>
                    <asp:Label ID="lblJobTitle" runat="server"></asp:Label>
                </td>
                <th>
                    直接上级：
                </th>
                <td>
                    <asp:Label ID="lblParent" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    出生日期：
                </th>
                <td>
                    <cc1:DateTimeLabel ID="lblBirthDay" runat="server"></cc1:DateTimeLabel>
                </td>
                <th>
                    邮箱：
                </th>
                <td>
                    <asp:Label ID="lblEmail" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    电话：
                </th>
                <td>
                    <asp:Label ID="lblTelephone" runat="server"></asp:Label>
                </td>
                <th>
                    手机：
                </th>
                <td>
                    <asp:Label ID="lblMobilePhone" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    最高学历：
                </th>
                <td>
                    <asp:Label ID="lblHightestEducation" runat="server"></asp:Label>
                </td>
                <th>
                    专业：
                </th>
                <td>
                    <asp:Label ID="lblSpecialty" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    政治面貌：
                </th>
                <td>
                    <cc1:DictionaryLabel runat="server" ID="lblPolitics" DictionaryType="Dic_Sys_Politics" />
                </td>
                <th>
                    <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:UIResource, ui_jobtime%>"></asp:Literal>：
                </th>
                <td>
                    <cc1:DateTimeLabel ID="lblJobTime" runat="server"></cc1:DateTimeLabel>
                </td>
            </tr>
            <tr>
                <th>
                    状态：
                </th>
                <td colspan="3">
                    <cc1:DictionaryLabel runat="server" ID="lblStatus" DictionaryType="Dic_Status" />
                </td>
            </tr>
            <tr>
                <th>
                    学员身份：
                </th>
                <td colspan="3"><cc1:DictionaryLabel runat="server" ID="dlabResettlementWay" DictionaryType="Dic_Sys_ResettlementWay" />
                </td>
            </tr>
            <tr>
                <th>
                    备注：
                </th>
                <td colspan="3">
                    <asp:Label ID="lblDescription" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    创建人：
                </th>
                <td>
                    <asp:Label ID="lblCreator" runat="server"></asp:Label>
                </td>
                <th>
                    创建时间：
                </th>
                <td>
                    <asp:Label ID="lblCreateTime" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </asp:View>
</cc1:CustomMuliView>