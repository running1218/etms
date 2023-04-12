<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Default.master" AutoEventWireup="true" CodeBehind="TestResultList.aspx.cs" Inherits="ETMS.Studying.Study.TestResultList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="DefaultPlaceHolder" runat="server">
    <link href="<%=ETMS.Utility.WebUtility.AppPath%>/Styles/testResultList.css" type="text/css" rel="stylesheet" />
    <div class="view-area">
        <p class="title">考试成绩明细</p>
        <table border="0">
            <thead>
                <tr>
                    <td style="width:50%;text-align:left">在线测评名称</td>
                    <td style="width:30%;">考试时间</td>
                    <td style="width:10%;">成绩</td>
                    <td style="width:40%;">操作</td>
                </tr>
            </thead>
            <tbody>
                 <asp:Repeater ID="rptTestResultList" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td style="text-align:left"><%# Eval("OnLineTestName") %></td>
                            <td>  
                                <%# Eval("ExamTime").ToString() == "" ? "":Eval("ExamTime").ToDateTime().ToString("yyyy-MM-dd HH:mm:ss")%>
                            </td>
                            <td><%# Eval("Score") %></td>
                            <td><a target="_blank" href='<%# getUrl(Eval("UserExamID").ToGuid()) %>'>查看</a></td>
                        </tr>
                    </ItemTemplate>
                 </asp:Repeater>
            </tbody>
        </table>
    </div>
</asp:Content>
