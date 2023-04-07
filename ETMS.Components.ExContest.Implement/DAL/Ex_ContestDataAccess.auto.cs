//==================================================================================================
//Version 1.0, auto-generated.
//Generated By xueyb.
//Date: 2012-4-1 16:17:20.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1), a product of ZhouYonghua(Zhou_Yonghua@163.com).
//==================================================================================================

using System;
using System.Data;

using System.Data.SqlClient;
using ETMS.Utility.Data;

using ETMS.Components.ExContest.API.Entity;

namespace ETMS.Components.ExContest.Implement.DAL
{
    /// <summary>
    /// 闯关竞赛表数据访问
    /// </summary>
    public partial class Ex_ContestDataAccess
	{
		/// <summary>
		/// 增加
		/// </summary>
		public void Add(Ex_Contest ex_Contest)
		{
			string commandName = "dbo.Pr_Ex_Contest_Insert";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@ContestID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@OrgID", SqlDbType.Int),
					new SqlParameter("@ContestName", SqlDbType.NVarChar, 200),
					new SqlParameter("@ContestDesc", SqlDbType.NVarChar, -1),
					new SqlParameter("@IsShowAnswer", SqlDbType.Int),
					new SqlParameter("@ContestStatus", SqlDbType.Int),
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
			
			parms[0].Value = ex_Contest.ContestID;
			parms[1].Value = ex_Contest.OrgID;
			if (ex_Contest.ContestName!= null){ parms[2].Value = ex_Contest.ContestName; } else { parms[2].Value = DBNull.Value; }
			if (ex_Contest.ContestDesc!= null){ parms[3].Value = ex_Contest.ContestDesc; } else { parms[3].Value = DBNull.Value; }
			parms[4].Value = ex_Contest.IsShowAnswer;
			parms[5].Value = ex_Contest.ContestStatus;
			parms[6].Value = ex_Contest.CreateTime;
			parms[7].Value = ex_Contest.CreateUserID;
			if (ex_Contest.CreateUser!= null){ parms[8].Value = ex_Contest.CreateUser; } else { parms[8].Value = DBNull.Value; }
			parms[9].Value = ex_Contest.ModifyTime;
			if (ex_Contest.ModifyUser!= null){ parms[10].Value = ex_Contest.ModifyUser; } else { parms[10].Value = DBNull.Value; }
			if (ex_Contest.Remark!= null){ parms[11].Value = ex_Contest.Remark; } else { parms[11].Value = DBNull.Value; }
			parms[12].Value = ex_Contest.DelFlag;
			if (ex_Contest.TestPaperID!= null){ parms[13].Value = ex_Contest.TestPaperID; } else { parms[13].Value = DBNull.Value; }
			#endregion
			SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
			
		}
		
		/// <summary>
		/// 删除
		/// </summary>
		public void Remove(Guid contestID)
		{
			string commandName = "dbo.Pr_Ex_Contest_Delete";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@ContestID", SqlDbType.UniqueIdentifier)
				};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
			}
			
			parms[0].Value = contestID;
			
