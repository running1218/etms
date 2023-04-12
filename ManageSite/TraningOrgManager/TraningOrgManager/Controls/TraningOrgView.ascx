<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TraningOrgView.ascx.cs"
    Inherits="TraningOrgManager_TraningOrgManager_Controls_TraningOrgView" %>
<div class="dv_information">
    <table class="GridviewGray  fixedTable">
        <tr>
            <th >
                机构名称：
            </th>
            <td colspan="3">
                <asp:Label ID="lblOuterOrgName" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    <table class="GridviewGray  fixedTable">
        <tr>
            <th >
                机构编码：
            </th>
            <td >
                <asp:Label ID="lblOuterOrgCode" runat="server"></asp:Label>
            </td>
            <th >
                联&nbsp;&nbsp;系&nbsp;&nbsp;人：
            </th>
            <td >
                 <asp:Label ID="lblLinkMan" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <th>
                邮&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;箱：
            </th>
            <td>
                <asp:Label ID="lblEMAIL" runat="server"></asp:Label>
            </td>
            <th>
                联系方式：
            </th>
            <td>
                <asp:Label ID="lblLinkMode" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    <table class="GridviewGray  fixedTable">
        <tr>
           <th>
               状&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;态：
            </th>
            <td colspan="3">
                <asp:Label ID="lblStatus" runat="server" ></asp:Label>
            </td>            
        </tr>
         <tr>
           <th>
                公司地址：
            </th>
            <td colspan="3">
                <asp:Label ID="lblAddress" runat="server" ></asp:Label>
            </td>            
        </tr>
        <tr>
           <th>
                公司网址：
            </th>
            <td colspan="3">
                <asp:Literal ID="lblHttp" runat="server" />
            </td>
        </tr>
        <tr>
            <th>
                服务内容：
            </th>
            <td colspan="3">
                <asp:Literal ID="lblServiceContent" runat="server" />
            </td>
        </tr>
        <tr>
            <th>
                精品课程：
            </th>
            <td colspan="3">
                <asp:Literal ID="lblBestClass" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <th>
                机构评价：
            </th>
            <td colspan="3">
               <asp:Literal ID="lblOrgAssess" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <th>
                历史合作：
            </th>
            <td colspan="3">
                 <asp:Literal ID="lblHistoryCooperation" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <th>
                合同模板：
            </th>
            <td colspan="3">
                 <asp:HyperLink ID="hlContractModal" runat="server"/>
            </td>
        </tr>
        <tr>
            <th>
                备注：
            </th>
            <td colspan="3">
                <asp:Literal ID="lblRemark" runat="server" ></asp:Literal>
            </td>
        </tr>
    </table>
</div>
<div class="dv_submit">
    <input value="关闭" type="button" onclick="javascript:closeWindow();" class="btn_Close"></div>
