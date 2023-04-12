<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LeftChapterTree.ascx.cs" Inherits="ETMS.Studying.Controls.Course.LeftChapterTree" %>
<div class="Notes-box">
    <ul>
        <li class="all">全部</li>
        <asp:Repeater ID="ChapterTree" runat="server">
            <ItemTemplate>
                <li id="<%# Eval("ContentID") %>">
                    <p><%# Eval("Type").ToString() == "1"?"<i class='video_icon'></i>":"<i class='file_icon'></i>"%><%# Eval("Name") %></p>
                </li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
</div>
