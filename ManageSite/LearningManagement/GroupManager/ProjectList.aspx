<%@ Page Title="学习群组管理" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="ProjectList.aspx.cs" Inherits="LearningManagement_GroupManager_ProjectList" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <!--导航路径-->
        <div class="dv_path" id="dv_path">
            当前位置：学习管理>>学习群组管理
        </div>
        <!--功能标题-->
        <h2 class="dv_title">
            学习群组管理
        </h2>
        <!--查找条件-->
        <div class="dv_searchbox">
            <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0" id="tableQueryControlList"
                runat="server">
                <tr>
                    <th>
                        项目名称：
                    </th>
                    <td>
                        <asp:TextBox ID="TextBox2" runat="server" CssClass="inputbox_120"></asp:TextBox>
                        <%--<input class="btn_Search" type="button" value="查询" /><a href="javascript:hideGridview()"
                            class="dropdownico" id="Highsearch">高级搜索</a>--%>
                    </td>
                    <td style="width: 20%; text-align: right;">
                        <asp:UpdatePanel ID="upQuery" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Button ID="btnSearch" runat="server" CssClass="btn_Search" Text="查询" OnClick="btnSearch_Click" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td style="width: 60%;">
                        <a href="javascript:hideGridview()" class="dropdownico" id="A1">高级搜索</a>
                    </td>
                </tr>
                <tr>
                    <th>
                        班级名称：
                    </th>
                    <td>
                        <asp:TextBox ID="TextBox3" runat="server" CssClass="inputbox_120"></asp:TextBox>
                    </td>
                    <th>
                        群组名称：
                    </th>
                    <td>
                        <asp:TextBox ID="TextBox4" runat="server" CssClass="inputbox_120"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>
                        项目开始时间：
                    </th>
                    <td colspan="3">
                        <cc1:DateTimeTextBox ID="begin_ItemBeginTime" runat="server" EndTimeControlID="end_ItemEndTime"></cc1:DateTimeTextBox>
                        至
                        <cc1:DateTimeTextBox ID="end_ItemEndTime" runat="server" BeginTimeControlID="begin_ItemBeginTime"></cc1:DateTimeTextBox>
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
                        <div class="dv_pageControl">
                            <uc2:PageSet ID="PageSet1" runat="server" />
                        </div>
                    </div>
                    <!--列表 begin-->
                    <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="False">
                        <Columns>
                            <asp:TemplateField HeaderText="" Visible="false">
                                <ItemStyle HorizontalAlign="Center" Width="40" />
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="项目编码" ItemStyle-CssClass="alignleft">
                                <ItemTemplate>
                                    <cc1:ShortTextLabel ID="lblItemCode" runat="server" ShowTextNum="10" Text='<%#Eval("ItemCode") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:BoundField DataField="ItemCode" HeaderText="项目编码" />--%>
                            <asp:TemplateField HeaderText="项目名称" ItemStyle-CssClass="alignleft">
                                <ItemTemplate>
                                    <cc1:ShortTextLabel ID="lblItemName" runat="server" ShowTextNum="10" Text='<%#Eval("ItemName") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ClassName" HeaderText="班级名称" />
                            <asp:BoundField DataField="SingnNum" HeaderText="学员总数" />
                            <asp:BoundField DataField="AssignNum" HeaderText="已分班学员数" />
                            <asp:TemplateField HeaderText="未分班学员数">
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblUnAssign" runat="server" Text='<%#Eval("SingnNum").ToInt()-Eval("AssignNum").ToInt() %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="" HeaderText="组内人数" />
                            <asp:TemplateField HeaderText="操作">
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <a href='<%#this.ActionHref(string.Format("GroupList.aspx?ClassID={0}&TrainingItemID={1}",Eval("ClassID").ToGuid(),Eval("TrainingItemID").ToGuid())) %>'>群组管理</a></ItemTemplate>
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
