using System;
using System.Data;

using System.Data.SqlClient;
using ETMS.Utility.Data;
using ETMS.Components.Basic.API.Entity.Course.Resources;

namespace ETMS.Components.Basic.Implement.DAL.Course.Resources
{
    /// <summary>
    /// 课程资源表数据访问
    /// </summary>
    public partial class Res_CourseResDataAccess
    {
        /// <summary>
        /// 根据标识获取对象
        /// </summary>
        public Res_CourseRes getCourseResByResID(string ResID, int ResTypeID)
        {
            Res_CourseRes res_CourseRes = null;

            string commandName = "dbo.Pr_Res_CourseRes_GetByResID";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@ResID", SqlDbType.VarChar),
                    new SqlParameter("@ResTypeID", SqlDbType.Int)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = ResID;
            parms[1].Value = ResTypeID;

            #endregion
            using (SqlDataReader dataReader = SqlHelper.ExecuteReader(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms))
            {
                if (dataReader.Read())
                {
                    res_CourseRes = PopulateRes_CourseResFromDataReader(dataReader);
                }
            }

            return res_CourseRes;
        }
        /// <summary>
        /// 根据课程资源ID和资源类型获取课程信息
        /// </summary>
        /// <param name="CourseID"></param>
        /// <param name="ResTypeID"></param>
        /// <returns></returns>
        public Res_CourseRes getCourseResByCourseID(Guid CourseID, int ResTypeID)
        {
            Res_CourseRes res_CourseRes = null;

            string commandName = "dbo.Pr_Res_CourseRes_GetByCourseID";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@CourseID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@ResTypeID", SqlDbType.Int)
                };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = CourseID;
            parms[1].Value = ResTypeID;

            #endregion
            using (SqlDataReader dataReader = SqlHelper.ExecuteReader(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms))
            {
                if (dataReader.Read())
                {
                    res_CourseRes = PopulateRes_CourseResFromDataReader(dataReader);
                }
            }

            return res_CourseRes;
        }
       
    }
}

