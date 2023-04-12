<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="ProfessorListOutSide.aspx.cs" Inherits="ETMS.WebApp.Manage.ResourceProfessorListOutSide" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBack" runat="Server">
    <!--导航路径-->
    <div class="dv_path" id="dv_path">
        当前位置：资源管理系统&gt;&gt;讲师资源库&gt;&gt;讲师管理
    </div>
    <!--功能标题-->
    <h2 class="dv_title">
        讲师管理
    </h2>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <!--查找条件-->
        <div class="dv_searchbox">
            <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <th width="120">
                        培训机构：
                    </th>
                    <td width="120">
                        <cc1:DictionaryDropDownList runat="server" ID="Dic_Organization" DictionaryType="Tr_OuterOrg"
                            IsShowAll="true" />
                    </td>
                    <th width="120">
                        讲师姓名：
                    </th>
                    <td>
                        <asp:TextBox ID="txtTeacherName" runat="server" CssClass="inputbox_120" MaxLength="50"></asp:TextBox>
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn_Search" Text="查询" OnClick="btnSearch_Click" />
                        <a href="javascript:hideGridview()" class="dropdownico" id="Highsearch">高级搜索</a>
                    </td>
                </tr>
                <tr>
                    <th>
                        讲师等级：
                    </th>
                    <td>
                        <cc1:DictionaryDropDownList runat="server" ID="Dic_ProfessorGrade" DictionaryType="Dic_Sys_TeacherLevel"
                            IsShowAll="true" />
                    </td>
                    <th>
                        讲师状态：
                    </th>
                    <td>
                        <cc1:DictionaryDropDownList runat="server" ID="Dic_Status" DictionaryType="Dic_Status" />
                    </td>
                </tr>
                <tr class="hide">
                    <th>
                        合作关系：
                    </th>
                    <td colspan="3">
                        <asp:DropDownList ID="ddlCooperationRelation" runat="server">
                            <asp:ListItem Value="-1">全部</asp:ListItem>
                            <asp:ListItem Value="1">已合作</asp:ListItem>
                            <asp:ListItem Value="0">未合作</asp:ListItem>
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
                    <div class="dv_pagePanel" id="divPage1">
                        <div class="dv_selectAll">
                            <input type="button" id="btn_Add2" class="btn_Add" value="新增" onclick="javascript:showWindow('新增讲师','<%=this.ActionHref(string.Format("ProfessorAddOutside.aspx?TeacherID={0}","0")) %>', 800, 600)" />
                        </div>
                        <div class="dv_pageControl" style="float: right;">
                            <uc2:PageSet ID="PageSet1" runat="server" />
                        </div>
                    </div>
                    <!--列表 begin-->
                    <cc1:CustomGridView ID="GridViewList" runat="server" AutoGenerateColumns="False"
                        DataKeyNames="TeacherID" OnRowCommand="GridViewList_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="用户名" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft field8">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <cc1:ShortTextLabel ID="lblLoginName" runat="server" ShowTextNum="10" Text='<%#Eval("LoginName") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="TeacherCode" HeaderText="讲师编码" SortExpression="TeacherCode"
                                HeaderStyle-CssClass="field12">
                                <ItemStyle HorizontalAlign="Center" CssClass="alignleft" />
                                <HeaderStyle HorizontalAlign="Center" CssClass="alignleft field12" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TeacherName" HeaderText="讲师姓名" SortExpression="TeacherName"
                                HeaderStyle-CssClass="alignleft">
                                <ItemStyle HorizontalAlign="Center" CssClass="alignleft" />
                            </asp:BoundField>                            
                            <asp:TemplateField HeaderText="讲师等级">
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" Width="60" />
                                <ItemTemplate>
                                    <cc1:DictionaryLabel ID="lblTeacherLevelID" runat="server" DictionaryType="Dic_Sys_TeacherLevel"
                                        FieldIDValue='<%#Eval("TeacherLevelID") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="讲师来源">
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" Width="60" />
                                <ItemTemplate>
                                    <cc1:DictionaryLabel ID="lblSourceID" runat="server" DictionaryType="Dic_Sys_TeacherSource"
                                        FieldIDValue='<%#Eval("TeacherSourceID") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="培训机构" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <cc1:DictionaryLabel ID="lbl_OuterOrg" runat="server" DictionaryType="Tr_OuterOrg"
                                        FieldIDValue='<%#Eval("OuterOrgID") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="讲师状态">
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" Width="60" />
                                <ItemTemplate>
                                    <asp:Label ID="lblTeacherStatus" runat="server" Text='<%#Eval("IsUse").ToInt()==1?"启用":"停用" %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="合作关系" ItemStyle-CssClass="alignleft hide" HeaderStyle-CssClass="alignleft field8 hide">
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%# Eval("IsCollaborate").ToInt() == 0 ? "未合作" : "已合作"%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="操作" HeaderStyle-Width="150">
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <a href="javascript:showWindow('编辑讲师', '<%# this.ActionHref(string.Format("ProfessorEditOutside.aspx?TeacherID={0}",Eval("TeacherID").ToString()))%>','800','600')">
                                        编辑</a> <a href="javascript:showWindow('查看讲师','<%# this.ActionHref(string.Format("ProfessorViewOutside.aspx?TeacherID={0}", Eval("TeacherID").ToString()))%>','800','600')">
                                            查看</a>
                                    <cc1:CustomLinkButton runat="server" ID="lbtn_Del" CommandArgument='<%# Eval("TeacherID") %>'
                                        CommandName="Del" Text="删除" EnableConfirm="true" ConfirmTitle="提示" ConfirmMessage="确定要删除吗？" />
                                    <a href='<%#this.ActionHref(string.Format("TeachingCourse.aspx?TeacherID={0}&IsInner=0",Eval("TeacherID"))) %>'>
                                        课程<%#string.Format("({0})",teacherCourseLogic.GetTeacherTeachCourse(Eval("TeacherID").ToInt()).Count) %></a>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </cc1:CustomGridView>
                    <!--列表 end-->
                    <div class="dv_splitLine">
                    </div>
                    <!--翻页-->
                    <div class="dv_pagePanel" id="divPage2">
                    </div>
                    <script language="javascript" type="text/javascript">
                        divPage2.innerHTML = divPage1.innerHTML;
                    </script>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
