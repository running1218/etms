//==================================================================================================
//Version 1.0, auto-generated.
//Generated By running1218
//Date: 2012-4-2 11:40:56.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1)
//==================================================================================================

using System;
using System.Data;

using System.Data.SqlClient;
using ETMS.Utility.Data;

using ETMS.Components.Basic.API.Entity.Import;

namespace ETMS.Components.Basic.Implement.DAL.Import
{
    /// <summary>
    /// 数据导入任务数据访问
    /// </summary>
    public partial class Import_TaskDataAccess
	{
		/// <summary>
		/// 增加
		/// </summary>
		public void Add(Import_Task import_Task)
		{
			string commandName = "dbo.Pr_Import_Task_Insert";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {	new SqlParameter("@TaskID", SqlDbType.Int, 4, ParameterDirection.Output, false, 0, 0, String.Empty, DataRowVersion.Default, null),
				
					new SqlParameter("@TaskName", SqlDbType.VarChar, 300),
					new SqlParameter("@OrganizationID", SqlDbType.Int),
					new SqlParameter("@ImportTypeID", SqlDbType.Int),
					new SqlParameter("@Status", SqlDbType.SmallInt),
					new SqlParameter("@Remark", SqlDbType.VarChar, -1),
					new SqlParameter("@FilePath", SqlDbType.VarChar, 256),
					new SqlParameter("@FilleName", SqlDbType.VarChar, 100),
					new SqlParameter("@CreatorID", SqlDbType.Int),
					new SqlParameter("@CreateTime", SqlDbType.DateTime)
					};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
			}
			
			if (import_Task.TaskName!= null){ parms[1].Value = import_Task.TaskName; } else { parms[1].Value = DBNull.Value; }
			parms[2].Value = import_Task.OrganizationID;
			parms[3].Value = import_Task.ImportTypeID;
			parms[4].Value = import_Task.Status;
			if (import_Task.Remark!= null){ parms[5].Value = import_Task.Remark; } else { parms[5].Value = DBNull.Value; }
			if (import_Task.FilePath!= null){ parms[6].Value = import_Task.FilePath; } else { parms[6].Value = DBNull.Value; }
			if (import_Task.FilleName!= null){ parms[7].Value = import_Task.FilleName; } else { parms[7].Value = DBNull.Value; }
			parms[8].Value = import_Task.CreatorID;
			parms[9].Value = import_Task.CreateTime;
			#endregion
			SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
			
			import_Task.TaskID = (Int32)parms[0].Value;
			
		}
		
