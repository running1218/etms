<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MiniUpFile2.ascx.cs" Inherits="Controls_MiniUpFile2" %>
<script type="text/javascript" src="<%=ETMS.Utility.WebUtility.AppPath%>/Tools/upload/js/plupload.full.min.js"></script>
<script type="text/javascript" src="<%=ETMS.Utility.WebUtility.AppPath%>/Tools/upload/langs/zh_CN.js"></script>

<div id="container2">    
    <input id="pickfiles2" type="button" value="选择文件" class="btnWhite" />
    &nbsp;&nbsp;<span class="miniUpload-span" style="color: #ff9900;">
        <asp:Panel runat="server" ID="FileTypePanel2">
    文件类型: <%=GetFileTypes(fileUploadConfig.FileTypes)%>,
    文件最大：<%=fileUploadConfig.MaxFileSize%>M
    </asp:Panel></span>&nbsp;&nbsp;<b id="percent_b2"></b>
</div>
<input type="hidden" runat="server" id="txt_file2" class="hiddentxt" />

<script type="text/javascript">


    var uploader2 = new plupload.Uploader({
        
        browse_button: 'pickfiles2', // you can pass in id...
        container: document.getElementById('container2'), // ... or DOM Element itself

        // General settings
        runtimes: 'html5,flash,silverlight,html4',
        url: '<%=UpUrl%>',

        // User can upload no more then 20 files in one go (sets multiple_queues to false)
        max_file_count: '1',

        chunk_size: '1mb',

        filters: {
            // Maximum file size
            max_file_size: '<%=fileUploadConfig.MaxFileSize%>mb',
            // Specify what files to browse for
            mime_types: [
                { title: "文件类型", extensions: "<%=GetFileTypes(fileUploadConfig.FileTypes)%>" },

                ]
        },         

        // Flash settings
        flash_swf_url: '<%=ETMS.Utility.WebUtility.AppPath%>/Tools/upload/js/Moxie.swf',

        // Silverlight settings
        silverlight_xap_url: '<%=ETMS.Utility.WebUtility.AppPath%>/Tools/upload/js/Moxie.xap',
       

        init: {
           
                       
           

            UploadProgress: function (up, file) {
                document.getElementById('percent_b2').innerHTML = '<span>' + file.percent + "%</span>";
            },

            Error: function (up, err) {
                alert(err.message);
            },

            FileUploaded: function (up, file, info) {            
                if(info.response.substr(0,1)=='{')
                {
                    $('#<%=txt_file2.ClientID%>').attr("value",  info.response );
                   
                    var json = eval("[" + info.response + "]");
                    
                    if(typeof(<%=CallBack%>) == 'function'){
                        <%=CallBack%>(json[0].FileName, json[0].FullUrl, json[0].FileSize);                    
                    }
                }
                
            },    
            FilesAdded: function(up, files) {
                    
                uploader2.start();                  
                  
            },
            ChunkUploaded: function(up, file, info) {
                if(info.response!=null)
                {                       
                       
                    if(info.response.indexOf("文件太大")>=0)
                    {
                        alert(info.response);
                        up.stop();
                        up.removeFile(file);                          
                           
                    }
                }
            }

                            

               
        }
    });

    uploader2.init();

   

</script>