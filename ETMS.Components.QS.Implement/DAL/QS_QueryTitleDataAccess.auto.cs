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
    /// 问卷调查题目表数据访问
    /// </summary>
    public partial class QS_QueryTitleDataAccess
	{
		/// <summary>
		/// 增加
		/// </summary>
		public void Add(QS_QueryTitle qS_QueryTitle)
		{
			string commandName = "dbo.Pr_QS_QueryTitle_Insert";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@TitleID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@QueryID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@TitleTypeID", SqlDbType.Int),
					new SqlParameter("@TitleName", SqlDbType.NVarChar, 2048),
					new SqlParameter("@TitleNo", SqlDbType.Int),
					new SqlParameter("@MinSelectNum", SqlDbType.Int),
					new SqlParameter("@MaxSelectNum", SqlDbType.Int),
					new SqlParameter("@CreateUserID", SqlDbType.Int),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUser", SqlDbType.NVarChar, 128),
					new SqlParameter("@ModifyTime", SqlDbType.DateTime),
					new SqlParameter("@ModifyUser", SqlDbType.NVarChar, 128),
					new SqlParameter("@Remark", SqlDbType.NVarChar, -1)
					};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
			}
			
			parms[0].Value = qS_QueryTitle.TitleID;
			parms[1].Value = qS_QueryTitle.QueryID;
			parms[2].Value = qS_QueryTitle.TitleTypeID;
			if (qS_QueryTitle.TitleName!= null){ parms[3].Value = qS_QueryTitle.TitleName; } else { parms[3].Value = DBNull.Value; }
			parms[4].Value = qS_QueryTitle.TitleNo;
			parms[5].Value = qS_QueryTitle.MinSelectNum;
			parms[6].Value = qS_QueryTitle.MaxSelectNum;
			parms[7].Value = qS_QueryTitle.CreateUserID;
			parms[8].Value = qS_QueryTitle.CreateTime;
			if (qS_QueryTitle.CreateUser!= null){ parms[9].Value = qS_QueryTitle.CreateUser; } else { parms[9].Value = DBNull.Value; }
			parms[10].Value = qS_QueryTitle.ModifyTime;
			if (qS_QueryTitle.ModifyUser!= null){ parms[11].Value = qS_QueryTitle.ModifyUser; } else { parms[11].Value = DBNull.Value; }
			if (qS_QueryTitle.Remark!= null){ parms[12].Value = qS_QueryTitle.Remark; } else { parms[12].Value = DBNull.Value; }
			#endregion
			SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
			
		}
		
		/// <summary>
		/// 删除
		/// </summary>
		public void Remove(Guid titleID)
		{
			string commandName = "dbo.Pr_QS_QueryTitle_Delete";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@TitleID", SqlDbType.UniqueIdentifier)
				};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
			}
			
			parms[0].Value = titleID;
			
			#endregion
			SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
		}
		
		/// <summary>
		/// 保存
		/// </summary>
		public void Save(QS_QueryTitle qS_QueryTitle)
		{
			string commandName = "dbo.Pr_QS_QueryTitle_Update";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@TitleID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@QueryID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@TitleTypeID", SqlDbType.Int),
					new SqlParameter("@TitleName", SqlDbType.NVarChar, 2048),
					new SqlParameter("@TitleNo", SqlDbType.Int),
					new SqlParameter("@MinSelectNum", SqlDbType.Int),
					new SqlParameter("@MaxSelectNum", SqlDbType.Int),
					new SqlParameter("@CreateUserID", SqlDbType.Int),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUser", SqlDbType.NVarChar, 128),
					new SqlParameter("@ModifyTime", SqlDbType.DateTime),
					new SqlParameter("@ModifyUser", SqlDbType.NVarChar, 128),
					new SqlParameter("@Remark", SqlDbType.NVarChar, -1)
				};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
			}
			
			parms[0].Value = qS_QueryTitle.TitleID;
			parms[1].Value = qS_QueryTitle.QueryID;
			parms[2].Value = qS_QueryTitle.TitleTypeID;
			if (qS_QueryTitle.TitleName!= null){ parms[3].Value = qS_QueryTitle.TitleName; } else { parms[3].Value = DBNull.Value; }
			parms[4].Value = qS_QueryTitle.TitleNo;
			parms[5].Value = qS_QueryTitle.MinSelectNum;
			parms[6].Value = qS_QueryTitle.MaxSelectNum;
			parms[7].Value = qS_QueryTitle.CreateUserID;
			parms[8].Value = qS_QueryTitle.CreateTime;
			if (qS_QueryTitle.CreateUser!= null){ parms[9].Value = qS_QueryTitle.CreateUser; } else { parms[9].Value = DBNull.Value; }
			parms[10].Value = qS_QueryTitle.ModifyTime;
			if (qS_QueryTitle.ModifyUser!= null){ parms[11].Value = qS_QueryTitle.ModifyUser; } else { parms[11].Value = DBNull.Value; }
			if (qS_QueryTitle.Remark!= null){ parms[12].Value = qS_QueryTitle.Remark; } else { parms[12].Value = DBNull.Value; }
			#endregion
			SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
		}
		
		/// <summary>
		/// 根据标识获取对象
		/// </summary>
		public QS_QueryTitle GetById(Guid titleID)
		{
			QS_QueryTitle qS_QueryTitle = null;
			
			string commandName = "dbo.Pr_QS_QueryTitle_GetByPK";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@TitleID", SqlDbType.UniqueIdentifier)
				};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
			}
			
			parms[0].Value = titleID;
			
			#endregion
			using (SqlDataReader dataReader = SqlHelper.ExecuteReader(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms))
			{
				if (dataReader.Read())
				{
					qS_QueryTitle = PopulateQS_QueryTitleFromDataReader(dataReader);
				}
			}
			
			return qS_QueryTitle;
		}				
		
		/// <summary>
		/// 根据参数获取对象列表（分页，可排序）
		/// </summary>
		public DataTable GetPagedList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
		{			
			string commandName = "dbo.Pr_QS_QueryTitle_GetPagedList";
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
		public IList<QS_QueryTitle> GetEntityList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
		{			
            IList<QS_QueryTitle> list=new List<QS_QueryTitle>();
			string commandName = "dbo.Pr_QS_QueryTitle_GetPagedList";
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
					list.Add(PopulateQS_QueryTitleFromDataReader(dataReader));
				}
			}			
			totalRecords = (int)parms[4].Value;
			return list;
		}
		
		/// <summary>
		/// 从DataReader中读取数据到业务对象
		/// </summary>
		private QS_QueryTitle PopulateQS_QueryTitleFromDataReader(SqlDataReader reader)
		{
			QS_QueryTitle qS_QueryTitle = new QS_QueryTitle();
			
			int titleIDIndex = reader.GetOrdinal("TitleID"); 
			if(!reader.IsDBNull(titleIDIndex))
			{
				qS_QueryTitle.TitleID= reader.GetGuid(titleIDIndex);
			}
			
			int queryIDIndex = reader.GetOrdinal("QueryID"); 
			if(!reader.IsDBNull(queryIDIndex))
			{
				qS_QueryTitle.QueryID= reader.GetGuid(queryIDIndex);
			}
			
			int titleTypeIDIndex = reader.GetOrdinal("TitleTypeID"); 
			if(!reader.IsDBNull(titleTypeIDIndex))
			{
				qS_QueryTitle.TitleTypeID= reader.GetInt32(titleTypeIDIndex);
			}
			
			int titleNameIndex = reader.GetOrdinal("TitleName"); 
			if(!reader.IsDBNull(titleNameIndex))
			{
				qS_QueryTitle.TitleName= reader.GetString(titleNameIndex);
			}
			
			int titleNoIndex = reader.GetOrdinal("TitleNo"); 
			if(!reader.IsDBNull(titleNoIndex))
			{
				qS_QueryTitle.TitleNo= reader.GetInt32(titleNoIndex);
			}
			
			int minSelectNumIndex = reader.GetOrdinal("MinSelectNum"); 
			if(!reader.IsDBNull(minSelectNumIndex))
			{
				qS_QueryTitle.MinSelectNum= reader.GetInt32(minSelectNumIndex);
			}
			
			int maxSelectNumIndex = reader.GetOrdinal("MaxSelectNum"); 
			if(!reader.IsDBNull(maxSelectNumIndex))
			{
				qS_QueryTitle.MaxSelectNum= reader.GetInt32(maxSelectNumIndex);
			}
			
			int createUserIDIndex = reader.GetOrdinal("CreateUserID"); 
			if(!reader.IsDBNull(createUserIDIndex))
			{
				qS_QueryTitle.CreateUserID= reader.GetInt32(createUserIDIndex);
			}
			
			int createTimeIndex = reader.GetOrdinal("CreateTime"); 
			if(!reader.IsDBNull(createTimeIndex))
			{
				qS_QueryTitle.CreateTime= reader.GetDateTime(createTimeIndex);
			}
			
			int createUserIndex = reader.GetOrdinal("CreateUser"); 
			if(!reader.IsDBNull(createUserIndex))
			{
				qS_QueryTitle.CreateUser= reader.GetString(createUserIndex);
			}
			
			int modifyTimeIndex = reader.GetOrdinal("ModifyTime"); 
			if(!reader.IsDBNull(modifyTimeIndex))
			{
				qS_QueryTitle.ModifyTime= reader.GetDateTime(modifyTimeIndex);
			}
			
			int modifyUserIndex = reader.GetOrdinal("ModifyUser"); 
			if(!reader.IsDBNull(modifyUserIndex))
			{
				qS_QueryTitle.ModifyUser= reader.GetString(modifyUserIndex);
			}
			
			int remarkIndex = reader.GetOrdinal("Remark"); 
			if(!reader.IsDBNull(remarkIndex))
			{
				qS_QueryTitle.Remark= reader.GetString(remarkIndex);
			}
			
			return qS_QueryTitle; 
		}
	}
}
