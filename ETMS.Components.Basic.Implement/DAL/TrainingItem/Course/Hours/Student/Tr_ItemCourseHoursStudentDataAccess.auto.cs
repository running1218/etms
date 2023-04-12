//==================================================================================================
//Version 1.0, auto-generated.
//Generated By huangzhf.
//Date: 2012-4-23 21:23:01.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1), a product of ZhouYonghua(Zhou_Yonghua@163.com).
//==================================================================================================

using System;
using System.Collections.Generic;
using System.Data;

using System.Data.SqlClient;
using ETMS.Utility.Data;

using ETMS.Components.Basic.API.Entity.TrainingItem.Course.Hours.Student;

namespace ETMS.Components.Basic.Implement.DAL.TrainingItem.Course.Hours.Student
{
    /// <summary>
    /// ��ѵ��Ŀ�γ̿�ʱѧԱ�����ݷ���
    /// </summary>
    public partial class Tr_ItemCourseHoursStudentDataAccess
	{
		/// <summary>
		/// ����
		/// </summary>
		public void Add(Tr_ItemCourseHoursStudent tr_ItemCourseHoursStudent)
		{
			string commandName = "dbo.Pr_Tr_ItemCourseHoursStudent_Insert";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@ItemCourseHoursStudentID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@ItemCourseHoursID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@StudentCourse", SqlDbType.UniqueIdentifier),
					new SqlParameter("@SigninTypeID", SqlDbType.Int),
					new SqlParameter("@LawlessnessID", SqlDbType.Int),
					new SqlParameter("@SigninTime", SqlDbType.DateTime),
					new SqlParameter("@SigninReason", SqlDbType.NVarChar, -1),
					new SqlParameter("@LeaveMinutes", SqlDbType.Int),
					new SqlParameter("@IsLeave", SqlDbType.Bit),
					new SqlParameter("@LeaveReason", SqlDbType.NVarChar, -1),
					new SqlParameter("@LeaveTime", SqlDbType.DateTime),
					new SqlParameter("@AuditStatus", SqlDbType.Int),
					new SqlParameter("@AuditUser", SqlDbType.NVarChar, 128),
					new SqlParameter("@AuditTime", SqlDbType.DateTime),
					new SqlParameter("@AuditOpinion", SqlDbType.NVarChar, -1),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUserID", SqlDbType.Int),
					new SqlParameter("@CreateUser", SqlDbType.NVarChar, 128),
					new SqlParameter("@ModifyTime", SqlDbType.DateTime),
					new SqlParameter("@ModifyUser", SqlDbType.NVarChar, 128),
					new SqlParameter("@Remark", SqlDbType.NVarChar, -1)
					};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
			}
			
