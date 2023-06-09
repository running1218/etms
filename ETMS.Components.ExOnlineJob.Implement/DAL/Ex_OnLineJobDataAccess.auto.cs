//==================================================================================================
//Version 1.0, auto-generated.
//Generated By running1218
//Date: 2012-4-1 16:00:51.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1)
//==================================================================================================

using System;
using System.Data;

using System.Data.SqlClient;
using ETMS.Utility.Data;

using ETMS.Components.ExOnlineJob.API.Entity.ExOnlineJob;

namespace ETMS.Components.ExOnlineJob.Implement.DAL.ExOnlineJob
{
    /// <summary>
    /// 在线作业表数据访问
    /// </summary>
    public partial class Ex_OnLineJobDataAccess
	{
		/// <summary>
		/// 增加
		/// </summary>
		public void Add(Ex_OnLineJob ex_OnLineJob)
		{
			string commandName = "dbo.Pr_Ex_OnLineJob_Insert";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@OnLineJobID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@OrgID", SqlDbType.Int),
					new SqlParameter("@OnLineJobName", SqlDbType.NVarChar, 200),
					new SqlParameter("@OnLineJobDesc", SqlDbType.NVarChar, -1),
					new SqlParameter("@IsShowAnswer", SqlDbType.Int),
					new SqlParameter("@OnLineJobStatus", SqlDbType.Int),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUserID", SqlDbType.Int),
					new SqlParameter("@CreateUser", SqlDbType.NVarChar, 128),
					new SqlParameter("@ModifyTime", SqlDbType.DateTime),
					new SqlParameter("@ModifyUser", SqlDbType.NVarChar, 128),
					new SqlParameter("@Remark", SqlDbType.NVarChar, -1),
					new SqlParameter("@DelFlag", SqlDbType.Bit),
					new SqlParameter("@TestPaperID", SqlDbType.NVarChar, 100)
					};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
			}
			
