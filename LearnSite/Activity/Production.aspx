<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Popup.Master" AutoEventWireup="true" CodeBehind="Production.aspx.cs" Inherits="ETMS.Studying.Activity.Production" %>
<%@ Register Src="~/Controls/AcUpFile.ascx" TagPrefix="uc1" TagName="AcUpFile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="<%=ETMS.Utility.WebUtility.AppPath%>/Styles/myactivity.css" type="text/css" rel="stylesheet" />
    <table class="con-pro-info">
        <tr>
            <th>作品类型：</th>
            <td>
                <asp:DropDownList ID="ddlProductType" runat="server" Width="120px"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <th>作品名称：</th>
            <td>
                <asp:TextBox ID="txtTitle" runat="server" MaxLength="50"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvTitle" runat="server" ControlToValidate="txtTitle" ErrorMessage="请输入作品名称！" ValidationGroup="Error"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th>上传作品：</th>
            <td>
                <input type="text" readonly="readonly" class="fileName"  id="txt_FileName"/>
                <uc1:AcUpFile runat="server" id="MiniUpFile" FunctionType="OfflineJob" CallBack="doCallBack" FileTypeIsDisplay="false"/>
                <br />
                <p class="prompt_info">.docx.pptx.pdf.mp4.mp3.flv，大小不能超过100MB</p>
                <p id="filetype" class=""></p>
            </td>
        </tr>        
    </table>
    <p class="pro-save">
        <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" ValidationGroup="Error" />
    </p>
    <script>
        function doCallBack(fileName, fileUrl, fileSize, FileOldName) {
            $("#txt_FileName").val(FileOldName);
        }

        $('#<%=ddlProductType.ClientID%>').change(function () {
            if ($(this).val() == "3") {
                $('.prompt_info').html('.mp4.mp3.flv，大小不能超过100MB');
                $('#filetype').html('.mp3,.mp4,.flv');
                editfiletype("mp3,mp4,flv");
            }
            else {
                $('.prompt_info').html('.docx.pptx.pdf，大小不能超过100MB');
                $('#filetype').html('.docx,.pptx,.pdf');
                editfiletype("docx,pptx,pdf");
            }
            clearFile();
        });

        $(document).ready(function () {
            if ($('#<%=ddlProductType.ClientID%>').val() == "3") {
                $('.prompt_info').html('.mp4.mp3.flv，大小不能超过100MB');
                $('#filetype').html('.mp3,.mp4,.flv');
                editfiletype("mp3,mp4,flv");
            }
            else {
                $('.prompt_info').html('.docx.pptx.pdf，大小不能超过100MB');
                $('#filetype').html('.docx,.pptx,.pdf');
                editfiletype("docx,pptx,pdf");
            }
        });
        function clearFile() {
            $("#txt_FileName").val("");
            $("#ctl00_ContentPlaceHolder1_MiniUpFile_txt_file").val("");
        }
    </script>
</asp:Content>
