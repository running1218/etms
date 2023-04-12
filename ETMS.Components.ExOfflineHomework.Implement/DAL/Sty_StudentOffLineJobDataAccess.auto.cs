//==================================================================================================
//Version 1.0, auto-generated.
//Generated By xueyb.
//Date: 2012-4-16 19:58:03.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1), a product of ZhouYonghua(Zhou_Yonghua@163.com).
//==================================================================================================

using System;
using System.Data;

using System.Data.SqlClient;
using ETMS.Utility.Data;

using ETMS.Components.ExOfflineHomework.API.Entity;

namespace ETMS.Components.ExOfflineHomework.Implement.DAL
{
    /// <summary>
    /// ѧ��������ҵ�����ݷ���
    /// </summary>
    public partial class Sty_StudentOffLineJobDataAccess
	{
		/// <summary>
		/// ����
		/// </summary>
		public void Add(Sty_StudentOffLineJob sty_StudentOffLineJob)
		{
			string commandName = "dbo.Pr_Sty_StudentOffLineJob_Insert";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@StudentJobID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@ItemCourseOffLineJobID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@UserID", SqlDbType.Int),
					new SqlParameter("@StudentDownloadTime", SqlDbType.DateTime),
					new SqlParameter("@UploadTime", SqlDbType.DateTime),
					new SqlParameter("@UploadFileName", SqlDbType.NVarChar, 200),
					new SqlParameter("@UploadFilePath", SqlDbType.NVarChar, 256),
					new SqlParameter("@MarkFileName", SqlDbType.NVarChar, 200),
					new SqlParameter("@MarkFilePath", SqlDbType.NVarChar, 256),
					new SqlParameter("@EvaluationTime", SqlDbType.DateTime),
					new SqlParameter("@EvaluationUser", SqlDbType.NVarChar, 128),
					new SqlParameter("@Evaluation", SqlDbType.NVarChar, -1),
					new SqlParameter("@Score", SqlDbType.Decimal, 5, ParameterDirection.Input, true, 5, 1, String.Empty, DataRowVersion.Default, null),
					new SqlParameter("@StudentJobStatus", SqlDbType.Int),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUserID", SqlDbType.Int),
					new SqlParameter("@Remark", SqlDbType.NVarChar, -1)
					};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
			}
			
