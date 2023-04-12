//==================================================================================================
//Version 1.0, auto-generated.
//Generated By huangzhf.
//Date: 2012-5-23 16:08:37.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1), a product of ZhouYonghua(Zhou_Yonghua@163.com).
//==================================================================================================

using System;
using System.Collections.Generic;
using System.Data;

using System.Data.SqlClient;
using ETMS.Utility.Data;

using ETMS.Components.Basic.API.Entity.TrainingItem;

namespace ETMS.Components.Basic.Implement.DAL.TrainingItem
{
    /// <summary>
    /// ��ѵ��Ŀ�����ݷ���
    /// </summary>
    public partial class Tr_ItemDataAccess
	{
		/// <summary>
		/// ����
		/// </summary>
		public void Add(Tr_Item tr_Item)
		{
			string commandName = "dbo.Pr_Tr_Item_Insert";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@TrainingItemID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@SpecialtyTypeCode", SqlDbType.NVarChar, 100),
					new SqlParameter("@IsPlanItem", SqlDbType.Bit),
					new SqlParameter("@PlanID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@OrgID", SqlDbType.Int),
					new SqlParameter("@TrainingLevelID", SqlDbType.Int),
					new SqlParameter("@DutyDeptID", SqlDbType.Int),
					new SqlParameter("@ItemCode", SqlDbType.NVarChar, 100),
					new SqlParameter("@ItemName", SqlDbType.NVarChar, 200),
					new SqlParameter("@ItemStatus", SqlDbType.Int),
					new SqlParameter("@ItemBeginTime", SqlDbType.DateTime),
					new SqlParameter("@ItemEndTime", SqlDbType.DateTime),
					new SqlParameter("@BudgetFee", SqlDbType.Decimal, 9, ParameterDirection.Input, true, 15, 2, String.Empty, DataRowVersion.Default, null),
					new SqlParameter("@DutyUser", SqlDbType.NVarChar, 128),
					new SqlParameter("@Mobile", SqlDbType.NVarChar, 100),
					new SqlParameter("@EMAIL", SqlDbType.NVarChar, 256),
					new SqlParameter("@ItemTarget", SqlDbType.NVarChar, -1),
					new SqlParameter("@ItemObjectStudent", SqlDbType.NVarChar, -1),
					new SqlParameter("@Remark", SqlDbType.NVarChar, -1),
					new SqlParameter("@AuditUser", SqlDbType.NVarChar, 128),
					new SqlParameter("@AuditTime", SqlDbType.DateTime),
					new SqlParameter("@AuditOpinion", SqlDbType.NVarChar, -1),
					new SqlParameter("@IsIssue", SqlDbType.Bit),
					new SqlParameter("@IssueUser", SqlDbType.NVarChar, 128),
					new SqlParameter("@IssueTime", SqlDbType.DateTime),
					new SqlParameter("@ItemEndModeID", SqlDbType.Int),
					new SqlParameter("@SignupModeID", SqlDbType.Int),
					new SqlParameter("@ItemEndReMark", SqlDbType.NVarChar, -1),
					new SqlParameter("@IsUse", SqlDbType.Int),
					new SqlParameter("@IsOrgItem", SqlDbType.Bit),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUserID", SqlDbType.Int),
					new SqlParameter("@CreateUser", SqlDbType.NVarChar, 128),
					new SqlParameter("@ModifyTime", SqlDbType.DateTime),
					new SqlParameter("@ModifyUser", SqlDbType.NVarChar, 128),
					new SqlParameter("@DelFlag", SqlDbType.Bit),
					new SqlParameter("@SignupBeginTime", SqlDbType.DateTime),
					new SqlParameter("@SignupEndTime", SqlDbType.DateTime),
					new SqlParameter("@IsAllowSignup", SqlDbType.Bit),
					new SqlParameter("@IsIssuePoint", SqlDbType.Bit),
					new SqlParameter("@PointIssueUser", SqlDbType.NVarChar, 128),
					new SqlParameter("@PointIssueTime", SqlDbType.DateTime),
                    new SqlParameter("@ThumbnailURL",SqlDbType.NVarChar)
					};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
			}
			
