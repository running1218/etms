<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="QueryUserAllList.aspx.cs" Inherits="Poll_ResourceQuery_QueryUserAllList" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBack" runat="Server">
    <asp:HyperLink runat="server" Text="返回" ID="hylReturn" CssClass="btn_Return">
    </asp:HyperLink>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--查找条件-->
    <div class="dv_searchbox">
        <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <th width="100">
                    调查名称：
                </th>
                <td width="300">
                    <asp:Label ID="lbl_QueryName" runat="server" Text=""></asp:Label>
                </td>
                <th width="100">
                    调查时间：
                </th>
                <td>
                    <asp:Label ID="lbl_QueryDate" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div class="dv_searchlist">
        <!--翻页-->
        <div class="dv_pagePanel">
            <div class="dv_selectAll">
                <asp:Button runat="server" ID="btnExport" Text="导出" CssClass="btn_Export" OnClick="btnExport_Click" />
            </div>
            <div class="dv_pageControl">
                <uc2:PageSet ID="PageSet1" runat="server" />
            </div>
        </div>
        <!--列表 begin-->
        <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="False"
            CustomAllowPaging="false" ShowFooter="false">
            <Columns>
                <asp:TemplateField HeaderText="姓名" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                    <ItemTemplate>
                        <a href='javascript:showWindow("学员信息","<%# this.ActionHref(String.Format("~/Security/StudentManager/View.aspx?op=view&id={0}", new Object[]{Eval("UserID")})) %>",650,500)'>
                            <%# Eval("RealName")%></a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="组织机构">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <cc1:DictionaryLabel ID="DictionaryLabel3" runat="server" DictionaryType='vw_Dic_Sys_Organization'
                            FieldIDValue='<%#Eval("OrganizationID") %>'></cc1:DictionaryLabel>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_department%>" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                    <ItemTemplate>
                        <cc1:DictionaryLabel ID="DictionaryLabel1" runat="server" DictionaryType='vw_Dic_Sys_Department'
                            FieldIDValue='<%#Eval("DepartmentID") %>'></cc1:DictionaryLabel>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_position%>" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <cc1:DictionaryLabel ID="DictionaryLabel2" runat="server" DictionaryType='vw_Dic_Sys_Post'
                            FieldIDValue='<%#Eval("PostID") %>'></cc1:DictionaryLabel>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="手机" HeaderStyle-CssClass="alignleft">
                    <ItemStyle CssClass="alignleft" />
                    <ItemTemplate>
                        <cc1:ShortTextLabel ID="ShortTextLabel1" runat="server" Text='<%#Eval("MobilePhone") %>'></cc1:ShortTextLabel>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="邮箱" HeaderStyle-CssClass="alignleft">
                    <ItemStyle CssClass="alignleft" />
                    <ItemTemplate>
                        <cc1:ShortTextLabel ID="ShortTextLabel1" runat="server" Text='<%#Eval("Email") %>'></cc1:ShortTextLabel>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="学员状态" HeaderStyle-Width="60">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <cc1:DictionaryLabel ID="lblIsUse" DictionaryType="Dic_Status" runat="server" FieldIDValue='<%#Eval("UserStatus") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="提交状态" HeaderStyle-Width="60">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <%# Eval("IsSubmit").ToInt() == 1 ? "已提交":"未提交" %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="提交时间" HeaderStyle-Width="100" HeaderStyle-CssClass="alignleft"
                    ItemStyle-CssClass="alignleft" DataField="SubmitDate" DataFormatString="{0:d}"
                    HtmlEncode="false" />
            </Columns>
        </cc1:CustomGridView>
        <!--列表 end-->
        <cc1:CustomGridView ID="CustomGridView2" runat="server" AutoGenerateColumns="False"
            CustomAllowPaging="false" ShowFooter="false">
            <Columns>
                <asp:BoundField HeaderText="姓名" HeaderStyle-Width="100" HeaderStyle-CssClass="alignleft"
                    ItemStyle-CssClass="alignleft" DataField="RealName" />
                <asp:TemplateField HeaderText="组织机构">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <cc1:DictionaryLabel ID="DictionaryLabel3" runat="server" DictionaryType='vw_Dic_Sys_Organization'
                            FieldIDValue='<%#Eval("OrganizationID") %>'></cc1:DictionaryLabel>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_department%>" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                    <ItemTemplate>
                        <cc1:DictionaryLabel ID="DictionaryLabel1" runat="server" DictionaryType='vw_Dic_Sys_Department'
                            FieldIDValue='<%#Eval("DepartmentID") %>'></cc1:DictionaryLabel>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_position%>" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <cc1:DictionaryLabel ID="DictionaryLabel2" runat="server" DictionaryType='vw_Dic_Sys_Post'
                            FieldIDValue='<%#Eval("PostID") %>'></cc1:DictionaryLabel>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="手机" HeaderStyle-CssClass="alignleft">
                    <ItemStyle CssClass="alignleft" />
                    <ItemTemplate>
                        <cc1:ShortTextLabel ID="ShortTextLabel1" runat="server" Text='<%#Eval("MobilePhone") %>'></cc1:ShortTextLabel>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="邮箱" HeaderStyle-CssClass="alignleft">
                    <ItemStyle CssClass="alignleft" />
                    <ItemTemplate>
                        <cc1:ShortTextLabel ID="ShortTextLabel1" runat="server" Text='<%#Eval("Email") %>'></cc1:ShortTextLabel>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="学员状态" HeaderStyle-Width="60">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <cc1:DictionaryLabel ID="lblIsUse" DictionaryType="Dic_Status" runat="server" FieldIDValue='<%#Eval("UserStatus") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="提交状态" HeaderStyle-Width="60">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <%# Eval("IsSubmit").ToInt() == 1 ? "已提交":"未提交" %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="提交时间" HeaderStyle-Width="100" HeaderStyle-CssClass="alignleft"
                    ItemStyle-CssClass="alignleft" DataField="SubmitDate" DataFormatString="{0:d}"
                    HtmlEncode="false" />
            </Columns>
        </cc1:CustomGridView>
        <div class="dv_splitLine">
        </div>
        <!--翻页-->
        <div class="dv_pagePanel" id="divPage2">
        </div>
    </div>
</asp:Content>
