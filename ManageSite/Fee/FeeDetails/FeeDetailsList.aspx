<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="FeeDetailsList.aspx.cs" Inherits="FeeDetailsList" %>

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
                        项目名称：
                    </th>
                    <td width="125">
                        <asp:TextBox ID="txt_b999ItemName" runat="server" CssClass="inputbox_120" MaxLength="50"></asp:TextBox>
                    </td>
                    <th width="120">
                        费用流水名称：
                    </th>
                    <td>
                        <asp:TextBox ID="txt_FeeCostDetailName" runat="server" CssClass="inputbox_120" MaxLength="50"></asp:TextBox><asp:Button ID="btnSearch" CssClass="btn_Search" runat="server" Text="查询" OnClick="btnSearch_Click" />
                        <a href="javascript:hideGridview()" class="dropdownico" id="Highsearch">高级搜索</a>
                    </td>
                </tr>
                <tr>
                    <th>
                        经 手 人：
                    </th>
                    <td>
                        <asp:TextBox ID="txt_Handler" runat="server" CssClass="inputbox_120"></asp:TextBox>
                    </td>
                    <th>
                        记 录 人：
                    </th>
                    <td>
                        <asp:TextBox ID="txt_a999CreateUser" runat="server" CssClass="inputbox_120"></asp:TextBox>
                    </td>
                    
                </tr>
                <tr>
                 <th>
                        用&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;途：
                    </th>
                    <td>
                        <asp:TextBox ID="txt_Purpose" runat="server" CssClass="inputbox_120"></asp:TextBox>
                    </td>
                <th>
                        费用发生日期：
                    </th>
                    <td>
                        <cc1:DateTimeTextBox ID="begin_CostDate" runat="server" EndTimeControlID="end_CostDate"></cc1:DateTimeTextBox><asp:RequiredFieldValidator
                    ID="RequiredFieldValidator3" Text="*" Display="None" runat="server" ErrorMessage="请填写开始时间！"
                    ControlToValidate="begin_CostDate" ValidationGroup="Saves"></asp:RequiredFieldValidator>
                    至
                    <cc1:DateTimeTextBox ID="end_CostDate" runat="server" BeginTimeControlID="begin_CostDate"></cc1:DateTimeTextBox><asp:RequiredFieldValidator
                    ID="RequiredFieldValidator1" Text="*" Display="None" runat="server" ErrorMessage="请填写结束时间！"
                    ControlToValidate="end_CostDate" ValidationGroup="Saves"></asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>
        </div>
        <!--查找结果-->
        <div class="dv_searchlist">
                 <!--翻页-->
                    <div class="dv_pagePanel">
                        <div class="dv_selectAll">                           
                            <input type="button" class="btn_Add" value="新增" onclick="javascript:showWindow('新增费用流水','FeeDetailsAdd.aspx')" />
                            <asp:Button ID="btnExport" runat="server" CssClass="btn_Export" Text="导出" OnClick="btnExport_Click" />
                        </div>
                        <div class="dv_pageControl">
                            <uc2:PageSet ID="PageSet1" runat="server" />
                        </div>
                    </div>
                    <!--列表 begin-->
                    <cc1:CustomGridView ID="CustomGridView1" runat="server" CssClass="GridviewGray" EmptyDataSourceTip="没有任何记录！"
                        AutoCreateColumnInsertIndex="0" AutoGenerateColumns="False" CurrentPageIndex="1"
                        CustomAllowPaging="False" IsEmpty="False" TotalRecordCount="0" DataKeyNames="FeeCostDetailID"
                        OnRowCommand="CustomGridView1_RowCommand">
                        <Columns>
                           
                            <asp:TemplateField HeaderText="流水名称"  HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                                <ItemTemplate>
                                    <cc1:ShortTextLabel ID="lblFeeCostDetailName" runat="server" ShowTextNum="30" Text='<%# Eval("FeeCostDetailName")%>'></cc1:ShortTextLabel>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="用途"  HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                                <ItemTemplate>
                                    <cc1:ShortTextLabel ID="lblPurpose" runat="server" ShowTextNum="50" Text='<%# Eval("Purpose")%>'></cc1:ShortTextLabel>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Amount" HeaderText="金额" HeaderStyle-CssClass="alignright" ItemStyle-CssClass="alignright" HeaderStyle-Width="60"/>
                            <asp:TemplateField HeaderText="发生日期" HeaderStyle-Width="80">
                                <ItemTemplate>
                                    <%# Eval("CostDate").ToDate()%>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField DataField="Handler" HeaderText="经手人" HeaderStyle-CssClass="field8"/>
                            <asp:BoundField DataField="CreateUser" HeaderText="记录人" HeaderStyle-CssClass="field8" />
                            <asp:TemplateField HeaderText="项目名称"  HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                                <ItemTemplate>
                                 <cc1:ShortTextLabel ID="lblTrainingItem" runat="server" ShowTextNum="50" Text='<%# getItemName(Eval("TrainingItemID").ToGuid())%>'></cc1:ShortTextLabel>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="操作" HeaderStyle-Width="100">
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                
                                    <a href="javascript:showWindow('编辑费用流水','<%# this.ActionHref(string.Format("~/Fee/FeeDetails/FeeDetailsEdit.aspx?FeeCostDetailID={0}", Eval("FeeCostDetailID")))%>')">编辑</a><a  href="javascript:showWindow('查看费用流水','<%# this.ActionHref(string.Format("~/Fee/FeeDetails/FeeDetailsView.aspx?FeeCostDetailID={0}", Eval("FeeCostDetailID")))%>')">查看</a><cc1:CustomLinkButton runat="server" ID="lbtn_Del" CommandArgument='<%# Eval("FeeCostDetailID") %>'
                                            CommandName="Del" Text="删除" EnableConfirm="true" ConfirmTitle="提示" ConfirmMessage="确定要删除吗？" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </cc1:CustomGridView>
                    <!--列表 end-->
                    <div class="dv_splitLine">
                    </div>
                    <!--翻页-->
                    <div class="dv_pagePanel">
                    </div>
       
        </div>
    </div>
</asp:Content>
