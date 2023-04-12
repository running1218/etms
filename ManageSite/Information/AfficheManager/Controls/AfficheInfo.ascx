<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AfficheInfo.ascx.cs" Inherits="Information_AfficheManager_Controls_AfficheInfo" %>
<%@ Register Assembly="ETMS.Editor" Namespace="ETMS.Editor.UEditor" TagPrefix="wuc" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<!--功能标题-->
<h2 class="dv_title">
    公告管理
</h2>
<div class="dv_information">
    <table class="GridviewGray">
        <tr>
            <th style="width:15%">
                公告标题：
            </th>
            <td  style="width:85%">
            <asp:TextBox ID="txtMainHead" runat="server" CssClass="inputbox_300" MaxLength="100"></asp:TextBox>
               <span style="color: Red;">*</span><asp:RequiredFieldValidator
                        ID="RequiredFieldValidatorJobName" runat="server" ErrorMessage="请填公告标题！" ControlToValidate="txtMainHead"
                        ValidationGroup="Error" Display="None"></asp:RequiredFieldValidator>   
            </td>
        </tr>
        <tr>
            <th>
                公告摘要：
            </th>
            <td>            
                 <asp:TextBox ID="TextBox2" runat="server" CssClass="inputbox_300" MaxLength="100"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>
                有 效 期：
            </th>
            <td>
                <cc1:DateTimeTextBox ID="dttbBeginTime" runat="server" CssClass="inputbox_120" EndTimeControlID="dttbEndTime"></cc1:DateTimeTextBox>
                        至
                <cc1:DateTimeTextBox ID="dttbEndTime" runat="server" CssClass="inputbox_120" BeginTimeControlID="dttbBeginTime"></cc1:DateTimeTextBox>
            </td>
        </tr>
        <tr id="trCompany" runat="server">
            <th>
                发布对象：
            </th>
            <td>
                <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="本组织机构" Value="1" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="本组织机构及下级组织机构" Value="2"></asp:ListItem>
                    <asp:ListItem Text="仅所有下级组织机构" Value="3"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
           <th>
                    状态：
                </th>
                <td colspan="3">
                   <asp:RadioButtonList ID="rblStatus" runat="server" RepeatDirection="Horizontal" CssClass="noborder" >
                      <asp:ListItem Value="1" Selected="True">启用</asp:ListItem>
                      <asp:ListItem Value="0">停用</asp:ListItem>
                   </asp:RadioButtonList>                   
                </td>
            </tr>           
        <tr id="trProject" runat="server">
            <th>
                发布对象：
            </th>
            <td>
                <asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList>支持模糊查询
            </td>
        </tr>
        <tr>
            <th>
                公告内容：
            </th>
            <td>
                <wuc:UEditor ID="fckEditor" runat="server" Width="100%" Height="180" ToolType="Basic">
                </wuc:UEditor>
            </td>
        </tr>
    </table>
</div>
<div class="dv_submit">
    <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn_Save" OnClick="LinkButton1_Click">保存</asp:LinkButton> <a href="javascript:closeWindow();" class="btn_Cancel padleft10">取消</a>
    </div>
