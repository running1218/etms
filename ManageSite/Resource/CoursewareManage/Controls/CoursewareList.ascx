<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CoursewareList.ascx.cs"
    Inherits="ETMS.WebApp.Manage.CoursewareList" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<div>
    <!--导航路径-->
    <div class="dv_path" id="dv_path">
        当前位置：资源管理系统>>学习资源管理>>在线课件管理
    </div>
    <!--功能标题-->
    <h2 class="dv_title">
        在线课件管理
    </h2>
    <!--查找条件-->
    <div class="dv_searchbox">
        <table class="GridviewGray th80" border="0" cellpadding="0" cellspacing="0" runat="server"
            id="tableQueryControlList">
            <tr>
            <th width="120">
                    课件名称：
                </th>
                <td>
                    <asp:TextBox ID="txt_CoursewareName" runat="server" CssClass="inputbox_120"></asp:TextBox>
                </td>
                <th width="120">
                    课程名称：
                </th>
                <td width="210">
                    <asp:TextBox ID="txt_CourseName" runat="server" CssClass="inputbox_120 floatleft"></asp:TextBox>
                   <asp:Button ID="Button1" runat="server" CssClass="btn_Search" Text="查询" OnClick="btnSearch_Click" />
                    </asp:UpdatePanel>
                    <a href="javascript:hideGridview()" class="dropdownico" id="Highsearch">高级搜索</a>
                </td>
            </tr>
            <tr>
                
                <th>
                    课件状态：
                </th>
                <td>
                    <cc1:DictionaryDropDownList runat="server" ID="ddl_CoursewareStatus" DictionaryType="Dic_Status" />
                </td>
                <th>
                    课件类型：
                </th>
                <td>
                    <cc1:DictionaryDropDownList runat="server" ID="ddl_CoursewareTypeID" DictionaryType="Dic_Sys_CoursewareType" />
                </td>
            </tr>
        </table>
    </div>
    <!--查找结果-->
    <asp:UpdatePanel ID="upList" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="dv_searchlist">
                <!--翻页-->
                <div class="dv_pagePanel">
                    <div class="dv_selectAll" id="dv_selectall" runat="server">
                        <%if (IsEnableScormCourseWare)
                          { %>
                        <input type="button" class="btn_SCORMbutton" value="SCORM标准" onclick="javascript:showWindow('新增SCORM标准','CoursewareAddScorm.aspx')" />
                        <%} %>
                        <%if (IsEnableNotScormCourseWare)
                          { %>
                        <input type="button" class="btn_NOTSCORMbutton" value="非SCORM标准" onclick="javascript:showWindow('新增非SCORM标准','<%=this.ActionHref(string.Format("{0}/Resource/CoursewareManage/CoursewareAdd.aspx?op=add",WebUtility.AppPath)) %>')" />
                        <%} %>
                    </div>
                    <div class="dv_pageControl">
                        <uc2:PageSet ID="PageSet1" runat="server" />
                    </div>
                </div>
                <!--列表 begin-->
                <cc1:CustomGridView ID="CustomGridView1" runat="server" CssClass="GridviewGray" EmptyDataSourceTip="没有任何记录！"
                    AutoCreateColumnInsertIndex="0" AutoGenerateColumns="False" CurrentPageIndex="1"
                    CustomAllowPaging="False" IsEmpty="False" TotalRecordCount="0" DataKeyNames="CoursewareID"
                    OnRowDataBound="CustomGridView1_RowDataBound" OnRowCommand="CustomGridView1_RowCommand">
                    <Columns>
                        
                        <asp:TemplateField HeaderText="课程名称" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft field12">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblCourseName" runat="server"   Text='<%# Eval("CourseName")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="课件名称" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft field12" >
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblCoursewareName" runat="server" Text='<%# Eval("CoursewareName")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="课件类型"  HeaderStyle-Width="80">
                            <ItemTemplate>
                                <asp:Label ID="lblCoursewareType" runat="server" Text='<%# Eval("CoursewareTypeID").ToString()=="1"?"SCORM标准":"非SCORM标准" %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="是否有效"  HeaderStyle-Width="80">
                            <ItemTemplate>
                                <asp:Label ID="lblCanUse" runat="server" Text='<%# Eval("CoursewarePath").ToString()=="" ? "<font color=\"#C03219\">无效</font>":"有效"  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="课件状态"  HeaderStyle-Width="60">
                            <ItemTemplate>
                                <asp:Label ID="lblCoursewareStatus" runat="server" Text='<%# Eval("CoursewareStatus").ToString()=="1"?"启用":"停用" %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="创建时间" HeaderStyle-Width="90">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblCreateTime" runat="server" Text='<%#Eval("CreateTime").ToDate() %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:BoundField DataField="CreateUser" HeaderText="创建者"  HeaderStyle-CssClass="field8 alignleft" ItemStyle-CssClass="alignleft" />
                        <asp:TemplateField HeaderText="操作" HeaderStyle-Width="100">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:LinkButton ID="lblScormEdit" Text="编辑" runat="server" />
                                <asp:LinkButton ID="lblEdited" Text="编辑" runat="server" />
                                <a href='<%# this.ActionHref(string.Format("~/Courseware/OpenCourseware.aspx?CourseWareID={0}&CourseID={1}",Eval("CoursewareID"),Eval("CourseID"))) %>'
                                    class="btn_Study" target="_blank">浏览</a>
                                <cc1:CustomLinkButton runat="server" ID="lbtn_Del" CommandArgument='<%# Eval("CourseResID") %>'
                                    CommandName="Del" Text="删除" EnableConfirm="true" ConfirmTitle="提示" ConfirmMessage="确定要删除吗？" />
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
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
