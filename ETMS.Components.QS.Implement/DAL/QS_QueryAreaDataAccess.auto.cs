//==================================================================================================
//Version 1.0, auto-generated.
//Generated By huangzf.
//Date: 2013-1-24 10:33:04.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1)
//==================================================================================================

using System;
using System.Collections.Generic;
using System.Data;

using System.Data.SqlClient;
using ETMS.Utility.Data;

using ETMS.Components.QS.API.Entity;

namespace ETMS.Components.QS.Implement.DAL
{
    /// <summary>
    /// 问卷调查范围数据访问
    /// </summary>
    public partial class QS_QueryAreaDataAccess
	{
		/// <summary>
		/// 增加
		/// </summary>
		public void Add(QS_QueryArea qS_QueryArea)
		{
			string commandName = "dbo.Pr_QS_QueryArea_Insert";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@QueryAreaID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@QueryID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@AreaType", SqlDbType.NVarChar, 200),
					new SqlParameter("@AreaCode", SqlDbType.NVarChar, 100),
					new SqlParameter("@CreateUserID", SqlDbType.Int),
					new SqlParameter("@Creator", SqlDbType.NVarChar, 128),
					new SqlParameter("@CreateTime", SqlDbType.DateTime)
					};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
			}
			
			parms[0].Value = qS_QueryArea.QueryAreaID;
			parms[1].Value = qS_QueryArea.QueryID;
			if (qS_QueryArea.AreaType!= null){ parms[2].Value = qS_QueryArea.AreaType; } else { parms[2].Value = DBNull.Value; }
			if (qS_QueryArea.AreaCode!= null){ parms[3].Value = qS_QueryArea.AreaCode; } else { parms[3].Value = DBNull.Value; }
			parms[4].Value = qS_QueryArea.CreateUserID;
			if (qS_QueryArea.Creator!= null){ parms[5].Value = qS_QueryArea.Creator; } else { parms[5].Value = DBNull.Value; }
			parms[6].Value = qS_QueryArea.CreateTime;
			#endregion
			SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
			
		}
		
		/// <summary>
		/// 删除
		/// </summary>
		public void Remove(Guid queryAreaID)
		{
			string commandName = "dbo.Pr_QS_QueryArea_Delete";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@QueryAreaID", SqlDbType.UniqueIdentifier)
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
		public void Save(QS_QueryArea qS_QueryArea)
		{
			string commandName = "dbo.Pr_QS_QueryArea_Update";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@QueryAreaID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@QueryID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@AreaType", SqlDbType.NVarChar, 200),
					new SqlParameter("@AreaCode", SqlDbType.NVarChar, 100),
					new SqlParameter("@CreateUserID", SqlDbType.Int),
					new SqlParameter("@Creator", SqlDbType.NVarChar, 128),
					new SqlParameter("@CreateTime", SqlDbType.DateTime)
				};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
			}
			
			parms[0].Value = qS_QueryArea.QueryAreaID;
			parms[1].Value = qS_QueryArea.QueryID;
			if (qS_QueryArea.AreaType!= null){ parms[2].Value = qS_QueryArea.AreaType; } else { parms[2].Value = DBNull.Value; }
			if (qS_QueryArea.AreaCode!= null){ parms[3].Value = qS_QueryArea.AreaCode; } else { parms[3].Value = DBNull.Value; }
			parms[4].Value = qS_QueryArea.CreateUserID;
			if (qS_QueryArea.Creator!= null){ parms[5].Value = qS_QueryArea.Creator; } else { parms[5].Value = DBNull.Value; }
			parms[6].Value = qS_QueryArea.CreateTime;
			#endregion
			SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
		}
		
		/// <summary>
		/// 根据标识获取对象
		/// </summary>
		public QS_QueryArea GetById(Guid queryAreaID)
		{
			QS_QueryArea qS_QueryArea = null;
			
			string commandName = "dbo.Pr_QS_QueryArea_GetByPK";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@QueryAreaID", SqlDbType.UniqueIdentifier)
				};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
			}
			
			parms[0].Value = queryAreaID;
			
			#endregion
			using (SqlDataReader dataReader = SqlHelper.ExecuteReader(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms))
			{
				if (dataReader.Read())
				{
					qS_QueryArea = PopulateQS_QueryAreaFromDataReader(dataReader);
				}
			}
			
			return qS_QueryArea;
		}				
		
		/// <summary>
		/// 根据参数获取对象列表（分页，可排序）
		/// </summary>
		public DataTable GetPagedList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
		{			
			string commandName = "dbo.Pr_QS_QueryArea_GetPagedList";
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
		public IList<QS_QueryArea> GetEntityList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
		{			
            IList<QS_QueryArea> list=new List<QS_QueryArea>();
			string commandName = "dbo.Pr_QS_QueryArea_GetPagedList";
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
					list.Add(PopulateQS_QueryAreaFromDataReader(dataReader));
				}
			}			
			totalRecords = (int)parms[4].Value;
			return list;
		}
		
		/// <summary>
		/// 从DataReader中读取数据到业务对象
		/// </summary>
		private QS_QueryArea PopulateQS_QueryAreaFromDataReader(SqlDataReader reader)
		{
			QS_QueryArea qS_QueryArea = new QS_QueryArea();
			
			int queryAreaIDIndex = reader.GetOrdinal("QueryAreaID"); 
			if(!reader.IsDBNull(queryAreaIDIndex))
			{
				qS_QueryArea.QueryAreaID= reader.GetGuid(queryAreaIDIndex);
			}
			
			int queryIDIndex = reader.GetOrdinal("QueryID"); 
			if(!reader.IsDBNull(queryIDIndex))
			{
				qS_QueryArea.QueryID= reader.GetGuid(queryIDIndex);
			}
			
			int areaTypeIndex = reader.GetOrdinal("AreaType"); 
			if(!reader.IsDBNull(areaTypeIndex))
			{
				qS_QueryArea.AreaType= reader.GetString(areaTypeIndex);
			}
			
			int areaCodeIndex = reader.GetOrdinal("AreaCode"); 
			if(!reader.IsDBNull(areaCodeIndex))
			{
				qS_QueryArea.AreaCode= reader.GetString(areaCodeIndex);
			}
			
			int createUserIDIndex = reader.GetOrdinal("CreateUserID"); 
			if(!reader.IsDBNull(createUserIDIndex))
			{
				qS_QueryArea.CreateUserID= reader.GetInt32(createUserIDIndex);
			}
			
			int creatorIndex = reader.GetOrdinal("Creator"); 
			if(!reader.IsDBNull(creatorIndex))
			{
				qS_QueryArea.Creator= reader.GetString(creatorIndex);
			}
			
			int createTimeIndex = reader.GetOrdinal("CreateTime"); 
			if(!reader.IsDBNull(createTimeIndex))
			{
				qS_QueryArea.CreateTime= reader.GetDateTime(createTimeIndex);
			}
			
			return qS_QueryArea; 
		}
	}
}