			parms[0].Value = sty_StudentOffLineJob.StudentJobID;
			parms[1].Value = sty_StudentOffLineJob.ItemCourseOffLineJobID;
			parms[2].Value = sty_StudentOffLineJob.UserID;
			parms[3].Value = sty_StudentOffLineJob.StudentDownloadTime;
			parms[4].Value = sty_StudentOffLineJob.UploadTime;
			if (sty_StudentOffLineJob.UploadFileName!= null){ parms[5].Value = sty_StudentOffLineJob.UploadFileName; } else { parms[5].Value = DBNull.Value; }
			if (sty_StudentOffLineJob.UploadFilePath!= null){ parms[6].Value = sty_StudentOffLineJob.UploadFilePath; } else { parms[6].Value = DBNull.Value; }
			if (sty_StudentOffLineJob.MarkFileName!= null){ parms[7].Value = sty_StudentOffLineJob.MarkFileName; } else { parms[7].Value = DBNull.Value; }
			if (sty_StudentOffLineJob.MarkFilePath!= null){ parms[8].Value = sty_StudentOffLineJob.MarkFilePath; } else { parms[8].Value = DBNull.Value; }
			parms[9].Value = sty_StudentOffLineJob.EvaluationTime;
			if (sty_StudentOffLineJob.EvaluationUser!= null){ parms[10].Value = sty_StudentOffLineJob.EvaluationUser; } else { parms[10].Value = DBNull.Value; }
			if (sty_StudentOffLineJob.Evaluation!= null){ parms[11].Value = sty_StudentOffLineJob.Evaluation; } else { parms[11].Value = DBNull.Value; }
			parms[12].Value = sty_StudentOffLineJob.Score;
			parms[13].Value = sty_StudentOffLineJob.StudentJobStatus;
			parms[14].Value = sty_StudentOffLineJob.CreateTime;
			parms[15].Value = sty_StudentOffLineJob.CreateUserID;
			if (sty_StudentOffLineJob.Remark!= null){ parms[16].Value = sty_StudentOffLineJob.Remark; } else { parms[16].Value = DBNull.Value; }
			#endregion
			SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
			
		}
		
		/// <summary>
		/// ɾ��
		/// </summary>
		public void Remove(Guid studentJobID)
		{
			string commandName = "dbo.Pr_Sty_StudentOffLineJob_Delete";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@StudentJobID", SqlDbType.UniqueIdentifier)
				};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
			}
			
			parms[0].Value = studentJobID;
			
			#endregion
			SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
		}
		
		/// <summary>
		/// ����
		/// </summary>
		public void Save(Sty_StudentOffLineJob sty_StudentOffLineJob)
		{
			string commandName = "dbo.Pr_Sty_StudentOffLineJob_Update";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@StudentJobID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@ItemCourseOffLineJobID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@UserID", SqlDbType.Int),
					new SqlParameter("@StudentDownloadTime", SqlDbType.DateTime),
					new SqlParameter("@UploadTime", SqlDbType.DateTime),
					new SqlParameter("@UploadFileName", SqlDbType.NVarChar, 200),
					new SqlParameter("@UploadFilePath", SqlDbType.NVarChar, 256),
					new SqlParameter("@MarkFileName", SqlDbType.NVarChar, 200),
					new SqlParameter("@MarkFilePath", SqlDbType.NVarChar, 256),
					new SqlParameter("@EvaluationTime", SqlDbType.DateTime),
					new SqlParameter("@EvaluationUser", SqlDbType.NVarChar, 128),
					new SqlParameter("@Evaluation", SqlDbType.NVarChar, -1),
					new SqlParameter("@Score", SqlDbType.Decimal, 5, ParameterDirection.Input, true, 5, 1, String.Empty, DataRowVersion.Default, null),
					new SqlParameter("@StudentJobStatus", SqlDbType.Int),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUserID", SqlDbType.Int),
					new SqlParameter("@Remark", SqlDbType.NVarChar, -1)
				};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
			}
			
			parms[0].Value = sty_StudentOffLineJob.StudentJobID;
			parms[1].Value = sty_StudentOffLineJob.ItemCourseOffLineJobID;
			parms[2].Value = sty_StudentOffLineJob.UserID;
			parms[3].Value = sty_StudentOffLineJob.StudentDownloadTime;
			parms[4].Value = sty_StudentOffLineJob.UploadTime;
			if (sty_StudentOffLineJob.UploadFileName!= null){ parms[5].Value = sty_StudentOffLineJob.UploadFileName; } else { parms[5].Value = DBNull.Value; }
			if (sty_StudentOffLineJob.UploadFilePath!= null){ parms[6].Value = sty_StudentOffLineJob.UploadFilePath; } else { parms[6].Value = DBNull.Value; }
			if (sty_StudentOffLineJob.MarkFileName!= null){ parms[7].Value = sty_StudentOffLineJob.MarkFileName; } else { parms[7].Value = DBNull.Value; }
			if (sty_StudentOffLineJob.MarkFilePath!= null){ parms[8].Value = sty_StudentOffLineJob.MarkFilePath; } else { parms[8].Value = DBNull.Value; }
			parms[9].Value = sty_StudentOffLineJob.EvaluationTime;
			if (sty_StudentOffLineJob.EvaluationUser!= null){ parms[10].Value = sty_StudentOffLineJob.EvaluationUser; } else { parms[10].Value = DBNull.Value; }
			if (sty_StudentOffLineJob.Evaluation!= null){ parms[11].Value = sty_StudentOffLineJob.Evaluation; } else { parms[11].Value = DBNull.Value; }
			parms[12].Value = sty_StudentOffLineJob.Score;
			parms[13].Value = sty_StudentOffLineJob.StudentJobStatus;
			parms[14].Value = sty_StudentOffLineJob.CreateTime;
			parms[15].Value = sty_StudentOffLineJob.CreateUserID;
			if (sty_StudentOffLineJob.Remark!= null){ parms[16].Value = sty_StudentOffLineJob.Remark; } else { parms[16].Value = DBNull.Value; }
			#endregion
			SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
		}
		
		/// <summary>
		/// ���ݱ�ʶ��ȡ����
		/// </summary>
		public Sty_StudentOffLineJob GetById(Guid studentJobID)
		{
			Sty_StudentOffLineJob sty_StudentOffLineJob = null;
			
			string commandName = "dbo.Pr_Sty_StudentOffLineJob_GetByPK";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@StudentJobID", SqlDbType.UniqueIdentifier)
				};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
			}
			
			parms[0].Value = studentJobID;
			
			#endregion
			using (SqlDataReader dataReader = SqlHelper.ExecuteReader(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms))
			{
				if (dataReader.Read())
				{
					sty_StudentOffLineJob = PopulateSty_StudentOffLineJobFromDataReader(dataReader);
				}
			}
			
			return sty_StudentOffLineJob;
		}				
		
		/// <summary>
		/// ���ݲ�����ȡ�����б�����ҳ��������
		/// </summary>
		public DataTable GetPagedList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
		{			
			string commandName = "dbo.Pr_Sty_StudentOffLineJob_GetPagedList";
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
		/// ��DataReader�ж�ȡ���ݵ�ҵ�����
		/// </summary>
		private Sty_StudentOffLineJob PopulateSty_StudentOffLineJobFromDataReader(SqlDataReader reader)
		{
			Sty_StudentOffLineJob sty_StudentOffLineJob = new Sty_StudentOffLineJob();
			
			int studentJobIDIndex = reader.GetOrdinal("StudentJobID"); 
			if(!reader.IsDBNull(studentJobIDIndex))
			{
				sty_StudentOffLineJob.StudentJobID= reader.GetGuid(studentJobIDIndex);
			}
			
			int itemCourseOffLineJobIDIndex = reader.GetOrdinal("ItemCourseOffLineJobID"); 
			if(!reader.IsDBNull(itemCourseOffLineJobIDIndex))
			{
				sty_StudentOffLineJob.ItemCourseOffLineJobID= reader.GetGuid(itemCourseOffLineJobIDIndex);
			}
			
			int userIDIndex = reader.GetOrdinal("UserID"); 
			if(!reader.IsDBNull(userIDIndex))
			{
				sty_StudentOffLineJob.UserID= reader.GetInt32(userIDIndex);
			}
			
			int studentDownloadTimeIndex = reader.GetOrdinal("StudentDownloadTime"); 
			if(!reader.IsDBNull(studentDownloadTimeIndex))
			{
				sty_StudentOffLineJob.StudentDownloadTime= reader.GetDateTime(studentDownloadTimeIndex);
			}
			
			int uploadTimeIndex = reader.GetOrdinal("UploadTime"); 
			if(!reader.IsDBNull(uploadTimeIndex))
			{
				sty_StudentOffLineJob.UploadTime= reader.GetDateTime(uploadTimeIndex);
			}
			
			int uploadFileNameIndex = reader.GetOrdinal("UploadFileName"); 
			if(!reader.IsDBNull(uploadFileNameIndex))
			{
				sty_StudentOffLineJob.UploadFileName= reader.GetString(uploadFileNameIndex);
			}
			
			int uploadFilePathIndex = reader.GetOrdinal("UploadFilePath"); 
			if(!reader.IsDBNull(uploadFilePathIndex))
			{
				sty_StudentOffLineJob.UploadFilePath= reader.GetString(uploadFilePathIndex);
			}
			
			int markFileNameIndex = reader.GetOrdinal("MarkFileName"); 
			if(!reader.IsDBNull(markFileNameIndex))
			{
				sty_StudentOffLineJob.MarkFileName= reader.GetString(markFileNameIndex);
			}
			
			int markFilePathIndex = reader.GetOrdinal("MarkFilePath"); 
			if(!reader.IsDBNull(markFilePathIndex))
			{
				sty_StudentOffLineJob.MarkFilePath= reader.GetString(markFilePathIndex);
			}
			
			int evaluationTimeIndex = reader.GetOrdinal("EvaluationTime"); 
			if(!reader.IsDBNull(evaluationTimeIndex))
			{
				sty_StudentOffLineJob.EvaluationTime= reader.GetDateTime(evaluationTimeIndex);
			}
			
			int evaluationUserIndex = reader.GetOrdinal("EvaluationUser"); 
			if(!reader.IsDBNull(evaluationUserIndex))
			{
				sty_StudentOffLineJob.EvaluationUser= reader.GetString(evaluationUserIndex);
			}
			
			int evaluationIndex = reader.GetOrdinal("Evaluation"); 
			if(!reader.IsDBNull(evaluationIndex))
			{
				sty_StudentOffLineJob.Evaluation= reader.GetString(evaluationIndex);
			}
			
			int scoreIndex = reader.GetOrdinal("Score"); 
			if(!reader.IsDBNull(scoreIndex))
			{
				sty_StudentOffLineJob.Score= reader.GetDecimal(scoreIndex);
			}
			
			int studentJobStatusIndex = reader.GetOrdinal("StudentJobStatus"); 
			if(!reader.IsDBNull(studentJobStatusIndex))
			{
				sty_StudentOffLineJob.StudentJobStatus= reader.GetInt32(studentJobStatusIndex);
			}
			
			int createTimeIndex = reader.GetOrdinal("CreateTime"); 
			if(!reader.IsDBNull(createTimeIndex))
			{
				sty_StudentOffLineJob.CreateTime= reader.GetDateTime(createTimeIndex);
			}
			
			int createUserIDIndex = reader.GetOrdinal("CreateUserID"); 
			if(!reader.IsDBNull(createUserIDIndex))
			{
				sty_StudentOffLineJob.CreateUserID= reader.GetInt32(createUserIDIndex);
			}
			
			int remarkIndex = reader.GetOrdinal("Remark"); 
			if(!reader.IsDBNull(remarkIndex))
			{
				sty_StudentOffLineJob.Remark= reader.GetString(remarkIndex);
			}
			
			return sty_StudentOffLineJob; 
		}
	}
}