<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MyIDPFeedBackStu.ascx.cs" Inherits="IDP_ManageIDP_Contorls_MyIDPFeedBackStu" %>
<%@ Register Src="~/Controls/PageSet.ascx" TagName="PageSet" TagPrefix="uc1" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<table class="GridviewGray">
    <tr>
        <th style="text-align: left;">
            学员自我评价
        </th>
        <th width="120" style="text-align: center;">
            学员评价日期
        </th>
        <th style="text-align: left;">
            上级反馈和评价
        </th>
        <th width="120" style="text-align: center;">
            上级反馈评价日期
        </th>

    </tr>
    <asp:Repeater ID="rptList" runat="server">
        <ItemTemplate>           
            <tr>
                <td style="text-align: left;">
                 <cc1:ShortTextLabel ID="lblStudentEvaluation" runat="server" ShowTextNum="6" Text='<%# Eval("StudentEvaluation")%>'></cc1:ShortTextLabel>
                </td>
                <td style="text-align: center;">
                    <%# Eval("StudentEvaluationTime").ToDate()%>
                </td>
                <td style="text-align: left;">
                    <cc1:ShortTextLabel ID="lblSuperiorEvaluation" runat="server" ShowTextNum="6" Text='<%# Eval("SuperiorEvaluation")%>'></cc1:ShortTextLabel>
                </td>
                <td style="text-align: center;">
                    <%# Eval("SuperiorEvaluationTime").ToDateTime().Year ==1 ? "" : Eval("SuperiorEvaluationTime").ToDate()%>
                </td>
   
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
<div id="divNoResult"  runat="server" visible="false">
        <table id="divPicture" runat="server">
            <tr>
                <td width="250">
                    <div class="dv_nodatap">
                    </div>
                </td>
                <td valign="middle" class="linkstudy">
                    没有任务记录！<br />
                </td>
            </tr>
        </table>
 </div>
<div class="dv_splitLine"> </div>
 <div class="dv_pagePanel">
        <div class="dv_pageControl">
            <uc1:PageSet ID="psPager" runat="server" />
        </div>
</div>

