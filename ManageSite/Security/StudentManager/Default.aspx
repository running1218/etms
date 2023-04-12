<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Admin_Site_Student_Default"
    MasterPageFile="~/MasterPages/MPageAdmin.master" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content runat="server" ID="PageContent" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="dv_searchbox">
        <table class="GridviewGray th80">
            <tr>
                <th width="120">
                    学员姓名：
                </th>
                <td width="120">
                    <asp:TextBox ID="txtRealName" runat="server"></asp:TextBox>
                </td>
                <th width="120">
                    学员账户：
                </th>
                <td>
                    <asp:TextBox ID="txtLoginName" runat="server"></asp:TextBox><asp:Button ID="btn_Search"

                        runat="server" Text="查询" OnClick="btn_Search_Click" SkinID="Search" />
                    <a href="javascript:hideGridview()" class="dropdownico" id="Highsearch">高级搜索</a>
                </td>
            </tr>
            <tr>
                <th>
                   <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:UIResource, ui_workno%>"></asp:Literal>：
                </th>
                <td >
                    <asp:TextBox ID="txtWorkNo" runat="server" CssClass="inputbox_120"></asp:TextBox>
                </td>
                <th>用户状态：</th>
                <td>
                    <cc1:DictionaryDropDownList runat="server" ID="ddl_UserStatus" DictionaryType="Dic_Status" CssText="select_120" IsShowAll="true" />
                </td>
            </tr>
            <tr>
                <th>
                    <asp:Literal ID="ltlRank" runat="server" Text="学员身份"></asp:Literal>：
                </th>
                <td colspan="3">
                    <cc1:DictionaryDropDownList runat="server" ID="ddlResettlementWay" DictionaryType="Dic_Sys_ResettlementWay"
                        CssClass="select_190" IsShowAll="true" />
                </td>
            </tr>
        </table>
    </div>
    <div class="dv_searchlist">
        <!--翻页-->
        <div class="dv_pagePanel">
            <div class="dv_selectAll">
                <%--<cc1:CheckBoxsController runat="server" ID="chkboxControler" ContainerID="GridViewList" />--%>
                <input type="button" class="btn_Add" value="新增" onclick="showWindow('新增学员信息','<%=this.ActionHref("View.aspx?op=add&id=0")%>', 650, 500)" />
                <input type="button" class="btn_Import" value="导入" onclick="javascript:showWindow('导入学员','<%=this.ActionHref("StudentImport.aspx")%>',500,360)" />
       <!--学员管理导出功能-->
                <asp:Button ID="btnExport" runat="server" CssClass="btn_Export" Text="导出"  OnClick="btnExport_Click"/>
                <%--<cc1:CustomButton CssClass="btn_Del" Text="删除" runat="server" ID="CustomButton1"
                    EnableConfirm="true" ConfirmMessage="确信要执行“批量删除”操作吗?" OnClick="btnDelete_Click" />--%>
            </div>
            <div class="dv_pageControl">
                <uc2:PageSet ID="PageSet1" runat="server" /> 
            </div>
        </div>
        <cc1:CustomGridView ID="GridViewList" runat="server" AutoGenerateColumns="False"
            DataKeyNames="UserID">
            <Columns>
                <%--<asp:TemplateField HeaderText="">
                    <ItemStyle HorizontalAlign="Center" Width="40" />
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:BoundField DataField="LoginName" HeaderText="学员账户"  ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft field14">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="RealName" HeaderText="学员姓名" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft field14">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center"  />
                </asp:BoundField>
                <asp:BoundField DataField="WorkerNo" HeaderText="<%$ Resources:UIResource, ui_workno%>" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft field14">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <%--<asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_department%>" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                    <ItemTemplate>
                        <cc1:DictionaryLabel runat="server" ID="lblDepartment" DictionaryType="Site_DepartmentByOrgID"
                            FieldIDValue='<%#Eval("DepartmentID") %>' TextLength="4"/>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <%--<asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_rank%>" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                    <ItemTemplate>
                        <cc1:DictionaryLabel runat="server" ID="lblRank" DictionaryType="vw_Dic_Sys_Rank"
                            FieldIDValue='<%#Eval("RankID") %>' TextLength="4"/>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:BoundField DataField="Email" HeaderText="邮箱">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="MobilePhone" HeaderText="手机">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="CreateTime" HeaderText="创建时间">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="状态" HeaderStyle-Width="35px">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="LabStatus" runat="server" Text='<%#(int)Eval("Status")==1?"启用":"停用" %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False" HeaderText="操作" HeaderStyle-Width="200px">
                    <ItemTemplate>
                        <a href='javascript:showWindow("编辑学员信息","<%# this.ActionHref(String.Format("View.aspx?op=edit&id={0}", new Object[]{Eval("UserID")})) %>")'>
                            编辑</a> <a href='javascript:showWindow("查看学员信息","<%# this.ActionHref(String.Format("View.aspx?op=view&id={0}", new Object[]{Eval("UserID")})) %>")'>
                                查看</a>
                        <cc1:CustomLinkButton runat="server" ID="lkReset" Text="重置密码" OnCommand="UserOpeator_Command"
                            EnableConfirm="true" ConfirmMessage="确认要重置该学员密码？" CommandName="Reset" CommandArgument='<%#Eval("UserID") %>'></cc1:CustomLinkButton>
                        <cc1:CustomLinkButton runat="server" ID="lkSwitchStatus" Text='<%#0==(int)Eval("Status")?"启用":"停用" %>'
                            OnCommand="UserOpeator_Command" EnableConfirm="true" ConfirmMessage='<%#string.Format("确认要{0}该用户？",0==(int)Eval("Status")?"启用":"停用") %>'
                            CommandName="SwitchStatus" CommandArgument='<%#Eval("UserID") %>'></cc1:CustomLinkButton>
                        <cc1:CustomLinkButton runat="server" ID="CustomLinkButton1" Text='删除' OnCommand="UserOpeator_Command"
                            EnableConfirm="true" ConfirmMessage='确认要删除该学员信息？' CommandName="Remove" CommandArgument='<%#Eval("UserID") %>'></cc1:CustomLinkButton>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
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
</asp:Content>
