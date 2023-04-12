<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Admin_Site_User_Default"
    MasterPageFile="~/MasterPages/MPageAdmin.master" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content runat="server" ID="PageContent" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="dv_searchbox">
        <table class="GridviewGray">
            <tr>
                <th width="120">
                    用户账户：
                </th>
                <td>
                    <cc1:CustomTextBox ButtonControlId="btn_Search" ID="txtLoginName" runat="server"
                        CssClass="inputbox_120" />
                    <asp:Button ID="btn_Search" runat="server" Text="查询" OnClick="btn_Search_Click" SkinID="Search" />
                    <a href="javascript:hideGridview()" class="dropdownico" id="Highsearch">高级搜索</a>
                </td>
            </tr>
            <tr>
                <th>
                    用户姓名：
                </th>
                <td>
                    <cc1:CustomTextBox ButtonControlId="btn_Search" ID="txtRealName" CssClass="inputbox_120"
                        runat="server" />
                </td>
            </tr>
        </table>
        <div class="center">
        </div>
    </div>
    <div class="dv_searchlist">
        <!--翻页-->
        <div class="dv_pagePanel">
            <div class="dv_selectAll">
                <cc1:CheckBoxsController runat="server" ID="chkboxControler" ContainerID="GridViewList" />
                <%if (IsDisplay)
                  { %>
                <input type="button" class="btn_Add" value="新增" onclick="showWindow('用户基本信息','<%=this.ActionHref("View.aspx?op=add&id=0")%>', 600, 450)" />
                <%} %>
                <cc1:CustomButton CssClass="btn_Repassword" Text="重置密码" runat="server" ID="btnReset"
                    UseSubmitBehavior="false" EnableConfirm="true" ConfirmMessage="确信要执行“批量重置”操作吗?"
                    OnClick="btnReset_Click" />
            </div>
            <div class="dv_pageControl">
                <uc2:PageSet ID="PageSet1" runat="server" />
            </div>
        </div>
        <cc1:CustomGridView ID="GridViewList" runat="server" AutoGenerateColumns="False"
            DataKeyNames="UserID" OnRowDataBound="GridViewList_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderStyle-Width="18">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="用户账户" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                    <ItemTemplate>
                        <cc1:ShortTextLabel Text='<%#Eval("LoginName")%>' runat="server" ShowTextNum="20"></cc1:ShortTextLabel>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="用户姓名" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft field17" >
                    <ItemTemplate>
                        <cc1:ShortTextLabel Text='<%#Eval("RealName")%>' runat="server" ShowTextNum="15"></cc1:ShortTextLabel>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="状态" HeaderStyle-Width="40">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="LabStatus" runat="server" Text='<%#(int)Eval("Status")==1?"启用":"停用" %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="是否超级管理员" HeaderStyle-Width="100">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="LabIsAdmin" runat="server" Text='<%#(bool)Eval("IsSysAccount")?"是":"否" %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False" HeaderText="分配角色" HeaderStyle-Width="70">
                    <ItemTemplate>
                        <a <%# (bool)Eval("IsSysAccount")?"disabled onclick='return false;' class='link_colorGray'":"" %>
                            href='javascript:showWindow("用户角色信息","<%# this.ActionHref(String.Format("../UserRole/View.aspx?op=edit&id={0}", new Object[]{Eval("UserID")})) %>")'>
                            分配</a> <a href='javascript:showWindow("用户角色信息","<%# this.ActionHref(String.Format("../UserRole/View.aspx?op=view&id={0}", new Object[]{Eval("UserID")})) %>")'>
                                查看</a>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False" HeaderText="分配权限" HeaderStyle-Width="70">
                    <ItemTemplate>
                        <a <%# (bool)Eval("IsSysAccount")?"disabled onclick='return false;' class='link_colorGray'":"" %>
                            href='javascript:showWindow("用户角色信息","<%# this.ActionHref(String.Format("../User_FunctionGroup/default.aspx?id={0}", new Object[]{Eval("UserID")})) %>")'>
                            授权</a> <a href='javascript:showWindow("用户角色信息","<%# this.ActionHref(String.Format("../User_FunctionGroup/View.aspx?id={0}", new Object[]{Eval("UserID")})) %>")'>
                                查看</a>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False" HeaderText="操作" HeaderStyle-Width="200">
                    <ItemTemplate>
                        <a <%# (bool)Eval("IsSysAccount")?"disabled onclick='return false;' class='link_colorGray'":"" %>
                            href='javascript:showWindow("用户基本信息","<%# this.ActionHref(String.Format("View.aspx?op=edit&id={0}", new Object[]{Eval("UserID")})) %>")'>
                            编辑</a> <a href='javascript:showWindow("用户基本信息","<%# this.ActionHref(String.Format("View.aspx?op=view&id={0}", new Object[]{Eval("UserID")})) %>")'>
                                查看</a>
                        <cc1:CustomLinkButton runat="server" ID="lkReset" Text="重置密码" OnCommand="UserOpeator_Command"
                            EnableConfirm="true" ConfirmMessage="确认要重置该用户密码？" CommandName="Reset" CommandArgument='<%#Eval("UserID") %>'></cc1:CustomLinkButton>
                        <cc1:CustomLinkButton runat="server" ID="lkSwitchStatus" Text='<%#0==(int)Eval("Status")?"启用":"停用" %>'
                            OnCommand="UserOpeator_Command" EnableConfirm="true" ConfirmMessage='<%#string.Format("确认要{0}该用户？",0==(int)Eval("Status")?"启用":"停用") %>'
                            CommandName="SwitchStatus" CommandArgument='<%#Eval("UserID") %>'></cc1:CustomLinkButton>
                        <cc1:CustomLinkButton runat="server" ID="CustomLinkButton1" Enabled='<%# !((bool)Eval("IsSysAccount"))%>'
                            CssClass='<%# ((bool)Eval("IsSysAccount"))?"link_colorGray":""%>' Text='删除' OnCommand="UserOpeator_Command"
                            EnableConfirm="true" ConfirmMessage='确认要删除该用户信息？' CommandName="Remove" CommandArgument='<%#Eval("UserID") %>'></cc1:CustomLinkButton>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
        </cc1:CustomGridView>
        <!--列表 end-->
        <div class="dv_splitLine">
        </div>
        <!--翻页-->
        <div class="dv_pagePanel">
        </div>
    </div>
</asp:Content>
