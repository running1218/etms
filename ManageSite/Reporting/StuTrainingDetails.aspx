<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="StuTrainingDetails.aspx.cs"
    MasterPageFile="~/MasterPages/MReport.master" Inherits="Reporting_StuTrainingDetails" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>

<asp:Content ID="content2" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
    <title>学员培训明细表</title>
</asp:Content>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1" runat="server">
  <div class="dv_HeaderTitle">
     <h2 class="h_titleName1">学员培训明细列表</h2>
  </div>
    <div class="dv_reports">
        <div class="dv_searchbox">
            <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0" runat="server"
                id="tableQueryControlList">
                <tr>
                    <th width="100">
                        姓名：
                    </th>
                    <td width="240">
                        <asp:TextBox ID="txtName" runat="server" CssClass="inputbox_120"></asp:TextBox>
                    </td>
                    <th width="100">
                        工号：
                    </th>
                    <td class="Search_Area">
                        <asp:TextBox ID="txtWorkerNo" runat="server" CssClass="inputbox_120 floatleft"></asp:TextBox>
                        <asp:Button ID="btnSearch" CssClass="btn_Search" runat="server" Text="查询" OnClick="btnSearch_Click" />
                        <asp:Button ID="btnExport" runat="server" Text="导出" CssClass="btn_Export" OnClick="btnExport_Click" />
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltlDepartment" runat="server" Text="<%$ Resources:UIResource, ui_department%>"></asp:Literal>：
                    </th>
                    <td>
                        <asp:TextBox ID="txtDepartment" runat="server" CssClass="inputbox_120"></asp:TextBox>
                    </td>
                    <th>
                        <asp:Literal ID="ltlPosition" runat="server" Text="<%$ Resources:UIResource, ui_position%>"></asp:Literal>：
                    </th>
                    <td>
                        <asp:TextBox ID="txtPost" runat="server" CssClass="inputbox_120"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>
                        项目周期：
                    </th>
                    <td colspan="3">
                        <cc1:DateTimeTextBox ID="txtBeginTime" runat="server" EndTimeControlID="txtEndTime"
                            DataTimeFormat="%Y-%M-%D" CssClass="date_format"></cc1:DateTimeTextBox>
                        至
                        <cc1:DateTimeTextBox ID="txtEndTime" runat="server" BeginTimeControlID="txtBeginTime"
                            DataTimeFormat="%Y-%M-%D" CssClass="date_format"></cc1:DateTimeTextBox>
                    </td>
                </tr>
            </table>
        </div>
        <!--查找结果-->
        <div>
            <div class="dv_pagePanel" id="divPage1">
                <div class="dv_pageControl">
                    <uc2:PageSet ID="PageSet1" runat="server" />
                </div>
            </div>
            <asp:Panel ID="pnlReportLayer" runat="server">
                <div id="pnlReport">
                <div class="dv_TableHeader">
                    <table class="tab_header">
                    <tr></tr>
                    </table>
                </div>
                <div class="dv_Datareport">
                    <table class="reporting-list">
                        <tr>
                            <th>
                                姓名
                            </th>
                            <th>
                                工号
                            </th>
                            <th>
                                <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:UIResource, ui_department%>"></asp:Literal>
                            </th>
                            <th>
                                <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:UIResource, ui_position%>"></asp:Literal>
                            </th>
                            <th>
                                职务
                            </th>
                            <td class="hide rolspan">
                                <table>
                                    <tr>
                                        <th style="width: 180px;">
                                            项目名称
                                        </th>
                                        <td class="hide rolspan">
                                            <table>
                                                <tr>
                                                    <th style="width: 120px;">
                                                        课程编码
                                                    </th>
                                                    <th style="width: 180px;">
                                                        课程名称
                                                    </th>
                                                    <th style="width: 120px;">
                                                        课程类别
                                                    </th>
                                                    <th style="width: 120px;">
                                                        授课方式
                                                    </th>
                                                    <th style="width: 100px;">
                                                        课时(小时)
                                                    </th>
                                                    <th style="width: 100px;">
                                                        考试成绩
                                                    </th>
                                                    <td class="hide rolspan">
                                                        <table>
                                                            <tr>
                                                                <th style="width: 100px;">
                                                                    培训讲师
                                                                </th>
                                                                <th style="width: 100px;">
                                                                    培训日期
                                                                </th>
                                                                <th style="width: 120px;">
                                                                    培训时段
                                                                </th>
                                                                <th style="width: 200px;">
                                                                    培训地点
                                                                </th>
                                                                <th style="width: 100px;">
                                                                    签到信息
                                                                </th>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <%--培训明细--%>
                        <asp:Repeater ID="rptTrainingDetails" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <%# Eval("RealName")%>
                                    </td>
                                    <td>
                                        <span>
                                            <%# Eval("WorkerNo")%></span>
                                    </td>
                                    <td>
                                        <cc1:DictionaryLabel ID="lblDepartement" runat="server" DictionaryType="Site_DepartmentByOrgID"
                                            FieldIDValue='<%# Eval("DepartmentID") %>'></cc1:DictionaryLabel>
                                    </td>
                                    <td>
                                        <cc1:DictionaryLabel ID="lblPost" runat="server" DictionaryType="Dic_PostByOrgID"
                                            FieldIDValue='<%# Eval("PostID") %>'></cc1:DictionaryLabel>
                                    </td>
                                    <td>
                                        <span>
                                            <%# Eval("TitleName")%></span>
                                    </td>
                                    <td class="rolspan" style="border-right: 1px solid #515151">
                                        <table class='stRls'>
                                            <%-- 培训项目 --%>
                                            <asp:Repeater ID="rptItems" runat="server" DataSource='<%# Eval("TrainingItems") %>'>
                                                <ItemTemplate>
                                                    <tr class="tr_a">
                                                        <td style="width: 180px;" class="porjItemName">
                                                            <%# Eval("ItemName")%>
                                                        </td>
                                                        <td class="rolspan prjcourseName">
                                                            <table class='snRls'>
                                                                <%-- 培训项目课程 --%>
                                                                <asp:Repeater ID="rptItemCourses" runat="server" DataSource='<%# Eval("TrainingItemCourses") %>'>
                                                                    <ItemTemplate>
                                                                        <tr class="tr_b">
                                                                            <td style="width: 120px;">
                                                                                <%# Eval("CourseCode")%>
                                                                            </td>
                                                                            <td style="width: 180px;">
                                                                                <%# Eval("CourseName")%>
                                                                            </td>
                                                                            <td style="width: 120px;">
                                                                                <cc1:DictionaryLabel ID="lblCourseType" runat="server" DictionaryType="Dic_Sys_CourseType"
                                                                                    FieldIDValue='<%# Eval("CourseTypeID") %>'></cc1:DictionaryLabel>
                                                                            </td>
                                                                            <td style="width: 120px;">
                                                                                <cc1:DictionaryLabel ID="lblTeachModel" runat="server" DictionaryType="Dic_Sys_TeachModel"
                                                                                    FieldIDValue='<%# Eval("TeachModelID") %>'></cc1:DictionaryLabel>
                                                                            </td>
                                                                            <td style="width: 100px;">
                                                                                <%# Eval("CourseHours")%>
                                                                            </td>
                                                                            <td style="width: 100px;">
                                                                                <%# Eval("SumGrade")%>
                                                                            </td>
                                                                            <td class="rolspan">
                                                                                <table class='smRls'>
                                                                                    <%-- 培训课时安排 --%>
                                                                                    <asp:Repeater ID="rptCourseHours" runat="server" DataSource='<%# Eval("TrainingItemCourseHours") %>'>
                                                                                        <ItemTemplate>
                                                                                            <tr class="tr_c">
                                                                                                <td style="width: 100px;">
                                                                                                    <span>
                                                                                                        <%# Eval("RealName")%></span>
                                                                                                </td>
                                                                                                <td style="width: 100px;">
                                                                                                    <span>
                                                                                                        <%# Eval("TrainingDate").ToDate()%></span>
                                                                                                </td>
                                                                                                <td style="width: 120px;">
                                                                                                    <span>
                                                                                                        <%#string.Format("{0}~{1}", Eval("TrainingBeginTime").ToDateTime().ToString("HH:mm"),Eval("TrainingEndTime").ToDateTime().ToString("HH:mm"))%></span>
                                                                                                </td>
                                                                                                <td style="width: 200px;">
                                                                                                    <span>
                                                                                                        <%# string.Format("{0}({1})", Eval("Address"), Eval("ClassRoomName"))%></span>
                                                                                                </td>
                                                                                                <td style="width:100px;">
                                                                                                    <span><%# Eval("SigninTypeName")%></span>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </ItemTemplate>
                                                                                    </asp:Repeater>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </table>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </div></div>
            </asp:Panel>
            <!--翻页-->
            <div class="dv_pagePanel" id="divPage2">
            </div>
        </div>
    </div>
</asp:Content>
