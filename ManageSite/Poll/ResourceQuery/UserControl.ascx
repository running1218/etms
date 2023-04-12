<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserControl.ascx.cs" Inherits="Poll_ResourceQuery_UserControl" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<cc1:CustomMuliView runat="server" ID="Views">
    <asp:View runat="server" ID="View_Edit">
        <table class="GridviewGray">
            <tr>
                <th>
                    �������ƣ�
                </th>
                <td>
                    <asp:TextBox ID="txtQueryName" runat="server" SkinID="Text300" MaxLength="100"></asp:TextBox><span
                        style="color: Red;">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidatorLoginName"
                            Text="&nbsp;" Display="None" runat="server" ErrorMessage="����д�������ƣ�" ControlToValidate="txtQueryName"
                            ValidationGroup="Edit"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th>
                    ����ʱ�䣺
                </th>
                <td>
                    <cc1:DateTimeTextBox runat="server" ID="txtBeginTime" EndTimeControlID="txtEndTime"></cc1:DateTimeTextBox><span
                        style="color: Red;">*</span>��&nbsp;<cc1:DateTimeTextBox runat="server" ID="txtEndTime"
                            BeginTimeControlID="txtBeginTime"></cc1:DateTimeTextBox>
                    <span style="color: Red;">*</span>
                    <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator1" runat="server"
                        ErrorMessage="����д���鿪ʼʱ�䣡" ControlToValidate="txtBeginTime" ValidationGroup="Edit"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidatorRealName" runat="server"
                        ErrorMessage="����д�������ʱ�䣡" ControlToValidate="txtBeginTime" ValidationGroup="Edit"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th>
                    �鿴�����
                </th>
                <td>
                    <cc1:DictionaryRadioButtonList runat="server" ID="rblIsAllShowResult" CheckedValue="0"
                        DictionaryType="Dic_TrueOrFalse" />
                </td>
            </tr>
            <tr>
                <th>
                    �ʾ�״̬��
                </th>
                <td>
                    <cc1:DictionaryRadioButtonList runat="server" ID="rblStatus" CheckedValue="1" DictionaryType="Dic_Status" />
                </td>
            </tr>
            <tr>
                <th>
                    �����ˣ�
                </th>
                <td>
                    <asp:TextBox ID="txtDutyUser" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>
                    �����ף�
                </th>
                <td>
                    <asp:TextBox ID="txtHeader" runat="server" TextMode="MultiLine" SkinID="textarea"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtHeader"
                        ValidationExpression="^(\s|\S){0,1000}$" Display="None" ErrorMessage="������������಻�ܳ���1000���ַ���"
                        ValidationGroup="Edit" />
                </td>
            </tr>
            <tr>
                <th>
                    �����
                </th>
                <td>
                    <asp:TextBox ID="txtFooter" runat="server" TextMode="MultiLine" SkinID="textarea"
                        MaxLength="500"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtFooter"
                        ValidationExpression="^(\s|\S){0,500}$" Display="None" ErrorMessage="������������಻�ܳ���500���ַ���"
                        ValidationGroup="Edit" />
                </td>
            </tr>
            <tr>
                <th>
                    ����˵����
                    <br />
                    (����Ա����ʹ��,<br />
                    ����ѧԱ����ʾ)
                </th>
                <td>
                    <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" SkinID="textarea"></asp:TextBox>
                </td>
            </tr>
        </table>
        <asp:ValidationSummary runat="server" ID="validationSummary1" ValidationGroup="Edit"
            ShowMessageBox="true" ShowSummary="false" />
    </asp:View>
    <asp:View runat="server" ID="View_Browse">
        <table class="GridviewGray th100">
            <tr>
                <th>
                    �������ƣ�
                </th>
                <td>
                    <asp:Label ID="lblQueryName" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    ����ʱ�䣺
                </th>
                <td>
                    <cc1:DateTimeLabel ID="lblBeginTime" runat="server" />��<cc1:DateTimeLabel ID="lblEndTime"
                        runat="server" />
                </td>
            </tr>
            <tr>
                <th>
                    �鿴�����
                </th>
                <td>
                    <cc1:DictionaryLabel runat="server" ID="lblIsShowResult" DictionaryType="Dic_TrueOrFalse"></cc1:DictionaryLabel>
                </td>
            </tr>
            <tr>
                <th>
                    �ʾ�״̬��
                </th>
                <td>
                    <cc1:DictionaryLabel runat="server" ID="lblState" DictionaryType="Dic_Status"></cc1:DictionaryLabel>
                </td>
            </tr>
            <tr>
                <th>
                    ����״̬��
                </th>
                <td>
                    <cc1:DictionaryLabel runat="server" ID="lblIsPublish" DictionaryType="Dic_PublishStatus"></cc1:DictionaryLabel>
                </td>
            </tr>
            <tr>
                <th>
                    �����ˣ�
                </th>
                <td>
                    <asp:Label runat="server" ID="lblDutyUser"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    �����ף�
                </th>
                <td>
                    <asp:Label runat="server" ID="lblHeader"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    �����
                </th>
                <td>
                    <asp:Label runat="server" ID="lblFooter"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    ����˵����
                    <br />
                    (����Ա����ʹ��,<br />
                    ����ѧԱ����ʾ)
                </th>
                <td>
                    <asp:Label runat="server" ID="lblRemark"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    �����ˣ�
                </th>
                <td>
                    <asp:Label runat="server" ID="lblCreator"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    ����ʱ�䣺
                </th>
                <td>
                    <cc1:DateTimeLabel runat="server" ID="lblCreateTime"></cc1:DateTimeLabel>
                </td>
            </tr>
        </table>
    </asp:View>
</cc1:CustomMuliView>
