<%@ Page Title="培训计划管理" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="TraningPlanList.aspx.cs" Inherits="ETMS.WebApp.Manage.TraningPlan.TraningPlan.TraningPlanList" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <!--查找条件-->
        <div class="dv_searchbox">
            <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0" runat="server"
                id="tableQueryControlList">
                <tr>
                    <th width="120">
                        计划编码：
                    </th>
                    <td width="130">
                        <asp:TextBox ID="txt_PlanCode" runat="server" CssClass="inputbox_120"></asp:TextBox>
                    </td>
                    <th width="100">
                        计划名称：
                    </th>
                    <td class="Search_Area">
                        <asp:TextBox ID="txt_PlanName" runat="server" CssClass="inputbox_120 floatleft"></asp:TextBox>
                        <asp:UpdatePanel ID="upQuery" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Button ID="btnSearch" CssClass="btn_Search" runat="server" Text="查询" OnClick="btnSearch_Click" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <a href="javascript:hideGridview()" class="dropdownico" id="Highsearch">高级搜索</a>
                    </td>
                </tr>
                <tr>
                    <th>
                        计划状态：
                    </th>
                    <td>
                        <cc1:DictionaryDropDownList runat="server" ID="ddl_PlanStatus" DictionaryType="Dic_TraningPlanState"
                            IsShowAll="true" />
                    </td>
                    <th>
                        类&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;型：
                    </th>
                    <td>
                        <cc1:DictionaryDropDownList runat="server" ID="ddl_PlanTypeID" DictionaryType="Dic_Sys_PlanType"
                            IsShowAll="true" />
                    </td>
                </tr>
                <tr>
                    <th>
                        培训级别：
                    </th>
                    <td colspan="3">
                        <cc1:DictionaryDropDownList runat="server" ID="ddl_TrainingLevelID" DictionaryType="Dic_Sys_TrainingLevel"
                            IsShowAll="true" />
                    </td>
                </tr>
                <tr>
                    <th>
                        创建时间：
                    </th>
                    <td colspan="3">
                        <cc1:DateTimeTextBox ID="begin_CreateTime" runat="server" EndTimeControlID="end_CreateTime"></cc1:DateTimeTextBox>
                        至
                        <cc1:DateTimeTextBox ID="end_CreateTime" runat="server" BeginTimeControlID="begin_CreateTime"></cc1:DateTimeTextBox>
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
                        <div class="dv_selectAll">
                            <asp:Button ID="btnAdd" CssClass="btn_Add" runat="server" Text="新增" />
                        </div>
                        <div class="dv_pageControl">
                            <uc2:PageSet ID="PageSet1" runat="server" />
                        </div>
                    </div>
                    <!--列表 begin-->
                    <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="false"
                        CustomAllowPaging="false" ShowFooter="false" AutoCreateColumnInsertIndex="0"
                        OnRowDataBound="CustomGridView1_RowDataBound" DataKeyNames="PlanID" OnRowCommand="CustomGridView1_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="计划编码"   ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft field12">
                                <ItemTemplate>
                                    <cc1:ShortTextLabel ID="lblPlanCode" runat="server" ShowTextNum="20" Text='<%# Eval("PlanCode")%>'></cc1:ShortTextLabel>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="计划名称" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                                <ItemTemplate>
                                    <cc1:ShortTextLabel ID="lblPlanName" runat="server" ShowTextNum="50" Text='<%# Eval("PlanName")%>'></cc1:ShortTextLabel>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="开始时间" HeaderStyle-Width="70">
                                <ItemTemplate>
                                    <%# Eval("PlanBeginTime").ToDate()%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="结束时间" HeaderStyle-Width="70">
                                <ItemTemplate>
                                    <%# Eval("PlanEndTime").ToDate()%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="计划状态" HeaderStyle-Width="60" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                                <ItemTemplate>
                                    <cc1:DictionaryLabel ID="dlblPlanStatus" DictionaryType="Dic_TraningPlanState" FieldIDValue='<%# Eval("PlanStatus") %>'
                                        runat="server" />
                                    <asp:HiddenField ID="Hf_PlanStatus" runat="server" Value='<%# Eval("PlanStatus") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="培训级别" HeaderStyle-Width="60">
                                <ItemTemplate>
                                    <cc1:DictionaryLabel ID="dlblTrainingLevelID" DictionaryType="Dic_Sys_TrainingLevel"
                                        FieldIDValue='<%# Eval("TrainingLevelID") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="是否启用" HeaderStyle-Width="60">
                                <ItemTemplate>
                                    <cc1:DictionaryLabel ID="dlblIsUse" DictionaryType="Dic_TrueOrFalse" FieldIDValue='<%# Eval("IsUse") %>'
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="课程数"  HeaderStyle-Width="50">
                                <ItemTemplate>
                                    <asp:Label ID="lblCourseTotal" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="操作" HeaderStyle-Width="140">
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtn_Edit" runat="server" CommandArgument="Edit" Enabled="false">编辑</asp:LinkButton><cc1:CustomLinkButton
                                        ID="lbtn_Del" runat="server" CommandName="Dels" CommandArgument='<%# Eval("PlanID") %>'
                                        EnableConfirm="false" Enabled="false" ConfirmTitle="提示" ConfirmMessage="确定删除吗？">删除</cc1:CustomLinkButton><asp:LinkButton
                                            ID="lbtn_SetCourse" runat="server" CommandName="SetCourse" Enabled="false">课程</asp:LinkButton><cc1:CustomLinkButton
                                                runat="server" ID="lbtn_IsUse" Text="启用" CommandName="IsUse" EnableConfirm="false"
                                                Enabled="false" ConfirmTitle="提示" ConfirmMessage="确定启用吗？" CommandArgument='<%# GetPlanIDIsUseValue(Eval("PlanID"), Eval("IsUse")) %>' /><asp:LinkButton
                                                    ID="lbtn_View" runat="server" CommandName="View">查看</asp:LinkButton>
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
