<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="CoursePeriodListView.aspx.cs" Inherits="TraningImplement_ProjectCoursePeriod_CoursePeriodListView" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBack" runat="Server">
    <asp:LinkButton ID="lbtnReturn" runat="server" Text="返回" CssClass="btn_Return"></asp:LinkButton>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <!--表单录入-->
        <div class="dv_GradeviewList">
            <table>
                <tr>
                    <th width="100">
                        项目编码：
                    </th>
                    <td width="200">
                        <asp:Label ID="lblItemCode" runat="server"></asp:Label>
                    </td>
                    <th width="100">
                        项目名称：
                    </th>
                    <td>
                        <asp:Label ID="lblItemName" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th>
                        课程编码：
                    </th>
                    <td>
                        <asp:Label ID="lblCourseCode" runat="server"></asp:Label>
                    </td>
                    <th>
                        课程名称：
                    </th>
                    <td>
                        <asp:Label ID="lblCourseName" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th>
                        课程属性：
                    </th>
                    <td>
                        <cc1:DictionaryLabel ID="dlblCourseAttr" DictionaryType="Dic_Sys_CourseAttr" runat="server" />
                    </td>
                    <th>
                        课程类型：
                    </th>
                    <td>
                        <cc1:DictionaryLabel ID="dlblCourseType" DictionaryType="Dic_Sys_CourseType" runat="server" />
                    </td>
                </tr>
                <tr>
                    <th>
                        培训方式：
                    </th>
                    <td>
                        <cc1:DictionaryLabel ID="dlblTrainingModel" DictionaryType="Dic_Sys_TrainingModel"
                            runat="server" />
                    </td>
                    <th>
                        授课方式：
                    </th>
                    <td>
                        <cc1:DictionaryLabel ID="dlblTeachModel" DictionaryType="Dic_Sys_TeachModel" runat="server" />
                    </td>
                </tr>
                <tr>
                    <th>
                        选课人数：
                    </th>
                    <td colspan="3">
                        <asp:Label ID="lblSelectCourse" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            <div style="display: none">
                <asp:Button ID="btnSearch" CssClass="btn_Search" runat="server" Text="查询" OnClick="btnSearch_Click" /></div>
        </div>
        <!--查找结果-->
        <div class="dv_searchlist">
            <!--翻页-->
            <div class="dv_pagePanel" id="divPage1">
                <div class="dv_selectAll">
                </div>
                <div class="dv_pageControl">
                    <uc2:PageSet ID="PageSet1" runat="server" />
                </div>
            </div>
            <!--列表 begin-->
            <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="false"
                CustomAllowPaging="false" ShowFooter="false" AutoCreateColumnInsertIndex="1"
                DataKeyNames="ItemCourseHoursID" OnRowDataBound="CustomGridView1_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="序号">
                        <ItemStyle HorizontalAlign="Center" Width="28" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="LabNo" runat="server" Text='<%#  ((this.PageSet1.PageIndex -1) * this.PageSet1.PageSize) + Container.DataItemIndex+1 %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="培训日期">
                        <ItemTemplate>
                            <asp:Label ID="lblTrainingDate" runat="server" Text='<%# Eval("TrainingDate").ToDate()%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="培训时段">
                        <ItemTemplate>
                            <asp:Label ID="lblTrainingBeginTime" runat="server" Text='<%#  Eval("TrainingBeginTime").ToDateTime().ToString("HH:mm")%>'></asp:Label>至<asp:Label ID="lblTrainingEndTime" runat="server" Text='<%#  Eval("TrainingEndTime").ToDateTime().ToString("HH:mm")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="培训时间说明">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="dlblTrainingTimeDesc" DictionaryType="Dic_Sys_TrainingTimeDesc"
                                FieldIDValue='<%# Eval("TrainingTimeDescID") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="讲师">
                        <ItemTemplate>
                            <%# Eval("TeacherName")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="培训地点">
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="lblAddress" runat="server" ShowTextNum="15" Text='<%# Eval("Address")+"（"+ Eval("ClassRoomName")+"）"%>'></cc1:ShortTextLabel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="学员人数">
                        <ItemTemplate>
                            <asp:Label ID="lblStudentTotal" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="教室容量">
                        <ItemTemplate>
                            <%# Eval("Capacity")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="课时状态">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="dlblCourseHoursStatus" DictionaryType="Dic_Sys_CourseHoursStatus"
                                FieldIDValue='<%# Eval("CourseHoursStatusID") %>' runat="server" />
                            <asp:HiddenField ID="hfCourseHoursStatusID" runat="server" Value='<%# Eval("CourseHoursStatusID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="操作">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnView" runat="server" Text="查看" CommandName="View" />
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
