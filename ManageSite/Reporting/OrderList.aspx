<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MReport.master" AutoEventWireup="true"
    CodeFile="OrderList.aspx.cs" Inherits="Reporting_OrderList" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="content2" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
    <title>订单查询</title>
</asp:Content>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript" language="javascript" src='<%=ETMS.Utility.WebUtility.AppPath%>/JScript/jquery.easyui.min.js'></script>
    <script>
        function CheckSelectData(id) {
            var result = false;

            if ($("#" + id).find("option").length < 1) {
                return result;
            }
            var value = $("#" + id).combobox('getText')

            $("#" + id).find("option").each(function () {
                var optiontext = $(this).text();
                if (value == optiontext) {
                    result = true;
                }
            });

            if (!result)
                popAlertMsg("您选择的组织机构不存在，请重新选择", "信息提示");
            return result;
        }
    </script>
    <div class="dv_HeaderTitle">
        <h2 class="h_titleName2">
            订单查询</h2>
    </div>
    <div class="dv_reports">
        <div class="dv_searchbox">
            <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0" runat="server"
                id="tableQueryControlList">
                <tr>
                    <th style="width: 120px">
                        订单编码：
                    </th>
                    <td style="width: 240px">
                        <asp:TextBox ID="txtOrderNo" runat="server" CssClass="inputbox_120 margin0"></asp:TextBox>
                    </td>
                    <th style="width: 100px">
                        订单状态：
                    </th>
                    <td class="Search_Area">
                        <asp:DropDownList ID="drpOrderStatus" runat="server" CssClass="select_120 floatleft">
                            <asp:ListItem Value="2">全部</asp:ListItem>
                            <asp:ListItem Value="0">未付款</asp:ListItem>
                            <asp:ListItem Value="1">付款成功</asp:ListItem>
                            <asp:ListItem Value="-1">付款失败</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Button ID="btnSearch" CssClass="btn_Search" runat="server" Text="查询" OnClick="btnSearch_Click" />
                        <asp:Button ID="btnExport" runat="server" Text="导出" CssClass="btn_Export" OnClick="btnExport_Click" />
                    </td>
                </tr>
                <tr>
                    <th>
                        组织机构：
                    </th>
                    <td colspan="3">
                        <asp:DropDownList ID="drpOrg" runat="server" CssClass="easyui-combobox">
                        </asp:DropDownList>
                        是否包含下级：<asp:CheckBox ID="cbkOrg" runat="server" Checked="true" />
                    </td>
                </tr>
                <tr>
                    <th>
                        用户名：
                    </th>
                    <td class="Search_Area">
                        <asp:TextBox ID="txtLoginName" runat="server" CssClass="inputbox_120 floatleft margin0"></asp:TextBox>
                    </td>
                    <th>
                        姓名：
                    </th>
                    <td>
                        <asp:TextBox ID="txtRealName" runat="server" CssClass="inputbox_120"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>
                        成交时间：
                    </th>
                    <td colspan="3">
                        <cc1:DateTimeTextBox ID="txtBeginTime" runat="server" EndTimeControlID="txtEndTime"
                            DataTimeFormat="%Y-%M-%D" CssClass="date_format"></cc1:DateTimeTextBox>
                        至
                        <cc1:DateTimeTextBox ID="txtEndTime" runat="server" BeginTimeControlID="txtBeginTime"
                            DataTimeFormat="%Y-%M-%D" CssClass="date_format"></cc1:DateTimeTextBox>
                    </td>
                </tr>
            </table>
        </div>
        <!--查找结果-->
        <div class="dv_searchlist" style="width: 98%;">
            <!--翻页-->
            <div class="dv_pagePanel" id="divPage1">
                <div class="dv_selectAll">
                </div>
                <div class="dv_pageControl">
                    <uc2:PageSet ID="PageSet1" runat="server" />
                </div>
            </div>
            <!--列表 begin-->
            <div style="overflow-x: auto; overflow-y: hidden">
                <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="false"
                    CustomAllowPaging="false" ShowFooter="false" AutoCreateColumnInsertIndex="0"
                    Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="订单编号" HeaderStyle-Width="100" ItemStyle-CssClass="alignleft"
                            HeaderStyle-CssClass="alignleft">
                            <ItemTemplate>
                                <cc1:ShortTextLabel ID="lblOrderNo" runat="server" ShowTextNum="20" Text='<%# Eval("OrderNo")%>'></cc1:ShortTextLabel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="成交时间" HeaderStyle-Width="80">
                            <ItemTemplate>
                                <%# Eval("CreateTime")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="姓名" HeaderStyle-Width="70">
                            <ItemTemplate>
                                <cc1:ShortTextLabel ID="lblRealName" runat="server" ShowTextNum="10" Text='<%# Eval("RealName")%>'></cc1:ShortTextLabel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="用户名" HeaderStyle-Width="75">
                            <ItemTemplate>
                                <cc1:ShortTextLabel ID="lblLoginName" runat="server" ShowTextNum="10" Text='<%# Eval("LoginName")%>'></cc1:ShortTextLabel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="组织机构" HeaderStyle-Width="75">
                            <ItemTemplate>
                                <cc1:ShortTextLabel ID="lblOrganizationName" runat="server" ShowTextNum="15" Text='<%# Eval("OrganizationName")%>'></cc1:ShortTextLabel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="价格" HeaderStyle-Width="85">
                            <ItemTemplate>
                                <%# Eval("TotalPrice")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="订单状态" HeaderStyle-Width="90">
                            <ItemTemplate>
                                <cc1:DictionaryLabel ID="DictionaryLabel2" DictionaryType="Pay_PaymentStatus" FieldIDValue='<%# Eval("PaymentStatus") %>'
                                    runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </cc1:CustomGridView>
            </div>
            <!--列表 end-->
            <div class="dv_splitLine">
            </div>
            <!--翻页-->
            <div class="dv_pagePanel" id="divPage2">
            </div>
        </div>
    </div>
</asp:Content>
