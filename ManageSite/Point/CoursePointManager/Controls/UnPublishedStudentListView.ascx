<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UnPublishedStudentListView.ascx.cs"
    Inherits="Point_CoursePointManager_Controls_UnPublishedStudentListView" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<div class="dv_GradeviewList">
    <table class="">
        <tr>
            <th width="120">
                培训项目：
            </th>
            <td width="220">
                <cc1:ShortTextLabel ID="lbl_ItemName" runat="server"></cc1:ShortTextLabel>
            </td>
            <th width="120">
                课程名称：
            </th>
            <td width="220">
                <cc1:ShortTextLabel ID="lbl_CourseName" runat="server"></cc1:ShortTextLabel>
            </td>
        </tr>
        <tr>
            <th width="120">
                课　　时：
            </th>
            <td>
                <cc1:ShortTextLabel ID="lbl_CourseHours" runat="server"></cc1:ShortTextLabel>
            </td>
            <th width="120">
                课程属性：
            </th>
            <td>
                <cc1:DictionaryLabel ID="lblCourseAttrID" runat="server" DictionaryType="Dic_Sys_CourseAttr" />
            </td>
        </tr>
    </table>
</div>
<div class="dv_GradeviewList">
    <table>
        <tr id="trOrg" runat="server">
            <th>
                组织机构：
            </th>
            <td>
                <cc1:DictionaryDropDownList ID="ddl_OrganizationID" runat="server" DictionaryType="Dic_CurrentAndSubOrganization"
                    IsShowAll="true" AutoPostBack="true" OnSelectedIndexChanged="ddl_Organization_SelectedIndexChanged"
                    CssClass="select_210" />
            </td>
            <th>
                部&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;门：
            </th>
            <td>
                <asp:DropDownList ID="ddl_DepartmentID" runat="server" CssClass="select_190" />
            </td>
        </tr>
        <tr>
            <th width="120">
                获得积分方式：
            </th>
            <td width="220">
                <asp:DropDownList runat="server" ID="ddl_AccessPointsMode">
                    <asp:ListItem Value="0">全部</asp:ListItem>
                    <asp:ListItem Value="1">系统计算</asp:ListItem>
                    <asp:ListItem Value="2">手动设置</asp:ListItem>
                </asp:DropDownList>
            </td>
            <th width="120">
                学员姓名：
            </th>
            <td width="220">
                <asp:TextBox ID="txt_RealName" runat="server" CssClass="inputbox_120 floatleft" MaxLength="50"></asp:TextBox>
                <asp:Button ID="btnSearch" CssClass="btn_Search" runat="server" Text="查询" OnClick="btnSearch_Click" />
            </td>
        </tr>
    </table>
</div>
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
            <asp:TemplateField HeaderText="用户ID" Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lblUserID" runat="server" Text='<%#Eval("UserID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="学员姓名" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft field8">
                <ItemTemplate>
                    <%# Eval("RealName")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="组织机构" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                <ItemTemplate>
                    <cc1:DictionaryLabel ID="lblOrganization" DictionaryType="Dic_CurrentAndSubOrganization"
                        FieldIDValue='<%# Eval("OrganizationID") %>' runat="server" TextLength="10" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_department%>" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                <ItemTemplate>
                    <cc1:DictionaryLabel ID="lblDepartmentID" DictionaryType="vw_Dic_Sys_Department"
                        FieldIDValue='<%# Eval("DepartmentID") %>' runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="成绩" HeaderStyle-CssClass="alignright" ItemStyle-CssClass="alignright"
                HeaderStyle-Width="60">
                <ItemTemplate>
                    <%# Eval("SumGrade")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="课程积分" HeaderStyle-CssClass="alignright" ItemStyle-CssClass="alignright"
                HeaderStyle-Width="60">
                <ItemTemplate>
                    <%# Eval("AccessPoints")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="积分获得方式" HeaderStyle-Width="100">
                <ItemTemplate>
                    <asp:Label ID="lblAccessPointsMode" runat="server" Text='<%# GetAccessPointModeValue(Eval("AccessPointsMode").ToString())%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="获得积分时间" HeaderStyle-Width="100">
                <ItemTemplate>
                    <asp:Label ID="lblAccessPointsTime" runat="server" Text='<%# Eval("AccessPointsTime").ToDate() %>'></asp:Label>
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
