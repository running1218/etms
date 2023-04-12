<%@ Page Title="培训计划审核" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="PlanAuditList.aspx.cs" Inherits="ETMS.WebApp.Manage.TraningPlan.TraningPlanAudit.PlanAuditList" %>

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
                    <td width="125">
                        <asp:TextBox ID="txt_PlanCode" runat="server" CssClass="inputbox_120"></asp:TextBox>
                    </td>
                    <th width="100">
                        计划名称：
                    </th>
                    <td class="Search_Area">
                        <asp:TextBox ID="txt_PlanName" runat="server" CssClass="inputbox_120 floatleft"></asp:TextBox>
                                <asp:Button ID="btnSearch" CssClass="btn_Search" runat="server" Text="查询" OnClick="btnSearch_Click" />
                        <a href="javascript:hideGridview()" class="dropdownico" id="Highsearch">高级搜索</a>
                    </td>
                </tr>
                <tr>
                    <th>
                        计划状态：
                    </th>
                    <td colspan="3">
                        <cc1:DictionaryDropDownList runat="server" ID="ddl_PlanStatus" DictionaryType="Dic_TraningPlanAuditState"
                            IsShowAll="true" />
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
                        OnRowDataBound="CustomGridView1_RowDataBound" DataKeyNames="PlanID" OnRowCommand="CustomGridView1_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="计划编码" HeaderStyle-Width="80" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                                <ItemTemplate>
                                    <cc1:ShortTextLabel ID="lblPlanCode" runat="server" ShowTextNum="50" Text='<%# Eval("PlanCode")%>'></cc1:ShortTextLabel>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="计划名称" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                                <ItemTemplate>
                                    <cc1:ShortTextLabel ID="lblPlanName" runat="server" ShowTextNum="50" Text='<%# Eval("PlanName")%>'></cc1:ShortTextLabel>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="计划状态" HeaderStyle-Width="60">
                                <ItemTemplate>
                                    <cc1:DictionaryLabel ID="dlblPlanStatus" DictionaryType="Dic_TraningPlanState" FieldIDValue='<%# Eval("PlanStatus") %>'
                                        runat="server" />
                                    <asp:HiddenField ID="Hf_PlanStatus" runat="server" Value='<%# Eval("PlanStatus") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="开始时间" HeaderStyle-Width="70">
                                <ItemTemplate>
                                    <%# Eval("PlanBeginTime").ToDate()%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="结束时间" HeaderStyle-Width="70" >
                                <ItemTemplate>
                                    <%# Eval("PlanEndTime").ToDate()%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="预算" HeaderStyle-Width="85" HeaderStyle-CssClass="alignright" ItemStyle-CssClass="alignright">
                                <ItemTemplate>
                                    <%# Eval("BudgetFee")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="创建时间" HeaderStyle-Width="70">
                                <ItemTemplate>
                                    <%# Eval("CreateTime").ToDate()%>
                                </ItemTemplate>
                            </asp:TemplateField>                            
                            <asp:TemplateField HeaderText="操作" HeaderStyle-Width="100">
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtn_Audit" runat="server" CommandName="Audit">审核</asp:LinkButton><cc1:CustomLinkButton runat="server"
                                            ID="lbtnUnapproved" CommandName="Unapproved" CommandArgument='<%# Eval("PlanID") %>'
                                            Text="取消审核" EnableConfirm="true" ConfirmTitle="提示" ConfirmMessage="确定取消审核吗？" Visible="false" /><asp:LinkButton
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
        </div>
    </div>
</asp:Content>
