<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="LearnProcessControlList.aspx.cs" Inherits="LearningManagement_LearnProcessControl_LearnProcessControlList" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <!--查找条件-->
        <div class="dv_searchbox">
            <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0" runat="server"
                id="tableQueryControlList">
                <tr>
                    <th width="120">
                        项目名称：
                    </th>
                    <td width="120">
                        <asp:TextBox ID="txt_Tr_Item999ItemName" runat="server" CssClass="inputbox_120"></asp:TextBox>
                    </td>
                    <th width="100">
                        课程编码：
                    </th>
                    <td class="Search_Area">
                        <asp:TextBox ID="txt_Res_Course999CourseCode" runat="server" CssClass="inputbox_120 floatleft"></asp:TextBox>
                        <asp:Button ID="btnSearch" CssClass="btn_Search" runat="server" Text="查询" OnClick="btnSearch_Click" />
                        <a href="javascript:hideGridview()" class="dropdownico" id="Highsearch">高级搜索</a>
                    </td>
                </tr>
                <tr>
                    <th>
                        课程名称：
                    </th>
                    <td>
                        <asp:TextBox ID="txt_Res_Course999CourseName" runat="server" CssClass="inputbox_120"></asp:TextBox>
                    </td>
                    <th>
                        授课方式：
                    </th>
                    <td>
                        <cc1:DictionaryDropDownList runat="server" ID="ddl_Tr_ItemCourse999TeachModelID"
                            DictionaryType="Dic_Sys_TeachModel" IsShowAll="true" />
                    </td>
                </tr>
                <tr>
                    <th>
                        项目开始时间：
                    </th>
                    <td colspan="3">
                        <cc1:DateTimeTextBox ID="begin_Tr_Item999ItemBeginTime" runat="server" EndTimeControlID="end_Tr_Item999ItemBeginTime"></cc1:DateTimeTextBox>
                        至
                        <cc1:DateTimeTextBox ID="end_Tr_Item999ItemBeginTime" runat="server" BeginTimeControlID="begin_Tr_Item999ItemBeginTime"></cc1:DateTimeTextBox>
                    </td>
                </tr>
            </table>
        </div>
        <!--查找结果-->
        <div class="dv_searchlist">
            <!--翻页-->
            <div class="dv_pagePanel">
                <div class="dv_pageControl">
                    <uc2:PageSet ID="PageSet1" runat="server" />
                </div>
            </div>
            <!--列表 begin-->
            <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="false"
                CustomAllowPaging="false" ShowFooter="false" DataKeyNames="TrainingItemCourseID"
                AutoCreateColumnInsertIndex="1" OnRowDataBound="CustomGridView1_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="项目名称" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="lblItemName" runat="server" ShowTextNum="50" Text='<%# Eval("ItemName")%>'></cc1:ShortTextLabel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="课程名称" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="lblCourseName" runat="server" ShowTextNum="50" Text='<%# Eval("CourseName")%>'></cc1:ShortTextLabel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="课程类型" HeaderStyle-Width="60">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="dlblCourseType" DictionaryType="Dic_Sys_CourseType" FieldIDValue='<%# Eval("CourseTypeID") %>'
                                runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="授课方式" HeaderStyle-Width="60">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="dlblTeachModel" DictionaryType="Dic_Sys_TeachModel" FieldIDValue='<%# Eval("TeachModelID") %>'
                                runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="学员数" HeaderStyle-Width="60">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnStudentTotal" runat="server" Text="0"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="开始学习人数" HeaderStyle-Width="90">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnOpenLearnStudentTotal" runat="server" Text="0"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="人均学习时间" HeaderStyle-Width="90">
                        <ItemTemplate>
                            <asp:Label ID="lblAverageSessionTime" runat="server">0</asp:Label>
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
        </div>
    </div>
</asp:Content>
