//==================================================================================================
//Version 1.0, auto-generated.
//Generated By xueyb.
//Date: 2012-4-17 15:53:39.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1), a product of ZhouYonghua(Zhou_Yonghua@163.com).
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
    /// ���ݷ���
    /// </summary>
    public partial class Poll_QueryPublishObjectDataAccess
    {
        /// <summary>
        /// ����
        /// </summary>
        public void Add(Poll_QueryPublishObject poll_QueryPublishObject)
        {
            string commandName = "dbo.Pr_Poll_QueryPublishObject_Insert";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {	new SqlParameter("@QueryPublishID", SqlDbType.Int, 4, ParameterDirection.Output, false, 0, 0, String.Empty, DataRowVersion.Default, null),
				
					new SqlParameter("@ResourceCode", SqlDbType.VarChar, 50),
					new SqlParameter("@QueryID", SqlDbType.Int),
					new SqlParameter("@ResourceTypeCode", SqlDbType.VarChar, 2),
					new SqlParameter("@FileName", SqlDbType.VarChar, 200),
					new SqlParameter("@FilePath", SqlDbType.VarChar, 500),
					new SqlParameter("@Creator", SqlDbType.VarChar, 50),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@Modifier", SqlDbType.VarChar, 50),
					new SqlParameter("@ModifiyTime", SqlDbType.DateTime),
					new SqlParameter("@Score", SqlDbType.Int)
					};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            if (poll_QueryPublishObject.ResourceCode != null) { parms[1].Value = poll_QueryPublishObject.ResourceCode; } else { parms[1].Value = DBNull.Value; }
            parms[2].Value = poll_QueryPublishObject.QueryID;
            if (poll_QueryPublishObject.ResourceTypeCode != null) { parms[3].Value = poll_QueryPublishObject.ResourceTypeCode; } else { parms[3].Value = DBNull.Value; }
            if (poll_QueryPublishObject.FileName != null) { parms[4].Value = poll_QueryPublishObject.FileName; } else { parms[4].Value = DBNull.Value; }
            if (poll_QueryPublishObject.FilePath != null) { parms[5].Value = poll_QueryPublishObject.FilePath; } else { parms[5].Value = DBNull.Value; }
            if (poll_QueryPublishObject.Creator != null) { parms[6].Value = poll_QueryPublishObject.Creator; } else { parms[6].Value = DBNull.Value; }
            parms[7].Value = poll_QueryPublishObject.CreateTime;
            if (poll_QueryPublishObject.Modifier != null) { parms[8].Value = poll_QueryPublishObject.Modifier; } else { parms[8].Value = DBNull.Value; }
            parms[9].Value = poll_QueryPublishObject.ModifiyTime;
            parms[10].Value = poll_QueryPublishObject.Score;
            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);

            poll_QueryPublishObject.QueryPublishID = (Int32)parms[0].Value;

        }

        /// <summary>
        /// ɾ��
        /// </summary>
        public void Remove(Int32 queryPublishID)
        {
            string commandName = "dbo.Pr_Poll_QueryPublishObject_Delete";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@QueryPublishID", SqlDbType.Int)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = queryPublishID;

            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
        }

        /// <summary>
        /// ����
        /// </summary>
        public void Save(Poll_QueryPublishObject poll_QueryPublishObject)
        {
            string commandName = "dbo.Pr_Poll_QueryPublishObject_Update";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@QueryPublishID", SqlDbType.Int),
					new SqlParameter("@ResourceCode", SqlDbType.VarChar, 50),
					new SqlParameter("@QueryID", SqlDbType.Int),
					new SqlParameter("@ResourceTypeCode", SqlDbType.VarChar, 2),
					new SqlParameter("@FileName", SqlDbType.VarChar, 200),
					new SqlParameter("@FilePath", SqlDbType.VarChar, 500),
					new SqlParameter("@Creator", SqlDbType.VarChar, 50),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@Modifier", SqlDbType.VarChar, 50),
					new SqlParameter("@ModifiyTime", SqlDbType.DateTime),
					new SqlParameter("@Score", SqlDbType.Int)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
            }

            parms[0].Value = poll_QueryPublishObject.QueryPublishID;
            if (poll_QueryPublishObject.ResourceCode != null) { parms[1].Value = poll_QueryPublishObject.ResourceCode; } else { parms[1].Value = DBNull.Value; }
            parms[2].Value = poll_QueryPublishObject.QueryID;
            if (poll_QueryPublishObject.ResourceTypeCode != null) { parms[3].Value = poll_QueryPublishObject.ResourceTypeCode; } else { parms[3].Value = DBNull.Value; }
            if (poll_QueryPublishObject.FileName != null) { parms[4].Value = poll_QueryPublishObject.FileName; } else { parms[4].Value = DBNull.Value; }
            if (poll_QueryPublishObject.FilePath != null) { parms[5].Value = poll_QueryPublishObject.FilePath; } else { parms[5].Value = DBNull.Value; }
            if (poll_QueryPublishObject.Creator != null) { parms[6].Value = poll_QueryPublishObject.Creator; } else { parms[6].Value = DBNull.Value; }
            parms[7].Value = poll_QueryPublishObject.CreateTime;
            if (poll_QueryPublishObject.Modifier != null) { parms[8].Value = poll_QueryPublishObject.Modifier; } else { parms[8].Value = DBNull.Value; }
            parms[9].Value = poll_QueryPublishObject.ModifiyTime;
            parms[10].Value = poll_QueryPublishObject.Score;
            #endregion
            SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
        }

        /// <summary>
        /// ���ݱ�ʶ��ȡ����
        /// </summary>
        public Poll_QueryPublishObject GetById(Int32 queryPublishID)
        {
            Poll_QueryPublishObject poll_QueryPublishObject = null;

            string commandName = "dbo.Pr_Poll_QueryPublishObject_GetByPK";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
					new SqlParameter("@QueryPublishID", SqlDbType.Int)
				};
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = queryPublishID;

            #endregion
            using (SqlDataReader dataReader = SqlHelper.ExecuteReader(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms))
            {
                if (dataReader.Read())
                {
                    poll_QueryPublishObject = PopulatePoll_QueryPublishObjectFromDataReader(dataReader);
                }
            }

            return poll_QueryPublishObject;
        }

        /// <summary>
        /// ���ݲ�����ȡ�����б�����ҳ��������
        /// </summary>
        public DataTable GetPagedList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            string commandName = "dbo.Pr_Poll_QueryPublishObject_GetPagedList";
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
        /// ���ݲ�����ȡ�����б�����ҳ��������
        /// </summary>
        public IList<Poll_QueryPublishObject> GetEntityList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            IList<Poll_QueryPublishObject> list = new List<Poll_QueryPublishObject>();
            string commandName = "dbo.Pr_Poll_QueryPublishObject_GetPagedList";
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
                    list.Add(PopulatePoll_QueryPublishObjectFromDataReader(dataReader));
                }
            }
            totalRecords = (int)parms[4].Value;
            return list;
        }

        /// <summary>
        /// ��DataReader�ж�ȡ���ݵ�ҵ�����
        /// </summary>
        private Poll_QueryPublishObject PopulatePoll_QueryPublishObjectFromDataReader(SqlDataReader reader)
        {
            Poll_QueryPublishObject poll_QueryPublishObject = new Poll_QueryPublishObject();

            int queryPublishIDIndex = reader.GetOrdinal("QueryPublishID");
            if (!reader.IsDBNull(queryPublishIDIndex))
            {
                poll_QueryPublishObject.QueryPublishID = reader.GetInt32(queryPublishIDIndex);
            }

            int resourceCodeIndex = reader.GetOrdinal("ResourceCode");
            if (!reader.IsDBNull(resourceCodeIndex))
            {
                poll_QueryPublishObject.ResourceCode = reader.GetString(resourceCodeIndex);
            }

            int queryIDIndex = reader.GetOrdinal("QueryID");
            if (!reader.IsDBNull(queryIDIndex))
            {
                poll_QueryPublishObject.QueryID = reader.GetInt32(queryIDIndex);
            }

            int resourceTypeCodeIndex = reader.GetOrdinal("ResourceTypeCode");
            if (!reader.IsDBNull(resourceTypeCodeIndex))
            {
                poll_QueryPublishObject.ResourceTypeCode = reader.GetString(resourceTypeCodeIndex);
            }

            int fileNameIndex = reader.GetOrdinal("FileName");
            if (!reader.IsDBNull(fileNameIndex))
            {
                poll_QueryPublishObject.FileName = reader.GetString(fileNameIndex);
            }

            int filePathIndex = reader.GetOrdinal("FilePath");
            if (!reader.IsDBNull(filePathIndex))
            {
                poll_QueryPublishObject.FilePath = reader.GetString(filePathIndex);
            }

            int creatorIndex = reader.GetOrdinal("Creator");
            if (!reader.IsDBNull(creatorIndex))
            {
                poll_QueryPublishObject.Creator = reader.GetString(creatorIndex);
            }

            int createTimeIndex = reader.GetOrdinal("CreateTime");
            if (!reader.IsDBNull(createTimeIndex))
            {
                poll_QueryPublishObject.CreateTime = reader.GetDateTime(createTimeIndex);
            }

            int modifierIndex = reader.GetOrdinal("Modifier");
            if (!reader.IsDBNull(modifierIndex))
            {
                poll_QueryPublishObject.Modifier = reader.GetString(modifierIndex);
            }

            int modifiyTimeIndex = reader.GetOrdinal("ModifiyTime");
            if (!reader.IsDBNull(modifiyTimeIndex))
            {
                poll_QueryPublishObject.ModifiyTime = reader.GetDateTime(modifiyTimeIndex);
            }

            int scoreIndex = reader.GetOrdinal("Score");
            if (!reader.IsDBNull(scoreIndex))
            {
                poll_QueryPublishObject.Score = reader.GetInt32(scoreIndex);
            }

            return poll_QueryPublishObject;
        }
    }
}