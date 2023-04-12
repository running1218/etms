<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="CourseWareList.aspx.cs" Inherits="TraningImplement_ProjectCourseResource_CourseWareList" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBack" runat="Server">
    <a class="btn_Return" runat="server" id="aBack">返回</a>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <!--查找条件-->
        <div class="dv_searchbox">
            <table class="GridviewGray fixedTable" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <th width="120">
                        项目名称：
                    </th>
                    <td width="120">
                        <asp:Label ID="lblItemName" runat="server"></asp:Label>
                    </td>
                    <th width="100">
                        课程名称：
                    </th>
                    <td>
                        <asp:Label ID="lblCourseName" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            <div style="display: none">
                <asp:UpdatePanel ID="upQuery" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Button ID="btnSearch" CssClass="btn_Search" runat="server" Text="查询" OnClick="btnSearch_Click" /></ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <!--查找结果-->
        <div class="dv_searchlist">
            <asp:UpdatePanel ID="upList" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <!--翻页-->
                    <div class="dv_pagePanel" id="divPage1">
                        <div class="dv_selectAll">
                            <cc1:CheckBoxsController runat="server" ID="chkboxControler" ContainerID="CustomGridView1" />
                            <asp:Button ID="btnAdd" runat="server" CssClass="btn_Add" Text="新增" />
                            <input id="btnSort" type="button" class="btn_Sort" value="排序" onclick="javascript:showWindow('课件排序','<%= SortUrl %>',500,450)" />
                            <cc1:CustomButton runat="server" ID="cbtnDel" Text="删除" CssClass="btn_Del" EnableConfirm="true"
                                ConfirmTitle="提示" ConfirmMessage="确定删除吗？" OnClick="cbtnDel_Click" />
                        </div>
                        <div class="dv_pageControl">
                            <uc2:PageSet ID="PageSet1" runat="server" />
                        </div>
                    </div>
                    <!--列表 begin-->
                    <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="false"
                        CustomAllowPaging="false" ShowFooter="false" AutoCreateColumnInsertIndex="0"
                        DataKeyNames="ItemCourseResID" OnRowDataBound="CustomGridView1_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="">
                                <ItemStyle HorizontalAlign="Center" Width="28" />
                                <HeaderStyle HorizontalAlign="Center" Width="20" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="课件名称" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                                <ItemTemplate>
                                    <cc1:ShortTextLabel ID="lblCoursewareName" runat="server" ShowTextNum="50" Text='<%# Eval("CoursewareName")%>'></cc1:ShortTextLabel>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="CoursewareTypeName" HeaderText="课件类型"  HeaderStyle-Width="80" />
                            <asp:TemplateField HeaderText="状态" HeaderStyle-Width="60">
                                <ItemTemplate>
                                    <%# Eval("IsUse").ToBoolean() ? "启用" : "停用"%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="开始时间" HeaderStyle-Width="80">
                                <ItemTemplate>
                                    <%# Eval("ResBeginTime").ToDate()%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="结束时间" HeaderStyle-Width="80">
                                <ItemTemplate>
                                    <%# Eval("ResEndTime").ToDate()%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="操作" HeaderStyle-Width="80">
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnEidt" runat="server" Text="编辑" CommandArgument='<%# Eval("ItemCourseResID") %>'></asp:LinkButton><a
                                        href='<%# GetViewUrl(Eval("CoursewareID"))  %>' class="btn_Study" target="_blank">浏览</a>
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
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
