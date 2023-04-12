<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AfficheList.ascx.cs" Inherits="Information_AfficheManager_Controls_AfficheList" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<div>
        <!--导航路径-->
        <div class="dv_path">
            当前位置：信息公告系统>>公告管理>><asp:Literal ID="Literal12" runat="server"></asp:Literal>
        </div>
        <!--功能标题-->
        <h2 class="dv_title">
            公告管理
        </h2>
        <!--查找条件-->
        <div class="dv_searchbox">
            <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <th>
                        公告标题：
                    </th>
                    <td><asp:TextBox ID="TextBox1" runat="server" CssClass="inputbox_120"></asp:TextBox></td>
                    <th>
                        创建日期：</th>
                        <td colspan="3">
                        <cc1:DateTimeTextBox ID="TextBox5" runat="server" CssClass="inputbox_120"></cc1:DateTimeTextBox>
                        至
                        <cc1:DateTimeTextBox ID="TextBox6" runat="server" CssClass="inputbox_120"></cc1:DateTimeTextBox>
                        <input class="btn_Search" type="button" value="查询"/>
                    </td>
                </tr>
            </table>
        </div>
        <!--查找结果-->
        <div class="dv_searchlist">
            <!--翻页-->
            <div class="dv_pagePanel" id="divPage1">
                <div class="dv_selectAll">
                    <cc1:CheckBoxsController runat="server" ID="chkboxControler" ContainerID="CustomGridView1" />
                    <% if (InfoType == 1)
                           { %>
                    <input type="button" class="btn_Add" value="新增" onclick="javascript:showWindow('新增公告','AfficheAddCompany.aspx',800,480)" />
                            <% }
                           else { %>
<input type="button" class="btn_Add" value="新增" onclick="javascript:showWindow('新增公告','AfficheAddProject.aspx',800,480)" />                           <%} %>
                    
                    <input type="button" class="btn_Del" value="删除" onclick="" />
                </div>
                <div class="dv_pageControl">
                    <uc2:PageSet ID="PageSet1" runat="server" />
                </div>
            </div>
            <!--列表 begin-->
            <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="True"
                CustomAllowPaging="false" ShowFooter="false" AutoCreateColumnInsertIndex="1">
                <Columns>
                    <asp:TemplateField HeaderText="">
                        <ItemStyle HorizontalAlign="Center" Width="40" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="序号" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                        <ItemStyle HorizontalAlign="Center" Width="60" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="LabNo" runat="server" Text='<%#  ((this.PageSet1.PageIndex -1) * this.PageSet1.PageSize) + Container.DataItemIndex+1 %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="操作">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                        <% if (InfoType == 1)
                           { %>
                            <a href="javascript:showWindow('编辑公告','AfficheEditCompany.aspx',800,480)">编辑</a>
                            <% }
                           else { %>
                           <a href="javascript:showWindow('编辑公告','AfficheEditProject.aspx',800,480)">编辑</a>
                           <%} %>
                            <a href="AfficheView.aspx" target="_blank">查看</a> 
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