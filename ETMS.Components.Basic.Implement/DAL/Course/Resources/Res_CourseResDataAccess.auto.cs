//==================================================================================================
//Version 1.0, auto-generated.
//Generated By xueyb.
//Date: 2012-4-1 19:52:25.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1), a product of ZhouYonghua(Zhou_Yonghua@163.com).
//==================================================================================================

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
        /// 增加
        /// </summary>
        public void Add(Res_CourseRes res_CourseRes)
        {
            string commandName = "dbo.Pr_Res_CourseRes_Insert";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@CourseResID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@CourseResTypeID", SqlDbType.Int),
					new SqlParameter("@CourseID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@ResName", SqlDbType.NVarChar, 200),
					new SqlParameter("@IsUse", SqlDbType.Int),
					new SqlParameter("@ResBeginTime", SqlDbType.DateTime),
					new SqlParameter("@ResEndTime", SqlDbType.DateTime),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUser", SqlDbType.NVarChar, 128),
					new SqlParameter("@CreateUserID", SqlDbType.Int),
					new SqlParameter("@ResID", SqlDbType.NVarChar, 100)
					};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = res_CourseRes.CourseResID;
            parms[1].Value = res_CourseRes.CourseResTypeID;
            parms[2].Value = res_CourseRes.CourseID;
            if (res_CourseRes.ResName != null) { parms[3].Value = res_CourseRes.ResName; } else { parms[3].Value = DBNull.Value; }
            parms[4].Value = res_CourseRes.IsUse;
            parms[5].Value = res_CourseRes.ResBeginTime;
            parms[6].Value = res_CourseRes.ResEndTime;
            parms[7].Value = res_CourseRes.CreateTime;
            if (res_CourseRes.CreateUser != null) { parms[8].Value = res_CourseRes.CreateUser; } else { parms[8].Value = DBNull.Value; }
            parms[9].Value = res_CourseRes.CreateUserID;
            if (res_CourseRes.ResID != null) { parms[10].Value = res_CourseRes.ResID; } else { parms[10].Value = DBNull.Value; }
            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);

        }

        /// <summary>
        /// 删除
        /// </summary>
        public void Remove(Guid courseResID)
        {
            string commandName = "dbo.Pr_Res_CourseRes_Delete";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@CourseResID", SqlDbType.UniqueIdentifier)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = courseResID;

            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
        }

        /// <summary>
        /// 保存
        /// </summary>
        public void Save(Res_CourseRes res_CourseRes)
        {
            string commandName = "dbo.Pr_Res_CourseRes_Update";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@CourseResID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@CourseResTypeID", SqlDbType.Int),
					new SqlParameter("@CourseID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@ResName", SqlDbType.NVarChar, 200),
					new SqlParameter("@IsUse", SqlDbType.Int),
					new SqlParameter("@ResBeginTime", SqlDbType.DateTime),
					new SqlParameter("@ResEndTime", SqlDbType.DateTime),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUser", SqlDbType.NVarChar, 128),
					new SqlParameter("@CreateUserID", SqlDbType.Int),
					new SqlParameter("@ResID", SqlDbType.NVarChar, 100)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = res_CourseRes.CourseResID;
            parms[1].Value = res_CourseRes.CourseResTypeID;
            parms[2].Value = res_CourseRes.CourseID;
            if (res_CourseRes.ResName != null) { parms[3].Value = res_CourseRes.ResName; } else { parms[3].Value = DBNull.Value; }
            parms[4].Value = res_CourseRes.IsUse;
            parms[5].Value = res_CourseRes.ResBeginTime;
            parms[6].Value = res_CourseRes.ResEndTime;
            parms[7].Value = res_CourseRes.CreateTime;
            if (res_CourseRes.CreateUser != null) { parms[8].Value = res_CourseRes.CreateUser; } else { parms[8].Value = DBNull.Value; }
            parms[9].Value = res_CourseRes.CreateUserID;
            if (res_CourseRes.ResID != null) { parms[10].Value = res_CourseRes.ResID; } else { parms[10].Value = DBNull.Value; }
            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
        }

        /// <summary>
        /// 根据标识获取对象
        /// </summary>
        public Res_CourseRes GetById(Guid courseResID)
        {
            Res_CourseRes res_CourseRes = null;

            string commandName = "dbo.Pr_Res_CourseRes_GetByPK";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@CourseResID", SqlDbType.UniqueIdentifier)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = courseResID;

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
        /// 根据参数获取对象列表（分页，可排序）
        /// </summary>
        public DataTable GetPagedList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            string commandName = "dbo.Pr_Res_CourseRes_GetPagedList";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@SortExpression", SqlDbType.VarChar),
					new SqlParameter("@Criteria", SqlDbType.VarChar),
					new SqlParameter("@ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, String.Empty, DataRowVersion.Default, null)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = pageIndex;
            parms[1].Value = pageSize;
            parms[2].Value = sortExpression;
            parms[3].Value = criteria;
            #endregion
            DataTable dt = SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
            totalRecords = (int)parms[4].Value;
            return dt;
        }

        /// <summary>
        /// 从DataReader中读取数据到业务对象
        /// </summary>
        private Res_CourseRes PopulateRes_CourseResFromDataReader(SqlDataReader reader)
        {
            Res_CourseRes res_CourseRes = new Res_CourseRes();

            int courseResIDIndex = reader.GetOrdinal("CourseResID");
            if (!reader.IsDBNull(courseResIDIndex))
            {
                res_CourseRes.CourseResID = reader.GetGuid(courseResIDIndex);
            }

            int courseResTypeIDIndex = reader.GetOrdinal("CourseResTypeID");
            if (!reader.IsDBNull(courseResTypeIDIndex))
            {
                res_CourseRes.CourseResTypeID = reader.GetInt32(courseResTypeIDIndex);
            }

            int courseIDIndex = reader.GetOrdinal("CourseID");
            if (!reader.IsDBNull(courseIDIndex))
            {
                res_CourseRes.CourseID = reader.GetGuid(courseIDIndex);
            }

            int resNameIndex = reader.GetOrdinal("ResName");
            if (!reader.IsDBNull(resNameIndex))
            {
                res_CourseRes.ResName = reader.GetString(resNameIndex);
            }

            int isUseIndex = reader.GetOrdinal("IsUse");
            if (!reader.IsDBNull(isUseIndex))
            {
                res_CourseRes.IsUse = reader.GetInt32(isUseIndex);
            }

            int resBeginTimeIndex = reader.GetOrdinal("ResBeginTime");
            if (!reader.IsDBNull(resBeginTimeIndex))
            {
                res_CourseRes.ResBeginTime = reader.GetDateTime(resBeginTimeIndex);
            }

            int resEndTimeIndex = reader.GetOrdinal("ResEndTime");
            if (!reader.IsDBNull(resEndTimeIndex))
            {
                res_CourseRes.ResEndTime = reader.GetDateTime(resEndTimeIndex);
            }

            int createTimeIndex = reader.GetOrdinal("CreateTime");
            if (!reader.IsDBNull(createTimeIndex))
            {
                res_CourseRes.CreateTime = reader.GetDateTime(createTimeIndex);
            }

            int createUserIndex = reader.GetOrdinal("CreateUser");
            if (!reader.IsDBNull(createUserIndex))
            {
                res_CourseRes.CreateUser = reader.GetString(createUserIndex);
            }

            int createUserIDIndex = reader.GetOrdinal("CreateUserID");
            if (!reader.IsDBNull(createUserIDIndex))
            {
                res_CourseRes.CreateUserID = reader.GetInt32(createUserIDIndex);
            }

            int resIDIndex = reader.GetOrdinal("ResID");
            if (!reader.IsDBNull(resIDIndex))
            {
                res_CourseRes.ResID = reader.GetString(resIDIndex);
            }

            return res_CourseRes;
        }
    }
}
