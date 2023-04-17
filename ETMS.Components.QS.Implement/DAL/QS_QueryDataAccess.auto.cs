//==================================================================================================
//Version 1.0, auto-generated.
//Generated By huangzf.
//Date: 2013/1/29 13:38:26.
//==================================================================================================
//==================================================================================================
//This file is generated by CodeGenerator(Ver 2.1)
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
    /// 问卷调查表数据访问
    /// </summary>
    public partial class QS_QueryDataAccess
	{
		/// <summary>
		/// 增加
		/// </summary>
		public void Add(QS_Query qS_Query)
		{
			string commandName = "dbo.Pr_QS_Query_Insert";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@QueryID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@PollTypeID", SqlDbType.Int),
					new SqlParameter("@QueryName", SqlDbType.NVarChar, 2048),
					new SqlParameter("@Header", SqlDbType.NVarChar, 2048),
					new SqlParameter("@TitlePrefix", SqlDbType.NVarChar, 20),
					new SqlParameter("@IsDisplayColumn", SqlDbType.Bit),
					new SqlParameter("@Location", SqlDbType.Int),
					new SqlParameter("@OrganizationID", SqlDbType.Int),
					new SqlParameter("@BeginTime", SqlDbType.DateTime),
					new SqlParameter("@EndTime", SqlDbType.DateTime),
					new SqlParameter("@Footer", SqlDbType.NVarChar, 2048),
					new SqlParameter("@QueryStatus", SqlDbType.SmallInt),
					new SqlParameter("@IsAllSave", SqlDbType.Bit),
					new SqlParameter("@IsTitleNoSort", SqlDbType.Bit),
					new SqlParameter("@IsRepeat", SqlDbType.Bit),
					new SqlParameter("@IsDisplayResult", SqlDbType.Bit),
					new SqlParameter("@IsTemplate", SqlDbType.Bit),
					new SqlParameter("@IsPublish", SqlDbType.Bit),
					new SqlParameter("@IsHasScore", SqlDbType.Bit),
					new SqlParameter("@DutyUser", SqlDbType.NVarChar, 100),
					new SqlParameter("@CreateUserID", SqlDbType.Int),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUser", SqlDbType.NVarChar, 128),
					new SqlParameter("@ModifyTime", SqlDbType.DateTime),
					new SqlParameter("@ModifyUser", SqlDbType.NVarChar, 128),
					new SqlParameter("@Remark", SqlDbType.NVarChar, -1),
					new SqlParameter("@QueryHTML", SqlDbType.NVarChar, -1),
					new SqlParameter("@FileName", SqlDbType.NVarChar, 2048),
					new SqlParameter("@FilePath", SqlDbType.NVarChar, 2048)
					};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
			}
			
			parms[0].Value = qS_Query.QueryID;
			parms[1].Value = qS_Query.PollTypeID;
			if (qS_Query.QueryName!= null){ parms[2].Value = qS_Query.QueryName; } else { parms[2].Value = DBNull.Value; }
			if (qS_Query.Header!= null){ parms[3].Value = qS_Query.Header; } else { parms[3].Value = DBNull.Value; }
			if (qS_Query.TitlePrefix!= null){ parms[4].Value = qS_Query.TitlePrefix; } else { parms[4].Value = DBNull.Value; }
			parms[5].Value = qS_Query.IsDisplayColumn;
			parms[6].Value = qS_Query.Location;
			parms[7].Value = qS_Query.OrganizationID;
			parms[8].Value = qS_Query.BeginTime;
			parms[9].Value = qS_Query.EndTime;
			if (qS_Query.Footer!= null){ parms[10].Value = qS_Query.Footer; } else { parms[10].Value = DBNull.Value; }
			parms[11].Value = qS_Query.QueryStatus;
			parms[12].Value = qS_Query.IsAllSave;
			parms[13].Value = qS_Query.IsTitleNoSort;
			parms[14].Value = qS_Query.IsRepeat;
			parms[15].Value = qS_Query.IsDisplayResult;
			parms[16].Value = qS_Query.IsTemplate;
			parms[17].Value = qS_Query.IsPublish;
			parms[18].Value = qS_Query.IsHasScore;
			if (qS_Query.DutyUser!= null){ parms[19].Value = qS_Query.DutyUser; } else { parms[19].Value = DBNull.Value; }
			parms[20].Value = qS_Query.CreateUserID;
			parms[21].Value = qS_Query.CreateTime;
			if (qS_Query.CreateUser!= null){ parms[22].Value = qS_Query.CreateUser; } else { parms[22].Value = DBNull.Value; }
			parms[23].Value = qS_Query.ModifyTime;
			if (qS_Query.ModifyUser!= null){ parms[24].Value = qS_Query.ModifyUser; } else { parms[24].Value = DBNull.Value; }
			if (qS_Query.Remark!= null){ parms[25].Value = qS_Query.Remark; } else { parms[25].Value = DBNull.Value; }
			if (qS_Query.QueryHTML!= null){ parms[26].Value = qS_Query.QueryHTML; } else { parms[26].Value = DBNull.Value; }
			if (qS_Query.FileName!= null){ parms[27].Value = qS_Query.FileName; } else { parms[27].Value = DBNull.Value; }
			if (qS_Query.FilePath!= null){ parms[28].Value = qS_Query.FilePath; } else { parms[28].Value = DBNull.Value; }
			#endregion
			SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
			
		}
		
		/// <summary>
		/// 删除
		/// </summary>
		public void Remove(Guid queryID)
		{
			string commandName = "dbo.Pr_QS_Query_Delete";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@QueryID", SqlDbType.UniqueIdentifier)
				};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
			}
			
			parms[0].Value = queryID;
			
			#endregion
			SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
		}
		
		/// <summary>
		/// 保存
		/// </summary>
		public void Save(QS_Query qS_Query)
		{
			string commandName = "dbo.Pr_QS_Query_Update";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSWrite, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@QueryID", SqlDbType.UniqueIdentifier),
					new SqlParameter("@PollTypeID", SqlDbType.Int),
					new SqlParameter("@QueryName", SqlDbType.NVarChar, 2048),
					new SqlParameter("@Header", SqlDbType.NVarChar, 2048),
					new SqlParameter("@TitlePrefix", SqlDbType.NVarChar, 20),
					new SqlParameter("@IsDisplayColumn", SqlDbType.Bit),
					new SqlParameter("@Location", SqlDbType.Int),
					new SqlParameter("@OrganizationID", SqlDbType.Int),
					new SqlParameter("@BeginTime", SqlDbType.DateTime),
					new SqlParameter("@EndTime", SqlDbType.DateTime),
					new SqlParameter("@Footer", SqlDbType.NVarChar, 2048),
					new SqlParameter("@QueryStatus", SqlDbType.SmallInt),
					new SqlParameter("@IsAllSave", SqlDbType.Bit),
					new SqlParameter("@IsTitleNoSort", SqlDbType.Bit),
					new SqlParameter("@IsRepeat", SqlDbType.Bit),
					new SqlParameter("@IsDisplayResult", SqlDbType.Bit),
					new SqlParameter("@IsTemplate", SqlDbType.Bit),
					new SqlParameter("@IsPublish", SqlDbType.Bit),
					new SqlParameter("@IsHasScore", SqlDbType.Bit),
					new SqlParameter("@DutyUser", SqlDbType.NVarChar, 100),
					new SqlParameter("@CreateUserID", SqlDbType.Int),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateUser", SqlDbType.NVarChar, 128),
					new SqlParameter("@ModifyTime", SqlDbType.DateTime),
					new SqlParameter("@ModifyUser", SqlDbType.NVarChar, 128),
					new SqlParameter("@Remark", SqlDbType.NVarChar, -1),
					new SqlParameter("@QueryHTML", SqlDbType.NVarChar, -1),
					new SqlParameter("@FileName", SqlDbType.NVarChar, 2048),
					new SqlParameter("@FilePath", SqlDbType.NVarChar, 2048)
				};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSWrite, commandName, parms);
			}
			
			parms[0].Value = qS_Query.QueryID;
			parms[1].Value = qS_Query.PollTypeID;
			if (qS_Query.QueryName!= null){ parms[2].Value = qS_Query.QueryName; } else { parms[2].Value = DBNull.Value; }
			if (qS_Query.Header!= null){ parms[3].Value = qS_Query.Header; } else { parms[3].Value = DBNull.Value; }
			if (qS_Query.TitlePrefix!= null){ parms[4].Value = qS_Query.TitlePrefix; } else { parms[4].Value = DBNull.Value; }
			parms[5].Value = qS_Query.IsDisplayColumn;
			parms[6].Value = qS_Query.Location;
			parms[7].Value = qS_Query.OrganizationID;
			parms[8].Value = qS_Query.BeginTime;
			parms[9].Value = qS_Query.EndTime;
			if (qS_Query.Footer!= null){ parms[10].Value = qS_Query.Footer; } else { parms[10].Value = DBNull.Value; }
			parms[11].Value = qS_Query.QueryStatus;
			parms[12].Value = qS_Query.IsAllSave;
			parms[13].Value = qS_Query.IsTitleNoSort;
			parms[14].Value = qS_Query.IsRepeat;
			parms[15].Value = qS_Query.IsDisplayResult;
			parms[16].Value = qS_Query.IsTemplate;
			parms[17].Value = qS_Query.IsPublish;
			parms[18].Value = qS_Query.IsHasScore;
			if (qS_Query.DutyUser!= null){ parms[19].Value = qS_Query.DutyUser; } else { parms[19].Value = DBNull.Value; }
			parms[20].Value = qS_Query.CreateUserID;
			parms[21].Value = qS_Query.CreateTime;
			if (qS_Query.CreateUser!= null){ parms[22].Value = qS_Query.CreateUser; } else { parms[22].Value = DBNull.Value; }
			parms[23].Value = qS_Query.ModifyTime;
			if (qS_Query.ModifyUser!= null){ parms[24].Value = qS_Query.ModifyUser; } else { parms[24].Value = DBNull.Value; }
			if (qS_Query.Remark!= null){ parms[25].Value = qS_Query.Remark; } else { parms[25].Value = DBNull.Value; }
			if (qS_Query.QueryHTML!= null){ parms[26].Value = qS_Query.QueryHTML; } else { parms[26].Value = DBNull.Value; }
			if (qS_Query.FileName!= null){ parms[27].Value = qS_Query.FileName; } else { parms[27].Value = DBNull.Value; }
			if (qS_Query.FilePath!= null){ parms[28].Value = qS_Query.FilePath; } else { parms[28].Value = DBNull.Value; }
			#endregion
			SqlHelper.ExecuteNonQuery(ConnectionString.ETMSWrite, CommandType.StoredProcedure, commandName, parms);
		}
		
		/// <summary>
		/// 根据标识获取对象
		/// </summary>
		public QS_Query GetById(Guid queryID)
		{
			QS_Query qS_Query = null;
			
			string commandName = "dbo.Pr_QS_Query_GetByPK";
			#region Parameters
			SqlParameter[] parms = SqlHelperParameterCache.GetCachedParameterSet(ConnectionString.ETMSRead, commandName);
			if (parms == null)
			{
				parms = new SqlParameter[] {
					new SqlParameter("@QueryID", SqlDbType.UniqueIdentifier)
				};
				SqlHelperParameterCache.CacheParameterSet(ConnectionString.ETMSRead, commandName, parms);
			}
			
			parms[0].Value = queryID;
			
			#endregion
			using (SqlDataReader dataReader = SqlHelper.ExecuteReader(ConnectionString.ETMSRead, CommandType.StoredProcedure, commandName, parms))
			{
				if (dataReader.Read())
				{
					qS_Query = PopulateQS_QueryFromDataReader(dataReader);
				}
			}
			
			return qS_Query;
		}				
		
		/// <summary>
		/// 根据参数获取对象列表（分页，可排序）
		/// </summary>
		public DataTable GetPagedList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
		{			
			string commandName = "dbo.Pr_QS_Query_GetPagedList";
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
		public IList<QS_Query> GetEntityList(int pageIndex, int pageSize, string sortExpression, string criteria, out int totalRecords)
		{			
            IList<QS_Query> list=new List<QS_Query>();
			string commandName = "dbo.Pr_QS_Query_GetPagedList";
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
					list.Add(PopulateQS_QueryFromDataReader(dataReader));
				}
			}			
			totalRecords = (int)parms[4].Value;
			return list;
		}
		
		/// <summary>
		/// 从DataReader中读取数据到业务对象
		/// </summary>
		private QS_Query PopulateQS_QueryFromDataReader(SqlDataReader reader)
		{
			QS_Query qS_Query = new QS_Query();
			
			int queryIDIndex = reader.GetOrdinal("QueryID"); 
			if(!reader.IsDBNull(queryIDIndex))
			{
				qS_Query.QueryID= reader.GetGuid(queryIDIndex);
			}
			
			int pollTypeIDIndex = reader.GetOrdinal("PollTypeID"); 
			if(!reader.IsDBNull(pollTypeIDIndex))
			{
				qS_Query.PollTypeID= reader.GetInt32(pollTypeIDIndex);
			}
			
			int queryNameIndex = reader.GetOrdinal("QueryName"); 
			if(!reader.IsDBNull(queryNameIndex))
			{
				qS_Query.QueryName= reader.GetString(queryNameIndex);
			}
			
			int headerIndex = reader.GetOrdinal("Header"); 
			if(!reader.IsDBNull(headerIndex))
			{
				qS_Query.Header= reader.GetString(headerIndex);
			}
			
			int titlePrefixIndex = reader.GetOrdinal("TitlePrefix"); 
			if(!reader.IsDBNull(titlePrefixIndex))
			{
				qS_Query.TitlePrefix= reader.GetString(titlePrefixIndex);
			}
			
			int isDisplayColumnIndex = reader.GetOrdinal("IsDisplayColumn"); 
			if(!reader.IsDBNull(isDisplayColumnIndex))
			{
				qS_Query.IsDisplayColumn= reader.GetBoolean(isDisplayColumnIndex);
			}
			
			int locationIndex = reader.GetOrdinal("Location"); 
			if(!reader.IsDBNull(locationIndex))
			{
				qS_Query.Location= reader.GetInt32(locationIndex);
			}
			
			int organizationIDIndex = reader.GetOrdinal("OrganizationID"); 
			if(!reader.IsDBNull(organizationIDIndex))
			{
				qS_Query.OrganizationID= reader.GetInt32(organizationIDIndex);
			}
			
			int beginTimeIndex = reader.GetOrdinal("BeginTime"); 
			if(!reader.IsDBNull(beginTimeIndex))
			{
				qS_Query.BeginTime= reader.GetDateTime(beginTimeIndex);
			}
			
			int endTimeIndex = reader.GetOrdinal("EndTime"); 
			if(!reader.IsDBNull(endTimeIndex))
			{
				qS_Query.EndTime= reader.GetDateTime(endTimeIndex);
			}
			
			int footerIndex = reader.GetOrdinal("Footer"); 
			if(!reader.IsDBNull(footerIndex))
			{
				qS_Query.Footer= reader.GetString(footerIndex);
			}
			
			int queryStatusIndex = reader.GetOrdinal("QueryStatus"); 
			if(!reader.IsDBNull(queryStatusIndex))
			{
				qS_Query.QueryStatus= reader.GetInt16(queryStatusIndex);
			}
			
			int isAllSaveIndex = reader.GetOrdinal("IsAllSave"); 
			if(!reader.IsDBNull(isAllSaveIndex))
			{
				qS_Query.IsAllSave= reader.GetBoolean(isAllSaveIndex);
			}
			
			int isTitleNoSortIndex = reader.GetOrdinal("IsTitleNoSort"); 
			if(!reader.IsDBNull(isTitleNoSortIndex))
			{
				qS_Query.IsTitleNoSort= reader.GetBoolean(isTitleNoSortIndex);
			}
			
			int isRepeatIndex = reader.GetOrdinal("IsRepeat"); 
			if(!reader.IsDBNull(isRepeatIndex))
			{
				qS_Query.IsRepeat= reader.GetBoolean(isRepeatIndex);
			}
			
			int isDisplayResultIndex = reader.GetOrdinal("IsDisplayResult"); 
			if(!reader.IsDBNull(isDisplayResultIndex))
			{
				qS_Query.IsDisplayResult= reader.GetBoolean(isDisplayResultIndex);
			}
			
			int isTemplateIndex = reader.GetOrdinal("IsTemplate"); 
			if(!reader.IsDBNull(isTemplateIndex))
			{
				qS_Query.IsTemplate= reader.GetBoolean(isTemplateIndex);
			}
			
			int isPublishIndex = reader.GetOrdinal("IsPublish"); 
			if(!reader.IsDBNull(isPublishIndex))
			{
				qS_Query.IsPublish= reader.GetBoolean(isPublishIndex);
			}
			
			int isHasScoreIndex = reader.GetOrdinal("IsHasScore"); 
			if(!reader.IsDBNull(isHasScoreIndex))
			{
				qS_Query.IsHasScore= reader.GetBoolean(isHasScoreIndex);
			}
			
			int dutyUserIndex = reader.GetOrdinal("DutyUser"); 
			if(!reader.IsDBNull(dutyUserIndex))
			{
				qS_Query.DutyUser= reader.GetString(dutyUserIndex);
			}
			
			int createUserIDIndex = reader.GetOrdinal("CreateUserID"); 
			if(!reader.IsDBNull(createUserIDIndex))
			{
				qS_Query.CreateUserID= reader.GetInt32(createUserIDIndex);
			}
			
			int createTimeIndex = reader.GetOrdinal("CreateTime"); 
			if(!reader.IsDBNull(createTimeIndex))
			{
				qS_Query.CreateTime= reader.GetDateTime(createTimeIndex);
			}
			
			int createUserIndex = reader.GetOrdinal("CreateUser"); 
			if(!reader.IsDBNull(createUserIndex))
			{
				qS_Query.CreateUser= reader.GetString(createUserIndex);
			}
			
			int modifyTimeIndex = reader.GetOrdinal("ModifyTime"); 
			if(!reader.IsDBNull(modifyTimeIndex))
			{
				qS_Query.ModifyTime= reader.GetDateTime(modifyTimeIndex);
			}
			
			int modifyUserIndex = reader.GetOrdinal("ModifyUser"); 
			if(!reader.IsDBNull(modifyUserIndex))
			{
				qS_Query.ModifyUser= reader.GetString(modifyUserIndex);
			}
			
			int remarkIndex = reader.GetOrdinal("Remark"); 
			if(!reader.IsDBNull(remarkIndex))
			{
				qS_Query.Remark= reader.GetString(remarkIndex);
			}
			
			int queryHTMLIndex = reader.GetOrdinal("QueryHTML"); 
			if(!reader.IsDBNull(queryHTMLIndex))
			{
				qS_Query.QueryHTML= reader.GetString(queryHTMLIndex);
			}
			
			int fileNameIndex = reader.GetOrdinal("FileName"); 
			if(!reader.IsDBNull(fileNameIndex))
			{
				qS_Query.FileName= reader.GetString(fileNameIndex);
			}
			
			int filePathIndex = reader.GetOrdinal("FilePath"); 
			if(!reader.IsDBNull(filePathIndex))
			{
				qS_Query.FilePath= reader.GetString(filePathIndex);
			}
			
			return qS_Query; 
		}
	}
}
