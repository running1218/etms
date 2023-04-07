//==================================================================================================
//Version 1.0, auto-generated.
//Generated By xueyb.
//Date: 2012-4-18 11:41:21.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1), a product of ZhouYonghua(Zhou_Yonghua@163.com).
//==================================================================================================

using System;
using System.Collections.Generic;
using System.Data;

using System.Data.SqlClient;
using ETMS.Utility.Data;

using ETMS.Components.Evaluation.API.Entity;

namespace ETMS.Components.Evaluation.Implement.DAL
{
    /// <summary>
    /// 评价项表数据访问
    /// </summary>
    public partial class Evaluation_ItemDataAccess
	{
		/// <summary>
		/// 增加
		/// </summary>
		public void Add(Evaluation_Item evaluation_Item)
		{
			string commandName = "dbo.Pr_Evaluation_Item_Insert";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@ItemID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@PlateID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@ItemName", SqlDbType.NVarChar, 200),
					new SqlParameter("@EvaluationLevel", SqlDbType.Int),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUserID", SqlDbType.Int),
					new SqlParameter("@CreateUser", SqlDbType.NVarChar, 128),
					new SqlParameter("@ModifyTime", SqlDbType.DateTime),
					new SqlParameter("@ModifyUser", SqlDbType.NVarChar, 128)
					};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
			}
			
