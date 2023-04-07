using System;
using System.Collections.Generic;
using System.Linq;
using ETMS.Utility.BizCache;
using ETMS.Utility;
using ETMS.Components.Reporting.API.Entity;
using ETMS.Components.Reporting.Implement.DAL;
using ETMS.AppContext;
using System.Data;

namespace ETMS.Components.Reporting.Implement.BLL
{
    public partial class StudentTrainingDetailsLogic
    {
        private static readonly StudentTrainingDetailsDataAccess DAL = new StudentTrainingDetailsDataAccess();

        /// <summary>
        /// 学员培训明细
        /// </summary>
        /// <param name="itemBeginTime"></param>
        /// <param name="itemEndTime"></param>
        /// <param name="realName"></param>
        /// <param name="workerNo"></param>
        /// <param name="departmentName"></param>
        /// <param name="postName"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<StudentTrainingDetails> GetStudentTrainingDetails(DateTime itemBeginTime, DateTime itemEndTime, string realName, string workerNo, string departmentName, string postName, int pageIndex, int pageSize, out int totalRecords)
        {
            var source = DAL.GetTrainingDetailsStudentList(UserContext.Current.OrganizationID, itemBeginTime, itemEndTime, realName, workerNo, departmentName, postName).PageList<StudentTrainingDetails>(pageIndex, pageSize, out totalRecords);
            source.ForEach(f => f.TrainingItems = GetTrainingItems(f.UserID));
            return source;
        }

        /// <summary>
        /// 获取符合条件的全集
        /// </summary>
        /// <param name="itemBeginTime"></param>
        /// <param name="itemEndTime"></param>
        /// <param name="realName"></param>
        /// <param name="workerNo"></param>
        /// <param name="departmentName"></param>
        /// <param name="postName"></param>
        /// <returns></returns>
        public List<StudentTrainingDetails> GetStudentTrainingDetails(DateTime itemBeginTime, DateTime itemEndTime, string realName, string workerNo, string departmentName, string postName)
        {
            var source = DAL.GetTrainingDetailsStudentList(UserContext.Current.OrganizationID, itemBeginTime, itemEndTime, realName, workerNo, departmentName, postName).ToList<StudentTrainingDetails>();
            source.ForEach(f => f.TrainingItems = GetTrainingItems(f.UserID));
            return source;
        }


        /// <summary>
        /// 获取符合条件的全集
        /// </summary>
        /// <param name="itemBeginTime"></param>
        /// <param name="itemEndTime"></param>
        /// <param name="realName"></param>
        /// <param name="workerNo"></param>
        /// <param name="departmentName"></param>
        /// <param name="postName"></param>
        /// <returns></returns>
        public System.Data.DataTable GetStudentTrainingDetailsExport(DateTime itemBeginTime, DateTime itemEndTime, string realName, string workerNo, string departmentName, string postName)
        {
            return DAL.GetStudentTrainingDetailsExport(UserContext.Current.OrganizationID, itemBeginTime, itemEndTime, realName, workerNo, departmentName, postName);

        }

        /// <summary>
        /// 学员的培训项目
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        List<StudentTrainingItems> GetTrainingItems(int userID)
        {
            var items = DAL.GetStudentTrainingItemList(userID).ToList<StudentTrainingItems>();
            items.ForEach(f => f.TrainingItemCourses = GetTrainingItemCourses(f.TrainingItemID, userID));
            return items;
        }

        /// <summary>
        /// 获取项目下课程信息
        /// </summary>
        /// <param name="trainingItemID"></param>
        /// <returns></returns>
        List<StudentTrainingItemCourses> GetTrainingItemCourses(Guid trainingItemID, int userID)
        {
            var source = BizCacheHelper.GetCacheItem<List<StudentTrainingItemCourses>>(string.Format("{0}-{1}", trainingItemID, userID));
            if (null == source || source == default(List<StudentTrainingItemCourses>))
            {
                source = DAL.GetStudentCourseByUserID(userID).ToList<StudentTrainingItemCourses>();
            }
            source = source.Where(t => t.TrainingItemID.Equals(trainingItemID)).ToList<StudentTrainingItemCourses>();
            source.ForEach(f => f.TrainingItemCourseHours = GetTrainingItemCourseHours(f.TrainingItemCourseID, f.StudentCourse));
            return source;
        }

        /// <summary>
        /// 培训项目课程课时
        /// </summary>
        /// <param name="trainingItemCourseID"></param>
        /// <returns></returns>
        List<StudentTrainingItemCourseHours> GetTrainingItemCourseHours(Guid trainingItemCourseID, Guid sdudentCourseID)
        {
            var courseHours = DAL.GetStudentTrainingItemCourseHoursList(trainingItemCourseID, sdudentCourseID).ToList<StudentTrainingItemCourseHours>();
            return courseHours;
        }

        /// <summary>
        /// 统计各机构（公司）的注册人数
        /// </summary>
        /// <param name="orgID"></param>
        /// <param name="stutentState"></param>
        /// <returns></returns>
        public DataTable GetStudentRegisterNumber(int orgID, int stuState)
        {
            if (orgID == -1)
                orgID = ETMS.AppContext.UserContext.Current.OrganizationID;
            return DAL.GetStudentRegisterNumber(orgID, stuState);
        }
    }
}
