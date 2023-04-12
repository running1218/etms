<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="CourseList.aspx.cs" Inherits="TraningImplement_CourseTeacherManager_CourseList" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <!--查找条件-->
        <div class="dv_searchbox">
            <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <th width="120">
                        项目名称：
                    </th>
                    <th width="220">
                        <asp:DropDownList ID="ddl_ItemName" runat="server" CssClass="easyui-combobox">
                        </asp:DropDownList>
                    </th>
                    <td width="200">
                        <asp:UpdatePanel ID="upQuery" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Button ID="btnSearch" CssClass="btn_Search" runat="server" Text="查询" OnClick="btnSearch_Click" /></ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </div>
        <!--查找结果-->
        <div class="dv_searchlist">
            <asp:UpdatePanel ID="upList" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <!--翻页-->
                    <div class="dv_pagePanel" id="divPage1">
                        <div class="dv_pageControl">
                            <uc2:PageSet ID="PageSet1" runat="server" />
                        </div>
                    </div>
                    <!--列表 begin-->
                    <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="false"
                        CustomAllowPaging="false" ShowFooter="false" AutoCreateColumnInsertIndex="0"
                        DataKeyNames="TrainingItemCourseID" OnRowDataBound="CustomGridView1_RowDataBound"
                        OnRowCommand="CustomGridView1_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="CourseCode"  HeaderText="课程编码" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft field12" />
                            <asp:TemplateField HeaderText="课程名称" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                                <ItemTemplate>
                                    <cc1:ShortTextLabel ID="lblCourseName" runat="server" ShowTextNum="50" Text='<%# Eval("CourseName")%>'></cc1:ShortTextLabel>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="状态" HeaderStyle-Width="50">
                                <ItemTemplate>
                                    <%# Eval("CourseStatus").ToBoolean() ? "启用" : "停用"%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="培训开始" HeaderStyle-Width="80">
                                <ItemTemplate>
                                    <%# Eval("CourseBeginTime").ToDate()%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="培训截止" HeaderStyle-Width="80">
                                <ItemTemplate>
                                    <%# Eval("CourseEndTime").ToDate()%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="培训方式" HeaderStyle-Width="60">
                                <ItemTemplate>
                                    <cc1:DictionaryLabel ID="DictionaryTrainingModel" DictionaryType="Dic_Sys_TrainingModel"
                                        FieldIDValue='<%# Eval("TrainingModelID") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="授课方式" HeaderStyle-Width="60">
                                <ItemTemplate>
                                    <cc1:DictionaryLabel ID="DictionaryTeachModel" DictionaryType="Dic_Sys_TeachModel" FieldIDValue='<%# Eval("TeachModelID") %>'
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="讲师数" HeaderStyle-Width="60">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_TeacherTotal" runat="server">0</asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="操作" HeaderStyle-Width="60">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtn_SetTeacher" runat="server" CommandName="SetTeacher">讲师管理</asp:LinkButton>
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
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
