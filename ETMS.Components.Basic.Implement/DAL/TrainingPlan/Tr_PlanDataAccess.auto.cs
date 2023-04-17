//==================================================================================================
//Version 1.0, auto-generated.
//Generated By huangzhf.
//Date: 2012-5-23 16:17:38.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1)
//==================================================================================================

using System;
using System.Collections.Generic;
using System.Data;

using System.Data.SqlClient;
using ETMS.Utility.Data;

using ETMS.Components.Basic.API.Entity.TrainingPlan;

namespace ETMS.Components.Basic.Implement.DAL.TrainingPlan
{
    /// <summary>
    /// 培训计划表数据访问
    /// </summary>
    public partial class Tr_PlanDataAccess
	{
		/// <summary>
		/// 增加
		/// </summary>
		public void Add(Tr_Plan tr_Plan)
		{
			string commandName = "dbo.Pr_Tr_Plan_Insert";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@PlanID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@OrgID", SqlDbType.Int),
					new SqlParameter("@TrainingLevelID", SqlDbType.Int),
					new SqlParameter("@PlanTypeID", SqlDbType.Int),
					new SqlParameter("@SpecialtyTypeCode", SqlDbType.NVarChar, 100),
					new SqlParameter("@PlanCode", SqlDbType.NVarChar, 100),
					new SqlParameter("@PlanName", SqlDbType.NVarChar, 200),
					new SqlParameter("@IsUse", SqlDbType.Int),
					new SqlParameter("@PlanBeginTime", SqlDbType.DateTime),
					new SqlParameter("@PlanEndTime", SqlDbType.DateTime),
					new SqlParameter("@DutyUser", SqlDbType.NVarChar, 128),
					new SqlParameter("@Mobile", SqlDbType.NVarChar, 100),
					new SqlParameter("@EMAIL", SqlDbType.NVarChar, 256),
					new SqlParameter("@BudgetFee", SqlDbType.Decimal, 9, ParameterDirection.Input, true, 15, 2, String.Empty, DataRowVersion.Default, null),
					new SqlParameter("@StudentNum", SqlDbType.Int),
					new SqlParameter("@PlanTarget", SqlDbType.NVarChar, -1),
					new SqlParameter("@PlanObjectStudent", SqlDbType.NVarChar, -1),
					new SqlParameter("@Remark", SqlDbType.NVarChar, -1),
					new SqlParameter("@DutyDeptID", SqlDbType.Int),
					new SqlParameter("@PlanStatus", SqlDbType.Int),
					new SqlParameter("@AuditUser", SqlDbType.NVarChar, 128),
					new SqlParameter("@AuditTime", SqlDbType.DateTime),
					new SqlParameter("@AuditOpinion", SqlDbType.NVarChar, -1),
					new SqlParameter("@PlanEndRemark", SqlDbType.NVarChar, -1),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUserID", SqlDbType.Int),
					new SqlParameter("@CreateUser", SqlDbType.NVarChar, 128),
					new SqlParameter("@ModifyTime", SqlDbType.DateTime),
					new SqlParameter("@ModifyUser", SqlDbType.NVarChar, 128),
					new SqlParameter("@DelFlag", SqlDbType.Bit),
					new SqlParameter("@PlanEndModeID", SqlDbType.Int)
					};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
			}
			
