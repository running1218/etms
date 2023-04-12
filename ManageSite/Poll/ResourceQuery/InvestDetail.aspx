<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InvestDetail.aspx.cs" Inherits="Poll_ResourceQuery_InvestDetail"
    MasterPageFile="~/MasterPages/MPageAdmin.Master" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content runat="server" ContentPlaceHolderID="cphBack" ID="Content2">
    <asp:HyperLink runat="server" Text="返回" ID="hylReturn" CssClass="btn_Return"></asp:HyperLink>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--查找条件-->
    <div class="dv_information" style="height: 550px">
        <table class="GridviewGray">
            <tr>
                <th style="width: 100px">
                    调查名称：
                </th>
                <td>
                    <asp:Label runat="server" ID="lblQueryName"></asp:Label>
                </td>
            </tr>
            <tr>
                <th style="width: 100px">
                    调查时间：
                </th>
                <td style="width: 280px">
                    <cc1:DateTimeLabel runat="server" ID="lblBeginTime" />&nbsp;至&nbsp;<cc1:DateTimeLabel
                        runat="server" ID="lblEndTime" />
                </td>
            </tr>
            <tr>
                <th style="width: 100px">
                    提交状态：
                </th>
                <td style="width: 280px">
                    已提交
                </td>
            </tr>
        </table>
        <div class="dv_searchlist">
            <!--翻页-->
            <div class="dv_pagePanel">
                <div class="dv_selectAll">
                    <asp:Button runat="server" ID="btnExport" Text="导出" OnClick="btnExport_Click" CssClass="btn_Export" />
                </div>
                <div class="dv_pageControl">
                    <uc2:PageSet ID="PageSet1" runat="server" />
                </div>
            </div>
            <!--列表 begin-->
            <cc1:CustomGridView ID="GridViewList" runat="server" AutoGenerateColumns="False"
                DataKeyNames="BatchID" CustomAllowPaging="false" ShowFooter="false">
                <Columns>
                    <asp:BoundField HeaderText="姓名" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft"
                        DataField="RealName" />
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
                            <cc1:ShortTextLabel ID="ShortTextLabel1" runat="server" Text='<%#Eval("Telphone") %>'></cc1:ShortTextLabel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="邮箱" HeaderStyle-CssClass="alignleft">
                        <ItemStyle CssClass="alignleft" />
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="ShortTextLabel1" runat="server" Text='<%#Eval("Email") %>'></cc1:ShortTextLabel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="学员状态" HeaderStyle-CssClass="alignleft">
                        <ItemStyle CssClass="alignleft" />
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="ShortTextLabel1" runat="server" Text='<%# ((int)Eval("Status")==0) ? "停用" : "启用" %>'></cc1:ShortTextLabel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="提交时间" HeaderStyle-Width="100" HeaderStyle-CssClass="alignleft"
                        ItemStyle-CssClass="alignleft" DataField="CreateTime" DataFormatString="{0:d}"
                        HtmlEncode="false" />
                    <asp:TemplateField HeaderText="操作" HeaderStyle-Width="60">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <a href="<%# this.ActionHref(String.Format("QueryAnswer.aspx?QueryID={0}&ResourceType={1}&BatchID={2}",QueryID,ResourceType,Eval("BatchID"))) %>"
                                target="_blank">查看</a>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </cc1:CustomGridView>
            <!--列表 end-->
            <!--导出表格信息 -->
            <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="False"
                DataKeyNames="BatchID" CustomAllowPaging="false" ShowFooter="false">
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
                            <cc1:ShortTextLabel ID="ShortTextLabel1" runat="server" Text='<%#Eval("Telphone") %>'></cc1:ShortTextLabel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="邮箱" HeaderStyle-CssClass="alignleft">
                        <ItemStyle CssClass="alignleft" />
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="ShortTextLabel1" runat="server" Text='<%#Eval("Email") %>'></cc1:ShortTextLabel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="提交状态">
                        <ItemStyle HorizontalAlign="Center" Width="60" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='已提交'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="学员状态" HeaderStyle-CssClass="alignleft">
                        <ItemStyle CssClass="alignleft" />
                        <ItemTemplate>
                            <cc1:ShortTextLabel ID="ShortTextLabel1" runat="server" Text='<%# ((int)Eval("Status")==0) ? "停用" : "启用" %>'></cc1:ShortTextLabel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="提交时间" HeaderStyle-Width="100" HeaderStyle-CssClass="alignleft"
                        ItemStyle-CssClass="alignleft" DataField="CreateTime" DataFormatString="{0:d}"
                        HtmlEncode="false" />
                </Columns>
            </cc1:CustomGridView>
            <div class="dv_splitLine">
            </div>
            <!--翻页-->
            <div class="dv_pagePanel">
            </div>
        </div>
    </div>
    <!--查找结果-->
</asp:Content>
