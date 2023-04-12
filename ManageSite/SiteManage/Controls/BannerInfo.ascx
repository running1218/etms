<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BannerInfo.ascx.cs" Inherits="SiteManage_Controls_BannerInfo" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/MiniUpFile.ascx" TagName="uploader" TagPrefix="uc" %>
<%@ Register Src="~/Controls/MiniUpFile2.ascx" TagName="uploader2" TagPrefix="uc2" %>
<%@ Register Src="~/Controls/ChooseCourseDropdown.ascx" TagName="ChooseCourseDropdown" TagPrefix="uc1" %>
<!--功能标题-->
<h2 class="dv_title">实践基本信息
</h2>
<!--表单录入-->
<div class="dv_information">

    <table class="GridviewGray fixedTable">
        <tr>
            <th>推广名称：
            </th>
            <td colspan="3">
                <asp:TextBox ID="txtSpreadName" runat="server" CssClass="inputbox_210" MaxLength="50"></asp:TextBox>
                <span style="color: Red;">*</span><asp:RequiredFieldValidator
                    ID="RequiredFieldValidatorJobName" runat="server" ErrorMessage="请填写推广名称！" ControlToValidate="txtSpreadName"
                    ValidationGroup="Error" Display="None"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th>SEO关键字：
            </th>
            <td colspan="3">
                <asp:TextBox ID="txtKeyWords" runat="server" CssClass="inputbox_210" MaxLength="50"></asp:TextBox>
                <span>关键词之间用","隔开</span>
            </td>
        </tr>
        <tr>
            <th>PC端轮换图：
            </th>
            <td valign="top" colspan="3">
                <div style="overflow: hidden;">
                    <asp:Image ID="imgPC" runat="server" CssClass="imgCourseLogo" Width="156px"
                        Height="105px" />
                </div>
                <uc:uploader ID="uploader" runat="server" FunctionType="BannerImage" CallBack="doCallBack"  FileTypeIsDisplay="false"/>
                <span class="upload-img-standard">支持jpg、png、gif格式图片，最佳尺寸为1920×450像素</span>
            </td>
        </tr>
        <tr>
            <th>移动端轮换图：
            </th>
            <td valign="top" colspan="3">
                <div style="overflow: hidden;">
                    <asp:Image ID="imgMobile" runat="server" CssClass="imgCourseLogo" Width="156px"
                        Height="105px" />
                </div>
                <uc2:uploader2 ID="uploader2" runat="server" FunctionType="BannerImage" CallBack="doCallBacks" FileTypeIsDisplay="false" />
                <span class="upload-img-standard">支持jpg、png、gif格式图片，最佳尺寸为1280×460像素</span>
            </td>
        </tr>
        <tr>
            <th>PC推广链接：
            </th>
            <td colspan="3">
                <asp:TextBox ID="txtPCSpreadLink" runat="server" Width="100px" CssClass="inputbox_210"></asp:TextBox>

            </td>
        </tr>
        <tr>
            <th>移动推广链接：
            </th>
            <td colspan="3">
                <asp:TextBox ID="txtMobileSpreadLink" runat="server" CssClass="inputbox_210"></asp:TextBox>

            </td>
        </tr>     
        <tr>
            <th>状态：
            </th>
            <td colspan="3">
                <asp:RadioButtonList ID="rbPubStatus" runat="server" RepeatDirection="Horizontal" CssClass="noborder">
                    <asp:ListItem Value="1" Selected="True">启用</asp:ListItem>
                    <asp:ListItem Value="0">停用</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
    </table>

</div>
<!--提交表单-->
<div class="dv_submit">
    <asp:LinkButton ID="lbnSave" runat="server" CssClass="btn_Save" OnClick="lbnSave_Click" ValidationGroup="Error">保存</asp:LinkButton>
    <asp:ValidationSummary runat="server" ID="ValidationSummary1" ValidationGroup="Error"
        ShowMessageBox="true" ShowSummary="false" />
    <a href="javascript:closeWindow();" class="btn_Cancel padleft10">取消</a>
</div>
<script  type="text/javascript">
    function doCallBack(imgName, imgUrl, imgSize) {
        document.getElementById('<%=imgPC.ClientID %>').src = imgUrl;
     }
     function doCallBacks(imgName, imgUrl, imgSize) {
         document.getElementById('<%=imgMobile.ClientID %>').src = imgUrl;
     }
</script>

