using System;

using System.Data;
using ETMS.Components.ExOnlineTest.Implement.DAL;
using ETMS.AppContext;

namespace ETMS.Components.ExOnlineTest.Implement.BLL
{
    public class Res_Student_OnLineTestLogic
    {
        Res_Student_OnlineTestDataAccess DAL = new Res_Student_OnlineTestDataAccess();

        /// <summary>
        /// 获取学员在线作业列表
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public DataTable GetOnlineTestListByUserID(Guid ItemID,Guid ItemCourseID)
        {
            return GetOnlineTestListByStudentID(UserContext.Current.UserID, ItemID, ItemCourseID);
        }



        /// <summary>
        /// 获取学员在线作业列表
        /// </summary>
        /// <param name="studentID">学员ID</param>
        /// <param name="ItemID">培训项目ID</param>
        /// <param name="ItemCourseID">培训项目课程ID</param>
        /// <returns></returns>
        public DataTable GetOnlineTestListByStudentID(int studentID, Guid ItemID, Guid ItemCourseID)
        {
            return DAL.GetOnlineTestListByUserID(studentID, ItemID, ItemCourseID);
        }




    }
}
