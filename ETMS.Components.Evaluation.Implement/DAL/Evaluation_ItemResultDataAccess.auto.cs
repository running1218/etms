//==================================================================================================
//Version 1.0, auto-generated.
//Generated By running1218
//Date: 2012-4-18 11:41:21.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1)
//==================================================================================================

using System;
using System.Collections.Generic;
using System.Data;

using System.Data.SqlClient;
using ETMS.Utility.Data;

using ETMS.Components.Evaluation.API.Entity;

namespace ETMS.Components.Evaluation.Implement.DAL
{
    /// <summary>
    /// 评价项记录表数据访问
    /// </summary>
    public partial class Evaluation_ItemResultDataAccess
	{
		/// <summary>
		/// 增加
		/// </summary>
		public void Add(Evaluation_ItemResult evaluation_ItemResult)
		{
			string commandName = "dbo.Pr_Evaluation_ItemResult_Insert";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@ResultID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@ItemID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@UserID", SqlDbType.Int),
					new SqlParameter("@ObjectID", SqlDbType.NVarChar, 100),
					new SqlParameter("@Score", SqlDbType.Int),
					new SqlParameter("@CreateTime", SqlDbType.DateTime)
					};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
			}
			
			parms[0].Value = evaluation_ItemResult.ResultID;
			parms[1].Value = evaluation_ItemResult.ItemID;
			parms[2].Value = evaluation_ItemResult.UserID;
			if (evaluation_ItemResult.ObjectID!= null){ parms[3].Value = evaluation_ItemResult.ObjectID; } else { parms[3].Value = DBNull.Value; }
			parms[4].Value = evaluation_ItemResult.Score;
			parms[5].Value = evaluation_ItemResult.CreateTime;
			#endregion
			SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
			
		}
		
		/// <summary>
		/// 删除
		/// </summary>
		public void Remove(Guid resultID)
		{
			string commandName = "dbo.Pr_Evaluation_ItemResult_Delete";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@ResultID", SqlDbType.UniqueIdentifier)
				};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
			}
			
			parms[0].Value = resultID;
			
			#endregion
			SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
		}
		
		/// <summary>
		/// 保存
		/// </summary>
		public void Save(Evaluation_ItemResult evaluation_ItemResult)
		{
			string commandName = "dbo.Pr_Evaluation_ItemResult_Update";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@ResultID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@ItemID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@UserID", SqlDbType.Int),
					new SqlParameter("@ObjectID", SqlDbType.NVarChar, 100),
					new SqlParameter("@Score", SqlDbType.Int),
					new SqlParameter("@CreateTime", SqlDbType.DateTime)
				};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
			}
			
			parms[0].Value = evaluation_ItemResult.ResultID;
			parms[1].Value = evaluation_ItemResult.ItemID;
			parms[2].Value = evaluation_ItemResult.UserID;
			if (evaluation_ItemResult.ObjectID!= null){ parms[3].Value = evaluation_ItemResult.ObjectID; } else { parms[3].Value = DBNull.Value; }
			parms[4].Value = evaluation_ItemResult.Score;
			parms[5].Value = evaluation_ItemResult.CreateTime;
			#endregion
			SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
		}
		
		/// <summary>
		/// 根据标识获取对象
		/// </summary>
		public Evaluation_ItemResult GetById(Guid resultID)
		{
			Evaluation_ItemResult evaluation_ItemResult = null;
			
			string commandName = "dbo.Pr_Evaluation_ItemResult_GetByPK";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@ResultID", SqlDbType.UniqueIdentifier)
				};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
			}
			
			parms[0].Value = resultID;
			
			#endregion
			using (SqlDataReader dataReader = SqlHelper.ExecuteReader(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms))
			{
				if (dataReader.Read())
				{
					evaluation_ItemResult = PopulateEvaluation_ItemResultFromDataReader(dataReader);
				}
			}
			
			return evaluation_ItemResult;
		}				
		
		/// <summary>
		/// 根据参数获取对象列表（分页，可排序）
		/// </summary>
		public DataTable GetPagedList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
		{			
			string commandName = "dbo.Pr_Evaluation_ItemResult_GetPagedList";
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
			DataTable dt=SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
			totalRecords = (int)parms[4].Value;
			return dt;
		}

        /// <summary>
        /// 根据参数获取对象列表（分页，可排序）
        /// </summary>
        public DataTable GetPagedListApprove(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
        {
            string commandName = "dbo.[Pr_Evaluation_ItemResult_GetPagedListApprove]";
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
		public IList<Evaluation_ItemResult> GetEntityList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
		{			
            IList<Evaluation_ItemResult> list=new List<Evaluation_ItemResult>();
			string commandName = "dbo.Pr_Evaluation_ItemResult_GetPagedList";
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
					list.Add(PopulateEvaluation_ItemResultFromDataReader(dataReader));
				}
			}			
			totalRecords = (int)parms[4].Value;
			return list;
		}
		
		/// <summary>
		/// 从DataReader中读取数据到业务对象
		/// </summary>
		private Evaluation_ItemResult PopulateEvaluation_ItemResultFromDataReader(SqlDataReader reader)
		{
			Evaluation_ItemResult evaluation_ItemResult = new Evaluation_ItemResult();
			
			int resultIDIndex = reader.GetOrdinal("ResultID"); 
			if(!reader.IsDBNull(resultIDIndex))
			{
				evaluation_ItemResult.ResultID= reader.GetGuid(resultIDIndex);
			}
			
			int itemIDIndex = reader.GetOrdinal("ItemID"); 
			if(!reader.IsDBNull(itemIDIndex))
			{
				evaluation_ItemResult.ItemID= reader.GetGuid(itemIDIndex);
			}
			
			int userIDIndex = reader.GetOrdinal("UserID"); 
			if(!reader.IsDBNull(userIDIndex))
			{
				evaluation_ItemResult.UserID= reader.GetInt32(userIDIndex);
			}
			
			int objectIDIndex = reader.GetOrdinal("ObjectID"); 
			if(!reader.IsDBNull(objectIDIndex))
			{
				evaluation_ItemResult.ObjectID= reader.GetString(objectIDIndex);
			}
			
			int scoreIndex = reader.GetOrdinal("Score"); 
			if(!reader.IsDBNull(scoreIndex))
			{
				evaluation_ItemResult.Score= reader.GetInt32(scoreIndex);
			}
			
			int createTimeIndex = reader.GetOrdinal("CreateTime"); 
			if(!reader.IsDBNull(createTimeIndex))
			{
				evaluation_ItemResult.CreateTime= reader.GetDateTime(createTimeIndex);
			}
			
			return evaluation_ItemResult; 
		}
	}
}
