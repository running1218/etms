//==================================================================================================
//Version 1.0, auto-generated.
//Generated By huangzhf.
//Date: 2012-5-19 21:15:42.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1), a product of ZhouYonghua(Zhou_Yonghua@163.com).
//==================================================================================================

using System;
using System.Collections.Generic;
using System.Data;

using System.Data.SqlClient;
using ETMS.Utility.Data;

using ETMS.Components.Basic.API.Entity.Import;

namespace ETMS.Components.Basic.Implement.DAL.Import
{
    /// <summary>
    /// 项目学员导入明细表数据访问
    /// </summary>
    public partial class Import_StudentSignupDataAccess
	{
		/// <summary>
		/// 增加
		/// </summary>
		public void Add(Import_StudentSignup import_StudentSignup)
		{
			string commandName = "dbo.Pr_Import_StudentSignup_Insert";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {	new SqlParameter("@DetailID", SqlDbType.Int, 4, ParameterDirection.Output, false, 0, 0, String.Empty, DataRowVersion.Default, null),
				
					new SqlParameter("@TaskID", SqlDbType.Int),
					new SqlParameter("@Status", SqlDbType.SmallInt),
					new SqlParameter("@Remark", SqlDbType.NVarChar, 1000),
					new SqlParameter("@ItemCode", SqlDbType.NVarChar, 100),
					new SqlParameter("@ItemName", SqlDbType.NVarChar, 200),
					new SqlParameter("@TrainingItemID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@OrgID", SqlDbType.Int),
					new SqlParameter("@UserID", SqlDbType.Int),
					new SqlParameter("@LoginName", SqlDbType.NVarChar, 100),
					new SqlParameter("@RealName", SqlDbType.NVarChar, 100),
					new SqlParameter("@DepartmentName", SqlDbType.NVarChar, 100),
					new SqlParameter("@RankName", SqlDbType.NVarChar, 100),
					new SqlParameter("@PostName", SqlDbType.NVarChar, 100)
					};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
			}
			
			parms[1].Value = import_StudentSignup.TaskID;
			parms[2].Value = import_StudentSignup.Status;
			if (import_StudentSignup.Remark!= null){ parms[3].Value = import_StudentSignup.Remark; } else { parms[3].Value = DBNull.Value; }
			if (import_StudentSignup.ItemCode!= null){ parms[4].Value = import_StudentSignup.ItemCode; } else { parms[4].Value = DBNull.Value; }
			if (import_StudentSignup.ItemName!= null){ parms[5].Value = import_StudentSignup.ItemName; } else { parms[5].Value = DBNull.Value; }
			parms[6].Value = import_StudentSignup.TrainingItemID;
			parms[7].Value = import_StudentSignup.OrgID;
			parms[8].Value = import_StudentSignup.UserID;
			if (import_StudentSignup.LoginName!= null){ parms[9].Value = import_StudentSignup.LoginName; } else { parms[9].Value = DBNull.Value; }
			if (import_StudentSignup.RealName!= null){ parms[10].Value = import_StudentSignup.RealName; } else { parms[10].Value = DBNull.Value; }
			if (import_StudentSignup.DepartmentName!= null){ parms[11].Value = import_StudentSignup.DepartmentName; } else { parms[11].Value = DBNull.Value; }
			if (import_StudentSignup.RankName!= null){ parms[12].Value = import_StudentSignup.RankName; } else { parms[12].Value = DBNull.Value; }
			if (import_StudentSignup.PostName!= null){ parms[13].Value = import_StudentSignup.PostName; } else { parms[13].Value = DBNull.Value; }
			#endregion
			SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
			
			import_StudentSignup.DetailID = (Int32)parms[0].Value;
			
		}
		
