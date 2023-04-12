<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master" AutoEventWireup="true" CodeFile="FeeAuditList.aspx.cs" Inherits="FeeAuditList" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <!--导航路径-->
        <div class="dv_path">
            当前位置：费用管理>>课酬管理>>课时费用确认单审核
        </div>
        <!--功能标题-->
        <h2 class="dv_title">
            课时费用确认单审核
        </h2>
        <!--查找条件-->
        <div class="dv_searchbox">
            <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <th>单据日期：</th>
                    <td>
                        <cc1:DateTimeTextBox ID="txtStartTime" runat="server"></cc1:DateTimeTextBox> 至 <cc1:DateTimeTextBox ID="DateTimeTextBox1" runat="server"></cc1:DateTimeTextBox>
                    </td>
                    <th>
                        单据名称：
                    </th>
                    <td>
                        <asp:TextBox ID="TextBox1" runat="server" CssClass="inputbox_120"></asp:TextBox>
                         <input class="btn_Search" type="button" value="查询"/><a href="javascript:hideGridview()" class="dropdownico"
                            id="Highsearch">高级搜索</a>
                    </td>
                </tr>
                <tr>
                    <th>
                        单据编号：</th>
                    <td>
                        <input type="text" class="inputbox_120" />
                    </td>                        
                    <th>
                        单据状态：</th>
                    <td>                        
                        <asp:DropdownList runat="server" ID="DictionaryDropDownList1">
                            <asp:ListItem Value="-1" Text="全部">全部</asp:ListItem>
                            <asp:ListItem Value="0" Text="未审核">待审核</asp:ListItem>
                            <asp:ListItem Value="1" Text="已审核">已审核</asp:ListItem>
                        </asp:DropdownList>
                    </td>                    
                </tr>
                 <tr>
                    <th>
                        审核日期：</th>
                    <td colspan="3">
                       <cc1:DateTimeTextBox ID="DateTimeTextBox2" runat="server"></cc1:DateTimeTextBox> 至 <cc1:DateTimeTextBox ID="DateTimeTextBox3" runat="server"></cc1:DateTimeTextBox>
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
                            <cc1:CheckBoxsController runat="server" ID="chkboxControler" ContainerID="CustomGridView1" />
                            <input type="button" class="btn_Verify" value="审核" onclick="showDiv('#dv_lay1','审核通过',function(){},340,200)" />
                            <input type="button" class="btn_Del" value="取消" />
                        </div>
                        <div class="dv_pageControl">
                            <uc2:PageSet ID="PageSet1" runat="server" />
                        </div>
                    </div>
                    <!--列表 begin-->
                    <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="True"
                        CustomAllowPaging="false" ShowFooter="false" 
                        AutoCreateColumnInsertIndex="0" onrowdatabound="CustomGridView1_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="">
                                <ItemStyle HorizontalAlign="Center" Width="40" />
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>                            
                            <asp:TemplateField HeaderText="操作">
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbnAuthid" runat="server" Text="审核" PostBackUrl="~/Fee/FeeAudit/FeeAudit.aspx"></asp:LinkButton>
                                    <a href="FeeAuditView.aspx">查看</a>
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

                    <!--审核意见的弹出层-->
                    <div id="dv_lay1" style="display:none">
                       <table style="width:90%;">
                          <tr>
                             <th>意见</th>
                             <td><textarea  class="inputbox_area210" id='txt1'></textarea></td>
                          </tr>
                       </table>     

                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>

