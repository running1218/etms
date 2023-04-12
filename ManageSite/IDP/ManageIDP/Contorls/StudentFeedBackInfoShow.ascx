<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StudentFeedBackInfoShow.ascx.cs" Inherits="IDP_ManageIDP_Contorls_StudentFeedBackInfoShow" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>

<div class="dv_pageInformation">
    <table class="GridviewGray th160">
        <tr>
            <th width="150" style="text-align:right;">
                计划发展能力：
            </th>
            <td style="vertical-align:top; text-align:left;">
               <asp:Literal ID="ltlPlanDevelopment" runat="server"></asp:Literal>
            </td>
              </tr>
        <tr>
            <th style="text-align:right;">
                能力现状：
            </th>
            <td style="vertical-align:top; text-align:left;">
                <asp:Literal ID="ltlAbility" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <th  style="text-align:right;">
                希望达到的水平：
            </th>
            <td style="vertical-align:top; text-align:left;">
                <asp:Literal ID="ltlHopeLevel" runat="server"></asp:Literal>
            </td>
              </tr>
        <tr>
            <th style="text-align:right;">
                直接上级建议和意见：
            </th>
            <td style="vertical-align:top; text-align:left;">
            <asp:Literal ID="ltlSuperiorEvaluation" runat="server"></asp:Literal>
            </td>
        </tr>
      
    </table>
</div>
