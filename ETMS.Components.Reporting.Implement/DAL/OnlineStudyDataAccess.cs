using System;
using System.Data;
using ETMS.Components.Reporting.API.Entity;
using System.Data.SqlClient;
using ETMS.Utility.Data;

namespace ETMS.Components.Reporting.Implement.DAL
{
    public partial class OnlineStudyDataAccess
    {
        /// <summary>
        /// 在线学习课程信息
        /// </summary>
        /// <param name="organizationID"></param>
        /// <returns></returns>
        public DataTable GetOnlineStudyDetails(OnlineItemCourse entity, int userOrganizationID)
        {
            return GetData(entity, userOrganizationID, "Pr_Report_GetOnlineStudyDetails");
        }

        /// <summary>
        /// 在线学习课程考试信息
        /// </summary>
        /// <param name="organizationID"></param>
        /// <returns></returns>
        public DataTable GetOnlineStudyTestDetails(OnlineItemCourse entity, int userOrganizationID)
        {
            return GetData(entity, userOrganizationID, "Pr_Report_GetOnlineStudyTestDetails");
        }

        /// <summary>
        /// 在线学习课程课件信息
        /// </summary>
        /// <param name="organizationID"></param>
        /// <returns></returns>
        public DataTable GetOnlineStudyCourseWareDetails(OnlineItemCourse entity, int userOrganizationID)
        {
            return GetData(entity, userOrganizationID, "Pr_Report_GetOnlineStudyCourseWareDetails");
        }

        /// <summary>
        /// 在线学习课程汇总信息
        /// </summary>
        /// <param name="organizationID"></param>
        /// <returns></returns>
        public DataTable GetOnlineStudyStatistics(OnlineItemCourse entity, int userOrganizationID)
        {
            return GetData(entity, userOrganizationID, "Pr_Report_GetOnlineStudyStatisticsDetail");
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="userOrganizationID"></param>
        /// <param name="produrceName"></param>
        /// <returns></returns>
        private DataTable GetData(OnlineItemCourse entity, int userOrganizationID, string produrceName)
        {
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, produrceName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@ItemName", SqlDbType.NVarChar),
				    new SqlParameter("@ItemCourseName", SqlDbType.NVarChar),
                    new SqlParameter("@ItemCourseAttrID", SqlDbType.Int),                   
                    new SqlParameter("@ItemCourseTypeID", SqlDbType.Int),
                    new SqlParameter("@ItemCourseBeginTime", SqlDbType.DateTime),
                    new SqlParameter("@ItemCourseEndTime", SqlDbType.DateTime),
                    new SqlParameter("@ItemCourseScoreStatus", SqlDbType.Int),
                    new SqlParameter("@OrganizationID", SqlDbType.Int),
                    new SqlParameter("@DepartmentID", SqlDbType.Int),
                    new SqlParameter("@PostID", SqlDbType.Int),
                    new SqlParameter("@PostTypeID", SqlDbType.Int),
                    new SqlParameter("@RankID", SqlDbType.Int),
                    new SqlParameter("@RealName", SqlDbType.NVarChar),
                    new SqlParameter("@UserOrganizationID", SqlDbType.Int),
                    new SqlParameter("@ItemCourseTeachModelID",SqlDbType.Int)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, produrceName, parms);
            }

