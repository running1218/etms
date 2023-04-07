using System;
using System.Data;
using System.Data.SqlClient;
using ETMS.Utility.Data;

namespace ETMS.Components.Reporting.Implement.DAL
{
    public partial class StudentTrainingDetailsDataAccess
    {
        /// <summary>
        /// 查询培训学员明细报表（学员）
        /// </summary>
        public DataTable GetTrainingDetailsStudentList(int organizationID, DateTime itemBeginTime, DateTime itemEndTime, string realName, string workerNo, string departmentName, string postName)
        {
            string commandName = "Pr_Reporting_GetStudentTrainingDetails";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@OrganizationID", SqlDbType.Int),
				    new SqlParameter("@ItemBeginTime", SqlDbType.DateTime),
                    new SqlParameter("@ItemEndTime", SqlDbType.DateTime),
                    new SqlParameter("@RealName", SqlDbType.NVarChar),
                    new SqlParameter("@DepartmentName", SqlDbType.NVarChar),
                    new SqlParameter("@WorkerNo", SqlDbType.NVarChar),
                    new SqlParameter("@PostName", SqlDbType.NVarChar)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = organizationID;
            parms[1].Value = itemBeginTime;
            parms[2].Value = itemEndTime;
            parms[3].Value = realName;
            parms[4].Value = departmentName;
            parms[5].Value = workerNo;
            parms[6].Value = postName;
            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            return dt;
        }

        /// <summary>
        /// 查询培训学员明细报表（学员）
        /// </summary>
        public DataTable GetStudentTrainingDetailsExport(int organizationID, DateTime itemBeginTime, DateTime itemEndTime, string realName, string workerNo, string departmentName, string postName)
        {
            string commandName = "Pr_Reporting_GetStudentTrainingDetailsExport";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@OrganizationID", SqlDbType.Int),
				    new SqlParameter("@ItemBeginTime", SqlDbType.DateTime),
                    new SqlParameter("@ItemEndTime", SqlDbType.DateTime),
                    new SqlParameter("@RealName", SqlDbType.NVarChar),
                    new SqlParameter("@DepartmentName", SqlDbType.NVarChar),
                    new SqlParameter("@WorkerNo", SqlDbType.NVarChar),
                    new SqlParameter("@PostName", SqlDbType.NVarChar)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = organizationID;
            parms[1].Value = itemBeginTime;
            parms[2].Value = itemEndTime;
            parms[3].Value = realName;
            parms[4].Value = departmentName;
            parms[5].Value = workerNo;
            parms[6].Value = postName;
            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            return dt;
        }

        /// <summary>
        /// 查询培训学员明细报表（项目）
        /// </summary>
        public DataTable GetStudentTrainingItemList(int userID)
        {
            string commandName = "Pr_Reporting_GetItemListByUserID";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@UserID", SqlDbType.Int)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = userID;
            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            return dt;
        }

        /// <summary>
        /// 查询培训学员明细报表(课时)
        /// </summary>
        public DataTable GetStudentTrainingItemCourseHoursList(Guid trainingItemCourseID, Guid studentCourseID)
        {
            string commandName = "Pr_Reporting_GetItemCourseHoursListByTrainingItemCourseID";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@TrainingItemCourseID", SqlDbType.UniqueIdentifier)
                    ,new SqlParameter("@StudentCourse", SqlDbType.UniqueIdentifier)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = trainingItemCourseID;
            parms[1].Value = studentCourseID;
            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            return dt;
        }

        /// <summary>
        /// 学员学习课程列表
        /// </summary>
        /// <param name="userID">学员编号</param>
        /// <param name="topNum">返回记录数量</param>
        /// <returns></returns>
        public DataTable GetStudentCourseByUserID(int userID)
        {
            string commandName = "[dbo].[Pr_Reporting_GetItemStudyCoursesByUserID]";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@UserID", SqlDbType.Int)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = userID;
            #endregion
            return SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
        }

        /// <summary>
        /// 统计各机构（公司）的注册人数
        /// </summary>
        /// <param name="orgID"></param>
        /// <param name="stutentState"></param>
        /// <returns></returns>
        public DataTable GetStudentRegisterNumber(int orgID, int stuState) 
        {
            string commandName = "Pr_Reporting_GetStudentRegisterNumber";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@orgID", SqlDbType.Int),
                    new SqlParameter("@stutentState", SqlDbType.Int),
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = orgID;
            parms[1].Value = stuState;
            #endregion
            return SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
        }
    }
}
