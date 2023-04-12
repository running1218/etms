<%@ Page Language="C#" AutoEventWireup="true" Inherits="Admin_InnerRole_Default"
    MasterPageFile="~/MasterPages/MPageAdmin.master" CodeFile="Default.aspx.cs" %>

<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<asp:Content runat="server" ID="PageContent" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="dv_searchlist">
        <!--翻页-->
        <div class="dv_pagePanel">
            <div class="dv_selectAll">
                <input type="button" class="btn_Add" value="新增" onclick="showWindow('角色基本信息','<%=this.ActionHref("View.aspx?op=add&id=0")%>',600,320)" />
            </div>
            <div class="dv_pageControl">
                <uc2:PageSet ID="PageSet1" runat="server" />
            </div>
        </div>
        <cc1:CustomGridView ID="gvRoles" runat="server" AutoGenerateColumns="False" EmptyDataSourceTip="没有任何记录！"
            ShowFooter="False">
            <Columns>
                <asp:TemplateField HeaderText="序号">
                    <HeaderStyle Width="40" />
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="LabNo" runat="server" Text='<%# ((this.PageSet1.PageIndex -1) * this.PageSet1.PageSize) + Container.DataItemIndex+1 %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="NodeName" HeaderText="角色名称" SortExpression="NodeName" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <%--   <asp:TemplateField HeaderText="角色类型">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblRoleType" Text='<%# 0==(int)Eval("OrganizationID")?"内置":"自建" %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:TemplateField HeaderText="状态" HeaderStyle-Width="40">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="LabStatus" runat="server" Text='<%#(int)Eval("State")==1?"启用":"停用" %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="权限" HeaderStyle-Width="80">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <a href='javascript:showWindow("角色权限设置","<%# this.ActionHref(String.Format("../InnerRole_FunctionGroup/default.aspx?id={0}", new Object[]{Eval("NodeID")})) %>")'>
                            设置</a> <a href='javascript:showWindow("角色权限查看","<%# this.ActionHref(String.Format("../InnerRole_FunctionGroup/View.aspx?id={0}", new Object[]{Eval("NodeID")})) %>")'>
                                查看</a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False" HeaderText="操作" HeaderStyle-Width="100">
                    <ItemTemplate>
                        <a href='javascript:showWindow("角色基本信息","<%# this.ActionHref(String.Format("View.aspx?op=edit&id={0}&parentid={1}", new Object[]{Eval("NodeID"),Eval("ParentNodeID")})) %>",600,0)'>
                            编辑</a> <a href='javascript:showWindow("角色基本信息","<%# this.ActionHref(String.Format("View.aspx?op=delete&id={0}&parentid={1}", new Object[]{Eval("NodeID"),Eval("ParentNodeID")})) %>",600,360)'>
                                删除</a> <a href='javascript:showWindow("角色基本信息","<%# this.ActionHref(String.Format("View.aspx?op=view&id={0}&parentid={1}", new Object[]{Eval("NodeID"),Eval("ParentNodeID")})) %>",600,360)'>
                                    查看</a>
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
