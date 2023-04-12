<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Default.master" AutoEventWireup="true" CodeBehind="Certificate.aspx.cs" Inherits="ETMS.Studying.Self.Certificate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="DefaultPlaceHolder" runat="server">
    <link href="<%=ETMS.Utility.WebUtility.AppPath%>/Styles/Certificate.css" type="text/css" rel="stylesheet" />
    <link href="<%=ETMS.Utility.WebUtility.AppPath%>/Styles/default.css" type="text/css" rel="stylesheet" />

    <div class="view-area">
        <div class="study-history">
            <div class="block archives">
                <h1 class="tit">结课证书<a class="study-history-back" href="<%=WebUtility.AppPath %>/Self/MyHistory.aspx">返回</a></h1>
            </div>
        </div>
        <div class="resources_list" id="CertificateDiv" runat="server">
            <div>
                <asp:Image ID="Logo" runat="server" CssClass="c_logo" />
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
    </div>
</asp:Content>
