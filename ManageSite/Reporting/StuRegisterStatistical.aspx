<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/MReport.master"
    CodeFile="StuRegisterStatistical.aspx.cs" Inherits="Reporting_StuRegisterStatistical" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="content2" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
    <title>公司注册人数汇总</title>
</asp:Content>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="dv_HeaderTitle">
        <h2 class="h_titleName2">公司注册人数汇总</h2>
    </div>
    <div class="dv_reports">
        <div class="dv_searchbox">
            <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0" runat="server"
                id="tableQueryControlList">
                <tr>
                    <th style="width: 120px">组织机构：
                    </th>
                    <td>
                        <cc1:DictionaryDropDownList runat="server" ID="ddl_OrganizationID" DictionaryType="Dic_CurrentAndSubOrganization"
                            IsShowAll="true" CssClass="select_390" />
                    </td>
                    <th style="width: 120px">学员状态：
                    </th>
                    <td>
                        <asp:DropDownList ID="ddlUserStatus" runat="server" CssClass="select_120">
                            <asp:ListItem Value="1">启用</asp:ListItem>
                            <asp:ListItem Value="0">停用</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Button ID="btnSearch" CssClass="btn_Search" runat="server" Text="查询" OnClick="btnSearch_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <!--查找结果-->
        <div class="dv_searchlist" style="width: 98%;">
            <!--翻页-->
            <div class="dv_pagePanel" id="divPage1">
                <div class="dv_selectAll">
                    <asp:Button ID="btnExport" runat="server" Text="导出" CssClass="btn_Export" OnClick="btnExport_Click" />
                    <input type="button" class="hide" value="导出" />
                </div>
                <div class="dv_pageControl">
                    <uc2:PageSet ID="PageSet1" runat="server" />
                </div>
            </div>
            <!--列表 begin-->
            <div style="overflow-x: auto; overflow-y: hidden">
                <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="false"
                    CustomAllowPaging="false" ShowFooter="false" AutoCreateColumnInsertIndex="0"
                    Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="上级组织机构" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                            <ItemTemplate>
                                <%# Eval("DisplayPath")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <Columns>
                        <asp:TemplateField HeaderText="组织机构" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                            <ItemTemplate>
                                <%# Eval("OrganizationName")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <Columns>
                        <asp:TemplateField HeaderText="注册人数" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft" HeaderStyle-Width="200">
                            <ItemTemplate>
                                <%# Eval("UserCount")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </cc1:CustomGridView>
            </div>
            <!--列表 end-->
            <div class="dv_splitLine">
            </div>
            <!--翻页-->
            <div class="dv_pagePanel" id="divPage2">
            </div>
        </div>
    </div>
</asp:Content>
