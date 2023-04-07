using System;
using System.Data;

using System.Data.SqlClient;
using ETMS.Utility.Data;

namespace ETMS.Components.StudyClass.Implement.DAL.StudyClass
{
    /// <summary>
    /// 班级群组学员表数据访问
    /// </summary>
    public partial class Sty_ClassSubgroupStudentDataAccess
	{
        /// <summary>
        /// 获取我的班级项目列表
        /// </summary>
        public DataTable GetGroupStudentByGroupID(Guid classSubgroupID)
        {
            string commandName = "dbo.Pr_Sty_ClassSubgroupStudent_GetByGroupID";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@ClassSubgroupID", SqlDbType.UniqueIdentifier)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = classSubgroupID;

            #endregion

            return SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, commandName, parms).Tables[0];
        }

        /// <summary>
        /// 获取我的班级项目列表
        /// </summary>
        public DataTable GetGroupStudentByClassID(Guid classID)
        {
            string commandName = "dbo.Pr_Sty_ClassSubgroupStudent_GetByClassID";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@ClassID", SqlDbType.UniqueIdentifier)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = classID;

            #endregion

            return SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, commandName, parms).Tables[0];
        }

        /// <summary>
        /// 获取我的班级项目列表
        /// </summary>
        public DataTable GetGroupByClassStudentID(Guid classStudentID)
        {
            string commandName = "dbo.Pr_Sty_ClassSubgroupStudent_GetUserGroupByClassStudentID";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@ClassStudentID", SqlDbType.UniqueIdentifier)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = classStudentID;

            #endregion

            return SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, commandName, parms).Tables[0];
        }
	}
}
