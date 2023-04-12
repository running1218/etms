<%@ Page Title="课程申请审核" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="CourseApplyList.aspx.cs" Inherits="LearningManagement_CourseApply_CourseApplyList" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <!--导航路径-->
        <div class="dv_path" id="dv_path">
            当前位置：学习管理>>课程申请审核
        </div>
        <!--功能标题-->
        <h2 class="dv_title">
            课程申请审核
        </h2>
        <!--查找条件-->
        <div class="dv_searchbox">
            <table width="98%" border="0" cellspacing="0" cellpadding="0" class="GridviewGray">
                <tr>
                    <th>
                        课程名称：
                    </th>
                    <td>
                        <asp:TextBox ID="TextBox4" runat="server" CssClass="inputbox_120"></asp:TextBox>
                    </td>
                    <th>
                        项目名称：
                    </th>
                    <td>
                        <asp:TextBox ID="TextBox1" runat="server" CssClass="inputbox_120"></asp:TextBox>
                        <input class="btn_Search" type="button" value="查询"/><a href="javascript:hideGridview()" class="dropdownico"
                            id="Highsearch">高级搜索</a>
                    </td>
                </tr>
                <tr>
                    <th>
                        审核状态：
                    </th>
                    <td>
                        <cc1:DictionaryDropDownList runat="server" ID="Dic_CourseApplyStatus1" DictionaryType="Dic_AuditState" />
                    </td>
                    <th>
                        <asp:Literal ID="ltlRank" runat="server" Text="<%$ Resources:UIResource, ui_rank%>"></asp:Literal>：
                    </th>
                    <td>
                        <cc1:DictionaryDropDownList runat="server" ID="Dic_Rank1" DictionaryType="Dic_Rank" />
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="ltlPosition" runat="server" Text="<%$ Resources:UIResource, ui_position%>"></asp:Literal>：
                    </th>
                    <td>
                        <cc1:DictionaryDropDownList runat="server" ID="DictionaryDropDownList1" DictionaryType="Dic_PostType" />
                    </td>
                    <th>
                        所属<asp:Literal ID="ltlDepartment" runat="server" Text="<%$ Resources:UIResource, ui_department%>"></asp:Literal>：
                    </th>
                    <td>
                        <cc1:DictionaryDropDownList runat="server" ID="Dic_DepartmentList1" DictionaryType="Dic_DepartmentList" />
                    </td>
                </tr>
                <tr>
                    <th>
                        学员姓名：
                    </th>
                    <td>
                        <asp:TextBox ID="TextBox5" runat="server" CssClass="inputbox_120"></asp:TextBox>
                    </td>
                    <th>
                        申请时间：
                    </th>
                    <td colspan="3">
                        <cc1:DateTimeTextBox ID="DateTimeTextBox1" runat="server" EndTimeControlID="DateTimeTextBox2"></cc1:DateTimeTextBox>
                        至
                        <cc1:DateTimeTextBox ID="DateTimeTextBox2" runat="server" BeginTimeControlID="DateTimeTextBox1"></cc1:DateTimeTextBox>
                    </td>
                </tr>
            </table>
        </div>
        <!--查找结果-->
        <div class="dv_searchlist">
            <!--翻页-->
            <div class="dv_pagePanel">
                <div class="dv_selectAll">
                    <cc1:CheckBoxsController runat="server" ID="chkboxControler" ContainerID="CustomGridView1" />
                    <input type="button" class="btn_Agree" value="审核通过" onclick="javascript:showWindow('课程申请审核','CourseApply.aspx')" />
                    <input type="button" class="btn_Deny" value="审核不通过" onclick="javascript:showWindow('课程申请审核','CourseApply.aspx')" />
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
                        <ItemStyle HorizontalAlign="Center" Width="28" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="序号">
                        <ItemStyle HorizontalAlign="Center" Width="28" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="LabNo" runat="server" Text='<%#  ((this.PageSet1.PageIndex -1) * this.PageSet1.PageSize) + Container.DataItemIndex+1 %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="操作">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <a href="javascript:showWindow('课程申请审核','CourseApply.aspx')">审核</a> <a href="javascript:showWindow('查看课程申请','CourseApplyView.aspx')">
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
    </div>
</asp:Content>
