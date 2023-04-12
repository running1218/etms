<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Study.master" AutoEventWireup="true" CodeBehind="CourseStudy.aspx.cs" Inherits="ETMS.Studying.Study.CourseStudy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="StudyPlaceHolder" runat="server">
    <link href="<%=ETMS.Utility.WebUtility.AppPath%>/Styles/courseStudy.css" type="text/css" rel="stylesheet" />
    <asp:Repeater ID="ResourceList" runat="server">
        <HeaderTemplate>
            <ul class="resources_list">
        </HeaderTemplate>
        <ItemTemplate>
            <li class="<%# GetResourceStatus(Eval("StudyStatus").ToString()) %>">
                <i class="status_icon"></i>
                <div class="resources_info">
                    <p><%# Eval("Name") %></p>
                    <p><%# Eval("TeacherName") %></p>
                    <p><%# GetTime(int.Parse(Eval("Type").ToString()),int.Parse(Eval("VideoTime").ToString()),int.Parse(Eval("PlayTime").ToString())) %></p>
                    <p><a href="../Study/StudyDetail.aspx?contentID=<%# Eval("ContentID") %>&TrainingItemCourseID=<%=TrainingItemCourseID %>&ContentType=<%# Eval("Type") %>">学习</a></p>
                </div>
            </li>
        </ItemTemplate>
        <FooterTemplate>
            </ul>
        </FooterTemplate>
    </asp:Repeater>
    <asp:Repeater ID="rptLiving" Visible="false" runat="server">
        <HeaderTemplate>
            <ul class="resources_list">
                </HeaderTemplate>
                <ItemTemplate>
                    <li class="<%# GetResourceStatus(Eval("StyStatus").ToString()) %>">
                        <i class="status_icon"></i>
                        <div class="resources_info">
                            <p><%# Eval("LivingName") %></p>
                            <p><%# Eval("TeacherName") %></p>
                            <p><%# string.Format("{0} {1} - {2}",Eval("Date"), Eval("SHHMM"), Eval("EHHMM")) %></p>
                            <p><a target="_blank" href="<%# string.Format("{0}/public/LivingGuide.aspx?LivingID={1}", WebUtility.AppPath, Eval("LivingID")) %>"><%# Eval("Flag").ToInt() == 0 ?"查看回放":"去上课" %></a></p>
                        </div>
                    </li>
                </ItemTemplate>
                <FooterTemplate>
            </ul>
        </FooterTemplate>
    </asp:Repeater>
    <script>
        if ($('.resources_list li').length == 0) {
            $('.no_content').removeClass('hide');
        }
    </script>
</asp:Content>
