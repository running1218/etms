<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="SetsStudentAdd.aspx.cs" Inherits="TraningImplement_TraningProjectManager_SetsStudentAdd" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBack" runat="Server">
    <asp:LinkButton ID="lbtnReturn" runat="server" Text="返回" CssClass="btn_Return"></asp:LinkButton>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <!--查找条件-->
        <div class="dv_searchbox">
            <cc1:DictionaryDropDownList runat="server" ID="DictionaryDropDownList1" DictionaryType="Dic_CurrentAndSubOrganization"
                IsShowAll="true" AutoPostBack="True" CssClass="select_390" Visible="false" />
            <table class="GridviewGray th80" border="0" cellpadding="0" cellspacing="0" runat="server"
                id="tableQueryControlList">
                <tr>
                    <th width="100" style="display:none;">
                        工&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;号：
                    </th>
                    <td style="display:none;">
                        <asp:TextBox ID="txt_WorkerNo" runat="server" CssClass="inputbox_120"></asp:TextBox>
                    </td>
                    <th width="100">
                        姓&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;名：
                    </th>
                    <td class="Search_Area">
                        <asp:TextBox ID="txt_RealName" runat="server" CssClass="inputbox_120 floatleft"></asp:TextBox>
                        <asp:Button ID="btnSearch" CssClass="btn_Search" runat="server" Text="查询" OnClick="btnSearch_Click" />
                        <a href="javascript:hideGridview()" class="dropdownico" id="Highsearch">高级搜索</a>
                    </td>
                </tr>
                <tr runat="server" id="trOrg">
                    <th width="100">
                        组织机构：
                    </th>
                    <td colspan="3">
                        <cc1:DictionaryDropDownList runat="server" ID="ddl_OrganizationID" DictionaryType="Dic_CurrentAndSubOrganization"
                            IsShowAll="true" AutoPostBack="True" CssClass="select_390" OnSelectedIndexChanged="ddl_OrganizationID_SelectedIndexChanged" />
                    </td>
                </tr>
                <tr class="hide">
                    <th width="100">
                        <asp:Literal ID="ltlDepartment" runat="server" Text="<%$ Resources:UIResource, ui_department%>"></asp:Literal>：
                    </th>
                    <td>
                        <asp:DropDownList runat="server" ID="ddl_DepartmentID" CssClass="select_190">
                        </asp:DropDownList>
                    </td>
                    <th width="100">
                        <asp:Literal ID="ltlPosition" runat="server" Text="<%$ Resources:UIResource, ui_position%>"></asp:Literal>：
                    </th>
                    <td>
                        <asp:DropDownList runat="server" ID="ddl_PostID" CssClass="select_190">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr class="hide">
                    <th width="100">
                        <asp:Literal ID="ltlRank" runat="server" Text="<%$ Resources:UIResource, ui_rank%>"></asp:Literal>：
                    </th>
                    <td colspan="3">
                        <cc1:DictionaryDropDownList runat="server" ID="ddl_RankID" DictionaryType="vw_Dic_Sys_Rank"
                            IsShowAll="true" CssClass="select_190" />
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
                    <asp:Button ID="btnAdd" runat="server" Text="确定" CssClass="btn_Ok" OnClick="btnAdd_Click" />
                    <cc1:CustomButton runat="server" ID="btnAddAll" Text="添加全部" CssClass="btn_AllStudent"
                        EnableConfirm="true" ConfirmTitle="提示" 
                        ConfirmMessage="您确认将查询结果全部学员加入到此培训项目中？" onclick="btnAddAll_Click" />
                </div>
                <div class="dv_pageControl">
                    <uc2:PageSet ID="PageSet1" runat="server" />
                </div>
            </div>
            <!--列表 begin-->
            <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="false"
                CustomAllowPaging="false" ShowFooter="false" AutoCreateColumnInsertIndex="0"
                DataKeyNames="UserID" OnRowDataBound="CustomGridView1_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="">
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" Width="20" />
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="LoginName" HeaderText="学员帐号" HeaderStyle-CssClass="alignleft field18"
                        ItemStyle-CssClass="alignleft" />
                    <asp:BoundField DataField="RealName" HeaderText="姓名" HeaderStyle-CssClass="alignleft field18"
                        ItemStyle-CssClass="alignleft" />
                    <asp:TemplateField HeaderText="组织机构" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="lblOrganization" DictionaryType="Dic_CurrentAndSubOrganization"
                                FieldIDValue='<%# Eval("OrganizationID") %>' runat="server" TextLength="30" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_department%>" HeaderStyle-CssClass="alignleft hide" ItemStyle-CssClass="alignleft hide">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="lblDepartmentID" DictionaryType="vw_Dic_Sys_Department"
                                FieldIDValue='<%# Eval("DepartmentID") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_position%>" HeaderStyle-CssClass="alignleft hide" ItemStyle-CssClass="alignleft hide">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="lblPost" DictionaryType="vw_Dic_Sys_Post" FieldIDValue='<%# Eval("PostID") %>'
                                runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_rank%>" HeaderStyle-Width="90" HeaderStyle-CssClass="alignleft hide"
                        ItemStyle-CssClass="alignleft hide">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="lblRank" DictionaryType="vw_Dic_Sys_Rank" FieldIDValue='<%# Eval("RankID") %>'
                                runat="server" />
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
</asp:Content>
