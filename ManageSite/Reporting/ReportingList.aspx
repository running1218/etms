<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MPageAdmin.Master"
    AutoEventWireup="true" CodeFile="ReportingList.aspx.cs" Inherits="Reporting_ReportingList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBack" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="dv_ImgReportlist">
        <a href="StuTrainingDetails.aspx" class="imgReport_1" target="_blank"><span class="text_title">
            学员培训明细表</span></a> <a href="StuTrainingSummary.aspx" class="imgReport_2" target="_blank">
                <span class="text_title">学员培训汇总表</span></a> <a href="OrgTrainingSummary.aspx" class="imgReport_3"
                    target="_blank"><span class="text_title">机构培训汇总表</span></a> <a href="OnlineStudyCourseDetail.aspx"
                        class="imgReport_4" target="_blank"><span class="text_title">在线学习情况监控</span></a>
        <a href="TraningCourseLearnDetail.aspx" class="imgReport_5" target="_blank"><span
            class="text_title">培训课程学习情况汇总</span></a> <a href="StuRegisterStatistical.aspx"
                class="imgReport_2" target="_blank"><span class="text_title">公司注册人数汇总</span></a>

                <a href="OrderList.aspx"
                class="imgReport_2" target="_blank"><span class="text_title">订单查询</span></a>
                <a href="TrainingFeeDetails.aspx"
                class="imgReport_2" target="_blank"><span class="text_title">培训项目费用明细</span></a>
    </div>
</asp:Content>
 