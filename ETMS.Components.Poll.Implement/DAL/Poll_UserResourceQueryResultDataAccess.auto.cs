//==================================================================================================
//Version 1.0, auto-generated.
//Generated By running1218
//Date: 2012-4-17 15:53:40.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1)
//==================================================================================================

using System;
using System.Collections.Generic;
using System.Data;

using System.Data.SqlClient;
using ETMS.Utility.Data;

using ETMS.Components.Poll.API.Entity;

namespace ETMS.Components.Poll.Implement.DAL
{
    /// <summary>
    /// 数据访问
    /// </summary>
    public partial class Poll_UserResourceQueryResultDataAccess
    {
        /// <summary>
        /// 增加
        /// </summary>
        public void Add(Poll_UserResourceQueryResult poll_UserResourceQueryResult)
        {
            string commandName = "dbo.Pr_Poll_UserResourceQueryResult_Insert";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {	new SqlParameter("@BatchID", SqlDbType.Int, 4, ParameterDirection.Output, false, 0, 0, String.Empty, DataRowVersion.Default, null),
				
					new SqlParameter("@UserName", SqlDbType.VarChar, 50),
					new SqlParameter("@UserType", SqlDbType.VarChar, 10),
					new SqlParameter("@QueryID", SqlDbType.Int),
					new SqlParameter("@ResourceTypeCode", SqlDbType.VarChar, 2),
					new SqlParameter("@ResourceCode", SqlDbType.VarChar, 36),
					new SqlParameter("@CreateTime", SqlDbType.DateTime)
					};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            if (poll_UserResourceQueryResult.UserName != null) { parms[1].Value = poll_UserResourceQueryResult.UserName; } else { parms[1].Value = DBNull.Value; }
            if (poll_UserResourceQueryResult.UserType != null) { parms[2].Value = poll_UserResourceQueryResult.UserType; } else { parms[2].Value = DBNull.Value; }
            parms[3].Value = poll_UserResourceQueryResult.QueryID;
            if (poll_UserResourceQueryResult.ResourceTypeCode != null) { parms[4].Value = poll_UserResourceQueryResult.ResourceTypeCode; } else { parms[4].Value = DBNull.Value; }
            if (poll_UserResourceQueryResult.ResourceCode != null) { parms[5].Value = poll_UserResourceQueryResult.ResourceCode; } else { parms[5].Value = DBNull.Value; }
            parms[6].Value = poll_UserResourceQueryResult.CreateTime;
            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);

            poll_UserResourceQueryResult.BatchID = (Int32)parms[0].Value;

        }

        /// <summary>
        /// 删除
        /// </summary>
        public void Remove(Int32 batchID)
        {
            string commandName = "dbo.Pr_Poll_UserResourceQueryResult_Delete";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@BatchID", SqlDbType.Int)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = batchID;

            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
        }

        /// <summary>
        /// 保存
        /// </summary>
        public void Save(Poll_UserResourceQueryResult poll_UserResourceQueryResult)
        {
            string commandName = "dbo.Pr_Poll_UserResourceQueryResult_Update";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@BatchID", SqlDbType.Int),
					new SqlParameter("@UserName", SqlDbType.VarChar, 50),
					new SqlParameter("@UserType", SqlDbType.VarChar, 10),
					new SqlParameter("@QueryID", SqlDbType.Int),
					new SqlParameter("@ResourceTypeCode", SqlDbType.VarChar, 2),
					new SqlParameter("@ResourceCode", SqlDbType.VarChar, 36),
					new SqlParameter("@CreateTime", SqlDbType.DateTime)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = poll_UserResourceQueryResult.BatchID;
            if (poll_UserResourceQueryResult.UserName != null) { parms[1].Value = poll_UserResourceQueryResult.UserName; } else { parms[1].Value = DBNull.Value; }
            if (poll_UserResourceQueryResult.UserType != null) { parms[2].Value = poll_UserResourceQueryResult.UserType; } else { parms[2].Value = DBNull.Value; }
            parms[3].Value = poll_UserResourceQueryResult.QueryID;
            if (poll_UserResourceQueryResult.ResourceTypeCode != null) { parms[4].Value = poll_UserResourceQueryResult.ResourceTypeCode; } else { parms[4].Value = DBNull.Value; }
            if (poll_UserResourceQueryResult.ResourceCode != null) { parms[5].Value = poll_UserResourceQueryResult.ResourceCode; } else { parms[5].Value = DBNull.Value; }
            parms[6].Value = poll_UserResourceQueryResult.CreateTime;
            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
        }

