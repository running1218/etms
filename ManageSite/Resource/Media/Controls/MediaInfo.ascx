<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MediaInfo.ascx.cs" Inherits="Resource_Media_Controls_MediaInfo" %>
<%@ Register Assembly="ETMS.Controls" Namespace="ETMS.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/ChooseCourseDropdown.ascx" TagName="ChooseCourseDropdown"
    TagPrefix="uc1" %>
<%@ Register Src="~/Controls/MiniUpFile.ascx" TagPrefix="uc1" TagName="MiniUpFile" %>


<!--功能标题-->
<h2 class="dv_title">
   
</h2>
<!--表单录入-->
<div class="dv_information">
    <table class="GridviewGray">
        
        <tr>
            <th>
                名称：
            </th>
            <td colspan="3">
                <asp:TextBox ID="txtMediaName" runat="server" CssClass="inputbox_300" MaxLength="50"></asp:TextBox>
                <span style="color: Red;">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                    runat="server" ErrorMessage="请输入名称！" ControlToValidate="txtMediaName"
                    ValidationGroup="Error" Display="None"></asp:RequiredFieldValidator>
            </td>
        </tr>
       <%-- <tr>
            <th>
                类型：
            </th>
            <td>                
                <cc1:DictionaryRadioButtonList runat="server" ID="radlMediaType" DictionaryType='Dic_MediaType' />
            </td>
            
        </tr>       --%>
        <tr id="trUpload" runat="server">
            <th>
                媒体文件上传：
            </th>
            <td colspan="3">
                <uc1:MiniUpFile runat="server" ID="MiniUpFile" FunctionType="Media"/>
                &nbsp;&nbsp;
             <asp:Label ID="lblState" runat="server" CssClass="lblState"></asp:Label>               
                <asp:LinkButton runat="server" ID="linkDownload" OnClick="linkDownload_Click" Text=""
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
                                    <li>1、类型：<%=this.MiniUpFile.GetFileTypes(this.MiniUpFile.fileUploadConfig.FileTypes) %></li>
                                    <li>2、最大：<%=this.MiniUpFile.fileUploadConfig.MaxFileSize %>M
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
                媒体介绍：
            </th>
            <td colspan="3">
                <asp:TextBox ID="txtRemark" TextMode="MultiLine" runat="server" CssClass="inputbox_area380"></asp:TextBox>
            </td>
        </tr>
    </table>
    
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
    <asp:LinkButton ID="lbtnSave" runat="server" CssClass="btn_Save" OnClick="lbtnSave_Click" OnClientClick="return ValidationData();" 
        ValidationGroup="Error">保 存</asp:LinkButton>
    <asp:ValidationSummary runat="server" ID="ValidationSummary1" ValidationGroup="Error"
        ShowMessageBox="true" ShowSummary="false" />
    <a href="javascript:closeWindow();" class="btn_Cancel padleft10">取消</a>
</div>

<script language="javascript" type="text/javascript">
    function ValidationData() {        
        if ($("#percent_b").html().indexOf("%</span>") > -1) {           
            if ($("#percent_b").html().indexOf("100%</span>") == -1) {
                popFailedMsg("文件还未上传完毕");               
                return false;
            }
            return true;
        }
        return true;
    }
 </script>