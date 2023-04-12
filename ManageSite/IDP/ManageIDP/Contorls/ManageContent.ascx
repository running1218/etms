<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ManageContent.ascx.cs" Inherits="IDP_ManageIDP_Contorls_ManageContent" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
 <div class="dv_searchlist">
 <table class="GridviewGray">
        <tr>
            <th style="text-align: left;">
                学习内容
            </th>
            <th style="text-align:left">
                学习方式
            </th>
            <th style="text-align: center;" width="100">
                计划完成时间
            </th>
            <th style="text-align: center;" width="120">
                学习内容完成情况
            </th>
    
        </tr>
   <asp:Repeater ID="rptList" runat="server">
            <ItemTemplate>      
        <tr>
            <td style="text-align:left;">
                <cc1:ShortTextLabel ID="lblStudyContent" runat="server" ShowTextNum="6" Text='<%# Eval("StudyContent")%>'></cc1:ShortTextLabel>
            </td>
            <td style="text-align:left;">
                <cc1:ShortTextLabel ID="lblStudyMode" runat="server" ShowTextNum="6" Text='<%# Eval("StudyMode")%>'></cc1:ShortTextLabel>
            </td>
            <td style="text-align: center;">
                <%# Eval("PlanFinishingTime").ToDate()%>
            </td>
            <td style="text-align: center;">
                 <cc1:DictionaryLabel ID="lblFinishedState" DictionaryType="Dic_FinishedState"  FieldIDValue='<%# Eval("FinishedState") %>' runat="server" />
            </td>
        </tr>
     
      </ItemTemplate>
        </asp:Repeater>
    </table>
    <div class="dv_splitLine"> </div>
    </div>