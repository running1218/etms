//==================================================================================================
//Version 1.0, auto-generated.
//Generated By running1218
//Date: 2012-4-8 14:21:20.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1)
//==================================================================================================

using System;
using System.Data;

using System.Data.SqlClient;
using ETMS.Utility.Data;

using ETMS.Components.Basic.API.Entity.Notify;

namespace ETMS.Components.Basic.Implement.DAL.Notify
{
    /// <summary>
    /// 消息类型（1：邮件 2：短信 3：站内信）数据访问
    /// </summary>
    public partial class Notify_MessageTypeDataAccess
	{
		/// <summary>
		/// 增加
		/// </summary>
		public void Add(Notify_MessageType notify_MessageType)
		{
			string commandName = "dbo.Pr_Notify_MessageType_Insert";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@MessageTypeID", SqlDbType.SmallInt),
					new SqlParameter("@MessageTypeName", SqlDbType.VarChar, 100),
					new SqlParameter("@OrderNum", SqlDbType.Int),
					new SqlParameter("@IsUse", SqlDbType.Int)
					};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
			}
			
			parms[0].Value = notify_MessageType.MessageTypeID;
			if (notify_MessageType.MessageTypeName!= null){ parms[1].Value = notify_MessageType.MessageTypeName; } else { parms[1].Value = DBNull.Value; }
			parms[2].Value = notify_MessageType.OrderNum;
			parms[3].Value = notify_MessageType.IsUse;
			#endregion
			SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
			
		}
		
		/// <summary>
		/// 删除
		/// </summary>
		public void Remove(Int16 messageTypeID)
		{
			string commandName = "dbo.Pr_Notify_MessageType_Delete";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@MessageTypeID", SqlDbType.SmallInt)
				};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
			}
			
			parms[0].Value = messageTypeID;
			
			#endregion
			SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
		}
		
		/// <summary>
		/// 保存
		/// </summary>
		public void Save(Notify_MessageType notify_MessageType)
		{
			string commandName = "dbo.Pr_Notify_MessageType_Update";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@MessageTypeID", SqlDbType.SmallInt),
					new SqlParameter("@MessageTypeName", SqlDbType.VarChar, 100),
					new SqlParameter("@OrderNum", SqlDbType.Int),
					new SqlParameter("@IsUse", SqlDbType.Int)
				};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
			}
			
			parms[0].Value = notify_MessageType.MessageTypeID;
			if (notify_MessageType.MessageTypeName!= null){ parms[1].Value = notify_MessageType.MessageTypeName; } else { parms[1].Value = DBNull.Value; }
			parms[2].Value = notify_MessageType.OrderNum;
			parms[3].Value = notify_MessageType.IsUse;
			#endregion
			SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
		}
		
		/// <summary>
		/// 根据标识获取对象
		/// </summary>
		public Notify_MessageType GetById(Int16 messageTypeID)
		{
			Notify_MessageType notify_MessageType = null;
			
			string commandName = "dbo.Pr_Notify_MessageType_GetByPK";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@MessageTypeID", SqlDbType.SmallInt)
				};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
			}
			
			parms[0].Value = messageTypeID;
			
			#endregion
			using (SqlDataReader dataReader = SqlHelper.ExecuteReader(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms))
			{
				if (dataReader.Read())
				{
					notify_MessageType = PopulateNotify_MessageTypeFromDataReader(dataReader);
				}
			}
			
			return notify_MessageType;
		}				
		
		/// <summary>
		/// 根据参数获取对象列表（分页，可排序）
		/// </summary>
		public DataTable GetPagedList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
		{			
			string commandName = "dbo.Pr_Notify_MessageType_GetPagedList";
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
		/// 从DataReader中读取数据到业务对象
		/// </summary>
		private Notify_MessageType PopulateNotify_MessageTypeFromDataReader(SqlDataReader reader)
		{
			Notify_MessageType notify_MessageType = new Notify_MessageType();
			
			int messageTypeIDIndex = reader.GetOrdinal("MessageTypeID"); 
			if(!reader.IsDBNull(messageTypeIDIndex))
			{
				notify_MessageType.MessageTypeID= reader.GetInt16(messageTypeIDIndex);
			}
			
			int messageTypeNameIndex = reader.GetOrdinal("MessageTypeName"); 
			if(!reader.IsDBNull(messageTypeNameIndex))
			{
				notify_MessageType.MessageTypeName= reader.GetString(messageTypeNameIndex);
			}
			
			int orderNumIndex = reader.GetOrdinal("OrderNum"); 
			if(!reader.IsDBNull(orderNumIndex))
			{
				notify_MessageType.OrderNum= reader.GetInt32(orderNumIndex);
			}
			
			int isUseIndex = reader.GetOrdinal("IsUse"); 
			if(!reader.IsDBNull(isUseIndex))
			{
				notify_MessageType.IsUse= reader.GetInt32(isUseIndex);
			}
			
			return notify_MessageType; 
		}
	}
}
