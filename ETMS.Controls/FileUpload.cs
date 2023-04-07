using System;
using System.Text;

using ETMS.Utility.Service;
using ETMS.Utility.Service.FileUpload;
using System.Web.UI.WebControls;
using ETMS.Utility;
namespace ETMS.Controls
{
    /// <summary>
    /// 文件上传控件，依赖元素：
    /// 1、js ajaxfileupload.js
    /// 2、服务 ServiceRepository.FileUploadService
    /// 3、controller FileUpload?action={upload|remove}
    /// 4、FunctionType 映射上传策略
    /// </summary>
    public class FileUpload : WebControl
    {
        /// <summary>
        /// 此次上传对应的业务功能类型
        /// </summary>
        public string FunctionType
        {
            get
            {
                return (string)ViewState["FunctionType"];
            }
            set
            {
                ViewState["FunctionType"] = value;
            }
        }
        /// <summary>
        /// 上传后回调js
        /// </summary>
        public string CallBack
        {
            get
            {
                return (ViewState["CallBack"] == null) ? "null" : (string)ViewState["CallBack"];
            }
            set
            {
                ViewState["CallBack"] = value;
            }
        }
        /// <summary>
        /// 显示状态客户端ID
        /// </summary>
        public string StatusID
        {
            get
            {
                return (ViewState["StatusID"] == null) ? "" : (string)ViewState["StatusID"];
            }
            set
            {
                ViewState["StatusID"] = value;
            }
        }
        /// <summary>
        /// 是否显示删除按钮
        /// </summary>
        public bool ShowDelButton
        {
            get
            {
                return (ViewState["ShowDelButton"] == null) ? true : (bool)ViewState["ShowDelButton"];
            }
            set
            {
                ViewState["ShowDelButton"] = value;
            }
        }
        /// <summary>
        /// 对话框高度（默认:200px)
        /// </summary>
        public int Height
        {
            get
            {
                return (ViewState["Height"] == null) ? 200 : (int)ViewState["Height"];
            }
            set
            {
                ViewState["Height"] = value;
            }
        }
        /// <summary>
        ///  对话框宽度（默认:400px)
        /// </summary>
        public int Width
        {
            get
            {
                return (ViewState["Width"] == null) ? 400 : (int)ViewState["Width"];
            }
            set
            {
                ViewState["Width"] = value;
            }
        }


        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            if (DesignMode) return;

            //必要参数验证
            if (FunctionType == null)
            {
                throw new Exception("文件上传部件，传入参数“FunctionType”未设置！");
            }

            IFileUploadService service = ServiceRepository.FileUploadService;
            //申请上传令牌
            FileUploadCard entity = service.Apply(FunctionType);
            //获取附件上传规则
            FileUploadConfig config = service.GetFunctionConfig(entity.ID);
            string suffix = "_" + FunctionType.ToString();
            string UploadUrl = this.ActionHref(this.ResolveUrl("~/FileUpload.ashx?action=upload"));
            string RemoveUrl = this.ActionHref(this.ResolveUrl("~/FileUpload.ashx?action=remove"));
            string supportFileTypes = Autumn.Util.StringUtils.CollectionToDelimitedString(config.FileTypes, "|");
            string displaySupportFileTypes = Autumn.Util.StringUtils.CollectionToDelimitedString(config.FileTypes, ",").Replace(".", "");
            //动态生成窗体HTML <multiple>
            StringBuilder mainWriter = new StringBuilder();
            mainWriter.AppendFormat(
                "<div id='upload_form{4}' title='文件上传(附件:{7})' style='font-size: 50%;'>" +
                        "<div id='fileupload{4}' ReturnInfoID='upload_state{4}' CallBack='{6}' MaxNumberOfFiles='{1}' MaxFileSize='{2}' AcceptFileTypes='{3}'  >" +
                            "<form id='uploadform{4}' action='" + UploadUrl + "' method='POST' enctype='multipart/form-data'>" +
                            "<input type='hidden' name='FileUpload_ConfigName' value='{5}'/>" +
                            "<input type='hidden' name='FileUpload_CardID' value='{0}'/>" +

                            "<input type='hidden' name='FileUpload_FileIndex{4}' id='FileUpload_FileIndex{4}' value='0' />" +
                            "<div title='添加文件' style='margin-left:10px;margin-top:5px;'>" +
                                "<span sytle='display:inline-block;overflow:hidden;'>" +
                                    "<span><a href='#' class='btn-upload-file'>添加</a></span>" +
                                          "<input type='file' id='upload_files{4}'  name='Files' style='position:relative;left:-85px;width:65px;height:25px;opacity:0;filter:alpha(opacity=0);'/>" +
                                "</span>" +
                                "<span style='display:inline-block;font-weight:normal;position:relative;left:-60px;'>可传<span class='colorGreen'>{1}</span>个文件，每个小于<span class='colorGreen' id='max_file_size{4}'>{2}</span></span>" +
                             "</div>" +
                            "</form>" +
                            "<div class='open_filelist'>" +
                                 "<table  class='filelist'></table>" +
                            "</div>" +
                         "</div>" +
                 "</div>",
                entity.ID,
                config.MaxFileCount,
                config.MaxFileSize,
                supportFileTypes,
                suffix,
                FunctionType.ToString(),
                CallBack,
                displaySupportFileTypes);

