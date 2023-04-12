<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master" AutoEventWireup="true" CodeFile="Excellent.aspx.cs" Inherits="Activity_Excellent" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBack" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
        <!--查找条件-->
        <div class="dv_searchbox">
            <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0" runat="server"
                id="tableQueryControlList">
                <tr>
                    <th>
                        活动时间：
                    </th>
                    <td>
                        <cc1:DateTimeTextBox ID="txtBeginTime" runat="server" EndTimeControlID="end_CreateTime"
                            DataTimeFormat="%Y-%M-%D"></cc1:DateTimeTextBox>
                        至
                        <cc1:DateTimeTextBox ID="txtEndTime" runat="server" BeginTimeControlID="begin_CreateTime"
                            DataTimeFormat="%Y-%M-%D"></cc1:DateTimeTextBox>
                    </td>
                    <th width="120">
                        活动名称：
                    </th>
                    <td class="Search_Area">
                        <asp:TextBox ID="txt_AppraisalName" runat="server" CssClass="inputbox_120 floatleft"></asp:TextBox>
                        <asp:Button ID="btnSearch" CssClass="btn_Search" runat="server" Text="查询" OnClick="btnSearch_Click" />
                        <%--<a href="javascript:hideGridview()" class="dropdownico " id="Highsearch">高级搜索</a>--%>
                    </td>
                </tr>               
            </table>
        </div>
        <!--查找结果-->
        <div class="dv_searchlist">
            <!--翻页-->
            <div class="dv_pagePanel">
                <div class="dv_selectAll">
                </div>
                <div class="dv_pageControl">
                    <uc2:PageSet ID="PageSet1" runat="server" />
                </div>
            </div>
            <!--列表 begin-->
            <cc1:CustomGridView ID="gvList" runat="server" CssClass="GridviewGray" EmptyDataSourceTip="没有任何记录！"
                AutoCreateColumnInsertIndex="0" AutoGenerateColumns="False" CurrentPageIndex="1"
                CustomAllowPaging="False" IsEmpty="False" TotalRecordCount="0" DataKeyNames="AppraisalID"
                OnRowCommand="gvList_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="活动标题" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="lblCourseName" runat="server" ShowTextNum="100" Text='<%# Eval("AppraisalTitle")%>'></cc1:ShortTextLabel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="活动类型" HeaderStyle-Width="60">
                        <ItemTemplate>
                            <asp:Label ID="LabelTypeName" runat="server" Text='<%# Eval("TypeName")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="活动形式" HeaderStyle-Width="60">
                        <ItemTemplate>
                            <asp:Label ID="LabelShapeName" runat="server" Text='<%# Eval("ShapeName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="开始时间" HeaderStyle-Width="160">
                        <ItemTemplate>
                            <asp:Label ID="LabelBeginTime" runat="server" Text='<%# Eval("BeginTime").ToDateTime().ToString("yyyy-MM-dd HH:mm:ss") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="结束时间" HeaderStyle-Width="160">
                        <ItemTemplate>
                            <asp:Label ID="LabelEndTime" runat="server" Text='<%# Eval("EndTime").ToDateTime().ToString("yyyy-MM-dd HH:mm:ss") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="状态" HeaderStyle-Width="80">
                        <ItemTemplate>
                            <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status").ToInt() == 0? "已保存":"已发布" %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="操作" HeaderStyle-Width="160px">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <a href="<%# this.ActionHref(string.Format("{0}/Activity/SetExcellent.aspx?AppraisalID={1}", WebUtility.AppPath, Eval("AppraisalID"))) %>">作品展示</a>
                            <a href="<%# this.ActionHref(string.Format("{0}/Activity/SetPrize.aspx?AppraisalID={1}", WebUtility.AppPath, Eval("AppraisalID"))) %>">结果公示</a>
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

