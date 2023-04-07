using ETMS.Components.ExOnlineTest.Implement.DAL;
using System;
using System.Data;

namespace ETMS.Components.ExOnlineTest.Implement.BLL
{
    public partial class Ex_StudentEvaluationLogic
    {
        Ex_StudentEvaluationDataAccess DAL = new Ex_StudentEvaluationDataAccess();
        /// <summary>
        /// 获取学生测评结果【在线测试和在线作业】 
        /// </summary>
        /// <param name="userID">学员编号</param>
        /// <param name="ItemCourseID">项目课程ID</param>
        /// <returns></returns>
        public DataSet GetStudentEvaluationListByUserID(int UserID, Guid ItemCourseID)
        {
            return DAL.GetStudentEvaluationListByUserID(UserID, ItemCourseID);
        }
        /// <summary>
        /// 获取学生测评结果【在线测试和在线作业】 
        /// </summary>
        /// <param name="userID">学员编号</param>
        /// <returns></returns>
        public DataTable GetStudentEvaluationListByUserID(int UserID)
        {
            return DAL.GetStudentEvaluationListByUserID(UserID);
        }
        /// <summary>
        /// 获取学生测评结果【在线测试和在线作业】 
        /// </summary>
        /// <param name="userID">学员编号</param>
        /// <param name="ItemCourseID">项目课程ID</param>
        /// <returns></returns>
        public DataTable GetStudentEvaluationListByItemCourseIDAndUserID(int UserID, Guid ItemCourseID)
        {
            return DAL.GetStudentEvaluationListByItemCourseIDAndUserID(UserID, ItemCourseID);
        }
    }
}
