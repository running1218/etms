<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserControl.ascx.cs" Inherits="Poll_ResourceQuery_UserControl" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<cc1:CustomMuliView runat="server" ID="Views">
    <asp:View runat="server" ID="View_Edit">
        <table class="GridviewGray">
            <tr>
                <th>
                    调查名称：
                </th>
                <td>
                    <asp:TextBox ID="txtQueryName" runat="server" SkinID="Text300" MaxLength="100"></asp:TextBox><span
                        style="color: Red;">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidatorLoginName"
                            Text="&nbsp;" Display="None" runat="server" ErrorMessage="请填写调查名称！" ControlToValidate="txtQueryName"
                            ValidationGroup="Edit"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th>
                    调查时间：
                </th>
                <td>
                    <cc1:DateTimeTextBox runat="server" ID="txtBeginTime" EndTimeControlID="txtEndTime"></cc1:DateTimeTextBox><span
                        style="color: Red;">*</span>至&nbsp;<cc1:DateTimeTextBox runat="server" ID="txtEndTime"
                            BeginTimeControlID="txtBeginTime"></cc1:DateTimeTextBox>
                    <span style="color: Red;">*</span>
                    <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator1" runat="server"
                        ErrorMessage="请填写调查开始时间！" ControlToValidate="txtBeginTime" ValidationGroup="Edit"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidatorRealName" runat="server"
                        ErrorMessage="请填写调查结束时间！" ControlToValidate="txtBeginTime" ValidationGroup="Edit"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th>
                    查看结果：
                </th>
                <td>
                    <cc1:DictionaryRadioButtonList runat="server" ID="rblIsAllShowResult" CheckedValue="0"
                        DictionaryType="Dic_TrueOrFalse" />
                </td>
            </tr>
            <tr>
                <th>
                    问卷状态：
                </th>
                <td>
                    <cc1:DictionaryRadioButtonList runat="server" ID="rblStatus" CheckedValue="1" DictionaryType="Dic_Status" />
                </td>
            </tr>
            <tr>
                <th>
                    负责人：
                </th>
                <td>
                    <asp:TextBox ID="txtDutyUser" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>
                    开场白：
                </th>
                <td>
                    <asp:TextBox ID="txtHeader" runat="server" TextMode="MultiLine" SkinID="textarea"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtHeader"
                        ValidationExpression="^(\s|\S){0,1000}$" Display="None" ErrorMessage="开场白字数最多不能超过1000个字符！"
                        ValidationGroup="Edit" />
                </td>
            </tr>
            <tr>
                <th>
                    结束语：
                </th>
                <td>
                    <asp:TextBox ID="txtFooter" runat="server" TextMode="MultiLine" SkinID="textarea"
                        MaxLength="500"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtFooter"
                        ValidationExpression="^(\s|\S){0,500}$" Display="None" ErrorMessage="结束语字数最多不能超过500个字符！"
                        ValidationGroup="Edit" />
                </td>
            </tr>
            <tr>
                <th>
                    调查说明：
                    <br />
                    (管理员备案使用,<br />
                    不在学员端显示)
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
                    调查名称：
                </th>
                <td>
                    <asp:Label ID="lblQueryName" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    调查时间：
                </th>
                <td>
                    <cc1:DateTimeLabel ID="lblBeginTime" runat="server" />至<cc1:DateTimeLabel ID="lblEndTime"
                        runat="server" />
                </td>
            </tr>
            <tr>
                <th>
                    查看结果：
                </th>
                <td>
                    <cc1:DictionaryLabel runat="server" ID="lblIsShowResult" DictionaryType="Dic_TrueOrFalse"></cc1:DictionaryLabel>
                </td>
            </tr>
            <tr>
                <th>
                    问卷状态：
                </th>
                <td>
                    <cc1:DictionaryLabel runat="server" ID="lblState" DictionaryType="Dic_Status"></cc1:DictionaryLabel>
                </td>
            </tr>
            <tr>
                <th>
                    发布状态：
                </th>
                <td>
                    <cc1:DictionaryLabel runat="server" ID="lblIsPublish" DictionaryType="Dic_PublishStatus"></cc1:DictionaryLabel>
                </td>
            </tr>
            <tr>
                <th>
                    负责人：
                </th>
                <td>
                    <asp:Label runat="server" ID="lblDutyUser"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    开场白：
                </th>
                <td>
                    <asp:Label runat="server" ID="lblHeader"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    结束语：
                </th>
                <td>
                    <asp:Label runat="server" ID="lblFooter"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    调查说明：
                    <br />
                    (管理员备案使用,<br />
                    不在学员端显示)
                </th>
                <td>
                    <asp:Label runat="server" ID="lblRemark"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    创建人：
                </th>
                <td>
                    <asp:Label runat="server" ID="lblCreator"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    创建时间：
                </th>
                <td>
                    <cc1:DateTimeLabel runat="server" ID="lblCreateTime"></cc1:DateTimeLabel>
                </td>
            </tr>
        </table>
    </asp:View>
</cc1:CustomMuliView>
