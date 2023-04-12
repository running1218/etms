<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/MPageAdmin.Master" CodeFile="RecommendTeacherList.aspx.cs" Inherits="SiteManage_RecommendTeacher_RecommendTeacherList" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <!--查找条件-->
        <div class="dv_searchbox">
            <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0" runat="server"
                id="tableQueryControlList">
                <tr>
                    <th width="120">讲师姓名：
                    </th>
                    <td width="130">
                        <asp:TextBox ID="txt_RealName" runat="server" CssClass="inputbox_120"></asp:TextBox>
                    </td>
                    <th width="120">讲师类型：
                    </th>
                    <td width="130">
                        <cc1:DictionaryDropDownList runat="server" ID="ddl_TeacherSourceID" DictionaryType="Dic_Sys_TeacherSource"
                            CssText="select_120" />
                        <asp:Button ID="btnSearch" CssClass="btn_Search" runat="server" Text="查询" OnClick="btnSearch_Click" />
                        <a href="javascript:hideGridview()" class="dropdownico" id="Highsearch">高级搜索</a>
                    </td>
                </tr>
                <tr>
                    <th>讲师状态：
                    </th>
                    <td colspan="3">
                        <cc1:DictionaryDropDownList runat="server" ID="ddl_Status" DictionaryType="Dic_Status"
                            CssText="select_120" />
                    </td>
                </tr>
            </table>
        </div>
        <!--查找结果-->
        <div class="dv_searchlist">
            <asp:UpdatePanel ID="upList" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <!--翻页-->
                    <div class="dv_pagePanel">
                        <div class="dv_selectAll">
                            <input type="button" class="btn_Add" value="新增" onclick="javascript: showWindow('新增推荐讲师', 'SetsTeacherAdd.aspx')" />
                            <input id="btnSort" type="button" class="btn_Sort" value="排序" title="课程资源排序" onclick="javascript: showWindow('推荐讲师排序', 'SetRecommendTeacherSort.aspx', 500, 450)" />
                        </div>
                        <div class="dv_pageControl">
                            <uc2:PageSet ID="PageSet1" runat="server" />
                        </div>
                    </div>
                    <!--列表 begin-->
                    <cc1:CustomGridView ID="gvList" runat="server" CssClass="GridviewGray" EmptyDataSourceTip="没有任何记录！"
                        AutoCreateColumnInsertIndex="0" AutoGenerateColumns="False" CurrentPageIndex="1"
                        CustomAllowPaging="False" IsEmpty="False" TotalRecordCount="0" DataKeyNames="TeacherID"
                        OnRowCommand="CustomGridView1_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="RealName" HeaderText="讲师姓名" ItemStyle-CssClass="alignleft"
                                HeaderStyle-CssClass="alignleft field12" />
                            <asp:TemplateField HeaderText="讲师类型" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                                <ItemTemplate>
                                    <cc1:DictionaryLabel ID="lblTeacherSource" DictionaryType="Dic_Sys_TeacherSource"
                                        FieldIDValue='<%# Eval("TeacherSourceID") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="讲师状态" HeaderStyle-Width="60">
                                <ItemTemplate>
                                    <asp:Label ID="lblTeacherStatus" runat="server" Text='<%# Eval("TeacherStatus").ToString()=="1"?"启用":"停用" %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="讲师级别" HeaderStyle-Width="60">
                                <ItemTemplate>
                                    <cc1:DictionaryLabel ID="lblTeacherLevel" DictionaryType="Dic_Sys_TeacherLevel" FieldIDValue='<%# Eval("TeacherLevelID") %>'
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="是否置顶" HeaderStyle-Width="60">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("IsTop").ToString().ToLower()=="true"?"是":"否" %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="操作" HeaderStyle-Width="200px">
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <cc1:CustomLinkButton runat="server" ID="lbtn_Top" CommandArgument='<%# Eval("TeacherID") %>'
                                        CommandName="Top" Text="置顶"  Visible='<%# Eval("IsTop").ToString().ToLower()=="true" ? false : true %>' />
                                     <cc1:CustomLinkButton runat="server" ID="lbtn_UnTop" CommandName="UnTop" CommandArgument='<%# Eval("TeacherID") %>'
                                            Text="取消置顶" Visible='<%# Eval("IsTop").ToString().ToLower()=="true" ? true : false %>' />
                                    <cc1:CustomLinkButton runat="server" ID="lbtn_Del" CommandArgument='<%# Eval("TeacherID") %>'
                                        CommandName="Del" Text="删除" EnableConfirm="true" ConfirmTitle="提示" ConfirmMessage="确定要删除吗？" />
                                   <%-- <a href='<%# this.ActionHref(string.Format("TeacherView.aspx?CourseID={0}",Eval("TeacherID").ToString())) %>'>查看</a>--%>
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
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>

