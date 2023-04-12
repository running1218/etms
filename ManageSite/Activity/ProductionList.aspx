<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master" AutoEventWireup="true" CodeFile="ProductionList.aspx.cs" Inherits="Activity_ProductionList" %>
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
                        <asp:Button ID="btnSearch" CssClass="btn_Search" OnClick="btnSearch_Click" runat="server" Text="查询" />
                        <%--<a href="javascript:hideGridview()" class="dropdownico " id="Highsearch">高级搜索</a>--%>
                    </td>
                </tr>               
            </table>
        </div>
        <!--查找结果-->
        <div class="dv_searchlist">
            <!--翻页-->
            <div class="dv_pagePanel hide">
                <div class="dv_pageControl">
                    <uc2:PageSet ID="PageSet1" runat="server" />
                </div>
            </div>
            <!--列表 begin-->
            <asp:Repeater ID="rptList" runat="server">
                <ItemTemplate>
                    <div class="marking-box">
                        <div class="apprisal-box">
                            <p>
                                <span class="apprisal-title"><%# Eval("AppraisalTitle") %></span>
                                <span class="hide" style="float:right;">可展开</span>
                            </p>
                            <p>
                                活动时间：<%# Eval("BeginTime") %> ~ <%# Eval("EndTime") %>
                            </p>
                            <p class="apprisal-num">
                                <span>已提交：<span class="apprisal-num-blue"><%# Eval("SubmitNum") %></span></span>
                                <span>已评审：<span class="apprisal-num-blue"><%# Eval("MarkingNum") %></span></span>
                                <span>未评审：<span class="apprisal-num-red"><%# Eval("NoMarkingNum") %></span></span>
                            </p>
                        </div>
                        <asp:Repeater ID="rptGroup" runat="server" DataSource='<%# Eval("MarkingGroup") %>'>
                            <ItemTemplate>
                                <div class="group-marking">
                                    <table>
                                        <tr>
                                            <td><%# Eval("GroupName") %>-<%# Eval("TypeName") %></td>
                                            <td>总量：<span class="apprisal-num-blue"><%# Eval("SubmitNum") %></span></td>
                                            <td>已评审：<span class="apprisal-num-blue"><%# Eval("MarkingNum") %></span></td>
                                            <td>未评审：<span class="apprisal-num-red"><%# Eval("NoMarkingNum") %></span></td>
                                            <td><a href="<%#this.ActionHref(string.Format("~/Activity/Marking.aspx?AppraisalID={0}&GroupID={1}&ProductType={2}", Eval("AppraisalID"), Eval("GroupID"), Eval("ProductType"))) %>" target="_blank" class="group-marking-action">去评审</a></td>
                                        </tr>
                                    </table>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            <!--列表 end-->
            <div class="dv_splitLine">
            </div>
            <!--翻页-->
            <div class="dv_pagePanel">
            </div>
        </div>
    </div>
</asp:Content>


