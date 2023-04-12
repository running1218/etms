<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ClassRoomInfo.ascx.cs"
    Inherits="ETMS.WebApp.Manage.Resource.ClassRoomManage.Controls.ClassRoomInfo" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<!--表单录入-->
<cc1:CustomMuliView runat="server" ID="Views">    
    <asp:View runat="server" ID="View_Browse">
        <div class="dv_information">
            <table class="GridviewGray fixedTable">                
                <tr>
                    <th width="100">
                        教室编码：
                    </th>
                    <td colspan="3">
                        <asp:Label ID="lblClassRoomCode" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th>
                        教室名称：
                    </th>
                    <td colspan="3">
                        <asp:Literal ID="lblClassRoomName" runat="server" />
                    </td>
                </tr>
            </table>
            <table class="GridviewGray fixedTable"> 
                <tr>
                    <th>
                        教室状态：
                    </th>
                    <td>
                        <asp:Label ID="lblClassRoomStatus" runat="server"></asp:Label>
                    </td>
                    <th width="100">
                        容&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;量：
                    </th>
                    <td>
                        <asp:Label ID="lblCapacity" runat="server"></asp:Label>
                    </td>
                </tr>               
                <tr>
                    <th>
                        负&nbsp;&nbsp;责&nbsp;&nbsp;人：
                    </th>
                    <td>
                        <asp:Label ID="lblDutyUser" runat="server"></asp:Label>
                    </td>
                     <th>
                        联系电话：
                    </th>
                    <td>
                        <asp:Label ID="lblPhone" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            <table class="GridviewGray fixedTable"> 
                <tr>
                   <th>价&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;格：</th>
                   <td  colspan="3"><asp:Label ID="lblPrice" runat="server"></asp:Label></td>
                </tr>
                 <tr>
                    <th>
                        教室用途：
                    </th>
                    <td colspan="3">
                        <asp:Literal ID="lblClassRoomPurpose" runat="server"/>
                    </td>
                </tr>               
                <tr>
                    <th>
                        地&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;址：
                    </th>
                    <td colspan="3">
                        <asp:Literal ID="lblAddress" runat="server"/>
                    </td>
                </tr>
                <tr>
                    <th>
                        教室说明：
                    </th>
                    <td colspan="3">
                        <asp:Literal ID="lblDescription" runat="server"/>
                    </td>
                </tr>
                <tr>
                    <th>
                        管理制度：
                    </th>
                    <td colspan="3">
                        <asp:Literal ID="lblRegulations" runat="server"/>
                    </td>
                </tr>
            </table>
            <table class="GridviewGray fixedTable"> 
                <tr>
                    <th>
                        创建时间：
                    </th>
                    <td>
                        <asp:Label ID="lblCreateTime" runat="server"></asp:Label>
                    </td>
                    <th width="100">
                        创&nbsp;&nbsp;建&nbsp;&nbsp;人：
                    </th>
                    <td>
                        <asp:Label ID="lblCreateUser" runat="server"></asp:Label>
                    </td>
                </tr>              
              
                <tr>
                    <th>
                        修改时间：
                    </th>
                    <td>
                        <asp:Label ID="lblModifyTime" runat="server"></asp:Label>
                    </td>
                    <th>
                        修&nbsp;&nbsp;改&nbsp;&nbsp;人：
                    </th>
                    <td>
                        <asp:Label ID="lblModifyUser" runat="server"></asp:Label>
                    </td>
                </tr>                
               
            </table>
        </div>
    </asp:View>
</cc1:CustomMuliView>
<%--<!--提交表单-->
<div class="dv_submit">
    <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn_Save" OnClick="LinkButton1_Click">保存</asp:LinkButton>
    <a href="javascript:closeWindow();" class="btn_Cancel padleft10">取消</a>
</div>
--%>