			parms[0].Value = tr_Plan.PlanID;
			parms[1].Value = tr_Plan.OrgID;
			parms[2].Value = tr_Plan.TrainingLevelID;
			parms[3].Value = tr_Plan.PlanTypeID;
			if (tr_Plan.SpecialtyTypeCode!= null){ parms[4].Value = tr_Plan.SpecialtyTypeCode; } else { parms[4].Value = DBNull.Value; }
			if (tr_Plan.PlanCode!= null){ parms[5].Value = tr_Plan.PlanCode; } else { parms[5].Value = DBNull.Value; }
			if (tr_Plan.PlanName!= null){ parms[6].Value = tr_Plan.PlanName; } else { parms[6].Value = DBNull.Value; }
			parms[7].Value = tr_Plan.IsUse;
			parms[8].Value = tr_Plan.PlanBeginTime;
			parms[9].Value = tr_Plan.PlanEndTime;
			if (tr_Plan.DutyUser!= null){ parms[10].Value = tr_Plan.DutyUser; } else { parms[10].Value = DBNull.Value; }
			if (tr_Plan.Mobile!= null){ parms[11].Value = tr_Plan.Mobile; } else { parms[11].Value = DBNull.Value; }
			if (tr_Plan.EMAIL!= null){ parms[12].Value = tr_Plan.EMAIL; } else { parms[12].Value = DBNull.Value; }
			parms[13].Value = tr_Plan.BudgetFee;
			parms[14].Value = tr_Plan.StudentNum;
			if (tr_Plan.PlanTarget!= null){ parms[15].Value = tr_Plan.PlanTarget; } else { parms[15].Value = DBNull.Value; }
			if (tr_Plan.PlanObjectStudent!= null){ parms[16].Value = tr_Plan.PlanObjectStudent; } else { parms[16].Value = DBNull.Value; }
			if (tr_Plan.Remark!= null){ parms[17].Value = tr_Plan.Remark; } else { parms[17].Value = DBNull.Value; }
			parms[18].Value = tr_Plan.DutyDeptID;
			parms[19].Value = tr_Plan.PlanStatus;
			if (tr_Plan.AuditUser!= null){ parms[20].Value = tr_Plan.AuditUser; } else { parms[20].Value = DBNull.Value; }
			parms[21].Value = tr_Plan.AuditTime;
			if (tr_Plan.AuditOpinion!= null){ parms[22].Value = tr_Plan.AuditOpinion; } else { parms[22].Value = DBNull.Value; }
			if (tr_Plan.PlanEndRemark!= null){ parms[23].Value = tr_Plan.PlanEndRemark; } else { parms[23].Value = DBNull.Value; }
			parms[24].Value = tr_Plan.CreateTime;
			parms[25].Value = tr_Plan.CreateUserID;
			if (tr_Plan.CreateUser!= null){ parms[26].Value = tr_Plan.CreateUser; } else { parms[26].Value = DBNull.Value; }
			parms[27].Value = tr_Plan.ModifyTime;
			if (tr_Plan.ModifyUser!= null){ parms[28].Value = tr_Plan.ModifyUser; } else { parms[28].Value = DBNull.Value; }
			parms[29].Value = tr_Plan.DelFlag;
			parms[30].Value = tr_Plan.PlanEndModeID;
			#endregion
			SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
			
		}
		
		/// <summary>
		/// 删除
		/// </summary>
		public void Remove(Guid planID)
		{
			string commandName = "dbo.Pr_Tr_Plan_Delete";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@PlanID", SqlDbType.UniqueIdentifier)
				};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
			}
			
			parms[0].Value = planID;
			
			#endregion
			SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
		}
		
		/// <summary>
		/// 保存
		/// </summary>
		public void Save(Tr_Plan tr_Plan)
		{
			string commandName = "dbo.Pr_Tr_Plan_Update";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@PlanID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@OrgID", SqlDbType.Int),
					new SqlParameter("@TrainingLevelID", SqlDbType.Int),
					new SqlParameter("@PlanTypeID", SqlDbType.Int),
					new SqlParameter("@SpecialtyTypeCode", SqlDbType.NVarChar, 100),
					new SqlParameter("@PlanCode", SqlDbType.NVarChar, 100),
					new SqlParameter("@PlanName", SqlDbType.NVarChar, 200),
					new SqlParameter("@IsUse", SqlDbType.Int),
					new SqlParameter("@PlanBeginTime", SqlDbType.DateTime),
					new SqlParameter("@PlanEndTime", SqlDbType.DateTime),
					new SqlParameter("@DutyUser", SqlDbType.NVarChar, 128),
					new SqlParameter("@Mobile", SqlDbType.NVarChar, 100),
					new SqlParameter("@EMAIL", SqlDbType.NVarChar, 256),
					new SqlParameter("@BudgetFee", SqlDbType.Decimal, 9, ParameterDirection.Input, true, 15, 2, String.Empty, DataRowVersion.Default, null),
					new SqlParameter("@StudentNum", SqlDbType.Int),
					new SqlParameter("@PlanTarget", SqlDbType.NVarChar, -1),
					new SqlParameter("@PlanObjectStudent", SqlDbType.NVarChar, -1),
					new SqlParameter("@Remark", SqlDbType.NVarChar, -1),
					new SqlParameter("@DutyDeptID", SqlDbType.Int),
					new SqlParameter("@PlanStatus", SqlDbType.Int),
					new SqlParameter("@AuditUser", SqlDbType.NVarChar, 128),
					new SqlParameter("@AuditTime", SqlDbType.DateTime),
					new SqlParameter("@AuditOpinion", SqlDbType.NVarChar, -1),
					new SqlParameter("@PlanEndRemark", SqlDbType.NVarChar, -1),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUserID", SqlDbType.Int),
					new SqlParameter("@CreateUser", SqlDbType.NVarChar, 128),
					new SqlParameter("@ModifyTime", SqlDbType.DateTime),
					new SqlParameter("@ModifyUser", SqlDbType.NVarChar, 128),
					new SqlParameter("@DelFlag", SqlDbType.Bit),
					new SqlParameter("@PlanEndModeID", SqlDbType.Int)
				};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
			}
			
			parms[0].Value = tr_Plan.PlanID;
			parms[1].Value = tr_Plan.OrgID;
			parms[2].Value = tr_Plan.TrainingLevelID;
			parms[3].Value = tr_Plan.PlanTypeID;
			if (tr_Plan.SpecialtyTypeCode!= null){ parms[4].Value = tr_Plan.SpecialtyTypeCode; } else { parms[4].Value = DBNull.Value; }
			if (tr_Plan.PlanCode!= null){ parms[5].Value = tr_Plan.PlanCode; } else { parms[5].Value = DBNull.Value; }
			if (tr_Plan.PlanName!= null){ parms[6].Value = tr_Plan.PlanName; } else { parms[6].Value = DBNull.Value; }
			parms[7].Value = tr_Plan.IsUse;
			parms[8].Value = tr_Plan.PlanBeginTime;
			parms[9].Value = tr_Plan.PlanEndTime;
			if (tr_Plan.DutyUser!= null){ parms[10].Value = tr_Plan.DutyUser; } else { parms[10].Value = DBNull.Value; }
			if (tr_Plan.Mobile!= null){ parms[11].Value = tr_Plan.Mobile; } else { parms[11].Value = DBNull.Value; }
			if (tr_Plan.EMAIL!= null){ parms[12].Value = tr_Plan.EMAIL; } else { parms[12].Value = DBNull.Value; }
			parms[13].Value = tr_Plan.BudgetFee;
			parms[14].Value = tr_Plan.StudentNum;
			if (tr_Plan.PlanTarget!= null){ parms[15].Value = tr_Plan.PlanTarget; } else { parms[15].Value = DBNull.Value; }
			if (tr_Plan.PlanObjectStudent!= null){ parms[16].Value = tr_Plan.PlanObjectStudent; } else { parms[16].Value = DBNull.Value; }
			if (tr_Plan.Remark!= null){ parms[17].Value = tr_Plan.Remark; } else { parms[17].Value = DBNull.Value; }
			parms[18].Value = tr_Plan.DutyDeptID;
			parms[19].Value = tr_Plan.PlanStatus;
			if (tr_Plan.AuditUser!= null){ parms[20].Value = tr_Plan.AuditUser; } else { parms[20].Value = DBNull.Value; }
			parms[21].Value = tr_Plan.AuditTime;
			if (tr_Plan.AuditOpinion!= null){ parms[22].Value = tr_Plan.AuditOpinion; } else { parms[22].Value = DBNull.Value; }
			if (tr_Plan.PlanEndRemark!= null){ parms[23].Value = tr_Plan.PlanEndRemark; } else { parms[23].Value = DBNull.Value; }
			parms[24].Value = tr_Plan.CreateTime;
			parms[25].Value = tr_Plan.CreateUserID;
			if (tr_Plan.CreateUser!= null){ parms[26].Value = tr_Plan.CreateUser; } else { parms[26].Value = DBNull.Value; }
			parms[27].Value = tr_Plan.ModifyTime;
			if (tr_Plan.ModifyUser!= null){ parms[28].Value = tr_Plan.ModifyUser; } else { parms[28].Value = DBNull.Value; }
			parms[29].Value = tr_Plan.DelFlag;
			parms[30].Value = tr_Plan.PlanEndModeID;
			#endregion
			SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
		}
		
		/// <summary>
		/// 根据标识获取对象
		/// </summary>
		public Tr_Plan GetById(Guid planID)
		{
			Tr_Plan tr_Plan = null;
			
			string commandName = "dbo.Pr_Tr_Plan_GetByPK";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@PlanID", SqlDbType.UniqueIdentifier)
				};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
			}
			
			parms[0].Value = planID;
			
			#endregion
			using (SqlDataReader dataReader = SqlHelper.ExecuteReader(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms))
			{
				if (dataReader.Read())
				{
					tr_Plan = PopulateTr_PlanFromDataReader(dataReader);
				}
			}
			
			return tr_Plan;
		}				
		
		/// <summary>
		/// 根据参数获取对象列表（分页，可排序）
		/// </summary>
		public DataTable GetPagedList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
		{			
			string commandName = "dbo.Pr_Tr_Plan_GetPagedList";
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
		public IList<Tr_Plan> GetEntityList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
		{			
            IList<Tr_Plan> list=new List<Tr_Plan>();
			string commandName = "dbo.Pr_Tr_Plan_GetPagedList";
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
					list.Add(PopulateTr_PlanFromDataReader(dataReader));
				}
			}			
			totalRecords = (int)parms[4].Value;
			return list;
		}
		
		/// <summary>
		/// 从DataReader中读取数据到业务对象
		/// </summary>
		private Tr_Plan PopulateTr_PlanFromDataReader(SqlDataReader reader)
		{
			Tr_Plan tr_Plan = new Tr_Plan();
			
			int planIDIndex = reader.GetOrdinal("PlanID"); 
			if(!reader.IsDBNull(planIDIndex))
			{
				tr_Plan.PlanID= reader.GetGuid(planIDIndex);
			}
			
			int orgIDIndex = reader.GetOrdinal("OrgID"); 
			if(!reader.IsDBNull(orgIDIndex))
			{
				tr_Plan.OrgID= reader.GetInt32(orgIDIndex);
			}
			
			int trainingLevelIDIndex = reader.GetOrdinal("TrainingLevelID"); 
			if(!reader.IsDBNull(trainingLevelIDIndex))
			{
				tr_Plan.TrainingLevelID= reader.GetInt32(trainingLevelIDIndex);
			}
			
			int planTypeIDIndex = reader.GetOrdinal("PlanTypeID"); 
			if(!reader.IsDBNull(planTypeIDIndex))
			{
				tr_Plan.PlanTypeID= reader.GetInt32(planTypeIDIndex);
			}
			
			int specialtyTypeCodeIndex = reader.GetOrdinal("SpecialtyTypeCode"); 
			if(!reader.IsDBNull(specialtyTypeCodeIndex))
			{
				tr_Plan.SpecialtyTypeCode= reader.GetString(specialtyTypeCodeIndex);
			}
			
			int planCodeIndex = reader.GetOrdinal("PlanCode"); 
			if(!reader.IsDBNull(planCodeIndex))
			{
				tr_Plan.PlanCode= reader.GetString(planCodeIndex);
			}
			
			int planNameIndex = reader.GetOrdinal("PlanName"); 
			if(!reader.IsDBNull(planNameIndex))
			{
				tr_Plan.PlanName= reader.GetString(planNameIndex);
			}
			
			int isUseIndex = reader.GetOrdinal("IsUse"); 
			if(!reader.IsDBNull(isUseIndex))
			{
				tr_Plan.IsUse= reader.GetInt32(isUseIndex);
			}
			
			int planBeginTimeIndex = reader.GetOrdinal("PlanBeginTime"); 
			if(!reader.IsDBNull(planBeginTimeIndex))
			{
				tr_Plan.PlanBeginTime= reader.GetDateTime(planBeginTimeIndex);
			}
			
			int planEndTimeIndex = reader.GetOrdinal("PlanEndTime"); 
			if(!reader.IsDBNull(planEndTimeIndex))
			{
				tr_Plan.PlanEndTime= reader.GetDateTime(planEndTimeIndex);
			}
			
			int dutyUserIndex = reader.GetOrdinal("DutyUser"); 
			if(!reader.IsDBNull(dutyUserIndex))
			{
				tr_Plan.DutyUser= reader.GetString(dutyUserIndex);
			}
			
			int mobileIndex = reader.GetOrdinal("Mobile"); 
			if(!reader.IsDBNull(mobileIndex))
			{
				tr_Plan.Mobile= reader.GetString(mobileIndex);
			}
			
			int eMAILIndex = reader.GetOrdinal("EMAIL"); 
			if(!reader.IsDBNull(eMAILIndex))
			{
				tr_Plan.EMAIL= reader.GetString(eMAILIndex);
			}
			
			int budgetFeeIndex = reader.GetOrdinal("BudgetFee"); 
			if(!reader.IsDBNull(budgetFeeIndex))
			{
				tr_Plan.BudgetFee= reader.GetDecimal(budgetFeeIndex);
			}
			
			int studentNumIndex = reader.GetOrdinal("StudentNum"); 
			if(!reader.IsDBNull(studentNumIndex))
			{
				tr_Plan.StudentNum= reader.GetInt32(studentNumIndex);
			}
			
			int planTargetIndex = reader.GetOrdinal("PlanTarget"); 
			if(!reader.IsDBNull(planTargetIndex))
			{
				tr_Plan.PlanTarget= reader.GetString(planTargetIndex);
			}
			
			int planObjectStudentIndex = reader.GetOrdinal("PlanObjectStudent"); 
			if(!reader.IsDBNull(planObjectStudentIndex))
			{
				tr_Plan.PlanObjectStudent= reader.GetString(planObjectStudentIndex);
			}
			
			int remarkIndex = reader.GetOrdinal("Remark"); 
			if(!reader.IsDBNull(remarkIndex))
			{
				tr_Plan.Remark= reader.GetString(remarkIndex);
			}
			
			int dutyDeptIDIndex = reader.GetOrdinal("DutyDeptID"); 
			if(!reader.IsDBNull(dutyDeptIDIndex))
			{
				tr_Plan.DutyDeptID= reader.GetInt32(dutyDeptIDIndex);
			}
			
			int planStatusIndex = reader.GetOrdinal("PlanStatus"); 
			if(!reader.IsDBNull(planStatusIndex))
			{
				tr_Plan.PlanStatus= reader.GetInt32(planStatusIndex);
			}
			
			int auditUserIndex = reader.GetOrdinal("AuditUser"); 
			if(!reader.IsDBNull(auditUserIndex))
			{
				tr_Plan.AuditUser= reader.GetString(auditUserIndex);
			}
			
			int auditTimeIndex = reader.GetOrdinal("AuditTime"); 
			if(!reader.IsDBNull(auditTimeIndex))
			{
				tr_Plan.AuditTime= reader.GetDateTime(auditTimeIndex);
			}
			
			int auditOpinionIndex = reader.GetOrdinal("AuditOpinion"); 
			if(!reader.IsDBNull(auditOpinionIndex))
			{
				tr_Plan.AuditOpinion= reader.GetString(auditOpinionIndex);
			}
			
			int planEndRemarkIndex = reader.GetOrdinal("PlanEndRemark"); 
			if(!reader.IsDBNull(planEndRemarkIndex))
			{
				tr_Plan.PlanEndRemark= reader.GetString(planEndRemarkIndex);
			}
			
			int createTimeIndex = reader.GetOrdinal("CreateTime"); 
			if(!reader.IsDBNull(createTimeIndex))
			{
				tr_Plan.CreateTime= reader.GetDateTime(createTimeIndex);
			}
			
			int createUserIDIndex = reader.GetOrdinal("CreateUserID"); 
			if(!reader.IsDBNull(createUserIDIndex))
			{
				tr_Plan.CreateUserID= reader.GetInt32(createUserIDIndex);
			}
			
			int createUserIndex = reader.GetOrdinal("CreateUser"); 
			if(!reader.IsDBNull(createUserIndex))
			{
				tr_Plan.CreateUser= reader.GetString(createUserIndex);
			}
			
			int modifyTimeIndex = reader.GetOrdinal("ModifyTime"); 
			if(!reader.IsDBNull(modifyTimeIndex))
			{
				tr_Plan.ModifyTime= reader.GetDateTime(modifyTimeIndex);
			}
			
			int modifyUserIndex = reader.GetOrdinal("ModifyUser"); 
			if(!reader.IsDBNull(modifyUserIndex))
			{
				tr_Plan.ModifyUser= reader.GetString(modifyUserIndex);
			}
			
			int delFlagIndex = reader.GetOrdinal("DelFlag"); 
			if(!reader.IsDBNull(delFlagIndex))
			{
				tr_Plan.DelFlag= reader.GetBoolean(delFlagIndex);
			}
			
			int planEndModeIDIndex = reader.GetOrdinal("PlanEndModeID"); 
			if(!reader.IsDBNull(planEndModeIDIndex))
			{
				tr_Plan.PlanEndModeID= reader.GetInt32(planEndModeIDIndex);
			}
			
			return tr_Plan; 
		}
	}
}
