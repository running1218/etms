using ETMS.Components.Basic.API.Entity.Course;
using ETMS.Components.Basic.Implement.BLL.Course;
using ETMS.Utility;
using FileAPI.Common;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FileAPI.Controllers
{
    public class PDFController : ApiController
    {
        [Route("api/PDFToImg/Path/{filePath}/{id}")]
        [HttpGet]
        public HttpResponseMessage PDFToImg(string filePath, string id)
        {
            string contentUrl = string.Empty;
            int pageCount = 0;
            try
            {
                //LoggerHelper.WriteLog(filePath, "Operation");
                filePath = CrypProvider.Decryptor(filePath);
                filePath = ConfigurationManager.AppSettings["DiskPath"]+"\\" + filePath.Replace("/","\\");
                //File.AppendAllText("D:\\ETMSSVN\\FileAPILog.txt", filePath);
                if (File.Exists(filePath))
                {
                    string filename = Path.GetFileNameWithoutExtension(filePath);
                    string savepath = Path.Combine(Path.GetDirectoryName(filePath), filename);

                    Pdf2ImageHelper helper = new Pdf2ImageHelper();
                    if (helper.ExportImage(filePath, savepath, "", out pageCount))
                    {
                        CallBack(id, pageCount);
                        return ResponseJson.GetSuccessJson(new { ID= id, Pages=pageCount});
                    }
                    else
                    {
                        return ResponseJson.GetFailedJson(0, "转换文档失败");
                    }

                }
                else {
                    return ResponseJson.GetFailedJson(0, "文件不存在");
                }
                //}
            }
            catch (Exception ex)
            {
                //LoggerHelper.WriteLog(ex.ToString(), "error");
                return ResponseJson.GetFailedJson(0, ex.Message);
            }
        }

        [Route("api/trans2pdf/{filePath}")]
        [HttpGet]
        public HttpResponseMessage Trans2Pdf(string filePath)
        {
            string contentUrl = string.Empty;
            int pageCount = 0;
            try
            {
                //LoggerHelper.WriteLog(filePath, "Operation");
                filePath = CrypProvider.Decryptor(filePath);
                filePath = ConfigurationManager.AppSettings["DiskPath"] + "\\" + filePath.Replace("/", "\\");
                //File.AppendAllText("D:\\ETMSSVN\\FileAPILog.txt", filePath);
                if (File.Exists(filePath))
                {
                    string filename = Path.GetFileNameWithoutExtension(filePath);
                    string savepath = Path.Combine(Path.GetDirectoryName(filePath), filename);

                    Pdf2ImageHelper helper = new Pdf2ImageHelper();
                    if (helper.ExportImage(filePath, savepath, "", out pageCount))
                    {
                        return ResponseJson.GetSuccessJson(new { filePath = filePath, Pages = pageCount });
                    }
                    else
                    {
                        return ResponseJson.GetFailedJson(0, "转换文档失败");
                    }

                }
                else
                {
                    return ResponseJson.GetFailedJson(0, "文件不存在");
                }
                //}
            }
            catch (Exception ex)
            {
                //LoggerHelper.WriteLog(ex.ToString(), "error");
                return ResponseJson.GetFailedJson(0, ex.Message);
            }
        }

        [Route("api/PDFToImg/{contentID}")]
        [HttpGet]
        public HttpResponseMessage ToImg(Guid contentID)
        {
            string contentUrl = string.Empty;
            int pageCount = 0;
            try
            {
                Res_ContentLogic res_ContentLogic = new Res_ContentLogic();
                ResContentMore resContentMore = res_ContentLogic.GetByID(contentID);
                if (resContentMore != null && Path.GetExtension(resContentMore.DataInfo).ToLower() == ".pdf")
                {
                    string filePath = ConfigurationManager.AppSettings["DiskPath"] + resContentMore.DataInfo;
                    if (File.Exists(filePath))
                    {
                        string filename = Path.GetFileNameWithoutExtension(filePath);
                        string savepath = Path.Combine(Path.GetDirectoryName(filePath), filename);

                        Pdf2ImageHelper helper = new Pdf2ImageHelper();
                        if (helper.ExportImage(filePath, savepath, "", out pageCount))
                        {
                            return ResponseJson.GetSuccessJson(pageCount);
                        }
                        else
                        {
                            return ResponseJson.GetFailedJson(0, "转换文档失败");
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                //MessageHelper.AlertMessageBox("转换文档失败。");
                return ResponseJson.GetFailedJson(0, ex.Message);
            }
            return ResponseJson.GetFailedJson();
        }

        [Route("api/PdfToImgResult/{id}/{pages}")]
        [HttpGet]
        public HttpResponseMessage Notice(string id, int pages)
        {           
            var client = new RestClient(string.Format("{0}?ID={1}&Pages={2}", ConfigurationManager.AppSettings["CallBackUrl"], id, pages));
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            return ResponseJson.GetSuccessJson(new { ID = id, Pages = pages });
        }

        [Route("api/enryptFile/{fileName}/{extention}")]
        [HttpGet]
        public HttpResponseMessage EnryptFile(string fileName, string extention)
        {
            string fileFullPath = string.Format("{0}.{1}", fileName, extention);
            var result = CrypProvider.Encryptor(fileFullPath);
            return ResponseJson.GetSuccessJson(new { result = result });
        }

        private void CallBack(string id, int pages)
        {
            var client = new RestClient(string.Format("{0}?ID={1}&Pages={2}", ConfigurationManager.AppSettings["CallBackUrl"], id, pages));
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
        }
    }
}