			parms[0].Value = ex_OnLineJob.OnLineJobID;
			parms[1].Value = ex_OnLineJob.OrgID;
			if (ex_OnLineJob.OnLineJobName!= null){ parms[2].Value = ex_OnLineJob.OnLineJobName; } else { parms[2].Value = DBNull.Value; }
			if (ex_OnLineJob.OnLineJobDesc!= null){ parms[3].Value = ex_OnLineJob.OnLineJobDesc; } else { parms[3].Value = DBNull.Value; }
			parms[4].Value = ex_OnLineJob.IsShowAnswer;
			parms[5].Value = ex_OnLineJob.OnLineJobStatus;
			parms[6].Value = ex_OnLineJob.CreateTime;
			parms[7].Value = ex_OnLineJob.CreateUserID;
			if (ex_OnLineJob.CreateUser!= null){ parms[8].Value = ex_OnLineJob.CreateUser; } else { parms[8].Value = DBNull.Value; }
			parms[9].Value = ex_OnLineJob.ModifyTime;
			if (ex_OnLineJob.ModifyUser!= null){ parms[10].Value = ex_OnLineJob.ModifyUser; } else { parms[10].Value = DBNull.Value; }
			if (ex_OnLineJob.Remark!= null){ parms[11].Value = ex_OnLineJob.Remark; } else { parms[11].Value = DBNull.Value; }
			parms[12].Value = ex_OnLineJob.DelFlag;
			if (ex_OnLineJob.TestPaperID!= null){ parms[13].Value = ex_OnLineJob.TestPaperID; } else { parms[13].Value = DBNull.Value; }
			#endregion
			SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
			
		}
		
		/// <summary>
		/// 删除
		/// </summary>
		public void Remove(Guid onLineJobID)
		{
			string commandName = "dbo.Pr_Ex_OnLineJob_Delete";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@OnLineJobID", SqlDbType.UniqueIdentifier)
				};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
			}
			
			parms[0].Value = onLineJobID;
			
			#endregion
			SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
		}
		
		/// <summary>
		/// 保存
		/// </summary>
		public void Save(Ex_OnLineJob ex_OnLineJob)
		{
			string commandName = "dbo.Pr_Ex_OnLineJob_Update";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@OnLineJobID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@OrgID", SqlDbType.Int),
					new SqlParameter("@OnLineJobName", SqlDbType.NVarChar, 200),
					new SqlParameter("@OnLineJobDesc", SqlDbType.NVarChar, -1),
					new SqlParameter("@IsShowAnswer", SqlDbType.Int),
					new SqlParameter("@OnLineJobStatus", SqlDbType.Int),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUserID", SqlDbType.Int),
					new SqlParameter("@CreateUser", SqlDbType.NVarChar, 128),
					new SqlParameter("@ModifyTime", SqlDbType.DateTime),
					new SqlParameter("@ModifyUser", SqlDbType.NVarChar, 128),
					new SqlParameter("@Remark", SqlDbType.NVarChar, -1),
					new SqlParameter("@DelFlag", SqlDbType.Bit),
					new SqlParameter("@TestPaperID", SqlDbType.NVarChar, 100)
				};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
			}
			
			parms[0].Value = ex_OnLineJob.OnLineJobID;
			parms[1].Value = ex_OnLineJob.OrgID;
			if (ex_OnLineJob.OnLineJobName!= null){ parms[2].Value = ex_OnLineJob.OnLineJobName; } else { parms[2].Value = DBNull.Value; }
			if (ex_OnLineJob.OnLineJobDesc!= null){ parms[3].Value = ex_OnLineJob.OnLineJobDesc; } else { parms[3].Value = DBNull.Value; }
			parms[4].Value = ex_OnLineJob.IsShowAnswer;
			parms[5].Value = ex_OnLineJob.OnLineJobStatus;
			parms[6].Value = ex_OnLineJob.CreateTime;
			parms[7].Value = ex_OnLineJob.CreateUserID;
			if (ex_OnLineJob.CreateUser!= null){ parms[8].Value = ex_OnLineJob.CreateUser; } else { parms[8].Value = DBNull.Value; }
			parms[9].Value = ex_OnLineJob.ModifyTime;
			if (ex_OnLineJob.ModifyUser!= null){ parms[10].Value = ex_OnLineJob.ModifyUser; } else { parms[10].Value = DBNull.Value; }
			if (ex_OnLineJob.Remark!= null){ parms[11].Value = ex_OnLineJob.Remark; } else { parms[11].Value = DBNull.Value; }
			parms[12].Value = ex_OnLineJob.DelFlag;
			if (ex_OnLineJob.TestPaperID!= null){ parms[13].Value = ex_OnLineJob.TestPaperID; } else { parms[13].Value = DBNull.Value; }
			#endregion
			SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
		}
		
		/// <summary>
		/// 根据标识获取对象
		/// </summary>
		public Ex_OnLineJob GetById(Guid onLineJobID)
		{
			Ex_OnLineJob ex_OnLineJob = null;
			
			string commandName = "dbo.Pr_Ex_OnLineJob_GetByPK";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@OnLineJobID", SqlDbType.UniqueIdentifier)
				};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
			}
			
			parms[0].Value = onLineJobID;
			
			#endregion
			using (SqlDataReader dataReader = SqlHelper.ExecuteReader(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms))
			{
				if (dataReader.Read())
				{
					ex_OnLineJob = PopulateEx_OnLineJobFromDataReader(dataReader);
				}
			}
			
			return ex_OnLineJob;
		}				
		
		/// <summary>
		/// 根据参数获取对象列表（分页，可排序）
		/// </summary>
		public DataTable GetPagedList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
		{			
			string commandName = "dbo.Pr_Ex_OnLineJob_GetPagedList";
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
		private Ex_OnLineJob PopulateEx_OnLineJobFromDataReader(SqlDataReader reader)
		{
			Ex_OnLineJob ex_OnLineJob = new Ex_OnLineJob();
			
			int onLineJobIDIndex = reader.GetOrdinal("OnLineJobID"); 
			if(!reader.IsDBNull(onLineJobIDIndex))
			{
				ex_OnLineJob.OnLineJobID= reader.GetGuid(onLineJobIDIndex);
			}
			
			int orgIDIndex = reader.GetOrdinal("OrgID"); 
			if(!reader.IsDBNull(orgIDIndex))
			{
				ex_OnLineJob.OrgID= reader.GetInt32(orgIDIndex);
			}
			
			int onLineJobNameIndex = reader.GetOrdinal("OnLineJobName"); 
			if(!reader.IsDBNull(onLineJobNameIndex))
			{
				ex_OnLineJob.OnLineJobName= reader.GetString(onLineJobNameIndex);
			}
			
			int onLineJobDescIndex = reader.GetOrdinal("OnLineJobDesc"); 
			if(!reader.IsDBNull(onLineJobDescIndex))
			{
				ex_OnLineJob.OnLineJobDesc= reader.GetString(onLineJobDescIndex);
			}
			
			int isShowAnswerIndex = reader.GetOrdinal("IsShowAnswer"); 
			if(!reader.IsDBNull(isShowAnswerIndex))
			{
				ex_OnLineJob.IsShowAnswer= reader.GetInt32(isShowAnswerIndex);
			}
			
			int onLineJobStatusIndex = reader.GetOrdinal("OnLineJobStatus"); 
			if(!reader.IsDBNull(onLineJobStatusIndex))
			{
				ex_OnLineJob.OnLineJobStatus= reader.GetInt32(onLineJobStatusIndex);
			}
			
			int createTimeIndex = reader.GetOrdinal("CreateTime"); 
			if(!reader.IsDBNull(createTimeIndex))
			{
				ex_OnLineJob.CreateTime= reader.GetDateTime(createTimeIndex);
			}
			
			int createUserIDIndex = reader.GetOrdinal("CreateUserID"); 
			if(!reader.IsDBNull(createUserIDIndex))
			{
				ex_OnLineJob.CreateUserID= reader.GetInt32(createUserIDIndex);
			}
			
			int createUserIndex = reader.GetOrdinal("CreateUser"); 
			if(!reader.IsDBNull(createUserIndex))
			{
				ex_OnLineJob.CreateUser= reader.GetString(createUserIndex);
			}
			
			int modifyTimeIndex = reader.GetOrdinal("ModifyTime"); 
			if(!reader.IsDBNull(modifyTimeIndex))
			{
				ex_OnLineJob.ModifyTime= reader.GetDateTime(modifyTimeIndex);
			}
			
			int modifyUserIndex = reader.GetOrdinal("ModifyUser"); 
			if(!reader.IsDBNull(modifyUserIndex))
			{
				ex_OnLineJob.ModifyUser= reader.GetString(modifyUserIndex);
			}
			
			int remarkIndex = reader.GetOrdinal("Remark"); 
			if(!reader.IsDBNull(remarkIndex))
			{
				ex_OnLineJob.Remark= reader.GetString(remarkIndex);
			}
			
			int delFlagIndex = reader.GetOrdinal("DelFlag"); 
			if(!reader.IsDBNull(delFlagIndex))
			{
				ex_OnLineJob.DelFlag= reader.GetBoolean(delFlagIndex);
			}
			
			int testPaperIDIndex = reader.GetOrdinal("TestPaperID"); 
			if(!reader.IsDBNull(testPaperIDIndex))
			{
				ex_OnLineJob.TestPaperID= reader.GetString(testPaperIDIndex);
			}
			
			return ex_OnLineJob; 
		}
	}
}
