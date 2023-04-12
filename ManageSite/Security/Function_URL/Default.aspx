<%@ Page Language="C#" AutoEventWireup="true" Inherits="Admin_Function_URL_Default"
    MasterPageFile="~/MasterPages/MPagePop.master" CodeFile="Default.aspx.cs" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<asp:Content runat="server" ID="PageContent" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="dv_information">
        <div class="dv_searchlist">
            <!--翻页-->
            <cc1:CustomGridView ID="gvRoles" runat="server" AutoGenerateColumns="False" CustomAllowPaging="False"
                ShowFooter="False">
                <Columns>
                    <asp:TemplateField HeaderText="序号">
                        <HeaderStyle Width="40" />
                        <ItemTemplate>
                            <asp:Label ID="LabNo" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="功能URL" DataField="PageURL" HeaderStyle-CssClass="alignleft"
                        ItemStyle-CssClass="alignleft" />
                    <asp:TemplateField HeaderText="是否首页" HeaderStyle-Width="60px">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblState" Text='<%#(1==(int)Eval("IsMainPage"))?"是":"否" %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False" HeaderText="操作" HeaderStyle-Width="100px">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" ID="lkbtnEdit" CommandArgument='<%# Eval("PageID") %>'
                                Text="修改" CommandName="Edit1" OnCommand="lkbtnEdit_Command"></asp:LinkButton>
                            <cc1:CustomLinkButton runat="server" ID="lkbtnDelete" CommandArgument='<%# Eval("PageID") %>'
                                Text="删除" CommandName="Delete1" EnableConfirm="true" ConfirmMessage="确认要删除？"
                                OnCommand="lkbtnEdit_Command"></cc1:CustomLinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </cc1:CustomGridView>
            <!--列表 end-->
            <div class="dv_splitLine">
            </div>
        </div>
        <table class="GridviewGray">
            <tr>
                <th>
                    功能URL:
                </th>
                <td>
                    <asp:HiddenField runat="server" ID="txtPageID" />
                    <asp:TextBox runat="server" ID="txtUrl" CssClass="inputbox_300"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvTextBoxRoleName" runat="server" Text="*" ErrorMessage="*请填写URL"
                        Display="dynamic" ValidationGroup="Edit" ControlToValidate="txtUrl"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th>
                    是否首页：
                </th>
                <td>
                    <asp:RadioButtonList runat="server" ID="rbtnlistIsMainPage" RepeatDirection="horizontal"
                        RepeatLayout="Flow">
                        <asp:ListItem Text="是" Value="1"></asp:ListItem>
                        <asp:ListItem Text="否" Value="0" Selected="true"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <th>
                    备注：
                </th>
                <td>
                    <asp:TextBox runat="server" ID="txtRemark" TextMode="MultiLine" SkinID="textarea" />
                </td>
            </tr>
            <tr>
                <th>
                    说明：
                </th>
                <td>
                    “是否首页”是用于用户登陆后看到的左边功能菜单项对应的URL链接，一个功能下仅允许一个设置为首页的URL启作用。
                    <br />
                    注意：
                    <br />
                    1、如果功能下没有一个URL设置为首页，则此功能不在用户功能菜单项显示！
                    <br />
                    2、URL格式支持两种，范例：
                    <br />
                    A：Admin/Function_URL/Default.aspx －－－＞指定具体的页面地址允许被访问
                    <br />
                    B：Admin/Function_URL/** －－－＞指定/Admin/Function_URL/路径下的所有页面都可以被访问
                    <br />
                    <b>如果URL将用于功能首页，则必须使用URL格式中的第一种！</b>
                </td>
            </tr>
        </table>
    </div>
    <div class="dv_submit">
        <asp:Button runat="server" ID="btnAdd" SkinID="Ok" ValidationGroup="Edit" OnClick="btnAdd_Click" />
        <asp:Button ID="ImageButtonReturn" runat="server" SkinID="Return" Text="取消" OnClientClick="closeWindow()"
            CausesValidation="False" /></div>
</asp:Content>
