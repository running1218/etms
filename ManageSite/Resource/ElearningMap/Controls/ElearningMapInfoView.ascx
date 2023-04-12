<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ElearningMapInfoView.ascx.cs"
    Inherits="Resource_ElearningMap_Controls_ElearningMapInfoView" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<div class="dv_pageInformation">
    <table class="GridviewGray">
        <tr>
            <th style="width: 120px; text-align: right;">
                学习地图编码：
            </th>
            <td style="width: 250px;">
                <asp:Literal ID="ltlStudyMapCode" runat="server"></asp:Literal>
            </td>
            <th style="width: 120px; text-align: right;">
                学习地图名称：
            </th>
            <td>
                <asp:Literal ID="ltlStudyMapName" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <th style="text-align: right;">
                <% if (StudyMapType == "1") {%>岗位、职级：<%}%>
                <% if (StudyMapType == "2"){%>部门、职级：<%}%>       
                <% if (StudyMapType == "3"){%>部门、岗位、职级：<%}%> 
            </th>
            <td colspan="3">
                <% if (StudyMapType == "2" || StudyMapType == "3")
                   {%>
                    <b>
                        <asp:Literal ID="ttDepartment" runat="server" Text="部门："></asp:Literal>
                    </b>
                    <cc1:DictionaryLabel ID="lblDepartment" DictionaryType="Site_DepartmentByOrgID" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;
                    <%} 
                %>
                <% if (StudyMapType == "1" || StudyMapType == "3")
                   { %>
                        <b><asp:Literal ID="ttPost" runat="server" Text="岗位：" /></b>
                        <cc1:DictionaryLabel ID="lblPost" DictionaryType="Dic_PostByOrgID" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;
                    <% }
                %>
                <b>
                    <asp:Literal ID="ttRank" runat="server" Text="职级：" /></b><cc1:DictionaryLabel ID="lblRank"
                        DictionaryType="vw_Dic_Sys_Rank" runat="server" />
            </td>
        </tr>
        <tr id="trStudyMapDesc"  runat="server">
            <th  style="text-align: right;">
                能力描述：
            </th>
            <td colspan="3" class="alignleft">
                <asp:Literal ID="ltlStudyMapDesc" runat="server"></asp:Literal>
            </td>
        </tr>
    </table>
</div>
