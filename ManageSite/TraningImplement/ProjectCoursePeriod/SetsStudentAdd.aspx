<%@ Page Title="新增学员" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="SetsStudentAdd.aspx.cs" Inherits="TraningImplement_ProjectCoursePeriod_SetsStudentAdd" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBack" runat="Server">
    <asp:LinkButton ID="lbtnReturn" runat="server" Text="返回" CssClass="btn_Return"></asp:LinkButton>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <div class="dv_GradeviewList fixedTable">
            <table class="" border="0" cellpadding="0" cellspacing="0" runat="server" id="tableQueryControlList">
                <tr>
                    <th width="100">
                        项目名称：
                    </th>
                    <td width="200">
                        <asp:Label ID="lblItemName" runat="server"></asp:Label>
                    </td>
                    <th width="100">
                        课程名称：
                    </th>
                    <td>
                        <asp:Label ID="lblCourseName" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        班&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;级：
                    </th>
                    <td width="200">
                        <asp:DropDownList ID="ddl_c999ClassID" runat="server">
                        </asp:DropDownList>
                    </td>
                    <th width="100">
                        姓&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;名：
                    </th>
                    <td>
                        <asp:TextBox ID="txt_u999RealName" runat="server" CssClass="inputbox_120"></asp:TextBox>
                        <asp:Button ID="btnSearch" CssClass="btn_Search" runat="server" Text="查询" OnClick="btnSearch_Click" />
                    </td>
                </tr>
                <tr id="trOrg" runat="server">
                    <th width="100">
                        组织机构：
                    </th>
                    <td colspan="3">
                        <asp:DropDownList runat="server" ID="ddl_u999OrganizationID" IsShowAll="true" AutoPostBack="True"
                            CssClass="select_120" OnSelectedIndexChanged="ddl_u999OrganizationID_SelectedIndexChanged" />
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        <asp:Literal ID="ltlDepartment" runat="server" Text="<%$ Resources:UIResource, ui_department%>"></asp:Literal>：
                    </th>
                    <td width="200">
                        <asp:DropDownList runat="server" ID="ddl_u999DepartmentID">
                        </asp:DropDownList>
                    </td>
                    <th width="100">
                        <asp:Literal ID="ltlPosition" runat="server" Text="<%$ Resources:UIResource, ui_position%>"></asp:Literal>：
                    </th>
                    <td>
                        <asp:DropDownList runat="server" ID="ddl_u999PostID">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th width="100">
                        <asp:Literal ID="ltlRank" runat="server" Text="<%$ Resources:UIResource, ui_rank%>"></asp:Literal>：
                    </th>
                    <td>
                        <cc1:DictionaryDropDownList runat="server" ID="ddl_u999RankID" DictionaryType="vw_Dic_Sys_Rank"
                            IsShowAll="true" />
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
                    <asp:Button ID="btnAdd" runat="server" Text="确定" CssClass="btn_Add" OnClick="btnAdd_Click" />
                </div>
                <div class="dv_pageControl">
                    <uc2:PageSet ID="PageSet1" runat="server" />
                </div>
            </div>
            <!--列表 begin-->
            <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="false"
                CustomAllowPaging="false" ShowFooter="false" AutoCreateColumnInsertIndex="0"
                DataKeyNames="StudentCourse" OnRowDataBound="CustomGridView1_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="">
                        <ItemStyle HorizontalAlign="Center" Width="28" />
                        <HeaderStyle HorizontalAlign="Center" Width="20" />
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="WorkerNo" HeaderText="工号" ItemStyle-CssClass="alignleft"
                        HeaderStyle-CssClass="alignleft" HeaderStyle-Width="60" />
                    <asp:BoundField DataField="RealName" HeaderText="姓名" ItemStyle-CssClass="alignleft"
                        HeaderStyle-CssClass="alignleft" HeaderStyle-Width="60" />
                    <asp:TemplateField HeaderText="组织机构"  ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="lblOrganization" DictionaryType="Dic_CurrentAndSubOrganization"
                                FieldIDValue='<%# Eval("OrganizationID") %>' runat="server" TextLength="10" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_department%>" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="lblDepartmentID" DictionaryType="vw_Dic_Sys_Department"
                                FieldIDValue='<%# Eval("DepartmentID") %>' runat="server" TextLength="10" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_position%>" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="lblPost" DictionaryType="vw_Dic_Sys_Post" TextLength="10"
                                FieldIDValue='<%# Eval("PostID") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_rank%>" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="lblRank" DictionaryType="vw_Dic_Sys_Rank" TextLength="10"
                                FieldIDValue='<%# Eval("RankID") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="班级" HeaderStyle-Width="100">
                        <ItemTemplate>
                            <cc1:DictionaryLabel ID="lblClassName" DictionaryType="" TextLength="10" FieldIDValue='<%# Eval("ClassID") %>'
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
