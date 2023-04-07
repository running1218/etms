using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

using ETMS.Utility.Logging;
using ETMS.Components.Basic.API.Entity.Course;
using ETMS.Components.Basic.API;
using ETMS.AppContext;
using ETMS.Utility;

using ETMS.Components.Basic.Implement.BLL.ELearningMap;

namespace ETMS.Components.Basic.Implement.BLL.Course
{
    public partial class Res_CourseLogic
    {
        /// <summary>
        /// 增加
        /// </summary>
        public void Add(Res_Course res_Course)
        {
            DAL.Add(res_Course);           
            BizLogHelper.AddOperate(res_Course);
        }

        public void Save(Res_Course course, OperationAction action)
        {
            try
            {
                if (action == OperationAction.Add)
                {
                    Add(course);                   
                }

                else if (action == OperationAction.Edit)
                {
                    Update(course);
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                if (ex.Message.IndexOf("Index_U_CourseCode", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    throw new ETMS.AppContext.BusinessException(BizErrorDefine.Resource_Course_AddFailed_CouseCodeExists);
                }
                else if (ex.Message.IndexOf("Index_U_CourseName", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    throw new ETMS.AppContext.BusinessException(BizErrorDefine.Resource_Course_AddFailed_CouseNameExists);
                }
                else if (ex.Message.IndexOf("CourseBBS.dbo.dnt_e_course", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    throw new ETMS.AppContext.BusinessException("将课程信息同步到课程论坛（CourseBBS）出错，请与系统管理员联系！");
                }
                throw;
            }
        }
        /// <summary>
        /// 保存
        /// </summary>
        public void Update(Res_Course res_Course)
        {
            Res_Course originalEntity = GetById(res_Course.CourseID);
            DAL.Save(res_Course);            
            BizLogHelper.UpdateOperate(originalEntity, res_Course);
        }

        /// <summary>
        /// 根据标识获取对象
        /// </summary>
        public Res_Course GetById(Guid courseID)
        {
            return DAL.GetById(courseID);
        }

        /// <summary>
        /// 获取组织结构下的课程
        /// </summary>
        /// <param name="orgID"></param>
        /// <returns></returns>
        public DataTable GetCourseByOrgID(int orgID)
        {
            return DAL.GetCourseByOrgID(orgID);
        }

        public DataTable GetCourseTypesByOrgID(int orgID)
        {
            return DAL.GetCourseTypesByOrgID(orgID);
        }
        
        /// <summary>
        /// 获取组织结构下的课程
        /// </summary>
        /// <param name="orgID"></param>
        /// <returns></returns>
        public List<Res_Course> GetCourseByOrgID()
        {
            return GetCourseByOrgID(UserContext.Current.OrganizationID).ToList<Res_Course>();
        }

        /// <summary>
        /// 删除
        /// </summary>
        public void Remove(Guid courseID)
        {
            try
            {
                Res_Course entity = GetById(courseID);
                DAL.Remove(courseID);
                BizLogHelper.DeleteOperate(entity);
            }
            catch (Exception ex)
            {
                if (ex.Message.IndexOf("FK_", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    throw new ETMS.AppContext.BusinessException(string.Format(BizErrorDefine.Resource_Course_DeleteFailed_IsUsing, GetById(courseID).CourseName));
                }
            }
        }

        
        /// <summary>
        /// 根据标识获取对象
        /// </summary>
        public int GetStudyTimeByUserID(Guid TrainingItemCourseID, int UserID)
        {
            return DAL.GetStudyTimeByUserID(TrainingItemCourseID, UserID);
        }
         
        #region 课程与其他对象的业务逻辑关系


        public DataTable GetCourseNotInListToTeacherCourseByObjectID(string ObjectRefID, int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            return GetCourseNotInListByObjectID(ObjectCourseRelation.TeacherCourse, ObjectRefID, pageIndex, pageSize, sortExpression, criteria, out  totalRecords);

        }



        public DataTable GetCourseNotInListToItemCourseByObjectID(string ObjectRefID, int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            return GetCourseNotInListByObjectID(ObjectCourseRelation.ItemCourse, ObjectRefID, pageIndex, pageSize, sortExpression, criteria, out  totalRecords);

        }


        public DataTable GetCourseNotInListToStudyMapByObjectID(string ObjectRefID, int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            return GetCourseNotInListByObjectID(ObjectCourseRelation.StudyMapReferCourse, ObjectRefID, pageIndex, pageSize, sortExpression, criteria, out  totalRecords);

        }
        /// <summary>
        /// 获取某一对象下未关联的课程列表
        /// </summary>
        /// <param name="ObjectRefType">业务类型</param>
        /// <param name="ObjectRefID">需关联的对象</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="criteria"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public DataTable GetCourseNotInListByObjectID(ObjectCourseRelation ObjectRefType, string ObjectRefID, int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            switch (ObjectRefType)
            {
                case ObjectCourseRelation.StudyMapReferCourse:
                    criteria += string.Format(" And CourseID not in( select courseID from Res_StudyMapReferCourse where StudyMapID='{0}')",ObjectRefID);
                    break;
                case ObjectCourseRelation.TeacherCourse:
                    criteria += string.Format(" And CourseID not in(select courseID from Res_TeacherCourse where TeacherID='{0}')", ObjectRefID);
                    break;
                case ObjectCourseRelation.ItemCourse:
                    criteria += string.Format(" AND CourseStatus=1 AND CourseID not in(select CourseID from dbo.Tr_ItemCourse where TrainingItemID='{0}')", ObjectRefID);
                    break;
                case ObjectCourseRelation.RecommendCourse:
                    criteria += string.Format(" AND CourseStatus=1 AND CourseID not in(select CourseID from dbo.Recommend_Course where orgid ={0})", UserContext.Current.OrganizationID);
                    break;
                default:
                    break;
            }
            return DAL.GetQueryList(pageIndex, pageSize, sortExpression, criteria, out totalRecords);
        }

        /// <summary>
        /// 建议对象与课程关系，批量的
        /// </summary>
        /// <param name="ObjectRefType">业务类型</param>
        /// <param name="ObjectRefID">需关联的对象</param>
        /// <param name="CourseIDBatch">课程ID字符串，以逗号分隔</param>
        public void ObjectCourseBatchAdd(ObjectCourseRelation ObjectRefType, string ObjectRefID, string CourseIDBatch)
        {
            switch (ObjectRefType)
            {
                case ObjectCourseRelation.StudyMapReferCourse:
                    Res_StudyMapReferCourseLogic studyMapRefLogic = new Res_StudyMapReferCourseLogic();

                    studyMapRefLogic.StudyMapReferCourseInsertBatch(CourseIDBatch, new Guid(ObjectRefID), ETMS.AppContext.UserContext.Current.UserID, ETMS.AppContext.UserContext.Current.RealName);

                    break;
                case ObjectCourseRelation.TeacherCourse:
                   
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 查询分页数据,含开放机构课程
        /// </summary>
        /// <param name="pageIndex">页号</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="sortExpression">排序条件</param>
        /// <param name="criteria">筛选条件</param>
        /// <param name="totalRecords">out 记录总数</param>
        /// <returns>返回查询结果</returns>
        public DataTable GetQueryList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            return DAL.GetQueryList(pageIndex, pageSize, sortExpression, criteria, out totalRecords);
        }

        public DataTable GetErrorResourcePageList(int orgID, int pageIndex, int pageSize, out int totalRecords)
        {
            return DAL.GetErrorResourcePageList(orgID, pageIndex, pageSize, out totalRecords);
        }

        public void Sort(Guid courseID, int sort)
        {
            DAL.Sort(courseID, sort);
        }
        #endregion
    }
}