            StringBuilder jsWriter = new StringBuilder();
            jsWriter.Append(@"<script type='text/javascript'>    
    var curFileName@{@suffix};
    var curFileSize@{@suffix};
    function showModalUploadDialog@{@suffix}() {
        //输出上传文件内容窗体");
            jsWriter.Append("\r\n   var html = \"@{@html}\";");

            jsWriter.Append(@" 
           //将窗体加入右侧工作区内
           if($('#upload_form@{@suffix}').attr('id') == undefined){
           //自动加入一个容器 
           $('body').append('<div class=fileupload_container@{@suffix}></div>');
           $('div.fileupload_container@{@suffix}').append(html);
           //配置文件里示意性最大值
           $('#max_file_size@{@suffix}').html(FormatFileSize2($('#max_file_size@{@suffix}').html()));         
        }
        
        RegEvent@{@suffix}();
  
        //先加载上传对话框
        $('#upload_form@{@suffix}').dialog({
            autoOpen: true,
            height: @{@Height},
            width: @{@Width},
            modal: true,
            time: 5000,
            fixed: true,
            closeOnEscape:true,        
            position:[($('#FileUploadBtn@{@suffix}').position().left-150<0)?10:$('#FileUploadBtn@{@suffix}').position().left-150,$('#FileUploadBtn@{@suffix}').position().top+30-ScollPostion().top]
        });
    }

    function addToList@{@suffix}(isAcceptable) {
        if(isAcceptable){
           if($('#fileupload@{@suffix} table.filelist tr').length>0){
                $('<tr class=send_ready id=fileitem_ ><td width=60%><span class=filename>' + curFileName@{@suffix}[0] + '</span></td><td class=status><span ><span class=progressbar></span></span></td></tr>').insertBefore('#fileupload@{@suffix} table.filelist tr:first');
           }
           else{
                $('#fileupload@{@suffix} table.filelist').append('<tr class=send_ready id=fileitem_ ><td width=60%><span class=filename>' + curFileName@{@suffix}[0] + '</span></td><td class=status><span ><span class=progressbar></span></span></td></tr>');
           }
        }
        else{
            if($('#fileupload@{@suffix} table.filelist tr').length>0){
                $('<tr class=send_fail id=fileitem_ ><td width=60%><span class=filename>' + curFileName@{@suffix}[0] + '</span></td><td class=status>失败! <font color=brown>【不支持的附件类型！】</font></td></tr>').insertBefore('#fileupload@{@suffix} table.filelist tr:first');
            }
            else{
                $('#fileupload@{@suffix} table.filelist').append('<tr class=send_fail id=fileitem_ ><td width=60%><span class=filename>' + curFileName@{@suffix}[0] + '</span></td><td class=status>失败! <font color=brown>【不支持的附件类型！】</font></td></tr>');
            }
            UpdateStatus@{@suffix}();
        }
    }

    function ajaxFileUpload@{@suffix}() {

        $.ajaxFileUpload
		(
			{
			    url: '@{@UploadUrl}',
			    secureuri: false,
			    fileElementId: 'upload_files@{@suffix}',
			    orgFormId: 'uploadform@{@suffix}',
			    dataType: 'json',
			    success: function (data, status) { 
			        if (data.IsSuccess === true) {
                                                                   
                        var targetobj = $('#fileupload@{@suffix} table.filelist').find('tr:first').find('td.status'); 
			            $('#fileupload@{@suffix} table.filelist').find('tr:first').removeClass('send_ready').addClass('send_ok').find('td.status').html( FormatFileSize(data.FileSize));
                        if(@{@ShowDelButton}){");
            jsWriter.Append("$(\"<td width=10%><a href='javascript:void(0)' class='link_colorBlue12'>删除</a></td>\").insertAfter(targetobj);");
            jsWriter.Append(@"   }
			            var index = 0;
                        	
			            $('#fileupload@{@suffix} table.filelist').find('tr.send_ok').each(function () {	
			                $(this).attr('id', 'fileitem_' + index);
			                $(this).find('a').each(function () {
			                    $(this).attr('href', 'javaScript:DeleteFile@{@suffix}(' + index + ')');
			                });
                            index++;
			            }); 
                                           
                        //callback
                        if(typeof(@{@CallBack}) == 'function'){
                            @{@CallBack}( $('#fileupload@{@suffix} table.filelist').find('tr:first span.filename').html(),data.Message);
                        }                       
			        }
			        else {
			            $('#fileupload@{@suffix} table.filelist').find('tr.send_ready').removeClass('send_ready').addClass('send_fail').find('td.status').html('失败! <font color=brown>【' + data.Message + '】</font>');
			        }
                    UpdateStatus@{@suffix}();
			    },
			    error: function (data, status, e) {
			       //alert(e);
			    }
			}
		)
        return false;
    }

    function UpdateStatus@{@suffix}(){   
        var resultStr = '<b>上传结果:</b> <font color=green>' +$('#fileupload@{@suffix} table.filelist').find('tr.send_ok').length+'</font>个成功，<font color=red>'+$('#fileupload@{@suffix} table.filelist').find('tr.send_fail').length + '</font>个失败';
        var StatusID = '@{@StatusID}';
        if(StatusID.length>0){
            $('#'+StatusID).html(resultStr);
        }
        else{    
            $('#upload_state@{@suffix}').html(resultStr);
        }

        if(@{@config.MaxFileCount}==$('#fileupload@{@suffix} table.filelist').find('tr.send_ok').length){
            $('#fileupload@{@suffix} input[type=file]').attr('disabled', true);
            //设置灰按钮
            $('#fileupload@{@suffix} input[type=file]').prev('span').find('a').addClass('btn-upload-fileDisabled');
        }
        else{
            $('#fileupload@{@suffix} input[type=file]').removeAttr('disabled');
            //设置正常按钮
             $('#fileupload@{@suffix} input[type=file]').prev('span').find('a').removeClass('btn-upload-fileDisabled');
        }
    }

    function DeleteFile@{@suffix}(index) {
        $.ajax({
            url: '@{@RemoveUrl}',
            type: 'POST',
            data: encodeURI('FileUpload_FileIndex@{@suffix}=' + index + '&FileUpload_ConfigName=@{@functionType}&FileUpload_CardID=@{@entity.ID}'),
            success: function (results) {
                eval('data = ' + results);
                if (data.IsSuccess === true) {
                    $('#fileupload@{@suffix} table.filelist').find('#fileitem_' + index).remove();
                    var index1 = 0;
                    $('#fileupload@{@suffix} table.filelist').find('tr.send_ok').each(function () {
                        $(this).attr('id', 'fileitem_' + index1);
                        $(this).find('a').each(function () {
                            $(this).attr('href', 'javaScript:DeleteFile@{@suffix}(' + index1 + ')');
                        });
                        index1++;
                    });
                    UpdateStatus@{@suffix}();
                } else {
                    popAlertMsg(data.Message,'出错提示' );
                }
            }
        });
    }

    function execFunction@{@suffix}() {
        var $input = $('#fileupload@{@suffix} input[type=file]');
        curFileName@{@suffix} = new Array();
        curFileSize@{@suffix} = new Array();
        
        if ($.browser.msie || $.browser.opera) {
            curFileName@{@suffix}[0] = $input.val().replace(/C:\\fakepath\\/ig, '');
            curFileName@{@suffix}[0] = curFileName@{@suffix}[0].substr(curFileName@{@suffix}[0].lastIndexOf('\\')+1);
        }
        else {
            for(var i=0;i< $input.get(0).files.length;i++){
                curFileName@{@suffix}[i] = $input.get(0).files[i].name; 
                curFileName@{@suffix}[i] = curFileName@{@suffix}[i].substr(curFileName@{@suffix}[i].lastIndexOf('\\')+1);
                curFileSize@{@suffix}[i] = $input.get(0).files[i].size;
            }
        }
        //
        if (curFileName@{@suffix}[0] != undefined && curFileName@{@suffix}[0].length > 0) {
            var filetype = curFileName@{@suffix}[0].substr(curFileName@{@suffix}[0].lastIndexOf('.'));
            var typeenum='@{@config.FileTypes}';
            var isAcceptable = (-1 != typeenum.toLowerCase().indexOf(filetype.toLowerCase()))? true:false;
            if(isAcceptable){
                addToList@{@suffix}(isAcceptable);
                ajaxFileUpload@{@suffix}();
                RegEvent@{@suffix}();
            }
            else{
                addToList@{@suffix}(isAcceptable);
            }
        }
    }



    function RegEvent@{@suffix}() {
        var $input = $('#upload_files@{@suffix}');
        $input.change(execFunction@{@suffix});
    }


</script>

<input type='button' id='FileUploadBtn@{@suffix}' value='上传文件' class='btn_upload'  onclick='showModalUploadDialog@{@suffix}()' />
<input type='hidden' id='FileUpload_ID@{@suffix}' name='FileUpload_ID@{@suffix}' value='@{@entity.ID}' />
<span id='upload_state@{@suffix}'></span>

");
            jsWriter.Replace("@{@suffix}", suffix);
            jsWriter.Replace("@{@html}", mainWriter.ToString());
            jsWriter.Replace("@{@Height}", Height.ToString());
            jsWriter.Replace("@{@Width}", Width.ToString());
            jsWriter.Replace("@{@UploadUrl}", UploadUrl);
            jsWriter.Replace("@{@RemoveUrl}", RemoveUrl);
            jsWriter.Replace("@{@CallBack}", CallBack);
            jsWriter.Replace("@{@config.MaxFileCount}", config.MaxFileCount.ToString());
            jsWriter.Replace("@{@config.FileTypes}", supportFileTypes);
            jsWriter.Replace("@{@functionType}", FunctionType);
            jsWriter.Replace("@{@entity.ID}", entity.ID);
            jsWriter.Replace("@{@ShowDelButton}", ShowDelButton.ToString().ToLower());
            jsWriter.Replace("@{@StatusID}", StatusID);

            //输出控件
            writer.Write(jsWriter.ToString());
            base.Render(writer);
        }

        #region 文件上传集成
        /// <summary>
        /// 保存上传的文件
        /// </summary>
        /// <param name="functionType">功能配置名称</param>
        /// <returns></returns>
        public virtual ETMS.Utility.Service.FileUpload.FileUploadCard SaveUploadFiles()
        {
            //如果功能配置名称不为空，则追加"_"分隔符
            string functionConfigName = "_" + this.FunctionType;
            ETMS.Utility.Service.FileUpload.IFileUploadService fileUploadService = ETMS.Utility.Service.ServiceRepository.FileUploadService;
            return fileUploadService.Save(this.Page.Request.Form["FileUpload_ID" + functionConfigName]);
        }
        #endregion
    }
}
