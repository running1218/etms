<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master/Popup.master" CodeBehind="UpPracticeFile.aspx.cs" Inherits="ETMS.Studying.Study.UpPracticeFile" %>
<%@ Register Src="~/Controls/MiniUpFile.ascx" TagPrefix="uc1" TagName="MiniUpFile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="upload_box">
        <h1>请上传自己的公开课作品</h1>
      <%--  <asp:HiddenField runat="server" ID="HdFilePath" />
        <asp:HiddenField runat="server" ID="HdFileName" />--%>
        <p class="upload"> 作答文件：<input type="text" readonly="readonly" class="fileName"  id="txt_FileName"/></p>
         <uc1:miniupfile runat="server" id="MiniUpFile" FunctionType="OfflineJob" CallBack="doCallBack" FileTypeIsDisplay="false"/>
        <p class="prompt_info">支持.mp4.doc.docx.xls.xlsx.ppt.pptx.zip.rar大小不要超过200MB</p>
        <asp:Button style="width:80px;height:25px;display:block;color:#fff;background:#3685ef;margin:0 auto;margin-top:45px;border:none;cursor:pointer;"  runat="server" ID="Btn_Save"  Text="提交" OnClick="Btn_Save_Click"/>
    </div>
    <style>
        .upload_box{
            width:550px;
            height:236px;
            background:#fff;
            padding-top:80px;
            padding-left:30px;
        }
        .upload_box h1{
            font-size:16px;
            padding-left:70px;
            margin-bottom:20px;
        }
        .upload_box .fileName{
            width:271px;
            height:22px;
            border:1px solid #cfcfcf;
            display:inline-block;
            vertical-align:middle;
            line-height:22px;
            padding:0 10px;
        }
        .upload_box  .upload{
            display: inline-block;
            height: 30px;
        }
         .upload_box #container{
            display: inline-block;
            height: 30px;
            vertical-align: bottom;
         }
        .upload_box  #pickfiles{
            width:78px;
            height:24px;
            position:absolute;
            border:1px solid #3685ef;
            color:#3685ef;
            left:0;
            top:0;
            z-index:2;
            display:inline-block;
            cursor:pointer;
        }
        .prompt_info{
            width: 281px;
            color: #999;
            line-height: 25px;
            padding-left: 71px;
        }
    </style>
    <script>
        function doCallBack(fileName,fileUrl,fileSize,FileOldName) {
            $("#txt_FileName").val(FileOldName);
            
      <%-- $("#<%=HdFileName.ClientID%>").val(fileName);
       $("#<%=HdFilePath.ClientID%>").val(fileUrl);--%>
        }
        //function setData()
        //{
        //    //window.opener.document.getElementById("txt1").value = "Tom";
        //    window.parent.location = window.parent.location;
           
        //}
</script>
</asp:Content>

