using System;
using Autumn.Context;
using Autumn.Context.Support;

using ETMS.Components.Basic.API.Entity.Security;
using ETMS.Components.Basic.Implement.BLL.Security;
namespace ETMS.Components.Basic.Implement.BLL.JobService
{
    /// <summary>
    /// 每天触发的业务提醒Job
    /// </summary>
    public class DayBizTriggerJob : IJobService
    {
        private IApplicationContext AppContext = null;
        private Log_SystemInfoLogic SystemInfoLogic = new Log_SystemInfoLogic();

        public int DoJob()
        {
            if (AppContext == null)
            {
                string objectXml = "assembly://ETMS.Components.Basic.Implement/ETMS.Components.Basic.Implement.BLL.JobService/DayBizTriggerJobs.xml";
                AppContext = new XmlApplicationContext(objectXml);//加载IOC应用

                if (this.Logger != null && this.Logger.IsDebug)
                {
                    this.Logger.Debug(string.Format("加载业务触发消息配置上下文,共{0}个业务提醒触发！ ", AppContext.GetObjectDefinitionNames().Length));
                }
            }
            //循环IOC应用配置
            foreach (string objectName in AppContext.GetObjectDefinitionNames())
            {

                if (this.Logger != null && this.Logger.IsDebug)
                {
                    this.Logger.Debug("循环加载业务触发消息,IOC对象-->" + objectName);
                }

                //1、判断此业务当天是否已经处理，如果处理则跳过
                int totalRecords;
                string filter = string.Format(" AND [Target]='{0}' AND [CreateTime]>='{1}' AND [CreateTime]<'{2}'"
                    , objectName
                    , DateTime.Now.ToString("yyyy-MM-dd")
                    , DateTime.Now.AddDays(1).ToString("yyyy-MM-dd")
                    );
                SystemInfoLogic.GetPagedList(1, 1, "", filter, out totalRecords);

                if (this.Logger != null && this.Logger.IsDebug)
                {
                    this.Logger.Debug(string.Format("业务触发消息“{0}”今日已经触发，服务处理跳过！", objectName));
                }

                if (totalRecords == 0)
                {
                    string remark = "";
                    try
                    {
                        IJobService job = (IJobService)AppContext.GetObject(objectName);
                        job.Logger = this.Logger;
                        int count = job.DoJob();
                        if (this.Logger != null && this.Logger.IsDebug)
                        {
                            this.Logger.Debug(string.Format("业务触发消息“{0}”，本次触发了{1}条消息！ ", objectName, count));
                        }
                    }
                    catch (Exception ex)
                    {
                        remark = ex.ToString();
                        if (this.Logger != null && this.Logger.IsError)
                        {
                            this.Logger.Error(string.Format("业务触发消息“{0}”触发失败，原因：{1}", objectName, remark));
                        }
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
            }

            return AppContext.GetObjectDefinitionNames().Length;
        }

        public OES.Logger.ILog Logger { get; set; }
    }
}
