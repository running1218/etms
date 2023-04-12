<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="TraningProjectList.aspx.cs" Inherits="TraningImplement_TraningProjectAudit_TraningProjectList" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <!--查找条件-->
        <div class="dv_searchbox">
            <table class="GridviewGray th100"  runat="server"  id="tableQueryControlList">
                <tr>
                    <th >
                        项目编码：
                    </th>
                    <td >
                        <asp:TextBox ID="txt_ItemCode" runat="server" CssClass="inputbox_120" MaxLength="50"></asp:TextBox>
                    </td>
                    <th >
                        项目名称：
                    </th>
                    <td class="Search_Area">
                        <asp:TextBox ID="txt_ItemName" runat="server" CssClass="inputbox_120 floatleft" MaxLength="100"></asp:TextBox>
                        <asp:UpdatePanel ID="upQuery" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Button ID="btnSearch" CssClass="btn_Search" runat="server" Text="查询" OnClick="btnSearch_Click" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <a href="javascript:hideGridview()" class="dropdownico" id="Highsearch">高级搜索</a>
                    </td>
                </tr>
                <tr>
                    <th >
                        专业类别：
                    </th>
                    <td>
                        <cc1:DictionaryDropDownList runat="server" ID="ddl_SpecialtyTypeCode" DictionaryType="Dic_Sys_SpecialtyType" SelectedDefaultValue=""
                            IsShowAll="true" />
                    </td>                        
                    <th >
                        项目状态：
                    </th>
                    <td>
                        <cc1:DictionaryDropDownList runat="server" ID="ddl_ItemStatus" DictionaryType="Dic_TraningProjectType" SelectedDefaultValue=""
                            IsShowAll="true" />
                    </td>
                </tr>
                <tr class="hide">
                    <th >
                        来自计划：
                    </th>
                    <td>
                        <cc1:DictionaryDropDownList runat="server" ID="ddl_IsPlanItem" DictionaryType="Dic_TrueOrFalse"
                            IsShowAll="true" />
                    </td>           
                </tr>
                <tr>
                    <th >
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
                        <div class="dv_pageControl">
                            <uc2:PageSet ID="PageSet1" runat="server" />
                        </div>
                    </div>
                    <!--列表 begin-->
                    <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="false"
                        CustomAllowPaging="false" ShowFooter="false" AutoCreateColumnInsertIndex="0"
                        DataKeyNames="TrainingItemID" OnRowDataBound="CustomGridView1_RowDataBound" OnRowCommand="CustomGridView1_RowCommand">
                        <Columns>
                        <asp:TemplateField HeaderText="项目编码"  ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft field12">
                            <ItemTemplate>
                                <cc1:ShortTextLabel ID="lblItemCode" runat="server" ShowTextNum="50" Text='<%# Eval("ItemCode")%>'></cc1:ShortTextLabel>
                            </ItemTemplate>
                        </asp:TemplateField>
                            <asp:TemplateField HeaderText="项目名称" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                                <ItemTemplate>
                                    <cc1:ShortTextLabel ID="lblItemName" runat="server" ShowTextNum="50" Text='<%# Eval("ItemName")%>'></cc1:ShortTextLabel>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="项目状态" HeaderStyle-Width="60">
                                <ItemTemplate>
                                    <cc1:DictionaryLabel ID="DictionaryLabel1" DictionaryType="Dic_TraningProjectType"
                                        FieldIDValue='<%# Eval("ItemStatus") %>' runat="server" />
                                    <asp:HiddenField ID="Hf_ItemStatus" runat="server" Value='<%# Eval("ItemStatus") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="所属计划" HeaderStyle-Width="60">
                                <ItemTemplate>
                                    <cc1:ShortTextLabel ID="lblPlan" runat="server" ShowTextNum="10" ></cc1:ShortTextLabel>
                                    <asp:HiddenField ID="hfPlanID" runat="server" Value='<%# Eval("PlanID")%>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="专业类别" HeaderStyle-Width="60">
                                <ItemTemplate>
                                    <cc1:DictionaryLabel ID="DictionaryLabelSpecialtyType" DictionaryType="Dic_Sys_SpecialtyType"
                                        runat="server" FieldIDValue='<%# Eval("SpecialtyTypeCode")%>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="预算（元）" HeaderStyle-Width="80" HeaderStyle-CssClass="alignright" ItemStyle-CssClass="alignright">
                                <ItemTemplate>
                                    <%# Eval("BudgetFee")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="创建时间" HeaderStyle-Width="80">
                                <ItemTemplate>
                                    <%# Eval("CreateTime").ToDate()%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="DutyUser" HeaderText="负责人"  HeaderStyle-CssClass=" field8"  />
                            <asp:TemplateField HeaderText="操作" HeaderStyle-Width="90">
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnAudit" runat="server" Visible="false">审核</asp:LinkButton><cc1:CustomLinkButton runat="server"
                                            ID="lbtnUnapproved" CommandName="Unapproved" CommandArgument='<%# Eval("TrainingItemID") %>'
                                            Text="取消审核" EnableConfirm="true" ConfirmTitle="提示" ConfirmMessage="确定取消审核吗？" Visible="false" /><asp:LinkButton
                                                ID="lbtnView" runat="server" CommandName="lbtnView" CommandArgument='<%# Eval("TrainingItemID") %>'>查看</asp:LinkButton></ItemTemplate>
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
