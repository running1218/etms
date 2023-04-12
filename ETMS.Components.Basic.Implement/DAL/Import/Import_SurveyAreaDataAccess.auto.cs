//==================================================================================================
//Version 1.0, auto-generated.
//Generated By huangzf.
//Date: 2013/3/4 9:26:27.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1), a product of ZhouYonghua(Zhou_Yonghua@163.com).
//==================================================================================================

using System;
using System.Collections.Generic;
using System.Data;

using System.Data.SqlClient;
using ETMS.Utility.Data;

using ETMS.Components.QS.API.Entity;

namespace ETMS.Components.QS.Implement.DAL
{
    /// <summary>
    /// ���ݷ���
    /// </summary>
    public partial class Import_SurveyAreaDataAccess
	{
		/// <summary>
		/// ����
		/// </summary>
		public void Add(Import_SurveyArea import_SurveyArea)
		{
			string commandName = "dbo.Pr_Import_SurveyArea_Insert";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {	new SqlParameter("@DetailID", SqlDbType.Int, 4, ParameterDirection.Output, false, 0, 0, String.Empty, DataRowVersion.Default, null),
				
					new SqlParameter("@TaskID", SqlDbType.Int),
					new SqlParameter("@QueryID", SqlDbType.Int),
					new SqlParameter("@QueryAreaID", SqlDbType.Int),
					new SqlParameter("@OrgType", SqlDbType.NVarChar, 20),
					new SqlParameter("@Status", SqlDbType.SmallInt),
					new SqlParameter("@Remark", SqlDbType.NVarChar, 1000),
					new SqlParameter("@LoginName", SqlDbType.NVarChar, 100),
					new SqlParameter("@RealName", SqlDbType.NVarChar, 100),
					new SqlParameter("@WorkNo", SqlDbType.NVarChar, 100),
					new SqlParameter("@OrganizationID", SqlDbType.Int),
					new SqlParameter("@DepartmentName", SqlDbType.NVarChar, 200),
					new SqlParameter("@PostName", SqlDbType.NVarChar, 100),
					new SqlParameter("@RankName", SqlDbType.NVarChar, 100),
					new SqlParameter("@Email", SqlDbType.NVarChar, 100),
					new SqlParameter("@DisplayPath", SqlDbType.NVarChar, 400)
					};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
			}
			
			parms[1].Value = import_SurveyArea.TaskID;
			parms[2].Value = import_SurveyArea.QueryID;
			parms[3].Value = import_SurveyArea.QueryAreaID;
			if (import_SurveyArea.OrgType!= null){ parms[4].Value = import_SurveyArea.OrgType; } else { parms[4].Value = DBNull.Value; }
			parms[5].Value = import_SurveyArea.Status;
			if (import_SurveyArea.Remark!= null){ parms[6].Value = import_SurveyArea.Remark; } else { parms[6].Value = DBNull.Value; }
			if (import_SurveyArea.LoginName!= null){ parms[7].Value = import_SurveyArea.LoginName; } else { parms[7].Value = DBNull.Value; }
			if (import_SurveyArea.RealName!= null){ parms[8].Value = import_SurveyArea.RealName; } else { parms[8].Value = DBNull.Value; }
			if (import_SurveyArea.WorkNo!= null){ parms[9].Value = import_SurveyArea.WorkNo; } else { parms[9].Value = DBNull.Value; }
			parms[10].Value = import_SurveyArea.OrganizationID;
			if (import_SurveyArea.DepartmentName!= null){ parms[11].Value = import_SurveyArea.DepartmentName; } else { parms[11].Value = DBNull.Value; }
			if (import_SurveyArea.PostName!= null){ parms[12].Value = import_SurveyArea.PostName; } else { parms[12].Value = DBNull.Value; }
			if (import_SurveyArea.RankName!= null){ parms[13].Value = import_SurveyArea.RankName; } else { parms[13].Value = DBNull.Value; }
			if (import_SurveyArea.Email!= null){ parms[14].Value = import_SurveyArea.Email; } else { parms[14].Value = DBNull.Value; }
			if (import_SurveyArea.DisplayPath!= null){ parms[15].Value = import_SurveyArea.DisplayPath; } else { parms[15].Value = DBNull.Value; }
			#endregion
			SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
			
			import_SurveyArea.DetailID = (Int32)parms[0].Value;
			
		}
		
