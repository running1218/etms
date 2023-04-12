<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BulletinInfo.ascx.cs"
    Inherits="Information_AfficheManager_Controls_BulletinInfo" %>
<%@ Register Assembly="ETMS.Editor" Namespace="ETMS.Editor.UEditor" TagPrefix="wuc" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/MiniUpFile.ascx" TagName="uploader" TagPrefix="uc" %>
<!--功能标题-->
<h2 class="dv_title">
    公告管理
</h2>
<div class="dv_information">
    <table class="GridviewGray" style="margin-bottom:0px;">
        <tr>
            <th >
                公告标题：
            </th>
            <td  colspan="3">
                <asp:TextBox ID="txtMainHead" runat="server" CssClass="inputbox_300" MaxLength="30"></asp:TextBox>
                <span style="color: Red;">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidatorJobName"
                    runat="server" ErrorMessage="请填公告标题！" ControlToValidate="txtMainHead" ValidationGroup="Error"
                    Display="None"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <%--<tr>
            <th>
                公告摘要：
            </th>
            <td colspan="3">
                <asp:TextBox ID="txtBrief" runat="server" TextMode="MultiLine" CssClass="inputbox_area520" MaxLength="100" Height="60px"></asp:TextBox>
                <span style="color: Red;">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator12"
                    runat="server" ErrorMessage="请填公告摘要！" ControlToValidate="txtBrief" ValidationGroup="Error"
                    Display="None"></asp:RequiredFieldValidator>
            </td>
        </tr>--%>
         <tr>
            <th>
                图&nbsp;&nbsp;片：
            </th>
            <td valign="top" colspan="3">
                <div style="overflow: hidden;">
                    <asp:Image ID="imgBulletin" runat="server" CssClass="imgCourseLogo" Width="156px"
                        Height="105px" /></div>
                <uc:uploader ID="uploader" runat="server" FunctionType="BulletinImage" CallBack="doCallBack" FileTypeIsDisplay="false" />
                <script type="text/javascript">
                    function doCallBack(imgName, imgUrl, imgSize) {
                        document.getElementById('<%=imgBulletin.ClientID %>').src = imgUrl;
                    }
                </script>
                <span class="upload-img-standard">支持jpg、png、gif格式图片，最佳尺寸为370×200像素</span>
            </td>
        </tr>
        <tr>
            <th>
                有&nbsp;&nbsp;效&nbsp;&nbsp;期：
            </th>
            <td colspan="3">
                <cc1:DateTimeTextBox ID="dttbBeginTime" runat="server" CssClass="inputbox_120" EndTimeControlID="dttbEndTime"></cc1:DateTimeTextBox>
                至
                <cc1:DateTimeTextBox ID="dttbEndTime" runat="server" CssClass="inputbox_120" BeginTimeControlID="dttbBeginTime"></cc1:DateTimeTextBox>
            </td>
        </tr>
        <tr id="trCompany" runat="server" class="hide" >
            <th>
                发布对象：
            </th>
            <td colspan="3">
                <asp:RadioButtonList ID="rbnBulletinObjectTypeName" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="本组织机构" Value="1"></asp:ListItem>
                    <asp:ListItem Text="本组织机构及下级组织机构" Value="2" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="仅所有下级组织机构" Value="3"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
       </table> 
        <table class="GridviewGray">
        <tr>
            <th>
                状&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;态：
            </th>
            <td>
                <asp:RadioButtonList ID="rblStatus" runat="server" RepeatDirection="Horizontal" CssClass="noborder">
                    <asp:ListItem Value="1" Selected="True">启用</asp:ListItem>
                    <asp:ListItem Value="0">停用</asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <th>
                公告级别： 
            </th>
            <td>
            <cc1:DictionaryRadioButtonList runat="server" ID="ddlInfoLevelID" DictionaryType="Inf_dic_InfoLevel" Enabled="true"  />
            </td>
        </tr>
        <tr>
            <th>
                公告内容：
            </th>
            <td colspan="3">
                <wuc:UEditor ID="fckEditor" runat="server" Width="100%" Height="200" ToolType="Basic">
                </wuc:UEditor>
            </td>
        </tr>
    </table>
</div>
<div class="dv_submit">
    <asp:LinkButton ID="lbnSave" runat="server" CssClass="btn_Save" OnClick="lbnSave_Click"
        ValidationGroup="Error">保存</asp:LinkButton>
    <asp:ValidationSummary runat="server" ID="ValidationSummary1" ValidationGroup="Error"
        ShowMessageBox="true" ShowSummary="false" />
    <a href="javascript:closeWindow();" class="btn_Cancel padleft10">取消</a>
</div>