        /// <summary>
        /// 根据标识获取对象
        /// </summary>
        public Poll_UserResourceQueryResult GetById(Int32 batchID)
        {
            Poll_UserResourceQueryResult poll_UserResourceQueryResult = null;

            string commandName = "dbo.Pr_Poll_UserResourceQueryResult_GetByPK";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@BatchID", SqlDbType.Int)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = batchID;

            #endregion
            using (SqlDataReader dataReader = SqlHelper.ExecuteReader(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms))
            {
                if (dataReader.Read())
                {
                    poll_UserResourceQueryResult = PopulatePoll_UserResourceQueryResultFromDataReader(dataReader);
                }
            }

            return poll_UserResourceQueryResult;
        }

        /// <summary>
        /// 根据参数获取对象列表（分页，可排序）
        /// </summary>
        public DataTable GetPagedList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            string commandName = "dbo.Pr_Poll_UserResourceQueryResult_GetPagedList";
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
        /// 根据参数获取对象列表（分页，可排序）
        /// </summary>
        public IList<Poll_UserResourceQueryResult> GetEntityList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            IList<Poll_UserResourceQueryResult> list = new List<Poll_UserResourceQueryResult>();
            string commandName = "dbo.Pr_Poll_UserResourceQueryResult_GetPagedList";
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
            using (SqlDataReader dataReader = SqlHelper.ExecuteReader(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms))
            {
                while (dataReader.Read())
                {
                    list.Add(PopulatePoll_UserResourceQueryResultFromDataReader(dataReader));
                }
            }
            totalRecords = (int)parms[4].Value;
            return list;
        }


        /// <summary>
        /// 从DataReader中读取数据到业务对象
        /// </summary>
        private Poll_UserResourceQueryResult PopulatePoll_UserResourceQueryResultFromDataReader(SqlDataReader reader)
        {
            Poll_UserResourceQueryResult poll_UserResourceQueryResult = new Poll_UserResourceQueryResult();

            int batchIDIndex = reader.GetOrdinal("BatchID");
            if (!reader.IsDBNull(batchIDIndex))
            {
                poll_UserResourceQueryResult.BatchID = reader.GetInt32(batchIDIndex);
            }

            int userNameIndex = reader.GetOrdinal("UserName");
            if (!reader.IsDBNull(userNameIndex))
            {
                poll_UserResourceQueryResult.UserName = reader.GetString(userNameIndex);
            }

            int userTypeIndex = reader.GetOrdinal("UserType");
            if (!reader.IsDBNull(userTypeIndex))
            {
                poll_UserResourceQueryResult.UserType = reader.GetString(userTypeIndex);
            }

            int queryIDIndex = reader.GetOrdinal("QueryID");
            if (!reader.IsDBNull(queryIDIndex))
            {
                poll_UserResourceQueryResult.QueryID = reader.GetInt32(queryIDIndex);
            }

            int resourceTypeCodeIndex = reader.GetOrdinal("ResourceTypeCode");
            if (!reader.IsDBNull(resourceTypeCodeIndex))
            {
                poll_UserResourceQueryResult.ResourceTypeCode = reader.GetString(resourceTypeCodeIndex);
            }

            int resourceCodeIndex = reader.GetOrdinal("ResourceCode");
            if (!reader.IsDBNull(resourceCodeIndex))
            {
                poll_UserResourceQueryResult.ResourceCode = reader.GetString(resourceCodeIndex);
            }

            int createTimeIndex = reader.GetOrdinal("CreateTime");
            if (!reader.IsDBNull(createTimeIndex))
            {
                poll_UserResourceQueryResult.CreateTime = reader.GetDateTime(createTimeIndex);
            }

            return poll_UserResourceQueryResult;
        }
    }
}
