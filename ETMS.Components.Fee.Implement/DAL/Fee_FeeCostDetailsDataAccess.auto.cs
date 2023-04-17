//==================================================================================================
//Version 1.0, auto-generated.
//Generated By running1218
//Date: 2012-4-25 16:15:18.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1)
//==================================================================================================

using System;
using System.Collections.Generic;
using System.Data;

using System.Data.SqlClient;
using ETMS.Utility.Data;

using ETMS.Components.Fee.API.Entity;

namespace ETMS.Components.Fee.Implement.DAL
{
    /// <summary>
    /// 培训项目费用流水表数据访问
    /// </summary>
    public partial class Fee_FeeCostDetailsDataAccess
	{
		/// <summary>
		/// 增加
		/// </summary>
		public void Add(Fee_FeeCostDetails fee_FeeCostDetails)
		{
			string commandName = "dbo.Pr_Fee_FeeCostDetails_Insert";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@FeeCostDetailID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@TrainingItemID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@FeeCostDetailNo", SqlDbType.NVarChar, 100),
					new SqlParameter("@FeeCostDetailName", SqlDbType.NVarChar, 200),
					new SqlParameter("@CostDate", SqlDbType.DateTime),
					new SqlParameter("@Amount", SqlDbType.Decimal),
					new SqlParameter("@Purpose", SqlDbType.NVarChar, 200),
					new SqlParameter("@PRNo", SqlDbType.NVarChar, 200),
					new SqlParameter("@IsGetInvoice", SqlDbType.Bit),
					new SqlParameter("@ReimbursementDate", SqlDbType.DateTime),
					new SqlParameter("@Handler", SqlDbType.NVarChar, 128),
					new SqlParameter("@Remark", SqlDbType.NVarChar, -1),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUserID", SqlDbType.Int),
					new SqlParameter("@CreateUser", SqlDbType.NVarChar, 128),
					new SqlParameter("@ModifyTime", SqlDbType.DateTime),
					new SqlParameter("@ModifyUser", SqlDbType.NVarChar, 128),
					new SqlParameter("@DelFlag", SqlDbType.Bit)
					};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
			}
			
