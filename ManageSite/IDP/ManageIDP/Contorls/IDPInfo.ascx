<%@ Control Language="C#" AutoEventWireup="true" CodeFile="IDPInfo.ascx.cs" Inherits="IDP_ManageIDP_Contorls_IDPInfo" %>
<div class="dv_pageInformation">
    <table class="GridviewGray">
      
        <tr>
            <th width="140" style="text-align:right;">
                学员姓名：
            </th>
            <td>
                <asp:Literal ID="lblStudentName" runat="server"></asp:Literal>
            </td>
            <th width="140" style="text-align:right;">
                计划周期：
            </th>
            <td>
            <asp:Literal ID="lblDate" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <th style="text-align:right;">
                直接上级姓名：
            </th>
            <td>
                <asp:Literal ID="lblLeaders" runat="server"></asp:Literal>
            </td>
            <th style="text-align:right;">
                上级<asp:Literal ID="ltlRank" runat="server" Text="<%$ Resources:UIResource, ui_rank%>"></asp:Literal>
            </th>
            <td>
            <asp:Literal ID="lblLeadersRank" runat="server"></asp:Literal>
                
            </td>
        </tr>
        <tr>
            <th style="text-align:right;">
                创建日期：
            </th>
            <td>
                <asp:Literal ID="lblCreateTime" runat="server"></asp:Literal>
            </td>
            <th style="text-align:right;">
                关闭日期：
            </th>
            <td>
                <asp:Literal ID="lblCloseTime" runat="server"></asp:Literal>
            </td>
            
        </tr>
        <tr>
            <th style="text-align:right;">
                学习计划完成率：
            </th>
            <td colspan="3">
                <asp:Literal ID="lblCompletionRate" runat="server"></asp:Literal>
            </td>
            
        </tr>
        <tr>
            <th style="text-align:right;">
                评　　价：
            </th>
            <td colspan="3">
                <asp:Literal ID="lblEvaluation" runat="server"></asp:Literal>
            </td>
            
        </tr>
    </table>
    
</div>