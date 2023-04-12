<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPagePop.Master"
    AutoEventWireup="true" CodeFile="CoursePeriodView.aspx.cs" Inherits="TraningPlan_TraningPlan_CoursePeriodView" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--功能标题-->
    <h2 class="dv_title">
        查看培训计划<input type="button" class="btn_Return" value="返回" onclick="javascript:history.back()" />
    </h2>
    <div class="dv_information">
        <table class="GridviewGray">
            <tr>
                <th width="80">
                    课程编码：
                </th>
                <td>
                    JH100234
                </td>
                <th width="80">
                    课程名称：
                </th>
                <td>
                    世界精品课程
                </td>
            </tr>
            <tr>
                <th>
                    课程类型：
                </th>
                <td>
                    制度文化
                </td>
                <th>
                    培训方式：
                </th>
                <td>
                    内部培训
                </td>
            </tr>
            <tr>
                <th>
                    授课方式：
                </th>
                <td>
                    在线
                </td>
                <th>
                    外训机构：
                </th>
                <td>
                    新东方
                </td>
            </tr>
        </table>
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
    <div class="dv_submit"><a href="javascript:closeWindow();" class="btn_Close">关闭</a></div>
</asp:Content>
