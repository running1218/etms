using ETMS.Components.Basic.API.Entity.Course;
using ETMS.Components.Basic.API.Entity.Course.Resources;
using ETMS.Components.Basic.Implement.DAL.Course.Resources;
using ETMS.Utility;
using ETMS.Utility.Logging;
using ETMS.Utility.Service;
using System;
using System.Configuration;

namespace ETMS.Components.Basic.Implement.BLL.Course.Resources
{
    public partial class Res_TranscodingQueueLogic
    {
        private static readonly Res_TranscodingQueueDataAccess DAL = new Res_TranscodingQueueDataAccess();
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="resTranscodingQueue">业务实体</param>
        public void Insert(Res_TranscodingQueue resTranscodingQueue)
        {
            DAL.Add(resTranscodingQueue);
            BizLogHelper.AddOperate(resTranscodingQueue);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="resTranscodingQueue"></param>
        public void Update(Res_TranscodingQueue resTranscodingQueue)
        {
            DAL.Update(resTranscodingQueue);
            BizLogHelper.AddOperate(resTranscodingQueue);
        }

        public Res_TranscodingQueue GetByID(Guid taskID) {
            return DAL.GetById(taskID);
        }

        public void VideoTranscoding(Guid taskID)
        {
            Res_TranscodingQueue queue = GetByID(taskID);
            if (queue != null) {
                ResContentMore originalEntity = new Res_ContentLogic().GetByID(queue.ContentID.ToGuid());
                if (queue.TranscodingCount == null)
                    queue.TranscodingCount = 1;
                if (queue.TranscodingCount < 2)
                {
                    if (originalEntity != null)
                    {
                        queue.TranscodingCount++;
                        Update(queue);
                        //发送请求
                        string url = string.IsNullOrEmpty(ConfigurationManager.AppSettings["TransCodingUrl"]) ? string.Empty : ConfigurationManager.AppSettings["TransCodingUrl"];
                        var fileUploadConfig = ServiceRepository.FileUploadStrategyService.GetStrategy("MediaResourceVideo");
                        string filePath = fileUploadConfig.Root.Substring(fileUploadConfig.Root.LastIndexOf('\\') + 1) + "/" + originalEntity.DataInfo;
                        string weburl = string.Format("{0}?taskid={1}&inpath={2}&mode={3}", url, taskID.ToString(), filePath, queue.StreamCode);
                        string result = new WebRequestHelper().Get(new Uri(weburl));
                        BizLogHelper.AddOperate(queue);
                    }
                }
                else {
                    throw new ETMS.AppContext.BusinessException(originalEntity.ContentID + "视频转码失败");
                }
            }
        }
    }
}
