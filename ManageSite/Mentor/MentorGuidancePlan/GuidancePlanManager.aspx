﻿<%@ Page Title="导师辅导计划" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="GuidancePlanManager.aspx.cs" Inherits="Mentor_MentorGuidancePlan_GuidancePlanManager" %>
    
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <!--导航路径-->
        <div class="dv_path">
            当前位置：导师辅导管理>>导师辅导>>导师辅导计划
        </div>
        <!--功能标题-->
        <h2 class="dv_title">
            导师辅导计划
        </h2>
        <div class="dv_pageInformation">
            <table class="GridviewGray">
                <tr>
                    <th>
                        学员<asp:Literal ID="ltlDepartment" runat="server" Text="<%$ Resources:UIResource, ui_department%>"></asp:Literal>
                    </th>
                    <td>
                        软件开发部
                    </td>
                    <th>
                        学员岗位：
                    </th>
                    <td>
                        软件工程师
                    </td>
                    <th>
                        学员<asp:Literal ID="ltlRank" runat="server" Text="<%$ Resources:UIResource, ui_rank%>"></asp:Literal>：
                    </th>
                    <td>
                        高级
                    </td>
                </tr>
                <tr>
                    <th>
                        学员姓名：
                    </th>
                    <td>
                        刘勇
                    </td>
                    <th>
                        辅导主题：
                    </th>
                    <td colspan="3">
                        新员工辅导
                    </td>
                </tr>
            </table>
        </div>

        <!--查找结果-->
        <div class="dv_searchlist">
            <!--翻页-->
            <div class="dv_pagePanel" id="divPage1">
                <div class="dv_selectAll">
                    <cc1:CheckBoxsController runat="server" ID="chkboxControler" ContainerID="CustomGridView1" />
                    <input type="button" class="btn_Add" value="新增" onclick="javascript:showWindow('新增导师辅导计划','GuidancePlanAdd.aspx')" />
                    <input type="button" class="btn_Del" value="删除" title="说明：学生未反馈的辅导计划可以删除。" />
                    <input type="button" class="btn_2" value="返回" onclick="javascript:history.back()" />
                </div>
                <div class="dv_pageControl">
                    <uc2:PageSet ID="PageSet1" runat="server" />
                </div>
            </div>
            <!--列表 begin-->
            <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="True"
                CustomAllowPaging="false" ShowFooter="false" 
                AutoCreateColumnInsertIndex="1" 
                onrowdatabound="CustomGridView1_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="">
                        <ItemStyle HorizontalAlign="Center" Width="40" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="序号">
                        <ItemStyle HorizontalAlign="Center" Width="26" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="LabNo" runat="server" Text='<%#  ((this.PageSet1.PageIndex -1) * this.PageSet1.PageSize) + Container.DataItemIndex+1 %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="操作">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <a href="javascript:showWindow('编辑导师辅导计划','GuidancePlanEdit.aspx')">编辑</a>
                            <a href="javascript:showWin()">反馈信息</a>
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
            <script language="javascript" type="text/javascript">
                function showWin() {
                    showWindow('反馈信息', 'GuidancePlanView.aspx')                }
            </script>
        </div>
</asp:Content>
