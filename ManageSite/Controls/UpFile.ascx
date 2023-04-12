<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UpFile.ascx.cs" Inherits="Controls_UpFile" %>
<link rel="stylesheet" href="<%=ETMS.Utility.WebUtility.AppPath%>/Tools/upload/js/jquery-ui.css" type="text/css" />
<link rel="stylesheet" href="<%=ETMS.Utility.WebUtility.AppPath%>/Tools/upload/js/jquery.ui.plupload/css/jquery.ui.plupload.css" type="text/css" />

<script type="text/javascript" src="<%=ETMS.Utility.WebUtility.AppPath%>/Tools/upload/js/jquery.min.js"></script>
<script type="text/javascript" src="<%=ETMS.Utility.WebUtility.AppPath%>/Tools/upload/js/jquery-ui.min.js"></script>
<script type="text/javascript" src="<%=ETMS.Utility.WebUtility.AppPath%>/Tools/upload/js/plupload.full.min.js"></script>
<script type="text/javascript" src="<%=ETMS.Utility.WebUtility.AppPath%>/Tools/upload/js/jquery.ui.plupload/jquery.ui.plupload.js"></script>
<script type="text/javascript" src="<%=ETMS.Utility.WebUtility.AppPath%>/Tools/upload/langs/zh_CN.js"></script>

<div id="uploader">
    <p>Your browser doesn't have Flash, Silverlight or HTML5 support.</p>
</div>
<br />
<pre id="console"></pre>
<input type="hidden" runat="server" id="txt_file" class="hiddentxt" />

<script type="text/javascript">
    var filecount=0;
    // Initialize the widget when the DOM is ready
    $(function () {
        $("#uploader").plupload({
            // General settings
            runtimes: 'html5,flash,silverlight,html4',
            url: '<%=UpUrl%>',

            // User can upload no more then 20 files in one go (sets multiple_queues to false)
            max_file_count: '<%=fileUploadConfig.MaxFileCount%>',            

            chunk_size: '1mb',                       

            filters: {
                // Maximum file size
                max_file_size: '<%=fileUploadConfig.MaxFileSize%>mb',
                // Specify what files to browse for
                mime_types: [
                    { title: "文件类型", extensions: "<%=GetFileTypes(fileUploadConfig.FileTypes)%>" },
                    
                ]
            },

            // Rename files by clicking on their titles
            rename: true,

            // Sort files
            sortable: true,

            // Enable ability to drag'n'drop files onto the widget (currently only HTML5 supports that)
            dragdrop: true,

            // Views to activate
            //views: {
            //    list: true,
            //    thumbs: true, // Show thumbs
            //    active: 'thumbs'
            //},

            // Flash settings
            flash_swf_url: '<%=ETMS.Utility.WebUtility.AppPath%>/Tools/upload/js/Moxie.swf',

            // Silverlight settings
            silverlight_xap_url: '<%=ETMS.Utility.WebUtility.AppPath%>/Tools/upload/js/Moxie.xap',
            init : {
                FileUploaded: function (up, file, info) {
                    
                    if(info.response.substr(0,1)=='{')
                    {
                        if ($('.hiddentxt').val() == "") {
                            $('.hiddentxt').attr("value",  info.response );
                        } else {
                            $('.hiddentxt').attr("value", $('.hiddentxt').val() + ',' + info.response);
                        }
                        var json = eval("[" + info.response + "]");
                    
                        if(typeof(<%=CallBack%>) == 'function'){
                            <%=CallBack%>(json[0].FileName, json[0].FullUrl, json[0].FileSize);                    
                        }
                    }
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
                },

                Browse: function(up) {
                    if(filecount>=up.getOption('max_file_count'))
                    {
                        alert("文件个数不能大于"+up.getOption('max_file_count'));
                        up.disableBrowse(true);
                    }
                },

                FilesAdded: function(up, files) {
                    
                    filecount=filecount+files.length;                   
                  
                },

                FilesRemoved: function(up, files) {
                    plupload.each(files, function(file) {
                        if(file.status!=5)
                        {
                            filecount=filecount-1;
                        }
                        
                    });
                                                           

                }
 
        
               
            }
        });


       


        // Handle the case when form was submitted before uploading has finished
        $('#form').submit(function (e) {
            // Files in queue upload them first
            if ($('#uploader').plupload('getFiles').length > 0) {

                // When all files are uploaded submit form
                $('#uploader').on('complete', function () {
                    $('#form')[0].submit();
                });

                $('#uploader').plupload('start');
            } else {
                alert("You must have at least one file in the queue.");
            }
            return false; // Keep the form from submitting
        });
    });
</script>
