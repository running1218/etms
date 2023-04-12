<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Study.master" AutoEventWireup="true" CodeBehind="CourseGuidance.aspx.cs" Inherits="ETMS.Studying.Study.CourseGuidance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="StudyPlaceHolder" runat="server">
    <style>
        .view-area {
            width: 1200px;
            margin: 0 auto;
        }

        .title {
            height: 40px;
            line-height: 40px;
            background: #f5f5f5;
            padding: 0 15px;
            margin-top: 30px;
        }

            .title h1 {
                float: left;
                font-size: 16px;
            }

            .title p {
                float: right;
            }

        .main_content p {
            line-height: 25px;
            margin-top: 20px;
            color: #747474;
        }
    </style>
    <div id="CourseGuidanceList">
        
    </div>
    <script src="<%=ETMS.Utility.WebUtility.AppPath%>/Scripts/CourseGuidance.js"></script>
    <script>
        $('.study_modular li').removeClass('cur').eq(2).addClass('cur');
    </script>
</asp:Content>
