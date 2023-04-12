<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DemandCourse.ascx.cs" Inherits="ETMS.Studying.Controls.DemandCourse" %>

<%@ Import Namespace="ETMS.Utility" %>
<%@ Import Namespace="ETMS.Utility.Service.FileUpload" %>

<asp:Repeater ID="CourseDataList" runat="server">
    <ItemTemplate>
        <dl id="<%#Eval("CourseID")%>">
            <dt>
                <img src="<%# StaticResourceUtility.GetFullPathByFileType(FileUploadFunctionType.CourseLogo, string.IsNullOrEmpty(Eval("ThumbnailURL").ToString())?"default.jpg":Eval("ThumbnailURL").ToString())%>">
            </dt>
            <dd>
                <p class="h1 ellipsis"><%#Eval("CourseName")%></p>
                <p><i><%#Eval("TeacherName")%></i>教师</p>
                <p>
                    <i><%#Eval("CourseHours")%></i>课时
                    <span><i><%#Eval("FocusCount")%></i>学习</span>
                </p>
            </dd>
        </dl>
    </ItemTemplate>
</asp:Repeater>
<script>
    /*跳转名师详情*/
    $("#DemandCourseList").on("click","dl",function () {
        console.log($(this).attr("id"));
        window.location.href = "Public/CourseView.aspx?courseid=" + $(this).attr("id");
    })
</script>
