<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="TeacherTraningItemCourseHoursList.aspx.cs" Inherits="Security_TeacherQuery_TeacherTraningItemCourseHoursList" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBack" runat="Server">
    <asp:LinkButton ID="lbtnReturn" runat="server" CssClass="btn_Return" Text="返回"></asp:LinkButton>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <!--查找条件-->
        <div class="dv_GradeviewList">
            <table id="Table1" class="fixedTable" border="0" cellpadding="0" cellspacing="0" runat="server">
                <tr>
                    <th width="100">
                        讲师姓名：
                    </th>
                    <td width="200">
                        <asp:Label ID="lblTeacherName" runat="server"></asp:Label>
                    </td>
                    <th width="100">
                        <asp:Label ID="lblOrgName" runat="server" Text="<%$ Resources:UIResource, ui_department%>"></asp:Label>/
                        <asp:Literal ID="ltlDepartment" runat="server" Text="<%$ Resources:UIResource, ui_org%>"></asp:Literal>：
                    </th>
                    <td>
                        <asp:Label ID="lblOrg" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        课程开始日期：
                    </th>
                    <td colspan="3">
                        <asp:Label ID="lblItemDate" runat="server"></asp:Label>
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
                 DataKeyNames="ItemCourseHoursID" OnRowDataBound="CustomGridView1_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="项目名称" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft" >
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="lblItemName" runat="server" ShowTextNum="10" Text='<%# Eval("ItemName")%>'></cc1:ShortTextLabel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="课程名称" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft" >
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="lblCourseName" runat="server" ShowTextNum="10" Text='<%# Eval("CourseName")%>'></cc1:ShortTextLabel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="培训日期" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft" HeaderStyle-Width="80">
                        <ItemTemplate>
                            <asp:Label ID="lblTrainingDate" runat="server" Text='<%# Eval("TrainingDate").ToDate()%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="培训时段" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft" HeaderStyle-Width="100">
                        <ItemTemplate>
                            <asp:Label ID="lblTrainingBeginTime" runat="server" Text='<%#  Eval("TrainingBeginTime").ToDateTime().ToString("HH:mm")%>'></asp:Label>至<asp:Label
                                ID="lblTrainingEndTime" runat="server" Text='<%#  Eval("TrainingEndTime").ToDateTime().ToString("HH:mm")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="培训地点"  HeaderStyle-Width="100" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="lblAddress" runat="server" ShowTextNum="50" Text='<%# GetAddress(Eval("ClassRoomName"),Eval("Address")) %>'></cc1:ShortTextLabel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="学员人数" HeaderStyle-Width="60" HeaderStyle-CssClass="alignright" ItemStyle-CssClass="alignright">
                        <ItemTemplate>
                            <asp:Label ID="lblStudentTotal" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="课时状态" HeaderStyle-Width="60">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="dlblCourseHoursStatus" DictionaryType="Dic_Sys_CourseHoursStatus"
                                FieldIDValue='<%# Eval("CourseHoursStatusID") %>' runat="server" />                            
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="培训课时" HeaderStyle-Width="80" HeaderStyle-CssClass="alignright" ItemStyle-CssClass="alignright">
                        <ItemTemplate>
                            <%# Eval("CourseHours")%>
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
