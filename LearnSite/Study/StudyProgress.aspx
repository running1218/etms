<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Study.master" AutoEventWireup="true" CodeBehind="StudyProgress.aspx.cs" Inherits="ETMS.Studying.Study.StudyProgress" %>

<asp:Content ID="Content1" ContentPlaceHolderID="StudyPlaceHolder" runat="server">
    <link href="<%=ETMS.Utility.WebUtility.AppPath%>/Styles/progress.css" type="text/css" rel="stylesheet" />
    <script src="<%=ETMS.Utility.WebUtility.AppPath%>/Scripts/library/echarts/echarts.min.js"></script>
    <script src="<%=ETMS.Utility.WebUtility.AppPath%>/Scripts/studyProgress.js"></script>
    <div class="view-area">
        <div class="chart">
            <div id="chart1"></div>
            <div id="chart2"></div>
        </div>
        <div class="notes">
            <p>
                <img src="<%=ETMS.Utility.WebUtility.AppPath%>/Styles/images/common/all_icon.jpg" />
                全部
            </p>
            <p>
                <img src="<%=ETMS.Utility.WebUtility.AppPath%>/Styles/images/common/study_end.jpg" />
                已完成
            </p>
            <p>
                <img src="<%=ETMS.Utility.WebUtility.AppPath%>/Styles/images/common/studying.jpg" />
                未完成
            </p>
            <p>
                <img style="width: 22px" src="<%=ETMS.Utility.WebUtility.AppPath%>/Styles/images/common/circle.png" />
                未学习
            </p>
        </div>
        <div class="progress_list">
            <%-- 学习内容 --%>
            <div class="study_content">
                <i class="status_icon default" id="content_status"></i>
                <p class="title">
                    <img src="<%=ETMS.Utility.WebUtility.AppPath%>/Styles/images/common/content_icon.jpg" />
                    学习内容
                </p>
                <ul class="cont_list" id="content_list"></ul>
            </div>
            <%-- 测评 --%>
            <div class="study_content">
                <i class="status_icon default" id="test_job_status"></i>
                <p class="title">
                    <img src="<%=ETMS.Utility.WebUtility.AppPath%>/Styles/images/common/test_icon.jpg" />
                    测评
                </p>
                <ul class="cont_list" id="test_job_list"></ul>
            </div>
        </div>
    </div>
</asp:Content>
