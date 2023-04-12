<%@ Control Language="C#" AutoEventWireup="true" CodeFile="IDPInfoShow.ascx.cs" Inherits="IDP_ManageIDP_Contorls_IDPInfoShow" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<div class="dv_pageInformation">
    <%--<a class="dv_expandInfor" href="javascript:void(0)" onfocus="blur()">展开</a>   --%>    
    <table class="GridviewGray">
        <tr>
            <th width="150">
                学员姓名：
            </th>
            <td>
                <asp:Literal ID="lblStudentName" runat="server"></asp:Literal>
            </td>
            <th width="150">
                计划周期：
            </th>
            <td>
            <asp:Literal ID="lblDate" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <th>
                直接上级姓名：
            </th>
            <td>
                <asp:Literal ID="lblLeaders" runat="server"></asp:Literal>
            </td>
            <th>
                上级职务：
            </th>
            <td>
            <asp:Literal ID="lblLeadersRank" runat="server"></asp:Literal>
                
            </td>
        </tr>
         <tr>
            <th>
                创建日期：
            </th>
            <td colspan="3">
                <asp:Literal ID="ltlCreateTime" runat="server"></asp:Literal>
            </td>
          
        </tr>
    </table>
</div>
<script type="text/javascript">
    $(function () {
        $(".dv_IntroudceList:first table").find("tr:gt(0)").show();
        if ($(".dv_IntroudceList").length > 1)
            $(".dv_IntroudceList:last").hide();
        $(".dv_expandInfor").toggle(function () {
            $(this).text("隐藏");
            $(this).addClass("dv_hideInfor");
            $(".dv_IntroudceList:last").show();
            $(".dv_IntroudceList:first table").find("tr:gt(0)").show();
        }, function () {
            $(this).text("展开");
            $(this).removeClass("dv_hideInfor");
            $(".dv_IntroudceList:first table").find("tr:gt(0)").hide();
            if ($(".dv_IntroudceList").length > 1)
                $(".dv_IntroudceList:last").hide();
        })
    })
    </script>