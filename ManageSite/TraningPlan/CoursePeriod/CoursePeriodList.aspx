<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="CoursePeriodList.aspx.cs" Inherits="TraningPlan_CoursePeriod_CoursePeriodList" %>

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
            计划课时安排管理<input type="button" class="btn_Return" value="返回" onclick="javascript:history.back()" />
        </h2>
        <!--表单录入-->
        <div class="dv_GradeviewList">
            <table >
                <tr>
                    <th>
                        计划编码：
                    </th>
                    <td>
                        JX1002369
                    </td>
                    <th>
                        计划名称：
                    </th>
                    <td>
                        飞鹰01
                    </td>
                </tr>
                <tr>
                    <th>
                        课程编码：
                    </th>
                    <td>
                        C56004
                    </td>
                    <th>
                        课程名称：
                    </th>
                    <td>
                        领导力培训1
                    </td>
                </tr>
                <tr>
                    <th>
                        组织机构：
                    </th>
                    <td>
                        组织机构一
                    </td>
                    <th>
                        培训方式：
                    </th>
                    <td>
                        内部授课
                    </td>
                </tr>
                <tr>
                    <th>
                        课程类型：
                    </th>
                    <td>
                        专业技能
                    </td>
                    <th>
                        授课方式：
                    </th>
                    <td colspan="3">
                        非在线
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
                    <input type="button" class="btn_Add" value="新增" onclick="javascript:showWindow('新增培训计划课时','CoursePeriodAdd.aspx')" />
                    <input type="button" class="btn_Del" value="删除" onclick="" />
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
                            <a href="javascript:showWindow('编辑培训计划课时','CoursePeriodEdit.aspx')">
                                编辑</a>
                            <a href="javascript:showWindow('查看培训计划课时','CoursePeriodView.aspx')">查看</a> 
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
