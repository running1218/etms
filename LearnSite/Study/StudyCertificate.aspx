<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Study.master" AutoEventWireup="true" CodeBehind="StudyCertificate.aspx.cs" Inherits="ETMS.Studying.Study.StudyCertificate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="StudyPlaceHolder" runat="server">
    <link href="<%=ETMS.Utility.WebUtility.AppPath%>/Styles/Certificate.css" type="text/css" rel="stylesheet" />
    <div class="view-area">
        <div class="resources_list" id="CertificateDiv" runat="server">
            <div>
                <asp:Image ID="Logo" runat="server" CssClass="c_logo" />
                <%--<image src="http://10.96.142.74:8010/files/OrgLogo/2017/02/23/20170223165355243.png" class="c_logo"></image>--%>
            </div>
            <div class="c_coursename">
                《<asp:Literal ID="CourseName" runat="server"></asp:Literal>》
            </div>
            <div class="c_username">
                <asp:Literal ID="StudyUserName" runat="server"></asp:Literal>
            </div>
            <div class="c_c1">
                恭喜您于<span><asp:Literal ID="StartDate" runat="server"></asp:Literal></span>至<span><asp:Literal ID="EndDate" runat="server"></asp:Literal></span>顺利完成《<asp:Literal ID="SmallCouresName" runat="server"></asp:Literal>》的学习
            </div>
            <div class="c_c2">
                <asp:Literal ID="OrganizationName" runat="server"></asp:Literal>
            </div>
            <div class="c_c3">
                <asp:Literal ID="FinalDate" runat="server"></asp:Literal>
            </div>
        </div>
        <div id="AlterDiv" runat="server" class="c_c4">
            完成课程100%学习后，才会获得证书，请继续学习！
        </div>
    </div>
    <script>
        $('.study_modular li').removeClass('cur').eq(7).addClass('cur');

    </script>
</asp:Content>
