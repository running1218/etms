<%@ Page Title="课程申请审核信息查询" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="CourseApplyQuery.aspx.cs" Inherits="LearningManagement_CourseApply_CourseApplyQuery" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <!--导航路径-->
        <div class="dv_path" id="dv_path">
            当前位置：学习管理&gt;&gt;课程申请审核信息查询
        </div>
        <!--功能标题-->
        <h2 class="dv_title">
            课程申请审核信息查询
        </h2>
        <!--查找条件-->
        <div class="dv_searchbox">
            <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <th>
                        课程名称：
                    </th>
                    <td colspan="5">
                        <asp:TextBox ID="TextBox1" runat="server" CssClass="inputbox_210"></asp:TextBox>
                        <input class="btn_Search" type="button" value="查询"/><a href="javascript:hideGridview()" class="dropdownico"
                            id="Highsearch">高级搜索</a>
                    </td>
                </tr>
                <tr>
                    <th>
                        项目名称：
                    </th>
                    <td>
                        <asp:TextBox ID="TextBox2" runat="server" CssClass="inputbox_120"></asp:TextBox>
                    </td>
                    <th>
                        项目状态：
                    </th>
                    <td>
                        <cc1:DictionaryDropDownList runat="server" ID="Dic_Status1" DictionaryType="Dic_TraningProjectReleaseState" />
                    </td>
                    <th>
                        课程状态：
                    </th>
                    <td>
                        <cc1:DictionaryDropDownList runat="server" ID="Dic_ClassroomUse1" DictionaryType="Dic_CourseApplyStatus" />
                    </td>
                </tr>
                <tr>
                    <th>
                        所属<asp:Literal ID="ltlDepartment" runat="server" Text="<%$ Resources:UIResource, ui_department%>"></asp:Literal>：
                    </th>
                    <td>
                        <asp:TextBox ID="TextBox4" runat="server" CssClass="inputbox_120"></asp:TextBox>
                    </td>
                    <th>
                        <asp:Literal ID="ltlPosition" runat="server" Text="<%$ Resources:UIResource, ui_position%>"></asp:Literal>：
                    </th>
                    <td>
                        <asp:TextBox ID="TextBox3" runat="server" CssClass="inputbox_120"></asp:TextBox>
                    </td>
                    <th>
                       <asp:Literal ID="ltlRank" runat="server" Text="<%$ Resources:UIResource, ui_rank%>"></asp:Literal>：
                    </th>
                    <td>
                        <asp:TextBox ID="TextBox5" runat="server" CssClass="inputbox_120"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>
                        学员姓名：
                    </th>
                    <td>
                        <asp:TextBox ID="TextBox6" runat="server" CssClass="inputbox_120"></asp:TextBox>
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
            <div class="dv_pagePanel" id="divPage1">
                <div class="dv_pageControl">
                    <uc2:PageSet ID="PageSet1" runat="server" />
                </div>
            </div>
            <!--列表 begin-->
            <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="True"
                CustomAllowPaging="false" ShowFooter="false" AutoCreateColumnInsertIndex="1">
                <Columns>
                    <asp:TemplateField HeaderText="" Visible="false">
                        <ItemStyle HorizontalAlign="Center" Width="40" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="序号">
                        <ItemStyle HorizontalAlign="Center" Width="26" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="LabNo" runat="server" Text='<%#  ((this.PageSet1.PageIndex -1) * this.PageSet1.PageSize) + Container.DataItemIndex+1 %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="操作">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <a href="javascript:showWindow('查看课程申请','CourseApplyView.aspx')">查看</a>
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
            <script language="javascript" type="text/javascript">
                divPage2.innerHTML = divPage1.innerHTML;
            </script>
        </div>
    </div>
</asp:Content>
