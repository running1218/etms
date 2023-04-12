<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ChooseCourse.ascx.cs"
    Inherits="Controls_ChooseCourse" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<!--查找条件-->
<div class="dv_searchbox">
    <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0" runat="server"
        id="tableQueryControlList">
        <tr>
            <th style="width: 15%;">
                课程编码：
            </th>
            <td style="width: 30%;">
                <asp:TextBox ID="txt_CourseCode" runat="server" CssClass="inputbox_120"></asp:TextBox>
            </td>
            <th style="width: 15%;">
                课程名称：
            </th>
            <td class="Search_Area">
                <asp:TextBox ID="txt_CourseName" runat="server" CssClass="inputbox_120 floatleft"></asp:TextBox>
                <asp:UpdatePanel ID="upQuery" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Button ID="btnSearch" CssClass="btn_Search" runat="server" Text="查询" OnClick="btnSearch_Click" />
                    </ContentTemplate>
                </asp:UpdatePanel>
                <a href="javascript:hideGridview()" class="dropdownico " id="Highsearch">高级搜索</a>
            </td>
        </tr>
        <tr>
            <th>
                课程类型：
            </th>
            <td>
                <cc1:DictionaryDropDownList runat="server" ID="ddl_CourseTypeID" DictionaryType="Dic_Sys_CourseType"
                    CssText="select_120" />
            </td>
            <th>
                课程等级：
            </th>
            <td>
                <cc1:DictionaryDropDownList runat="server" ID="ddl_CourseLevelID" DictionaryType="Dic_Sys_CourseLevel"
                    CssText="select_120" />
            </td>
        </tr>
    </table>
</div>
<div class="dv_searchlist">
    <asp:UpdatePanel ID="upList" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <!--翻页-->
            <div class="dv_pagePanel" id="divPage1">
                <div class="dv_selectAll">
                    <cc1:CheckBoxsController runat="server" ID="chkboxControler" ContainerID="CustomGridView1" />
                </div>
                <div class="dv_pageControl" style="float: right;">
                    <uc2:PageSet ID="PageSet1" runat="server" />
                </div>
            </div>
            <!--列表 begin-->
            <cc1:CustomGridView ID="CustomGridView1" runat="server" CssClass="GridviewGray" EmptyDataSourceTip="没有任何记录！"
                AutoCreateColumnInsertIndex="0" AutoGenerateColumns="False" CurrentPageIndex="1"
                CustomAllowPaging="False" IsEmpty="False" TotalRecordCount="0" DataKeyNames="CourseID">
                <Columns>
                    <asp:TemplateField HeaderText="">
                        <ItemStyle HorizontalAlign="Center" Width="40" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="CourseCode" HeaderText="课程编码" SortExpression="CourseCode" />
                    <asp:TemplateField HeaderText="课程名称" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="lblCourseName" runat="server" ShowTextNum="10" Text='<%# Eval("CourseName")%>'></cc1:ShortTextLabel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="课程状态" HeaderStyle-Width="80">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="lblCourseStatus" DictionaryType="Dic_Status" FieldIDValue='<%# Eval("CourseStatus") %>'
                                runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="课程类型" HeaderStyle-Width="80">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="lblCourseType" DictionaryType="Dic_Sys_CourseType" FieldIDValue='<%# Eval("CourseTypeID") %>'
                                runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="课程等级" HeaderStyle-Width="80">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="lblCourseLevel" DictionaryType="Dic_Sys_CourseLevel" FieldIDValue='<%# Eval("CourseLevelID") %>'
                                runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:TemplateField HeaderText="是否公开">
                                <ItemTemplate>
                                    <cc1:DictionaryLabel ID="lblIsPublic" DictionaryType="Dic_TrueOrFalseBool" FieldIDValue='<%# Eval("IsPublic").ToString().ToLower() %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                    <%--<asp:TemplateField HeaderText="评价">
                                <ItemTemplate>
                                    ***
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                </Columns>
            </cc1:CustomGridView>
            <!--列表 end-->
            <div class="dv_splitLine">
            </div>
            <div class="dv_pagePanel" id="divPage2">
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
<!--提交表单-->
<div class="dv_submit">
    <asp:LinkButton ID="lbtnSave" runat="server" CssClass="btn_Save" OnClick="lbtnSave_Click"
        ValidationGroup="Error">保存</asp:LinkButton>
    <asp:ValidationSummary runat="server" ID="ValidationSummary1" ValidationGroup="Error"
        ShowMessageBox="true" ShowSummary="false" />
    <a href="<%=getCancelUrl() %>" class="btn_Cancel">取消</a>
</div>
