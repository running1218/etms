using System;
using System.Collections.Generic;
using System.Data;

using ETMS.AppContext;
using ETMS.Components.Basic.API.Entity.ELearningMap;
using ETMS.Components.Basic.API.Entity.TrainingItem.Course;
using ETMS.Utility;
namespace ETMS.Components.Basic.Implement.BLL.ELearningMap
{
    /// <summary>
    /// 学习地图与课程关系表业务逻辑
    /// </summary>
    public partial class Res_StudyMapReferCourseLogic
    {
        /// <summary>
        /// 获取学习地图下所有课程
        /// </summary>
        /// <param name="StudyMapID"></param>
        /// <returns></returns>
        public DataTable GetCourseListByStudyMapID(Guid StudyMapID, int pageIndex, int pageSize, string sortExpression, out int totalRecords)
        {
            return DAL.GetCourseListByStudyMapID(StudyMapID, pageIndex, pageSize, sortExpression, out totalRecords);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        public void Remove(Guid[] selectedValues)
        {
            foreach (Guid obj in selectedValues)
            {
                Remove(obj);
            }
        }

        /// <summary>
        /// 新增学习地图课程
        /// </summary>
        /// <param name="source"></param>
        public void Save(List<Res_StudyMapReferCourse> source)
        {
            foreach (var entity in source)
            {
                DAL.Add(entity);
            }
        }

        /// <summary>
        /// 批量建立学习地图和课程的关系
        /// </summary>
        /// <param name="CourseIDBatch"></param>
        /// <param name="StudyMapID"></param>
        /// <param name="UserID"></param>
        /// <param name="UserName"></param>
        public void StudyMapReferCourseInsertBatch(string CourseIDBatch, Guid StudyMapID, int UserID, string UserName)
        {
            DAL.StudyMapReferCourseInsertBatch(CourseIDBatch, StudyMapID, UserID, UserName);
        }

        /// <summary>
        /// 学习地图课程学习情况
        /// </summary>
        /// <returns></returns>
        public List<Tr_ItemCourse> GetStudyMapCourseStatus()
        {
            var source = (List<Tr_ItemCourse>) CacheHelper.Get(string.Format("{0}_{1}", "StudyMapCourseStatus", UserContext.Current.UserID));
            if (null == source)
                source = DAL.GetStudyMapCourseStatus(UserContext.Current.UserID).ToList<Tr_ItemCourse>();

            return source;
        }
    }
}

