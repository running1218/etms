<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SetsCourseAdd.ascx.cs" Inherits="SiteManage_Controls_SetsCourseAdd" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<div>
    <!--查找条件-->
    <div class="dv_searchbox" runat="server" id="div_Query">
        <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0" runat="server"
            id="tableQueryControlList">
            <tr>
                <th width="120">课程编码：
                </th>
                <td width="120">
                    <asp:TextBox ID="txt_CourseCode" runat="server" CssClass="inputbox_120"></asp:TextBox>
                </td>
                <th width="100">课程名称：
                </th>
                <td class="Search_Area">
                    <asp:TextBox ID="txt_CourseName" runat="server" CssClass="inputbox_120 floatleft"></asp:TextBox>
                    <asp:Button ID="btnSearch" CssClass="btn_Search" runat="server" Text="查询" OnClick="btnSearch_Click" />
                    <a href="javascript:hideGridview()" class="dropdownico" id="Highsearch">高级搜索</a>
                </td>
            </tr>
            <tr>
                <th width="100">课程分类：
                </th>
                <td>
                    <cc1:DictionaryDropDownList runat="server" ID="ddl_CourseTypeID" DictionaryType="Dic_Sys_CourseType"
                        IsShowAll="true" />
                </td><th>
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
        <!--翻页-->
        <div class="dv_pagePanel" id="divPage1">
            <div class="dv_selectAll">
                <cc1:CheckBoxsController runat="server" ID="chkboxControler" ContainerID="CustomGridView1" />
                <asp:ValidationSummary runat="server" ID="ValidationSummary1" ValidationGroup="Saves"
                    ShowMessageBox="true" ShowSummary="false" />
                <asp:Button ID="btnAdd" runat="server" Text="确定" CssClass="btn_Ok" OnClick="btnAdd_Click"
                    ValidationGroup="Saves" />
            </div>
            <div class="dv_pageControl">
                <uc2:PageSet ID="PageSet1" runat="server" />
            </div>
        </div>
        <!--列表 begin-->
        <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="false"
            CustomAllowPaging="false" ShowFooter="false" IsRemeberChecks="true" AutoCreateColumnInsertIndex="0"
            DataKeyNames="CourseID">
            <Columns>
                <asp:TemplateField HeaderText="">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" Width="20" />
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="CourseCode" HeaderText="课程编码" ItemStyle-CssClass="alignleft"
                    HeaderStyle-CssClass="alignleft field12" />
                <asp:TemplateField HeaderText="课程名称" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                    <ItemTemplate>
                        <cc1:ShortTextLabel ID="lblCourseName" runat="server" ShowTextNum="10" Text='<%# Eval("CourseName")%>'></cc1:ShortTextLabel>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="课程类型" HeaderStyle-Width="60">
                                <ItemTemplate>
                                    <asp:Label ID="lblCourseModel" runat="server" Text='<%# Eval("CourseModel").ToInt()==1?"录播":"直播" %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                <asp:BoundField DataField="CourseTypeName" HeaderStyle-Width="60" HeaderText="课程分类" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft" />
               <%-- <asp:TemplateField HeaderText="操作" HeaderStyle-Width="150">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <a href="javascript:showWindow('预览','<%# this.ActionHref(string.Format("{0}", Eval("CourseID").ToString()))%>','800','600')">预览</a>
                    </ItemTemplate>
                </asp:TemplateField>--%>
            </Columns>
        </cc1:CustomGridView>
        <!--列表 end-->
        <div class="dv_splitLine">
        </div>
        <!--翻页-->
        <div class="dv_pagePanel" id="divPage2">
        </div>
    </div>
</div>
