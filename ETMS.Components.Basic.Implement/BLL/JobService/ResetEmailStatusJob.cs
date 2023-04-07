using System;
using System.Collections.Generic;

using ETMS.Components.Basic.API.Entity.Notify;
using ETMS.Components.Basic.Implement.BLL.Notify;
namespace ETMS.Components.Basic.Implement.BLL.JobService
{
    /// <summary>
    /// 重置邮件状态Job
    /// 情景描述：如果邮件发送失败，但属于可以通过重试方式再次发送可以解决的，则可以重置此邮件状态
    /// </summary>
    public class ResetEmailStatusJob : IJobService
    {
        private Notify_MessageLogic MessageLogic = new Notify_MessageLogic();
        private OrganizationEmailNotifyQueue EmailNotifyHelper = new OrganizationEmailNotifyQueue();
        private int PageSize = 100;

        public int DoJob()
        {
            int count = 0;
            int totalRecords = 0;
            /*
             * 采用小步快跑的策略:每次取100条发送失败的邮件，重置完再继续！
             */
            do
            {
                //1、提取失败邮件列表(一个月内)
                IList<Notify_Message> messages = MessageLogic.GetEntityList(1, this.PageSize, " MessageID ASC ", string.Format(" AND [MessageTypeID]=1 AND [Status]=2 AND [CreateTime]>='{0}'", DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd")), out totalRecords);//提取发送失败的邮件信息
                //2、循环发送，并记录发送结构
                foreach (Notify_Message item in messages)
                {
                    //if(item.Remark) 何种错误允许重置
                    {
                        item.Status = 0;
                        MessageLogic.Save(item);//更新
                    }
                }
                count += totalRecords;
            }
            while (totalRecords > 0);
            
            return count;
        }

        public OES.Logger.ILog Logger { get; set; }
    }
}