			parms[0].Value = tr_Item.TrainingItemID;
			if (tr_Item.SpecialtyTypeCode!= null){ parms[1].Value = tr_Item.SpecialtyTypeCode; } else { parms[1].Value = DBNull.Value; }
			parms[2].Value = tr_Item.IsPlanItem;
			parms[3].Value = tr_Item.PlanID;
			parms[4].Value = tr_Item.OrgID;
			parms[5].Value = tr_Item.TrainingLevelID;
			parms[6].Value = tr_Item.DutyDeptID;
			if (tr_Item.ItemCode!= null){ parms[7].Value = tr_Item.ItemCode; } else { parms[7].Value = DBNull.Value; }
			if (tr_Item.ItemName!= null){ parms[8].Value = tr_Item.ItemName; } else { parms[8].Value = DBNull.Value; }
			parms[9].Value = tr_Item.ItemStatus;
			parms[10].Value = tr_Item.ItemBeginTime;
			parms[11].Value = tr_Item.ItemEndTime;
			parms[12].Value = tr_Item.BudgetFee;
			if (tr_Item.DutyUser!= null){ parms[13].Value = tr_Item.DutyUser; } else { parms[13].Value = DBNull.Value; }
			if (tr_Item.Mobile!= null){ parms[14].Value = tr_Item.Mobile; } else { parms[14].Value = DBNull.Value; }
			if (tr_Item.EMAIL!= null){ parms[15].Value = tr_Item.EMAIL; } else { parms[15].Value = DBNull.Value; }
			if (tr_Item.ItemTarget!= null){ parms[16].Value = tr_Item.ItemTarget; } else { parms[16].Value = DBNull.Value; }
			if (tr_Item.ItemObjectStudent!= null){ parms[17].Value = tr_Item.ItemObjectStudent; } else { parms[17].Value = DBNull.Value; }
			if (tr_Item.Remark!= null){ parms[18].Value = tr_Item.Remark; } else { parms[18].Value = DBNull.Value; }
			if (tr_Item.AuditUser!= null){ parms[19].Value = tr_Item.AuditUser; } else { parms[19].Value = DBNull.Value; }
			parms[20].Value = tr_Item.AuditTime;
			if (tr_Item.AuditOpinion!= null){ parms[21].Value = tr_Item.AuditOpinion; } else { parms[21].Value = DBNull.Value; }
			parms[22].Value = tr_Item.IsIssue;
			if (tr_Item.IssueUser!= null){ parms[23].Value = tr_Item.IssueUser; } else { parms[23].Value = DBNull.Value; }
			parms[24].Value = tr_Item.IssueTime;
			parms[25].Value = tr_Item.ItemEndModeID;
			parms[26].Value = tr_Item.SignupModeID;
			if (tr_Item.ItemEndReMark!= null){ parms[27].Value = tr_Item.ItemEndReMark; } else { parms[27].Value = DBNull.Value; }
			parms[28].Value = tr_Item.IsUse;
			parms[29].Value = tr_Item.IsOrgItem;
			parms[30].Value = tr_Item.CreateTime;
			parms[31].Value = tr_Item.CreateUserID;
			if (tr_Item.CreateUser!= null){ parms[32].Value = tr_Item.CreateUser; } else { parms[32].Value = DBNull.Value; }
			parms[33].Value = tr_Item.ModifyTime;
			if (tr_Item.ModifyUser!= null){ parms[34].Value = tr_Item.ModifyUser; } else { parms[34].Value = DBNull.Value; }
			parms[35].Value = tr_Item.DelFlag;
			parms[36].Value = tr_Item.SignupBeginTime;
			parms[37].Value = tr_Item.SignupEndTime;
			parms[38].Value = tr_Item.IsAllowSignup;
			parms[39].Value = tr_Item.IsIssuePoint;
			if (tr_Item.PointIssueUser!= null){ parms[40].Value = tr_Item.PointIssueUser; } else { parms[40].Value = DBNull.Value; }
			parms[41].Value = tr_Item.PointIssueTime;
            parms[42].Value = tr_Item.ThumbnailURL;
			#endregion
			SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
			
		}
		
		/// <summary>
		/// ɾ��
		/// </summary>
		public void Remove(Guid trainingItemID)
		{
			string commandName = "dbo.Pr_Tr_Item_Delete";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@TrainingItemID", SqlDbType.UniqueIdentifier)
				};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
			}
			
			parms[0].Value = trainingItemID;
			
			#endregion
			SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
		}
		
		/// <summary>
		/// ����
		/// </summary>
		public void Save(Tr_Item tr_Item)
		{
			string commandName = "dbo.Pr_Tr_Item_Update";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@TrainingItemID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@SpecialtyTypeCode", SqlDbType.NVarChar, 100),
					new SqlParameter("@IsPlanItem", SqlDbType.Bit),
					new SqlParameter("@PlanID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@OrgID", SqlDbType.Int),
					new SqlParameter("@TrainingLevelID", SqlDbType.Int),
					new SqlParameter("@DutyDeptID", SqlDbType.Int),
					new SqlParameter("@ItemCode", SqlDbType.NVarChar, 100),
					new SqlParameter("@ItemName", SqlDbType.NVarChar, 200),
					new SqlParameter("@ItemStatus", SqlDbType.Int),
					new SqlParameter("@ItemBeginTime", SqlDbType.DateTime),
					new SqlParameter("@ItemEndTime", SqlDbType.DateTime),
					new SqlParameter("@BudgetFee", SqlDbType.Decimal, 9, ParameterDirection.Input, true, 15, 2, String.Empty, DataRowVersion.Default, null),
					new SqlParameter("@DutyUser", SqlDbType.NVarChar, 128),
					new SqlParameter("@Mobile", SqlDbType.NVarChar, 100),
					new SqlParameter("@EMAIL", SqlDbType.NVarChar, 256),
					new SqlParameter("@ItemTarget", SqlDbType.NVarChar, -1),
					new SqlParameter("@ItemObjectStudent", SqlDbType.NVarChar, -1),
					new SqlParameter("@Remark", SqlDbType.NVarChar, -1),
					new SqlParameter("@AuditUser", SqlDbType.NVarChar, 128),
					new SqlParameter("@AuditTime", SqlDbType.DateTime),
					new SqlParameter("@AuditOpinion", SqlDbType.NVarChar, -1),
					new SqlParameter("@IsIssue", SqlDbType.Bit),
					new SqlParameter("@IssueUser", SqlDbType.NVarChar, 128),
					new SqlParameter("@IssueTime", SqlDbType.DateTime),
					new SqlParameter("@ItemEndModeID", SqlDbType.Int),
					new SqlParameter("@SignupModeID", SqlDbType.Int),
					new SqlParameter("@ItemEndReMark", SqlDbType.NVarChar, -1),
					new SqlParameter("@IsUse", SqlDbType.Int),
					new SqlParameter("@IsOrgItem", SqlDbType.Bit),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUserID", SqlDbType.Int),
					new SqlParameter("@CreateUser", SqlDbType.NVarChar, 128),
					new SqlParameter("@ModifyTime", SqlDbType.DateTime),
					new SqlParameter("@ModifyUser", SqlDbType.NVarChar, 128),
					new SqlParameter("@DelFlag", SqlDbType.Bit),
					new SqlParameter("@SignupBeginTime", SqlDbType.DateTime),
					new SqlParameter("@SignupEndTime", SqlDbType.DateTime),
					new SqlParameter("@IsAllowSignup", SqlDbType.Bit),
					new SqlParameter("@IsIssuePoint", SqlDbType.Bit),
					new SqlParameter("@PointIssueUser", SqlDbType.NVarChar, 128),
					new SqlParameter("@PointIssueTime", SqlDbType.DateTime),
                    new SqlParameter("@ThumbnailURL",SqlDbType.NVarChar)
				};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
			}
			
			parms[0].Value = tr_Item.TrainingItemID;
			if (tr_Item.SpecialtyTypeCode!= null){ parms[1].Value = tr_Item.SpecialtyTypeCode; } else { parms[1].Value = DBNull.Value; }
			parms[2].Value = tr_Item.IsPlanItem;
			parms[3].Value = tr_Item.PlanID;
			parms[4].Value = tr_Item.OrgID;
			parms[5].Value = tr_Item.TrainingLevelID;
			parms[6].Value = tr_Item.DutyDeptID;
			if (tr_Item.ItemCode!= null){ parms[7].Value = tr_Item.ItemCode; } else { parms[7].Value = DBNull.Value; }
			if (tr_Item.ItemName!= null){ parms[8].Value = tr_Item.ItemName; } else { parms[8].Value = DBNull.Value; }
			parms[9].Value = tr_Item.ItemStatus;
			parms[10].Value = tr_Item.ItemBeginTime;
			parms[11].Value = tr_Item.ItemEndTime;
			parms[12].Value = tr_Item.BudgetFee;
			if (tr_Item.DutyUser!= null){ parms[13].Value = tr_Item.DutyUser; } else { parms[13].Value = DBNull.Value; }
			if (tr_Item.Mobile!= null){ parms[14].Value = tr_Item.Mobile; } else { parms[14].Value = DBNull.Value; }
			if (tr_Item.EMAIL!= null){ parms[15].Value = tr_Item.EMAIL; } else { parms[15].Value = DBNull.Value; }
			if (tr_Item.ItemTarget!= null){ parms[16].Value = tr_Item.ItemTarget; } else { parms[16].Value = DBNull.Value; }
			if (tr_Item.ItemObjectStudent!= null){ parms[17].Value = tr_Item.ItemObjectStudent; } else { parms[17].Value = DBNull.Value; }
			if (tr_Item.Remark!= null){ parms[18].Value = tr_Item.Remark; } else { parms[18].Value = DBNull.Value; }
			if (tr_Item.AuditUser!= null){ parms[19].Value = tr_Item.AuditUser; } else { parms[19].Value = DBNull.Value; }
			parms[20].Value = tr_Item.AuditTime;
			if (tr_Item.AuditOpinion!= null){ parms[21].Value = tr_Item.AuditOpinion; } else { parms[21].Value = DBNull.Value; }
			parms[22].Value = tr_Item.IsIssue;
			if (tr_Item.IssueUser!= null){ parms[23].Value = tr_Item.IssueUser; } else { parms[23].Value = DBNull.Value; }
			parms[24].Value = tr_Item.IssueTime;
			parms[25].Value = tr_Item.ItemEndModeID;
			parms[26].Value = tr_Item.SignupModeID;
			if (tr_Item.ItemEndReMark!= null){ parms[27].Value = tr_Item.ItemEndReMark; } else { parms[27].Value = DBNull.Value; }
			parms[28].Value = tr_Item.IsUse;
			parms[29].Value = tr_Item.IsOrgItem;
			parms[30].Value = tr_Item.CreateTime;
			parms[31].Value = tr_Item.CreateUserID;
			if (tr_Item.CreateUser!= null){ parms[32].Value = tr_Item.CreateUser; } else { parms[32].Value = DBNull.Value; }
			parms[33].Value = tr_Item.ModifyTime;
			if (tr_Item.ModifyUser!= null){ parms[34].Value = tr_Item.ModifyUser; } else { parms[34].Value = DBNull.Value; }
			parms[35].Value = tr_Item.DelFlag;
			parms[36].Value = tr_Item.SignupBeginTime;
			parms[37].Value = tr_Item.SignupEndTime;
			parms[38].Value = tr_Item.IsAllowSignup;
			parms[39].Value = tr_Item.IsIssuePoint;
			if (tr_Item.PointIssueUser!= null){ parms[40].Value = tr_Item.PointIssueUser; } else { parms[40].Value = DBNull.Value; }
			parms[41].Value = tr_Item.PointIssueTime;
            parms[42].Value = tr_Item.ThumbnailURL;
			#endregion
			SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
		}
		
		/// <summary>
		/// ���ݱ�ʶ��ȡ����
		/// </summary>
		public Tr_Item GetById(Guid trainingItemID)
		{
			Tr_Item tr_Item = null;
			
			string commandName = "dbo.Pr_Tr_Item_GetByPK";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@TrainingItemID", SqlDbType.UniqueIdentifier)
				};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
			}
			
			parms[0].Value = trainingItemID;
			
			#endregion
			using (SqlDataReader dataReader = SqlHelper.ExecuteReader(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms))
			{
				if (dataReader.Read())
				{
					tr_Item = PopulateTr_ItemFromDataReader(dataReader);
				}
			}
			
			return tr_Item;
		}				
		
		/// <summary>
		/// ���ݲ�����ȡ�����б�����ҳ��������
		/// </summary>
		public DataTable GetPagedList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
		{			
			string commandName = "dbo.Pr_Tr_Item_GetPagedList";
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
		public IList<Tr_Item> GetEntityList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
		{			
            IList<Tr_Item> list=new List<Tr_Item>();
			string commandName = "dbo.Pr_Tr_Item_GetPagedList";
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
					list.Add(PopulateTr_ItemFromDataReader(dataReader));
				}
			}			
			totalRecords = (int)parms[4].Value;
			return list;
		}
		
		/// <summary>
		/// ��DataReader�ж�ȡ���ݵ�ҵ�����
		/// </summary>
		private Tr_Item PopulateTr_ItemFromDataReader(SqlDataReader reader)
		{
			Tr_Item tr_Item = new Tr_Item();
			
			int trainingItemIDIndex = reader.GetOrdinal("TrainingItemID"); 
			if(!reader.IsDBNull(trainingItemIDIndex))
			{
				tr_Item.TrainingItemID= reader.GetGuid(trainingItemIDIndex);
			}
			
			int specialtyTypeCodeIndex = reader.GetOrdinal("SpecialtyTypeCode"); 
			if(!reader.IsDBNull(specialtyTypeCodeIndex))
			{
				tr_Item.SpecialtyTypeCode= reader.GetString(specialtyTypeCodeIndex);
			}
			
			int isPlanItemIndex = reader.GetOrdinal("IsPlanItem"); 
			if(!reader.IsDBNull(isPlanItemIndex))
			{
				tr_Item.IsPlanItem= reader.GetBoolean(isPlanItemIndex);
			}
			
			int planIDIndex = reader.GetOrdinal("PlanID"); 
			if(!reader.IsDBNull(planIDIndex))
			{
				tr_Item.PlanID= reader.GetGuid(planIDIndex);
			}
			
			int orgIDIndex = reader.GetOrdinal("OrgID"); 
			if(!reader.IsDBNull(orgIDIndex))
			{
				tr_Item.OrgID= reader.GetInt32(orgIDIndex);
			}
			
			int trainingLevelIDIndex = reader.GetOrdinal("TrainingLevelID"); 
			if(!reader.IsDBNull(trainingLevelIDIndex))
			{
				tr_Item.TrainingLevelID= reader.GetInt32(trainingLevelIDIndex);
			}
			
			int dutyDeptIDIndex = reader.GetOrdinal("DutyDeptID"); 
			if(!reader.IsDBNull(dutyDeptIDIndex))
			{
				tr_Item.DutyDeptID= reader.GetInt32(dutyDeptIDIndex);
			}
			
			int itemCodeIndex = reader.GetOrdinal("ItemCode"); 
			if(!reader.IsDBNull(itemCodeIndex))
			{
				tr_Item.ItemCode= reader.GetString(itemCodeIndex);
			}
			
			int itemNameIndex = reader.GetOrdinal("ItemName"); 
			if(!reader.IsDBNull(itemNameIndex))
			{
				tr_Item.ItemName= reader.GetString(itemNameIndex);
			}
			
			int itemStatusIndex = reader.GetOrdinal("ItemStatus"); 
			if(!reader.IsDBNull(itemStatusIndex))
			{
				tr_Item.ItemStatus= reader.GetInt32(itemStatusIndex);
			}
			
			int itemBeginTimeIndex = reader.GetOrdinal("ItemBeginTime"); 
			if(!reader.IsDBNull(itemBeginTimeIndex))
			{
				tr_Item.ItemBeginTime= reader.GetDateTime(itemBeginTimeIndex);
			}
			
			int itemEndTimeIndex = reader.GetOrdinal("ItemEndTime"); 
			if(!reader.IsDBNull(itemEndTimeIndex))
			{
				tr_Item.ItemEndTime= reader.GetDateTime(itemEndTimeIndex);
			}
			
			int budgetFeeIndex = reader.GetOrdinal("BudgetFee"); 
			if(!reader.IsDBNull(budgetFeeIndex))
			{
				tr_Item.BudgetFee= reader.GetDecimal(budgetFeeIndex);
			}
			
			int dutyUserIndex = reader.GetOrdinal("DutyUser"); 
			if(!reader.IsDBNull(dutyUserIndex))
			{
				tr_Item.DutyUser= reader.GetString(dutyUserIndex);
			}
			
			int mobileIndex = reader.GetOrdinal("Mobile"); 
			if(!reader.IsDBNull(mobileIndex))
			{
				tr_Item.Mobile= reader.GetString(mobileIndex);
			}
			
			int eMAILIndex = reader.GetOrdinal("EMAIL"); 
			if(!reader.IsDBNull(eMAILIndex))
			{
				tr_Item.EMAIL= reader.GetString(eMAILIndex);
			}
			
			int itemTargetIndex = reader.GetOrdinal("ItemTarget"); 
			if(!reader.IsDBNull(itemTargetIndex))
			{
				tr_Item.ItemTarget= reader.GetString(itemTargetIndex);
			}
			
			int itemObjectStudentIndex = reader.GetOrdinal("ItemObjectStudent"); 
			if(!reader.IsDBNull(itemObjectStudentIndex))
			{
				tr_Item.ItemObjectStudent= reader.GetString(itemObjectStudentIndex);
			}
			
			int remarkIndex = reader.GetOrdinal("Remark"); 
			if(!reader.IsDBNull(remarkIndex))
			{
				tr_Item.Remark= reader.GetString(remarkIndex);
			}
			
			int auditUserIndex = reader.GetOrdinal("AuditUser"); 
			if(!reader.IsDBNull(auditUserIndex))
			{
				tr_Item.AuditUser= reader.GetString(auditUserIndex);
			}
			
			int auditTimeIndex = reader.GetOrdinal("AuditTime"); 
			if(!reader.IsDBNull(auditTimeIndex))
			{
				tr_Item.AuditTime= reader.GetDateTime(auditTimeIndex);
			}
			
			int auditOpinionIndex = reader.GetOrdinal("AuditOpinion"); 
			if(!reader.IsDBNull(auditOpinionIndex))
			{
				tr_Item.AuditOpinion= reader.GetString(auditOpinionIndex);
			}
			
			int isIssueIndex = reader.GetOrdinal("IsIssue"); 
			if(!reader.IsDBNull(isIssueIndex))
			{
				tr_Item.IsIssue= reader.GetBoolean(isIssueIndex);
			}
			
			int issueUserIndex = reader.GetOrdinal("IssueUser"); 
			if(!reader.IsDBNull(issueUserIndex))
			{
				tr_Item.IssueUser= reader.GetString(issueUserIndex);
			}
			
			int issueTimeIndex = reader.GetOrdinal("IssueTime"); 
			if(!reader.IsDBNull(issueTimeIndex))
			{
				tr_Item.IssueTime= reader.GetDateTime(issueTimeIndex);
			}
			
			int itemEndModeIDIndex = reader.GetOrdinal("ItemEndModeID"); 
			if(!reader.IsDBNull(itemEndModeIDIndex))
			{
				tr_Item.ItemEndModeID= reader.GetInt32(itemEndModeIDIndex);
			}
			
			int signupModeIDIndex = reader.GetOrdinal("SignupModeID"); 
			if(!reader.IsDBNull(signupModeIDIndex))
			{
				tr_Item.SignupModeID= reader.GetInt32(signupModeIDIndex);
			}
			
			int itemEndReMarkIndex = reader.GetOrdinal("ItemEndReMark"); 
			if(!reader.IsDBNull(itemEndReMarkIndex))
			{
				tr_Item.ItemEndReMark= reader.GetString(itemEndReMarkIndex);
			}
			
			int isUseIndex = reader.GetOrdinal("IsUse"); 
			if(!reader.IsDBNull(isUseIndex))
			{
				tr_Item.IsUse= reader.GetInt32(isUseIndex);
			}
			
			int isOrgItemIndex = reader.GetOrdinal("IsOrgItem"); 
			if(!reader.IsDBNull(isOrgItemIndex))
			{
				tr_Item.IsOrgItem= reader.GetBoolean(isOrgItemIndex);
			}
			
			int createTimeIndex = reader.GetOrdinal("CreateTime"); 
			if(!reader.IsDBNull(createTimeIndex))
			{
				tr_Item.CreateTime= reader.GetDateTime(createTimeIndex);
			}
			
			int createUserIDIndex = reader.GetOrdinal("CreateUserID"); 
			if(!reader.IsDBNull(createUserIDIndex))
			{
				tr_Item.CreateUserID= reader.GetInt32(createUserIDIndex);
			}
			
			int createUserIndex = reader.GetOrdinal("CreateUser"); 
			if(!reader.IsDBNull(createUserIndex))
			{
				tr_Item.CreateUser= reader.GetString(createUserIndex);
			}
			
			int modifyTimeIndex = reader.GetOrdinal("ModifyTime"); 
			if(!reader.IsDBNull(modifyTimeIndex))
			{
				tr_Item.ModifyTime= reader.GetDateTime(modifyTimeIndex);
			}
			
			int modifyUserIndex = reader.GetOrdinal("ModifyUser"); 
			if(!reader.IsDBNull(modifyUserIndex))
			{
				tr_Item.ModifyUser= reader.GetString(modifyUserIndex);
			}
			
			int delFlagIndex = reader.GetOrdinal("DelFlag"); 
			if(!reader.IsDBNull(delFlagIndex))
			{
				tr_Item.DelFlag= reader.GetBoolean(delFlagIndex);
			}
			
			int signupBeginTimeIndex = reader.GetOrdinal("SignupBeginTime"); 
			if(!reader.IsDBNull(signupBeginTimeIndex))
			{
				tr_Item.SignupBeginTime= reader.GetDateTime(signupBeginTimeIndex);
			}
			
			int signupEndTimeIndex = reader.GetOrdinal("SignupEndTime"); 
			if(!reader.IsDBNull(signupEndTimeIndex))
			{
				tr_Item.SignupEndTime= reader.GetDateTime(signupEndTimeIndex);
			}
			
			int isAllowSignupIndex = reader.GetOrdinal("IsAllowSignup"); 
			if(!reader.IsDBNull(isAllowSignupIndex))
			{
				tr_Item.IsAllowSignup= reader.GetBoolean(isAllowSignupIndex);
			}
			
			int isIssuePointIndex = reader.GetOrdinal("IsIssuePoint"); 
			if(!reader.IsDBNull(isIssuePointIndex))
			{
				tr_Item.IsIssuePoint= reader.GetBoolean(isIssuePointIndex);
			}
			
			int pointIssueUserIndex = reader.GetOrdinal("PointIssueUser"); 
			if(!reader.IsDBNull(pointIssueUserIndex))
			{
				tr_Item.PointIssueUser= reader.GetString(pointIssueUserIndex);
			}
			
			int pointIssueTimeIndex = reader.GetOrdinal("PointIssueTime"); 
			if(!reader.IsDBNull(pointIssueTimeIndex))
			{
				tr_Item.PointIssueTime= reader.GetDateTime(pointIssueTimeIndex);
			}

            int payFrom = reader.GetOrdinal("PayFrom");
            if (!reader.IsDBNull(payFrom))
            {
                tr_Item.PayFrom = reader.GetInt32(payFrom);
            }
            int pointThumbnailURLIndex = reader.GetOrdinal("ThumbnailURL");
            if (!reader.IsDBNull(pointThumbnailURLIndex))
            {
                tr_Item.ThumbnailURL = reader.GetString(pointThumbnailURLIndex);
            }
			return tr_Item; 
		}
	}
}