		/// <summary>
		/// ɾ��
		/// </summary>
		public void Remove(Int32 detailID)
		{
			string commandName = "dbo.Pr_Import_SurveyArea_Delete";
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
		/// ����
		/// </summary>
		public void Save(Import_SurveyArea import_SurveyArea)
		{
			string commandName = "dbo.Pr_Import_SurveyArea_Update";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@DetailID", SqlDbType.Int),
					new SqlParameter("@TaskID", SqlDbType.Int),
					new SqlParameter("@QueryID", SqlDbType.Int),
					new SqlParameter("@QueryAreaID", SqlDbType.Int),
					new SqlParameter("@OrgType", SqlDbType.NVarChar, 20),
					new SqlParameter("@Status", SqlDbType.SmallInt),
					new SqlParameter("@Remark", SqlDbType.NVarChar, 1000),
					new SqlParameter("@LoginName", SqlDbType.NVarChar, 100),
					new SqlParameter("@RealName", SqlDbType.NVarChar, 100),
					new SqlParameter("@WorkNo", SqlDbType.NVarChar, 100),
					new SqlParameter("@OrganizationID", SqlDbType.Int),
					new SqlParameter("@DepartmentName", SqlDbType.NVarChar, 200),
					new SqlParameter("@PostName", SqlDbType.NVarChar, 100),
					new SqlParameter("@RankName", SqlDbType.NVarChar, 100),
					new SqlParameter("@Email", SqlDbType.NVarChar, 100),
					new SqlParameter("@DisplayPath", SqlDbType.NVarChar, 400)
				};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
			}
			
