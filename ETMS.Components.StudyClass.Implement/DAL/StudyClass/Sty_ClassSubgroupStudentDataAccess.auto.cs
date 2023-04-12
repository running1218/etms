//==================================================================================================
//Version 1.0, auto-generated.
//Generated By xueyb.
//Date: 2012-04-18 22:30:53.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1), a product of ZhouYonghua(Zhou_Yonghua@163.com).
//==================================================================================================

using System;
using System.Collections.Generic;
using System.Data;

using System.Data.SqlClient;
using ETMS.Utility.Data;

using ETMS.Components.StudyClass.API.Entity.StudyClass;

namespace ETMS.Components.StudyClass.Implement.DAL.StudyClass
{
    /// <summary>
    /// �༶Ⱥ��ѧԱ�����ݷ���
    /// </summary>
    public partial class Sty_ClassSubgroupStudentDataAccess
	{
		/// <summary>
		/// ����
		/// </summary>
		public void Add(Sty_ClassSubgroupStudent sty_ClassSubgroupStudent)
		{
			string commandName = "dbo.Pr_Sty_ClassSubgroupStudent_Insert";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@SubgroupStudentID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@ClassSubgroupID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@ClassStudentID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@IsLeader", SqlDbType.Bit),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUserID", SqlDbType.Int),
					new SqlParameter("@CreateUser", SqlDbType.NVarChar, 128),
					new SqlParameter("@ModifyTime", SqlDbType.DateTime),
					new SqlParameter("@ModifyUser", SqlDbType.NVarChar, 128),
					new SqlParameter("@Remark", SqlDbType.NVarChar, -1)
					};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
			}
			
			parms[0].Value = sty_ClassSubgroupStudent.SubgroupStudentID;
			parms[1].Value = sty_ClassSubgroupStudent.ClassSubgroupID;
			parms[2].Value = sty_ClassSubgroupStudent.ClassStudentID;
			parms[3].Value = sty_ClassSubgroupStudent.IsLeader;
			parms[4].Value = sty_ClassSubgroupStudent.CreateTime;
			parms[5].Value = sty_ClassSubgroupStudent.CreateUserID;
			if (sty_ClassSubgroupStudent.CreateUser!= null){ parms[6].Value = sty_ClassSubgroupStudent.CreateUser; } else { parms[6].Value = DBNull.Value; }
			parms[7].Value = sty_ClassSubgroupStudent.ModifyTime;
			if (sty_ClassSubgroupStudent.ModifyUser!= null){ parms[8].Value = sty_ClassSubgroupStudent.ModifyUser; } else { parms[8].Value = DBNull.Value; }
			if (sty_ClassSubgroupStudent.Remark!= null){ parms[9].Value = sty_ClassSubgroupStudent.Remark; } else { parms[9].Value = DBNull.Value; }
			#endregion
			SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
			
		}
		
		/// <summary>
		/// ɾ��
		/// </summary>
		public void Remove(Guid subgroupStudentID)
		{
			string commandName = "dbo.Pr_Sty_ClassSubgroupStudent_Delete";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@SubgroupStudentID", SqlDbType.UniqueIdentifier)
				};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
			}
			
			parms[0].Value = subgroupStudentID;
			
			#endregion
			SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
		}
		
		/// <summary>
		/// ����
		/// </summary>
		public void Save(Sty_ClassSubgroupStudent sty_ClassSubgroupStudent)
		{
			string commandName = "dbo.Pr_Sty_ClassSubgroupStudent_Update";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@SubgroupStudentID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@ClassSubgroupID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@ClassStudentID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@IsLeader", SqlDbType.Bit),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUserID", SqlDbType.Int),
					new SqlParameter("@CreateUser", SqlDbType.NVarChar, 128),
					new SqlParameter("@ModifyTime", SqlDbType.DateTime),
					new SqlParameter("@ModifyUser", SqlDbType.NVarChar, 128),
					new SqlParameter("@Remark", SqlDbType.NVarChar, -1)
				};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
			}
			
			parms[0].Value = sty_ClassSubgroupStudent.SubgroupStudentID;
			parms[1].Value = sty_ClassSubgroupStudent.ClassSubgroupID;
			parms[2].Value = sty_ClassSubgroupStudent.ClassStudentID;
			parms[3].Value = sty_ClassSubgroupStudent.IsLeader;
			parms[4].Value = sty_ClassSubgroupStudent.CreateTime;
			parms[5].Value = sty_ClassSubgroupStudent.CreateUserID;
			if (sty_ClassSubgroupStudent.CreateUser!= null){ parms[6].Value = sty_ClassSubgroupStudent.CreateUser; } else { parms[6].Value = DBNull.Value; }
			parms[7].Value = sty_ClassSubgroupStudent.ModifyTime;
			if (sty_ClassSubgroupStudent.ModifyUser!= null){ parms[8].Value = sty_ClassSubgroupStudent.ModifyUser; } else { parms[8].Value = DBNull.Value; }
			if (sty_ClassSubgroupStudent.Remark!= null){ parms[9].Value = sty_ClassSubgroupStudent.Remark; } else { parms[9].Value = DBNull.Value; }
			#endregion
			SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
		}
		
		/// <summary>
		/// ���ݱ�ʶ��ȡ����
		/// </summary>
		public Sty_ClassSubgroupStudent GetById(Guid subgroupStudentID)
		{
			Sty_ClassSubgroupStudent sty_ClassSubgroupStudent = null;
			
			string commandName = "dbo.Pr_Sty_ClassSubgroupStudent_GetByPK";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@SubgroupStudentID", SqlDbType.UniqueIdentifier)
				};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
			}
			
			parms[0].Value = subgroupStudentID;
			
			#endregion
			using (SqlDataReader dataReader = SqlHelper.ExecuteReader(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms))
			{
				if (dataReader.Read())
				{
					sty_ClassSubgroupStudent = PopulateSty_ClassSubgroupStudentFromDataReader(dataReader);
				}
			}
			
			return sty_ClassSubgroupStudent;
		}				
		
		/// <summary>
		/// ���ݲ�����ȡ�����б�����ҳ��������
		/// </summary>
		public DataTable GetPagedList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
		{			
			string commandName = "dbo.Pr_Sty_ClassSubgroupStudent_GetPagedList";
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
		public IList<Sty_ClassSubgroupStudent> GetEntityList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
		{			
            IList<Sty_ClassSubgroupStudent> list=new List<Sty_ClassSubgroupStudent>();
			string commandName = "dbo.Pr_Sty_ClassSubgroupStudent_GetPagedList";
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
					list.Add(PopulateSty_ClassSubgroupStudentFromDataReader(dataReader));
				}
			}			
			totalRecords = (int)parms[4].Value;
			return list;
		}
		
		/// <summary>
		/// ��DataReader�ж�ȡ���ݵ�ҵ�����
		/// </summary>
		private Sty_ClassSubgroupStudent PopulateSty_ClassSubgroupStudentFromDataReader(SqlDataReader reader)
		{
			Sty_ClassSubgroupStudent sty_ClassSubgroupStudent = new Sty_ClassSubgroupStudent();
			
			int subgroupStudentIDIndex = reader.GetOrdinal("SubgroupStudentID"); 
			if(!reader.IsDBNull(subgroupStudentIDIndex))
			{
				sty_ClassSubgroupStudent.SubgroupStudentID= reader.GetGuid(subgroupStudentIDIndex);
			}
			
			int classSubgroupIDIndex = reader.GetOrdinal("ClassSubgroupID"); 
			if(!reader.IsDBNull(classSubgroupIDIndex))
			{
				sty_ClassSubgroupStudent.ClassSubgroupID= reader.GetGuid(classSubgroupIDIndex);
			}
			
			int classStudentIDIndex = reader.GetOrdinal("ClassStudentID"); 
			if(!reader.IsDBNull(classStudentIDIndex))
			{
				sty_ClassSubgroupStudent.ClassStudentID= reader.GetGuid(classStudentIDIndex);
			}
			
			int isLeaderIndex = reader.GetOrdinal("IsLeader"); 
			if(!reader.IsDBNull(isLeaderIndex))
			{
				sty_ClassSubgroupStudent.IsLeader= reader.GetBoolean(isLeaderIndex);
			}
			
			int createTimeIndex = reader.GetOrdinal("CreateTime"); 
			if(!reader.IsDBNull(createTimeIndex))
			{
				sty_ClassSubgroupStudent.CreateTime= reader.GetDateTime(createTimeIndex);
			}
			
			int createUserIDIndex = reader.GetOrdinal("CreateUserID"); 
			if(!reader.IsDBNull(createUserIDIndex))
			{
				sty_ClassSubgroupStudent.CreateUserID= reader.GetInt32(createUserIDIndex);
			}
			
			int createUserIndex = reader.GetOrdinal("CreateUser"); 
			if(!reader.IsDBNull(createUserIndex))
			{
				sty_ClassSubgroupStudent.CreateUser= reader.GetString(createUserIndex);
			}
			
			int modifyTimeIndex = reader.GetOrdinal("ModifyTime"); 
			if(!reader.IsDBNull(modifyTimeIndex))
			{
				sty_ClassSubgroupStudent.ModifyTime= reader.GetDateTime(modifyTimeIndex);
			}
			
			int modifyUserIndex = reader.GetOrdinal("ModifyUser"); 
			if(!reader.IsDBNull(modifyUserIndex))
			{
				sty_ClassSubgroupStudent.ModifyUser= reader.GetString(modifyUserIndex);
			}
			
			int remarkIndex = reader.GetOrdinal("Remark"); 
			if(!reader.IsDBNull(remarkIndex))
			{
				sty_ClassSubgroupStudent.Remark= reader.GetString(remarkIndex);
			}
			
			return sty_ClassSubgroupStudent; 
		}
	}
}