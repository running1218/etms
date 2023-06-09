//==================================================================================================
//Version 1.0, auto-generated.
//Generated By running1218
//Date: 2012-3-30 14:32:19.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1)
//==================================================================================================

using System;
using System.Data;

using System.Data.SqlClient;
using ETMS.Utility.Data;

using ETMS.Components.Basic.API.Entity.Security;

namespace ETMS.Components.Basic.Implement.DAL.Security
{

    /// <summary>
    /// 学员信息(用户扩展表)数据访问
    /// </summary>
    public partial class Site_StudentDataAccess
    {
        /// <summary>
        /// 增加
        /// </summary>
        public void Add(Site_Student site_Student)
        {
            string commandName = "dbo.Pr_Site_Student_Insert";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@UserID", SqlDbType.Int),
					new SqlParameter("@WorkerNo", SqlDbType.VarChar, 20),
					new SqlParameter("@RankID", SqlDbType.Int),
					new SqlParameter("@PostID", SqlDbType.Int),
					new SqlParameter("@Superior", SqlDbType.NVarChar, 100),
					new SqlParameter("@LastEducation", SqlDbType.NVarChar, 100),
					new SqlParameter("@Specialty", SqlDbType.NVarChar, 100),
					new SqlParameter("@JoinTime", SqlDbType.DateTime),
					new SqlParameter("@ResettlementWayID", SqlDbType.Int)
					};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = site_Student.UserID;
            if (site_Student.WorkerNo != null) { parms[1].Value = site_Student.WorkerNo; } else { parms[1].Value = DBNull.Value; }
            if (-1 !=site_Student.RankID && site_Student.RankID != default(int)) { parms[2].Value = site_Student.RankID; } else { parms[2].Value = DBNull.Value; }
            if (-1 != site_Student.PostID && site_Student.PostID != default(int)) { parms[3].Value = site_Student.PostID; } else { parms[3].Value = DBNull.Value; }            
            if (site_Student.Superior != null) { parms[4].Value = site_Student.Superior; } else { parms[4].Value = DBNull.Value; }
            if (site_Student.LastEducation != null) { parms[5].Value = site_Student.LastEducation; } else { parms[5].Value = DBNull.Value; }
            if (site_Student.Specialty != null) { parms[6].Value = site_Student.Specialty; } else { parms[6].Value = DBNull.Value; }
            parms[7].Value = site_Student.JoinTime;
            parms[8].Value = site_Student.ResettlementWayID;
            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);

        }

        /// <summary>
        /// 删除
        /// </summary>
        public void Remove(Int32 userID)
        {
            string commandName = "dbo.Pr_Site_Student_Delete";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@UserID", SqlDbType.Int)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = userID;

            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
        }

        /// <summary>
        /// 保存
        /// </summary>
        public void Save(Site_Student site_Student)
        {
            string commandName = "dbo.Pr_Site_Student_Update";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@UserID", SqlDbType.Int),
					new SqlParameter("@WorkerNo", SqlDbType.VarChar, 20),
					new SqlParameter("@RankID", SqlDbType.Int),
					new SqlParameter("@PostID", SqlDbType.Int),
					new SqlParameter("@Superior", SqlDbType.NVarChar, 100),
					new SqlParameter("@LastEducation", SqlDbType.NVarChar, 100),
					new SqlParameter("@Specialty", SqlDbType.NVarChar, 100),
					new SqlParameter("@JoinTime", SqlDbType.DateTime),
                    new SqlParameter("@ResettlementWayID", SqlDbType.Int)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = site_Student.UserID;
            if (site_Student.WorkerNo != null) { parms[1].Value = site_Student.WorkerNo; } else { parms[1].Value = DBNull.Value; }
            if (site_Student.RankID != -1 && site_Student.RankID != default(int)) { parms[2].Value = site_Student.RankID; } else { parms[2].Value = DBNull.Value; }
            if (site_Student.PostID != -1 && site_Student.PostID != default(int)) { parms[3].Value = site_Student.PostID; } else { parms[3].Value = DBNull.Value; }
            if (site_Student.Superior != null) { parms[4].Value = site_Student.Superior; } else { parms[4].Value = DBNull.Value; }
            if (site_Student.LastEducation != null) { parms[5].Value = site_Student.LastEducation; } else { parms[5].Value = DBNull.Value; }
            if (site_Student.Specialty != null) { parms[6].Value = site_Student.Specialty; } else { parms[6].Value = DBNull.Value; }
            parms[7].Value = site_Student.JoinTime;
            parms[8].Value = site_Student.ResettlementWayID;
            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
        }

        /// <summary>
        /// 根据标识获取对象
        /// </summary>
        public Site_Student GetById(Int32 userID)
        {
            Site_Student site_Student = null;

            string commandName = "dbo.Pr_Site_Student_GetByPK";
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
            using (SqlDataReader dataReader = SqlHelper.ExecuteReader(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms))
            {
                if (dataReader.Read())
                {
                    site_Student = PopulateSite_StudentFromDataReader(dataReader);
                }
            }

            return site_Student;
        }

        /// <summary>
        /// 根据参数获取对象列表（分页，可排序）
        /// </summary>
        public DataTable GetPagedList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            string commandName = "dbo.Pr_Site_Student_GetPagedList";
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
        private Site_Student PopulateSite_StudentFromDataReader(SqlDataReader reader)
        {
            Site_Student site_Student = new Site_Student();

            int userIDIndex = reader.GetOrdinal("UserID");
            if (!reader.IsDBNull(userIDIndex))
            {
                site_Student.UserID = reader.GetInt32(userIDIndex);
            }

            int workerNoIndex = reader.GetOrdinal("WorkerNo");
            if (!reader.IsDBNull(workerNoIndex))
            {
                site_Student.WorkerNo = reader.GetString(workerNoIndex);
            }

            int rankIDIndex = reader.GetOrdinal("RankID");
            if (!reader.IsDBNull(rankIDIndex))
            {
                site_Student.RankID = reader.GetInt32(rankIDIndex);
            }

            int postIDIndex = reader.GetOrdinal("PostID");
            if (!reader.IsDBNull(postIDIndex))
            {
                site_Student.PostID = reader.GetInt32(postIDIndex);
            }

            int superiorIndex = reader.GetOrdinal("Superior");
            if (!reader.IsDBNull(superiorIndex))
            {
                site_Student.Superior = reader.GetString(superiorIndex);
            }

            int lastEducationIndex = reader.GetOrdinal("LastEducation");
            if (!reader.IsDBNull(lastEducationIndex))
            {
                site_Student.LastEducation = reader.GetString(lastEducationIndex);
            }

            int specialtyIndex = reader.GetOrdinal("Specialty");
            if (!reader.IsDBNull(specialtyIndex))
            {
                site_Student.Specialty = reader.GetString(specialtyIndex);
            }

            int joinTimeIndex = reader.GetOrdinal("JoinTime");
            if (!reader.IsDBNull(joinTimeIndex))
            {
                site_Student.JoinTime = reader.GetDateTime(joinTimeIndex);
            }

            int resettlementWayIDIndex = reader.GetOrdinal("ResettlementWayID");
            if (!reader.IsDBNull(resettlementWayIDIndex))
            {
                site_Student.ResettlementWayID = reader.GetInt32(resettlementWayIDIndex);
            }

            return site_Student;
        }
    }
}