		/// <summary>
		/// 删除
		/// </summary>
		public void Remove(Int32 detailID)
		{
			string commandName = "dbo.Pr_Import_StudentSignup_Delete";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@DetailID", SqlDbType.Int)
				};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
			}
			
			parms[0].Value = detailID;
			
			#endregion
			SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
		}
		
		/// <summary>
		/// 保存
		/// </summary>
		public void Save(Import_StudentSignup import_StudentSignup)
		{
			string commandName = "dbo.Pr_Import_StudentSignup_Update";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@DetailID", SqlDbType.Int),
					new SqlParameter("@TaskID", SqlDbType.Int),
					new SqlParameter("@Status", SqlDbType.SmallInt),
					new SqlParameter("@Remark", SqlDbType.NVarChar, 1000),
					new SqlParameter("@ItemCode", SqlDbType.NVarChar, 100),
					new SqlParameter("@ItemName", SqlDbType.NVarChar, 200),
					new SqlParameter("@TrainingItemID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@OrgID", SqlDbType.Int),
					new SqlParameter("@UserID", SqlDbType.Int),
					new SqlParameter("@LoginName", SqlDbType.NVarChar, 100),
					new SqlParameter("@RealName", SqlDbType.NVarChar, 100),
					new SqlParameter("@DepartmentName", SqlDbType.NVarChar, 100),
					new SqlParameter("@RankName", SqlDbType.NVarChar, 100),
					new SqlParameter("@PostName", SqlDbType.NVarChar, 100)
				};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
			}
			
			parms[0].Value = import_StudentSignup.DetailID;
			parms[1].Value = import_StudentSignup.TaskID;
			parms[2].Value = import_StudentSignup.Status;
			if (import_StudentSignup.Remark!= null){ parms[3].Value = import_StudentSignup.Remark; } else { parms[3].Value = DBNull.Value; }
			if (import_StudentSignup.ItemCode!= null){ parms[4].Value = import_StudentSignup.ItemCode; } else { parms[4].Value = DBNull.Value; }
			if (import_StudentSignup.ItemName!= null){ parms[5].Value = import_StudentSignup.ItemName; } else { parms[5].Value = DBNull.Value; }
			parms[6].Value = import_StudentSignup.TrainingItemID;
			parms[7].Value = import_StudentSignup.OrgID;
			parms[8].Value = import_StudentSignup.UserID;
			if (import_StudentSignup.LoginName!= null){ parms[9].Value = import_StudentSignup.LoginName; } else { parms[9].Value = DBNull.Value; }
			if (import_StudentSignup.RealName!= null){ parms[10].Value = import_StudentSignup.RealName; } else { parms[10].Value = DBNull.Value; }
			if (import_StudentSignup.DepartmentName!= null){ parms[11].Value = import_StudentSignup.DepartmentName; } else { parms[11].Value = DBNull.Value; }
			if (import_StudentSignup.RankName!= null){ parms[12].Value = import_StudentSignup.RankName; } else { parms[12].Value = DBNull.Value; }
			if (import_StudentSignup.PostName!= null){ parms[13].Value = import_StudentSignup.PostName; } else { parms[13].Value = DBNull.Value; }
			#endregion
			SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
		}
		
		/// <summary>
		/// 根据标识获取对象
		/// </summary>
		public Import_StudentSignup GetById(Int32 detailID)
		{
			Import_StudentSignup import_StudentSignup = null;
			
			string commandName = "dbo.Pr_Import_StudentSignup_GetByPK";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@DetailID", SqlDbType.Int)
				};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
			}
			
			parms[0].Value = detailID;
			
			#endregion
			using (SqlDataReader dataReader = SqlHelper.ExecuteReader(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms))
			{
				if (dataReader.Read())
				{
					import_StudentSignup = PopulateImport_StudentSignupFromDataReader(dataReader);
				}
			}
			
			return import_StudentSignup;
		}				
		
		/// <summary>
		/// 根据参数获取对象列表（分页，可排序）
		/// </summary>
		public DataTable GetPagedList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
		{			
			string commandName = "dbo.Pr_Import_StudentSignup_GetPagedList";
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
		public IList<Import_StudentSignup> GetEntityList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
		{			
            IList<Import_StudentSignup> list=new List<Import_StudentSignup>();
			string commandName = "dbo.Pr_Import_StudentSignup_GetPagedList";
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
					list.Add(PopulateImport_StudentSignupFromDataReader(dataReader));
				}
			}			
			totalRecords = (int)parms[4].Value;
			return list;
		}
		
		/// <summary>
		/// 从DataReader中读取数据到业务对象
		/// </summary>
		private Import_StudentSignup PopulateImport_StudentSignupFromDataReader(SqlDataReader reader)
		{
			Import_StudentSignup import_StudentSignup = new Import_StudentSignup();
			
			int detailIDIndex = reader.GetOrdinal("DetailID"); 
			if(!reader.IsDBNull(detailIDIndex))
			{
				import_StudentSignup.DetailID= reader.GetInt32(detailIDIndex);
			}
			
			int taskIDIndex = reader.GetOrdinal("TaskID"); 
			if(!reader.IsDBNull(taskIDIndex))
			{
				import_StudentSignup.TaskID= reader.GetInt32(taskIDIndex);
			}
			
			int statusIndex = reader.GetOrdinal("Status"); 
			if(!reader.IsDBNull(statusIndex))
			{
				import_StudentSignup.Status= reader.GetInt16(statusIndex);
			}
			
			int remarkIndex = reader.GetOrdinal("Remark"); 
			if(!reader.IsDBNull(remarkIndex))
			{
				import_StudentSignup.Remark= reader.GetString(remarkIndex);
			}
			
			int itemCodeIndex = reader.GetOrdinal("ItemCode"); 
			if(!reader.IsDBNull(itemCodeIndex))
			{
				import_StudentSignup.ItemCode= reader.GetString(itemCodeIndex);
			}
			
			int itemNameIndex = reader.GetOrdinal("ItemName"); 
			if(!reader.IsDBNull(itemNameIndex))
			{
				import_StudentSignup.ItemName= reader.GetString(itemNameIndex);
			}
			
			int trainingItemIDIndex = reader.GetOrdinal("TrainingItemID"); 
			if(!reader.IsDBNull(trainingItemIDIndex))
			{
				import_StudentSignup.TrainingItemID= reader.GetGuid(trainingItemIDIndex);
			}
			
			int orgIDIndex = reader.GetOrdinal("OrgID"); 
			if(!reader.IsDBNull(orgIDIndex))
			{
				import_StudentSignup.OrgID= reader.GetInt32(orgIDIndex);
			}
			
			int userIDIndex = reader.GetOrdinal("UserID"); 
			if(!reader.IsDBNull(userIDIndex))
			{
				import_StudentSignup.UserID= reader.GetInt32(userIDIndex);
			}
			
			int loginNameIndex = reader.GetOrdinal("LoginName"); 
			if(!reader.IsDBNull(loginNameIndex))
			{
				import_StudentSignup.LoginName= reader.GetString(loginNameIndex);
			}
			
			int realNameIndex = reader.GetOrdinal("RealName"); 
			if(!reader.IsDBNull(realNameIndex))
			{
				import_StudentSignup.RealName= reader.GetString(realNameIndex);
			}
			
			int departmentNameIndex = reader.GetOrdinal("DepartmentName"); 
			if(!reader.IsDBNull(departmentNameIndex))
			{
				import_StudentSignup.DepartmentName= reader.GetString(departmentNameIndex);
			}
			
			int rankNameIndex = reader.GetOrdinal("RankName"); 
			if(!reader.IsDBNull(rankNameIndex))
			{
				import_StudentSignup.RankName= reader.GetString(rankNameIndex);
			}
			
			int postNameIndex = reader.GetOrdinal("PostName"); 
			if(!reader.IsDBNull(postNameIndex))
			{
				import_StudentSignup.PostName= reader.GetString(postNameIndex);
			}
			
			return import_StudentSignup; 
		}
	}
}