			parms[0].Value = evaluation_Item.ItemID;
			parms[1].Value = evaluation_Item.PlateID;
			if (evaluation_Item.ItemName!= null){ parms[2].Value = evaluation_Item.ItemName; } else { parms[2].Value = DBNull.Value; }
			parms[3].Value = evaluation_Item.EvaluationLevel;
			parms[4].Value = evaluation_Item.CreateTime;
			parms[5].Value = evaluation_Item.CreateUserID;
			if (evaluation_Item.CreateUser!= null){ parms[6].Value = evaluation_Item.CreateUser; } else { parms[6].Value = DBNull.Value; }
			parms[7].Value = evaluation_Item.ModifyTime;
			if (evaluation_Item.ModifyUser!= null){ parms[8].Value = evaluation_Item.ModifyUser; } else { parms[8].Value = DBNull.Value; }
			#endregion
			SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
			
		}
		
		/// <summary>
		/// 删除
		/// </summary>
		public void Remove(Guid itemID)
		{
			string commandName = "dbo.Pr_Evaluation_Item_Delete";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@ItemID", SqlDbType.UniqueIdentifier)
				};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
			}
			
			parms[0].Value = itemID;
			
			#endregion
			SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
		}
		
		/// <summary>
		/// 保存
		/// </summary>
		public void Save(Evaluation_Item evaluation_Item)
		{
			string commandName = "dbo.Pr_Evaluation_Item_Update";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@ItemID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@PlateID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@ItemName", SqlDbType.NVarChar, 200),
					new SqlParameter("@EvaluationLevel", SqlDbType.Int),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUserID", SqlDbType.Int),
					new SqlParameter("@CreateUser", SqlDbType.NVarChar, 128),
					new SqlParameter("@ModifyTime", SqlDbType.DateTime),
					new SqlParameter("@ModifyUser", SqlDbType.NVarChar, 128)
				};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
			}
			
			parms[0].Value = evaluation_Item.ItemID;
			parms[1].Value = evaluation_Item.PlateID;
			if (evaluation_Item.ItemName!= null){ parms[2].Value = evaluation_Item.ItemName; } else { parms[2].Value = DBNull.Value; }
			parms[3].Value = evaluation_Item.EvaluationLevel;
			parms[4].Value = evaluation_Item.CreateTime;
			parms[5].Value = evaluation_Item.CreateUserID;
			if (evaluation_Item.CreateUser!= null){ parms[6].Value = evaluation_Item.CreateUser; } else { parms[6].Value = DBNull.Value; }
			parms[7].Value = evaluation_Item.ModifyTime;
			if (evaluation_Item.ModifyUser!= null){ parms[8].Value = evaluation_Item.ModifyUser; } else { parms[8].Value = DBNull.Value; }
			#endregion
			SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
		}
		
		/// <summary>
		/// 根据标识获取对象
		/// </summary>
		public Evaluation_Item GetById(Guid itemID)
		{
			Evaluation_Item evaluation_Item = null;
			
			string commandName = "dbo.Pr_Evaluation_Item_GetByPK";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@ItemID", SqlDbType.UniqueIdentifier)
				};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
			}
			
			parms[0].Value = itemID;
			
			#endregion
			using (SqlDataReader dataReader = SqlHelper.ExecuteReader(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms))
			{
				if (dataReader.Read())
				{
					evaluation_Item = PopulateEvaluation_ItemFromDataReader(dataReader);
				}
			}
			
			return evaluation_Item;
		}				
		
		/// <summary>
		/// 根据参数获取对象列表（分页，可排序）
		/// </summary>
		public DataTable GetPagedList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
		{			
			string commandName = "dbo.Pr_Evaluation_Item_GetPagedList";
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
		public IList<Evaluation_Item> GetEntityList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
		{			
            IList<Evaluation_Item> list=new List<Evaluation_Item>();
			string commandName = "dbo.Pr_Evaluation_Item_GetPagedList";
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
					list.Add(PopulateEvaluation_ItemFromDataReader(dataReader));
				}
			}			
			totalRecords = (int)parms[4].Value;
			return list;
		}
		
		/// <summary>
		/// 从DataReader中读取数据到业务对象
		/// </summary>
		private Evaluation_Item PopulateEvaluation_ItemFromDataReader(SqlDataReader reader)
		{
			Evaluation_Item evaluation_Item = new Evaluation_Item();
			
			int itemIDIndex = reader.GetOrdinal("ItemID"); 
			if(!reader.IsDBNull(itemIDIndex))
			{
				evaluation_Item.ItemID= reader.GetGuid(itemIDIndex);
			}
			
			int plateIDIndex = reader.GetOrdinal("PlateID"); 
			if(!reader.IsDBNull(plateIDIndex))
			{
				evaluation_Item.PlateID= reader.GetGuid(plateIDIndex);
			}
			
			int itemNameIndex = reader.GetOrdinal("ItemName"); 
			if(!reader.IsDBNull(itemNameIndex))
			{
				evaluation_Item.ItemName= reader.GetString(itemNameIndex);
			}
			
			int evaluationLevelIndex = reader.GetOrdinal("EvaluationLevel"); 
			if(!reader.IsDBNull(evaluationLevelIndex))
			{
				evaluation_Item.EvaluationLevel= reader.GetInt32(evaluationLevelIndex);
			}
			
			int createTimeIndex = reader.GetOrdinal("CreateTime"); 
			if(!reader.IsDBNull(createTimeIndex))
			{
				evaluation_Item.CreateTime= reader.GetDateTime(createTimeIndex);
			}
			
			int createUserIDIndex = reader.GetOrdinal("CreateUserID"); 
			if(!reader.IsDBNull(createUserIDIndex))
			{
				evaluation_Item.CreateUserID= reader.GetInt32(createUserIDIndex);
			}
			
			int createUserIndex = reader.GetOrdinal("CreateUser"); 
			if(!reader.IsDBNull(createUserIndex))
			{
				evaluation_Item.CreateUser= reader.GetString(createUserIndex);
			}
			
			int modifyTimeIndex = reader.GetOrdinal("ModifyTime"); 
			if(!reader.IsDBNull(modifyTimeIndex))
			{
				evaluation_Item.ModifyTime= reader.GetDateTime(modifyTimeIndex);
			}
			
			int modifyUserIndex = reader.GetOrdinal("ModifyUser"); 
			if(!reader.IsDBNull(modifyUserIndex))
			{
				evaluation_Item.ModifyUser= reader.GetString(modifyUserIndex);
			}
			
			return evaluation_Item; 
		}
	}
}
