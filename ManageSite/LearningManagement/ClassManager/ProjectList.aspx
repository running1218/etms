<%@ Page Title="班级管理" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="ProjectList.aspx.cs" Inherits="LearningManagement_ClassManager_ProjectList" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <!--导航路径-->
        <div class="dv_path" id="dv_path">
            当前位置：学习管理>>班级管理
        </div>
        <!--功能标题-->
        <h2 class="dv_title">
            班级管理
        </h2>
        <!--查找条件-->
        <div class="dv_searchbox">
            <table width="98%" border="0" cellspacing="0" cellpadding="0" class="GridviewGray" id="tableQueryControlList" runat="server">
                <tr>
                    <th width="120">
                        项目名称：
                    </th>
                    <td width="120">
                        <asp:TextBox ID="txt_ItemName" runat="server" CssClass="inputbox_120"></asp:TextBox>
                         <asp:Button ID="btnSearch" runat="server" CssClass="btn_Search" Text="查询" OnClick="btnSearch_Click" />
                         <a href="javascript:hideGridview()" class="dropdownico" id="Highsearch">高级搜索</a>
                    </td>
            
                </tr>
                <tr>
                    <th height="34">
                        周期开始时间：
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
                    <div class="dv_pagePanel">
                        <div class="dv_pageControl">
                            <uc2:PageSet ID="PageSet1" runat="server" />
                        </div>
                    </div>
                    <!--列表 begin-->
                   <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="False" >
                        <Columns>
                            <asp:TemplateField HeaderText="" Visible="false">
                                <ItemStyle HorizontalAlign="Center"  />
                                <HeaderStyle HorizontalAlign="Center" Width="20"/>
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="项目编码" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft" >
                                <ItemTemplate>
                                    <cc1:ShortTextLabel ID="lblItemCode" runat="server" ShowTextNum="50" Text='<%#Eval("ItemCode") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:BoundField DataField="ItemCode" HeaderText="项目编码" />--%>
                            <asp:TemplateField HeaderText="项目名称" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft" >
                                <ItemTemplate>
                                    <cc1:ShortTextLabel ID="lblItemName" runat="server" ShowTextNum="50" Text='<%#Eval("ItemName") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:BoundField DataField="ItemName" HeaderText="项目名称" />--%>
                            <asp:TemplateField HeaderText="项目开始时间" HeaderStyle-Width="90">                                
                                <ItemTemplate>
                                    <asp:Label ID="lblBeginTime" runat="server" Text='<%#Eval("ItemBeginTime").ToDate() %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="项目结束时间" HeaderStyle-Width="90">                                
                                <ItemTemplate>
                                    <asp:Label ID="lblEndTime" runat="server" Text='<%#Eval("ItemEndTime").ToDate() %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="SingnNum" HeaderText="学员总数" HeaderStyle-Width="60" HeaderStyle-CssClass="alignright" ItemStyle-CssClass="alignright"/>
                            <asp:BoundField DataField="AssignNum" HeaderText="已分班学员数" HeaderStyle-Width="80" HeaderStyle-CssClass="alignright" ItemStyle-CssClass="alignright"/>
                            <asp:TemplateField HeaderText="未分班学员数" HeaderStyle-Width="80" HeaderStyle-CssClass="alignright" ItemStyle-CssClass="alignright">
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblUnAssign" runat="server" Text='<%#Eval("SingnNum").ToInt()-Eval("AssignNum").ToInt() %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ClassNum" HeaderText="班级数" HeaderStyle-Width="60" HeaderStyle-CssClass="alignright" ItemStyle-CssClass="alignright"/>
                            <asp:TemplateField HeaderText="操作" HeaderStyle-Width="60">
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <a href='<%# this.ActionHref(string.Format("ClassList.aspx?TrainingItemID={0}",Eval("TrainingItemID").ToGuid())) %>'>班级管理</a>
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
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
