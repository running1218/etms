﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="ProjectStudentList.aspx.cs" Inherits="TraningImplement_TraningProjectManager_ProjectStudentList" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <!--查找条件-->
        <div class="dv_searchbox">
            <table class="GridviewGray th100" border="0" cellpadding="0" cellspacing="0" runat="server"
                id="tableQueryControlList">
                <tr>
                    <th width="120">
                        项目编码：
                    </th>
                    <td width="130">
                        <asp:TextBox ID="txt_ItemCode" runat="server" CssClass="inputbox_120" MaxLength="50"></asp:TextBox>
                    </td>
                    <th width="100">
                        项目名称：
                    </th>
                    <td class="Search_Area">
                        <asp:TextBox ID="txt_ItemName" runat="server" CssClass="inputbox_120 floatleft" MaxLength="100"></asp:TextBox>
                        <asp:Button ID="btnSearch" CssClass="btn_Search" runat="server" Text="查询" OnClick="btnSearch_Click" />
                        <a href="javascript:hideGridview()" class="dropdownico" id="Highsearch">高级搜索</a>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        培训级别：
                    </th>
                    <td>
                        <cc1:DictionaryDropDownList runat="server" ID="ddl_TrainingLevelID" DictionaryType="Dic_Sys_TrainingLevel"
                            IsShowAll="true" />
                    </td>
                    <th width="100">
                        来自计划：
                    </th>
                    <td>
                        <cc1:DictionaryDropDownList runat="server" ID="ddl_IsPlanItem" DictionaryType="Dic_TrueOrFalse"
                            IsShowAll="true" />
                    </td>
                </tr>
                <tr class="hide">
                    <th width="100">
                        专业类别：
                    </th>
                    <td>
                        <cc1:DictionaryDropDownList runat="server" ID="ddl_SpecialtyTypeCode" DictionaryType="Dic_Sys_SpecialtyType"
                            IsShowAll="true" />
                    </td>                  
                    <th width="100">
                        创建时间：
                    </th>
                    <td>
                        <cc1:DateTimeTextBox ID="begin_CreateTime" runat="server" EndTimeControlID="end_CreateTime"></cc1:DateTimeTextBox>
                        至
                        <cc1:DateTimeTextBox ID="end_CreateTime" runat="server" BeginTimeControlID="begin_CreateTime"></cc1:DateTimeTextBox>
                    </td>
                </tr>
                <tr class="hide">
                     <th width="100">
                        报名方式：
                    </th>
                    <td colspan="3">
                        <cc1:DictionaryDropDownList runat="server" ID="dddl_SignupModeID" DictionaryType="Dic_Sys_SignupMode"
                            IsShowAll="true" />
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
                DataKeyNames="TrainingItemID" OnRowDataBound="CustomGridView1_RowDataBound" OnRowCommand="CustomGridView1_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="项目编码" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft field12">
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="lblItemCode" runat="server" ShowTextNum="16" Text='<%# Eval("ItemCode")%>'></cc1:ShortTextLabel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="项目名称" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="lblItemName" runat="server" ShowTextNum="20" Text='<%# Eval("ItemName")%>'></cc1:ShortTextLabel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="项目开始时间" HeaderStyle-Width="90">
                        <ItemTemplate>
                            <%# Eval("ItemBeginTime").ToDate()%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="项目结束时间" HeaderStyle-Width="90">
                        <ItemTemplate>
                            <%# Eval("ItemEndTime").ToDate() %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="课程数" HeaderStyle-Width="50" ItemStyle-CssClass="alignright"
                        HeaderStyle-CssClass="alignright">
                        <ItemTemplate>
                            <%#  new ETMS.Components.Basic.Implement.BLL.TrainingItem.Course.Tr_ItemCourseLogic().GetItemCourseCountByTrainingItemID(new Guid(Eval("TrainingItemID").ToString()))%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="学员数" HeaderStyle-Width="50" ItemStyle-CssClass="alignright"
                        HeaderStyle-CssClass="alignright">
                        <ItemTemplate>
                            <%# new ETMS.Components.Basic.Implement.BLL.TrainingItem.Student.Sty_StudentSignupLogic().GetTrainingItemStudentTotal(new Guid(Eval("TrainingItemID").ToString()))%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="报名方式" HeaderStyle-Width="80" HeaderStyle-CssClass="alignleft"
                        ItemStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="lblSignupMode" DictionaryType="Dic_Sys_SignupMode" FieldIDValue='<%# Eval("SignupModeID") %>'
                                TextLength="6" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="操作" HeaderStyle-Width="60">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnStudent" runat="server" Visible="false">设置学员</asp:LinkButton></ItemTemplate>
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
