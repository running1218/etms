<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserControl.ascx.cs" Inherits="Log_SystemException_UserControl" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<cc1:CustomMuliView runat="server" ID="Views">
    <asp:View runat="server" ID="View_Manager">
        <div class="dv_searchbox">
            <table class="GridviewGray">
                <tr>
                    <th>
                        消息描述：
                    </th>
                    <td>
                        <asp:TextBox ID="txtquery" runat="server" CssClass="inputbox_210"></asp:TextBox>
                        <%--<a href="javascript:hideGridview()" class="dropdownico" id="Highsearch">高级搜索</a>--%>
                    </td>
                    <th>
                        操作人：
                    </th>
                    <td>
                        <asp:TextBox runat="server" ID="txtOpeator"></asp:TextBox><asp:Button ID="btn_Search"
                            runat="server" Text="查询" CssClass="btn_Search" OnClick="btn_Search_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="dv_searchlist">
            <!--翻页-->
            <div class="dv_pagePanel">
                <div class="dv_selectAll">
                    <cc1:CheckBoxsController runat="server" ID="chkboxControler" ContainerID="GridViewList" />
                    <cc1:CustomButton CssClass="btn_Del" Text="删除" runat="server" ID="btnDeletes" EnableConfirm="true"
                        ConfirmMessage="确信要执行“批量删除”操作吗?" OnClick="btnDeletes_Click" />
                </div>
                <div class="dv_pageControl">
                    <uc2:PageSet ID="PageSet1" runat="server" />
                </div>
            </div>
            <cc1:CustomGridView ID="GridViewList" runat="server" AutoGenerateColumns="False"
                DataKeyNames="SysExLogID">
                <Columns>
                    <asp:TemplateField HeaderStyle-Width="15">
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="序号" HeaderStyle-Width="60">
                        <ItemTemplate>
                            <asp:Label ID="LabNo" runat="server" Text='<%#((this.PageSet1.PageIndex -1) * this.PageSet1.PageSize) + Container.DataItemIndex+1 %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ApplicationName" HeaderText="应用名称" SortExpression="ApplicationName"
                        HeaderStyle-Width="100"></asp:BoundField>
                    <asp:TemplateField HeaderText="错误消息" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <div class="dv_SystemerrorTextbox">
                                <%#Eval("Message")%></div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="LoginName" HeaderText="操作人" SortExpression="LoginName"
                        HeaderStyle-CssClass="field8">
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CreateTime" HeaderText="创建时间" SortExpression="CreateTime"
                        HeaderStyle-Width="120">
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="操作" HeaderStyle-Width="40">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <a href="javascript:showWindow('系统异常日志管理','<%# this.ActionHref(String.Format("View.aspx?op=view&id={0}", new Object[]{Eval("SysExLogID")})) %>')">
                                查看</a>
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
    </asp:View>
    <asp:View runat="server" ID="View_Browse">
        <div class="dv_information">
            <table class="GridviewGray">
                <tr>
                    <th>
                        系统异常日志编号：
                    </th>
                    <td>
                        <asp:Label ID="lblSysExLogID" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th>
                        应用名称：
                    </th>
                    <td>
                        <asp:Label ID="lblApplicationName" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th>
                        错误消息：
                    </th>
                    <td>
                        <asp:TextBox runat="server" ID="lblMessage" SkinID="textarea" TextMode="MultiLine" />
                    </td>
                </tr>
                <tr>
                    <th>
                        上级错误消息：
                    </th>
                    <td>
                        <asp:TextBox runat="server" ID="lblBaseMessage" SkinID="textarea" TextMode="MultiLine" />
                    </td>
                </tr>
                <tr>
                    <th>
                        堆栈信息：
                    </th>
                    <td>
                        <asp:TextBox runat="server" ID="lblStackTrace" SkinID="textarea" TextMode="MultiLine" />
                    </td>
                </tr>
                <tr>
                    <th>
                        操作人：
                    </th>
                    <td>
                        <asp:Label ID="lblLoginName" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th>
                        创建时间：
                    </th>
                    <td>
                        <asp:Label ID="lblCreateTime" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th>
                        服务器名称：
                    </th>
                    <td>
                        <asp:Label ID="lblServerName" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th>
                        客户端IP：
                    </th>
                    <td>
                        <asp:Label ID="lblClientIP" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th>
                        请求URL：
                    </th>
                    <td>
                        <asp:TextBox runat="server" ID="lblPageUrl" SkinID="textarea" TextMode="MultiLine" />
                    </td>
                </tr>
            </table>
        </div>
    </asp:View>
</cc1:CustomMuliView>
