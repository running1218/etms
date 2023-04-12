<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="SetsStudentList.aspx.cs" Inherits="TraningImplement_TraningProjectManager_SetsStudentList" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBack" runat="Server">
    <a href="ProjectStudentList.aspx" class="btn_Return">返回</a>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <!--查找条件-->
        <div class="dv_GradeviewList">
            <table class="" border="0" cellpadding="0" cellspacing="0" runat="server" id="tableQueryControlList">
                <tr>
                    <th width="100">
                        项目编码：
                    </th>
                    <td>
                        <asp:Label ID="lblItemCode" runat="server"></asp:Label>
                    </td>
                    <th width="100">
                        项目名称：
                    </th>
                    <td>
                        <asp:Label ID="lblItemName" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        姓&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;名：
                    </th>
                    <td class="Search_Area">
                        <asp:TextBox ID="txt_Site_User999RealName" runat="server" CssClass="inputbox_120 floatleft"></asp:TextBox>                        
                    </td>
                    <th width="100">
                        入职日期：
                    </th>
                    <td colspan="3">
                        <cc1:DateTimeTextBox ID="begin_Site_Student999JoinTime" runat="server" EndTimeControlID="end_Site_Student999JoinTime"></cc1:DateTimeTextBox>
                        至
                        <cc1:DateTimeTextBox ID="end_Site_Student999JoinTime" runat="server" BeginTimeControlID="begin_Site_Student999JoinTime"></cc1:DateTimeTextBox>
                    </td>
                </tr>
                <tr style="display:none;">
                    <th width="100">
                        工&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;号：
                    </th>
                    <td colspan="3">
                        <asp:TextBox ID="txt_Site_Student999WorkerNo" runat="server" CssClass="inputbox_120"></asp:TextBox>
                    </td>
                </tr>
                <tr id="trOrg" runat="server">
                    <th width="100">
                        组织机构：
                    </th>
                    <td colspan="3">
                        <asp:DropDownList runat="server" ID="ddl_Site_User999OrganizationID" IsShowAll="true"
                            CssClass="select_390" />
                        <asp:Button ID="btnSearch" CssClass="btn_Search" runat="server" Text="查询" OnClick="btnSearch_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <!--查找结果-->
        <div class="dv_searchlist">
            <!--翻页-->
            <div class="dv_pagePanel" id="divPage1">
                <div class="dv_selectAll">
                    <cc1:CheckBoxsController runat="server" ID="chkboxControler" ContainerID="CustomGridView1" /><cc1:CustomButton
                        ID="lbtnAdd" runat="server" Text="新增" EnableConfirm="false" CssClass="btn_Add">
                    </cc1:CustomButton><cc1:CustomButton ID="lbtnImport" runat="server" CssClass="btn_Import"
                        OnClientClick="" Text="导入" EnableConfirm="false"></cc1:CustomButton><cc1:CustomButton
                            ID="lbtnExport" runat="server" CssClass="btn_Export" Text="导出" OnClick="btn_Export_Click"
                            EnableConfirm="false"></cc1:CustomButton><cc1:CustomButton runat="server" ID="cbtnDel"
                                Text="删除" CssClass="btn_Del" EnableConfirm="true" ConfirmTitle="提示" ConfirmMessage="删除学员，将直接删掉学员项目课程信息，你确认删除所选的项目学员吗？"
                                OnClick="cbtnDel_Click" />
                </div>
                <div class="dv_pageControl">
                    <uc2:PageSet ID="PageSet1" runat="server" />
                </div>
            </div>
            <!--列表 begin-->
            <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="false"
                CustomAllowPaging="false" ShowFooter="false" AutoCreateColumnInsertIndex="0"
                DataKeyNames="StudentSignupID" OnRowDataBound="CustomGridView1_RowDataBound" OnRowCommand="CustomGridView1_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="">
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" Width="20" />
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:BoundField DataField="WorkerNo" HeaderText="工号" HeaderStyle-CssClass="alignleft field10 hide"
                        ItemStyle-CssClass="alignleft hide" />--%>
                    <asp:BoundField DataField="LoginName" HeaderText="学员账户" HeaderStyle-CssClass="alignleft field18"
                        ItemStyle-CssClass="alignleft" />
                    <asp:BoundField DataField="RealName" HeaderText="姓名" HeaderStyle-CssClass="alignleft field18"
                        ItemStyle-CssClass="alignleft" />
                    <asp:TemplateField HeaderText="组织机构" HeaderStyle-CssClass="alignleft" ItemStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="lblOrganization" DictionaryType="Dic_CurrentOrganization"
                                FieldIDValue='<%# Eval("OrganizationID") %>' FieldToolTipValue='<%# Eval("OrganizationID") %>' runat="server" TextLength="30" />
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
                                runat="server" TextLength="10" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_rank%>" ItemStyle-CssClass="alignleft hide" HeaderStyle-CssClass="alignleft hide">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="lblRank" DictionaryType="vw_Dic_Sys_Rank" FieldIDValue='<%# Eval("RankID") %>'
                                runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="状态" HeaderStyle-CssClass="aligncenter field8" ItemStyle-CssClass="aligncenter field8">
                        <ItemTemplate>
                            <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("IsUse").ToString()=="1"?"启用":"停用" %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="操作" ItemStyle-CssClass="aligncenter field8" HeaderStyle-CssClass="aligncenter field8">
                        <ItemTemplate>
                            <cc1:CustomLinkButton ID="lbnStatus" runat="server" Text='<%# Eval("IsUse").ToInt() == 0 ? "启用":"停用" %>' CommandArgument='<%# Eval("StudentSignupID") %>' CommandName="Status"></cc1:CustomLinkButton>
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