			parms[0].Value = tr_ItemCourseHoursStudent.ItemCourseHoursStudentID;
			parms[1].Value = tr_ItemCourseHoursStudent.ItemCourseHoursID;
			parms[2].Value = tr_ItemCourseHoursStudent.StudentCourse;
			parms[3].Value = tr_ItemCourseHoursStudent.SigninTypeID;
			parms[4].Value = tr_ItemCourseHoursStudent.LawlessnessID;
			parms[5].Value = tr_ItemCourseHoursStudent.SigninTime;
			if (tr_ItemCourseHoursStudent.SigninReason!= null){ parms[6].Value = tr_ItemCourseHoursStudent.SigninReason; } else { parms[6].Value = DBNull.Value; }
			parms[7].Value = tr_ItemCourseHoursStudent.LeaveMinutes;
			parms[8].Value = tr_ItemCourseHoursStudent.IsLeave;
			if (tr_ItemCourseHoursStudent.LeaveReason!= null){ parms[9].Value = tr_ItemCourseHoursStudent.LeaveReason; } else { parms[9].Value = DBNull.Value; }
			parms[10].Value = tr_ItemCourseHoursStudent.LeaveTime;
			parms[11].Value = tr_ItemCourseHoursStudent.AuditStatus;
			if (tr_ItemCourseHoursStudent.AuditUser!= null){ parms[12].Value = tr_ItemCourseHoursStudent.AuditUser; } else { parms[12].Value = DBNull.Value; }
			parms[13].Value = tr_ItemCourseHoursStudent.AuditTime;
			if (tr_ItemCourseHoursStudent.AuditOpinion!= null){ parms[14].Value = tr_ItemCourseHoursStudent.AuditOpinion; } else { parms[14].Value = DBNull.Value; }
			parms[15].Value = tr_ItemCourseHoursStudent.CreateTime;
			parms[16].Value = tr_ItemCourseHoursStudent.CreateUserID;
			if (tr_ItemCourseHoursStudent.CreateUser!= null){ parms[17].Value = tr_ItemCourseHoursStudent.CreateUser; } else { parms[17].Value = DBNull.Value; }
			parms[18].Value = tr_ItemCourseHoursStudent.ModifyTime;
			if (tr_ItemCourseHoursStudent.ModifyUser!= null){ parms[19].Value = tr_ItemCourseHoursStudent.ModifyUser; } else { parms[19].Value = DBNull.Value; }
			if (tr_ItemCourseHoursStudent.Remark!= null){ parms[20].Value = tr_ItemCourseHoursStudent.Remark; } else { parms[20].Value = DBNull.Value; }
			#endregion
			SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
			
		}
		
		/// <summary>
		/// ɾ��
		/// </summary>
		public void Remove(Guid itemCourseHoursStudentID)
		{
			string commandName = "dbo.Pr_Tr_ItemCourseHoursStudent_Delete";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@ItemCourseHoursStudentID", SqlDbType.UniqueIdentifier)
				};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
			}
			
			parms[0].Value = itemCourseHoursStudentID;
			
			#endregion
			SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
		}
		
		/// <summary>
		/// ����
		/// </summary>
		public void Save(Tr_ItemCourseHoursStudent tr_ItemCourseHoursStudent)
		{
			string commandName = "dbo.Pr_Tr_ItemCourseHoursStudent_Update";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@ItemCourseHoursStudentID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@ItemCourseHoursID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@StudentCourse", SqlDbType.UniqueIdentifier),
					new SqlParameter("@SigninTypeID", SqlDbType.Int),
					new SqlParameter("@LawlessnessID", SqlDbType.Int),
					new SqlParameter("@SigninTime", SqlDbType.DateTime),
					new SqlParameter("@SigninReason", SqlDbType.NVarChar, -1),
					new SqlParameter("@LeaveMinutes", SqlDbType.Int),
					new SqlParameter("@IsLeave", SqlDbType.Bit),
					new SqlParameter("@LeaveReason", SqlDbType.NVarChar, -1),
					new SqlParameter("@LeaveTime", SqlDbType.DateTime),
					new SqlParameter("@AuditStatus", SqlDbType.Int),
					new SqlParameter("@AuditUser", SqlDbType.NVarChar, 128),
					new SqlParameter("@AuditTime", SqlDbType.DateTime),
					new SqlParameter("@AuditOpinion", SqlDbType.NVarChar, -1),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUserID", SqlDbType.Int),
					new SqlParameter("@CreateUser", SqlDbType.NVarChar, 128),
					new SqlParameter("@ModifyTime", SqlDbType.DateTime),
					new SqlParameter("@ModifyUser", SqlDbType.NVarChar, 128),
					new SqlParameter("@Remark", SqlDbType.NVarChar, -1)
				};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
			}
			
			parms[0].Value = tr_ItemCourseHoursStudent.ItemCourseHoursStudentID;
			parms[1].Value = tr_ItemCourseHoursStudent.ItemCourseHoursID;
			parms[2].Value = tr_ItemCourseHoursStudent.StudentCourse;
			parms[3].Value = tr_ItemCourseHoursStudent.SigninTypeID;
			parms[4].Value = tr_ItemCourseHoursStudent.LawlessnessID;
			parms[5].Value = tr_ItemCourseHoursStudent.SigninTime;
			if (tr_ItemCourseHoursStudent.SigninReason!= null){ parms[6].Value = tr_ItemCourseHoursStudent.SigninReason; } else { parms[6].Value = DBNull.Value; }
			parms[7].Value = tr_ItemCourseHoursStudent.LeaveMinutes;
			parms[8].Value = tr_ItemCourseHoursStudent.IsLeave;
			if (tr_ItemCourseHoursStudent.LeaveReason!= null){ parms[9].Value = tr_ItemCourseHoursStudent.LeaveReason; } else { parms[9].Value = DBNull.Value; }
			parms[10].Value = tr_ItemCourseHoursStudent.LeaveTime;
			parms[11].Value = tr_ItemCourseHoursStudent.AuditStatus;
			if (tr_ItemCourseHoursStudent.AuditUser!= null){ parms[12].Value = tr_ItemCourseHoursStudent.AuditUser; } else { parms[12].Value = DBNull.Value; }
			parms[13].Value = tr_ItemCourseHoursStudent.AuditTime;
			if (tr_ItemCourseHoursStudent.AuditOpinion!= null){ parms[14].Value = tr_ItemCourseHoursStudent.AuditOpinion; } else { parms[14].Value = DBNull.Value; }
			parms[15].Value = tr_ItemCourseHoursStudent.CreateTime;
			parms[16].Value = tr_ItemCourseHoursStudent.CreateUserID;
			if (tr_ItemCourseHoursStudent.CreateUser!= null){ parms[17].Value = tr_ItemCourseHoursStudent.CreateUser; } else { parms[17].Value = DBNull.Value; }
			parms[18].Value = tr_ItemCourseHoursStudent.ModifyTime;
			if (tr_ItemCourseHoursStudent.ModifyUser!= null){ parms[19].Value = tr_ItemCourseHoursStudent.ModifyUser; } else { parms[19].Value = DBNull.Value; }
			if (tr_ItemCourseHoursStudent.Remark!= null){ parms[20].Value = tr_ItemCourseHoursStudent.Remark; } else { parms[20].Value = DBNull.Value; }
			#endregion
			SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
		}
		
		/// <summary>
		/// ���ݱ�ʶ��ȡ����
		/// </summary>
		public Tr_ItemCourseHoursStudent GetById(Guid itemCourseHoursStudentID)
		{
			Tr_ItemCourseHoursStudent tr_ItemCourseHoursStudent = null;
			
			string commandName = "dbo.Pr_Tr_ItemCourseHoursStudent_GetByPK";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@ItemCourseHoursStudentID", SqlDbType.UniqueIdentifier)
				};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
			}
			
			parms[0].Value = itemCourseHoursStudentID;
			
			#endregion
			using (SqlDataReader dataReader = SqlHelper.ExecuteReader(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms))
			{
				if (dataReader.Read())
				{
					tr_ItemCourseHoursStudent = PopulateTr_ItemCourseHoursStudentFromDataReader(dataReader);
				}
			}
			
			return tr_ItemCourseHoursStudent;
		}				
		
		/// <summary>
		/// ���ݲ�����ȡ�����б�����ҳ��������
		/// </summary>
		public DataTable GetPagedList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
		{			
			string commandName = "dbo.Pr_Tr_ItemCourseHoursStudent_GetPagedList";
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
		public IList<Tr_ItemCourseHoursStudent> GetEntityList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
		{			
            IList<Tr_ItemCourseHoursStudent> list=new List<Tr_ItemCourseHoursStudent>();
			string commandName = "dbo.Pr_Tr_ItemCourseHoursStudent_GetPagedList";
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
					list.Add(PopulateTr_ItemCourseHoursStudentFromDataReader(dataReader));
				}
			}			
			totalRecords = (int)parms[4].Value;
			return list;
		}
		
		/// <summary>
		/// ��DataReader�ж�ȡ���ݵ�ҵ�����
		/// </summary>
		private Tr_ItemCourseHoursStudent PopulateTr_ItemCourseHoursStudentFromDataReader(SqlDataReader reader)
		{
			Tr_ItemCourseHoursStudent tr_ItemCourseHoursStudent = new Tr_ItemCourseHoursStudent();
			
			int itemCourseHoursStudentIDIndex = reader.GetOrdinal("ItemCourseHoursStudentID"); 
			if(!reader.IsDBNull(itemCourseHoursStudentIDIndex))
			{
				tr_ItemCourseHoursStudent.ItemCourseHoursStudentID= reader.GetGuid(itemCourseHoursStudentIDIndex);
			}
			
			int itemCourseHoursIDIndex = reader.GetOrdinal("ItemCourseHoursID"); 
			if(!reader.IsDBNull(itemCourseHoursIDIndex))
			{
				tr_ItemCourseHoursStudent.ItemCourseHoursID= reader.GetGuid(itemCourseHoursIDIndex);
			}
			
			int studentCourseIndex = reader.GetOrdinal("StudentCourse"); 
			if(!reader.IsDBNull(studentCourseIndex))
			{
				tr_ItemCourseHoursStudent.StudentCourse= reader.GetGuid(studentCourseIndex);
			}
			
			int signinTypeIDIndex = reader.GetOrdinal("SigninTypeID"); 
			if(!reader.IsDBNull(signinTypeIDIndex))
			{
				tr_ItemCourseHoursStudent.SigninTypeID= reader.GetInt32(signinTypeIDIndex);
			}
			
			int lawlessnessIDIndex = reader.GetOrdinal("LawlessnessID"); 
			if(!reader.IsDBNull(lawlessnessIDIndex))
			{
				tr_ItemCourseHoursStudent.LawlessnessID= reader.GetInt32(lawlessnessIDIndex);
			}
			
			int signinTimeIndex = reader.GetOrdinal("SigninTime"); 
			if(!reader.IsDBNull(signinTimeIndex))
			{
				tr_ItemCourseHoursStudent.SigninTime= reader.GetDateTime(signinTimeIndex);
			}
			
			int signinReasonIndex = reader.GetOrdinal("SigninReason"); 
			if(!reader.IsDBNull(signinReasonIndex))
			{
				tr_ItemCourseHoursStudent.SigninReason= reader.GetString(signinReasonIndex);
			}
			
			int leaveMinutesIndex = reader.GetOrdinal("LeaveMinutes"); 
			if(!reader.IsDBNull(leaveMinutesIndex))
			{
				tr_ItemCourseHoursStudent.LeaveMinutes= reader.GetInt32(leaveMinutesIndex);
			}
			
			int isLeaveIndex = reader.GetOrdinal("IsLeave"); 
			if(!reader.IsDBNull(isLeaveIndex))
			{
				tr_ItemCourseHoursStudent.IsLeave= reader.GetBoolean(isLeaveIndex);
			}
			
			int leaveReasonIndex = reader.GetOrdinal("LeaveReason"); 
			if(!reader.IsDBNull(leaveReasonIndex))
			{
				tr_ItemCourseHoursStudent.LeaveReason= reader.GetString(leaveReasonIndex);
			}
			
			int leaveTimeIndex = reader.GetOrdinal("LeaveTime"); 
			if(!reader.IsDBNull(leaveTimeIndex))
			{
				tr_ItemCourseHoursStudent.LeaveTime= reader.GetDateTime(leaveTimeIndex);
			}
			
			int auditStatusIndex = reader.GetOrdinal("AuditStatus"); 
			if(!reader.IsDBNull(auditStatusIndex))
			{
				tr_ItemCourseHoursStudent.AuditStatus= reader.GetInt32(auditStatusIndex);
			}
			
			int auditUserIndex = reader.GetOrdinal("AuditUser"); 
			if(!reader.IsDBNull(auditUserIndex))
			{
				tr_ItemCourseHoursStudent.AuditUser= reader.GetString(auditUserIndex);
			}
			
			int auditTimeIndex = reader.GetOrdinal("AuditTime"); 
			if(!reader.IsDBNull(auditTimeIndex))
			{
				tr_ItemCourseHoursStudent.AuditTime= reader.GetDateTime(auditTimeIndex);
			}
			
			int auditOpinionIndex = reader.GetOrdinal("AuditOpinion"); 
			if(!reader.IsDBNull(auditOpinionIndex))
			{
				tr_ItemCourseHoursStudent.AuditOpinion= reader.GetString(auditOpinionIndex);
			}
			
			int createTimeIndex = reader.GetOrdinal("CreateTime"); 
			if(!reader.IsDBNull(createTimeIndex))
			{
				tr_ItemCourseHoursStudent.CreateTime= reader.GetDateTime(createTimeIndex);
			}
			
			int createUserIDIndex = reader.GetOrdinal("CreateUserID"); 
			if(!reader.IsDBNull(createUserIDIndex))
			{
				tr_ItemCourseHoursStudent.CreateUserID= reader.GetInt32(createUserIDIndex);
			}
			
			int createUserIndex = reader.GetOrdinal("CreateUser"); 
			if(!reader.IsDBNull(createUserIndex))
			{
				tr_ItemCourseHoursStudent.CreateUser= reader.GetString(createUserIndex);
			}
			
			int modifyTimeIndex = reader.GetOrdinal("ModifyTime"); 
			if(!reader.IsDBNull(modifyTimeIndex))
			{
				tr_ItemCourseHoursStudent.ModifyTime= reader.GetDateTime(modifyTimeIndex);
			}
			
			int modifyUserIndex = reader.GetOrdinal("ModifyUser"); 
			if(!reader.IsDBNull(modifyUserIndex))
			{
				tr_ItemCourseHoursStudent.ModifyUser= reader.GetString(modifyUserIndex);
			}
			
			int remarkIndex = reader.GetOrdinal("Remark"); 
			if(!reader.IsDBNull(remarkIndex))
			{
				tr_ItemCourseHoursStudent.Remark= reader.GetString(remarkIndex);
			}
			
			return tr_ItemCourseHoursStudent; 
		}
	}
}