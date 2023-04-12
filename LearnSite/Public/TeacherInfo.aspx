<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Default.master" AutoEventWireup="true" CodeBehind="TeacherInfo.aspx.cs" Inherits="ETMS.Studying.Public.TeacherInfo" %>

<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Import Namespace="ETMS.Utility" %>
<%@ Import Namespace="ETMS.Utility.Service.FileUpload" %>
<asp:Content ID="Content1" ContentPlaceHolderID="DefaultPlaceHolder" runat="server">
    <link href="<%=ETMS.Utility.WebUtility.AppPath%>/Styles/default.css" type="text/css" rel="stylesheet" />
    <div class="banner-bg">
        <dl>
            <dt>
                <img src="<%= TeacherPhotoUrl %>"></dt>
            <dd><%= TeacherName %><span><%= TeacherLevelName %></span></dd>
        </dl>
    </div>
    <div class="view-area">
        <div class="teacherInfo-container">
            <!--简介-->
            <div class="infoBox">
                <h1><i class="circle"></i>简介</h1>
                <p>
                    <asp:Literal ID="lblBrife" runat="server" />
                </p>
            </div>
            <!--工作履历-->
            <div class="infoBox">
                <h1><i class="circle"></i>工作履历</h1>
                <p>
                    <asp:Literal ID="lblWorkExperience" runat="server" />
                </p>
            </div>
            <!--专长-->
            <div class="infoBox">
                <h1><i class="circle"></i>专长</h1>
                <p>
                    <asp:Literal ID="lblExpertise" runat="server" />
                </p>
            </div>
            <!--相关课程-->
            <div class="infoBox">
                <h1><i class="circle"></i>相关课程</h1>
                <div class="course-list">
                    <div class="list-block cur-block">

                        <asp:Repeater ID="TeacherDataList" runat="server">
                            <ItemTemplate>
                                <dl id="<%# Eval("CourseID")%>">
                                    <dt>
                                        <img src="<%# StaticResourceUtility.GetFullPathByFileType(FileUploadFunctionType.CourseLogo, string.IsNullOrEmpty(Eval("ThumbnailURL").ToString())?"default.jpg":Eval("ThumbnailURL").ToString())%>">
                                    </dt>
                                    <dd>
                                        <p class="h1 ellipsis"><%# Eval("CourseName")%></p>
                                    </dd>
                                </dl>
                            </ItemTemplate>
                        </asp:Repeater>

                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>
        $('.header-menu #menu-teacher').addClass('cur').siblings().removeClass('cur');
        $(".course-list .list-block dl").on("click", function () {
            window.location.href = "CourseView.aspx?courseid=" + $(this).attr("id");
        })
    </script>
</asp:Content>
