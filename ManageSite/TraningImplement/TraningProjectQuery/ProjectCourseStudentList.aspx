<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="ProjectCourseStudentList.aspx.cs" Inherits="TraningImplement_TraningProjectQuery_ProjectCourseStudentList" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBack" runat="Server">
    <asp:LinkButton ID="lbtnBack" runat="server" CssClass="btn_Return" Text="返回" />
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
                <th>
                    项目周期：
                </th>
                <td colspan="3">
                    <asp:Label ID="lblItemDate" runat="server"></asp:Label>
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
                    课程周期：
                </th>
                <td colspan="3">
                    <asp:Label ID="lblCourseDate" runat="server"></asp:Label>
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
            DataKeyNames="StudentCourseID" 
            onrowdatabound="CustomGridView1_RowDataBound">
            <Columns>
                <asp:BoundField DataField="WorkerNo" HeaderText="工号" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft field10"/>
                <asp:BoundField DataField="RealName" HeaderText="姓名" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft field8" />
                <asp:TemplateField HeaderText="组织机构" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                    <ItemTemplate>
                        <cc1:DictionaryLabel ID="lblOrganization" DictionaryType="Dic_CurrentAndSubOrganization" FieldIDValue='<%# Eval("OrganizationID") %>'
                            runat="server" TextLength="10" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_department%>" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                    <ItemTemplate>
                        <cc1:DictionaryLabel ID="lblDepartmentID" DictionaryType="vw_Dic_Sys_Department"
                            TextLength="8" FieldIDValue='<%# Eval("DepartmentID") %>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_position%>" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                    <ItemTemplate>
                        <cc1:DictionaryLabel ID="lblPost" DictionaryType="vw_Dic_Sys_Post" FieldIDValue='<%# Eval("PostID") %>'
                            TextLength="8" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_rank%>"  HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                    <ItemTemplate>
                        <cc1:DictionaryLabel ID="lblRank" DictionaryType="vw_Dic_Sys_Rank" FieldIDValue='<%# Eval("RankID") %>'
                            TextLength="8" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="学员课程状态" HeaderStyle-Width="80">
                    <ItemTemplate>
                        <cc1:DictionaryLabel ID="lblIsUseStudentCourse" DictionaryType="Dic_Status" FieldIDValue='<%# Eval("IsUseStudentCourse") %>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="学员状态" HeaderStyle-Width="60">
                    <ItemTemplate>
                        <cc1:DictionaryLabel ID="lblStatus" DictionaryType="Dic_Status" FieldIDValue='<%# Eval("Status") %>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="成绩" HeaderStyle-Width="40" HeaderStyle-CssClass="alignright" ItemStyle-CssClass="alignright">
                    <ItemTemplate>
                       <%# Eval("Score")%>
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