			parms[0].Value = import_SurveyArea.DetailID;
			parms[1].Value = import_SurveyArea.TaskID;
			parms[2].Value = import_SurveyArea.QueryID;
			parms[3].Value = import_SurveyArea.QueryAreaID;
			if (import_SurveyArea.OrgType!= null){ parms[4].Value = import_SurveyArea.OrgType; } else { parms[4].Value = DBNull.Value; }
			parms[5].Value = import_SurveyArea.Status;
			if (import_SurveyArea.Remark!= null){ parms[6].Value = import_SurveyArea.Remark; } else { parms[6].Value = DBNull.Value; }
			if (import_SurveyArea.LoginName!= null){ parms[7].Value = import_SurveyArea.LoginName; } else { parms[7].Value = DBNull.Value; }
			if (import_SurveyArea.RealName!= null){ parms[8].Value = import_SurveyArea.RealName; } else { parms[8].Value = DBNull.Value; }
			if (import_SurveyArea.WorkNo!= null){ parms[9].Value = import_SurveyArea.WorkNo; } else { parms[9].Value = DBNull.Value; }
			parms[10].Value = import_SurveyArea.OrganizationID;
			if (import_SurveyArea.DepartmentName!= null){ parms[11].Value = import_SurveyArea.DepartmentName; } else { parms[11].Value = DBNull.Value; }
			if (import_SurveyArea.PostName!= null){ parms[12].Value = import_SurveyArea.PostName; } else { parms[12].Value = DBNull.Value; }
			if (import_SurveyArea.RankName!= null){ parms[13].Value = import_SurveyArea.RankName; } else { parms[13].Value = DBNull.Value; }
			if (import_SurveyArea.Email!= null){ parms[14].Value = import_SurveyArea.Email; } else { parms[14].Value = DBNull.Value; }
			if (import_SurveyArea.DisplayPath!= null){ parms[15].Value = import_SurveyArea.DisplayPath; } else { parms[15].Value = DBNull.Value; }
			#endregion
			SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
		}
		
		/// <summary>
		/// ���ݱ�ʶ��ȡ����
		/// </summary>
		public Import_SurveyArea GetById(Int32 detailID)
		{
			Import_SurveyArea import_SurveyArea = null;
			
			string commandName = "dbo.Pr_Import_SurveyArea_GetByPK";
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
					import_SurveyArea = PopulateImport_SurveyAreaFromDataReader(dataReader);
				}
			}
			
			return import_SurveyArea;
		}				
		
		/// <summary>
		/// ���ݲ�����ȡ�����б�����ҳ��������
		/// </summary>
		public DataTable GetPagedList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
		{			
			string commandName = "dbo.Pr_Import_SurveyArea_GetPagedList";
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
		public IList<Import_SurveyArea> GetEntityList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
		{			
            IList<Import_SurveyArea> list=new List<Import_SurveyArea>();
			string commandName = "dbo.Pr_Import_SurveyArea_GetPagedList";
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
					list.Add(PopulateImport_SurveyAreaFromDataReader(dataReader));
				}
			}			
			totalRecords = (int)parms[4].Value;
			return list;
		}
		
		/// <summary>
		/// ��DataReader�ж�ȡ���ݵ�ҵ�����
		/// </summary>
		private Import_SurveyArea PopulateImport_SurveyAreaFromDataReader(SqlDataReader reader)
		{
			Import_SurveyArea import_SurveyArea = new Import_SurveyArea();
			
			int detailIDIndex = reader.GetOrdinal("DetailID"); 
			if(!reader.IsDBNull(detailIDIndex))
			{
				import_SurveyArea.DetailID= reader.GetInt32(detailIDIndex);
			}
			
			int taskIDIndex = reader.GetOrdinal("TaskID"); 
			if(!reader.IsDBNull(taskIDIndex))
			{
				import_SurveyArea.TaskID= reader.GetInt32(taskIDIndex);
			}
			
			int queryIDIndex = reader.GetOrdinal("QueryID"); 
			if(!reader.IsDBNull(queryIDIndex))
			{
				import_SurveyArea.QueryID= reader.GetInt32(queryIDIndex);
			}
			
			int queryAreaIDIndex = reader.GetOrdinal("QueryAreaID"); 
			if(!reader.IsDBNull(queryAreaIDIndex))
			{
				import_SurveyArea.QueryAreaID= reader.GetInt32(queryAreaIDIndex);
			}
			
			int orgTypeIndex = reader.GetOrdinal("OrgType"); 
			if(!reader.IsDBNull(orgTypeIndex))
			{
				import_SurveyArea.OrgType= reader.GetString(orgTypeIndex);
			}
			
			int statusIndex = reader.GetOrdinal("Status"); 
			if(!reader.IsDBNull(statusIndex))
			{
				import_SurveyArea.Status= reader.GetInt16(statusIndex);
			}
			
			int remarkIndex = reader.GetOrdinal("Remark"); 
			if(!reader.IsDBNull(remarkIndex))
			{
				import_SurveyArea.Remark= reader.GetString(remarkIndex);
			}
			
			int loginNameIndex = reader.GetOrdinal("LoginName"); 
			if(!reader.IsDBNull(loginNameIndex))
			{
				import_SurveyArea.LoginName= reader.GetString(loginNameIndex);
			}
			
			int realNameIndex = reader.GetOrdinal("RealName"); 
			if(!reader.IsDBNull(realNameIndex))
			{
				import_SurveyArea.RealName= reader.GetString(realNameIndex);
			}
			
			int workNoIndex = reader.GetOrdinal("WorkNo"); 
			if(!reader.IsDBNull(workNoIndex))
			{
				import_SurveyArea.WorkNo= reader.GetString(workNoIndex);
			}
			
			int organizationIDIndex = reader.GetOrdinal("OrganizationID"); 
			if(!reader.IsDBNull(organizationIDIndex))
			{
				import_SurveyArea.OrganizationID= reader.GetInt32(organizationIDIndex);
			}
			
			int departmentNameIndex = reader.GetOrdinal("DepartmentName"); 
			if(!reader.IsDBNull(departmentNameIndex))
			{
				import_SurveyArea.DepartmentName= reader.GetString(departmentNameIndex);
			}
			
			int postNameIndex = reader.GetOrdinal("PostName"); 
			if(!reader.IsDBNull(postNameIndex))
			{
				import_SurveyArea.PostName= reader.GetString(postNameIndex);
			}
			
			int rankNameIndex = reader.GetOrdinal("RankName"); 
			if(!reader.IsDBNull(rankNameIndex))
			{
				import_SurveyArea.RankName= reader.GetString(rankNameIndex);
			}
			
			int emailIndex = reader.GetOrdinal("Email"); 
			if(!reader.IsDBNull(emailIndex))
			{
				import_SurveyArea.Email= reader.GetString(emailIndex);
			}
			
			int displayPathIndex = reader.GetOrdinal("DisplayPath"); 
			if(!reader.IsDBNull(displayPathIndex))
			{
				import_SurveyArea.DisplayPath= reader.GetString(displayPathIndex);
			}
			
			return import_SurveyArea; 
		}
	}
}