<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ExerciseView.ascx.cs"
    Inherits="QuestionDB_Controls_ExerciseView" %>
<!--功能标题-->
<h2 class="dv_title">
    <asp:Literal ID="Literal99" runat="server"></asp:Literal>
</h2>
<!--表单录入-->
<div class="dv_information">
    <table class="GridviewGray">
        <tr>
            <th style="width:15%;">
                课程名称：
            </th>
            <td colspan="3" style="width:85%;">
                <asp:Literal ID="Literal9" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <th>
                <asp:Literal ID="Literal2" runat="server"></asp:Literal>：
            </th>
             <td colspan="3">
                <asp:Literal ID="Literal1" runat="server"></asp:Literal>
            </td>
            
        </tr>
        <tr>
            <th style="vertical-align: top">
                描　　述：
            </th>
            <td colspan="3" style="height:50px; vertical-align:top;">
     
                <asp:Literal ID="Literal3" runat="server"></asp:Literal>
     
            </td>
        </tr>
        <tr>
            <th style="width:15%;">
                显示答案：
            </th>
            <td style="width:35%;">
                <asp:Literal ID="Literal8" runat="server"></asp:Literal>
            </td>
            <th style="width:15%;">
                及格分数：
            </th>
            <td style="width:35%;">
                <asp:Literal ID="Literal5" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <th>
                题　　目：
            </th>
            <td colspan="3">
                <a href="<%=ETMS.Utility.WebUtility.AppPath %>/QuestionDB/Testpaper/TestpaperView.aspx" target="_blank">查看题目</a>
            </td>
            
        </tr>
        <tr>
            <th>
                创建时间：
            </th>
            <td>
                <asp:Literal ID="Literal12" runat="server"></asp:Literal>
            </td>
            <th>
                创 建 者：
            </th>
            <td>
                <asp:Literal ID="Literal13" runat="server"></asp:Literal>
            </td>
        </tr>
    </table>
    
</div>
<!--提交表单-->
    <div class="dv_submit">
        <a href="javascript:closeWindow();" class="btn_Close">关闭</a>
    </div>