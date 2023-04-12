<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CoursewareInfo.ascx.cs"
    Inherits="ETMS.WebApp.Manage.Resource.CoursewareManage.Controls.CoursewareInfo" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/ChooseCourseDropdown.ascx" TagName="ChooseCourseDropdown"
    TagPrefix="uc1" %>
<!--功能标题-->
<h2 class="dv_title">
    非SCORM标准
</h2>
<!--表单录入-->
<div class="dv_information">
    <table class="GridviewGray">
        <tr>
            <th>
                课程名称：
            </th>
            <td colspan="3">
                <uc1:ChooseCourseDropdown ID="ddlCourseID" runat="server" />
            </td>
        </tr>
        <tr>
            <th>
                课件名称：
            </th>
            <td colspan="3">
                <asp:TextBox ID="txtCoursewareName" runat="server" CssClass="inputbox_300" MaxLength="50"></asp:TextBox>
                <span style="color: Red;">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                    runat="server" ErrorMessage="请输入课件名称！" ControlToValidate="txtCoursewareName"
                    ValidationGroup="Error" Display="None"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th>
                课件状态：
            </th>
            <td>
                <cc1:DictionaryRadioButtonList runat="server" ID="radlCoursewareStatus" DictionaryType='Dic_Status' />
            </td>
            <th>
                课件时长：
            </th>
            <td>
                <asp:TextBox ID="txtShowHoures" runat="server" CssClass="inputbox_60" MaxLength="6"></asp:TextBox>分钟
                <span style="color: Red;">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                    runat="server" ErrorMessage="请输入课件时长！" ControlToValidate="txtShowHoures"
                    ValidationGroup="Error" Display="None"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator Display="None" ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtShowHoures"
                    ErrorMessage="课件时长格式错误！" ValidationExpression="^(0|[1-9]\d*)(\.\d*)?$" ValidationGroup="Error"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <th>
                课件来源：
            </th>
            <td colspan="3">
                <asp:TextBox ID="txtCoursewareSource" runat="server" CssClass="inputbox_300" MaxLength="100"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>
                课件类型：
            </th>
            <td colspan="3">
                <asp:RadioButtonList ID="rblType" runat="server" RepeatColumns="2" CssClass="nostyletable"
                    Width="180px" OnPreRender="rblType_PreRender">
                    <asp:ListItem Text="非地址" Value="1" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="地址" Value="0"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr id="trAddress" style="display: none;" runat="server">
            <th>
                地址：
            </th>
            <td colspan="3">
                <asp:TextBox ID="txtAddress" runat="server" CssClass="inputbox_300" MaxLength="100"></asp:TextBox>
            </td>
        </tr>
        <tr id="trUpload" runat="server">
            <th>
                课件上传：
            </th>
            <td colspan="3">
                <asp:Button runat="server" ID="btnUpFile" Text="上传文件" CssClass="btn_upload" />
                <asp:Label ID="lblFileName" runat="server" CssClass="lblFileName"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
             <asp:Label ID="lblState" runat="server" CssClass="lblState"></asp:Label>
                <span style="display: none">
                    <asp:TextBox ID="txtFileName" runat="server" CssClass="txtFileName"></asp:TextBox></span>
                <asp:LinkButton runat="server" ID="linkDownload" OnClick="linkDownload_Click" Text="上传说明"
                    Style="padding-left: 18px"></asp:LinkButton>
                <asp:LinkButton runat="server" ID="linkDownloadHide" Style="display: none"></asp:LinkButton>
                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="linkDownloadHide"
                    PopupControlID="PanelDownload" Drag="True" BackgroundCssClass="modalBackground">
                </ajaxToolkit:ModalPopupExtender>
                <asp:Panel runat="server" ID="PanelDownload" Style="display: none" Height="120px"
                    Width="347px" CssClass="modalBox">
                    <table class="modalBox">
                        <tr>
                            <td style="color: red">
                                <ul>
                                    提示：
                                    <li>1、Office 格式文件请下载<a href="../../Tools/FlashPaper.rar" style="color:Blue;">Swf格式转换器</a>，转换为Swf格式后再上传。</li>
                                    <li>2、媒体格式请下载<a href="../../Tools/FlvTransfer.rar" style="color: Blue">FLV转换器</a>，转换为FLV格式后再上传。
                                    </li>
                                </ul>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:LinkButton runat="server" ID="lbtnClose" Text="关闭" OnClick="lbtnClose_Click"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
         <tr>
            <th>
                缩&nbsp;&nbsp;略&nbsp;&nbsp;图：
            </th>
            <td valign="top" colspan="3">
                <div style="overflow: hidden;">
                    <asp:Image ID="imgCoverLogo" runat="server" CssClass="imgCourseLogo" Width="156px"
                        Height="105px" />建议图片大小：156 * 105px</div>
                <cc1:FileUpload ID="fuCoverImage" runat="server" CallBack="doCallBack" />
                <script type="text/javascript">
                    function doCallBack(imgName, imgUrl) {
                        document.getElementById('<%=imgCoverLogo.ClientID %>').src = imgUrl;
                    }
                </script>
            </td>
        </tr>
        <tr>
            <th>
                课件介绍：
            </th>
            <td colspan="3">
                <asp:TextBox ID="txtRemark" TextMode="MultiLine" runat="server" CssClass="inputbox_area380"></asp:TextBox>
            </td>
        </tr>
    </table>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="lbtnSave" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div id="div_Import_Status" runat="server">
                <div class="import_status">
                    &nbsp;</div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</div>
<!--提交表单-->
<div class="dv_submit">
    <asp:LinkButton ID="lbtnSave" runat="server" CssClass="btn_Save" OnClick="lbtnSave_Click"
        ValidationGroup="Error">保存</asp:LinkButton>
    <asp:ValidationSummary runat="server" ID="ValidationSummary1" ValidationGroup="Error"
        ShowMessageBox="true" ShowSummary="false" />
    <a href="javascript:closeWindow();" class="btn_Cancel padleft10">取消</a>
</div>
<script type="text/javascript">
    function rblChange() {
        var rblType = document.getElementById("<%=rblType.ClientID %>");
        var item = rblType.getElementsByTagName("input");

        if (item[0].checked) {
            document.getElementById("<%=trUpload.ClientID %>").style.display = "";
            document.getElementById('<%=trAddress.ClientID %>').style.display = "none";
        }
        else {
            document.getElementById('<%=trUpload.ClientID %>').style.display = "none";
            document.getElementById('<%=trAddress.ClientID %>').style.display = "";
        }
    }
</script>
