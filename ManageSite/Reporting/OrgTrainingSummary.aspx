<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="OrgTrainingSummary.aspx.cs"
    MasterPageFile="~/MasterPages/MReport.master" Inherits="Reporting_OrgTrainingSummary" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="content2" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
    <title>机构培训汇总表</title>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1" runat="server">
    <div class="dv_HeaderTitle">
        <h2 class="h_titleName3">
            机构培训汇总表</h2>
    </div>
    <div class="dv_reports">
        <div class="dv_searchbox">
            <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0" runat="server"
                id="tableQueryControlList">
                <tr>
                    <th width="100">
                        机构名称：
                    </th>
                    <td class="Search_Area">
                        <asp:TextBox ID="txtOrgnizationName" runat="server" CssClass="inputbox_210 floatleft"></asp:TextBox>
                        <asp:Button ID="btnSearch" CssClass="btn_Search" runat="server" Text="查询" OnClick="btnSearch_Click" />
                    </td>
                </tr>
                <tr>
                    <th>
                        项目周期：
                    </th>
                    <td>
                        <cc1:DateTimeTextBox ID="txtBeginTime" runat="server" EndTimeControlID="txtEndTime"
                            DataTimeFormat="%Y-%M-%D" CssClass="date_format"></cc1:DateTimeTextBox>
                        至
                        <cc1:DateTimeTextBox ID="txtEndTime" runat="server" BeginTimeControlID="txtBeginTime"
                            DataTimeFormat="%Y-%M-%D" CssClass="date_format"></cc1:DateTimeTextBox>
                    </td>
                </tr>
            </table>
        </div>
        <!--查找结果-->
        <div class="dv_searchlist" style="width: 98%;">
            <!--翻页-->
            <div class="dv_pagePanel" id="divPage1">
                <div class="dv_selectAll">
                    
                    <asp:Button ID="btnExportNew"  Text="导出" runat="server"  onclick="btnExportNew_Click" CssClass="btn_Export"/>
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
                        <asp:TemplateField HeaderText="组织机构" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                            <ItemTemplate>
                                <cc1:ShortTextLabel ID="lblOrganizationName" runat="server" ShowTextNum="6" Text='<%# Eval("OrganizationName")%>'></cc1:ShortTextLabel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="项目数" HeaderStyle-Width="75">
                            <ItemTemplate>
                                <cc1:ShortTextLabel ID="lblItemNum" runat="server" ShowTextNum="6" Text='<%# Eval("ItemNum")%>'></cc1:ShortTextLabel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="在线课程数" HeaderStyle-Width="75">
                            <ItemTemplate>
                                <cc1:ShortTextLabel ID="lblOnLineCourseNum" runat="server" ShowTextNum="6" Text='<%# Eval("OnLineCourseNum")%>'></cc1:ShortTextLabel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="在线课时数" HeaderStyle-Width="85">
                            <ItemTemplate>
                                <cc1:ShortTextLabel ID="lblOnLineCourseHours" runat="server" ShowTextNum="6" Text='<%# Eval("OnLineCourseHours")%>'></cc1:ShortTextLabel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="非在线课程数" HeaderStyle-Width="90">
                            <ItemTemplate>
                                <cc1:ShortTextLabel ID="lblOffLineCourseNum" runat="server" ShowTextNum="6" Text='<%#  decimal.Round(Eval("OffLineCourseNum").ToHours(), 2)%>'></cc1:ShortTextLabel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="非在线课时数" HeaderStyle-Width="90">
                            <ItemTemplate>
                                <cc1:ShortTextLabel ID="lblOffLineCourseHours" runat="server" ShowTextNum="6" Text='<%# decimal.Round(Eval("OffLineCourseHours").ToHours(), 2)%>'></cc1:ShortTextLabel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="总课时数" HeaderStyle-Width="90">
                            <ItemTemplate>
                                <cc1:ShortTextLabel ID="lblOnLineCourseHoursTotal" runat="server" ShowTextNum="6"
                                    Text='<%# decimal.Round((Eval("OnLineCourseHours").ToHours() + Eval("OffLineCourseHours").ToHours()), 2) %>'></cc1:ShortTextLabel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="考试数" HeaderStyle-Width="90">
                            <ItemTemplate>
                                <cc1:ShortTextLabel ID="lblExamNum" runat="server" ShowTextNum="6" Text='<%# Eval("ExamNum")%>'></cc1:ShortTextLabel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="平均考试得分" HeaderStyle-Width="90">
                            <ItemTemplate>
                                <cc1:ShortTextLabel ID="lblAvgExam" runat="server" ShowTextNum="6" Text='<%# decimal.Round(Eval("AverageScore").ToString().ToDecimal() , 2).ToString() %>'></cc1:ShortTextLabel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="内部导师数" HeaderStyle-Width="90">
                            <ItemTemplate>
                                <cc1:ShortTextLabel ID="lblInnerTeacherNum" runat="server" ShowTextNum="6" Text='<%# Eval("InnerTeacherNum")%>'></cc1:ShortTextLabel>
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
    <script type="text/javascript">
        function hideLodfileDiv() {
            $(".shadowBj").hide();
            $(".loadfile").hide();
        }
    </script>
</asp:Content>