            parms[0].Value = entity.ItemName;
            parms[1].Value = entity.ItemCourseName;
            parms[2].Value = entity.ItemCourseAttrID;
            parms[3].Value = entity.ItemCourseTypeID;
            parms[4].Value = entity.ItemCourseBeginTime;
            parms[5].Value = entity.ItemCourseEndTime;
            parms[6].Value = entity.ItemCourseScoreStatus;
            parms[7].Value = entity.OrganizationID;
            parms[8].Value = entity.DepartmentID;
            parms[9].Value = entity.PostID;
            parms[10].Value = entity.PostTypeID;
            parms[11].Value = entity.RankID;
            parms[12].Value = entity.RealName;
            parms[13].Value = userOrganizationID;
            parms[14].Value = entity.ItemCourseTeachModelID;
            #endregion
            return SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, produrceName, parms).Tables[0];
        }

        /// <summary>
        /// 在线学习情况监控 分页查询
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="userOrganizationID"></param>
        /// <param name="produrceName"></param>
        /// <returns></returns>
        public DataTable GetOnlineStudyPageList(OnlineItemCourse entity, int userOrganizationID, int pageIndex, int pageSize, out int totalRecords)
        {
            string produrceName = "[Pr_Report_GetOnlineStudyPageList]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, produrceName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@ItemName", SqlDbType.NVarChar),
				    new SqlParameter("@ItemCourseName", SqlDbType.NVarChar),
                    new SqlParameter("@ItemCourseAttrID", SqlDbType.Int),                   
                    new SqlParameter("@ItemCourseTypeID", SqlDbType.Int),
                    new SqlParameter("@ItemCourseBeginTime", SqlDbType.DateTime),
                    new SqlParameter("@ItemCourseEndTime", SqlDbType.DateTime),
                    new SqlParameter("@ItemCourseScoreStatus", SqlDbType.Int),
                    new SqlParameter("@OrganizationID", SqlDbType.Int),
                    new SqlParameter("@DepartmentID", SqlDbType.Int),
                    new SqlParameter("@PostID", SqlDbType.Int),
                    new SqlParameter("@PostTypeID", SqlDbType.Int),
                    new SqlParameter("@RankID", SqlDbType.Int),
                    new SqlParameter("@RealName", SqlDbType.NVarChar),
                    new SqlParameter("@UserOrganizationID", SqlDbType.Int),
                    new SqlParameter("@ItemCourseTeachModelID",SqlDbType.Int),
                    new SqlParameter("@PageIndex",SqlDbType.Int),
                    new SqlParameter("@PageSize",SqlDbType.Int),
                    new SqlParameter("@Status",SqlDbType.Int),
                    new SqlParameter("@ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, String.Empty, DataRowVersion.Default, null)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, produrceName, parms);
            }

            parms[0].Value = entity.ItemName;
            parms[1].Value = entity.ItemCourseName;
            parms[2].Value = entity.ItemCourseAttrID;
            parms[3].Value = entity.ItemCourseTypeID;
            parms[4].Value = entity.ItemCourseBeginTime;
            parms[5].Value = entity.ItemCourseEndTime;
            parms[6].Value = entity.ItemCourseScoreStatus;
            parms[7].Value = entity.OrganizationID;
            parms[8].Value = entity.DepartmentID;
            parms[9].Value = entity.PostID;
            parms[10].Value = entity.PostTypeID;
            parms[11].Value = entity.RankID;
            parms[12].Value = entity.RealName;
            parms[13].Value = userOrganizationID;
            parms[14].Value = entity.ItemCourseTeachModelID;
            parms[15].Value = pageIndex;
            parms[16].Value = pageSize;
            parms[17].Value = entity.Status;
            #endregion
            DataTable tab=SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, produrceName, parms).Tables[0];
            totalRecords = (int)parms[18].Value;
            return tab;
        }

        /// <summary>
        /// 在线学习情况监控 无分页 导出数据用
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="userOrganizationID"></param>
        /// <param name="produrceName"></param>
        /// <returns></returns>
        public DataTable GetOnlineStudyList(OnlineItemCourse entity, int userOrganizationID)
        {
            string produrceName = "[Pr_Report_GetOnlineStudyList]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, produrceName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@ItemName", SqlDbType.NVarChar),
				    new SqlParameter("@ItemCourseName", SqlDbType.NVarChar),
                    new SqlParameter("@ItemCourseAttrID", SqlDbType.Int),                   
                    new SqlParameter("@ItemCourseTypeID", SqlDbType.Int),
                    new SqlParameter("@ItemCourseBeginTime", SqlDbType.DateTime),
                    new SqlParameter("@ItemCourseEndTime", SqlDbType.DateTime),
                    new SqlParameter("@ItemCourseScoreStatus", SqlDbType.Int),
                    new SqlParameter("@OrganizationID", SqlDbType.Int),
                    new SqlParameter("@DepartmentID", SqlDbType.Int),
                    new SqlParameter("@PostID", SqlDbType.Int),
                    new SqlParameter("@PostTypeID", SqlDbType.Int),
                    new SqlParameter("@RankID", SqlDbType.Int),
                    new SqlParameter("@RealName", SqlDbType.NVarChar),
                    new SqlParameter("@UserOrganizationID", SqlDbType.Int),
                    new SqlParameter("@ItemCourseTeachModelID",SqlDbType.Int),
                    new SqlParameter("@Status",SqlDbType.Int)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, produrceName, parms);
            }

            parms[0].Value = entity.ItemName;
            parms[1].Value = entity.ItemCourseName;
            parms[2].Value = entity.ItemCourseAttrID;
            parms[3].Value = entity.ItemCourseTypeID;
            parms[4].Value = entity.ItemCourseBeginTime;
            parms[5].Value = entity.ItemCourseEndTime;
            parms[6].Value = entity.ItemCourseScoreStatus;
            parms[7].Value = entity.OrganizationID;
            parms[8].Value = entity.DepartmentID;
            parms[9].Value = entity.PostID;
            parms[10].Value = entity.PostTypeID;
            parms[11].Value = entity.RankID;
            parms[12].Value = entity.RealName;
            parms[13].Value = userOrganizationID;
            parms[14].Value = entity.ItemCourseTeachModelID;
            parms[15].Value = entity.Status;
            #endregion
            DataTable tab = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, produrceName, parms).Tables[0];
            return tab;
        }

        /// <summary>
        /// 培训课程学习情况 分页查询
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="userOrganizationID"></param>
        /// <param name="produrceName"></param>
        /// <returns></returns>
        public DataTable GetTraningCourseLearnPageList(OnlineItemCourse entity, int userOrganizationID, int pageIndex, int pageSize, out int totalRecords)
        {
            string produrceName = "[Pr_Report_GetOnlineStudyStatisticsPageList]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, produrceName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@ItemName", SqlDbType.NVarChar),
				    new SqlParameter("@ItemCourseName", SqlDbType.NVarChar),
                    new SqlParameter("@ItemCourseAttrID", SqlDbType.Int),                   
                    new SqlParameter("@ItemCourseTypeID", SqlDbType.Int),
                    new SqlParameter("@ItemCourseBeginTime", SqlDbType.DateTime),
                    new SqlParameter("@ItemCourseEndTime", SqlDbType.DateTime),
                    new SqlParameter("@ItemCourseScoreStatus", SqlDbType.Int),
                    new SqlParameter("@OrganizationID", SqlDbType.Int),
                    new SqlParameter("@DepartmentID", SqlDbType.Int),
                    new SqlParameter("@PostID", SqlDbType.Int),
                    new SqlParameter("@PostTypeID", SqlDbType.Int),
                    new SqlParameter("@RankID", SqlDbType.Int),
                    new SqlParameter("@RealName", SqlDbType.NVarChar),
                    new SqlParameter("@UserOrganizationID", SqlDbType.Int),
                    new SqlParameter("@ItemCourseTeachModelID",SqlDbType.Int),
                    new SqlParameter("@PageIndex",SqlDbType.Int),
                    new SqlParameter("@PageSize",SqlDbType.Int),
                    new SqlParameter("@ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, String.Empty, DataRowVersion.Default, null)
				}; 
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, produrceName, parms);
            }

            parms[0].Value = entity.ItemName;
            parms[1].Value = entity.ItemCourseName;
            parms[2].Value = entity.ItemCourseAttrID;
            parms[3].Value = entity.ItemCourseTypeID;
            parms[4].Value = entity.ItemCourseBeginTime;
            parms[5].Value = entity.ItemCourseEndTime;
            parms[6].Value = entity.ItemCourseScoreStatus;
            parms[7].Value = entity.OrganizationID;
            parms[8].Value = entity.DepartmentID;
            parms[9].Value = entity.PostID;
            parms[10].Value = entity.PostTypeID;
            parms[11].Value = entity.RankID;
            parms[12].Value = entity.RealName;
            parms[13].Value = userOrganizationID;
            parms[14].Value = entity.ItemCourseTeachModelID;
            parms[15].Value = pageIndex;
            parms[16].Value = pageSize;
            #endregion
            DataTable tab = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, produrceName, parms).Tables[0];
            totalRecords = (int)parms[17].Value;
            return tab;
        }
    }
}
