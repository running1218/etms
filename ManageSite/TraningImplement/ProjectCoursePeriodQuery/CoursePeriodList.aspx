<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="CoursePeriodList.aspx.cs" Inherits="TraningImplement_ProjectCoursePeriodQuery_CoursePeriodList" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <!--表单录入-->
        <div class="dv_searchbox">
            <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0" runat="server"
                id="tableQueryControlList">
                <tr>
                    <th width="120">
                        项目名称：
                    </th>
                    <td width="120">
                        <asp:TextBox ID="txt_f999ItemName" runat="server" CssClass="inputbox_120" MaxLength="100"></asp:TextBox>
                    </td>
                    <th width="100">
                        课程名称：
                    </th>
                    <td class="Search_Area">
                        <asp:TextBox ID="txt_g999CourseName" runat="server" CssClass="inputbox_120 floatleft"></asp:TextBox><asp:Button
                            ID="btnSearch" CssClass="btn_Search" runat="server" Text="查询" OnClick="btnSearch_Click" /><a
                                href="javascript:hideGridview()" class="dropdownico" id="Highsearch">高级搜索</a>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        课时状态：
                    </th>
                    <td>
                        <cc1:DictionaryDropDownList runat="server" ID="ddl_a999CourseHoursStatusID" DictionaryType="Dic_Sys_CourseHoursStatus"
                            IsShowAll="true" />
                    </td>
                    <th>
                        讲&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;师：
                    </th>
                    <td>
                        <asp:TextBox ID="txt_d999RealName" runat="server" CssClass="inputbox_120"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>
                        培训日期：
                    </th>
                    <td colspan="3">
                        <cc1:DateTimeTextBox ID="begin_a999TrainingDate" runat="server" EndTimeControlID="end_a999TrainingDate"></cc1:DateTimeTextBox>
                        至
                        <cc1:DateTimeTextBox ID="end_a999TrainingDate" runat="server" BeginTimeControlID="begin_a999TrainingDate"></cc1:DateTimeTextBox>
                    </td>
                </tr>
            </table>
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
                DataKeyNames="ItemCourseHoursID" OnRowDataBound="CustomGridView1_RowDataBound"
                OnRowCommand="CustomGridView1_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="项目名称" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="lblItemName" runat="server" ShowTextNum="50" Text='<%# Eval("ItemName")%>'></cc1:ShortTextLabel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="课程名称" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <asp:HiddenField ID="hfTrainingItemCourseID" runat="server" Value='<%# Eval("TrainingItemCourseID") %>' />
                            <cc1:ShortTextLabel ID="lblCourseName" runat="server" ShowTextNum="50" Text='<%# Eval("CourseName")%>'></cc1:ShortTextLabel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="培训日期" HeaderStyle-Width="80">
                        <ItemTemplate>
                            <asp:Label ID="lblTrainingDate" runat="server" Text='<%# Eval("TrainingDate").ToDate()%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="培训时段" HeaderStyle-Width="80">
                        <ItemTemplate>
                            <asp:Label ID="lblTrainingBeginTime" runat="server" Text='<%#  Eval("TrainingBeginTime").ToDateTime().ToString("HH:mm")%>'></asp:Label>至<asp:Label
                                ID="lblTrainingEndTime" runat="server" Text='<%#  Eval("TrainingEndTime").ToDateTime().ToString("HH:mm")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="讲师" HeaderStyle-Width="80" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <%# Eval("TeacherName")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="培训地点" HeaderStyle-Width="60">
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="lblAddress" runat="server" ShowTextNum="50" Text='<%# Eval("ClassRoomName")+"（"+ Eval("Address")+"）"%>'></cc1:ShortTextLabel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="课时状态" HeaderStyle-Width="60">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="dlblCourseHoursStatus" DictionaryType="Dic_Sys_CourseHoursStatus"
                                FieldIDValue='<%# Eval("CourseHoursStatusID") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="学员人数" HeaderStyle-Width="60">
                        <ItemTemplate>
                            <asp:Label ID="lblStudentTotal" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="操作" HeaderStyle-Width="80">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnView" runat="server" Text="查看" CommandName="View"></asp:LinkButton><asp:LinkButton
                                ID="lbtnSignInTable" runat="server" Text="签到表" CommandName="SignInTable" CommandArgument='<%# Eval("ItemCourseHoursID") %>'></asp:LinkButton>
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
                    DataKeyNames="ItemCourseHoursStudentID">
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
                                <cc1:DictionaryLabel ID="lblOrganization" DictionaryType="Dic_CurrentAndSubOrganization"
                                    FieldIDValue='<%# Eval("OrganizationID") %>' runat="server" />
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
                            <%# Eval("SigninTypeName")%>
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
    </div>
</asp:Content>
