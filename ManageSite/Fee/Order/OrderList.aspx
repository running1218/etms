<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="OrderList.aspx.cs" Inherits="Fee_Order_OrderList" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <!--导航路径-->
        <div class="dv_path" id="dv_path">
            当前位置：测评系统&gt;&gt;考试与成绩&gt;&gt;<asp:Literal ID="Literal7" runat="server" Text="成绩管理"></asp:Literal>
        </div>
        <!--功能标题-->
        <h2 class="dv_title">
            成绩管理
        </h2>
        <!--查找条件-->
        <div class="dv_searchbox">
            <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0" id="tableQueryControlList"
                runat="server">
                <tr>
                    <th style="width: 100px;">
                        订单状态：
                    </th>
                    <td style="width:200px;">
                        <cc1:DictionaryDropDownList runat="server" ID="ddlOrderStatus" DictionaryType="Pay_PaymentStatus" IsShowAll="true" CssClass="select_160" />
                    </td>
                    <th>
                        订单周期：
                    </th>
                    <td style="width:300px;" >
                        <cc1:DateTimeTextBox ID="txtStartTime" runat="server" EndTimeControlID="txtEndTime"></cc1:DateTimeTextBox>
                        至<cc1:DateTimeTextBox ID="txtEndTime" runat="server" BeginTimeControlID="txtStartTime"></cc1:DateTimeTextBox>
                    </td>                    
                    <td>
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn_Search" Text="查询" OnClick="btnSearch_Click" />
                        <a href="javascript:hideGridview()" class="dropdownico" id="Highsearch">高级搜索</a>                     
                    </td>
                </tr>
                <tr>
                    <th style="width: 100px;">
                        用户名：
                    </th>
                    <td colspan="4">
                        <asp:TextBox ID="txtUserName" runat="server" CssClass="inputbox_190"></asp:TextBox>
                    </td>
                                       
                </tr>                                
            </table>
        </div>
        <!--查找结果-->
        <div class="dv_searchlist">
            <!--翻页-->
            <div class="dv_pagePanel">
                <div class="dv_selectAll" id="dv_select" runat="server">
                    <asp:Button runat="server" ID="btnExport" Text="导出" OnClick="btnExport_Click" CssClass="btn_Export" />
                </div>
                <div class="dv_pageControl">
                    <uc2:PageSet ID="PageSet1" runat="server" />
                </div>
            </div>
            <!--列表 begin-->
            <cc1:CustomGridView ID="CustomGridView1" runat="server" CssClass="GridviewGray" EmptyDataSourceTip="没有任何记录！"
                AutoCreateColumnInsertIndex="0" AutoGenerateColumns="False" CurrentPageIndex="1" CustomAllowPaging="False" IsEmpty="False"
                IsRemeberChecks="true" DataKeyNames="OrderNo">
                <Columns>
                    <asp:TemplateField HeaderText="订单号" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                        <ItemStyle HorizontalAlign="Center" CssClass="alignleft" />
                        <HeaderStyle HorizontalAlign="Center" Width="120px" />
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="lblItemCode" runat="server" ShowTextNum="50" Text='<%# Eval("OrderNo")%>'></cc1:ShortTextLabel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="用户名" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                        <ItemStyle HorizontalAlign="Center" CssClass="alignleft" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="lblItemName" runat="server" ShowTextNum="50" Text='<%# Eval("LoginName")%>'></cc1:ShortTextLabel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="姓名" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                        <ItemStyle HorizontalAlign="Center" CssClass="alignleft" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="lblName" runat="server" ShowTextNum="50" Text='<%# Eval("PayerName")%>'></cc1:ShortTextLabel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="手机" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                        <ItemStyle HorizontalAlign="Center" CssClass="alignleft" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="lblMobilePhone" runat="server" ShowTextNum="50" Text='<%# Eval("MobilePhone")%>'></cc1:ShortTextLabel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="支付方式" HeaderStyle-CssClass="aligncenter">
                        <ItemStyle HorizontalAlign="Center" CssClass="aligncenter" />
                        <HeaderStyle HorizontalAlign="Center" Width="60px" />
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="lblCourseCode" runat="server" ShowTextNum="50" Text='<%# Eval("PayFrom")%>'></cc1:ShortTextLabel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="订单状态" HeaderStyle-CssClass="aligncenter">
                        <ItemStyle HorizontalAlign="Center" CssClass="aligncenter" />
                        <HeaderStyle HorizontalAlign="Center"  Width="60px"/>
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="lblCourseName" runat="server" ShowTextNum="50" Text='<%# Eval("OrderStatus")%>'></cc1:ShortTextLabel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="总价格" HeaderStyle-Width="60">
                        <ItemStyle CssClass="alignright" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="lblCourseName" runat="server" ShowTextNum="50" Text='<%# Eval("TotalPrice")%>'></cc1:ShortTextLabel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="下单时间" HeaderStyle-CssClass="aligncenter" ItemStyle-CssClass="aligncenter"
                        HeaderStyle-Width="120">
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblCreateTime" runat="server" Text='<%# Eval("CreateTime") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="支付时间" HeaderStyle-CssClass="aligncenter" ItemStyle-CssClass="aligncenter"
                        HeaderStyle-Width="120">
                        <ItemTemplate>
                            <asp:Label ID="lblStudentNum" runat="server" Text='<%# Eval("PayTime") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="课程名称" HeaderStyle-Width="150" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <asp:Label ID="lblIsInputGrade" runat="server" Text='<%# Eval("OrderDescription") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>    
                    <asp:TemplateField HeaderText="单价" HeaderStyle-Width="60">
                        <ItemStyle CssClass="alignright" />
                        <ItemTemplate>
                            <asp:Label ID="lblTotalPrice" runat="server" Text='<%# Eval("TotalPrice") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="数量" HeaderStyle-Width="40" >
                        <ItemTemplate>
                            <asp:Label ID="lblBuyNum" runat="server" Text='<%# Eval("BuyNum") %>' />
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
