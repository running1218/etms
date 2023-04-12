<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="CourseList.aspx.cs" Inherits="TraningImplement_StudentCourseManager_CourseList" %>

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
                    <td style="width: 220px;">
                        <asp:DropDownList ID="ddl_ItemName" runat="server" CssClass="select_210">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 70px;">
                        <asp:Button ID="btnSearch" CssClass="btn_Search" runat="server" Text="查询" OnClick="btnSearch_Click" />
                    </td>
                    <th width="120">
                        项目学员数：
                    </th>
                    <td>
                        <asp:Label ID="labProjectStudentTotal" runat="server">0</asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <!--查找结果-->
        <div class="dv_searchlist">
            <!--翻页-->
            <div class="dv_pagePanel" id="divPage1">
                <div class="dv_selectAll">
                    <cc1:CustomButton runat="server" ID="cbtnOpenAllCourse" Text="项目学员开放所有课程" CssClass="btn_Open_Course"
                        EnableConfirm="true" ConfirmTitle="提示" ConfirmMessage="您确认为项目所有学员开放所有课程吗？" OnClick="cbtnOpenAllCourse_Click" Visible="false" /><cc1:CustomButton
                            runat="server" ID="cbtnCloseAllCourse" Text="清除学员开放课程信息" CssClass="btn_Clear_Course" EnableConfirm="true"
                            ConfirmTitle="提示" ConfirmMessage="您确认将项目所有学员开放的课程全部清除吗？" OnClick="cbtnCloseAllCourse_Click"  Visible="false" />
                </div>
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
                    <asp:BoundField DataField="CourseCode" HeaderText="课程编码" ItemStyle-CssClass="alignleft"
                        HeaderStyle-CssClass="alignleft field12" />
                    <asp:TemplateField HeaderText="课程名称" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="lblCourseName" runat="server" ShowTextNum="10" Text='<%# Eval("CourseName")%>'></cc1:ShortTextLabel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="课程类型" HeaderStyle-Width="60">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="DictionaryLabel0" DictionaryType="Dic_Sys_CourseType" FieldIDValue='<%# Eval("CourseTypeID") %>'
                                runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="授课属性" HeaderStyle-Width="60">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="DictionaryLabel1" DictionaryType="Dic_Sys_CourseAttr" FieldIDValue='<%# Eval("CourseAttrID") %>'
                                runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="开始日期" HeaderStyle-Width="70">
                        <ItemTemplate>
                            <%# Eval("CourseBeginTime").ToDate()%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="截止日期" HeaderStyle-Width="70">
                        <ItemTemplate>
                            <%# Eval("CourseEndTime").ToDate()%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="状态" HeaderStyle-Width="40">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="lblCourseStatus" DictionaryType="Dic_Status" FieldIDValue='<%# Eval("CourseStatus") %>'
                                runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="学员数" HeaderStyle-Width="60">
                        <ItemTemplate>
                            <asp:Label ID="lbl_SignUpStudentTotal" runat="server">0</asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="操作" HeaderStyle-Width="140">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtn_SetStudent" runat="server" CommandName="SetStudent">学员管理</asp:LinkButton><cc1:CustomLinkButton
                                runat="server" ID="lbtnAddAll" CommandName="AddAll" CommandArgument='<%# Eval("TrainingItemCourseID") %>'
                                Text="添加全部学员" EnableConfirm="true" ConfirmTitle="提示" ConfirmMessage="确定添加全部学员吗？"
                                Visible="false" />
                            <asp:LinkButton ID="lbtnAddAll2" runat="server" Text="添加全部学员"></asp:LinkButton>
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
