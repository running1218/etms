//==================================================================================================
//Version 1.0, auto-generated.
//Generated By liuyx.
//Date: 2012-3-29 9:04:04.
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
    /// ��Ŀ�γ�������ҵ�����ݷ���
    /// </summary>
    public partial class Res_ItemCourseOffLineJobDataAccess
	{
		/// <summary>
		/// ����
		/// </summary>
		public void Add(Res_ItemCourseOffLineJob res_ItemCourseOffLineJob)
		{
			string commandName = "dbo.Pr_Res_ItemCourseOffLineJob_Insert";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@ItemCourseOffLineJobID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@TrainingItemID", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@JobID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@TrainingItemCourseID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@IsUse", SqlDbType.Int),
					new SqlParameter("@BeginTime", SqlDbType.DateTime),
					new SqlParameter("@EndTime", SqlDbType.DateTime),
					new SqlParameter("@CreateTime", SqlDbType.DateTime)
					};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
			}
			
			parms[0].Value = res_ItemCourseOffLineJob.ItemCourseOffLineJobID;
            parms[1].Value = res_ItemCourseOffLineJob.TrainingItemID;
            parms[2].Value = res_ItemCourseOffLineJob.JobID;
			parms[3].Value = res_ItemCourseOffLineJob.TrainingItemCourseID;
			parms[4].Value = res_ItemCourseOffLineJob.IsUse;
			parms[5].Value = res_ItemCourseOffLineJob.BeginTime;
			parms[6].Value = res_ItemCourseOffLineJob.EndTime;
			parms[7].Value = res_ItemCourseOffLineJob.CreateTime;
			#endregion
			SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
			
		}
		
		/// <summary>
		/// ɾ��
		/// </summary>
		public void Remove(Guid itemCourseOffLineJobID)
		{
			string commandName = "dbo.Pr_Res_ItemCourseOffLineJob_Delete";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@ItemCourseOffLineJobID", SqlDbType.UniqueIdentifier)
				};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
			}
			
			parms[0].Value = itemCourseOffLineJobID;
			
			#endregion
			SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
		}
		
		/// <summary>
		/// ����
		/// </summary>
		public void Save(Res_ItemCourseOffLineJob res_ItemCourseOffLineJob)
		{
			string commandName = "dbo.Pr_Res_ItemCourseOffLineJob_Update";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@ItemCourseOffLineJobID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@JobID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@TrainingItemCourseID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@IsUse", SqlDbType.Int),
					new SqlParameter("@BeginTime", SqlDbType.DateTime),
					new SqlParameter("@EndTime", SqlDbType.DateTime),
					new SqlParameter("@CreateTime", SqlDbType.DateTime)
				};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
			}
			
			parms[0].Value = res_ItemCourseOffLineJob.ItemCourseOffLineJobID;
			parms[1].Value = res_ItemCourseOffLineJob.JobID;
			parms[2].Value = res_ItemCourseOffLineJob.TrainingItemCourseID;
			parms[3].Value = res_ItemCourseOffLineJob.IsUse;
			parms[4].Value = res_ItemCourseOffLineJob.BeginTime;
			parms[5].Value = res_ItemCourseOffLineJob.EndTime;
			parms[6].Value = res_ItemCourseOffLineJob.CreateTime;
			#endregion
			SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
		}
		
		/// <summary>
		/// ���ݱ�ʶ��ȡ����
		/// </summary>
		public Res_ItemCourseOffLineJob GetById(Guid itemCourseOffLineJobID)
		{
			Res_ItemCourseOffLineJob res_ItemCourseOffLineJob = null;
			
			string commandName = "dbo.Pr_Res_ItemCourseOffLineJob_GetByPK";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@ItemCourseOffLineJobID", SqlDbType.UniqueIdentifier)
				};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
			}
			
			parms[0].Value = itemCourseOffLineJobID;
			
			#endregion
			using (SqlDataReader dataReader = SqlHelper.ExecuteReader(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms))
			{
				if (dataReader.Read())
				{
					res_ItemCourseOffLineJob = PopulateRes_ItemCourseOffLineJobFromDataReader(dataReader);
				}
			}
			
			return res_ItemCourseOffLineJob;
		}
        /// <summary>
        /// ���ݱ�ʶ��ȡ����
        /// </summary>
        public DataTable GetByJobId(Guid JobId)
        {
            Res_ItemCourseOffLineJob res_ItemCourseOffLineJob = null;

            string commandName = "dbo.Pr_Res_ItemCourseOffLineJob_GetByJobId";
            #region Parameters
            SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
            if (parms == null)
            {
                parms = new SqlParameter[] {
                    new SqlParameter("@JobId", SqlDbType.UniqueIdentifier)
                };
                SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
            }

            parms[0].Value = JobId;

            #endregion
            return SqlHelper.ExecuteDataset(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms).Tables[0];
        }
        /// <summary>
        /// ���ݲ�����ȡ�����б�����ҳ��������
        /// </summary>
        public DataTable GetPagedList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
		{			
			string commandName = "dbo.Pr_Res_ItemCourseOffLineJob_GetPagedList";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@SortExpression", SqlDbType.NVarChar),
					new SqlParameter("@Criteria", SqlDbType.NVarChar),
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
		private Res_ItemCourseOffLineJob PopulateRes_ItemCourseOffLineJobFromDataReader(SqlDataReader reader)
		{
			Res_ItemCourseOffLineJob res_ItemCourseOffLineJob = new Res_ItemCourseOffLineJob();
			
			int itemCourseOffLineJobIDIndex = reader.GetOrdinal("ItemCourseOffLineJobID"); 
			if(!reader.IsDBNull(itemCourseOffLineJobIDIndex))
			{
				res_ItemCourseOffLineJob.ItemCourseOffLineJobID= reader.GetGuid(itemCourseOffLineJobIDIndex);
			}
			
			int jobIDIndex = reader.GetOrdinal("JobID"); 
			if(!reader.IsDBNull(jobIDIndex))
			{
				res_ItemCourseOffLineJob.JobID= reader.GetGuid(jobIDIndex);
			}
			
			int trainingItemCourseIDIndex = reader.GetOrdinal("TrainingItemCourseID"); 
			if(!reader.IsDBNull(trainingItemCourseIDIndex))
			{
				res_ItemCourseOffLineJob.TrainingItemCourseID= reader.GetGuid(trainingItemCourseIDIndex);
			}
			
			int isUseIndex = reader.GetOrdinal("IsUse"); 
			if(!reader.IsDBNull(isUseIndex))
			{
				res_ItemCourseOffLineJob.IsUse= reader.GetInt32(isUseIndex);
			}
			
			int beginTimeIndex = reader.GetOrdinal("BeginTime"); 
			if(!reader.IsDBNull(beginTimeIndex))
			{
				res_ItemCourseOffLineJob.BeginTime= reader.GetDateTime(beginTimeIndex);
			}
			
			int endTimeIndex = reader.GetOrdinal("EndTime"); 
			if(!reader.IsDBNull(endTimeIndex))
			{
				res_ItemCourseOffLineJob.EndTime= reader.GetDateTime(endTimeIndex);
			}
			
			int createTimeIndex = reader.GetOrdinal("CreateTime"); 
			if(!reader.IsDBNull(createTimeIndex))
			{
				res_ItemCourseOffLineJob.CreateTime= reader.GetDateTime(createTimeIndex);
			}
			
			return res_ItemCourseOffLineJob; 
		}
	}
}