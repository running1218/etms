//==================================================================================================
//Version 1.0, auto-generated.
//Generated By xueyb.
//Date: 2012-4-13 20:34:59.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1), a product of ZhouYonghua(Zhou_Yonghua@163.com).
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
    /// ϵͳ�쳣��־���ݷ���
    /// </summary>
    public partial class Log_SystemExceptionDataAccess
	{
		/// <summary>
		/// ����
		/// </summary>
		public void Add(Log_SystemException log_SystemException)
		{
			string commandName = "dbo.Pr_Log_SystemException_Insert";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {	new SqlParameter("@SysExLogID", SqlDbType.BigInt, 8, ParameterDirection.Output, false, 0, 0, String.Empty, DataRowVersion.Default, null),
				
					new SqlParameter("@ApplicationName", SqlDbType.VarChar, 50),
					new SqlParameter("@Message", SqlDbType.VarChar, 500),
					new SqlParameter("@BaseMessage", SqlDbType.VarChar, 500),
					new SqlParameter("@StackTrace", SqlDbType.VarChar, -1),
					new SqlParameter("@LoginName", SqlDbType.VarChar, 50),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@ServerName", SqlDbType.VarChar, 50),
					new SqlParameter("@ClientIP", SqlDbType.VarChar, 20),
					new SqlParameter("@PageUrl", SqlDbType.VarChar, 1024)
					};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
			}
			
			if (log_SystemException.ApplicationName!= null){ parms[1].Value = log_SystemException.ApplicationName; } else { parms[1].Value = DBNull.Value; }
			if (log_SystemException.Message!= null){ parms[2].Value = log_SystemException.Message; } else { parms[2].Value = DBNull.Value; }
			if (log_SystemException.BaseMessage!= null){ parms[3].Value = log_SystemException.BaseMessage; } else { parms[3].Value = DBNull.Value; }
			if (log_SystemException.StackTrace!= null){ parms[4].Value = log_SystemException.StackTrace; } else { parms[4].Value = DBNull.Value; }
			if (log_SystemException.LoginName!= null){ parms[5].Value = log_SystemException.LoginName; } else { parms[5].Value = DBNull.Value; }
			parms[6].Value = log_SystemException.CreateTime;
			if (log_SystemException.ServerName!= null){ parms[7].Value = log_SystemException.ServerName; } else { parms[7].Value = DBNull.Value; }
			if (log_SystemException.ClientIP!= null){ parms[8].Value = log_SystemException.ClientIP; } else { parms[8].Value = DBNull.Value; }
			if (log_SystemException.PageUrl!= null){ parms[9].Value = log_SystemException.PageUrl; } else { parms[9].Value = DBNull.Value; }
			#endregion
			SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
			
			log_SystemException.SysExLogID = (Int64)parms[0].Value;
			
		}
		
		/// <summary>
		/// ɾ��
		/// </summary>
		public void Remove(Int64 sysExLogID)
		{
			string commandName = "dbo.Pr_Log_SystemException_Delete";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@SysExLogID", SqlDbType.BigInt)
				};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
			}
			
			parms[0].Value = sysExLogID;
			
			#endregion
			SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
		}
		
		/// <summary>
		/// ����
		/// </summary>
		public void Save(Log_SystemException log_SystemException)
		{
			string commandName = "dbo.Pr_Log_SystemException_Update";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@SysExLogID", SqlDbType.BigInt),
					new SqlParameter("@ApplicationName", SqlDbType.VarChar, 50),
					new SqlParameter("@Message", SqlDbType.VarChar, 500),
					new SqlParameter("@BaseMessage", SqlDbType.VarChar, 500),
					new SqlParameter("@StackTrace", SqlDbType.VarChar, -1),
					new SqlParameter("@LoginName", SqlDbType.VarChar, 50),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@ServerName", SqlDbType.VarChar, 50),
					new SqlParameter("@ClientIP", SqlDbType.VarChar, 20),
					new SqlParameter("@PageUrl", SqlDbType.VarChar, 1024)
				};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
			}
			
			parms[0].Value = log_SystemException.SysExLogID;
			if (log_SystemException.ApplicationName!= null){ parms[1].Value = log_SystemException.ApplicationName; } else { parms[1].Value = DBNull.Value; }
			if (log_SystemException.Message!= null){ parms[2].Value = log_SystemException.Message; } else { parms[2].Value = DBNull.Value; }
			if (log_SystemException.BaseMessage!= null){ parms[3].Value = log_SystemException.BaseMessage; } else { parms[3].Value = DBNull.Value; }
			if (log_SystemException.StackTrace!= null){ parms[4].Value = log_SystemException.StackTrace; } else { parms[4].Value = DBNull.Value; }
			if (log_SystemException.LoginName!= null){ parms[5].Value = log_SystemException.LoginName; } else { parms[5].Value = DBNull.Value; }
			parms[6].Value = log_SystemException.CreateTime;
			if (log_SystemException.ServerName!= null){ parms[7].Value = log_SystemException.ServerName; } else { parms[7].Value = DBNull.Value; }
			if (log_SystemException.ClientIP!= null){ parms[8].Value = log_SystemException.ClientIP; } else { parms[8].Value = DBNull.Value; }
			if (log_SystemException.PageUrl!= null){ parms[9].Value = log_SystemException.PageUrl; } else { parms[9].Value = DBNull.Value; }
			#endregion
			SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
		}
		
		/// <summary>
		/// ���ݱ�ʶ��ȡ����
		/// </summary>
		public Log_SystemException GetById(Int64 sysExLogID)
		{
			Log_SystemException log_SystemException = null;
			
			string commandName = "dbo.Pr_Log_SystemException_GetByPK";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@SysExLogID", SqlDbType.BigInt)
				};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
			}
			
			parms[0].Value = sysExLogID;
			
			#endregion
			using (SqlDataReader dataReader = SqlHelper.ExecuteReader(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms))
			{
				if (dataReader.Read())
				{
					log_SystemException = PopulateLog_SystemExceptionFromDataReader(dataReader);
				}
			}
			
			return log_SystemException;
		}				
		
		/// <summary>
		/// ���ݲ�����ȡ�����б�����ҳ��������
		/// </summary>
		public DataTable GetPagedList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
		{			
			string commandName = "dbo.Pr_Log_SystemException_GetPagedList";
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
		/// ���ݲ�����ȡ�����б�����ҳ��������
		/// </summary>
		public IList<Log_SystemException> GetEntityList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
		{			
            IList<Log_SystemException> list=new List<Log_SystemException>();
			string commandName = "dbo.Pr_Log_SystemException_GetPagedList";
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
					list.Add(PopulateLog_SystemExceptionFromDataReader(dataReader));
				}
			}			
			totalRecords = (int)parms[4].Value;
			return list;
		}
		
		/// <summary>
		/// ��DataReader�ж�ȡ���ݵ�ҵ�����
		/// </summary>
		private Log_SystemException PopulateLog_SystemExceptionFromDataReader(SqlDataReader reader)
		{
			Log_SystemException log_SystemException = new Log_SystemException();
			
			int sysExLogIDIndex = reader.GetOrdinal("SysExLogID"); 
			if(!reader.IsDBNull(sysExLogIDIndex))
			{
				log_SystemException.SysExLogID= reader.GetInt64(sysExLogIDIndex);
			}
			
			int applicationNameIndex = reader.GetOrdinal("ApplicationName"); 
			if(!reader.IsDBNull(applicationNameIndex))
			{
				log_SystemException.ApplicationName= reader.GetString(applicationNameIndex);
			}
			
			int messageIndex = reader.GetOrdinal("Message"); 
			if(!reader.IsDBNull(messageIndex))
			{
				log_SystemException.Message= reader.GetString(messageIndex);
			}
			
			int baseMessageIndex = reader.GetOrdinal("BaseMessage"); 
			if(!reader.IsDBNull(baseMessageIndex))
			{
				log_SystemException.BaseMessage= reader.GetString(baseMessageIndex);
			}
			
			int stackTraceIndex = reader.GetOrdinal("StackTrace"); 
			if(!reader.IsDBNull(stackTraceIndex))
			{
				log_SystemException.StackTrace= reader.GetString(stackTraceIndex);
			}
			
			int loginNameIndex = reader.GetOrdinal("LoginName"); 
			if(!reader.IsDBNull(loginNameIndex))
			{
				log_SystemException.LoginName= reader.GetString(loginNameIndex);
			}
			
			int createTimeIndex = reader.GetOrdinal("CreateTime"); 
			if(!reader.IsDBNull(createTimeIndex))
			{
				log_SystemException.CreateTime= reader.GetDateTime(createTimeIndex);
			}
			
			int serverNameIndex = reader.GetOrdinal("ServerName"); 
			if(!reader.IsDBNull(serverNameIndex))
			{
				log_SystemException.ServerName= reader.GetString(serverNameIndex);
			}
			
			int clientIPIndex = reader.GetOrdinal("ClientIP"); 
			if(!reader.IsDBNull(clientIPIndex))
			{
				log_SystemException.ClientIP= reader.GetString(clientIPIndex);
			}
			
			int pageUrlIndex = reader.GetOrdinal("PageUrl"); 
			if(!reader.IsDBNull(pageUrlIndex))
			{
				log_SystemException.PageUrl= reader.GetString(pageUrlIndex);
			}
			
			return log_SystemException; 
		}
	}
}