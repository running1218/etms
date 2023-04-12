<%@ Page Language="C#" AutoEventWireup="true" Inherits="Admin_FunctionGroup_Function_Default"
    MasterPageFile="~/MasterPages/MPageAdmin.master" CodeFile="Default.aspx.cs" %>

<%@ Register Src="../Controls/FunctionGroupTree.ascx" TagName="FunctionGroupTree"
    TagPrefix="uc1" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content runat="server" ID="PageContent" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="dv_searchlist">
        <table>
            <!-- 导航条 -->
            <tr>
                <td>
                    功能组结构
                </td>
                <td>
                    功能列表
                </td>
            </tr>
            <tr>
                <td align="left" valign="top" style="width: 20%;">
                    <!-- 管理者分级列表 -->
                    <uc1:FunctionGroupTree ID="FunctionGroupTree1" runat="server" RedirectUrlFormat="../FunctionGroup_Function/Default.aspx?groupid={0}">
                    </uc1:FunctionGroupTree>
                </td>
                <td align="left" valign="top">
                    <!--翻页-->
                    <div class="dv_pagePanel">
                        <div class="dv_selectAll">
                            <input type="button" class="btn_Add" value="新增" onclick="showWindow('功能信息','<%=this.ActionHref(string.Format("View.aspx?op=add&id={0}&groupid={0}",Request.QueryString["groupid"]))%>')" />
                        </div>
                        <div class="dv_pageControl">
                            <uc2:PageSet ID="PageSet1" runat="server" />
                        </div>
                    </div>
                    <!--成员分级列表 -->
                    <cc1:CustomGridView ID="gvRoles" runat="server" AutoGenerateColumns="False" CustomAllowPaging="False"
                        ShowFooter="False">
                        <Columns>
                            <asp:TemplateField HeaderText="序号">
                                <HeaderStyle Width="40" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="LabNo" runat="server" Text='<%# ((this.PageSet1.PageIndex -1) * this.PageSet1.PageSize) + Container.DataItemIndex+1 %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="功能名称" DataField="FunctionName" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft" />
                            <asp:TemplateField HeaderText="状态" HeaderStyle-Width="40">
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblState" Text='<%#(1==(int)Eval("Status"))?"启用":"停用" %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="顺序号" DataField="OrderNo" HeaderStyle-Width="60">
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField ShowHeader="False" HeaderText="URL管理" HeaderStyle-Width="60">
                                <ItemTemplate>
                                    <a href='javascript:showWindow("功能URL信息","<%# this.ActionHref(String.Format("../Function_URL/Default.aspx?id={0}&groupid={1}", new Object[]{Eval("FunctionID"),Eval("FunctionGroupID")})) %>")'>
                                        管理</a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="False" HeaderText="操作" HeaderStyle-Width="100">
                                <ItemTemplate>
                                    <a href='javascript:showWindow("功能信息","<%# this.ActionHref(String.Format("View.aspx?op=edit&id={0}&groupid={1}", new Object[]{Eval("FunctionID"),Eval("FunctionGroupID")})) %>")'>
                                        编辑</a> <a href='javascript:showWindow("功能信息","<%# this.ActionHref(String.Format("View.aspx?op=view&id={0}&groupid={1}", new Object[]{Eval("FunctionID"),Eval("FunctionGroupID")})) %>")'>
                                            查看</a> <a href='javascript:showWindow("功能信息","<%# this.ActionHref(String.Format("View.aspx?op=delete&id={0}&groupid={1}", new Object[]{Eval("FunctionID"),Eval("FunctionGroupID")})) %>")'>
                                                删除</a>
                                </ItemTemplate>
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
</asp:Content>
