using System;
using System.Data;
using ETMS.Utility;
using ETMS.Components.Basic.API.Entity.Notify;

namespace ETMS.Components.Basic.Implement.BLL.JobService
{
    public class QuestionnaireUnSubmitSendNotifyJob : IJobService
    {
        /// <summary>
        /// 项目提醒消息产生
        /// </summary>
        /// <param name="dt">UserID, OrganizationID, RealName, Email, MobilePhone, ItemName, ItemBeginTime, ItemEndTime</param>
        public static void Notify(DataTable dt)
        {
            //产生提醒
            foreach (DataRow row in dt.Rows)
            {
                //快到期的学员课程信息

                //接收者变量定义
                NotifyReceiver receiver = new NotifyReceiver()
                {
                    UserID = Convert.ToString(row["UserID"]),//学员ID
                    LoginName = Convert.ToString(row["LoginName"]),
                    Email = Convert.ToString(row["Email"]),
                    MobilePhone = Convert.ToString(row["MobilePhone"]),
                    UserName = Convert.ToString(row["RealName"]),//真实姓名
                };

                //业务变量定义
                object bizContext = new { QueryName = row["QueryName"].ToString(), QeuryEndTime = row["QeuryEndTime"].ToDateTime().ToString("yyyy-MM-dd"), DutyUser = row["DutyUser"].ToString(), SendDate = DateTime.Now.ToString("yyyy-MM-dd") };

                //设置当前机构，用户环境信息
                ETMS.AppContext.UserContext.Current.OrganizationID = Convert.ToInt32(row["OrganizationID"]);
                ETMS.AppContext.UserContext.Current.UserID = 1;//默认机构管理员
                NotifyUtility.Notify(Notify_MessageClass.SendEmailNotify.MessageClassName, receiver, bizContext);
            }
        }

        public OES.Logger.ILog Logger { get; set; }

        public int DoJob()
        {
            throw new NotImplementedException();
        }
    }
}
