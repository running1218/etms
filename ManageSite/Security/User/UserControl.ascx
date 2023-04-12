<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserControl.ascx.cs" Inherits="Admin_Site_User_UserControl" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<cc1:CustomMuliView runat="server" ID="Views">
    <asp:View runat="server" ID="View_Edit">
        <table class="GridviewGray ">
            <tr>
                <th>
                    �û��˻���
                </th>
                <td colspan="3">
                    <asp:TextBox ID="txtLoginName" runat="server"></asp:TextBox><span style="color: Red;">*</span><asp:RequiredFieldValidator
                        ID="RequiredFieldValidatorLoginName" Text="&nbsp;" Display="None" runat="server"
                        ErrorMessage="����д�û��˻���" ControlToValidate="txtLoginName" ValidationGroup="Edit"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <%--�����ģʽ����ʾ��������--%>
            <%if ("add".Equals(Request.QueryString["op"], StringComparison.InvariantCultureIgnoreCase))
              { %>
            <tr>
                <th>
                    ���룺
                </th>
                <td>
                    <asp:TextBox ID="txtPassWord" runat="server" TextMode="Password"></asp:TextBox><span
                        style="color: Red;">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidatorPassWord"
                            Text="&nbsp;" Display="None" runat="server" ErrorMessage="����д���룡" ControlToValidate="txtPassWord"
                            ValidationGroup="Edit"></asp:RequiredFieldValidator>
                </td>
                <th>
                    ȷ�����룺
                </th>
                <td>
                    <asp:TextBox ID="txtPassWord1" runat="server" TextMode="Password"></asp:TextBox><span
                        style="color: Red;">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                            Display="None" Text="&nbsp;" runat="server" ErrorMessage="����дȷ�����룡" ControlToValidate="txtPassWord1"
                            ValidationGroup="Edit"></asp:RequiredFieldValidator>
                    <asp:CompareValidator runat="server" ID="CompareValidator1" ControlToValidate="txtPassWord1"
                        Text="&nbsp;" ErrorMessage="������������д��һ��" Display="Dynamic" ValidationGroup="Edit"
                        ControlToCompare="txtPassWord"></asp:CompareValidator>
                </td>
            </tr>
            <%} %>
            <tr>
                <th>
                    �û�������
                </th>
                <td colspan="3">
                    <asp:TextBox ID="txtRealName" runat="server"></asp:TextBox>
                    <%-- <span style="color: Red;">*</span><asp:RequiredFieldValidator  Display="None" 
                        ID="RequiredFieldValidatorRealName" runat="server" ErrorMessage="����д��" ControlToValidate="txtRealName"
                        ValidationGroup="Edit"></asp:RequiredFieldValidator> --%>
                </td>
            </tr>
            <tr>
                <th>
                    ���䣺
                </th>
                <td>
                    <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                    <%-- <span style="color: Red;">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                        runat="server" ErrorMessage="����д���䣡" ControlToValidate="txtEmail" ValidationGroup="Edit"></asp:RequiredFieldValidator>--%>
                    <asp:RegularExpressionValidator ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                        ID="RequiredFieldValidatorEmail" runat="server" ErrorMessage="����ȷ��д���䣡" ControlToValidate="txtEmail"
                        Display="None" ValidationGroup="Edit"></asp:RegularExpressionValidator>
                </td>
                <th>
                    �ֻ���
                </th>
                <td>
                    <asp:TextBox ID="txtMobilePhone" runat="server"></asp:TextBox>
                    <asp:RegularExpressionValidator ValidationExpression="\d{11}" ID="RegularExpressionValidator1"
                        Display="None" runat="server" ErrorMessage="����ȷ��д�ֻ���" ControlToValidate="txtMobilePhone"
                        ValidationGroup="Edit"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <th>
                    �칫�绰��
                </th>
                <td>
                    <asp:TextBox ID="txtOfficeTelphone" runat="server"></asp:TextBox>
                    <%--<asp:RegularExpressionValidator ValidationExpression="(\(\d{3}\)|\d{3}-)?\d{8}" ID="RegularExpressionValidator3"
                        Display="None"  runat="server" ErrorMessage="����ȷ��д�칫�绰" ControlToValidate="txtOfficeTelphone"
                        ValidationGroup="Edit"></asp:RegularExpressionValidator>--%>
                </td>
                <th>
                    ��ͥ�绰��
                </th>
                <td>
                    <asp:TextBox ID="txtTelphone" runat="server"></asp:TextBox>
                    <%-- <asp:RegularExpressionValidator ValidationExpression="(\(\d{3}\)|\d{3}-)?\d{8}" ID="RegularExpressionValidator2"
                        Display="None"  runat="server" ErrorMessage="����ȷ��д��ͥ�绰��" ControlToValidate="txtTelphone"
                        ValidationGroup="Edit"></asp:RegularExpressionValidator>--%>
                </td>
            </tr>
            <tr>
                <th>
                    ��ע��
                </th>
                <td colspan="3">
                    <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" SkinID="textarea"></asp:TextBox>
                </td>
            </tr>
            <%--�޸ĸ�����Ϣʱ������ʾ״̬--%>
            <%if (this.Request.Path.IndexOf("ChangeUserInfo.aspx", StringComparison.InvariantCultureIgnoreCase) == -1)
              { %>
            <tr>
                <th>
                    ״̬��
                </th>
                <td colspan="3">
                    <asp:RadioButtonList runat="server" ID="rbStatus" RepeatDirection="horizontal" RepeatLayout="Flow">
                        <asp:ListItem Text="����" Value="1" Selected="true"></asp:ListItem>
                        <asp:ListItem Text="����" Value="0"></asp:ListItem>
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
                    �û����룺
                </th>
                <td>
                    <asp:Label ID="lblUserID" runat="server"></asp:Label>
                </td>
                <th>
                    �û��˻���
                </th>
                <td>
                    <asp:Label ID="lblLoginName" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    �û����� ��
                </th>
                <td>
                    <asp:Label ID="lblRealName" runat="server"></asp:Label>
                </td>
                <th>
                    �û�״̬ ��
                </th>
                <td>
                    <asp:Label ID="lblStatus" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    ���䣺
                </th>
                <td>
                    <asp:Label ID="lblEmail" runat="server"></asp:Label>
                </td>
                <th>
                    �ֻ���
                </th>
                <td>
                    <asp:Label ID="lblMobilePhone" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    �칫�绰��
                </th>
                <td>
                    <asp:Label ID="lblTelphone" runat="server"></asp:Label>
                </td>
                <th>
                    ��ͥ�绰��
                </th>
                <td>
                    <asp:Label ID="lblHomeTelphone" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    ��ע��
                </th>
                <td colspan="3">
                    <asp:Label ID="lblDescription" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    �����ˣ�
                </th>
                <td>
                    <asp:Label ID="lblCreator" runat="server"></asp:Label>
                </td>
                <th>
                    ����ʱ�䣺
                </th>
                <td>
                    <asp:Label ID="lblCreateTime" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </asp:View>
</cc1:CustomMuliView>
