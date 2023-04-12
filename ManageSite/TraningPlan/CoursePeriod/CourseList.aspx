<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master" AutoEventWireup="true" CodeFile="CourseList.aspx.cs" Inherits="TraningPlan_CoursePeriod_CourseList" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <!--导航路径-->
        <div class="dv_path" id="dv_path">
            当前位置：培训计划>>计划课时安排管理
        </div>
        <!--功能标题-->
        <h2 class="dv_title">
            计划课时安排管理
        </h2>
        <!--查找条件-->
        <div class="dv_searchbox">
            <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <th>
                        计划编码：
                    </th>
                    <td>
                        <asp:TextBox ID="TextBox1" runat="server" CssClass="inputbox_120"></asp:TextBox>
                    </td>
                    <th>
                        计划名称：
                    </th>
                    <td>
                        <asp:TextBox ID="TextBox2" runat="server" CssClass="inputbox_120"></asp:TextBox>
                        <input class="btn_Search" type="button" value="查询"/><a href="javascript:hideGridview()" class="dropdownico"
                            id="Highsearch">高级搜索</a>
                    </td>
                </tr>
                <tr>
                    <th>
                        计划状态：
                    </th>
                    <td>
                        <cc1:DictionaryDropDownList runat="server" ID="DictionaryDropDownList1" DictionaryType="Dic_TraningPlanCoursePeriodState"
                            IsShowAll="true" />
                    </td>
                    <th>
                        负责讲师：
                    </th>
                    <td>
                        <asp:TextBox ID="TextBox3" runat="server" CssClass="inputbox_120"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>
                        课程编码：
                    </th>
                    <td>
                        <asp:TextBox ID="TextBox5" runat="server" CssClass="inputbox_120"></asp:TextBox>
                    </td>
                    <th>
                        课程名称：
                    </th>
                    <td>
                        <asp:TextBox ID="TextBox4" runat="server" CssClass="inputbox_120"></asp:TextBox>
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
                        <ItemStyle HorizontalAlign="Center" Width="28" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="序号" Visible="false">
                        <ItemStyle HorizontalAlign="Center" Width="28" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="LabNo" runat="server" Text='<%#  ((this.PageSet1.PageIndex -1) * this.PageSet1.PageSize) + Container.DataItemIndex+1 %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="操作">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <a href="CoursePeriodList.aspx">课时管理</a>
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

