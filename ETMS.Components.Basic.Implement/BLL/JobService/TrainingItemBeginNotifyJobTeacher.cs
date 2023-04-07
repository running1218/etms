using System;
using System.Data;
using ETMS.Utility;
using ETMS.Components.Basic.API.Entity.Notify;
using ETMS.Components.Basic.Implement.BLL.TrainingItem.Course;
using ETMS.Components.Basic.Implement.BLL.TrainingItem;

namespace ETMS.Components.Basic.Implement.BLL.JobService
{
    /// <summary>
    /// 培训项目开始提醒教师Job
    /// </summary>
    public class TrainingItemBeginNotifyJobTeacher : IJobService
    {
        //Tr_ItemLogic TrainingItemLogic = new Tr_ItemLogic();

        /// <summary>
        /// 前几天提醒
        /// </summary>
        public int BeforeDay { get; set; }

        public int DoJob()
        {
            //获取项目学员数据
            //DataTable dt = TrainingItemLogic.GetNoticeItemStudent(this.BeforeDay);
            //Notify(dt);
            return 0;
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


                #region 课程列表
                DataTable tab = new Tr_ItemCourseLogic().GetItemCourseListByTrainingItemIDAndTeacherID(row["TrainingItemID"].ToGuid(), row["UserID"].ToInt());

                string courseList = string.Format("<table {0}><tr bgcolor='#cccccc'><td {1}>课程编码</td><td {1}>课程名称</td><td {1}>开始时间</td><td {1}>结束时间</td></tr>"
                    , "style='border: 1px solid black; border-collapse: collapse; width: 660px'"
                    , "style=' border: 1px solid black;'"
                    );
                foreach (DataRow r in tab.Rows)
                {
                    courseList += string.Format("<tr><td {0}>{1}</td><td {0}>{2}</td><td {0}>{3}</td><td {0}>{4}</td></tr>"
                        , "style=' border: 1px solid black;'"
                        , r["CourseCode"].ToString()
                        , r["CourseName"].ToString()
                        , r["CourseBeginTime"].ToDate()
                        , r["CourseEndTime"].ToDate());
                }
                courseList += "</table>";
                #endregion

                //业务变量定义
                object bizContext = new
                {
                    ItemName = new Tr_ItemLogic().GetById(row["TrainingItemID"].ToGuid()).ItemName,
                    CourseList = courseList
                };

                //设置当前机构，用户环境信息
                ETMS.AppContext.UserContext.Current.OrganizationID = Convert.ToInt32(row["OrganizationID"]);
                ETMS.AppContext.UserContext.Current.UserID = 1;//默认机构管理员
                NotifyUtility.Notify(Notify_MessageClass.TrainingItemBeginNotifyTeacher.MessageClassName, receiver, bizContext);
            }
        }

        public OES.Logger.ILog Logger { get; set; }
    }

}
