using System;

using ETMS.Components.Basic.API.Entity.Security;
using ETMS.Components.Basic.Implement.BLL.Security;
using ETMS.Components.Basic.Implement.BLL.TrainingItem;
namespace ETMS.Components.Basic.Implement.BLL.JobService
{
    /// <summary>
    /// 培训项目发布提醒Job（页面事件触发）
    /// </summary>
    public class TrainingItemPublishNotifyJob : IJobService
    {
        private Log_SystemInfoLogic SystemInfoLogic = new Log_SystemInfoLogic();

        private Tr_ItemLogic TrainingItemLogic = new Tr_ItemLogic();
        /// <summary>
        /// 培训项目ID
        /// </summary>
        public Guid TrainingItemID { get; set; }

        public TrainingItemPublishNotifyJob(Guid trainingItemID)
        {
            this.TrainingItemID = trainingItemID;
        }

        public int DoJob()
        {
            string objectName = string.Format("TrainingItemPublishNotifyJob_{0}", this.TrainingItemID.ToString("n"));
            //1、判断此业务是否已经处理，如果处理则跳过
            int totalRecords;
            string filter = string.Format(" AND [Target]='{0}'"
                , objectName
                );
            SystemInfoLogic.GetPagedList(1, 1, "", filter, out totalRecords);

            if (this.Logger != null && this.Logger.IsDebug)
            {
                this.Logger.Debug(string.Format("业务触发消息“{0}”已经触发，服务处理跳过！", objectName));
            }

            if (totalRecords == 0)
            {
                string remark = "";
                try
                {
                    System.Data.DataTable dt = TrainingItemLogic.GetNoticeItemStudent(this.TrainingItemID);
                    TrainingItemBeginNotifyJob.Notify(dt);
                    if (this.Logger != null && this.Logger.IsDebug)
                    {
                        this.Logger.Debug(string.Format("业务触发消息“{0}”，本次触发了{1}条消息！ ", objectName, dt.Rows.Count));
                    }
                    return dt.Rows.Count;
                }
                catch (Exception ex)
                {
                    remark = ex.ToString();
                    if (this.Logger != null && this.Logger.IsError)
                    {
                        this.Logger.Error(string.Format("业务触发消息“{0}”触发失败，原因：{1}", objectName, remark));
                    }
                    throw new ETMS.AppContext.BusinessException("TrainingItemPublishNotify.Failed", new object[] { this.TrainingItemID }, ex);
                }
                finally
                {
                    SystemInfoLogic.Save(new Log_SystemInfo()
                    {
                        Target = objectName,
                        LogType = "INFO",
                        Message = remark,
                        CreateTime = DateTime.Now,
                    });
                }
            }
            return 0;
        }

        private OES.Logger.ILog m_Logger;
        public OES.Logger.ILog Logger
        {
            get
            {
                if (m_Logger == null)
                {
                    m_Logger = new OESLoggerAdapter(string.Format("培训项目发布_{0}", this.TrainingItemID));
                }
                return m_Logger;
            }
            set
            {
                m_Logger = value;
            }
        }
    }
}
