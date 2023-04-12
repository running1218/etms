<%@ Control Language="C#" AutoEventWireup="true" CodeFile="QuFillBlanksEdit.ascx.cs"
    Inherits="Poll_QuestionManage_Controls_QuFillBlanksEdit" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<!--表单录入-->
<div class="dv_information">
    <table class="GridviewGray GridviewGrayboder">
        <tr>
            <th class="thleft" style="width:auto">
                题目<span style="color: Red;">*</span>
            </th>
        </tr>
        <tr>
            <td class="alignleft">
                <asp:TextBox ID="RichTextTitle" runat="server" TextMode="MultiLine" CssClass="inputbox_area520"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorLoginName" Text="&nbsp;" Display="None"
                    runat="server" ErrorMessage="请填写题目内容！" ControlToValidate="RichTextTitle" ValidationGroup="Edit"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="RichTextTitle"
                    ValidationExpression="^(\s|\S){0,1024}$" Display="None" ErrorMessage="题目字数最多不能超过1024个字符！"
                    ValidationGroup="Edit" />
            </td>
        </tr>
        <tr style="display:none">
            <th class="thleft" style="width:auto;padding:3px 0 0 4px;">
                <span class="floatleft" style="display:inline-block;margin-top:4px;">题目序号：</span>
                <asp:DropDownList runat="server" ID="ddlTitleNo">
                </asp:DropDownList>
            </th>
        </tr>
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