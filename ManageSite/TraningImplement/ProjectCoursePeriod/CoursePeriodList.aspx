<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="CoursePeriodList.aspx.cs" Inherits="TraningImplement_ProjectCoursePeriod_CoursePeriodList" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBack" runat="Server">
    <asp:LinkButton ID="lbtnReturn" runat="server" Text="返回" CssClass="btn_Return"></asp:LinkButton>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 
        <!--表单录入-->
        <div class="dv_GradeviewList">
            <table>
                <tr>
                    <th width="100">
                        项目名称：
                    </th>
                    <td>
                        <asp:Label ID="lblItemName" runat="server"></asp:Label>
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
                        培训方式：
                    </th>
                    <td>
                        <cc1:DictionaryLabel ID="dlblTrainingModel" DictionaryType="Dic_Sys_TrainingModel"
                            runat="server" />
                    </td>
                </tr>
                <tr>
                    <th>
                        授课方式：
                    </th>
                    <td>
                        <cc1:DictionaryLabel ID="dlblTeachModel" DictionaryType="Dic_Sys_TeachModel" runat="server" />
                    </td>
                    <th>
                        选课人数：
                    </th>
                    <td>
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
                    <asp:Button ID="btnAdd" runat="server" CssClass="btn_Add" Text="新增" />
                </div>
                <div class="dv_pageControl">
                    <uc2:PageSet ID="PageSet1" runat="server" />
                </div>
            </div>
            <!--列表 begin-->
            <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="false"
                CustomAllowPaging="false" ShowFooter="false" AutoCreateColumnInsertIndex="1"
                DataKeyNames="ItemCourseHoursID" OnRowDataBound="CustomGridView1_RowDataBound"
                OnRowCommand="CustomGridView1_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="培训日期" HeaderStyle-Width="80" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <asp:Label ID="lblTrainingDate" runat="server" Text='<%# Eval("TrainingDate").ToDate()%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="培训时段"  HeaderStyle-Width="80">
                        <ItemTemplate>
                            <asp:Label ID="lblTrainingBeginTime" runat="server" Text='<%#  Eval("TrainingBeginTime").ToDateTime().ToString("HH:mm")%>'></asp:Label>至<asp:Label
                                ID="lblTrainingEndTime" runat="server" Text='<%#  Eval("TrainingEndTime").ToDateTime().ToString("HH:mm")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="讲师" HeaderStyle-Width="60" >
                        <ItemTemplate>
                            <%# Eval("TeacherName")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="培训地点" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft" >
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="lblAddress" runat="server" ShowTextNum="50" Text='<%# GetAddress(Eval("ClassRoomName"),Eval("Address")) %>'></cc1:ShortTextLabel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="学员人数" HeaderStyle-Width="60">
                        <ItemTemplate>
                            <asp:Label ID="lblStudentTotal" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="教室容量" HeaderStyle-Width="60">
                        <ItemTemplate>
                            <%# Eval("Capacity")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="课时状态" HeaderStyle-Width="60">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="dlblCourseHoursStatus" DictionaryType="Dic_Sys_CourseHoursStatus"
                                FieldIDValue='<%# Eval("CourseHoursStatusID") %>' runat="server" />
                            <asp:HiddenField ID="hfCourseHoursStatusID" runat="server" Value='<%# Eval("CourseHoursStatusID") %>' />
                            <asp:HiddenField ID="hfPayStatus" runat="server" Value='<%# Eval("PayStatus")%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="操作" HeaderStyle-Width="260">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnEdit" runat="server" Text="编辑" CommandName="Edit"></asp:LinkButton><cc1:CustomLinkButton
                                runat="server" ID="clbtnDel" CommandName="delCourseHours" CommandArgument='<%# Eval("ItemCourseHoursID") %>'
                                Text="删除" EnableConfirm="true" ConfirmTitle="提示" ConfirmMessage="确定删除吗？" /><asp:LinkButton
                                    ID="lbtnView" runat="server" Text="查看" CommandName="View"></asp:LinkButton><asp:LinkButton
                                        ID="lbtnSetStudent" runat="server" Text="设置学员" CommandName="SetStudent"></asp:LinkButton><asp:LinkButton
                                            ID="lbtnSignInTable" runat="server" Text="签到表" CommandName="SignInTable" CommandArgument='<%# Eval("ItemCourseHoursID") %>'></asp:LinkButton>
                            <asp:LinkButton ID="lbtnSetResult" runat="server" Text="设置执行结果" CommandName="SetResult"
                                CommandArgument='<%# Eval("TrainingItemCourseID") %>'></asp:LinkButton>
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
            <div style="display: none">
                <cc1:CustomGridView ID="CustomGridViewExport" runat="server" AutoGenerateColumns="false"
                    CustomAllowPaging="false" ShowFooter="false" AutoCreateColumnInsertIndex="1"
                    DataKeyNames="ItemCourseHoursStudentID" OnRowDataBound="CustomGridViewExport_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="序号">
                            <ItemStyle HorizontalAlign="Center" Width="28" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="LabNo" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="WorkerNo" HeaderText="工号" />
                        <asp:BoundField DataField="RealName" HeaderText="姓名" />
                        <asp:TemplateField HeaderText="组织机构">
                            <ItemTemplate>
                                <cc1:DictionaryLabel ID="lblOrganization" DictionaryType="Dic_CurrentAndSubOrganization" FieldIDValue='<%# Eval("OrganizationID") %>'
                                    runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_department%>">
                            <ItemTemplate>
                                <cc1:DictionaryLabel ID="lblDepartmentID" DictionaryType="vw_Dic_Sys_Department"
                                    FieldIDValue='<%# Eval("DepartmentID") %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_rank%>">
                            <ItemTemplate>
                                <cc1:DictionaryLabel ID="lblRank" DictionaryType="vw_Dic_Sys_Rank" FieldIDValue='<%# Eval("RankID") %>'
                                    runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="签到">
                            <ItemTemplate>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="备注">
                            <ItemTemplate>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </cc1:CustomGridView>
            </div>
        </div>
   
</asp:Content>
