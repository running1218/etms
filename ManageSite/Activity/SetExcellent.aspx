<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="SetExcellent.aspx.cs" Inherits="Activity_SetExcellent" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBack" runat="Server">
    <asp:LinkButton ID="lbtnReturn" runat="server" Text="返回" PostBackUrl="Excellent.aspx" CssClass="btn_Return"></asp:LinkButton>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <!--查找条件-->
        <div class="dv_searchbox">
            <table class="GridviewGray th80" border="0" cellpadding="0" cellspacing="0" runat="server"
                id="tableQueryControlList">
                <tr>
                    <td style="width:60px;">作品名称：</td>
                    <td class="Search_Area">
                        <asp:TextBox ID="txt_ProductionName" runat="server" CssClass="inputbox_190 floatleft"></asp:TextBox>
                        <asp:Button ID="btnSearch" CssClass="btn_Search" runat="server" Text="查询" OnClick="btnSearch_Click" />
                        <%--<a href="javascript:hideGridview()" class="dropdownico" id="Highsearch">高级搜索</a>--%>
                    </td>
                </tr>               
            </table>
        </div>
        <!--查找结果-->
        <div class="dv_searchlist">
            <!--翻页-->
            <div class="dv_pagePanel" id="divPage1">
                <div class="dv_selectAll hide">
                    <cc1:CheckBoxsController runat="server" ID="chkboxControler" ContainerID="CustomGridView1" />
                    <asp:Button ID="btnAdd" runat="server" Text="推优" CssClass="btn_Ok" OnClick="btnAdd_Click" />
                </div>
                <div class="dv_pageControl">
                    <uc2:PageSet ID="PageSet1" runat="server" />
                </div>
            </div>
            <!--列表 begin-->
            <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="false" IsRemeberChecks="true"
                CustomAllowPaging="false" ShowFooter="false" AutoCreateColumnInsertIndex="0" DataKeyNames="ProductID" OnRowCommand="CustomGridView1_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="hide" ItemStyle-CssClass="hide">
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" Width="20" />
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="SiginupNo" HeaderText="报名编号" HeaderStyle-CssClass="alignleft field12"
                        ItemStyle-CssClass="alignleft" />
                    <asp:BoundField DataField="Name" HeaderText="姓名" HeaderStyle-CssClass="alignleft field8"
                        ItemStyle-CssClass="alignleft" />
                    <asp:TemplateField HeaderText="活动区域" HeaderStyle-CssClass="alignleft field8" ItemStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <asp:Label ID="lblRegion" runat="server" Text='<%# Eval("RegionName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="活动组别" HeaderStyle-CssClass="alignleft hide" ItemStyle-CssClass="alignleft hide">
                        <ItemTemplate>
                            <asp:Label ID="lblGroup" runat="server" Text='<%# Eval("GroupName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ProductName" HeaderText="作品名称" ItemStyle-CssClass="alignleft" />
                    <asp:TemplateField HeaderText="批阅状态" HeaderStyle-CssClass="field8" ItemStyle-CssClass="field8">
                        <ItemTemplate>
                            <asp:Label ID="lblAppraiseStatus" runat="server" Text='<%# Eval("AppraiseStatus").ToInt() == 1 ?"已批阅":"未批阅" %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="" HeaderText="分数" HeaderStyle-CssClass="field8" ItemStyle-CssClass="alignleft field8" />
                    <asp:TemplateField HeaderText="推优状态" HeaderStyle-CssClass="field8" ItemStyle-CssClass="field8">
                        <ItemTemplate>
                            <asp:Label ID="lblIsExcellent" runat="server" Text='<%# Eval("IsExcellent").ToInt() == 1 ?"已推":"" %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-CssClass="field10" ItemStyle-CssClass="field10" HeaderText="操作">
                        <ItemTemplate>
                            <cc1:CustomLinkButton runat="server" ID="lbtnExcellent" CommandArgument='<%# Eval("ProductID") %>'
                                CommandName="Excellent" Text='<%# Eval("IsExcellent").ToInt() == 0 ? "推优":"取消" %>' EnableConfirm="true" ConfirmTitle="提示" ConfirmMessage='<%# Eval("IsExcellent").ToInt() == 0? "确定要推优吗?":"确定要取消推优吗?" %>' />
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
