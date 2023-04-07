using System;
using System.Collections.Generic;
using System.Data;

using ETMS.Utility.Logging;
using ETMS.Components.Basic.API.Entity.Course;
using ETMS.Components.Basic.Implement.DAL.Course;
using ETMS.Utility;
using ETMS.AppContext;
using ETMS.Components.Basic.API.Entity.Course.Resources;
using ETMS.Components.Basic.Implement.BLL.Course.Resources;
using System.Configuration;
using ETMS.Utility.Service;
using System.IO;
using ETMS.Components.Basic.API.Entity;

namespace ETMS.Components.Basic.Implement.BLL.Course
{
    public partial class Res_ContentLogic
    {
        private static readonly Res_ContentDataAccess DAL = new Res_ContentDataAccess();

        /// <summary>
        /// 查询课件下资源列表
        /// </summary>
        /// <param name="CourseWareID"></param>
        /// <returns></returns>
        public DataTable GetCourseContentList(Guid CourseWareID)
        {
            var dt = DAL.GetCourseContentList(CourseWareID);
            return dt;
        }



        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="resContent">业务实体</param>
        public void Insert(ResContentMore resContentMore)
        {
            DAL.Add(resContentMore);
            BizLogHelper.AddOperate(resContentMore);
        }

        /// <summary>
        /// 批量保存视频
        /// </summary>
        /// <param name="resContentMores"></param>
        public void BatchVideoSave(ResContentMore[] resContentMores)
        {
            try
            {
                string url = string.IsNullOrEmpty(ConfigurationManager.AppSettings["TransCodingUrl"]) ? string.Empty : ConfigurationManager.AppSettings["TransCodingUrl"];
                string streams = string.IsNullOrEmpty(ConfigurationManager.AppSettings["TransCodingStream"]) ? string.Empty : ConfigurationManager.AppSettings["TransCodingStream"];
                string[] streamAry = streams.Split(',');
                var fileUploadConfig = ServiceRepository.FileUploadStrategyService.GetStrategy("MediaResourceVideo");

                for (int i = 0; i < resContentMores.Length; i++)
                {
                    string extension = Path.GetExtension(resContentMores[i].DataInfo).ToLower();
                    resContentMores[i].Type = 1;
                    Insert(resContentMores[i]);

                    //发送请求
                    for (int j = 0; j < streamAry.Length; j++)
                    {
                        //保存至队列
                        Res_TranscodingQueue resTranscodingQueue = new Res_TranscodingQueue();
                        resTranscodingQueue.TranscodingQueueID = Guid.NewGuid();
                        resTranscodingQueue.ContentID = resContentMores[i].ContentID;
                        resTranscodingQueue.CreateTime = DateTime.Now;
                        resTranscodingQueue.StreamCode = streamAry[j];
                        //resTranscodingQueue.Status = (result == "true" ? 1 : 0);
                        Res_TranscodingQueueLogic resTranscodingLogic = new Res_TranscodingQueueLogic();
                        resTranscodingLogic.Insert(resTranscodingQueue);
                        //发送转码请求
                        string filePath = fileUploadConfig.Root.Substring(fileUploadConfig.Root.LastIndexOf('\\') + 1) + "/" + resContentMores[i].DataInfo;
                        string weburl = string.Format("{0}?taskid={1}&inpath={2}&mode={3}", url, resTranscodingQueue.TranscodingQueueID.ToString(), filePath, streamAry[j]);
                        string result = new WebRequestHelper().Get(new Uri(weburl));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ETMS.AppContext.BusinessException(ex.Message);
            }
        }
        public void Save(ResContentMore resContentMore, OperationAction action, int isEdit)
        {
            try
            {
                string extension = Path.GetExtension(resContentMore.DataInfo).ToLower();
                if (extension == ".pdf")
                {
                    resContentMore.Type = 2;
                    if (isEdit == 1)
                    {
                        //转码请求
                        string url = string.IsNullOrEmpty(ConfigurationManager.AppSettings["FileAPIUrl"]) ? string.Empty : ConfigurationManager.AppSettings["FileAPIUrl"];
                        if (url != string.Empty)
                        {
                            Pdf2Image pdf2Image = new Pdf2Image();
                            TransferHandler handler = new TransferHandler(pdf2Image.Transfer);
                            IAsyncResult result = handler.BeginInvoke(resContentMore.DataInfo, resContentMore.ContentID.ToString(), new AsyncCallback(pdf2Image.CallBack), handler);
                            //url += "/api/PDFToImg/Path/" + CrypProvider.Encryptor(resContentMore.DataInfo); // + resContentMore.ContentID.ToString();

                            //string result = new WebRequestHelper().Get(new Uri(url));
                            //JsonMessage<int> returnobj = JsonHelper.DeserializeObject<JsonMessage<int>>(result);

                            if (action == OperationAction.Add)
                            {
                                Insert(resContentMore);
                            }
                            else if (action == OperationAction.Edit)
                            {
                                Update(resContentMore);
                            }
                        }
                        else
                        {
                            throw new ETMS.AppContext.BusinessException("PDF转码服务未配置");
                            //throw;
                        }
                    }
                    else
                    {
                        if (action == OperationAction.Add)
                        {
                            Insert(resContentMore);
                        }
                        else if (action == OperationAction.Edit)
                        {
                            Update(resContentMore);
                        }
                    }
                }
                else// if (extension == ".mp4" || extension == ".flv")
                {
                    resContentMore.Type = 1;
                    if (action == OperationAction.Add)
                    {
                        Insert(resContentMore);

                    }
                    else if (action == OperationAction.Edit)
                    {
                        Update(resContentMore);
                    }
                    if (isEdit == 1)
                    {
                        //发送请求
                        string url = string.IsNullOrEmpty(ConfigurationManager.AppSettings["TransCodingUrl"]) ? string.Empty : ConfigurationManager.AppSettings["TransCodingUrl"];
                        string streams = string.IsNullOrEmpty(ConfigurationManager.AppSettings["TransCodingStream"]) ? string.Empty : ConfigurationManager.AppSettings["TransCodingStream"];

                        string[] streamAry = streams.Split(',');
                        for (int i = 0; i < streamAry.Length; i++)
                        {
                            //保存至队列
                            Res_TranscodingQueue resTranscodingQueue = new Res_TranscodingQueue();
                            resTranscodingQueue.TranscodingQueueID = Guid.NewGuid();
                            resTranscodingQueue.ContentID = resContentMore.ContentID;
                            resTranscodingQueue.CreateTime = DateTime.Now;
                            resTranscodingQueue.StreamCode = streamAry[i];
                            resTranscodingQueue.TranscodingCount = 1;
                            //resTranscodingQueue.Status = (result == "true" ? 1 : 0);
                            Res_TranscodingQueueLogic resTranscodingLogic = new Res_TranscodingQueueLogic();
                            resTranscodingLogic.Insert(resTranscodingQueue);

                            var fileUploadConfig = ServiceRepository.FileUploadStrategyService.GetStrategy("MediaResourceVideo");

                            string filePath = fileUploadConfig.Root.Substring(fileUploadConfig.Root.LastIndexOf('\\') + 1) + "/" + resContentMore.DataInfo;
                            string weburl = string.Format("{0}?taskid={1}&inpath={2}&mode={3}", url, resTranscodingQueue.TranscodingQueueID.ToString(), filePath, streamAry[i]);
                            string result = new WebRequestHelper().Get(new Uri(weburl));
                            BizLogHelper.AddOperate(resTranscodingQueue);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ETMS.AppContext.BusinessException(ex.Message);
            }
        }

        public void ReTranscode(Guid contentID)
        {
            ResContentMore resContent = new Res_ContentLogic().GetByID(contentID);
            if (resContent.Type == 2)
            {
                //转码请求
                string url = string.IsNullOrEmpty(ConfigurationManager.AppSettings["FileAPIUrl"]) ? string.Empty : ConfigurationManager.AppSettings["FileAPIUrl"];
                if (url != string.Empty)
                {
                    Pdf2Image pdf2Image = new Pdf2Image();
                    TransferHandler handler = new TransferHandler(pdf2Image.Transfer);
                    IAsyncResult result = handler.BeginInvoke(resContent.DataInfo, resContent.ContentID.ToString(), new AsyncCallback(pdf2Image.CallBack), handler);
                }
            }
            else if (resContent.Type == 1)
            {
                //发送请求
                string url = string.IsNullOrEmpty(ConfigurationManager.AppSettings["TransCodingUrl"]) ? string.Empty : ConfigurationManager.AppSettings["TransCodingUrl"];
                string streams = string.IsNullOrEmpty(ConfigurationManager.AppSettings["TransCodingStream"]) ? string.Empty : ConfigurationManager.AppSettings["TransCodingStream"];

                string[] streamAry = streams.Split(',');
                for (int i = 0; i < streamAry.Length; i++)
                {
                    //保存至队列
                    Res_TranscodingQueue resTranscodingQueue = new Res_TranscodingQueue();
                    resTranscodingQueue.TranscodingQueueID = Guid.NewGuid();
                    resTranscodingQueue.ContentID = resContent.ContentID;
                    resTranscodingQueue.CreateTime = DateTime.Now;
                    resTranscodingQueue.StreamCode = streamAry[i];
                    resTranscodingQueue.TranscodingCount = 1;
                    //resTranscodingQueue.Status = (result == "true" ? 1 : 0);
                    Res_TranscodingQueueLogic resTranscodingLogic = new Res_TranscodingQueueLogic();
                    resTranscodingLogic.Insert(resTranscodingQueue);

                    var fileUploadConfig = ServiceRepository.FileUploadStrategyService.GetStrategy("MediaResourceVideo");

                    string filePath = fileUploadConfig.Root.Substring(fileUploadConfig.Root.LastIndexOf('\\') + 1) + "/" + resContent.DataInfo;
                    string weburl = string.Format("{0}?taskid={1}&inpath={2}&mode={3}", url, resTranscodingQueue.TranscodingQueueID.ToString(), filePath, streamAry[i]);
                    string result = new WebRequestHelper().Get(new Uri(weburl));
                    BizLogHelper.AddOperate(resTranscodingQueue);
                }
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="resContent">业务实体</param>
        public void Update(ResContentMore resContent)
        {
            ResContentMore originalEntity = GetByID(resContent.ContentID);
            DAL.Update(resContent);
            BizLogHelper.UpdateOperate(originalEntity, resContent);
        }

        /// <summary>
        /// 根据主键删除业务实体
        /// </summary>
        /// <param name="contentID"></param>
		public void Remove(Guid contentID)
        {
            ResContentMore originalEntity = GetByID(contentID);
            DAL.Remove(contentID);
            BizLogHelper.DeleteOperate(originalEntity);
        }

        /// <summary>
        /// 根据主键获取业务实体
        /// </summary>
        /// <param name="contentID"></param>
		public ResContentMore GetByID(Guid contentID)
        {
            DataTable dt = DAL.GetByID(contentID);
            return dt.Rows.Count > 0 ? dt.Rows[0].ToEntity<ResContentMore>() : null;
        }

        /// <summary>
        /// 根据主键获取业务实体
        /// </summary>
        /// <param name="contentID"></param>
		public DataTable GetContentDataInfo(Guid contentID, int type, string streamcode)
        {
            return DAL.GetContentDataInfo(contentID, type, streamcode);
        }

        /// <summary>
        /// 分页查询业务表
        /// </summary>
        /// <param name="pageIndex">页号</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="totalRecords">out 记录总数</param>
        /// <returns>业务表</returns>
        public DataTable GetPagedList(int pageIndex, int pageSize, string sortExpression, string criteria, Guid CourseID, out int totalRecords)
        {
            return DAL.GetPagedList(pageIndex, pageSize, sortExpression, criteria, CourseID, out totalRecords);
        }

        /// <summary>
        /// 查询课件下资源数量
        /// </summary>
        /// <param name="CoursewareID"></param>
        /// <returns></returns>
        public int GetContentCount(Guid CoursewareID)
        {
            return DAL.GetContentCount(CoursewareID);
        }


        /// <summary>
        /// 根据课程资源ID修改排序号
        /// </summary>
        /// <param name="CourseContentID">课程资源ID</param>
        /// <param name="orderNum">排序号</param>
        public void UpdateOrderNumByCourseContentID(Guid CourseContentID, int orderNum)
        {
            DAL.UpdateOrderNumByCourseContentID(CourseContentID, orderNum);
        }
        /// <summary>
        /// 查询课程下资源总数
        /// </summary>
        /// <param name="CourseID">课程ID</param>
        /// <returns></returns>
        public int GetContentCountByCourseID(Guid CourseID)
        {
            return DAL.GetContentCountByCourseID(CourseID);
        }

        public void SetCourseOpenResource(int orgID, Guid courseID, Guid resourceID, int userID)
        {
            DAL.SetCourseOpenResource(orgID, courseID, resourceID, userID);
        }
        public void SetCourseOpenLiving(int orgID, Guid courseID, string livingID, int userID)
        {
            DAL.SetCourseOpenLiving(orgID, courseID, livingID, userID);
        }

        public void RemoveCourseOpenResource(int orgID, Guid courseID)
        {
            DAL.RemoveCourseOpenResource(orgID, courseID);
        }

        public List<CourseOpenResource> GetCourseOpenResource(int orgID, Guid courseID)
        {
            return DAL.GetCourseOpenResource(orgID, courseID).ToList<CourseOpenResource>();
        }
    }

    public delegate JsonMessage<ResultData> TransferHandler(string filePhysicalPath, string id);
    public class Pdf2Image
    {
        public JsonMessage<ResultData> Transfer(string filePhysicalPath, string id)
        {
            string url = string.IsNullOrEmpty(ConfigurationManager.AppSettings["FileAPIUrl"]) ? string.Empty : ConfigurationManager.AppSettings["FileAPIUrl"];
            if (url != string.Empty)
            {
                url += string.Format("/api/PDFToImg/Path/{0}/{1}", CrypProvider.Encryptor(filePhysicalPath),id); // + resContentMore.ContentID.ToString();

                string result = new WebRequestHelper().Get(new Uri(url));
                JsonMessage<ResultData> returnobj = JsonHelper.DeserializeObject<JsonMessage<ResultData>>(result);
                return returnobj;
            }
            return null;
        }
        public void CallBack(IAsyncResult result)
        {
            TransferHandler caller = (TransferHandler)result.AsyncState;
            JsonMessage<ResultData> info = caller.EndInvoke(result);

            if (info.Code != 1)
                ErrorLogHelper.WriteLog("PDF转码服务未配置");
            else {
                var entity = new Res_ContentLogic().GetByID(info.Data.ID.ToGuid());
                entity.PlayTime = info.Data.Pages;
                new Res_ContentLogic().Update(entity);
            }
        }
    }
}