<%@ Control Language="C#" AutoEventWireup="true" CodeFile="QuFillBlanksEdit.ascx.cs"
    Inherits="Poll_QuestionManage_Controls_QuFillBlanksEdit" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/QS/Controls/QuSelectionEdit1.ascx" TagName="QuSelectionEdit"
    TagPrefix="uc2" %>
<!--表单录入-->
<div class="dv_information">
    <table class="GridviewGray GridviewGrayboder">
        <tr>
            <th class="thleft" colspan="3" style="width: auto">
                题型
                <asp:DropDownList runat="server" AutoPostBack="true" ID="ddl_OrganizationID" CssClass="select_120"
                    OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                </asp:DropDownList>
            </th>
        </tr>
        <tr>
            <th class="thleft" colspan="3" style="width: auto">
                题目名称<span style="color: Red;">*</span>
            </th>
        </tr>
        <tr>
            <td class="alignleft" colspan="3">
                <asp:TextBox ID="RichTextTitle" runat="server" TextMode="MultiLine" CssClass="inputbox_area520"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorLoginName" Text="&nbsp;" Display="None"
                    runat="server" ErrorMessage="请填写题目内容！" ControlToValidate="RichTextTitle" ValidationGroup="Edit"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="RichTextTitle"
                    ValidationExpression="^(\s|\S){0,1024}$" Display="None" ErrorMessage="题目字数最多不能超过1024个字符！"
                    ValidationGroup="Edit" />
            </td>
        </tr>
        <asp:Panel ID="pannel1" runat="server">
            <tr style="display: none">
                <th class="thleft" style="width: auto">
                    最少选择选项数
                    <asp:DropDownList runat="server" ID="DropDownListDingle" CssClass="select_120">
                        <asp:ListItem Text="0" Value="0"></asp:ListItem>
                        <asp:ListItem Text="1" Value="1" Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList runat="server" ID="DropDownListDingle1" Visible="false" CssClass="select_120">
                        <asp:ListItem Text="0" Value="0"></asp:ListItem>
                        <asp:ListItem Text="1" Value="1" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="2" Value="2"></asp:ListItem>
                        <asp:ListItem Text="3" Value="3"></asp:ListItem>
                        <asp:ListItem Text="4" Value="4"></asp:ListItem>
                        <asp:ListItem Text="5" Value="5"></asp:ListItem>
                        <asp:ListItem Text="6" Value="6"></asp:ListItem>
                        <asp:ListItem Text="7" Value="7"></asp:ListItem>
                        <asp:ListItem Text="8" Value="8"></asp:ListItem>
                        <asp:ListItem Text="9" Value="9"></asp:ListItem>
                        <asp:ListItem Text="10" Value="10"></asp:ListItem>
                    </asp:DropDownList>
                </th>
            </tr>
            <tr style="display: none">
                <th class="thleft" style="width: auto">
                    最多选择选项数
                    <asp:DropDownList runat="server" ID="txtSelect2" CssClass="select_120">
                        <asp:ListItem Text="0" Value="0"></asp:ListItem>
                        <asp:ListItem Text="1" Value="1" Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList runat="server" ID="DropDownList2" Visible="false" CssClass="select_120">
                        <asp:ListItem Text="0" Value="0"></asp:ListItem>
                        <asp:ListItem Text="1" Value="1"></asp:ListItem>
                        <asp:ListItem Text="2" Value="2"></asp:ListItem>
                        <asp:ListItem Text="3" Value="3"></asp:ListItem>
                        <asp:ListItem Text="4" Value="4"></asp:ListItem>
                        <asp:ListItem Text="5" Value="5" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="6" Value="6"></asp:ListItem>
                        <asp:ListItem Text="7" Value="7"></asp:ListItem>
                        <asp:ListItem Text="8" Value="8"></asp:ListItem>
                    </asp:DropDownList>
                </th>
            </tr>
            <tr>
                <td colspan="3" style="text-align: left">
                    <asp:Button runat="server" ID="Button1" CssClass="btn_AddNew" OnClick="btnAdd_Click"
                        Text="增加选项"></asp:Button>
                </td>
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
        </asp:Panel>
    </table>
    <asp:ValidationSummary runat="server" ID="validationSummary1" ValidationGroup="Edit"
        ShowMessageBox="true" ShowSummary="false" />
</div>
<!--提交表单-->
<div class="dv_submit">
    <asp:LinkButton ID="LinkButton1" runat="server" ValidationGroup="Edit" CssClass="btn_Save"
        OnClick="LinkButton1_Click">保存</asp:LinkButton>
    <a href="javascript:closeWindow();" class="btn_Cancel padleft10">取消</a>
</div>
