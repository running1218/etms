using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

using ETMS.AppContext;
using ETMS.Components.Basic.API.Entity.ELearningMap;
using ETMS.Components.Basic.API;
using ETMS.Utility;
using ETMS.Components.Basic.API.Entity.Security;
using ETMS.Components.Basic.Implement.BLL.Security;

namespace ETMS.Components.Basic.Implement.BLL.ELearningMap
{
    /// <summary>
    /// 学习地图表业务逻辑
    /// </summary>
    public partial class Res_StudyMapLogic
    {
        /// <summary>
        /// 保存
        /// </summary>
        public void SaveStudyMap(Res_StudyMap res_StudyMap, OperationAction action)
        {
            try
            {
                if (action == OperationAction.Add)
                    Add(res_StudyMap);
                else if (action == OperationAction.Edit)
                    Save(res_StudyMap);
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string errorMsg = ex.Message;
                if (errorMsg.IndexOf("Index_U_StudyMapNameOrgID", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    throw new ETMS.AppContext.BusinessException(BizErrorDefine.ElearningMap_StudyMapNameOrgID);
                }
                else if (errorMsg.IndexOf("Index_U_OrgDepPostRank", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    throw new ETMS.AppContext.BusinessException(BizErrorDefine.ElearningMap_OrgDepPostRank);
                }
                else if (errorMsg.IndexOf("Index_U_StudyMapCode", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    throw new ETMS.AppContext.BusinessException("该学习地图的编码已经存在，请修改！");
                }
                else if (errorMsg.IndexOf("Index_U_StudyMapName", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    throw new ETMS.AppContext.BusinessException("该学习地图的名称已经存在，请修改！");
                }
                throw new ETMS.AppContext.BusinessException(ex.Message);
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        public void RemoveCheck(Guid studyMapID)
        {
            try
            {
                DAL.Remove(studyMapID);
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                if (ex.Message.IndexOf("FK_RES_STUD_REFERENCE_RES_STUD") != -1)
                {
                    throw new ETMS.AppContext.BusinessException(BizErrorDefine.ElearningMap_FK_RES_STUD_REFERENCE_RES_STUD);
                }
            }
        }

        /// <summary>
        /// 获取组织机构下的学习地图
        /// </summary>
        /// <returns></returns>
        public List<Res_StudyMap> GetStudyMapByOrganizationID()
        {
             return DAL.GetStudyMapByOrganizationID(UserContext.Current.OrganizationID).ToList<Res_StudyMap>();             
        }

        public List<Res_StudyMap> GetAllStudentStudyMap()
        {
            List<Res_StudyMap> source = GetStudyMapByOrganizationID();
            User user = new UserLogic().GetUserByID(UserContext.Current.UserID);
            Site_Student student = new Site_StudentLogic().GetStudentById(UserContext.Current.UserID);
            List<Res_StudyMap> studyMap = null;

            if (null != student)
            {
                studyMap = source.Where(s => s.DeptID.Equals(user.DepartmentID) & s.PostID.Equals(student.PostID)).ToList();

                if (null == studyMap || studyMap.Count == 0)
                {
                    studyMap = source.Where(s => s.PostID.Equals(student.PostID)).ToList();

                    if (null == studyMap || studyMap.Count == 0)
                    {
                        studyMap = source.Where(s => s.DeptID.Equals(user.DepartmentID)).ToList();
                    }
                }
            }

            return studyMap;
        }

        /// <summary>
        /// 获取学生学习地图
        /// </summary>
        /// <returns></returns>
        public Res_StudyMap GetStudentStudyMap()
        {
            User user = new UserLogic().GetUserByID(UserContext.Current.UserID);
            Site_Student student = new Site_StudentLogic().GetStudentById(UserContext.Current.UserID);
            List<Res_StudyMap> source = GetStudyMapByOrganizationID();            
            Res_StudyMap studyMap = null;

            if (null != student)
            {
                studyMap = source.SingleOrDefault(s => s.DeptID.Equals(user.DepartmentID) & s.RankID.Equals(student.RankID) & s.PostID.Equals(student.PostID));

                if (null == studyMap)
                {
                    studyMap = source.FirstOrDefault(s => s.RankID.Equals(student.RankID) & s.PostID.Equals(student.PostID));

                    if (null == studyMap)
                    {
                        studyMap = source.FirstOrDefault(s => s.DeptID.Equals(user.DepartmentID) & s.RankID.Equals(student.RankID));
                    }
                }
            }
            
            return studyMap;
        }

        /// <summary>
        /// 获取学生地图课程
        /// </summary>
        /// <param name="studyMapID"></param>
        /// <returns></returns>
        public List<Res_StudyMapCourse> GetStudentStudyMapCourse(Guid studyMapID)
        {
            var source = DAL.GetStudyMapCourse(studyMapID).ToList<Res_StudyMapCourse>();
            var studyCourseStatus = new Res_StudyMapReferCourseLogic().GetStudyMapCourseStatus();
            foreach (var enity in source)
            {
                var studyTime = studyCourseStatus.SingleOrDefault(s => s.CourseID.Equals(enity.CourseID));
                if (null != studyTime)
                {
                    enity.StudyMapDesc = studyTime.StudyTimes > 0 ? "已学习" : "学习中";
                }
                else
                {
                    enity.StudyMapDesc = "未学习";
                }
            }

            return source;
        }

        /// <summary>
        /// 查询学习地图列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="criteria"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public List<Res_StudyMap> GetStudyMapList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            var source = GetPagedList(pageIndex, pageSize, sortExpression, criteria, out totalRecords).ToList<Res_StudyMap>();
            var dataSource = DAL.GetStudyMapCourseAndDataNum(UserContext.Current.OrganizationID).ToList<Res_StudyMap>();

            foreach (var entity in source)
            {
                var data = dataSource.SingleOrDefault(d => d.StudyMapID.Equals(entity.StudyMapID));
                if (null != data)
                {
                    entity.CourseNum = data.CourseNum;
                    entity.DataNum = data.DataNum;
                }
            }

            return source;
        }

        /// <summary>
        /// 查询学习地图列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpression"></param>
        /// <param name="criteria"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public Res_StudyMap GetStudyCourseAndDataNum(Guid studyMapID)
        {
            var dataSource = DAL.GetStudyMapCourseAndDataNum(UserContext.Current.OrganizationID).ToList<Res_StudyMap>();
            var entity = dataSource.SingleOrDefault(d => d.StudyMapID.Equals(studyMapID));
            return entity;
        }

        /// <summary>
        /// 查询学习地图列表
        /// </summary>
        /// <returns></returns>
        public List<Res_StudyMap> GetStudyCourseAndDataNum()
        {
            return DAL.GetStudyMapCourseAndDataNum(UserContext.Current.OrganizationID).ToList<Res_StudyMap>();
        }

        /// <summary>
        /// 获取学习地图非课程已选资料列表
        /// </summary>
        /// <param name="studyMapID"></param>
        /// <returns></returns>
        public List<Res_StudyMapCourse> GetMapDataList(Guid studyMapID)
        {
            var source = GetStudentStudyMapCourse(studyMapID).ToList<Res_StudyMapCourse>();
            var studyCourseStatus = new Res_StudyMapReferCourseLogic().GetStudyMapCourseStatus();
            foreach (var enity in source)
            {
                var studyTime = studyCourseStatus.SingleOrDefault(s => s.CourseID.Equals(enity.CourseID));
                if (null != studyTime)
                {
                    enity.StudyMapDesc = studyTime.StudyTimes > 0 ? "已学习" : "学习中";
                }
                else
                {
                    enity.StudyMapDesc = "未学习";
                }
            }

            return source;
        }
    }
}