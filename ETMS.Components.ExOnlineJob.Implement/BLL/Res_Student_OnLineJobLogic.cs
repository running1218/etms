using System;

using System.Data;
using ETMS.Components.ExOnlineJob.Implement.DAL;
using ETMS.AppContext;

namespace ETMS.Components.ExOnlineJob.Implement.BLL
{
    public class Res_Student_OnLineJobLogic
    {
        Res_Student_OnLineJobDataAccess DAL = new Res_Student_OnLineJobDataAccess();

       /// <summary>
        /// 获取学员在线作业列表
       /// </summary>
       /// <param name="ItemCourseID">项目课程ID</param>
       /// <returns></returns>
        public DataTable GetOnlineJobListByUserID(Guid ItemCourseID)
        {
            return DAL.GetOnlineJobListByUserID(UserContext.Current.UserID, ItemCourseID);
        }
    }
}
