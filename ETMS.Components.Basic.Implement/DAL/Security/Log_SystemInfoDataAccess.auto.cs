//==================================================================================================
//Version 1.0, auto-generated.
//Generated By running1218
//Date: 2012-5-11 23:06:45.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1)
//==================================================================================================

using System;
using System.Collections.Generic;
using System.Data;

using System.Data.SqlClient;
using ETMS.Utility.Data;

using ETMS.Components.Basic.API.Entity.Security;

namespace ETMS.Components.Basic.Implement.DAL.Security
{
    /// <summary>
    /// 系统日志消息数据访问
    /// </summary>
    public partial class Log_SystemInfoDataAccess
	{
		/// <summary>
		/// 增加
		/// </summary>
		public void Add(Log_SystemInfo log_SystemInfo)
		{
			string commandName = "dbo.Pr_Log_SystemInfo_Insert";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {	new SqlParameter("@SysInfoLogID", SqlDbType.BigInt, 8, ParameterDirection.Output, false, 0, 0, String.Empty, DataRowVersion.Default, null),
				
					new SqlParameter("@Target", SqlDbType.VarChar, 50),
					new SqlParameter("@LogType", SqlDbType.VarChar, 10),
					new SqlParameter("@Message", SqlDbType.VarChar, -1),
					new SqlParameter("@LoginName", SqlDbType.VarChar, 50),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@ServerName", SqlDbType.VarChar, 50),
					new SqlParameter("@ClientIP", SqlDbType.VarChar, 20)
					};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
			}
			
			if (log_SystemInfo.Target!= null){ parms[1].Value = log_SystemInfo.Target; } else { parms[1].Value = DBNull.Value; }
			if (log_SystemInfo.LogType!= null){ parms[2].Value = log_SystemInfo.LogType; } else { parms[2].Value = DBNull.Value; }
			if (log_SystemInfo.Message!= null){ parms[3].Value = log_SystemInfo.Message; } else { parms[3].Value = DBNull.Value; }
			if (log_SystemInfo.LoginName!= null){ parms[4].Value = log_SystemInfo.LoginName; } else { parms[4].Value = DBNull.Value; }
			parms[5].Value = log_SystemInfo.CreateTime;
			if (log_SystemInfo.ServerName!= null){ parms[6].Value = log_SystemInfo.ServerName; } else { parms[6].Value = DBNull.Value; }
			if (log_SystemInfo.ClientIP!= null){ parms[7].Value = log_SystemInfo.ClientIP; } else { parms[7].Value = DBNull.Value; }
			#endregion
			SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
			
			log_SystemInfo.SysInfoLogID = (Int64)parms[0].Value;
			
		}
		
