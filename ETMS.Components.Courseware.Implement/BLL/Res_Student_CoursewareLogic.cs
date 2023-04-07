using System;

using System.Data;
using ETMS.Components.Courseware.Implement.DAL;
using ETMS.AppContext;

namespace ETMS.Components.Courseware.Implement.BLL
{
    public class Res_Student_CoursewareLogic
    {
        Res_Student_CoursewareDataAccess DAL = new Res_Student_CoursewareDataAccess();

        /// <summary>
        /// 获取学员在线课件列表
        /// </summary>
        /// <param name="ItemCourseID">项目课程ID</param>
        /// <returns></returns>
        public DataTable GetCoursewareListByUserID(Guid ItemCourseID)
        {
            return DAL.GetCoursewareListByUserID(UserContext.Current.UserID, ItemCourseID);
        }

        /// <summary>
        /// 跟据课程ID，UserID 获得学习时长、已学资源、全部资源
        /// </summary>
        /// <param name="userID">UserID</param>
        /// <param name="coursewareID">课程ID</param>
        /// <returns></returns>
        public DataTable GetCoursewareSessionTimeByCoursewareID( Guid coursewareID,Guid itemCourseResID)
        {
            return DAL.GetCoursewareSessionTimeByCoursewareID(UserContext.Current.UserID,itemCourseResID, coursewareID);
        }

    }
}
