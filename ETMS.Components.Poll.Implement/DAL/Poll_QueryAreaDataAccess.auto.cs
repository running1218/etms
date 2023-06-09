//==================================================================================================
//Version 1.0, auto-generated.
//Generated By running1218
//Date: 2012-4-23 15:08:15.
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
    public partial class Poll_QueryAreaDataAccess
    {
        /// <summary>
        /// 增加
        /// </summary>
        public void Add(Poll_QueryArea poll_QueryArea)
        {
            string commandName = "dbo.Pr_Poll_QueryArea_Insert";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {	new SqlParameter("@QueryAreaID", SqlDbType.Int, 4, ParameterDirection.Output, false, 0, 0, String.Empty, DataRowVersion.Default, null),
				
					new SqlParameter("@AreaType", SqlDbType.VarChar, 20),
					new SqlParameter("@AreaCode", SqlDbType.VarChar, 50),
					new SqlParameter("@QueryPublishID", SqlDbType.Int),
					new SqlParameter("@Creator", SqlDbType.VarChar, 50),
					new SqlParameter("@CreateTime", SqlDbType.DateTime)
					};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            if (poll_QueryArea.AreaType != null) { parms[1].Value = poll_QueryArea.AreaType; } else { parms[1].Value = DBNull.Value; }
            if (poll_QueryArea.AreaCode != null) { parms[2].Value = poll_QueryArea.AreaCode; } else { parms[2].Value = DBNull.Value; }
            parms[3].Value = poll_QueryArea.QueryPublishID;
            if (poll_QueryArea.Creator != null) { parms[4].Value = poll_QueryArea.Creator; } else { parms[4].Value = DBNull.Value; }
            parms[5].Value = poll_QueryArea.CreateTime;
            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);

            poll_QueryArea.QueryAreaID = (Int32)parms[0].Value;

        }

        /// <summary>
        /// 删除
        /// </summary>
        public void Remove(Int32 queryAreaID)
        {
            string commandName = "dbo.Pr_Poll_QueryArea_Delete";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@QueryAreaID", SqlDbType.Int)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = queryAreaID;

            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
        }

        /// <summary>
        /// 保存
        /// </summary>
        public void Save(Poll_QueryArea poll_QueryArea)
        {
            string commandName = "dbo.Pr_Poll_QueryArea_Update";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@QueryAreaID", SqlDbType.Int),
					new SqlParameter("@AreaType", SqlDbType.VarChar, 20),
					new SqlParameter("@AreaCode", SqlDbType.VarChar, 50),
					new SqlParameter("@QueryPublishID", SqlDbType.Int),
					new SqlParameter("@Creator", SqlDbType.VarChar, 50),
					new SqlParameter("@CreateTime", SqlDbType.DateTime)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = poll_QueryArea.QueryAreaID;
            if (poll_QueryArea.AreaType != null) { parms[1].Value = poll_QueryArea.AreaType; } else { parms[1].Value = DBNull.Value; }
            if (poll_QueryArea.AreaCode != null) { parms[2].Value = poll_QueryArea.AreaCode; } else { parms[2].Value = DBNull.Value; }
            parms[3].Value = poll_QueryArea.QueryPublishID;
            if (poll_QueryArea.Creator != null) { parms[4].Value = poll_QueryArea.Creator; } else { parms[4].Value = DBNull.Value; }
            parms[5].Value = poll_QueryArea.CreateTime;
            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
        }

        /// <summary>
        /// 根据标识获取对象
        /// </summary>
        public Poll_QueryArea GetById(Int32 queryAreaID)
        {
            Poll_QueryArea poll_QueryArea = null;

            string commandName = "dbo.Pr_Poll_QueryArea_GetByPK";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@QueryAreaID", SqlDbType.Int)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = queryAreaID;

            #endregion
            using (SqlDataReader dataReader = SqlHelper.ExecuteReader(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms))
            {
                if (dataReader.Read())
                {
                    poll_QueryArea = PopulatePoll_QueryAreaFromDataReader(dataReader);
                }
            }

            return poll_QueryArea;
        }

        /// <summary>
        /// 根据参数获取对象列表（分页，可排序）
        /// </summary>
        public DataTable GetPagedList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            string commandName = "dbo.Pr_Poll_QueryArea_GetPagedList";
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
        public IList<Poll_QueryArea> GetEntityList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            IList<Poll_QueryArea> list = new List<Poll_QueryArea>();
            string commandName = "dbo.Pr_Poll_QueryArea_GetPagedList";
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
                    list.Add(PopulatePoll_QueryAreaFromDataReader(dataReader));
                }
            }
            totalRecords = (int)parms[4].Value;
            return list;
        }


        /// <summary>
        /// 删除调查范围的机构
        /// </summary>
        /// <param name="queryAreaID">区域编码</param>
        /// <returns></returns>
        public int DeleteUnOrg(int queryAreaID)
        {
            string commandName = "dbo.Pr_Poll_QueryArea_DeleteUnUser";
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@QueryAreaID", SqlDbType.Int)
									};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }
            parms[0].Value = queryAreaID;
            return SqlHelper.ExecuteNonQuery(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms);
        }


        /// <summary>
        /// 从DataReader中读取数据到业务对象
        /// </summary>
        private Poll_QueryArea PopulatePoll_QueryAreaFromDataReader(SqlDataReader reader)
        {
            Poll_QueryArea poll_QueryArea = new Poll_QueryArea();

            int queryAreaIDIndex = reader.GetOrdinal("QueryAreaID");
            if (!reader.IsDBNull(queryAreaIDIndex))
            {
                poll_QueryArea.QueryAreaID = reader.GetInt32(queryAreaIDIndex);
            }

            int areaTypeIndex = reader.GetOrdinal("AreaType");
            if (!reader.IsDBNull(areaTypeIndex))
            {
                poll_QueryArea.AreaType = reader.GetString(areaTypeIndex);
            }

            int areaCodeIndex = reader.GetOrdinal("AreaCode");
            if (!reader.IsDBNull(areaCodeIndex))
            {
                poll_QueryArea.AreaCode = reader.GetString(areaCodeIndex);
            }

            int queryPublishIDIndex = reader.GetOrdinal("QueryPublishID");
            if (!reader.IsDBNull(queryPublishIDIndex))
            {
                poll_QueryArea.QueryPublishID = reader.GetInt32(queryPublishIDIndex);
            }

            int creatorIndex = reader.GetOrdinal("Creator");
            if (!reader.IsDBNull(creatorIndex))
            {
                poll_QueryArea.Creator = reader.GetString(creatorIndex);
            }

            int createTimeIndex = reader.GetOrdinal("CreateTime");
            if (!reader.IsDBNull(createTimeIndex))
            {
                poll_QueryArea.CreateTime = reader.GetDateTime(createTimeIndex);
            }

            return poll_QueryArea;
        }
    }
}