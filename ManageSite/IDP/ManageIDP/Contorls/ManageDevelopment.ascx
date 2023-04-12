<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ManageDevelopment.ascx.cs" Inherits="IDP_ManageIDP_Contorls_ManageDevelopment" %>
    <%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
    <div class="dv_searchlist">
<table class="GridviewGray">
    <tr>
        <th style="text-align: left;">
            计划发展能力
        </th>
        <th style="text-align: left;">
            能力现状
        </th>
        <th style="text-align: left;">
            希望达到的水平
        </th>
        <th style="text-align: left;">
            上级建议与意见
        </th>
        <th width="80 style="text-align: center;">继续培养</th>
        <th width="90" style="text-align: center;">查看</th>
    </tr>
    <asp:Repeater ID="rptList" runat="server">
        <ItemTemplate>
            <tr>
                <td style="text-align: left;">
                    <cc1:ShortTextLabel ID="lblPlanDevelopment" runat="server" ShowTextNum="6" Text='<%# Eval("PlanDevelopment")%>'></cc1:ShortTextLabel>
                </td>
                <td style="text-align: left;">
                    <cc1:ShortTextLabel ID="lblAbility" runat="server" ShowTextNum="6" Text='<%# Eval("Ability")%>'></cc1:ShortTextLabel>
                </td>
                <td style="text-align: left;">
                     <cc1:ShortTextLabel ID="lblHopeLevel" runat="server" ShowTextNum="6" Text='<%# Eval("HopeLevel")%>'></cc1:ShortTextLabel>
                </td>
                <td style="text-align: left;">
                     <cc1:ShortTextLabel ID="lblSuperiorOpinion" runat="server" ShowTextNum="6" Text='<%# Eval("SuperiorOpinion")%>'></cc1:ShortTextLabel>
                </td>
                <td style="text-align: center;">
                     <cc1:DictionaryLabel ID="lblIsContinue" DictionaryType="Dic_TrueOrFalseBool" FieldIDValue='<%# Eval("IsContinue") %>' runat="server" />
                </td>
                <td style="text-align: center;">
                    <a href='<%# GetUrl3(Eval("IDPPlanObjectID").ToGuid()) %>'>查看跟进反馈</a>
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
<div class="dv_splitLine"> </div>
</div>