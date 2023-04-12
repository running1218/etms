<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="ProfessorListInner.aspx.cs" Inherits="Resource_ProfessorManage_ProfessorListInner" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBack" runat="Server">
    <!--导航路径-->
    <div class="dv_path" id="dv_path">
        当前位置：资源管理系统&gt;&gt;讲师资源库&gt;&gt;内部讲师管理
    </div>
    <!--功能标题-->
    <h2 class="dv_title">
        内部讲师管理
    </h2>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <!--查找条件-->
        <div class="dv_searchbox">
            <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <th width="120">
                        讲师姓名：
                    </th>
                    <td width="120">
                        <asp:TextBox ID="txtTeacherName" runat="server" MaxLength="50" CssClass="inputbox_120" />
                    </td>
                    <th width="120">
                        工&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;号：
                    </th>
                    <td width="120">
                        <asp:TextBox ID="txtWorkNo" runat="server" CssClass="inputbox_120" MaxLength="20"></asp:TextBox>
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn_Search" Text="查询" OnClick="btnSearch_Click" /><a
                            href="javascript:hideGridview()" class="dropdownico" id="Highsearch">高级搜索</a>
                    </td>
                </tr>
                <tr>
                    <th>
                        讲师分类：
                    </th>
                    <td>
                        <cc1:DictionaryDropDownList runat="server" ID="ddlTeacherType" DictionaryType="Dic_Sys_TeacherType"
                            IsShowAll="true" />
                    </td>
                    <th>
                        讲师等级：
                    </th>
                    <td>
                        <cc1:DictionaryDropDownList runat="server" ID="Dic_ProfessorGrade" DictionaryType="Dic_Sys_TeacherLevel"
                            IsShowAll="true" />
                    </td>
                </tr>
                <tr>
                    <th>
                        讲师状态：
                    </th>
                    <td>
                        <cc1:DictionaryDropDownList runat="server" ID="Dic_Status" DictionaryType="Dic_Status" />
                    </td>
                    <th>
                        课程设计人员：
                    </th>
                    <td>
                        <asp:DropDownList ID="ddlCourseDesiner" runat="server">
                            <asp:ListItem Value="-1">全部</asp:ListItem>
                            <asp:ListItem Value="1">是</asp:ListItem>
                            <asp:ListItem Value="0">否</asp:ListItem>
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
                            <asp:Button ID="btnAdd" runat="server" CssClass="btn_Add" Text="新增" />
                            <%-- <input type="button" id="btn_Add2" class="btn_Add" value="新增"  onclick="javascript:showWindow('新增内部讲师','<%=this.ActionHref(string.Format("{0}/Security/ProfessorManage/ProfessorAddInner.aspx",WebUtility.AppPath)) %>')" />--%>
                        </div>
                        <div class="dv_pageControl" style="float: right;">
                            <uc2:PageSet ID="PageSet1" runat="server" />
                        </div>
                    </div>
                    <!--列表 begin-->
                    <cc1:CustomGridView ID="GridViewList" runat="server" AutoGenerateColumns="False"
                        DataKeyNames="TeacherID" OnRowCommand="GridViewList_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="账户" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                                <ItemTemplate>
                                    <cc1:ShortTextLabel ID="lblLoginName" runat="server" Text='<%#Eval("LoginName") %>'
                                        ShowTextNum="10" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="WorkerNo" HeaderText="工号" SortExpression="WorkerNo" ItemStyle-CssClass="alignleft"
                                HeaderStyle-CssClass="alignleft " />
                            <asp:BoundField DataField="TeacherName" HeaderText="讲师姓名" SortExpression="TeacherName"
                                ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft field12" />
                            <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_department%>" ItemStyle-CssClass="alignleft hide" HeaderStyle-CssClass="alignleft hide">
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <cc1:DictionaryLabel ID="lblDepartment" DictionaryType="Site_DepartmentByOrgID" TextLength="10"
                                        runat="server" FieldIDValue='<%#Eval("DepartmentID").ToString() %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_position%>" ItemStyle-CssClass="alignleft hide" HeaderStyle-CssClass="alignleft hide">
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <cc1:DictionaryLabel ID="lblPost" DictionaryType="Dic_PostByOrgID" TextLength="10"
                                        runat="server" FieldIDValue='<%#Eval("PostID").ToString() %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="讲师分类" HeaderStyle-Width="100">
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <cc1:DictionaryLabel ID="lblType" runat="server" DictionaryType="Dic_Sys_TeacherType"
                                        FieldIDValue='<%# Eval("TeacherTypeID").ToString() %>'></cc1:DictionaryLabel>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="讲师状态" HeaderStyle-Width="80">
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblTeacherStatus" runat="server" Text='<%#Eval("IsUse").ToInt()==1?"启用":"停用" %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="讲师等级" HeaderStyle-Width="100">
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <cc1:DictionaryLabel ID="lblTeacherLevelID" runat="server" DictionaryType="Dic_Sys_TeacherLevel"
                                        FieldIDValue='<%#Eval("TeacherLevelID").ToString() %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="课程设计人员" HeaderStyle-Width="100" HeaderStyle-CssClass="hide" ItemStyle-CssClass="hide">
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%# Eval("IsCourseDesigner").ToInt()==0 ? "否":"是"%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="操作" HeaderStyle-Width="160">
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <a href="javascript:showWindow('编辑讲师', '<%# this.ActionHref(string.Format("{0}/Security/ProfessorManage/ProfessorEditOutside.aspx?TeacherID={1}",WebUtility.AppPath,Eval("TeacherID").ToString()))%>','800','600')">
                                        编辑</a> <a href="javascript:showWindow('查看讲师','<%# this.ActionHref(string.Format("ProfessorViewOutside.aspx?TeacherID={0}&op=1", Eval("TeacherID").ToString()))%>','800','600')">
                                            查看</a>
                                    <cc1:CustomLinkButton runat="server" ID="lbtn_Del" CommandArgument='<%# Eval("TeacherID") %>'
                                        CommandName="Del" Text="删除" EnableConfirm="true" ConfirmTitle="提示" ConfirmMessage="确定要删除吗？" />
                                    <a href='<%#this.ActionHref(string.Format("TeachingCourse.aspx?TeacherID={0}&IsInner=1",Eval("TeacherID"))) %>'>
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
