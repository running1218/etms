<%@ Control Language="C#" AutoEventWireup="true" CodeFile="QuSelectionInfo.ascx.cs"
    Inherits="Poll_QuestionManage_Controls_QuSelectionInfo" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/ChooseCourseDropdown.ascx" TagName="ChooseCourseDropdown"
    TagPrefix="uc1" %>
<div class="dv_information">
    <!--表单录入-->
    <table class="GridviewGray GridviewGrayboder">
        <tr>
            <th class="thleft">
                题目:
            </th>
        </tr>
        <tr>
            <td class="alignleft">
                <asp:Label ID="RichTextTitle" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    <table class="GridviewGray GridviewGrayboder">
        <tr>
            <th class="center">
                序号
            </th>
            <th class="center">
                选项
            </th>
        </tr>
        <asp:Repeater ID="rpOptions" runat="server">
            <ItemTemplate>
                <tr>
                    <td>
                        <%# Convert.ToChar(65 + Container.ItemIndex)%>
                    </td>
                    <td class="alignleft">
                        <%#Eval("OptionName") %>
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
        </tr>
    </table>
    <table class="GridviewGray">
        <tr>
            <th style="width: 100px;">
                题目序号：
            </th>
            <td class="alignleft">
                <asp:Label runat="server" ID="lblTitleNo"></asp:Label>
            </td>
        </tr>
    </table>
</div>
<!--提交表单-->
<div class="dv_submit">
    <a href="javascript:closeWindow();" class="btn_Cancel padleft10">关闭</a>
</div>
