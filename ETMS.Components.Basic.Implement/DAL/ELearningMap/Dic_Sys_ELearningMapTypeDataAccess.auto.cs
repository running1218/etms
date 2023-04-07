//==================================================================================================
//Version 1.0, auto-generated.
//Generated By xueyb.
//Date: 2012-3-29 22:16:00.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1), a product of ZhouYonghua(Zhou_Yonghua@163.com).
//==================================================================================================

using System;
using System.Data;

using System.Data.SqlClient;
using ETMS.Utility.Data;

using ETMS.Components.Basic.API.Entity.ELearningMap;

namespace ETMS.Components.Basic.Implement.DAL.ELearningMap
{
    /// <summary>
    /// 学习地图类型（系统字典表）数据访问
    /// </summary>
    public partial class Dic_Sys_ELearningMapTypeDataAccess
	{
		/// <summary>
		/// 增加
		/// </summary>
		public void Add(Dic_Sys_ELearningMapType dic_Sys_ELearningMapType)
		{
			string commandName = "dbo.Pr_Dic_Sys_ELearningMapType_Insert";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@ELearningMapTypeID", SqlDbType.Int),
					new SqlParameter("@ELearningMapTypeName", SqlDbType.NVarChar, 200),
					new SqlParameter("@OrderNum", SqlDbType.Int),
					new SqlParameter("@IsUse", SqlDbType.Int)
					};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
			}
			
			parms[0].Value = dic_Sys_ELearningMapType.ELearningMapTypeID;
			if (dic_Sys_ELearningMapType.ELearningMapTypeName!= null){ parms[1].Value = dic_Sys_ELearningMapType.ELearningMapTypeName; } else { parms[1].Value = DBNull.Value; }
			parms[2].Value = dic_Sys_ELearningMapType.OrderNum;
			parms[3].Value = dic_Sys_ELearningMapType.IsUse;
			#endregion
			SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
			
		}
		
		/// <summary>
		/// 删除
		/// </summary>
		public void Remove(Int32 eLearningMapTypeID)
		{
			string commandName = "dbo.Pr_Dic_Sys_ELearningMapType_Delete";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@ELearningMapTypeID", SqlDbType.Int)
				};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
			}
			
			parms[0].Value = eLearningMapTypeID;
			
			#endregion
			SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
		}
		
		/// <summary>
		/// 保存
		/// </summary>
		public void Save(Dic_Sys_ELearningMapType dic_Sys_ELearningMapType)
		{
			string commandName = "dbo.Pr_Dic_Sys_ELearningMapType_Update";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@ELearningMapTypeID", SqlDbType.Int),
					new SqlParameter("@ELearningMapTypeName", SqlDbType.NVarChar, 200),
					new SqlParameter("@OrderNum", SqlDbType.Int),
					new SqlParameter("@IsUse", SqlDbType.Int)
				};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
			}
			
			parms[0].Value = dic_Sys_ELearningMapType.ELearningMapTypeID;
			if (dic_Sys_ELearningMapType.ELearningMapTypeName!= null){ parms[1].Value = dic_Sys_ELearningMapType.ELearningMapTypeName; } else { parms[1].Value = DBNull.Value; }
			parms[2].Value = dic_Sys_ELearningMapType.OrderNum;
			parms[3].Value = dic_Sys_ELearningMapType.IsUse;
			#endregion
			SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
		}
		
		/// <summary>
		/// 根据标识获取对象
		/// </summary>
		public Dic_Sys_ELearningMapType GetById(Int32 eLearningMapTypeID)
		{
			Dic_Sys_ELearningMapType dic_Sys_ELearningMapType = null;
			
			string commandName = "dbo.Pr_Dic_Sys_ELearningMapType_GetByPK";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@ELearningMapTypeID", SqlDbType.Int)
				};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
			}
			
			parms[0].Value = eLearningMapTypeID;
			
			#endregion
			using (SqlDataReader dataReader = SqlHelper.ExecuteReader(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms))
			{
				if (dataReader.Read())
				{
					dic_Sys_ELearningMapType = PopulateDic_Sys_ELearningMapTypeFromDataReader(dataReader);
				}
			}
			
			return dic_Sys_ELearningMapType;
		}				
		
		/// <summary>
		/// 根据参数获取对象列表（分页，可排序）
		/// </summary>
		public DataTable GetPagedList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
		{			
			string commandName = "dbo.Pr_Dic_Sys_ELearningMapType_GetPagedList";
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
		private Dic_Sys_ELearningMapType PopulateDic_Sys_ELearningMapTypeFromDataReader(SqlDataReader reader)
		{
			Dic_Sys_ELearningMapType dic_Sys_ELearningMapType = new Dic_Sys_ELearningMapType();
			
			int eLearningMapTypeIDIndex = reader.GetOrdinal("ELearningMapTypeID"); 
			if(!reader.IsDBNull(eLearningMapTypeIDIndex))
			{
				dic_Sys_ELearningMapType.ELearningMapTypeID= reader.GetInt32(eLearningMapTypeIDIndex);
			}
			
			int eLearningMapTypeNameIndex = reader.GetOrdinal("ELearningMapTypeName"); 
			if(!reader.IsDBNull(eLearningMapTypeNameIndex))
			{
				dic_Sys_ELearningMapType.ELearningMapTypeName= reader.GetString(eLearningMapTypeNameIndex);
			}
			
			int orderNumIndex = reader.GetOrdinal("OrderNum"); 
			if(!reader.IsDBNull(orderNumIndex))
			{
				dic_Sys_ELearningMapType.OrderNum= reader.GetInt32(orderNumIndex);
			}
			
			int isUseIndex = reader.GetOrdinal("IsUse"); 
			if(!reader.IsDBNull(isUseIndex))
			{
				dic_Sys_ELearningMapType.IsUse= reader.GetInt32(isUseIndex);
			}
			
			return dic_Sys_ELearningMapType; 
		}
	}
}
