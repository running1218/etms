<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/MPageAdmin.Master" CodeFile="RecommendCourseList.aspx.cs" Inherits="SiteManage_RecommendCourse_RecommendCourseList" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <!--查找条件-->
        <div class="dv_searchbox">
            <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0" runat="server"
                id="tableQueryControlList">
                <tr>
                    <th width="120">
                        课程编码：
                    </th>
                    <td width="130">
                        <asp:TextBox ID="txt_CourseCode" runat="server" CssClass="inputbox_120"></asp:TextBox>
                    </td>
                    <th width="120">
                        课程名称：
                    </th>
                    <td class="Search_Area">
                        <asp:TextBox ID="txt_CourseName" runat="server" CssClass="inputbox_120 floatleft"></asp:TextBox>
                        <asp:Button ID="btnSearch" CssClass="btn_Search" runat="server" Text="查询" OnClick="btnSearch_Click" />
                        <a href="javascript:hideGridview()" class="dropdownico" id="Highsearch">高级搜索</a>
                    </td>
                </tr>
                  <tr>
                     <th>
                        课程状态：
                    </th>
                    <td>
                         <cc1:DictionaryDropDownList runat="server" ID="ddl_CourseStatus" DictionaryType="Dic_Status"
                            CssText="select_120" />
                    </td>
                    <th>
                        课程类型：
                    </th>
                    <td>
                         <asp:DropDownList runat="server" ID="ddl_CourseModel" CssText="select_120">
                             <asp:ListItem Value="" Text="请选择" Selected="True"></asp:ListItem>
                             <asp:ListItem Value="1" Text="录播"></asp:ListItem>
                             <asp:ListItem Value="2" Text="直播"></asp:ListItem>
                         </asp:DropDownList>
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
                            <input type="button" class="btn_Add" value="新增" onclick="javascript: showWindow('新增推荐课程', 'SetsCourseAdd.aspx')" />
                            <input id="btnSort" type="button" class="btn_Sort" value="排序" title="推荐课程排序" onclick="javascript: showWindow('推荐课程排序', 'SetRecommendCourseSort.aspx', 500, 450)" />
                        </div>
                        <div class="dv_pageControl">
                            <uc2:PageSet ID="PageSet1" runat="server" />
                        </div>
                    </div>
                    <!--列表 begin-->
                    <cc1:CustomGridView ID="gvList" runat="server" CssClass="GridviewGray" EmptyDataSourceTip="没有任何记录！"
                        AutoCreateColumnInsertIndex="0" AutoGenerateColumns="False" CurrentPageIndex="1"
                        CustomAllowPaging="False" IsEmpty="False" TotalRecordCount="0" DataKeyNames="RecommendID"
                        OnRowCommand="CustomGridView1_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="CourseCode" HeaderText="课程编码" ItemStyle-CssClass="alignleft"
                                HeaderStyle-CssClass="alignleft field12"  />
                            <asp:TemplateField HeaderText="课程名称" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                                <ItemTemplate>
                                    <cc1:ShortTextLabel ID="lblCourseName" runat="server" ShowTextNum="100" Text='<%# Eval("CourseName")%>'></cc1:ShortTextLabel>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="课程类型" HeaderStyle-Width="60">
                                <ItemTemplate>
                                    <asp:Label ID="lblCourseModel" runat="server" Text='<%# Eval("CourseModel").ToInt()==1?"录播":"直播" %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="课程状态" HeaderStyle-Width="60">
                                <ItemTemplate>
                                    <asp:Label ID="lblCourseStatus" runat="server" Text='<%# Eval("CourseStatus").ToString()=="1"?"启用":"停用" %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="CourseTypeName" HeaderText="课程类型" HeaderStyle-Width="60" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft"/>
                            <asp:TemplateField HeaderText="是否置顶" HeaderStyle-Width="60">
                                <ItemTemplate>
                                    <asp:Label ID="lblIsTop" runat="server" Text='<%# Eval("IsTop").ToString().ToLower()=="true"?"是":"否" %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="操作" HeaderStyle-Width="200px">
                                <ItemStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <a href='<%# Eval("CourseModel").ToInt() ==1 ? this.ActionHref(string.Format("~/Resource/CourseManage/CourseResourceOpen.aspx?CourseID={0}",Eval("CourseID").ToString())) : this.ActionHref(string.Format("~/Resource/CourseManage/CourseLivingResourceOpen.aspx?CourseID={0}",Eval("CourseID").ToString())) %>'>
                                            开放管理</a>                                   
                                    <cc1:CustomLinkButton runat="server" ID="lbtn_Del" CommandArgument='<%# Eval("RecommendID") %>'
                                        CommandName="Del" Text="删除" EnableConfirm="true" ConfirmTitle="提示" ConfirmMessage="确定要删除吗？" />
                                     <cc1:CustomLinkButton runat="server" ID="lbtn_Top" CommandArgument='<%# Eval("RecommendID") %>'
                                        CommandName="Top" Text="置顶" ToolTip="置顶"  Visible='<%# Eval("IsTop").ToString().ToLower()=="true" ? false : true %>' />
                                     <cc1:CustomLinkButton runat="server" ID="lbtn_UnTop" CommandName="UnTop" CommandArgument='<%# Eval("RecommendID") %>'
                                            Text="取消" ToolTip="取消置顶" Visible='<%# Eval("IsTop").ToString().ToLower()=="true" ? true : false %>' />
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

