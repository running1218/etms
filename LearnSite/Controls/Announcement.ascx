<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Announcement.ascx.cs" Inherits="ETMS.Studying.Controls.Announcement" %>

<%@ Import Namespace="ETMS.Utility" %>
<%@ Import Namespace="ETMS.Utility.Service.FileUpload" %>

<div class="announcement-block">
    <asp:Repeater ID="rptAnnouncementList1" runat="server">
        <ItemTemplate>
            <dl class="announcement-cont toDetail" id="<%# Eval("ArticleID") %>">
                <dd>
                    <p class="newIcon">new</p>
                    <p class="h1 ellipsis"><%# Eval("MainHead") %></p>
                    <p class="time"><%# Eval("CreateTime").ToDateTime().ToString("yyyy-MM-dd") %></p>
                    <p class="text"><%# StringUtility.StripHTML(Eval("ArticleContent").ToString()) %></p>
                </dd>
                <dt>
                    <img src="<%# StaticResourceUtility.GetFullPathByFileType(FileUploadFunctionType.BulletinImage, string.IsNullOrEmpty(Eval("ImageUrl").ToString())?"default.png":Eval("ImageUrl").ToString())%>"></dt>
            </dl>
        </ItemTemplate>
    </asp:Repeater>
    <dl class="announcement-cont">
        <dd>
            <%--<span class="newIcon">new</span>--%>
            <asp:Repeater ID="rptAnnouncementList2" runat="server">
                <ItemTemplate>
                    <p class="text toDetail" id="<%# Eval("ArticleID") %>">
                        <span><%# Eval("MainHead") %></span>
                        <i><%# Eval("CreateTime").ToDateTime().ToString("yyyy-MM-dd") %></i>
                    </p>
                </ItemTemplate>
            </asp:Repeater>
        </dd>
    </dl>
</div>
<script>
    /*跳转日常公告详情*/
    $(".announcement-block .toDetail").on("click", function () {
        window.open("Public/AnnouncementView.aspx?ArticleID=" + $(this).attr("id"), '_blank');
    })
</script>