<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="ProjectStudentCourseList.aspx.cs" Inherits="TraningImplement_TraningProjectQuery_ProjectStudentCourseList" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBack" runat="Server">
    <asp:LinkButton ID="lbtnReturn" runat="server" Text="返回" CssClass="btn_Return"></asp:LinkButton>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--查找条件-->
    <div class="dv_GradeviewList">
        <table class="" border="0" cellpadding="0" cellspacing="0">
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
                <th width="100">
                    项目周期：
                </th>
                <td colspan="3">
                    <asp:Label ID="lblItemDate" runat="server"></asp:Label>
                </td>
            </tr>
            <tr id="trOrg" runat="server">
                <th>
                    组织机构：
                </th>
                <td colspan="3">
                    <cc1:DictionaryLabel ID="lblOrganization" DictionaryType="Dic_CurrentAndSubOrganization"
                        runat="server" />
                </td>
            </tr>
            <tr>
                <th>
                    <asp:Literal ID="ltlDepartment" runat="server" Text="<%$ Resources:UIResource, ui_department%>"></asp:Literal>：
                </th>
                <td>
                    <cc1:DictionaryLabel ID="lblDepartmentID" DictionaryType="vw_Dic_Sys_Department"
                        runat="server" />
                </td>
                <th>
                    <asp:Literal ID="ltlPosition" runat="server" Text="<%$ Resources:UIResource, ui_position%>"></asp:Literal>：
                </th>
                <td>
                    <cc1:DictionaryLabel ID="lblPost" DictionaryType="vw_Dic_Sys_Post" runat="server" />
                </td>
            </tr>
            <tr>
                <th>
                    学员姓名：
                </th>
                <td colspan="3">
                    <asp:Label ID="lblRealName" runat="server"></asp:Label>
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
            CustomAllowPaging="false" ShowFooter="false" AutoCreateColumnInsertIndex="0"
            DataKeyNames="TrainingItemCourseID" OnRowDataBound="CustomGridView1_RowDataBound">
            <Columns>
                <asp:BoundField DataField="CourseCode" HeaderText="课程编码" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft"  />
                <asp:TemplateField HeaderText="课程名称" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnCourseName" runat="server" Text='<%# Eval("CourseName")%>'
                            ToolTip='<%# Eval("CourseName")%>' CommandArgument='<%# Eval("CourseID")%>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="学员课程状态" HeaderStyle-Width="80">
                    <ItemTemplate>
                        <%# Eval("IsUseStudentCourse").ToBoolean() ? "启用" : "停用"%>
                        <asp:HiddenField ID="hfCourseStatus" runat="server" Value='<%# Eval("IsUseStudentCourse") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="课程属性" HeaderStyle-Width="60">
                    <ItemTemplate>
                        <cc1:DictionaryLabel ID="dlblCourseAttr" DictionaryType="Dic_Sys_CourseAttr" FieldIDValue='<%# Eval("CourseAttrID") %>'
                            runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="授课方式" HeaderStyle-Width="60">
                    <ItemTemplate>
                        <cc1:DictionaryLabel ID="dlblTeachModel" DictionaryType="Dic_Sys_TeachModel" FieldIDValue='<%# Eval("TeachModelID") %>'
                            runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="开始日期" HeaderStyle-Width="80">
                    <ItemTemplate>
                        <%# Eval("CourseBeginTime").ToDate()%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="截止日期" HeaderStyle-Width="80">
                    <ItemTemplate>
                        <%# Eval("CourseEndTime").ToDate()%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="CourseHours" HeaderText="课时" ItemStyle-CssClass="alignright" HeaderStyle-CssClass="alignright" HeaderStyle-Width="50" />
                <asp:TemplateField HeaderText="负责讲师" ItemStyle-CssClass="alignright" HeaderStyle-CssClass="alignright" HeaderStyle-Width="60">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnTeacherTotal" runat="server"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="成绩" ItemStyle-CssClass="alignright" HeaderStyle-CssClass="alignright" HeaderStyle-Width="50">
                    <ItemTemplate>
                        <%# Eval("SumGrade")%>
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
</asp:Content>
