<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="ProjectCourseList.aspx.cs" Inherits="TraningImplement_TraningProjectQuery_ProjectCourseList" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBack" runat="Server">
    <asp:LinkButton ID="lbtnBack" runat="server" CssClass="btn_Return" Text="返回" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <!--查找条件-->
        <div class="dv_GradeviewList fixedTable">
            <table>
                <tr>
                    <th width="100">
                        项目编码：
                    </th>
                    <td width="300">
                        <asp:Label ID="Lbl_ItemCode" runat="server" Text=""></asp:Label>
                    </td>
                    <th width="100">
                        项目名称：
                    </th>
                    <td>
                        <asp:Label ID="Lbl_ItemName" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        项目周期：
                    </th>
                    <td>
                        <asp:Label ID="lbl_ItemDate" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <!--查找结果-->
        <div class="dv_searchlist">
            <!--翻页-->
            <div class="dv_pagePanel" id="divPage1">
                <div class="dv_pageControl">
                    <uc2:PageSet ID="PageSet1" runat="server" />
                </div>
            </div>
            <!--列表 begin-->
            <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="false"
                CustomAllowPaging="false" ShowFooter="false" AutoCreateColumnInsertIndex="0"
                DataKeyNames="TrainingItemCourseID" OnRowDataBound="CustomGridView1_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="CourseCode" HeaderText="课程编码" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft" />
                    <asp:TemplateField HeaderText="课程名称" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnCourseName" runat="server" Text='<%# Eval("CourseName")%>' ToolTip='<%# Eval("CourseName")%>'
                                CommandArgument='<%# Eval("CourseID")%>'></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="状态" HeaderStyle-Width="30">
                        <ItemTemplate>
                            <%# Eval("CourseStatus").ToBoolean() ? "启用" : "停用"%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="课程类型" HeaderStyle-Width="60" >
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="DictionaryCourseType" DictionaryType="Dic_Sys_CourseType"
                                FieldIDValue='<%# Eval("CourseTypeID") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="课程属性" HeaderStyle-Width="60">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="DictionaryCourseAttr" DictionaryType="Dic_Sys_CourseAttr"
                                FieldIDValue='<%# Eval("CourseAttrID") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="授课方式" HeaderStyle-Width="60">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="DictionaryTeachModel" DictionaryType="Dic_Sys_TeachModel"
                                FieldIDValue='<%# Eval("TeachModelID") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="培训开始" HeaderStyle-Width="70">
                        <ItemTemplate>
                            <%# Eval("CourseBeginTime").ToDate()%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="培训截止" HeaderStyle-Width="70">
                        <ItemTemplate>
                            <%# Eval("CourseEndTime").ToDate()%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="CourseHours" HeaderText="课程学时" HeaderStyle-Width="60" HeaderStyle-CssClass="alignright" ItemStyle-CssClass="alignright"/>
                    <asp:TemplateField HeaderText="讲师数" HeaderStyle-Width="60">
                        <ItemTemplate>
                          <asp:LinkButton ID="lbtnTeacherTotal" runat="server"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="学员数" HeaderStyle-Width="50">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnStudentTotal" runat="server"></asp:LinkButton>
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
