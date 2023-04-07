using System;
using System.Collections.Generic;

using ETMS.Components.Basic.API.Entity.Notify;
using ETMS.Components.Basic.Implement.BLL.Notify;
namespace ETMS.Components.Basic.Implement.BLL.JobService
{
    /// <summary>
    /// 发送邮件Job
    /// </summary>
    public class SendEmailJob : IJobService
    {
        private Notify_MessageLogic MessageLogic = new Notify_MessageLogic();
        private OrganizationEmailNotifyQueue EmailNotifyHelper = new OrganizationEmailNotifyQueue();
        private int PageSize = 100;

        public int DoJob()
        {
            int count = 0;

            int totalRecords = 0;
            /*
             * 采用小步快跑的策略:每次取100条待发邮件，发送完再取!
             */
            do
            {
                //1、提取待发邮件列表
                IList<Notify_Message> messages = MessageLogic.GetEntityList(1, this.PageSize, " MessageID ASC ", " AND [MessageTypeID]=1 AND [Status]=0", out totalRecords);//提取未发送邮件信息
                //2、循环发送，并记录发送结构
                foreach (Notify_Message item in messages)
                {
                    try
                    {
                        //设置消息对应的机构ID，以便查找此机构对应的邮件配置信息
                        EmailNotifyHelper.OrganizationID = item.OrganizationID;
                        EmailNotifyHelper.Send(item.Receiver, item.Subject, item.Body, 0, null);
                        item.Status = 3;
                        item.Remark = "";
                    }
                    catch (Exception ex)
                    {
                        item.Status = 2;
                        item.Remark = ex.ToString();
                    }
                    finally
                    {
                        //更新数据库状态
                        MessageLogic.Save(item);
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
