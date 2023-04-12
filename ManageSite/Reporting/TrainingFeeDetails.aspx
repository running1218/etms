<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="TrainingFeeDetails.aspx.cs"
    MasterPageFile="~/MasterPages/MReport.master" Inherits="TrainingFeeDetails" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc2" %>
<asp:Content ID="content2" ContentPlaceHolderID="TitleContentPlaceHolder" runat="server">
    <title>培训项目费用明细</title>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1" runat="server">
    <div class="dv_HeaderTitle">
        <h2 class="h_titleName3">
            培训项目费用明细</h2>
    </div>
    <div class="dv_reports">
        <div class="dv_searchbox">
            <table id="tableQueryControlList" class="GridviewGray " border="0" cellpadding="0" cellspacing="0" runat="server">
                <tr>
                    <th style="width: 70px;">
                        培训项目：
                    </th>
                    <td>
                        <asp:TextBox ID="txt_f999ItemName" runat="server" CssClass="inputbox_120 floatleft"
                            MaxLength="50"></asp:TextBox>
                    </td>
                    <th style="width: 70px;">
                        讲师姓名：
                    </th>
                    <td>
                        <asp:TextBox ID="txt_d999RealName" runat="server" CssClass="inputbox_120 floatleft"
                            MaxLength="50"></asp:TextBox>
                    </td>
                    <th style="width: 70px;">
                        课程名称：
                    </th>
                    <td>
                        <asp:TextBox ID="txt_g999CourseName" runat="server" CssClass="inputbox_120 floatleft"
                            MaxLength="50"></asp:TextBox>
                        <asp:Button ID="btnSearch" CssClass="btn_Search" runat="server" Text="查询" OnClick="btnSearch_Click" />
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
                        <asp:TemplateField HeaderText="项目编码" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft field17">
                            <ItemTemplate>
                                <asp:Label ID="lblItemCode" runat="server" Text='<%# Eval("ItemCode")%>'></asp:Label>                                
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="项目名称" ItemStyle-CssClass="alignleft" HeaderStyle-CssClass="alignleft">
                            <ItemTemplate>
                                <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ItemName")%>'></asp:Label>                                
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="课程编码" HeaderStyle-Width="100">
                            <ItemTemplate>
                                <asp:Label ID="lblCourseCode" runat="server" Text='<%# Eval("CourseCode")%>'></asp:Label>   
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="课程名称">
                            <ItemTemplate>
                                <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("CourseName")%>'></asp:Label>   
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="培训日期" HeaderStyle-Width="85">
                            <ItemTemplate>
                                <asp:Label ID="lblTrainingDate" runat="server" Text='<%# Eval("TrainingDate").ToDate()%>'></asp:Label>   
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="培训时间" HeaderStyle-Width="90">
                            <ItemTemplate>
                                <asp:Label ID="lblBeginTime" runat="server" Text='<%# Eval("TrainingBeginTime").ToDateTime().ToShortTimeString() + " - " + Eval("TrainingEndTime").ToDateTime().ToShortTimeString() %>'></asp:Label>                                  
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="讲师" HeaderStyle-Width="90">
                            <ItemTemplate>
                                <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("TeacherName")%>'></asp:Label>   
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="课时数" HeaderStyle-Width="90">
                            <ItemTemplate>
                                <asp:Label ID="lblRealCourseHours" runat="server" Text='<%# Eval("RealCourseHours") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="课酬标准" HeaderStyle-Width="90">
                            <ItemTemplate>
                                <asp:Label ID="lblExamNum" runat="server" ShowTextNum="6" Text='<%# string.Format("{0:N2}", Eval("CourseFee").ToString().ToDecimal())%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="课酬" HeaderStyle-Width="90">
                            <ItemTemplate>
                                <asp:Label ID="lblAvgExam" runat="server" ShowTextNum="6" Text='<%# decimal.Round(Eval("RealCourseFee").ToString().ToDecimal() , 2).ToString() %>'></asp:Label>
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
