<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserControl.ascx.cs" Inherits="Admin_Site_User_UserControl" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<cc1:CustomMuliView runat="server" ID="Views">
    <asp:View runat="server" ID="View_Edit">
        <table class="GridviewGray ">
            <tr>
                <th>
                    用户账户：
                </th>
                <td colspan="3">
                    <asp:TextBox ID="txtLoginName" runat="server"></asp:TextBox><span style="color: Red;">*</span><asp:RequiredFieldValidator
                        ID="RequiredFieldValidatorLoginName" Text="&nbsp;" Display="None" runat="server"
                        ErrorMessage="请填写用户账户！" ControlToValidate="txtLoginName" ValidationGroup="Edit"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <%--仅添加模式下显示密码输入--%>
            <%if ("add".Equals(Request.QueryString["op"], StringComparison.InvariantCultureIgnoreCase))
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
                    用户姓名：
                </th>
                <td colspan="3">
                    <asp:TextBox ID="txtRealName" runat="server"></asp:TextBox>
                    <%-- <span style="color: Red;">*</span><asp:RequiredFieldValidator  Display="None" 
                        ID="RequiredFieldValidatorRealName" runat="server" ErrorMessage="请填写！" ControlToValidate="txtRealName"
                        ValidationGroup="Edit"></asp:RequiredFieldValidator> --%>
                </td>
            </tr>
            <tr>
                <th>
                    邮箱：
                </th>
                <td>
                    <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                    <%-- <span style="color: Red;">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                        runat="server" ErrorMessage="请填写邮箱！" ControlToValidate="txtEmail" ValidationGroup="Edit"></asp:RequiredFieldValidator>--%>
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
                    办公电话：
                </th>
                <td>
                    <asp:TextBox ID="txtOfficeTelphone" runat="server"></asp:TextBox>
                    <%--<asp:RegularExpressionValidator ValidationExpression="(\(\d{3}\)|\d{3}-)?\d{8}" ID="RegularExpressionValidator3"
                        Display="None"  runat="server" ErrorMessage="请正确填写办公电话" ControlToValidate="txtOfficeTelphone"
                        ValidationGroup="Edit"></asp:RegularExpressionValidator>--%>
                </td>
                <th>
                    家庭电话：
                </th>
                <td>
                    <asp:TextBox ID="txtTelphone" runat="server"></asp:TextBox>
                    <%-- <asp:RegularExpressionValidator ValidationExpression="(\(\d{3}\)|\d{3}-)?\d{8}" ID="RegularExpressionValidator2"
                        Display="None"  runat="server" ErrorMessage="请正确填写家庭电话！" ControlToValidate="txtTelphone"
                        ValidationGroup="Edit"></asp:RegularExpressionValidator>--%>
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
            <%--修改个人信息时，不显示状态--%>
            <%if (this.Request.Path.IndexOf("ChangeUserInfo.aspx", StringComparison.InvariantCultureIgnoreCase) == -1)
              { %>
            <tr>
                <th>
                    状态：
                </th>
                <td colspan="3">
                    <asp:RadioButtonList runat="server" ID="rbStatus" RepeatDirection="horizontal" RepeatLayout="Flow">
                        <asp:ListItem Text="启用" Value="1" Selected="true"></asp:ListItem>
                        <asp:ListItem Text="禁用" Value="0"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <%} %>
        </table>
        <asp:ValidationSummary runat="server" ID="validationSummary1" ValidationGroup="Edit"
            ShowMessageBox="true" ShowSummary="false" />
    </asp:View>
    <asp:View runat="server" ID="View_Browse">
        <table class="GridviewGray">
            <tr>
                <th>
                    用户编码：
                </th>
                <td>
                    <asp:Label ID="lblUserID" runat="server"></asp:Label>
                </td>
                <th>
                    用户账户：
                </th>
                <td>
                    <asp:Label ID="lblLoginName" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    用户姓名 ：
                </th>
                <td>
                    <asp:Label ID="lblRealName" runat="server"></asp:Label>
                </td>
                <th>
                    用户状态 ：
                </th>
                <td>
                    <asp:Label ID="lblStatus" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    邮箱：
                </th>
                <td>
                    <asp:Label ID="lblEmail" runat="server"></asp:Label>
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
                    办公电话：
                </th>
                <td>
                    <asp:Label ID="lblTelphone" runat="server"></asp:Label>
                </td>
                <th>
                    家庭电话：
                </th>
                <td>
                    <asp:Label ID="lblHomeTelphone" runat="server"></asp:Label>
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