		/// <summary>
		/// 删除
		/// </summary>
		public void Remove(Int32 taskID)
		{
			string commandName = "dbo.Pr_Import_Task_Delete";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@TaskID", SqlDbType.Int)
				};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
			}
			
			parms[0].Value = taskID;
			
			#endregion
			SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
		}
		
		/// <summary>
		/// 保存
		/// </summary>
		public void Save(Import_Task import_Task)
		{
			string commandName = "dbo.Pr_Import_Task_Update";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@TaskID", SqlDbType.Int),
					new SqlParameter("@TaskName", SqlDbType.VarChar, 300),
					new SqlParameter("@OrganizationID", SqlDbType.Int),
					new SqlParameter("@ImportTypeID", SqlDbType.Int),
					new SqlParameter("@Status", SqlDbType.SmallInt),
					new SqlParameter("@Remark", SqlDbType.VarChar, -1),
					new SqlParameter("@FilePath", SqlDbType.VarChar, 256),
					new SqlParameter("@FilleName", SqlDbType.VarChar, 100),
					new SqlParameter("@CreatorID", SqlDbType.Int),
					new SqlParameter("@CreateTime", SqlDbType.DateTime)
				};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
			}
			
			parms[0].Value = import_Task.TaskID;
			if (import_Task.TaskName!= null){ parms[1].Value = import_Task.TaskName; } else { parms[1].Value = DBNull.Value; }
			parms[2].Value = import_Task.OrganizationID;
			parms[3].Value = import_Task.ImportTypeID;
			parms[4].Value = import_Task.Status;
			if (import_Task.Remark!= null){ parms[5].Value = import_Task.Remark; } else { parms[5].Value = DBNull.Value; }
			if (import_Task.FilePath!= null){ parms[6].Value = import_Task.FilePath; } else { parms[6].Value = DBNull.Value; }
			if (import_Task.FilleName!= null){ parms[7].Value = import_Task.FilleName; } else { parms[7].Value = DBNull.Value; }
			parms[8].Value = import_Task.CreatorID;
			parms[9].Value = import_Task.CreateTime;
			#endregion
			SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
		}
		
		/// <summary>
		/// 根据标识获取对象
		/// </summary>
		public Import_Task GetById(Int32 taskID)
		{
			Import_Task import_Task = null;
			
			string commandName = "dbo.Pr_Import_Task_GetByPK";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@TaskID", SqlDbType.Int)
				};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
			}
			
			parms[0].Value = taskID;
			
			#endregion
			using (SqlDataReader dataReader = SqlHelper.ExecuteReader(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms))
			{
				if (dataReader.Read())
				{
					import_Task = PopulateImport_TaskFromDataReader(dataReader);
				}
			}
			
			return import_Task;
		}				
		
		/// <summary>
		/// 根据参数获取对象列表（分页，可排序）
		/// </summary>
		public DataTable GetPagedList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
		{			
			string commandName = "dbo.Pr_Import_Task_GetPagedList";
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
		private Import_Task PopulateImport_TaskFromDataReader(SqlDataReader reader)
		{
			Import_Task import_Task = new Import_Task();
			
			int taskIDIndex = reader.GetOrdinal("TaskID"); 
			if(!reader.IsDBNull(taskIDIndex))
			{
				import_Task.TaskID= reader.GetInt32(taskIDIndex);
			}
			
			int taskNameIndex = reader.GetOrdinal("TaskName"); 
			if(!reader.IsDBNull(taskNameIndex))
			{
				import_Task.TaskName= reader.GetString(taskNameIndex);
			}
			
			int organizationIDIndex = reader.GetOrdinal("OrganizationID"); 
			if(!reader.IsDBNull(organizationIDIndex))
			{
				import_Task.OrganizationID= reader.GetInt32(organizationIDIndex);
			}
			
			int importTypeIDIndex = reader.GetOrdinal("ImportTypeID"); 
			if(!reader.IsDBNull(importTypeIDIndex))
			{
				import_Task.ImportTypeID= reader.GetInt32(importTypeIDIndex);
			}
			
			int statusIndex = reader.GetOrdinal("Status"); 
			if(!reader.IsDBNull(statusIndex))
			{
				import_Task.Status= reader.GetInt16(statusIndex);
			}
			
			int remarkIndex = reader.GetOrdinal("Remark"); 
			if(!reader.IsDBNull(remarkIndex))
			{
				import_Task.Remark= reader.GetString(remarkIndex);
			}
			
			int filePathIndex = reader.GetOrdinal("FilePath"); 
			if(!reader.IsDBNull(filePathIndex))
			{
				import_Task.FilePath= reader.GetString(filePathIndex);
			}
			
			int filleNameIndex = reader.GetOrdinal("FilleName"); 
			if(!reader.IsDBNull(filleNameIndex))
			{
				import_Task.FilleName= reader.GetString(filleNameIndex);
			}
			
			int creatorIDIndex = reader.GetOrdinal("CreatorID"); 
			if(!reader.IsDBNull(creatorIDIndex))
			{
				import_Task.CreatorID= reader.GetInt32(creatorIDIndex);
			}
			
			int createTimeIndex = reader.GetOrdinal("CreateTime"); 
			if(!reader.IsDBNull(createTimeIndex))
			{
				import_Task.CreateTime= reader.GetDateTime(createTimeIndex);
			}
			
			return import_Task; 
		}
	}
}
