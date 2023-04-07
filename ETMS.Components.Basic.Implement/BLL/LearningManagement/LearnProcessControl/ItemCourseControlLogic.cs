using System;
using System.Data;
using ETMS.Components.Basic.Implement.DAL.LearningManagement.LearnProcessControl;

namespace ETMS.Components.Basic.Implement.BLL.LearningManagement.LearnProcessControl
{
    /// <summary>
    /// 学习过程监控
    /// </summary>
    public class ItemCourseControlLogic
    {
        ItemCourseControlDataAccess dal = new ItemCourseControlDataAccess();
        /// <summary>
        /// 根据参数获取对象列表（分页，可排序）
        /// </summary>
        public DataTable GetPagedList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords) {
            return dal.GetPagedList(pageIndex, pageSize, sortExpression, criteria, out totalRecords);
        }

        
        /// <summary>
        /// 获取培训项目课程下开始学习人数与学习时长
        /// </summary>
        /// <param name="TrainingItemCourseID">项目课程ID</param>
        /// <returns></returns>
        public DataTable GetStudentNumSessionTime(Guid TrainingItemCourseID) {
            return dal.GetStudentNumSessionTime(TrainingItemCourseID);
        }

        /// <summary>
        /// 获取项目课程学员中已经开始学习的学员信息
        /// </summary>
        /// <param name="TrainingItemCourseID">项目课程ID</param>
        /// <returns></returns>
        public DataTable GetStudentOpenLearn(Guid TrainingItemCourseID) {
            return dal.GetStudentOpenLearn(TrainingItemCourseID);
        }
    }
}