			#endregion
			SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
		}
		
		/// <summary>
		/// 保存
		/// </summary>
		public void Save(Ex_Contest ex_Contest)
		{
			string commandName = "dbo.Pr_Ex_Contest_Update";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@ContestID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@OrgID", SqlDbType.Int),
					new SqlParameter("@ContestName", SqlDbType.NVarChar, 200),
					new SqlParameter("@ContestDesc", SqlDbType.NVarChar, -1),
					new SqlParameter("@IsShowAnswer", SqlDbType.Int),
					new SqlParameter("@ContestStatus", SqlDbType.Int),
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
			
			parms[0].Value = ex_Contest.ContestID;
			parms[1].Value = ex_Contest.OrgID;
			if (ex_Contest.ContestName!= null){ parms[2].Value = ex_Contest.ContestName; } else { parms[2].Value = DBNull.Value; }
			if (ex_Contest.ContestDesc!= null){ parms[3].Value = ex_Contest.ContestDesc; } else { parms[3].Value = DBNull.Value; }
			parms[4].Value = ex_Contest.IsShowAnswer;
			parms[5].Value = ex_Contest.ContestStatus;
			parms[6].Value = ex_Contest.CreateTime;
			parms[7].Value = ex_Contest.CreateUserID;
			if (ex_Contest.CreateUser!= null){ parms[8].Value = ex_Contest.CreateUser; } else { parms[8].Value = DBNull.Value; }
			parms[9].Value = ex_Contest.ModifyTime;
			if (ex_Contest.ModifyUser!= null){ parms[10].Value = ex_Contest.ModifyUser; } else { parms[10].Value = DBNull.Value; }
			if (ex_Contest.Remark!= null){ parms[11].Value = ex_Contest.Remark; } else { parms[11].Value = DBNull.Value; }
			parms[12].Value = ex_Contest.DelFlag;
			if (ex_Contest.TestPaperID!= null){ parms[13].Value = ex_Contest.TestPaperID; } else { parms[13].Value = DBNull.Value; }
			#endregion
			SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
		}
		
		/// <summary>
		/// 根据标识获取对象
		/// </summary>
		public Ex_Contest GetById(Guid contestID)
		{
			Ex_Contest ex_Contest = null;
			
			string commandName = "dbo.Pr_Ex_Contest_GetByPK";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@ContestID", SqlDbType.UniqueIdentifier)
				};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
			}
			
			parms[0].Value = contestID;
			
			#endregion
			using (SqlDataReader dataReader = SqlHelper.ExecuteReader(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms))
			{
				if (dataReader.Read())
				{
					ex_Contest = PopulateEx_ContestFromDataReader(dataReader);
				}
			}
			
			return ex_Contest;
		}				
		
		/// <summary>
		/// 根据参数获取对象列表（分页，可排序）
		/// </summary>
		public DataTable GetPagedList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
		{			
			string commandName = "dbo.Pr_Ex_Contest_GetPagedList";
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
		private Ex_Contest PopulateEx_ContestFromDataReader(SqlDataReader reader)
		{
			Ex_Contest ex_Contest = new Ex_Contest();
			
			int contestIDIndex = reader.GetOrdinal("ContestID"); 
			if(!reader.IsDBNull(contestIDIndex))
			{
				ex_Contest.ContestID= reader.GetGuid(contestIDIndex);
			}
			
			int orgIDIndex = reader.GetOrdinal("OrgID"); 
			if(!reader.IsDBNull(orgIDIndex))
			{
				ex_Contest.OrgID= reader.GetInt32(orgIDIndex);
			}
			
			int contestNameIndex = reader.GetOrdinal("ContestName"); 
			if(!reader.IsDBNull(contestNameIndex))
			{
				ex_Contest.ContestName= reader.GetString(contestNameIndex);
			}
			
			int contestDescIndex = reader.GetOrdinal("ContestDesc"); 
			if(!reader.IsDBNull(contestDescIndex))
			{
				ex_Contest.ContestDesc= reader.GetString(contestDescIndex);
			}
			
			int isShowAnswerIndex = reader.GetOrdinal("IsShowAnswer"); 
			if(!reader.IsDBNull(isShowAnswerIndex))
			{
				ex_Contest.IsShowAnswer= reader.GetInt32(isShowAnswerIndex);
			}
			
			int contestStatusIndex = reader.GetOrdinal("ContestStatus"); 
			if(!reader.IsDBNull(contestStatusIndex))
			{
				ex_Contest.ContestStatus= reader.GetInt32(contestStatusIndex);
			}
			
			int createTimeIndex = reader.GetOrdinal("CreateTime"); 
			if(!reader.IsDBNull(createTimeIndex))
			{
				ex_Contest.CreateTime= reader.GetDateTime(createTimeIndex);
			}
			
			int createUserIDIndex = reader.GetOrdinal("CreateUserID"); 
			if(!reader.IsDBNull(createUserIDIndex))
			{
				ex_Contest.CreateUserID= reader.GetInt32(createUserIDIndex);
			}
			
			int createUserIndex = reader.GetOrdinal("CreateUser"); 
			if(!reader.IsDBNull(createUserIndex))
			{
				ex_Contest.CreateUser= reader.GetString(createUserIndex);
			}
			
			int modifyTimeIndex = reader.GetOrdinal("ModifyTime"); 
			if(!reader.IsDBNull(modifyTimeIndex))
			{
				ex_Contest.ModifyTime= reader.GetDateTime(modifyTimeIndex);
			}
			
			int modifyUserIndex = reader.GetOrdinal("ModifyUser"); 
			if(!reader.IsDBNull(modifyUserIndex))
			{
				ex_Contest.ModifyUser= reader.GetString(modifyUserIndex);
			}
			
			int remarkIndex = reader.GetOrdinal("Remark"); 
			if(!reader.IsDBNull(remarkIndex))
			{
				ex_Contest.Remark= reader.GetString(remarkIndex);
			}
			
			int delFlagIndex = reader.GetOrdinal("DelFlag"); 
			if(!reader.IsDBNull(delFlagIndex))
			{
				ex_Contest.DelFlag= reader.GetBoolean(delFlagIndex);
			}
			
			int testPaperIDIndex = reader.GetOrdinal("TestPaperID"); 
			if(!reader.IsDBNull(testPaperIDIndex))
			{
				ex_Contest.TestPaperID= reader.GetString(testPaperIDIndex);
			}
			
			return ex_Contest; 
		}
	}
}
