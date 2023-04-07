using System;
using System.Web;
namespace ETMS.Utility.Service.FileUpload
{
    /// <summary>
    /// 文件上传Handler，提供类似MVC控制器功能
    /// </summary>
    public class FileUploadHandler : System.Web.IHttpHandler
    {

        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(System.Web.HttpContext context)
        {
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;


            string action = request.QueryString["action"];
            if (request.HttpMethod != "POST" || (!action.Equals("Upload", StringComparison.InvariantCultureIgnoreCase)) && !action.Equals("Remove", StringComparison.InvariantCultureIgnoreCase))//仅处理POST请求
            {
                response.End();
            }

            if (action.Equals("Upload", StringComparison.InvariantCultureIgnoreCase))
            {
                Upload(request, response);
            }
            else
            {
                Remove(request, response);
            }
        }

        #region 标准文件上传支持

        /// <summary>
        /// 文件上传
        /// 保存至临时文件区域
        /// </summary>
        /// <returns></returns> 
        public void Upload(HttpRequest Request, HttpResponse Response)
        {
            string functionConfigName = Request.Form["FileUpload_ConfigName"];
            //保存前段通过异步方式提交的文件！，并以json方式返回
            if (Request.Files.Count == 0 || string.IsNullOrEmpty(Request.Form["FileUpload_CardID"]) || string.IsNullOrEmpty(Request.Form["FileUpload_FileIndex_" + functionConfigName]))
            {
                ResponseJsonResult(Response, false, "无效上传参数");
                return;
            }

            IFileUploadService service = ServiceRepository.FileUploadService;
            try
            {
                FileUploadCard entity = service.Get(Request.Form["FileUpload_CardID"]);                 
                
                int fileIndex = int.Parse(Request.Form["FileUpload_FileIndex_" + functionConfigName]);
                service.Upload(entity.ID, Request.Files[0], fileIndex);
                //获取全路径，便于客户端预览
                entity = service.Get(entity.ID);
                UploadFileDefine fileDefine = entity.FileDetails[fileIndex];
                fileDefine.FullUrl = ETMS.Utility.StaticResourceUtility.GetFullPathByFileType(entity.FunctionType, fileDefine.BizUrl);
                ResponseJsonResult(Response, fileDefine);
            }
            catch (Exception ex)
            {
                ResponseJsonResult(Response, false, Utility.ExceptionUtility.GetTranslateExceptionMessage(ex));
            }
            return;
        }


        public void Remove(HttpRequest Request, HttpResponse Response)
        {
            string functionConfigName = Request.Form["FileUpload_ConfigName"];
            //保存前段通过异步方式提交的文件！，并以json方式返回
            if (string.IsNullOrEmpty(Request.Form["FileUpload_CardID"]) || string.IsNullOrEmpty(Request.Form["FileUpload_FileIndex_" + functionConfigName]))
            {
                ResponseJsonResult(Response, false, "无效上传参数");
                return;
            }
            try
            {
                IFileUploadService service = ServiceRepository.FileUploadService;
                service.Remove(Request.Form["FileUpload_CardID"], int.Parse(Request.Form["FileUpload_FileIndex_" + functionConfigName]));
                ResponseJsonResult(Response, true, "删除成功!");
                return;
            }
            catch (Exception ex)
            {
                ResponseJsonResult(Response, false, Utility.ExceptionUtility.GetTranslateExceptionMessage(ex));
            }
            return;
        }
        #endregion

        #region Helper
        /// <summary>
        /// 获取Json返回结果{IsSuccess={true|false},Message=''}
        /// </summary>
        /// <param name="isSuccess"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private void ResponseJsonResult(HttpResponse Response, UploadFileDefine fileDefine)
        {

            Response.Clear();
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(
                new
            {
                IsSuccess = true,
                Message = fileDefine.FullUrl,
                FileSize = fileDefine.FileSize,
                FileName = fileDefine.FileName,
                FileTypeHelper = fileDefine.FileType
            }));
        }
        /// <summary>
        /// 获取Json返回结果{IsSuccess={true|false},Message=''}
        /// </summary>
        /// <param name="isSuccess"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private void ResponseJsonResult(HttpResponse Response, bool isSuccess, string message)
        {
            Response.Clear();
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(new { IsSuccess = isSuccess, Message = message }));
        }

        #endregion
    }
}
