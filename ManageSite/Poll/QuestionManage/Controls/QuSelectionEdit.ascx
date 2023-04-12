<%@ Control Language="C#" AutoEventWireup="true" CodeFile="QuSelectionEdit.ascx.cs"
    Inherits="Poll_QuestionManage_Controls_QuSelectionEdit" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/ChooseCourseDropdown.ascx" TagName="ChooseCourseDropdown"
    TagPrefix="uc1" %>
<div class="dv_information">
    <!--表单录入-->
    <table class="GridviewGray GridviewGrayboder">
        <tr>
            <th class="thleft" style="padding:1px 5px!important;">
                <table class="nostyletable">
                    <tr>
                        <td style="width:70px!important;"><div style="width:70px!important;">试题类型：</div></td>
                        <td align="left"><asp:DropDownList runat="server" AutoPostBack="true" ID="ddl_TitleType" CssClass="select_120 valign-m">
                    <asp:ListItem Value="1">单选</asp:ListItem>
                    <asp:ListItem Value="2">多选</asp:ListItem>
                </asp:DropDownList></td>
                    </tr>
                </table>
            </th>
        </tr>
        <tr>
            <th class="thleft">
                题目<span style="color: Red;">*</span>
            </th>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="RichTextTitle" runat="server" TextMode="MultiLine" CssClass="inputbox_area520"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorLoginName" Text="&nbsp;" Display="None"
                    runat="server" ErrorMessage="请填写题目内容！" ControlToValidate="RichTextTitle" ValidationGroup="Edit"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="RichTextTitle"
                    ValidationExpression="^(\s|\S){0,1024}$" Display="None" ErrorMessage="题目字数最多不能超过1024个字符！"
                    ValidationGroup="Edit" />
            </td>
        </tr>
    </table>
    <%--<asp:LinkButton runat="server" ID="btnAdd" CssClass="btn_6" OnClick="btnAdd_Click"
        Text="+增加答案选项"></asp:LinkButton>--%>
    <div style="padding-left: 10px!important; padding-left: 6px;">
        <asp:Button runat="server" ID="btnAdd" CssClass="btn_AddNew" OnClick="btnAdd_Click"
            Text="增加答案选项"></asp:Button>
    </div>
    <table class="GridviewGray GridviewGrayboder">
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
                        <asp:TextBox runat="server" ID="txtOptionName" SkinID="Text300" Text='<%#Eval("OptionName") %>'></asp:TextBox>
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
    <table class="GridviewGray" style="display: none">
        <tr>
            <th style="width: 100px;">
                题目序号：
            </th>
            <td colspan="2" class="alignleft">
                <asp:DropDownList runat="server" ID="ddlTitleNo">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
</div>
<asp:ValidationSummary runat="server" ID="validationSummary1" ValidationGroup="Edit"
    ShowMessageBox="true" ShowSummary="false" />
<!--提交表单-->
<div class="dv_submit">
    <asp:Button Text="保存" ID="lbtnSave" runat="server" CssClass="btn_Save" OnClick="lbtnSave_Click"
        ValidationGroup="Edit"></asp:Button>
    <input value="取消" type="button" onclick="javascript:closeWindow();" class="btn_Cancel padleft10" />
</div>
