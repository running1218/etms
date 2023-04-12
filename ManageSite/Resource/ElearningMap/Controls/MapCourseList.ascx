<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MapCourseList.ascx.cs"
    Inherits="Resource_ElearningMap_Controls_MapCourseList" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<div class="dv_searchlist">
    <!--翻页-->
    <div class="dv_pagePanel">
        <div class="dv_pageControl">
            <uc2:PageSet ID="PageSet1" runat="server" />
        </div>
    </div>
    <!--列表 begin-->
    <cc1:CustomGridView ID="CustomGridView1" runat="server" CssClass="GridviewGray" EmptyDataSourceTip="没有任何记录！"
        AutoCreateColumnInsertIndex="0" AutoGenerateColumns="False" CurrentPageIndex="1"
        CustomAllowPaging="False" IsEmpty="False" TotalRecordCount="0">
        <Columns>
            <asp:BoundField DataField="CourseCode" HeaderText="课程编码" SortExpression="CourseCode" />
            <asp:BoundField DataField="CourseName" HeaderText="课程名称" SortExpression="CourseName"
                ItemStyle-HorizontalAlign="Left" />
            <asp:TemplateField HeaderText="课程状态">
                <ItemTemplate>
                    <cc1:DictionaryLabel ID="lblCourseStatus" DictionaryType="Dic_Status" FieldIDValue='<%# Eval("CourseStatus") %>'
                        runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="课程类型">
                <ItemTemplate>
                    <cc1:DictionaryLabel ID="lblCourseType" DictionaryType="Dic_Sys_CourseType" FieldIDValue='<%# Eval("CourseTypeID") %>'
                        runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="课程等级">
                <ItemTemplate>
                    <cc1:DictionaryLabel ID="lblCourseLevel" DictionaryType="Dic_Sys_CourseLevel" FieldIDValue='<%# Eval("CourseLevelID") %>'
                        runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="是否公开">
                <ItemTemplate>
                    <cc1:DictionaryLabel ID="lblIsPublic" DictionaryType="Dic_TrueOrFalseBool" FieldIDValue='<%# Eval("IsPublic").ToString().ToLower() %>' runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="评价">
                <ItemTemplate>
                    ***
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