			parms[0].Value = fee_FeeCostDetails.FeeCostDetailID;
			parms[1].Value = fee_FeeCostDetails.TrainingItemID;
			if (fee_FeeCostDetails.FeeCostDetailNo!= null){ parms[2].Value = fee_FeeCostDetails.FeeCostDetailNo; } else { parms[2].Value = DBNull.Value; }
			if (fee_FeeCostDetails.FeeCostDetailName!= null){ parms[3].Value = fee_FeeCostDetails.FeeCostDetailName; } else { parms[3].Value = DBNull.Value; }
			parms[4].Value = fee_FeeCostDetails.CostDate;
			parms[5].Value = fee_FeeCostDetails.Amount;
			if (fee_FeeCostDetails.Purpose!= null){ parms[6].Value = fee_FeeCostDetails.Purpose; } else { parms[6].Value = DBNull.Value; }
			if (fee_FeeCostDetails.PRNo!= null){ parms[7].Value = fee_FeeCostDetails.PRNo; } else { parms[7].Value = DBNull.Value; }
			parms[8].Value = fee_FeeCostDetails.IsGetInvoice;
			parms[9].Value = fee_FeeCostDetails.ReimbursementDate;
			if (fee_FeeCostDetails.Handler!= null){ parms[10].Value = fee_FeeCostDetails.Handler; } else { parms[10].Value = DBNull.Value; }
			if (fee_FeeCostDetails.Remark!= null){ parms[11].Value = fee_FeeCostDetails.Remark; } else { parms[11].Value = DBNull.Value; }
			parms[12].Value = fee_FeeCostDetails.CreateTime;
			parms[13].Value = fee_FeeCostDetails.CreateUserID;
			if (fee_FeeCostDetails.CreateUser!= null){ parms[14].Value = fee_FeeCostDetails.CreateUser; } else { parms[14].Value = DBNull.Value; }
			parms[15].Value = fee_FeeCostDetails.ModifyTime;
			if (fee_FeeCostDetails.ModifyUser!= null){ parms[16].Value = fee_FeeCostDetails.ModifyUser; } else { parms[16].Value = DBNull.Value; }
			parms[17].Value = fee_FeeCostDetails.DelFlag;
			#endregion
			SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
			
		}
		
		/// <summary>
		/// 删除
		/// </summary>
		public void Remove(Guid feeCostDetailID)
		{
			string commandName = "dbo.Pr_Fee_FeeCostDetails_Delete";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@FeeCostDetailID", SqlDbType.UniqueIdentifier)
				};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
			}
			
			parms[0].Value = feeCostDetailID;
			
			#endregion
			SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
		}
		
		/// <summary>
		/// 保存
		/// </summary>
		public void Save(Fee_FeeCostDetails fee_FeeCostDetails)
		{
			string commandName = "dbo.Pr_Fee_FeeCostDetails_Update";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@FeeCostDetailID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@TrainingItemID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@FeeCostDetailNo", SqlDbType.NVarChar, 100),
					new SqlParameter("@FeeCostDetailName", SqlDbType.NVarChar, 200),
					new SqlParameter("@CostDate", SqlDbType.DateTime),
					new SqlParameter("@Amount", SqlDbType.Decimal),
					new SqlParameter("@Purpose", SqlDbType.NVarChar, 200),
					new SqlParameter("@PRNo", SqlDbType.NVarChar, 200),
					new SqlParameter("@IsGetInvoice", SqlDbType.Bit),
					new SqlParameter("@ReimbursementDate", SqlDbType.DateTime),
					new SqlParameter("@Handler", SqlDbType.NVarChar, 128),
					new SqlParameter("@Remark", SqlDbType.NVarChar, -1),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUserID", SqlDbType.Int),
					new SqlParameter("@CreateUser", SqlDbType.NVarChar, 128),
					new SqlParameter("@ModifyTime", SqlDbType.DateTime),
					new SqlParameter("@ModifyUser", SqlDbType.NVarChar, 128),
					new SqlParameter("@DelFlag", SqlDbType.Bit)
				};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
			}
			
			parms[0].Value = fee_FeeCostDetails.FeeCostDetailID;
			parms[1].Value = fee_FeeCostDetails.TrainingItemID;
			if (fee_FeeCostDetails.FeeCostDetailNo!= null){ parms[2].Value = fee_FeeCostDetails.FeeCostDetailNo; } else { parms[2].Value = DBNull.Value; }
			if (fee_FeeCostDetails.FeeCostDetailName!= null){ parms[3].Value = fee_FeeCostDetails.FeeCostDetailName; } else { parms[3].Value = DBNull.Value; }
			parms[4].Value = fee_FeeCostDetails.CostDate;
			parms[5].Value = fee_FeeCostDetails.Amount;
			if (fee_FeeCostDetails.Purpose!= null){ parms[6].Value = fee_FeeCostDetails.Purpose; } else { parms[6].Value = DBNull.Value; }
			if (fee_FeeCostDetails.PRNo!= null){ parms[7].Value = fee_FeeCostDetails.PRNo; } else { parms[7].Value = DBNull.Value; }
			parms[8].Value = fee_FeeCostDetails.IsGetInvoice;
			parms[9].Value = fee_FeeCostDetails.ReimbursementDate;
			if (fee_FeeCostDetails.Handler!= null){ parms[10].Value = fee_FeeCostDetails.Handler; } else { parms[10].Value = DBNull.Value; }
			if (fee_FeeCostDetails.Remark!= null){ parms[11].Value = fee_FeeCostDetails.Remark; } else { parms[11].Value = DBNull.Value; }
			parms[12].Value = fee_FeeCostDetails.CreateTime;
			parms[13].Value = fee_FeeCostDetails.CreateUserID;
			if (fee_FeeCostDetails.CreateUser!= null){ parms[14].Value = fee_FeeCostDetails.CreateUser; } else { parms[14].Value = DBNull.Value; }
			parms[15].Value = fee_FeeCostDetails.ModifyTime;
			if (fee_FeeCostDetails.ModifyUser!= null){ parms[16].Value = fee_FeeCostDetails.ModifyUser; } else { parms[16].Value = DBNull.Value; }
			parms[17].Value = fee_FeeCostDetails.DelFlag;
			#endregion
			SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
		}
		
		/// <summary>
		/// 根据标识获取对象
		/// </summary>
		public Fee_FeeCostDetails GetById(Guid feeCostDetailID)
		{
			Fee_FeeCostDetails fee_FeeCostDetails = null;
			
			string commandName = "dbo.Pr_Fee_FeeCostDetails_GetByPK";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@FeeCostDetailID", SqlDbType.UniqueIdentifier)
				};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
			}
			
			parms[0].Value = feeCostDetailID;
			
			#endregion
			using (SqlDataReader dataReader = SqlHelper.ExecuteReader(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms))
			{
				if (dataReader.Read())
				{
					fee_FeeCostDetails = PopulateFee_FeeCostDetailsFromDataReader(dataReader);
				}
			}
			
			return fee_FeeCostDetails;
		}				
		
		/// <summary>
		/// 根据参数获取对象列表（分页，可排序）
		/// </summary>
		public DataTable GetPagedList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
		{			
			string commandName = "dbo.Pr_Fee_FeeCostDetails_GetPagedList";
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
		public IList<Fee_FeeCostDetails> GetEntityList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
		{			
            IList<Fee_FeeCostDetails> list=new List<Fee_FeeCostDetails>();
			string commandName = "dbo.Pr_Fee_FeeCostDetails_GetPagedList";
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
					list.Add(PopulateFee_FeeCostDetailsFromDataReader(dataReader));
				}
			}			
			totalRecords = (int)parms[4].Value;
			return list;
		}
		
		/// <summary>
		/// 从DataReader中读取数据到业务对象
		/// </summary>
		private Fee_FeeCostDetails PopulateFee_FeeCostDetailsFromDataReader(SqlDataReader reader)
		{
			Fee_FeeCostDetails fee_FeeCostDetails = new Fee_FeeCostDetails();
			
			int feeCostDetailIDIndex = reader.GetOrdinal("FeeCostDetailID"); 
			if(!reader.IsDBNull(feeCostDetailIDIndex))
			{
				fee_FeeCostDetails.FeeCostDetailID= reader.GetGuid(feeCostDetailIDIndex);
			}
			
			int trainingItemIDIndex = reader.GetOrdinal("TrainingItemID"); 
			if(!reader.IsDBNull(trainingItemIDIndex))
			{
				fee_FeeCostDetails.TrainingItemID= reader.GetGuid(trainingItemIDIndex);
			}
			
			int feeCostDetailNoIndex = reader.GetOrdinal("FeeCostDetailNo"); 
			if(!reader.IsDBNull(feeCostDetailNoIndex))
			{
				fee_FeeCostDetails.FeeCostDetailNo= reader.GetString(feeCostDetailNoIndex);
			}
			
			int feeCostDetailNameIndex = reader.GetOrdinal("FeeCostDetailName"); 
			if(!reader.IsDBNull(feeCostDetailNameIndex))
			{
				fee_FeeCostDetails.FeeCostDetailName= reader.GetString(feeCostDetailNameIndex);
			}
			
			int costDateIndex = reader.GetOrdinal("CostDate"); 
			if(!reader.IsDBNull(costDateIndex))
			{
				fee_FeeCostDetails.CostDate= reader.GetDateTime(costDateIndex);
			}
			
			int amountIndex = reader.GetOrdinal("Amount"); 
			if(!reader.IsDBNull(amountIndex))
			{
				fee_FeeCostDetails.Amount= reader.GetDecimal(amountIndex);
			}
			
			int purposeIndex = reader.GetOrdinal("Purpose"); 
			if(!reader.IsDBNull(purposeIndex))
			{
				fee_FeeCostDetails.Purpose= reader.GetString(purposeIndex);
			}
			
			int pRNoIndex = reader.GetOrdinal("PRNo"); 
			if(!reader.IsDBNull(pRNoIndex))
			{
				fee_FeeCostDetails.PRNo= reader.GetString(pRNoIndex);
			}
			
			int isGetInvoiceIndex = reader.GetOrdinal("IsGetInvoice"); 
			if(!reader.IsDBNull(isGetInvoiceIndex))
			{
				fee_FeeCostDetails.IsGetInvoice= reader.GetBoolean(isGetInvoiceIndex);
			}
			
			int reimbursementDateIndex = reader.GetOrdinal("ReimbursementDate"); 
			if(!reader.IsDBNull(reimbursementDateIndex))
			{
				fee_FeeCostDetails.ReimbursementDate= reader.GetDateTime(reimbursementDateIndex);
			}
			
			int handlerIndex = reader.GetOrdinal("Handler"); 
			if(!reader.IsDBNull(handlerIndex))
			{
				fee_FeeCostDetails.Handler= reader.GetString(handlerIndex);
			}
			
			int remarkIndex = reader.GetOrdinal("Remark"); 
			if(!reader.IsDBNull(remarkIndex))
			{
				fee_FeeCostDetails.Remark= reader.GetString(remarkIndex);
			}
			
			int createTimeIndex = reader.GetOrdinal("CreateTime"); 
			if(!reader.IsDBNull(createTimeIndex))
			{
				fee_FeeCostDetails.CreateTime= reader.GetDateTime(createTimeIndex);
			}
			
			int createUserIDIndex = reader.GetOrdinal("CreateUserID"); 
			if(!reader.IsDBNull(createUserIDIndex))
			{
				fee_FeeCostDetails.CreateUserID= reader.GetInt32(createUserIDIndex);
			}
			
			int createUserIndex = reader.GetOrdinal("CreateUser"); 
			if(!reader.IsDBNull(createUserIndex))
			{
				fee_FeeCostDetails.CreateUser= reader.GetString(createUserIndex);
			}
			
			int modifyTimeIndex = reader.GetOrdinal("ModifyTime"); 
			if(!reader.IsDBNull(modifyTimeIndex))
			{
				fee_FeeCostDetails.ModifyTime= reader.GetDateTime(modifyTimeIndex);
			}
			
			int modifyUserIndex = reader.GetOrdinal("ModifyUser"); 
			if(!reader.IsDBNull(modifyUserIndex))
			{
				fee_FeeCostDetails.ModifyUser= reader.GetString(modifyUserIndex);
			}
			
			int delFlagIndex = reader.GetOrdinal("DelFlag"); 
			if(!reader.IsDBNull(delFlagIndex))
			{
				fee_FeeCostDetails.DelFlag= reader.GetBoolean(delFlagIndex);
			}
			
			return fee_FeeCostDetails; 
		}
	}
}
