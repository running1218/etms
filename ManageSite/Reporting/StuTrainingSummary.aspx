<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StuTrainingSummary.aspx.cs" MasterPageFile="~/MasterPages/MReport.master" Inherits="Reporting_StuTrainingSummary" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>

<asp:Content ID="content2" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
    <title>学员培训汇总表</title>
</asp:Content>
<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">    
<div class="dv_HeaderTitle">
     <h2 class="h_titleName2">学员培训汇总表</h2>
  </div>
    <div class="dv_reports">
        <div class="dv_searchbox">
            <table class="GridviewGray" border="0" cellpadding="0" cellspacing="0" runat="server"
                id="tableQueryControlList">
                <tr>
                    <th style="width:120px">
                        姓名：
                    </th>
                    <td style="width:240px">
                        <asp:TextBox ID="txtName" runat="server" CssClass="inputbox_120"></asp:TextBox>
                    </td>
                    <th style="width:120px">
                        工号：
                    </th>
                    <td class="Search_Area">
                        <asp:TextBox ID="txtWorkerNo" runat="server" CssClass="inputbox_120 floatleft"></asp:TextBox>
                        <asp:Button ID="btnSearch" CssClass="btn_Search" runat="server" Text="查询" OnClick="btnSearch_Click" />
                    </td>
                </tr>
                <tr>
                    <th>
                       <asp:Literal ID="ltlDepartment" runat="server" Text="<%$ Resources:UIResource, ui_department%>"></asp:Literal>：
                    </th>
                    <td>
                        <asp:TextBox ID="txtDepartment" runat="server" CssClass="inputbox_120"></asp:TextBox>
                    </td>
                    <th>
                        <asp:Literal ID="ltlPosition" runat="server" Text="<%$ Resources:UIResource, ui_position%>"></asp:Literal>：
                    </th>
                    <td>
                        <asp:TextBox ID="txtPost" runat="server" CssClass="inputbox_120"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>
                        项目周期：
                    </th>
                    <td colspan="3">
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
                    <asp:Button ID="btnExport" runat="server" Text="导出" CssClass="btn_Export" OnClick="btnExport_Click" />
                    <input type="button" class="hide" value="导出"/>
                </div>
                <div class="dv_pageControl">
                    <uc2:PageSet ID="PageSet1" runat="server" />
                </div>
            </div>
            <!--列表 begin-->
            <div style="overflow-x: auto; overflow-y: hidden">
                <cc1:CustomGridView ID="CustomGridView1" runat="server" AutoGenerateColumns="false"
                    CustomAllowPaging="false" ShowFooter="false" AutoCreateColumnInsertIndex="0" Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="<%$ Resources:UIResource, ui_department%>" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                            <ItemTemplate>
                                <cc1:ShortTextLabel ID="lblDepartmentName" runat="server" ShowTextNum="6" Text='<%# Eval("DepartmentName")%>'></cc1:ShortTextLabel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="学员姓名" HeaderStyle-Width="80">
                            <ItemTemplate>
                                <cc1:ShortTextLabel ID="lblRealName" runat="server" ShowTextNum="6" Text='<%# Eval("RealName")%>'></cc1:ShortTextLabel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="工号" HeaderStyle-Width="70">
                            <ItemTemplate>
                                <cc1:ShortTextLabel ID="lblWorkNo" runat="server" ShowTextNum="6" Text='<%# Eval("WorkerNo")%>'></cc1:ShortTextLabel>
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
                                <cc1:ShortTextLabel ID="lblOffLineCourseNum" runat="server" ShowTextNum="6" Text='<%# Eval("OffLineCourseNum")%>'></cc1:ShortTextLabel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="非在线课时数" HeaderStyle-Width="90">
                            <ItemTemplate>
                                <cc1:ShortTextLabel ID="lblOffLineCourseHours" runat="server" ShowTextNum="6" Text='<%# decimal.Round(Eval("OffLineCourseHours").ToHours(), 2)%>'></cc1:ShortTextLabel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="总课时数" HeaderStyle-Width="90">
                            <ItemTemplate>
                                <cc1:ShortTextLabel ID="lblOnLineCourseHoursTotal" runat="server" ShowTextNum="6" Text='<%# decimal.Round((Eval("OnLineCourseHours").ToHours() + Eval("OffLineCourseHours").ToHours()), 2) %>'></cc1:ShortTextLabel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="参加考试数" HeaderStyle-Width="90">
                            <ItemTemplate>
                                <cc1:ShortTextLabel ID="lblJoinExamNum" runat="server" ShowTextNum="6" Text='<%# Eval("JoinExamNum")%>'></cc1:ShortTextLabel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="平均考试得分" HeaderStyle-Width="90">
                            <ItemTemplate>
                                <cc1:ShortTextLabel ID="lblAvgExam" runat="server" ShowTextNum="6" Text='<%# Eval("JoinExamNum").ToString() == "0"? string.Empty : (decimal.Round(Eval("AverageScore").ToString().ToDecimal(), 2)).ToString() %>'></cc1:ShortTextLabel>
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
//        $(document).ready(function () {
//            $(".btn_Export").click(function () {
//                var scrollHeight = document.documentElement.scrollHeight;
//                $(".shadowBj").height(scrollHeight);
//                $(".shadowBj").show();
//                $(".loadfile").show();

//                $.post("ExportFile.ashx", {
//                    ExportType: "StuTraining",
//                    RealName: $("#<%=txtName.ClientID %>").val(),
//                    WorkerNo: $("#<%=txtWorkerNo.ClientID %>").val(),
//                    Department: $("#<%=txtDepartment.ClientID %>").val(),
//                    Post: $("#<%=txtPost.ClientID %>").val(),
//                    BeginTime: $("#<%=txtBeginTime.ClientID %>").val(),
//                    EndTime: $("#<%=txtEndTime.ClientID %>").val()
//                }, function (data) {
//                    if (data != "") {
//                        window.location = data;
//                    }
//                    $(".shadowBj").hide();
//                    $(".loadfile").hide();
//                });

//            });
//        });
//        function hideLodfileDiv() {
//            $(".shadowBj").hide();
//            $(".loadfile").hide();
//        }
    </script>
</asp:Content>
