<%@ Control Language="C#" AutoEventWireup="true" CodeFile="QuSelectionEdit.ascx.cs"
    Inherits="Poll_QuestionManage_Controls_QuSelectionEdit" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/ChooseCourseDropdown.ascx" TagName="ChooseCourseDropdown"
    TagPrefix="uc1" %>

<div class="dv_information">
    <!--表单录入-->
<%--    <div style="padding-left: 10px!important; *padding-left: 6px;">
    </div>--%>
    <table class="GridviewGray GridviewGrayboder">
        <tr>
            <asp:Button runat="server" ID="btnAdd" CssClass="btn_AddNew" OnClick="btnAdd_Click"
                Text="增加选项"></asp:Button>
        </tr>
        <tr>
            <th class="center" style="width: 50px">
                序号
            </th>
            <th class="center widthauto">
                选项
            </th>
            <th class="center" style="width: 50px">
                操作
            </th>
        </tr>
        <asp:Repeater ID="rpOptions" runat="server">
            <ItemTemplate>
                <tr>
                    <td>
                        <%# Convert.ToChar(65 + Container.ItemIndex)%>
                    </td>
                    <td class="aligncenter">
                        <asp:TextBox runat="server" ID="txtOptionName" TextMode="MultiLine" Height="40" SkinID="Text300"
                            Width="330" Text='<%#Eval("OptionName") %>'></asp:TextBox>
                        <span style="color: Red;">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidatorLoginName"
                            Text="&nbsp;" Display="None" runat="server" ErrorMessage='<%# string.Format("请填写选项{0}内容！", Convert.ToChar(65 + Container.ItemIndex))%>'
                            ControlToValidate="txtOptionName" ValidationGroup="Edit"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtOptionName"
                            ValidationExpression="^(\s|\S){0,1024}$" Display="None" ErrorMessage="选项字数最多不能超过1024个字符！"
                            ValidationGroup="Edit" />
                    </td>
                    <td>
                        <cc1:CustomLinkButton ID="CustomLinkButton1" runat="server" EnableConfirm="true"
                            ConfirmMessage="确定要删除？" OnCommand="btnDelete_Command" CommandArgument='<%#Eval("OptionID")%>'
                            ConfirmTitle="选项删除">删除</cc1:CustomLinkButton>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
        <tr>
            <td>
            </td>
            <td class="alignleft">
                <asp:RadioButtonList runat="server" ID="rblOther" RepeatDirection="horizontal" RepeatLayout="Flow">
                    <asp:ListItem Text="仅上述可选被选中" Value="0" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="包含“其他”输入框" Value="1"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td>
            </td>
        </tr>
    </table>
</div>
<asp:ValidationSummary runat="server" ID="validationSummary1" ValidationGroup="Edit"
    ShowMessageBox="true" ShowSummary="false" />
<!--提交表单-->
<div class="dv_submit" style="display: none">
    <asp:Button Text="保存" ID="lbtnSave" runat="server" CssClass="btn_Save" OnClick="lbtnSave_Click"
        ValidationGroup="Edit"></asp:Button>
    <input value="取消" type="button" onclick="javascript:closeWindow();" class="btn_Cancel padleft10" />
</div>
