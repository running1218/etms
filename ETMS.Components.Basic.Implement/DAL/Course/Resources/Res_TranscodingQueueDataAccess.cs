using ETMS.Components.Basic.API.Entity.Course.Resources;
using ETMS.Utility.Data;
using System;
using System.Data;
using System.Data.SqlClient;

namespace ETMS.Components.Basic.Implement.DAL.Course.Resources
{
    public partial class Res_TranscodingQueueDataAccess
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="resContent">业务实体</param>
		public void Add(Res_TranscodingQueue resContent)
        {
            string commandName = "dbo.Pr_Res_TranscodingQueue_Insert";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@TranscodingQueueID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@ContentID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@StreamCode", SqlDbType.NVarChar),
                    new SqlParameter("@CreateTime", SqlDbType.DateTime),
                    new SqlParameter("@TranscodingCount", SqlDbType.Int)
                };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }
            parms[0].Value = resContent.TranscodingQueueID;
            parms[1].Value = resContent.ContentID;
            parms[2].Value = resContent.StreamCode;
            parms[3].Value = resContent.CreateTime;
            parms[4].Value = resContent.TranscodingCount;
            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);

        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="resContent">业务实体</param>
		public void Update(Res_TranscodingQueue resContent)
        {
            string commandName = "dbo.Pr_Res_TranscodingQueue_Update";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@TranscodingQueueID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@ContentID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@StreamCode", SqlDbType.NVarChar),
                    new SqlParameter("@TranscodingCount", SqlDbType.Int)
                };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }
            parms[0].Value = resContent.TranscodingQueueID;
            parms[1].Value = resContent.ContentID;
            parms[2].Value = resContent.StreamCode;
            parms[3].Value = resContent.TranscodingCount;
            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);

        }

        /// <summary>
		/// 删除
		/// </summary>
		public void Remove(Guid transcodingQueueID)
        {
            string commandName = "dbo.Pr_Res_TranscodingQueue_Delete";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@TranscodingQueueID", SqlDbType.UniqueIdentifier)
                };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = transcodingQueueID;

            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
        }

        /// <summary>
		/// 根据标识获取对象
		/// </summary>
		public Res_TranscodingQueue GetById(Guid transcodingQueueID)
        {
            Res_TranscodingQueue res_TranscodingQueue = null;

            string commandName = "dbo.Pr_Res_TranscodingQueue_GetByPK";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@TranscodingQueueID", SqlDbType.UniqueIdentifier)
                };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = transcodingQueueID;

            #endregion
            using (SqlDataReader dataReader = SqlHelper.ExecuteReader(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms))
            {
                if (dataReader.Read())
                {
                    res_TranscodingQueue = PopulateRes_CourseFromDataReader(dataReader);
                }
            }

            return res_TranscodingQueue;
        }

        /// <summary>
		/// 从DataReader中读取数据到业务对象
		/// </summary>
		private Res_TranscodingQueue PopulateRes_CourseFromDataReader(SqlDataReader reader)
        {
            Res_TranscodingQueue res_TranscodingQueue = new Res_TranscodingQueue();

            int courseIDIndex = reader.GetOrdinal("TranscodingQueueID");
            if (!reader.IsDBNull(courseIDIndex))
            {
                res_TranscodingQueue.TranscodingQueueID = reader.GetGuid(courseIDIndex);
            }

            int courseCodeIndex = reader.GetOrdinal("ContentID");
            if (!reader.IsDBNull(courseCodeIndex))
            {
                res_TranscodingQueue.ContentID = reader.GetGuid(courseCodeIndex);
            }

            int courseNameIndex = reader.GetOrdinal("StreamCode");
            if (!reader.IsDBNull(courseNameIndex))
            {
                res_TranscodingQueue.StreamCode = reader.GetString(courseNameIndex);
            }

            int createTimeIndex = reader.GetOrdinal("CreateTime");
            if (!reader.IsDBNull(createTimeIndex))
            {
                res_TranscodingQueue.CreateTime = reader.GetDateTime(createTimeIndex);
            }

            return res_TranscodingQueue;
        }
    }
}
