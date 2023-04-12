<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="TraningPlanResultList.aspx.cs" Inherits="TraningPlan_TraningPlanResult_TraningPlanResultList" %>

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
                        <asp:Button ID="btnSearch" CssClass="btn_Search" runat="server" Text="查询" OnClick="btnSearch_Click" />
                        <a href="javascript:hideGridview()" class="dropdownico" id="Highsearch">高级搜索</a>
                    </td>
                </tr>
                <tr>
                    <th>
                        计划状态：
                    </th>
                    <td>
                        <cc1:DictionaryDropDownList runat="server" ID="ddl_PlanStatus" DictionaryType="Dic_TraningPlanResultState"
                            IsShowAll="true" />
                    </td>
                    <th>
                        计划类型：
                    </th>
                    <td>
                        <cc1:DictionaryDropDownList runat="server" ID="ddl_PlanTypeID" DictionaryType="Dic_Sys_PlanType"
                            IsShowAll="true" />
                    </td>
                </tr>
                <tr>
                    <th>
                        计划开始时间：
                    </th>
                    <td colspan="3">
                        <cc1:DateTimeTextBox ID="begin_PlanBeginTime" runat="server" EndTimeControlID="end_PlanBeginTime"></cc1:DateTimeTextBox>
                        至
                        <cc1:DateTimeTextBox ID="end_PlanBeginTime" runat="server" BeginTimeControlID="begin_PlanBeginTime"></cc1:DateTimeTextBox>
                    </td>
                </tr>
                <tr>
                    <th>
                        计划结束时间：
                    </th>
                    <td colspan="3">
                        <cc1:DateTimeTextBox ID="begin_PlanEndTime" runat="server" EndTimeControlID="end_PlanEndTime"></cc1:DateTimeTextBox>
                        至
                        <cc1:DateTimeTextBox ID="end_PlanEndTime" runat="server" BeginTimeControlID="begin_PlanEndTime"></cc1:DateTimeTextBox>
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
                DataKeyNames="PlanID" OnRowDataBound="CustomGridView1_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="计划编码"  ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft field12">
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="lblPlanCode" runat="server" ShowTextNum="50" Text='<%# Eval("PlanCode")%>'></cc1:ShortTextLabel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="计划名称" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="lblPlanName" runat="server" ShowTextNum="50" Text='<%# Eval("PlanName")%>'></cc1:ShortTextLabel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="开始时间" HeaderStyle-Width="80">
                        <ItemTemplate>
                            <%# Eval("PlanBeginTime").ToDate()%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="结束时间" HeaderStyle-Width="80">
                        <ItemTemplate>
                            <%# Eval("PlanEndTime").ToDate()%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="计划类型" HeaderStyle-Width="60">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="dlblPlanTypeID" DictionaryType="Dic_Sys_PlanType" FieldIDValue='<%# Eval("PlanTypeID") %>'
                                runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="负责人" HeaderStyle-CssClass="field8 alignleft"  ItemStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <%# Eval("DutyUser")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="计划状态" HeaderStyle-Width="60" HeaderStyle-CssClass="alignleft"  ItemStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="dlblPlanStatus" DictionaryType="Dic_TraningPlanState" FieldIDValue='<%# Eval("PlanStatus") %>'
                                runat="server" />
                            <asp:HiddenField ID="Hf_PlanStatus" runat="server" Value='<%# Eval("PlanStatus") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="操作" HeaderStyle-Width="60">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtn_File" runat="server">归档</asp:LinkButton><asp:LinkButton
                                ID="lbtn_View" runat="server" Visible="false">查看</asp:LinkButton>
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
