<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Default.master" AutoEventWireup="true" CodeBehind="MyHistory.aspx.cs" Inherits="ETMS.Studying.Self.MyHistory" %>

<%@ Register Src="~/Controls/MiniUpFile.ascx" TagName="uploader" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="DefaultPlaceHolder" runat="server">
    <%--<script src="<%=ETMS.Utility.WebUtility.AppPath%>/Scripts/UserInfo.js"></script>--%>
    <link href="<%=ETMS.Utility.WebUtility.AppPath%>/Styles/default.css" type="text/css" rel="stylesheet" />
    <!--个人中心-->
    <div class="view-area">
        <div class="study-history">
            <!--学习档案-->
            <div class="block archives">
                <h1 class="tit">学习档案</h1>
                <asp:Repeater ID="ArchivesRepter" runat="server">
                    <HeaderTemplate>
                        <table border="0" cellpadding="0" cellspacing="0">
                            <thead>
                                <tr>
                                    <td style="width:20%;">名称</td>
                                    <td>起止时间</td>
                                    <td>所属项目</td>
                                    <td>成绩</td>
                                </tr>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td style="width:20%;"><%# Eval("ObjectName") %></td>
                            <td><%# Eval("BeginTime") %>至<%# Eval("EndTime") %></td>
                            <td><%# Eval("ItemName") %></td>
                            <td>
                                <%# Eval("Score") %>
                                <a href="<%# Eval("IsCompleted").ToInt() == 1? WebUtility.AppPath + "/Self/Certificate.aspx?TrainingItemCourseID=" + Eval("TrainingItemCourseID"):"#"%>" class="study-history-score <%# Eval("IsCompleted").ToInt() == 1? "study-completed":"study-uncompleted" %>"></a>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                            </tbody>
                    </table>
                    </FooterTemplate>
                </asp:Repeater>                   
            </div>
        </div>
    </div>
    <script>

    </script>
</asp:Content>
