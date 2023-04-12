<%@ Page Language="C#" AutoEventWireup="true" Inherits="Admin_FunctionGroup_Default"
    MasterPageFile="~/MasterPages/MPageAdmin.master" CodeFile="Default.aspx.cs" %>

<%@ Register Src="../Controls/FunctionGroupTree.ascx" TagName="FunctionGroupTree"
    TagPrefix="uc1" %>
<%@ Register Src="../Controls/TreeNavigationTool.ascx" TagName="TreeNavigationTool"
    TagPrefix="uc1" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content runat="server" ID="PageContent" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="dv_searchlist">
        <!--查找条件-->
        <table>
            <!-- 导航条 -->
            <tr>
                <td>
                    功能组结构
                </td>
                <td>
                    下级功能组管理
                </td>
            </tr>
            <tr>
                <td align="left" valign="top" style="width: 20%;">
                    <!-- 管理者分级列表 -->
                    <uc1:FunctionGroupTree ID="FunctionGroupTree1" runat="server" RedirectUrlFormat="../FunctionGroup/Default.aspx?groupid={0}">
                    </uc1:FunctionGroupTree>
                </td>
                <td align="left" valign="top">
                    <div>
                        <!--翻页-->
                        <div class="dv_pagePanel">
                            <div class="dv_selectAll">
                                <input type="button" class="btn_Add" value="新增" onclick="showWindow('功能组信息','<%=this.ActionHref(string.Format("View.aspx?op=add&id={0}&parentid={1}",(Request.QueryString["groupid"]??"0"),Request.QueryString["groupid"]))%>',600,440)" />
                            </div>
                            <div class="dv_pageControl">
                                <uc2:PageSet ID="PageSet1" runat="server" />
                            </div>
                        </div>
                        <cc1:CustomGridView ID="gvRoles" runat="server" AutoGenerateColumns="False" CustomAllowPaging="False"
                            ShowFooter="false">
                            <Columns>
                                <asp:TemplateField HeaderText="序号">
                                    <HeaderStyle Width="40" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="LabNo" runat="server" Text='<%# ((this.PageSet1.PageIndex -1) * this.PageSet1.PageSize) + Container.DataItemIndex+1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="功能组名称" DataField="NodeName" HeaderStyle-CssClass="alignleft"
                                    ItemStyle-CssClass="alignleft" />
                                <asp:TemplateField HeaderText="状态" HeaderStyle-Width="40">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblState" Text='<%#((bool)Eval("IsStateOpen"))?"启用":"停用" %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="顺序号" DataField="OrderNo" HeaderStyle-Width="60">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="子功能组管理" HeaderStyle-Width="80">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%#this.ActionHref(string.Format("default.aspx?groupid={0}&parentid={1}",Eval("NodeID"),Eval("ParentNodeID")))%>'
                                            Text='<%#string.Format("[{0}]管理",((IList<ETMS.Components.Basic.API.Entity.Common.Node>)Eval("ChildNodes")).Count) %>'></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="False" HeaderText="操作" HeaderStyle-Width="130">
                                    <ItemTemplate>
                                        <a href='javascript:showWindow("功能组信息","<%# this.ActionHref(String.Format("View.aspx?op=edit&id={0}&parentid={1}", new Object[]{Eval("NodeID"),Eval("ParentNodeID")})) %>",600,700)'>
                                            编辑</a> <a href='javascript:showWindow("功能组信息","<%# this.ActionHref(String.Format("View.aspx?op=view&id={0}&parentid={1}", new Object[]{Eval("NodeID"),Eval("ParentNodeID")})) %>",600,500)'>
                                                查看</a> <a href='javascript:showWindow("功能组信息","<%# this.ActionHref(String.Format("View.aspx?op=delete&id={0}&parentid={1}", new Object[]{Eval("NodeID"),Eval("ParentNodeID")})) %>",600,500)'>
                                                    删除</a>
                                        <cc1:CustomLinkButton runat="server" ID="lkSwitchStatus" Text='<%#0==(int)Eval("State")?"启用":"停用" %>'
                                            OnCommand="OrganizationOpeator_Command" EnableConfirm="true" ConfirmMessage='<%#string.Format("确认要{0}该功能组？",0==(int)Eval("State")?"启用":"停用") %>'
                                            CommandName="SwitchStatus" CommandArgument='<%#Eval("NodeID") %>'></cc1:CustomLinkButton>
                                        <%--<a href='javascript:showWindow("功能组信息","<%# this.ActionHref(String.Format("MoveFunctionGroup.aspx?id={0}", new Object[]{Eval("NodeID")})) %>")'
                                            >调整</a>--%>
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
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
