using System;
using System.Data;
using System.Data.SqlClient;
using ETMS.Utility.Data;
using ETMS.Components.Basic.API.Entity.Course;

namespace ETMS.Components.Basic.Implement.DAL.Course
{
    public class CourseOpenRangeDataAccess
    {
        /// <summary>
        /// 跟据课程ID 返回开放组织机构
        /// </summary>
        /// <param name="courseID">课程ID</param>
        /// <param name="orgCount">返回组织机构数量</param>
        /// <returns></returns>
        public DataTable GetList(Guid courseID, out int orgCount)
        {
            string commandName = "dbo.Pr_Res_CourseOpenRange_GetListByCourseID";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@CourseID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, String.Empty, DataRowVersion.Default, null)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = courseID;
            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            orgCount = (int)parms[1].Value;
            return dt;
        }

        /// <summary>
        /// 增加
        /// </summary>
        public void Add(Res_CourseOpenRange res_CourseOpen)
        {
            string commandName = "dbo.Pr_Res_CourseOpenRange_Insert";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@OpenRangeID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@CourseID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@OrgID", SqlDbType.Int),
					new SqlParameter("@CreateUser", SqlDbType.NVarChar),
					new SqlParameter("@CreateUserID", SqlDbType.Int)
					};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = res_CourseOpen.OpenRangeID;
            parms[1].Value = res_CourseOpen.CourseID;
            parms[2].Value = res_CourseOpen.OrgID;
            parms[3].Value = res_CourseOpen.CreateUser;
            parms[4].Value = res_CourseOpen.CreateUserID;
            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
        }

        /// <summary>
        /// 删除
        /// </summary>
        public void Remove(Guid courseID, int orgID)
        {
            string commandName = "dbo.Pr_Res_CourseOpenRange_Delete";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@CourseID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@OrgID", SqlDbType.Int)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = courseID;
            parms[1].Value = orgID;

            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
        }

    }
}
