<%@ Page Language="C#" AutoEventWireup="true" Inherits="ETMS.WebApp.Manage.Security.Department.Default"
    MasterPageFile="~/MasterPages/MPageAdmin.master" CodeFile="Default.aspx.cs" %>

<%@ Register Src="../Controls/DepartmentTree.ascx" TagName="DepartmentTree" TagPrefix="uc1" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="../../Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content runat="server" ID="PageContent" ContentPlaceHolderID="ContentPlaceHolder1">
    <div>
        <div class="dv_searchlist">
            <table width="100%">
                <!-- 导航条 -->
                <tr>
                    <td>
                    </td>
                    <td>
                        下级<asp:Literal ID="ltlDepartment" runat="server" Text="<%$ Resources:UIResource, ui_department%>"></asp:Literal>管理
                    </td>
                </tr>
                <tr>
                    <td align="left" valign="top" style="width: 20%;">
                        <!-- 管理者分级列表 -->
                        <uc1:DepartmentTree ID="DepartmentTree1" runat="server" RedirectUrlFormat="~/Security/Department/Default.aspx?departmentid={0}">
                        </uc1:DepartmentTree>
                    </td>
                    <td align="left" valign="top">
                        <!--翻页-->
                        <div class="dv_pagePanel">
                            <div class="dv_selectAll">
                                <input type="button" class="btn_Add" value="新增" onclick="showWindow('新增','<%=this.ActionHref(string.Format("View.aspx?op=add&id={0}&parentid={1}",(Request.QueryString["departmentid"]??"0"),(Request.QueryString["departmentid"]??"0")))%>')" />
                            </div>
                            <div class="dv_pageControl">
                                <uc2:PageSet ID="PageSet1" runat="server" />
                            </div>
                        </div>
                        <cc1:CustomGridView ID="gvRoles" runat="server" AutoGenerateColumns="False" CustomAllowPaging="false"
                            OnRowDataBound="gvRoles_RowDataBound" ShowFooter="false">
                            <Columns>
                                <asp:TemplateField HeaderText="序号" HeaderStyle-Width="40">
                                    <HeaderStyle Width="40" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="LabNo" runat="server" Text='<%# ((this.PageSet1.PageIndex -1) * this.PageSet1.PageSize) + Container.DataItemIndex+1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="<%$ Resources:UIResource, ui_department%>" DataField="NodeName"  ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft"/>
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
                                <asp:TemplateField HeaderText="" HeaderStyle-Width="80">
                                    <HeaderTemplate>
                                        子<asp:Literal ID="ltlSubDepartment" runat="server" Text="<%$ Resources:UIResource, ui_department%>"></asp:Literal>管理
                                    </HeaderTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%#this.ActionHref(string.Format("default.aspx?orgid={0}&departmentid={1}",Eval("OrganizationID"),Eval("NodeID")))%>'></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="False" HeaderText="操作" HeaderStyle-Width="180">
                                    <ItemTemplate>
                                        <a href='javascript:showWindow("修改信息","<%# this.ActionHref(String.Format("View.aspx?op=edit&id={0}&parentid={1}", new Object[]{Eval("NodeID"),Eval("ParentNodeID")})) %>")'>
                                            编辑</a> <a href='javascript:showWindow("查看信息","<%# this.ActionHref(String.Format("View.aspx?op=view&id={0}&parentid={1}", new Object[]{Eval("NodeID"),Eval("ParentNodeID")})) %>",700,400)'>
                                                查看</a> <a href='javascript:showWindow("删除信息","<%# this.ActionHref(String.Format("View.aspx?op=delete&id={0}&parentid={1}", new Object[]{Eval("NodeID"),Eval("ParentNodeID")})) %>",700,400)'>
                                                    删除</a>
                                        <cc1:CustomLinkButton runat="server" ID="lkSwitchStatus" Text='<%#0==(int)Eval("State")?"启用":"停用" %>'
                                            OnCommand="DepartmentOpeator_Command" EnableConfirm="true" ConfirmMessage='<%#string.Format("确认要{0}？",0==(int)Eval("State")?"启用":"停用") %>'
                                            CommandName="SwitchStatus" CommandArgument='<%#Eval("NodeID") %>'></cc1:CustomLinkButton>
                                        <a href='javascript:showWindow("调整信息","<%# this.ActionHref(String.Format("MoveDepartment.aspx?id={0}", new Object[]{Eval("NodeID")})) %>")'>
                                            调整</a>
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
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