		/// <summary>
		/// 删除
		/// </summary>
		public void Remove(Int64 sysInfoLogID)
		{
			string commandName = "dbo.Pr_Log_SystemInfo_Delete";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@SysInfoLogID", SqlDbType.BigInt)
				};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
			}
			
			parms[0].Value = sysInfoLogID;
			
			#endregion
			SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
		}
		
		/// <summary>
		/// 保存
		/// </summary>
		public void Save(Log_SystemInfo log_SystemInfo)
		{
			string commandName = "dbo.Pr_Log_SystemInfo_Update";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@SysInfoLogID", SqlDbType.BigInt),
					new SqlParameter("@Target", SqlDbType.VarChar, 50),
					new SqlParameter("@LogType", SqlDbType.VarChar, 10),
					new SqlParameter("@Message", SqlDbType.VarChar, -1),
					new SqlParameter("@LoginName", SqlDbType.VarChar, 50),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@ServerName", SqlDbType.VarChar, 50),
					new SqlParameter("@ClientIP", SqlDbType.VarChar, 20)
				};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
			}
			
			parms[0].Value = log_SystemInfo.SysInfoLogID;
			if (log_SystemInfo.Target!= null){ parms[1].Value = log_SystemInfo.Target; } else { parms[1].Value = DBNull.Value; }
			if (log_SystemInfo.LogType!= null){ parms[2].Value = log_SystemInfo.LogType; } else { parms[2].Value = DBNull.Value; }
			if (log_SystemInfo.Message!= null){ parms[3].Value = log_SystemInfo.Message; } else { parms[3].Value = DBNull.Value; }
			if (log_SystemInfo.LoginName!= null){ parms[4].Value = log_SystemInfo.LoginName; } else { parms[4].Value = DBNull.Value; }
			parms[5].Value = log_SystemInfo.CreateTime;
			if (log_SystemInfo.ServerName!= null){ parms[6].Value = log_SystemInfo.ServerName; } else { parms[6].Value = DBNull.Value; }
			if (log_SystemInfo.ClientIP!= null){ parms[7].Value = log_SystemInfo.ClientIP; } else { parms[7].Value = DBNull.Value; }
			#endregion
			SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
		}
		
		/// <summary>
		/// 根据标识获取对象
		/// </summary>
		public Log_SystemInfo GetById(Int64 sysInfoLogID)
		{
			Log_SystemInfo log_SystemInfo = null;
			
			string commandName = "dbo.Pr_Log_SystemInfo_GetByPK";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@SysInfoLogID", SqlDbType.BigInt)
				};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
			}
			
			parms[0].Value = sysInfoLogID;
			
			#endregion
			using (SqlDataReader dataReader = SqlHelper.ExecuteReader(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms))
			{
				if (dataReader.Read())
				{
					log_SystemInfo = PopulateLog_SystemInfoFromDataReader(dataReader);
				}
			}
			
			return log_SystemInfo;
		}				
		
		/// <summary>
		/// 根据参数获取对象列表（分页，可排序）
		/// </summary>
		public DataTable GetPagedList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
		{			
			string commandName = "dbo.Pr_Log_SystemInfo_GetPagedList";
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
		public IList<Log_SystemInfo> GetEntityList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
		{			
            IList<Log_SystemInfo> list=new List<Log_SystemInfo>();
			string commandName = "dbo.Pr_Log_SystemInfo_GetPagedList";
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
					list.Add(PopulateLog_SystemInfoFromDataReader(dataReader));
				}
			}			
			totalRecords = (int)parms[4].Value;
			return list;
		}
		
		/// <summary>
		/// 从DataReader中读取数据到业务对象
		/// </summary>
		private Log_SystemInfo PopulateLog_SystemInfoFromDataReader(SqlDataReader reader)
		{
			Log_SystemInfo log_SystemInfo = new Log_SystemInfo();
			
			int sysInfoLogIDIndex = reader.GetOrdinal("SysInfoLogID"); 
			if(!reader.IsDBNull(sysInfoLogIDIndex))
			{
				log_SystemInfo.SysInfoLogID= reader.GetInt64(sysInfoLogIDIndex);
			}
			
			int targetIndex = reader.GetOrdinal("Target"); 
			if(!reader.IsDBNull(targetIndex))
			{
				log_SystemInfo.Target= reader.GetString(targetIndex);
			}
			
			int logTypeIndex = reader.GetOrdinal("LogType"); 
			if(!reader.IsDBNull(logTypeIndex))
			{
				log_SystemInfo.LogType= reader.GetString(logTypeIndex);
			}
			
			int messageIndex = reader.GetOrdinal("Message"); 
			if(!reader.IsDBNull(messageIndex))
			{
				log_SystemInfo.Message= reader.GetString(messageIndex);
			}
			
			int loginNameIndex = reader.GetOrdinal("LoginName"); 
			if(!reader.IsDBNull(loginNameIndex))
			{
				log_SystemInfo.LoginName= reader.GetString(loginNameIndex);
			}
			
			int createTimeIndex = reader.GetOrdinal("CreateTime"); 
			if(!reader.IsDBNull(createTimeIndex))
			{
				log_SystemInfo.CreateTime= reader.GetDateTime(createTimeIndex);
			}
			
			int serverNameIndex = reader.GetOrdinal("ServerName"); 
			if(!reader.IsDBNull(serverNameIndex))
			{
				log_SystemInfo.ServerName= reader.GetString(serverNameIndex);
			}
			
			int clientIPIndex = reader.GetOrdinal("ClientIP"); 
			if(!reader.IsDBNull(clientIPIndex))
			{
				log_SystemInfo.ClientIP= reader.GetString(clientIPIndex);
			}
			
			return log_SystemInfo; 
		}
	}
}
