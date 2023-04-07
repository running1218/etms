using System;

using ETMS.Utility;
using ETMS.Components.Basic.API.Entity.Notify;
using ETMS.Components.Basic.Implement.BLL.TrainingItem;
using System.Data;
namespace ETMS.Components.Basic.Implement.BLL.JobService
{
    /// <summary>
    /// 培训项目开始提醒Job
    /// </summary>
    public class TrainingItemBeginNotifyJob : IJobService
    {
        Tr_ItemLogic TrainingItemLogic = new Tr_ItemLogic();

        /// <summary>
        /// 前几天提醒
        /// </summary>
        public int BeforeDay { get; set; }

        public int DoJob()
        {
            //获取项目学员数据
            DataTable dt = TrainingItemLogic.GetNoticeItemStudent(this.BeforeDay);
            Notify(dt);
            return dt.Rows.Count;
        }

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
                object bizContext = new { 
                    ItemName = Convert.ToString(row["ItemName"]), 
                    BeginTime = Convert.ToDateTime(row["ItemBeginTime"]).ToString("yyyy-MM-dd"),
                    DutyUser = Convert.ToString(row["DutyUser"]),
                    DutyEMAIL = Convert.ToString(row["DutyEMAIL"]),
                    DutyMobile = Convert.ToString(row["DutyMobile"])
                };

                //设置当前机构，用户环境信息
                ETMS.AppContext.UserContext.Current.OrganizationID = Convert.ToInt32(row["OrganizationID"]);
                ETMS.AppContext.UserContext.Current.UserID = 1;//默认机构管理员
                NotifyUtility.Notify(Notify_MessageClass.TrainingItemBeginNotify.MessageClassName, receiver, bizContext);
            }
        }
            
        public OES.Logger.ILog Logger { get; set; }
    }
}
